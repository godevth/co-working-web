using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Commands;
using CoreCMS.Application.TermAndCondition.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
    public class TermAndConditionController : BaseController
    {
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody]GetPlaceConsentByUserIdQuery model)
        {
            model.UserId = GetCurrentUserId();
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        public async Task<IActionResult> Submit(int id)
        {
            CommandResult<bool> result = null;
            SubmitPlaceConsentCommand submit = new SubmitPlaceConsentCommand()
            {
                TermId  = id,
                CurrentUserId = GetCurrentUserId(),
                CurrentEmail = GetCurrentEmail()
            };
            result = await Mediator.Send(submit);

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