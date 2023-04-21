using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Commands;
using CoreCMS.Application.TermAndCondition.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class TermAndConditionController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] SearchTermAndConditionQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(int id)
        {
            GetTermAndConditionQuery query = new GetTermAndConditionQuery() { TermId = id};
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> PostAdd([FromBody] AddTermAndConditionCommand model)
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
            ToggleActiveStatusTermAndConditionCommand toggleCmd = new ToggleActiveStatusTermAndConditionCommand()
            {
                TermId = id,
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
            SoftDeleteTermAndConditionCommand deleteCmd = new SoftDeleteTermAndConditionCommand()
            {
                TermId = id,
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

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Edit([FromBody] EditTermAndConditionCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            model.CurrentUserId = GetCurrentUserId();
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
    }
}