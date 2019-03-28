using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NARE.Application.Listing.Command.CreateListing;
using NARE.Application.Listing.Command.RemoveListing;
using NARE.Application.Listing.Command.UpdateListing;
using NARE.Application.Listing.Query.GetAgentListings;
using NARE.Application.Listing.Query.GetAllActiveListingsPaginated;
using NARE.Application.Listing.Query.GetAllListingsPaginated;
using NARE.Application.Listing.Query.GetFeaturedListings;
using NARE.Application.Listing.Query.GetListingById;
using NARE.Application.Listing.Query.GetListingCount;
using NARE.Application.Listing.Query.GetNewestListings;
using NARE.Application.Listing.Query.SearchActiveListings;
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
        public async Task<IActionResult> PostSearchActiveListingsAsync([FromBody] PaginationModel model) => Ok(new { result = await _mediator.Send(new GetAllActiveListingsPaginatedQuery(model)) });

        [HttpPost("Search")]
        public async Task<IActionResult> PostAllActiveListingsAsync([FromBody] SearchActiveListingsQuery searchActiveListingsQuery) => Ok(new { result = await _mediator.Send(searchActiveListingsQuery) });
        //        public async Task<IActionResult> PostAllActiveListingsAsync(PaginationModel paginationModel, SearchModel searchModel) => Ok(new { result = await _mediator.Send(new SearchActiveListingsQuery(paginationModel, searchModel)) });

        [HttpPost("AgentListings")]
        public async Task<IActionResult> PostAllAgentListingsAsync([FromBody] PaginationModel model)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return Ok(new { result = await _mediator.Send(new GetAgentListingsQuery(id, model)) });
        } 

        [HttpPost("AllListings")]
        public async Task<IActionResult> PostAllListingsAsync([FromBody] PaginationModel model) => Ok(new { result = await _mediator.Send(new GetAllListingsPaginatedQuery(model)) });

        [HttpGet("Featured/{Count}")]
        public async Task<IActionResult> GetFeaturedListingsAsync(int count) => Ok(new { result = await _mediator.Send(new GetFeaturedListingsQuery(count)) });

        [HttpGet("Newest/{Count}")]
        public async Task<IActionResult> GetNewestListingsAsync(int count) => Ok(new { result = await _mediator.Send(new GetNewestListingsQuery(count)) });

        [HttpGet("Count")]
        public async Task<IActionResult> GetListingCountAsync() => Ok(new { result = await _mediator.Send(new GetListingCountQuery(null)) });

        [HttpGet("AgentListingCount")]
        public async Task<IActionResult> GetAgentListingsCountAsync()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return Ok(new { result = await _mediator.Send(new GetListingCountQuery(id)) });
        }

        [HttpGet("{ListingId}/Details")]
        public async Task<IActionResult> GetListingByIdAsync(string listingId) => Ok(new { result = await _mediator.Send(new GetListingByIdQuery(Guid.Parse(listingId))) });

        [HttpPut("Create")]
        [Authorize]
        public async Task<IActionResult> PostCreateListingAsync([FromBody] CreateListingCommand createListingCommand)
        {
            createListingCommand.Listing.Agent.Id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return Ok(new { result = await _mediator.Send(createListingCommand) });
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> PostUpdateListingAsync([FromBody] UpdateListingCommand updateListingCommand) => Ok(new { result = await _mediator.Send(updateListingCommand) });

        [HttpDelete("Delete/{ListingId}")]
        [Authorize]
        public async Task<IActionResult> PostUpdateListingAsync(string listingId) => Ok(new { result = await _mediator.Send(new RemoveListingCommand(listingId)) });
    }
}