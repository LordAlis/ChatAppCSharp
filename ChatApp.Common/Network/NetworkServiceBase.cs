using ChatApp.Common.Helpers;
using ChatApp.Common.Models;

namespace ChatApp.Common.Network
{
    public abstract class NetworkServiceBase : IDisposable
    {
        private bool _isRunning;
        protected CancellationTokenSource? CancellationSource;

        public bool IsRunning
        {
            get => _isRunning;
            protected set => _isRunning = value;
        }

        public event Action<string>? LogMessage;

        protected void OnLog(string message)
        {
            LogMessage?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        protected async Task SendMessageAsync(StreamWriter writer, ChatMessage message)
        {
            string json = MessageSerializer.Serialize(message);
            await writer.WriteLineAsync(json);
            await writer.FlushAsync();
        }

        protected async Task<ChatMessage?> ReceiveMessageAsync(StreamReader reader)
        {
            string? line = await reader.ReadLineAsync();
            if (line == null) return null;
            return MessageSerializer.Deserialize(line);
        }

        public virtual void Dispose()
        {
            CancellationSource?.Cancel();
            CancellationSource?.Dispose();
            IsRunning = false;
        }
    }
}
