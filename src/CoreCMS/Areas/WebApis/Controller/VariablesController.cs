using System.Threading.Tasks;
using CoreCMS.Application.Shared.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class VariablesController : BaseController
    {
        [HttpPost("Options")]
        public async Task<IActionResult> GetOptions([FromBody]GetSystemVariableOptionQuery model)
        {
            return Ok(await Mediator.Send(model));
        }
    }
}