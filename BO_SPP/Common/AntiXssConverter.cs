using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BO_SPP.Common
{
    public class AntiXssConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader,
                                Type typeToConvert, JsonSerializerOptions options)
        {
            var sanitiser = new HtmlSanitizer();
            var raw = reader.GetString();
            var sanitised = sanitiser.Sanitize(raw);
            raw = sanitised;

            if (raw == sanitised)
                return sanitised;

            throw new BadHttpRequestException("XSS injection detected.");
        }

        public override void Write(Utf8JsonWriter writer, string value,
                                   JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
