using ChatApp.Client.Services;
using ChatApp.Common.Models;

namespace ChatApp.Client
{
    public partial class ClientForm : Form
    {
        private ChatClient? _client;

        public ClientForm()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_client == null || !_client.IsRunning)
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Kullanıcı adı girin.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtPort.Text, out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show("Geçerli bir port numarası girin.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    _client = new ChatClient(txtIP.Text.Trim(), port, txtUsername.Text.Trim());
                    _client.MessageReceived += OnMessageReceived;
                    _client.LogMessage += OnLogMessage;
                    _client.ConnectionLost += OnConnectionLost;

                    SetConnectedUI(true);
                    await _client.StartAsync();
                }
                catch (Exception ex)
                {
                    AppendMessage($"Bağlantı hatası: {ex.Message}", Color.Red);
                    _client?.Dispose();
                    _client = null;
                    SetConnectedUI(false);
                }
            }
            else
            {
                Disconnect();
            }
        }

        private void Disconnect()
        {
            _client?.Stop();
            _client?.Dispose();
            _client = null;
            SetConnectedUI(false);
        }

        private void SetConnectedUI(bool connected)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => SetConnectedUI(connected));
                return;
            }

            txtIP.Enabled = !connected;
            txtPort.Enabled = !connected;
            txtUsername.Enabled = !connected;
            btnConnect.Text = connected ? "Bağlantıyı Kes" : "Bağlan";
            txtMessage.Enabled = connected;
            btnSend.Enabled = connected;

            if (connected) txtMessage.Focus();
        }

        private void OnMessageReceived(ChatMessage message)
        {
            if (rtbMessages.InvokeRequired)
            {
                rtbMessages.BeginInvoke(() => OnMessageReceived(message));
                return;
            }

            Color color = message.Type switch
            {
                MessageType.ServerMessage => Color.Gray,
                MessageType.Join => Color.Green,
                MessageType.Leave => Color.OrangeRed,
                _ => Color.Black
            };

            string text = message.Type == MessageType.Text
                ? $"[{message.Timestamp:HH:mm}] {message.Sender}: {message.Content}"
                : $"[{message.Timestamp:HH:mm}] {message.Content}";

            AppendMessage(text, color);
        }

        private void AppendMessage(string text, Color color)
        {
            if (rtbMessages.InvokeRequired)
            {
                rtbMessages.BeginInvoke(() => AppendMessage(text, color));
                return;
            }

            rtbMessages.SelectionStart = rtbMessages.TextLength;
            rtbMessages.SelectionLength = 0;
            rtbMessages.SelectionColor = color;
            rtbMessages.AppendText(text + Environment.NewLine);
            rtbMessages.ScrollToCaret();
        }

        private void OnLogMessage(string message)
        {
            AppendMessage(message, Color.DarkBlue);
        }

        private void OnConnectionLost()
        {
            SetConnectedUI(false);
            AppendMessage("Sunucu ile bağlantı koptu.", Color.Red);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await SendMessage();
            }
        }

        private async Task SendMessage()
        {
            if (_client == null || !_client.IsRunning) return;

            string text = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            await _client.SendAsync(text);
            AppendMessage($"[{DateTime.Now:HH:mm}] Ben: {text}", Color.Blue);
            txtMessage.Clear();
            txtMessage.Focus();
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
