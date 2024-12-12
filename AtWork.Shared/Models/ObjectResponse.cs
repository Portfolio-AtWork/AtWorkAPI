using AtWork.Shared.Enums.Models;
using System.Text.Json.Serialization;

namespace AtWork.Shared.Models
{
    public class ObjectResponse<T>
    {
        [JsonPropertyName("value")]
        public T Value { get; set; } = default!;

        [JsonPropertyName("notifications")]
        public List<Notification> Notifications { get; private set; } = [];

        [JsonPropertyName("ok")]
        public bool Ok => !Notifications.Any(n => n.Kind == NotificationKind.Warning || n.Kind == NotificationKind.Error);

        public ObjectResponse() { }

        public ObjectResponse(T value)
        {
            Value = value;
            Notifications = [];
        }

        public ObjectResponse(T value, List<Notification> notifications)
        {
            Value = value;
            Notifications = notifications;
        }

        public void AddNotification(Notification notification)
        {
            Notifications.Add(notification);
        }

        public void AddNotification(string message, NotificationKind kind)
        {
            Notifications.Add(new(message, kind));
        }

        public void AddNotification(string message, NotificationKind kind, object parameters)
        {
            Notifications.Add(new(message, kind, parameters));
        }
    }
}
