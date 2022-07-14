using MediatR;
using Microsoft.AspNetCore.Mvc;
using XProject.API.Commands.Security;

namespace XProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SecurityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser(SecurityRegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
