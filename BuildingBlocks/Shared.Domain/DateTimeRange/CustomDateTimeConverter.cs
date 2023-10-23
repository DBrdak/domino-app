using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.DateTimeRange
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public CustomDateTimeConverter()
        {
            
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string dateString = reader.GetString();
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    return date;
                }
            }

            throw new SerializationException($"Cannot serialize value of DateTime type: {reader.GetString()}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            string formattedDateTimeRange = value.ToString("yyyy-MM-ddTHH:mm:ss");
            writer.WriteStringValue(formattedDateTimeRange);
        }
    }

}
