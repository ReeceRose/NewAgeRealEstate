using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NARE.Application.Agent.Command.NewAgent;
using NARE.Application.Agent.Query.LoginAgent;

namespace NARE.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator) => _mediator = mediator;

        [HttpPost("Login")]
        public async Task<IActionResult> PostLoginAsync([FromBody] LoginAgentQuery loginAgentQuery) => Ok(new { token = await _mediator.Send(loginAgentQuery) });

        [HttpPost("Create")]
        public async Task<IActionResult> PostRegisterAsync([FromBody] NewAgentCommand newAgentCommand) => Ok(new { result = await _mediator.Send(newAgentCommand) });
    }
}