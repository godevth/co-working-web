
using System.Threading.Tasks;
using CoreCMS.Application.Room.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    public class RoomController : BaseController
    {
       [HttpPost("GetTimeHourly")]
       [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> GetTimeHourly([FromBody]GetRangeTimeHourlyQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("GetTimeDaily")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> GetTimeDaily([FromBody]GetRangeTimeDailyQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("GetTimeMonthly")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> GetTimeMonthly([FromBody]GetRangeTimeMonthlyQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
