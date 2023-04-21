using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using CoreCMS.Models.Extensions;
using OfficeOpenXml.Style;
using IdentityServer4.Validation;
using System.Drawing;

namespace CoreCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private ITokenValidator _tokenValidator;

        public FilesController(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        [HttpGet("CustomerForm")]
        public IActionResult CustomerForm()
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Custumer");

                // Set Column Width
                workSheet.Column(1).Width = 30;
                workSheet.Column(2).Width = 30;
                workSheet.Column(3).Width = 30;
                workSheet.Column(4).Width = 30;
                workSheet.Column(5).Width = 30;
                workSheet.Column(6).Width = 30;
                workSheet.Column(7).Width = 30;

                // Set Headers
                workSheet.Cells[1, 1].Value = "Email";
                workSheet.Cells[1, 2].Value = "Password";
                workSheet.Cells[1, 3].Value = "Firstname";
                workSheet.Cells[1, 4].Value = "Lastname";
                workSheet.Cells[1, 5].Value = "PhoneNo";
                workSheet.Cells[1, 6].Value = "Gender";

                workSheet.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                
                workSheet.Column(5).Style.Numberformat.Format ="@";
                int row = 2;
                workSheet.Cells[row, 1].Value = "example@mail.com";
                workSheet.Cells[row, 2].Value = "e1234567 *";
                workSheet.Cells[row, 3].Value = "Example";
                workSheet.Cells[row, 4].Value = "Example";
                workSheet.Cells[row, 5].Value = "0611111111";
                workSheet.Cells[row, 6].Value = "M  * M = Male, F = Female";
                workSheet.Cells[row, 7].Value = "*** กรุณาลบข้อมูลตัวอย่าง";

                workSheet.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"Template Import Custumer.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}