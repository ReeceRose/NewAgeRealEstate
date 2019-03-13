using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NARE.Application.Listing.Command.CreateListing;
using NARE.Application.Listing.Query.GetAllListingsPaginated;
using NARE.Application.Listing.Query.GetListingById;
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

        //        [HttpGet("Count")]
        //        public async Task<IActionResult> GetListingCountAsync() => Ok(new { result = await _mediator.Send(new GetListingCountQuery()) });

        [HttpGet]
        public async Task<IActionResult> GetListingByIdAsync([FromBody] Guid id) => Ok(new { result = await _mediator.Send(new GetListingByIdQuery(id)) });

        [HttpPost("Create")]
        public async Task<IActionResult> PostCreateListingAsync([FromBody] Listing listing) => Ok(new { result = await _mediator.Send(new CreateListingCommand(listing)) });
    }
}