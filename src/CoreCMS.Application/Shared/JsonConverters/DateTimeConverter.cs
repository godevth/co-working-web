using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CoreCMS.Application.Shared.Helpers;

namespace CoreCMS.Application.Shared.JsonConverters
{
    // http://www.vickram.me/custom-datetime-model-binding-in-asp-net-core-web-api
    public class DateTimeConverter : DateTimeConverterBase
    {
        private string dateFormat = null;
        private readonly DateTimeConverterBase innerConverter = null;

        public DateTimeConverter() : this(dateFormat: null) { }

        public DateTimeConverter(string dateFormat = null) : this(dateFormat, innerConverter: new IsoDateTimeConverter()) { }

        public DateTimeConverter(string dateFormat = null, DateTimeConverterBase innerConverter = null)
        {
            this.dateFormat = dateFormat;
            this.innerConverter = innerConverter ?? new IsoDateTimeConverter();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullableType = DateTimeHelper.IsNullableType(objectType);

            if (reader.TokenType == JsonToken.Null)
            {
                if (isNullableType)
                {
                    return null;
                }

                throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException($"Unexpected token parsing date. Expected {nameof(String)}, got {reader.TokenType}.");
            }

            var dateToParse = reader.Value.ToString();

            if (isNullableType && string.IsNullOrWhiteSpace(dateToParse))
            {
                return null;
            }

            if (string.IsNullOrEmpty(this.dateFormat))
            {
                return DateTimeHelper.ParseDateTime(dateToParse);
            }

            return DateTimeHelper.ParseDateTime(dateToParse, new string[] { this.dateFormat });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (dateFormat == null)
                dateFormat = "dd/MM/yyyy";

            if (value != null)
                writer.WriteValue(((DateTime)value).ToString(dateFormat));
        }
    }
}