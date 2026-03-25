using System.Net.Sockets;
using ChatApp.Common.Helpers;
using ChatApp.Common.Models;

namespace ChatApp.Server.Services
{
    public class ClientHandler : IDisposable
    {
        private readonly TcpClient _tcpClient;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;
        private readonly CancellationToken _cancellationToken;

        public string Username { get; private set; } = "Bilinmeyen";
        public string ClientId { get; }

        public event Action<ClientHandler, ChatMessage>? MessageReceived;
        public event Action<ClientHandler>? Disconnected;

        public ClientHandler(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            _tcpClient = tcpClient;
            ClientId = Guid.NewGuid().ToString("N")[..8];
            _cancellationToken = cancellationToken;

            var stream = _tcpClient.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = true };
        }

        public async Task StartAsync()
        {
            try
            {
                while (!_cancellationToken.IsCancellationRequested && _tcpClient.Connected)
                {
                    string? line = await _reader.ReadLineAsync(_cancellationToken);
                    if (line == null) break;

                    var message = MessageSerializer.Deserialize(line);
                    if (message == null) continue;

                    if (message.Type == MessageType.Join)
                    {
                        Username = message.Sender;
                    }

                    MessageReceived?.Invoke(this, message);
                }
            }
            catch (OperationCanceledException) { }
            catch (IOException) { }
            finally
            {
                Disconnected?.Invoke(this);
            }
        }

        public async Task SendAsync(ChatMessage message)
        {
            try
            {
                string json = MessageSerializer.Serialize(message);
                await _writer.WriteLineAsync(json);
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
        }

        public void Dispose()
        {
            _reader.Dispose();
            _writer.Dispose();
            _tcpClient.Close();
            _tcpClient.Dispose();
        }
    }
}
