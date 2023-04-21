using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    public class DownloadFileController : BaseController
    {
        [HttpGet("Base64")]
        public async Task<IActionResult> Base64([FromQuery]string fileInfoId)
        {
            CommandResult<string> result;
            try
            {
                DownloadFileCommand downloadFile = new DownloadFileCommand()
                {
                    FileInfoId = fileInfoId
                };

                var response = await Mediator.Send(downloadFile);

                if(response != null && response.DataFile != null && response.DataFile.Length > 0)
                {
                    var str64 = Convert.ToBase64String(response.DataFile, 0, response.DataFile.Length);
                    var fileImg = "data:" + response.ContentType + ";base64," + str64;
                    return Ok(fileImg);
                }
                else 
                    return NoContent();
            }
            catch(Exception e)
            {
                result = new CommandResult<string>() 
                { 
                    Errors = new Dictionary<string, string[]>
                    {
                        { "DOWNLOAD_FILE_RESULT", new string[1] { e.Message } }
                    }
                };
                return BadRequest(result);
            }
        }

        [HttpGet("Byte")]
        public async Task<IActionResult> Byte([FromQuery]string fileInfoId)
        {
            CommandResult<string> result;
            try
            {
                DownloadFileCommand downloadFile = new DownloadFileCommand()
                {
                    FileInfoId = fileInfoId
                };

                var response = await Mediator.Send(downloadFile);

                if(response != null && response.DataFile != null && response.DataFile.Length > 0)
                {
                    return File(response.DataFile, response.ContentType, fileInfoId);
                }
                else 
                    return NoContent();
            }
            catch(Exception e)
            {
                result = new CommandResult<string>() 
                { 
                    Errors = new Dictionary<string, string[]>
                    {
                        { "DOWNLOAD_FILE_RESULT", new string[1] { e.Message } }
                    }
                };
                return BadRequest(result);
            }
        }

        [HttpGet("ByteFile")]
        public async Task<IActionResult> ByteFile([FromQuery]string fileInfoId ,string filename)
        {
            CommandResult<string> result;
            try
            {
                DownloadFileCommand downloadFile = new DownloadFileCommand()
                {
                    FileInfoId = fileInfoId
                };

                var response = await Mediator.Send(downloadFile);

                if(response != null && response.DataFile != null && response.DataFile.Length > 0)
                {
                    return File(response.DataFile, response.ContentType, filename);
                }
                else 
                    return NoContent();
            }
            catch(Exception e)
            {
                result = new CommandResult<string>() 
                { 
                    Errors = new Dictionary<string, string[]>
                    {
                        { "DOWNLOAD_FILE_RESULT", new string[1] { e.Message } }
                    }
                };
                return BadRequest(result);
            }
        }
    }
}