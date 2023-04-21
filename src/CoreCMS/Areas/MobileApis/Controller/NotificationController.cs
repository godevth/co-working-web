using System.Threading.Tasks;
using CoreCMS.Application.Device.Commands;
using CoreCMS.Application.Notification.Commands;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
    public class NotificationController : BaseController
    {
        //[Route]
        [HttpPost("RegisterDevice")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> RegisterDevice([FromBody] AddDeviceCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("UpdateDevice")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> UpdateDevice([FromBody] EditDeviceByIdCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("UnRegisterDevice")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> UnRegisterDevice([FromBody] DeleteDeviceCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("ResetBadge")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> ResetBadge()
        {
            ResetBadgeCommand model = new ResetBadgeCommand(){
                UserId = GetCurrentUserId()
            };
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("CancelNotification")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> CancelNotification([FromBody] MessageCanceledCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
