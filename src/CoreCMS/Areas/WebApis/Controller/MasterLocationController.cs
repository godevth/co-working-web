using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterData.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBP.Application.ApiMasterData.Queries;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class MasterLocationController : BaseController
    {
        [HttpPost("Select2Province")]
        public async Task<IActionResult> Province(Select2MasterProvinceQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
        [HttpPost("Select2Ampher")]
        public async Task<IActionResult> Ampher([FromBody]Select2MasterAmpherQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
        [HttpPost("Select2Tumbol")]
        public async Task<IActionResult> Tumbol([FromBody]Select2MasterTumbolQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
