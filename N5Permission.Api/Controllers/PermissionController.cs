using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Features.Permission.Commands;
using N5Permission.Application.Features.Permission.Queries;

namespace N5Permission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("request")]
        public async Task<IActionResult> CreateRequestPermission([FromBody] CreateRequestPermission requestPermission) 
        {
            var result = await _mediator.Send(new CreateRequestPermissionCommand(requestPermission));

            if (!result.Succeeded) 
                return BadRequest(result);
            
            return Ok(result);  
        }

        [HttpPut("modify")]
        public async Task<IActionResult> ModifyRequestPermission([FromBody] ModifyRequestPermission requestPermission)
        {
            var result = await _mediator.Send(new ModifyPermissionCommand(requestPermission));

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("getPermissions")]
        public async Task<IActionResult> GetPermissions(string permissionId)
        {
            var result = await _mediator.Send(new GetPermissionsQuery(permissionId));

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
