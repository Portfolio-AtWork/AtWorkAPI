using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimeZoneConverter;

namespace AtWorkAPI.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly TimeZoneInfo SaoPauloTimeZone = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var raw = reader.GetString();
            if (string.IsNullOrWhiteSpace(raw))
                throw new JsonException("Data inválida.");

            var parsed = DateTime.SpecifyKind(
                DateTime.Parse(raw, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None),
                DateTimeKind.Unspecified
            );

            return TimeZoneInfo.ConvertTimeToUtc(parsed, SaoPauloTimeZone);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            DateTime utcValue;

            if (value.Kind == DateTimeKind.Unspecified)
            {
                // Assume que value está no fuso São Paulo e cria DateTimeOffset para converter para UTC
                var offset = SaoPauloTimeZone.GetUtcOffset(value);
                var dto = new DateTimeOffset(value, offset);
                utcValue = dto.UtcDateTime;
            }
            else if (value.Kind == DateTimeKind.Local)
            {
                // Converte local para UTC
                utcValue = value.ToUniversalTime();
            }
            else
            {
                // Já é UTC
                utcValue = value;
            }

            // Converte de UTC para São Paulo para exibição
            var saoPauloTime = TimeZoneInfo.ConvertTimeFromUtc(utcValue, SaoPauloTimeZone);

            writer.WriteStringValue(saoPauloTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.GetCultureInfo("pt-BR")));
        }
    }
}
