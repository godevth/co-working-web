using System.Threading.Tasks;
using CoreCMS.Application.PlaceTheme.Commands;
using CoreCMS.Application.PlaceTheme.Queries;
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
    public class PlaceThemeController : BaseController
    {
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody]GetPlaceThemeByUserIdQuery model)
        {
            model.UserId = GetCurrentUserId();
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        public async Task<IActionResult> Change(int id)
        {
            CommandResult<bool> result = null;
            ChangeThemeCommand changeCmd = new ChangeThemeCommand()
            {
                ThemeId  = id,
                CurrentUserId = GetCurrentUserId()
            };
            result = await Mediator.Send(changeCmd);

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