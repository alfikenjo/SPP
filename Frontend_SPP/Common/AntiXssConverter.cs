using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ganss.XSS;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Frontend_SPP.Common
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
