
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Commands;
using CoreCMS.Application.User.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    public class UserController : BaseController
    {
        [Route("Create")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Create([FromBody] RegisterCommand command)
        {
            CommandResult<string> result;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string,string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }

            result = await Mediator.Send(command);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("Get")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Get()
        {
            GetUserProfileQuery query = new GetUserProfileQuery() { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) };
            return Ok(await Mediator.Send(query));
        }

        [Route("Edit")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Edit([FromBody] EditProfileUserCommand command)
        {
            CommandResult<string> result;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string,string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }
            command.UserId = GetCurrentUserId();
            result = await Mediator.Send(command);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("ForgotPassword")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            CommandResult<string> result;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string,string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }
            result = await Mediator.Send(command);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("ChangePassword")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            CommandResult<string> result;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string,string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }
            command.UserId = GetCurrentUserId();
            result = await Mediator.Send(command);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}