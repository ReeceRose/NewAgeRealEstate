using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NARE.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ListingController(IMediator mediator)
        {
            _mediator = mediator;
        }

//        [HttpGet]
//        public async Task<IActionResult> GetListingByIdAsync([FromBody] Guid id) => Ok(new { result = await _mediator.Send(new GetListingById(id)) });
    }
}