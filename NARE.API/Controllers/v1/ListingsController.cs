using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NARE.Application.Listing.Command.CreateListing;
using NARE.Application.Listing.Command.UpdateListing;
using NARE.Application.Listing.Query.GetAllListingsPaginated;
using NARE.Application.Listing.Query.GetListingById;
using NARE.Application.Listing.Query.GetListingCount;
using NARE.Domain.Entities;

namespace NARE.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ListingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAllListingsAsync([FromBody] PaginationModel model) => Ok(new { result = await _mediator.Send(new GetAllListingsPaginatedQuery(model)) });

        [HttpGet("Count")]
        public async Task<IActionResult> GetListingCountAsync() => Ok(new { result = await _mediator.Send(new GetListingCountQuery()) });

        [HttpGet("{ListingId}/Details")]
        public async Task<IActionResult> GetListingByIdAsync(string listingId) => Ok(new { result = await _mediator.Send(new GetListingByIdQuery(Guid.Parse(listingId))) });

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> PostCreateListingAsync([FromBody] CreateListingCommand createListingCommand)
        {
            createListingCommand.Listing.Agent.Id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return Ok(new { result = await _mediator.Send(createListingCommand) });
        }

        [HttpPost("Update")]
        [Authorize]
        public async Task<IActionResult> PostUpdateListingAsync([FromBody] UpdateListingCommand updateListingCommand) => Ok(new { result = await _mediator.Send(updateListingCommand) });
    }
}