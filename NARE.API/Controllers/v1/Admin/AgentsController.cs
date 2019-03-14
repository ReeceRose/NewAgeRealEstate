using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NARE.Application.Agent.Command.AddAgentClaim;
using NARE.Application.Agent.Command.DisableAgent;
using NARE.Application.Agent.Command.EnableAgent;
using NARE.Application.Agent.Command.RemoveAgent;
using NARE.Application.Agent.Command.RemoveAgentClaim;
using NARE.Application.Agent.Command.ResetPassword;
using NARE.Application.Agent.Command.UpdateAgentInformation;
using NARE.Application.Agent.Query.GenerateResetPassword.Email;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Application.Agent.Query.GetAgentClaim;
using NARE.Application.Agent.Query.GetAgentCount;
using NARE.Application.Agent.Query.GetAgentDtoById;
using NARE.Application.Agent.Query.GetAllAgentsPaginated;
using NARE.Application.Agent.Query.SearchAgentsByEmail;
using NARE.Domain.Entities;

namespace NARE.API.Controllers.v1.Admin
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AgentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostAllAgentsAsync([FromBody] PaginationModel model) => Ok(new { result = await _mediator.Send(new GetAllAgentsPaginatedQuery(model)) });


        [HttpGet("Count")]
        public async Task<IActionResult> GetAgentCountAsync() => Ok(new { result = await _mediator.Send(new GetAgentCountQuery()) });

        // Agent specific actions

        [HttpGet("{AgentId}/Details")]
        public async Task<IActionResult> GetAgentDetailsByIdAsync(string agentId)
        {
            var agent = await _mediator.Send(new GetAgentDtoByIdQuery(agentId));
            return Ok(new { result = new { agent, claims = await _mediator.Send(new GetAgentClaimQuery(agent)) }});
        }

        [HttpGet("Search/{Email}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetAgentsDetailsByEmailAsync(string email) => Ok(new { result = await _mediator.Send(new SearchAgentsByEmailQuery(email)) });

        [HttpGet("{AgentId}/Enable")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetEnableAAgentByIdAsync(string agentId) => Ok(new { result = await _mediator.Send(new EnableAgentCommand(agentId)) });

        [HttpGet("{AgentId}/Disable")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetDisableAAgentByIdAsync(string agentId) => Ok(new { result = await _mediator.Send(new DisableAgentCommand(agentId)) });

        [HttpPost("{AgentId}/Update")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostUpdateAgentAsync([FromBody] UpdateAgentInformationCommand updateAgentInformationCommand) => Ok(new { result = await _mediator.Send(updateAgentInformationCommand) });

        [HttpGet("{AgentId}/Delete")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetRemoveAgentAsync(string agentId) => Ok(new { result = await _mediator.Send(new RemoveAgentCommand(agentId)) });

        [HttpPost("{AgentId}/AddClaim/{Claim}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostAddClaimAsync(string agentId, string claim) => Ok( new { result = await _mediator.Send(new AddAgentClaimCommand(await _mediator.Send(new GetAgentByIdQuery(agentId)), claim, "")) });

        [HttpPost("{AgentId}/RemoveClaim/{Claim}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostRemoveClaimAsync(string agentId, string claim) => Ok( new { result = await _mediator.Send(new RemoveAgentClaimCommand(await _mediator.Send(new GetAgentByIdQuery(agentId)), claim))} );

        [HttpPost("GenerateResetPasswordEmail")]
        public async Task<IActionResult> PostGenerateRestResetPasswordEmailAsync([FromBody] GenerateResetPasswordEmailQuery generateResetPasswordEmailQuery) => Ok(new { result = await _mediator.Send(generateResetPasswordEmailQuery) });

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> PostResetPasswordAsync([FromBody] ResetPasswordCommand resetPasswordCommand) => Ok(new { result = await _mediator.Send(resetPasswordCommand) });

    }
}