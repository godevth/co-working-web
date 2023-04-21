using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCMS.Application.IoTDevice.Commands;
using CoreCMS.Application.IoTDevice.Queries;
using CoreCMS.Application.IoTDeviceGroup.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
    public class IoTDeviceController : BaseController
    {
        [HttpGet("Select2DeviceGroup")]
        public async Task<IActionResult> DeviceGroup(Select2IoTDeviceGroupQuery model)
        {
            model.UserId = GetCurrentUserId();
            model.IsSuperAdmin = User.IsInRole("super_admin");
            var result = await Mediator.Send(model);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("Select2Device")]
        public async Task<IActionResult> Device(Select2IoTDeviceQuery model)
        {
            model.UserId = GetCurrentUserId();
            model.IsSuperAdmin = User.IsInRole("super_admin");
            var result = await Mediator.Send(model);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("ChangeStatus")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusDeviceCommand command)
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

            command.CreateUserId = GetCurrentUserId();
            command.IsSuperAdmin = User.IsInRole("super_admin");
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