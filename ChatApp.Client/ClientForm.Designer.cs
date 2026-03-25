namespace ChatApp.Client
{
    partial class ClientForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new Panel();
            this.lblIP = new Label();
            this.txtIP = new TextBox();
            this.lblPort = new Label();
            this.txtPort = new TextBox();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.btnConnect = new Button();
            this.rtbMessages = new RichTextBox();
            this.pnlBottom = new Panel();
            this.txtMessage = new TextBox();
            this.btnSend = new Button();

            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Controls.Add(this.btnConnect);
            this.pnlTop.Controls.Add(this.txtUsername);
            this.pnlTop.Controls.Add(this.lblUsername);
            this.pnlTop.Controls.Add(this.txtPort);
            this.pnlTop.Controls.Add(this.lblPort);
            this.pnlTop.Controls.Add(this.txtIP);
            this.pnlTop.Controls.Add(this.lblIP);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Size = new Size(600, 50);
            this.pnlTop.Padding = new Padding(10);

            // lblIP
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new Point(10, 17);
            this.lblIP.Text = "IP:";

            // txtIP
            this.txtIP.Location = new Point(30, 13);
            this.txtIP.Size = new Size(100, 23);
            this.txtIP.Text = "127.0.0.1";

            // lblPort
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new Point(140, 17);
            this.lblPort.Text = "Port:";

            // txtPort
            this.txtPort.Location = new Point(175, 13);
            this.txtPort.Size = new Size(55, 23);
            this.txtPort.Text = "5000";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(245, 17);
            this.lblUsername.Text = "Kullanıcı Adı:";

            // txtUsername
            this.txtUsername.Location = new Point(340, 13);
            this.txtUsername.Size = new Size(100, 23);

            // btnConnect
            this.btnConnect.Location = new Point(455, 12);
            this.btnConnect.Size = new Size(120, 28);
            this.btnConnect.Text = "Bağlan";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new EventHandler(this.btnConnect_Click);

            // pnlBottom
            this.pnlBottom.Controls.Add(this.txtMessage);
            this.pnlBottom.Controls.Add(this.btnSend);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Location = new Point(0, 425);
            this.pnlBottom.Size = new Size(600, 45);
            this.pnlBottom.Padding = new Padding(10, 8, 10, 8);

            // txtMessage
            this.txtMessage.Dock = DockStyle.Fill;
            this.txtMessage.Enabled = false;
            this.txtMessage.Font = new Font("Segoe UI", 10F);
            this.txtMessage.KeyDown += new KeyEventHandler(this.txtMessage_KeyDown);

            // btnSend
            this.btnSend.Dock = DockStyle.Right;
            this.btnSend.Enabled = false;
            this.btnSend.Size = new Size(80, 29);
            this.btnSend.Text = "Gönder";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);

            // rtbMessages
            this.rtbMessages.Dock = DockStyle.Fill;
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.BackColor = System.Drawing.Color.White;
            this.rtbMessages.BorderStyle = BorderStyle.FixedSingle;
            this.rtbMessages.Font = new Font("Segoe UI", 10F);

            // ClientForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 470);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Chat İstemcisi";
            this.FormClosing += new FormClosingEventHandler(this.ClientForm_FormClosing);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel pnlTop;
        private Label lblIP;
        private TextBox txtIP;
        private Label lblPort;
        private TextBox txtPort;
        private Label lblUsername;
        private TextBox txtUsername;
        private Button btnConnect;
        private RichTextBox rtbMessages;
        private Panel pnlBottom;
        private TextBox txtMessage;
        private Button btnSend;
    }
}
