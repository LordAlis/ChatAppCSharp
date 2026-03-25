using ChatApp.Server.Services;

namespace ChatApp.Server
{
    public partial class ServerForm : Form
    {
        private ChatServer? _server;

        public ServerForm()
        {
            InitializeComponent();
        }

        private async void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_server == null || !_server.IsRunning)
            {
                if (!int.TryParse(txtPort.Text, out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show("Geçerli bir port numarası girin (1-65535).",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _server = new ChatServer(port);
                _server.LogMessage += OnLogMessage;
                _server.ClientConnected += OnClientConnected;
                _server.ClientDisconnected += OnClientDisconnected;

                btnStartStop.Text = "Durdur";
                txtPort.Enabled = false;

                await _server.StartAsync();
            }
            else
            {
                StopServer();
            }
        }

        private void StopServer()
        {
            _server?.Stop();
            _server?.Dispose();
            _server = null;

            btnStartStop.Text = "Başlat";
            txtPort.Enabled = true;
            lstUsers.Items.Clear();
        }

        private void OnLogMessage(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.BeginInvoke(() => OnLogMessage(message));
                return;
            }
            rtbLog.AppendText(message + Environment.NewLine);
            rtbLog.ScrollToCaret();
        }

        private void OnClientConnected(string username)
        {
            if (lstUsers.InvokeRequired)
            {
                lstUsers.BeginInvoke(() => OnClientConnected(username));
                return;
            }
            lstUsers.Items.Add(username);
        }

        private void OnClientDisconnected(string username)
        {
            if (lstUsers.InvokeRequired)
            {
                lstUsers.BeginInvoke(() => OnClientDisconnected(username));
                return;
            }
            lstUsers.Items.Remove(username);
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }
    }
}
