using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimeZoneConverter;

namespace AtWorkAPI.Converters
{
    public class NullableDateTimeConverter : JsonConverter<DateTime?>
    {
        private static readonly TimeZoneInfo SaoPauloTimeZone = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var raw = reader.GetString();
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            var parsed = DateTime.SpecifyKind(
                DateTime.Parse(raw, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None),
                DateTimeKind.Unspecified
            );

            return TimeZoneInfo.ConvertTimeToUtc(parsed, SaoPauloTimeZone);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            DateTime utcValue;

            if (value.Value.Kind == DateTimeKind.Unspecified)
            {
                var offset = SaoPauloTimeZone.GetUtcOffset(value.Value);
                var dto = new DateTimeOffset(value.Value, offset);
                utcValue = dto.UtcDateTime;
            }
            else if (value.Value.Kind == DateTimeKind.Local)
            {
                utcValue = value.Value.ToUniversalTime();
            }
            else
            {
                utcValue = value.Value;
            }

            var saoPauloTime = TimeZoneInfo.ConvertTimeFromUtc(utcValue, SaoPauloTimeZone);

            writer.WriteStringValue(saoPauloTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.GetCultureInfo("pt-BR")));
        }
    }
}
