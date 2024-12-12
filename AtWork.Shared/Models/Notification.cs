using AtWork.Shared.Enums.Models;
using System.Text.Json.Serialization;

namespace AtWork.Shared.Models
{
    public class Notification
    {
        [JsonPropertyName("id")]
        public Guid Id { get; private set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("kind")]
        public NotificationKind Kind { get; set; }

        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid();
            Message = string.Empty;
            Kind = NotificationKind.Error;
            Parameters = new object();
        }

        public Notification(string message, NotificationKind kind)
        {
            Id = Guid.NewGuid();
            Message = message;
            Kind = kind;
            Parameters = new object();
        }

        public Notification(string message, NotificationKind kind, object parameters)
        {
            Id = Guid.NewGuid();
            Message = message;
            Kind = kind;
            Parameters = parameters;
        }
    }
}
