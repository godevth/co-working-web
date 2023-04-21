using System.Threading.Tasks;
using CoreCMS.Application.PlaceTheme.Commands;
using CoreCMS.Application.PlaceTheme.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class PlaceThemeController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] SearchThemeQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> PostAdd([FromBody] AddPlaceThemeCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            model.CreateUserId = GetCurrentUserId();
            result = await Mediator.Send(model);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Toggle(int id)
        {
            CommandResult<bool> result = null;
            ToggleActiveStatusPlaceThemeCommand toggleCmd = new ToggleActiveStatusPlaceThemeCommand()
            {
                ThemeId = id,
                UpdatedUserId = GetCurrentUserId()
            };
            result = await Mediator.Send(toggleCmd);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Delete(int id)
        {
            CommandResult<bool> result = null;
            SoftDeletePlaceThemeCommand deleteCmd = new SoftDeletePlaceThemeCommand()
            {
                ThemeId = id,
                DeleteUserId = GetCurrentUserId()
            };
            result = await Mediator.Send(deleteCmd);

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