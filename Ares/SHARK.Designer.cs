namespace Ares
{
    partial class SHARK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SHARK));
            txtDebug = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            timerMonitorProcess = new System.Windows.Forms.Timer(components);
            notifyIcon = new NotifyIcon(components);
            contextMenuTray = new ContextMenuStrip(components);
            toolStripMenuItem_AutoSwitch = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem_Exit = new ToolStripMenuItem();
            label1 = new Label();
            btRestart = new Button();
            numPort = new NumericUpDown();
            openFileDialog1 = new OpenFileDialog();
            btCast = new Button();
            timerCheckConnection = new System.Windows.Forms.Timer(components);
            lstCastDevices = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            pbBackground = new PictureBox();
            statusStrip1 = new StatusStrip();
            statusAuth = new ToolStripStatusLabel();
            statusData = new ToolStripStatusLabel();
            statusCast = new ToolStripStatusLabel();
            optionsMenu = new MenuStrip();
            deckEditorToolStripMenuItem = new ToolStripMenuItem();
            toolstripOpenDeckEditor = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            optAutoCast = new ToolStripMenuItem();
            optWindowsStart = new ToolStripMenuItem();
            optSystray = new ToolStripMenuItem();
            optAutoSwitchDeck = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            resetAllOptionsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            toolRefreshDevices = new ToolStripMenuItem();
            toolClearLog = new ToolStripMenuItem();
            pictureLogo = new PictureBox();
            contextMenuTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbBackground).BeginInit();
            statusStrip1.SuspendLayout();
            optionsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            SuspendLayout();
            // 
            // txtDebug
            // 
            txtDebug.BackColor = Color.Black;
            txtDebug.BorderStyle = BorderStyle.None;
            txtDebug.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            txtDebug.ForeColor = Color.LightGray;
            txtDebug.Location = new Point(4, 81);
            txtDebug.Margin = new Padding(3, 4, 3, 4);
            txtDebug.Multiline = true;
            txtDebug.Name = "txtDebug";
            txtDebug.ReadOnly = true;
            txtDebug.Size = new Size(516, 169);
            txtDebug.TabIndex = 1;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // timerMonitorProcess
            // 
            timerMonitorProcess.Enabled = true;
            timerMonitorProcess.Interval = 500;
            timerMonitorProcess.Tick += timer1_Tick;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuTray;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Nest Deck";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuTray
            // 
            contextMenuTray.ImageScalingSize = new Size(20, 20);
            contextMenuTray.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_AutoSwitch, toolStripSeparator1, toolStripMenuItem_Exit });
            contextMenuTray.Name = "contextMenuTray";
            contextMenuTray.Size = new Size(197, 58);
            contextMenuTray.Opening += contextMenuTray_Opening;
            // 
            // toolStripMenuItem_AutoSwitch
            // 
            toolStripMenuItem_AutoSwitch.CheckOnClick = true;
            toolStripMenuItem_AutoSwitch.Name = "toolStripMenuItem_AutoSwitch";
            toolStripMenuItem_AutoSwitch.Size = new Size(196, 24);
            toolStripMenuItem_AutoSwitch.Text = "Auto switch decks";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(193, 6);
            // 
            // toolStripMenuItem_Exit
            // 
            toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            toolStripMenuItem_Exit.Size = new Size(196, 24);
            toolStripMenuItem_Exit.Text = "Exit";
            toolStripMenuItem_Exit.Click += toolStripMenuItem_Exit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(9, 201);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 5;
            label1.Text = "Server Port";
            label1.Visible = false;
            // 
            // btRestart
            // 
            btRestart.ForeColor = SystemColors.ControlText;
            btRestart.Location = new Point(171, 196);
            btRestart.Margin = new Padding(3, 4, 3, 4);
            btRestart.Name = "btRestart";
            btRestart.Size = new Size(316, 31);
            btRestart.TabIndex = 7;
            btRestart.Text = "Restart Server";
            btRestart.UseVisualStyleBackColor = true;
            btRestart.Visible = false;
            btRestart.Click += btRestart_Click;
            // 
            // numPort
            // 
            numPort.BorderStyle = BorderStyle.FixedSingle;
            numPort.Location = new Point(95, 199);
            numPort.Margin = new Padding(3, 4, 3, 4);
            numPort.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(70, 27);
            numPort.TabIndex = 9;
            numPort.Value = new decimal(new int[] { 58417, 0, 0, 0 });
            numPort.Visible = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btCast
            // 
            btCast.Enabled = false;
            btCast.ForeColor = SystemColors.ControlText;
            btCast.Location = new Point(12, 398);
            btCast.Name = "btCast";
            btCast.Size = new Size(508, 35);
            btCast.TabIndex = 15;
            btCast.Text = "Cast";
            btCast.UseVisualStyleBackColor = true;
            btCast.Click += btCast_Click;
            // 
            // timerCheckConnection
            // 
            timerCheckConnection.Interval = 5000;
            // 
            // lstCastDevices
            // 
            lstCastDevices.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            lstCastDevices.ForeColor = SystemColors.ControlText;
            lstCastDevices.FullRowSelect = true;
            lstCastDevices.GridLines = true;
            lstCastDevices.Location = new Point(0, 257);
            lstCastDevices.MultiSelect = false;
            lstCastDevices.Name = "lstCastDevices";
            lstCastDevices.Size = new Size(531, 134);
            lstCastDevices.TabIndex = 14;
            lstCastDevices.UseCompatibleStateImageBehavior = false;
            lstCastDevices.View = View.Details;
            lstCastDevices.SelectedIndexChanged += lstCastDevices_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Device";
            columnHeader1.Width = 270;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Model";
            columnHeader2.Width = 250;
            // 
            // pbBackground
            // 
            pbBackground.BackColor = Color.Black;
            pbBackground.Location = new Point(-2, 29);
            pbBackground.Name = "pbBackground";
            pbBackground.Size = new Size(534, 226);
            pbBackground.TabIndex = 24;
            pbBackground.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusAuth, statusData, statusCast });
            statusStrip1.Location = new Point(0, 442);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(531, 30);
            statusStrip1.TabIndex = 25;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusAuth
            // 
            statusAuth.BorderStyle = Border3DStyle.Etched;
            statusAuth.Image = Nest_Deck.Properties.Resources.icons8_connection_status_on_16;
            statusAuth.Name = "statusAuth";
            statusAuth.Padding = new Padding(20, 0, 0, 0);
            statusAuth.Size = new Size(146, 24);
            statusAuth.Text = "Authentication";
            // 
            // statusData
            // 
            statusData.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusData.BorderStyle = Border3DStyle.Etched;
            statusData.Image = Nest_Deck.Properties.Resources.icons8_no_network_16;
            statusData.Name = "statusData";
            statusData.Padding = new Padding(20, 0, 0, 0);
            statusData.Size = new Size(162, 24);
            statusData.Text = "Data connection";
            // 
            // statusCast
            // 
            statusCast.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusCast.BorderStyle = Border3DStyle.Etched;
            statusCast.Image = Nest_Deck.Properties.Resources.icons8_no_network_16;
            statusCast.Name = "statusCast";
            statusCast.Padding = new Padding(20, 0, 0, 0);
            statusCast.Size = new Size(208, 24);
            statusCast.Text = "Chromecast connection";
            // 
            // optionsMenu
            // 
            optionsMenu.ImageScalingSize = new Size(20, 20);
            optionsMenu.Items.AddRange(new ToolStripItem[] { deckEditorToolStripMenuItem, optionsToolStripMenuItem, aboutToolStripMenuItem });
            optionsMenu.Location = new Point(0, 0);
            optionsMenu.Name = "optionsMenu";
            optionsMenu.Size = new Size(531, 28);
            optionsMenu.TabIndex = 27;
            optionsMenu.Text = "menuStrip1";
            // 
            // deckEditorToolStripMenuItem
            // 
            deckEditorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolstripOpenDeckEditor });
            deckEditorToolStripMenuItem.Name = "deckEditorToolStripMenuItem";
            deckEditorToolStripMenuItem.Size = new Size(100, 24);
            deckEditorToolStripMenuItem.Text = "Deck editor";
            // 
            // toolstripOpenDeckEditor
            // 
            toolstripOpenDeckEditor.Name = "toolstripOpenDeckEditor";
            toolstripOpenDeckEditor.Size = new Size(201, 26);
            toolstripOpenDeckEditor.Text = "Open in browser";
            toolstripOpenDeckEditor.Click += toolstripOpenDeckEditor_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { optAutoCast, optWindowsStart, optSystray, optAutoSwitchDeck, toolStripSeparator2, resetAllOptionsToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(75, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // optAutoCast
            // 
            optAutoCast.CheckOnClick = true;
            optAutoCast.Name = "optAutoCast";
            optAutoCast.Size = new Size(266, 26);
            optAutoCast.Text = "Start cast automatically";
            optAutoCast.CheckedChanged += SaveSettings;
            // 
            // optWindowsStart
            // 
            optWindowsStart.CheckOnClick = true;
            optWindowsStart.Name = "optWindowsStart";
            optWindowsStart.Size = new Size(266, 26);
            optWindowsStart.Text = "Start when Windows starts";
            optWindowsStart.Click += optWindowsStart_Click;
            // 
            // optSystray
            // 
            optSystray.CheckOnClick = true;
            optSystray.Name = "optSystray";
            optSystray.Size = new Size(266, 26);
            optSystray.Text = "Start in system tray";
            optSystray.CheckedChanged += SaveSettings;
            // 
            // optAutoSwitchDeck
            // 
            optAutoSwitchDeck.CheckOnClick = true;
            optAutoSwitchDeck.Name = "optAutoSwitchDeck";
            optAutoSwitchDeck.Size = new Size(266, 26);
            optAutoSwitchDeck.Text = "Auto change deck";
            optAutoSwitchDeck.Click += optAutoSwitchDeck_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(263, 6);
            // 
            // resetAllOptionsToolStripMenuItem
            // 
            resetAllOptionsToolStripMenuItem.Name = "resetAllOptionsToolStripMenuItem";
            resetAllOptionsToolStripMenuItem.Size = new Size(266, 26);
            resetAllOptionsToolStripMenuItem.Text = "Reset all options";
            resetAllOptionsToolStripMenuItem.Click += resetAllOptionsToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolRefreshDevices, toolClearLog });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(58, 24);
            aboutToolStripMenuItem.Text = "Tools";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // toolRefreshDevices
            // 
            toolRefreshDevices.Name = "toolRefreshDevices";
            toolRefreshDevices.Size = new Size(224, 26);
            toolRefreshDevices.Text = "Refresh cast devices";
            toolRefreshDevices.Click += toolRefreshDevices_Click;
            // 
            // toolClearLog
            // 
            toolClearLog.Name = "toolClearLog";
            toolClearLog.Size = new Size(224, 26);
            toolClearLog.Text = "Clear log";
            toolClearLog.Click += toolClearLog_Click;
            // 
            // pictureLogo
            // 
            pictureLogo.Image = Nest_Deck.Properties.Resources.NestDeck;
            pictureLogo.Location = new Point(8, 32);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(196, 47);
            pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogo.TabIndex = 28;
            pictureLogo.TabStop = false;
            // 
            // SHARK
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(531, 472);
            Controls.Add(pictureLogo);
            Controls.Add(numPort);
            Controls.Add(btRestart);
            Controls.Add(label1);
            Controls.Add(txtDebug);
            Controls.Add(pbBackground);
            Controls.Add(statusStrip1);
            Controls.Add(optionsMenu);
            Controls.Add(btCast);
            Controls.Add(lstCastDevices);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = optionsMenu;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "SHARK";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NestDeck - Windows client";
            Load += ARES_Load;
            Resize += SHARK_Resize;
            contextMenuTray.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbBackground).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            optionsMenu.ResumeLayout(false);
            optionsMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtDebug;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timerMonitorProcess;
        private NotifyIcon notifyIcon;
        private Label label1;
        private Button btRestart;
        private NumericUpDown numPort;
        private OpenFileDialog openFileDialog1;
        private ContextMenuStrip contextMenuTray;
        private ToolStripMenuItem toolStripMenuItem_AutoSwitch;
        private ToolStripMenuItem toolStripMenuItem_Exit;
        private ToolStripSeparator toolStripSeparator1;
        private Button btCast;
        private System.Windows.Forms.Timer timerCheckConnection;
        private ListView lstCastDevices;
        private ColumnHeader columnHeader1;
        private PictureBox pbBackground;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusAuth;
        private ToolStripStatusLabel statusData;
        private ToolStripStatusLabel statusCast;
        private MenuStrip optionsMenu;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem optWindowsStart;
        private ToolStripMenuItem optAutoCast;
        private ToolStripMenuItem optSystray;
        private ToolStripMenuItem optAutoSwitchDeck;
        private ToolStripMenuItem deckEditorToolStripMenuItem;
        private ColumnHeader columnHeader2;
        private ToolStripMenuItem toolstripOpenDeckEditor;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem resetAllOptionsToolStripMenuItem;
        private ToolStripMenuItem toolClearLog;
        private ToolStripMenuItem toolRefreshDevices;
        private PictureBox pictureLogo;
    }
}