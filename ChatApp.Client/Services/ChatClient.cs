using System.Net.Sockets;
using ChatApp.Common.Interfaces;
using ChatApp.Common.Models;
using ChatApp.Common.Network;

namespace ChatApp.Client.Services
{
    public class ChatClient : NetworkServiceBase, INetworkService
    {
        private TcpClient? _tcpClient;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        private readonly string _serverAddress;
        private readonly int _serverPort;

        public string Username { get; }

        public event Action<ChatMessage>? MessageReceived;
        public event Action? ConnectionLost;

        public ChatClient(string serverAddress, int serverPort, string username)
        {
            _serverAddress = serverAddress;
            _serverPort = serverPort;
            Username = username;
        }

        public async Task StartAsync()
        {
            if (IsRunning) return;

            CancellationSource = new CancellationTokenSource();
            _tcpClient = new TcpClient();

            await _tcpClient.ConnectAsync(_serverAddress, _serverPort, CancellationSource.Token);

            var stream = _tcpClient.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = true };

            IsRunning = true;
            OnLog("Sunucuya bağlanıldı.");

            var joinMessage = new ChatMessage(Username, $"{Username} sohbete katıldı.", MessageType.Join);
            await SendMessageAsync(_writer, joinMessage);

            try
            {
                while (!CancellationSource.Token.IsCancellationRequested)
                {
                    var message = await ReceiveMessageAsync(_reader);
                    if (message == null) break;
                    MessageReceived?.Invoke(message);
                }
            }
            catch (OperationCanceledException) { }
            catch (IOException) { }
            finally
            {
                bool wasRunning = IsRunning;
                IsRunning = false;
                OnLog("Bağlantı kesildi.");
                if (wasRunning) ConnectionLost?.Invoke();
            }
        }

        public async Task SendAsync(string content)
        {
            if (!IsRunning || _writer == null) return;

            var message = new ChatMessage(Username, content, MessageType.Text);
            await SendMessageAsync(_writer, message);
        }

        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            CancellationSource?.Cancel();
            _reader?.Dispose();
            _writer?.Dispose();
            _tcpClient?.Close();
            _tcpClient?.Dispose();
        }

        public override void Dispose()
        {
            Stop();
            base.Dispose();
        }
    }
}
