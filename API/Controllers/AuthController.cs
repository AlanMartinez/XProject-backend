using MediatR;
using Microsoft.AspNetCore.Mvc;
using XProject.API.Queries.Auth;

namespace XProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Authentication(AuthorizationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
