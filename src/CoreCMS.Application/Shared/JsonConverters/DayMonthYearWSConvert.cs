using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Web;
using Newtonsoft.Json;

namespace CoreCMS.Application.Shared.JsonConverters
{
    public class DayMonthYearWSConverter : IsoDateTimeConverter 
    {
        public DayMonthYearWSConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            UpdateCurrentCulture();
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value !=null)
            {
                UpdateCurrentCulture();
                base.WriteJson(writer, value, serializer);
            }
        }

        private void UpdateCurrentCulture()
        {
            Culture = CultureInfo.CreateSpecificCulture("en");
        }
    }
}