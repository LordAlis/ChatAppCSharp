namespace ChatApp.Server
{
    partial class ServerForm
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
            this.lblPort = new Label();
            this.txtPort = new TextBox();
            this.btnStartStop = new Button();
            this.splitContainer = new SplitContainer();
            this.lstUsers = new ListBox();
            this.lblUsers = new Label();
            this.rtbLog = new RichTextBox();
            this.lblLog = new Label();

            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Controls.Add(this.btnStartStop);
            this.pnlTop.Controls.Add(this.txtPort);
            this.pnlTop.Controls.Add(this.lblPort);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Size = new Size(650, 50);
            this.pnlTop.Padding = new Padding(10);

            // lblPort
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new Point(15, 17);
            this.lblPort.Text = "Port:";

            // txtPort
            this.txtPort.Location = new Point(55, 13);
            this.txtPort.Size = new Size(80, 23);
            this.txtPort.Text = "5000";

            // btnStartStop
            this.btnStartStop.Location = new Point(150, 12);
            this.btnStartStop.Size = new Size(100, 28);
            this.btnStartStop.Text = "Başlat";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new EventHandler(this.btnStartStop_Click);

            // splitContainer
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Location = new Point(0, 50);
            this.splitContainer.SplitterDistance = 180;

            // lblUsers
            this.lblUsers.Dock = DockStyle.Top;
            this.lblUsers.Height = 25;
            this.lblUsers.Text = "  Bağlı Kullanıcılar";
            this.lblUsers.TextAlign = ContentAlignment.MiddleLeft;
            this.lblUsers.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);

            // lstUsers
            this.lstUsers.Dock = DockStyle.Fill;
            this.lstUsers.BorderStyle = BorderStyle.FixedSingle;
            this.lstUsers.Font = new Font("Segoe UI", 9.5F);

            this.splitContainer.Panel1.Controls.Add(this.lstUsers);
            this.splitContainer.Panel1.Controls.Add(this.lblUsers);

            // lblLog
            this.lblLog.Dock = DockStyle.Top;
            this.lblLog.Height = 25;
            this.lblLog.Text = "  Sunucu Günlüğü";
            this.lblLog.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLog.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);

            // rtbLog
            this.rtbLog.Dock = DockStyle.Fill;
            this.rtbLog.ReadOnly = true;
            this.rtbLog.BackColor = System.Drawing.Color.White;
            this.rtbLog.BorderStyle = BorderStyle.FixedSingle;
            this.rtbLog.Font = new Font("Consolas", 9F);

            this.splitContainer.Panel2.Controls.Add(this.rtbLog);
            this.splitContainer.Panel2.Controls.Add(this.lblLog);

            // ServerForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(650, 420);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlTop);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Chat Sunucusu";
            this.FormClosing += new FormClosingEventHandler(this.ServerForm_FormClosing);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Panel pnlTop;
        private Label lblPort;
        private TextBox txtPort;
        private Button btnStartStop;
        private SplitContainer splitContainer;
        private ListBox lstUsers;
        private Label lblUsers;
        private RichTextBox rtbLog;
        private Label lblLog;
    }
}
