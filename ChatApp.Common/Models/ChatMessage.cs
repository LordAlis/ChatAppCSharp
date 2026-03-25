using System.Text.Json.Serialization;

namespace ChatApp.Common.Models
{
    public class ChatMessage
    {
        public string Sender { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MessageType Type { get; set; } = MessageType.Text;

        [JsonConstructor]
        public ChatMessage() { }

        public ChatMessage(string sender, string content, MessageType type = MessageType.Text)
        {
            Sender = sender;
            Content = content;
            Type = type;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm}] {Sender}: {Content}";
        }
    }
}
