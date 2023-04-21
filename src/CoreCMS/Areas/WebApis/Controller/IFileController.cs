using System;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.ApiMasterFile.Models;
using CoreCMS.Controllers;
using CoreCMS.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    public class IFileController : BaseController
    {
         private readonly IMediator _mediator;
         private readonly ApiConfig _config;     

        public IFileController(IMediator mediator, IOptions<ApiConfig> config) : base()
        {
            _config = config.Value;
            _mediator = mediator;
        }

        
        [HttpPost("UploadFile")]
        public async Task<IActionResult> Add([FromBody] UploadFile model)
        {
             byte[] dataPic = Convert.FromBase64String(model.Base64);
                    UploadFileCommand uploadFileCommand = new UploadFileCommand()
                    {
                        Data = dataPic,
                        FileName = Guid.NewGuid().ToString(),
                        ContentType = model.ContentType
                    };
            var result = await _mediator.Send(uploadFileCommand);
            return Ok(result);
        }
        
    }
}