using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CoreCMS.Models.Extensions
{
    public static class ExcelRangeExtensions
    {
        private static Color _lightGray = System.Drawing.ColorTranslator.FromHtml("#FAFAFA");

        public static void SetHeader<T>(this ExcelRange cell, T value, 
            ExcelHorizontalAlignment horizontalAlignment = ExcelHorizontalAlignment.Center,
            bool FontBold = true,
            ExcelFillStyle fillStyle = ExcelFillStyle.Solid)
        {
            cell.Value = value;
            cell.Style.HorizontalAlignment = horizontalAlignment;
            cell.Style.Font.Bold = FontBold;
            cell.Style.Fill.PatternType = fillStyle;
            cell.Style.Fill.BackgroundColor.SetColor(_lightGray);
        }

        public static void SetValue<T> (this ExcelRange cell, T value, 
            ExcelHorizontalAlignment horizontalAlignment = ExcelHorizontalAlignment.Left,
            bool FontBold = false,
            string format = null)
        {
            cell.Value = value;
            cell.Style.HorizontalAlignment = horizontalAlignment;
            cell.Style.Font.Bold = FontBold;
            if (!string.IsNullOrEmpty(format)) {
                cell.Style.Numberformat.Format = format;
            }
        }
    }
}