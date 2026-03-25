using System.Net;
using System.Net.Sockets;
using ChatApp.Common.Interfaces;
using ChatApp.Common.Models;
using ChatApp.Common.Network;

namespace ChatApp.Server.Services
{
    public class ChatServer : NetworkServiceBase, INetworkService
    {
        private TcpListener? _listener;
        private readonly List<ClientHandler> _clients = new();
        private readonly object _clientsLock = new();
        private readonly int _port;

        public event Action<string>? ClientConnected;
        public event Action<string>? ClientDisconnected;
        public event Action<ChatMessage>? MessageBroadcasted;

        public IReadOnlyList<string> ConnectedUsers
        {
            get
            {
                lock (_clientsLock)
                {
                    return _clients.Select(c => c.Username).ToList();
                }
            }
        }

        public ChatServer(int port)
        {
            _port = port;
        }

        public async Task StartAsync()
        {
            if (IsRunning) return;

            CancellationSource = new CancellationTokenSource();
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            IsRunning = true;

            OnLog($"Sunucu {_port} portunda başlatıldı.");

            try
            {
                while (!CancellationSource.Token.IsCancellationRequested)
                {
                    var tcpClient = await _listener.AcceptTcpClientAsync(CancellationSource.Token);
                    var handler = new ClientHandler(tcpClient, CancellationSource.Token);
                    handler.MessageReceived += OnClientMessageReceived;
                    handler.Disconnected += OnClientDisconnected;

                    lock (_clientsLock)
                    {
                        _clients.Add(handler);
                    }

                    _ = Task.Run(() => handler.StartAsync(), CancellationSource.Token);
                }
            }
            catch (OperationCanceledException) { }
            catch (SocketException) when (!IsRunning) { }
            finally
            {
                OnLog("Sunucu durduruldu.");
            }
        }

        private async void OnClientMessageReceived(ClientHandler sender, ChatMessage message)
        {
            if (message.Type == MessageType.Join)
            {
                OnLog($"{sender.Username} bağlandı.");
                ClientConnected?.Invoke(sender.Username);

                var welcome = new ChatMessage("Sunucu", $"Hoş geldiniz, {sender.Username}!", MessageType.ServerMessage);
                await sender.SendAsync(welcome);

                var joinNotification = new ChatMessage("Sunucu", $"{sender.Username} sohbete katıldı.", MessageType.ServerMessage);
                await BroadcastAsync(joinNotification, sender);
            }
            else if (message.Type == MessageType.Text)
            {
                OnLog($"{sender.Username}: {message.Content}");
                MessageBroadcasted?.Invoke(message);
                await BroadcastAsync(message, sender);
            }
        }

        private async void OnClientDisconnected(ClientHandler handler)
        {
            lock (_clientsLock)
            {
                _clients.Remove(handler);
            }

            OnLog($"{handler.Username} ayrıldı.");
            ClientDisconnected?.Invoke(handler.Username);

            var leaveMessage = new ChatMessage("Sunucu", $"{handler.Username} sohbetten ayrıldı.", MessageType.ServerMessage);
            await BroadcastAsync(leaveMessage);

            handler.Dispose();
        }

        private async Task BroadcastAsync(ChatMessage message, ClientHandler? exclude = null)
        {
            List<ClientHandler> clients;
            lock (_clientsLock)
            {
                clients = _clients.ToList();
            }

            foreach (var client in clients)
            {
                if (client != exclude)
                {
                    await client.SendAsync(message);
                }
            }
        }

        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            CancellationSource?.Cancel();

            lock (_clientsLock)
            {
                foreach (var client in _clients)
                {
                    client.Dispose();
                }
                _clients.Clear();
            }

            _listener?.Stop();
            OnLog("Sunucu kapatıldı.");
        }

        public override void Dispose()
        {
            Stop();
            base.Dispose();
        }
    }
}
