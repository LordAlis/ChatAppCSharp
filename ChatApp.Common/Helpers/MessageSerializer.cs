using System.Text.Json;
using ChatApp.Common.Models;

namespace ChatApp.Common.Helpers
{
    public static class MessageSerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string Serialize(ChatMessage message)
        {
            return JsonSerializer.Serialize(message, Options);
        }

        public static ChatMessage? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<ChatMessage>(json, Options);
        }
    }
}
