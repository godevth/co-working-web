using System;
using System.Globalization;
using System.Linq;

namespace CoreCMS.Application.Shared.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime? ParseDateTime(
            string dateToParse,
            string[] formats = null,
            IFormatProvider provider = null,
            DateTimeStyles styles = DateTimeStyles.None)
        {
            var CUSTOM_DATE_FORMATS = new string[] {
            "dd/MM/yyyy",
            "M/d/yyyy HH:mm:ss tt",
            "yyyyMMddTHHmmssZ",
            "yyyyMMddTHHmmZ",
            "yyyyMMddTHHmmss",
            "yyyyMMddTHHmm",
            "yyyyMMddHHmmss",
            "yyyyMMddHHmm",
            "yyyyMMdd",
            "yyyy-MM-ddTHH-mm-ss",
            "yyyy-MM-dd-HH-mm-ss",
            "yyyy-MM-dd-HH-mm",
            "yyyy-MM-dd"
            };

            if (formats == null || !formats.Any())
            {
                formats = CUSTOM_DATE_FORMATS;
            }

            if (provider == null)
            {
                provider = new CultureInfo("en-US");
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                            provider,
                            DateTimeStyles.AssumeUniversal,
                            out validDate))
                    {
                        return validDate;
                    }
                }

                if (DateTime.TryParseExact(dateToParse, format,
                        provider, styles, out validDate))
                {
                    return validDate;
                }
            }

            return null;
        }

        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}