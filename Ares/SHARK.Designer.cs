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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            timerMonitorProcess = new System.Windows.Forms.Timer(components);
            notifyIcon = new NotifyIcon(components);
            contextMenuTray = new ContextMenuStrip(components);
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem_Exit = new ToolStripMenuItem();
            label1 = new Label();
            btRestart = new Button();
            numPort = new NumericUpDown();
            openFileDialog1 = new OpenFileDialog();
            pbBackground = new PictureBox();
            statusStrip1 = new StatusStrip();
            statusAuth = new ToolStripStatusLabel();
            statusData = new ToolStripStatusLabel();
            statusCast = new ToolStripStatusLabel();
            pictureLogo = new PictureBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btUpdate = new Button();
            btReset = new Button();
            groupBox2 = new GroupBox();
            chkAutoStartCast = new CheckBox();
            chkWindowsStart = new CheckBox();
            chkStartTray = new CheckBox();
            chkAlwaysOnTop = new CheckBox();
            groupBox3 = new GroupBox();
            chkShowTrail = new CheckBox();
            chkMonitorProcess = new CheckBox();
            rdAudioFeedback_PlayInNH = new RadioButton();
            chkPlaySounds = new CheckBox();
            rdAudioFeedback_PlayInPc = new RadioButton();
            tabPage2 = new TabPage();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            btOBSWSConnect = new Button();
            lblOBSWSStatus = new Label();
            label6 = new Label();
            label5 = new Label();
            txtOBSPwd = new TextBox();
            label4 = new Label();
            lblOBSStatus = new Label();
            label3 = new Label();
            numOBSPort = new NumericUpDown();
            label2 = new Label();
            tabPage3 = new TabPage();
            groupBox1 = new GroupBox();
            button2 = new Button();
            label35 = new Label();
            panel6 = new Panel();
            label33 = new Label();
            ThresholdPingMax = new NumericUpDown();
            label32 = new Label();
            label34 = new Label();
            panel5 = new Panel();
            label30 = new Label();
            ThresholdNetUpMax = new NumericUpDown();
            label29 = new Label();
            label31 = new Label();
            panel4 = new Panel();
            label27 = new Label();
            ThresholdNetDownMax = new NumericUpDown();
            label26 = new Label();
            label28 = new Label();
            panel2 = new Panel();
            label24 = new Label();
            ThresholdRAMMax = new NumericUpDown();
            ThresholdRAMMin = new NumericUpDown();
            label23 = new Label();
            label25 = new Label();
            panel1 = new Panel();
            label20 = new Label();
            ThresholdGPUMax = new NumericUpDown();
            ThresholdGPUMin = new NumericUpDown();
            label19 = new Label();
            label22 = new Label();
            panel3 = new Panel();
            label17 = new Label();
            ThresholdCPUMax = new NumericUpDown();
            ThresholdCPUMin = new NumericUpDown();
            label14 = new Label();
            label16 = new Label();
            ThresholdPingMin = new NumericUpDown();
            ThresholdNetUpMin = new NumericUpDown();
            ThresholdNetDownMin = new NumericUpDown();
            tabPage4 = new TabPage();
            label37 = new Label();
            label15 = new Label();
            chkEnableBluetoothMonitoring = new CheckBox();
            label36 = new Label();
            button3 = new Button();
            gridBluetooth = new DataGridView();
            colSelected = new DataGridViewCheckBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCharge = new DataGridViewTextBoxColumn();
            colType = new DataGridViewComboBoxColumn();
            btEditor = new Button();
            label13 = new Label();
            lblNetDownload = new Label();
            lblMemoryUsage = new Label();
            lblMemoryUsed = new Label();
            lblGPUPower = new Label();
            lblGPUUsage = new Label();
            lblGPUTemp = new Label();
            lblCPUPower = new Label();
            lblCPUUsage = new Label();
            lblCPUTemp = new Label();
            timerMonitorHardware = new System.Windows.Forms.Timer(components);
            panelHwMon = new Panel();
            lblOBSSocket = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label21 = new Label();
            label18 = new Label();
            cmbDeviceList = new ComboBox();
            btCast = new Button();
            bgWorkerRefreshBtDevices = new System.ComponentModel.BackgroundWorker();
            btRefreshCastDevices = new Button();
            lnkClearLog = new LinkLabel();
            lblBattery = new Label();
            lnkAbout = new LinkLabel();
            txtDebugColor = new RichTextBox();
            chkVerbose = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbBackground).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numOBSPort).BeginInit();
            tabPage3.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdPingMax).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetUpMax).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetDownMax).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdRAMMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdRAMMin).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdGPUMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdGPUMin).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdCPUMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdCPUMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdPingMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetUpMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetDownMin).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridBluetooth).BeginInit();
            panelHwMon.SuspendLayout();
            SuspendLayout();
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
            contextMenuTray.Name = "contextMenuTray";
            contextMenuTray.Size = new Size(61, 4);
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
            label1.Location = new Point(619, 260);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 5;
            label1.Text = "Server Port";
            label1.Visible = false;
            // 
            // btRestart
            // 
            btRestart.ForeColor = SystemColors.ControlText;
            btRestart.Location = new Point(781, 255);
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
            numPort.Location = new Point(705, 258);
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
            // pbBackground
            // 
            pbBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbBackground.BackColor = Color.Black;
            pbBackground.Location = new Point(560, -3);
            pbBackground.Name = "pbBackground";
            pbBackground.Size = new Size(570, 525);
            pbBackground.TabIndex = 24;
            pbBackground.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusAuth, statusData, statusCast });
            statusStrip1.Location = new Point(0, 492);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1130, 30);
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
            // pictureLogo
            // 
            pictureLogo.BackColor = Color.Transparent;
            pictureLogo.Image = Nest_Deck.Properties.Resources.NestDeck;
            pictureLogo.Location = new Point(578, 13);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(196, 47);
            pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogo.TabIndex = 28;
            pictureLogo.TabStop = false;
            pictureLogo.Click += pictureLogo_Click;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(12, 100);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(542, 390);
            tabControl1.TabIndex = 29;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btUpdate);
            tabPage1.Controls.Add(btReset);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Location = new Point(4, 32);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(534, 354);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "🔧 Nest Deck";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btUpdate
            // 
            btUpdate.Location = new Point(3, 10);
            btUpdate.Name = "btUpdate";
            btUpdate.Size = new Size(180, 29);
            btUpdate.TabIndex = 6;
            btUpdate.Text = "✨ Check for updates";
            btUpdate.UseVisualStyleBackColor = true;
            btUpdate.Click += checkForUpdatesToolStripMenuItem_Click;
            // 
            // btReset
            // 
            btReset.Location = new Point(341, 10);
            btReset.Name = "btReset";
            btReset.Size = new Size(174, 29);
            btReset.TabIndex = 4;
            btReset.Text = "\U0001f9f9 Reset options";
            btReset.UseVisualStyleBackColor = true;
            btReset.Click += resetAllOptionsToolStripMenuItem_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkAutoStartCast);
            groupBox2.Controls.Add(chkWindowsStart);
            groupBox2.Controls.Add(chkStartTray);
            groupBox2.Controls.Add(chkAlwaysOnTop);
            groupBox2.Location = new Point(3, 45);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(512, 154);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Windows client settings";
            // 
            // chkAutoStartCast
            // 
            chkAutoStartCast.AutoSize = true;
            chkAutoStartCast.Location = new Point(17, 26);
            chkAutoStartCast.Name = "chkAutoStartCast";
            chkAutoStartCast.Size = new Size(323, 24);
            chkAutoStartCast.TabIndex = 0;
            chkAutoStartCast.Text = "Start cast automatically (to last used device)";
            chkAutoStartCast.UseVisualStyleBackColor = true;
            chkAutoStartCast.CheckedChanged += SaveSettings;
            // 
            // chkWindowsStart
            // 
            chkWindowsStart.AutoSize = true;
            chkWindowsStart.Location = new Point(17, 56);
            chkWindowsStart.Name = "chkWindowsStart";
            chkWindowsStart.Size = new Size(276, 24);
            chkWindowsStart.TabIndex = 1;
            chkWindowsStart.Text = "Start Nest Deck when Windows starts";
            chkWindowsStart.UseVisualStyleBackColor = true;
            chkWindowsStart.CheckedChanged += chkWindowsStart_CheckedChanged;
            // 
            // chkStartTray
            // 
            chkStartTray.AutoSize = true;
            chkStartTray.Location = new Point(17, 86);
            chkStartTray.Name = "chkStartTray";
            chkStartTray.Size = new Size(311, 24);
            chkStartTray.TabIndex = 2;
            chkStartTray.Text = "Start Nest Deck in system tray (minimized)";
            chkStartTray.UseVisualStyleBackColor = true;
            chkStartTray.CheckedChanged += SaveSettings;
            // 
            // chkAlwaysOnTop
            // 
            chkAlwaysOnTop.AutoSize = true;
            chkAlwaysOnTop.Location = new Point(17, 116);
            chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            chkAlwaysOnTop.Size = new Size(266, 24);
            chkAlwaysOnTop.TabIndex = 8;
            chkAlwaysOnTop.Text = "Keep Windows client always on top";
            chkAlwaysOnTop.UseVisualStyleBackColor = true;
            chkAlwaysOnTop.CheckedChanged += SaveSettings;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(chkShowTrail);
            groupBox3.Controls.Add(chkMonitorProcess);
            groupBox3.Controls.Add(rdAudioFeedback_PlayInNH);
            groupBox3.Controls.Add(chkPlaySounds);
            groupBox3.Controls.Add(rdAudioFeedback_PlayInPc);
            groupBox3.Location = new Point(3, 214);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(512, 124);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "Nest Hub settings";
            // 
            // chkShowTrail
            // 
            chkShowTrail.AutoSize = true;
            chkShowTrail.Location = new Point(17, 87);
            chkShowTrail.Name = "chkShowTrail";
            chkShowTrail.Size = new Size(256, 24);
            chkShowTrail.TabIndex = 12;
            chkShowTrail.Text = "Show red dot trail on touch/swipe";
            chkShowTrail.UseVisualStyleBackColor = true;
            chkShowTrail.CheckedChanged += SaveSettingsAndUpdateHub;
            // 
            // chkMonitorProcess
            // 
            chkMonitorProcess.AutoSize = true;
            chkMonitorProcess.Location = new Point(17, 26);
            chkMonitorProcess.Name = "chkMonitorProcess";
            chkMonitorProcess.Size = new Size(335, 24);
            chkMonitorProcess.TabIndex = 3;
            chkMonitorProcess.Text = "Auto change deck according to active process";
            chkMonitorProcess.UseVisualStyleBackColor = true;
            chkMonitorProcess.CheckedChanged += SaveSettings;
            // 
            // rdAudioFeedback_PlayInNH
            // 
            rdAudioFeedback_PlayInNH.AutoSize = true;
            rdAudioFeedback_PlayInNH.Location = new Point(365, 56);
            rdAudioFeedback_PlayInNH.Name = "rdAudioFeedback_PlayInNH";
            rdAudioFeedback_PlayInNH.Size = new Size(113, 24);
            rdAudioFeedback_PlayInNH.TabIndex = 11;
            rdAudioFeedback_PlayInNH.TabStop = true;
            rdAudioFeedback_PlayInNH.Text = "on Nest Hub";
            rdAudioFeedback_PlayInNH.UseVisualStyleBackColor = true;
            rdAudioFeedback_PlayInNH.CheckedChanged += SaveSettingsAndUpdateHub;
            // 
            // chkPlaySounds
            // 
            chkPlaySounds.AutoSize = true;
            chkPlaySounds.Location = new Point(17, 57);
            chkPlaySounds.Name = "chkPlaySounds";
            chkPlaySounds.Size = new Size(272, 24);
            chkPlaySounds.TabIndex = 9;
            chkPlaySounds.Text = "Play audio feedback on button press";
            chkPlaySounds.UseVisualStyleBackColor = true;
            chkPlaySounds.CheckedChanged += SaveSettingsAndUpdateHub;
            // 
            // rdAudioFeedback_PlayInPc
            // 
            rdAudioFeedback_PlayInPc.AutoSize = true;
            rdAudioFeedback_PlayInPc.Location = new Point(295, 56);
            rdAudioFeedback_PlayInPc.Name = "rdAudioFeedback_PlayInPc";
            rdAudioFeedback_PlayInPc.Size = new Size(68, 24);
            rdAudioFeedback_PlayInPc.TabIndex = 10;
            rdAudioFeedback_PlayInPc.TabStop = true;
            rdAudioFeedback_PlayInPc.Text = "on PC";
            rdAudioFeedback_PlayInPc.UseVisualStyleBackColor = true;
            rdAudioFeedback_PlayInPc.CheckedChanged += SaveSettingsAndUpdateHub;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(btOBSWSConnect);
            tabPage2.Controls.Add(lblOBSWSStatus);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(txtOBSPwd);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(lblOBSStatus);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(numOBSPort);
            tabPage2.Controls.Add(label2);
            tabPage2.Location = new Point(4, 32);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(534, 354);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "📽️ OBS";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 161);
            label9.Name = "label9";
            label9.Size = new Size(410, 20);
            label9.TabIndex = 12;
            label9.Text = "Copy your Port and Password to NestDeck and press Connect";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 141);
            label8.Name = "label8";
            label8.Size = new Size(220, 20);
            label8.TabIndex = 11;
            label8.Text = "Tick ✓ Enable WebSocket server";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 121);
            label7.Name = "label7";
            label7.Size = new Size(328, 20);
            label7.TabIndex = 10;
            label7.Text = "In OBS go to Tools -> WebSocket server settings";
            // 
            // btOBSWSConnect
            // 
            btOBSWSConnect.Location = new Point(12, 88);
            btOBSWSConnect.Name = "btOBSWSConnect";
            btOBSWSConnect.Size = new Size(478, 29);
            btOBSWSConnect.TabIndex = 9;
            btOBSWSConnect.Text = "Connect";
            btOBSWSConnect.UseVisualStyleBackColor = true;
            btOBSWSConnect.Click += btOBSWSConnect_Click;
            // 
            // lblOBSWSStatus
            // 
            lblOBSWSStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblOBSWSStatus.Location = new Point(381, 9);
            lblOBSWSStatus.Name = "lblOBSWSStatus";
            lblOBSWSStatus.Size = new Size(142, 20);
            lblOBSWSStatus.TabIndex = 8;
            lblOBSWSStatus.Text = "Not Connected";
            lblOBSWSStatus.TextAlign = ContentAlignment.TopRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(234, 9);
            label6.Name = "label6";
            label6.Size = new Size(125, 20);
            label6.TabIndex = 7;
            label6.Text = "WebSocket status";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(310, 69);
            label5.Name = "label5";
            label5.Size = new Size(180, 12);
            label5.TabIndex = 6;
            label5.Text = "(leave blank for disabled authentication)";
            // 
            // txtOBSPwd
            // 
            txtOBSPwd.Location = new Point(310, 39);
            txtOBSPwd.Name = "txtOBSPwd";
            txtOBSPwd.PasswordChar = '•';
            txtOBSPwd.Size = new Size(179, 27);
            txtOBSPwd.TabIndex = 5;
            txtOBSPwd.TextChanged += txtOBSPwd_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(234, 42);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 4;
            label4.Text = "Password";
            // 
            // lblOBSStatus
            // 
            lblOBSStatus.AutoSize = true;
            lblOBSStatus.Location = new Point(126, 9);
            lblOBSStatus.Name = "lblOBSStatus";
            lblOBSStatus.Size = new Size(92, 20);
            lblOBSStatus.TabIndex = 3;
            lblOBSStatus.Text = "Not Running";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 9);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 2;
            label3.Text = "OBS status";
            // 
            // numOBSPort
            // 
            numOBSPort.Location = new Point(130, 40);
            numOBSPort.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numOBSPort.Name = "numOBSPort";
            numOBSPort.Size = new Size(76, 27);
            numOBSPort.TabIndex = 1;
            numOBSPort.Value = new decimal(new int[] { 4445, 0, 0, 0 });
            numOBSPort.ValueChanged += numOBSPort_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 42);
            label2.Name = "label2";
            label2.Size = new Size(113, 20);
            label2.TabIndex = 0;
            label2.Text = "WebSocket Port";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Controls.Add(button2);
            tabPage3.Controls.Add(label35);
            tabPage3.Controls.Add(panel6);
            tabPage3.Controls.Add(ThresholdPingMax);
            tabPage3.Controls.Add(label32);
            tabPage3.Controls.Add(label34);
            tabPage3.Controls.Add(panel5);
            tabPage3.Controls.Add(ThresholdNetUpMax);
            tabPage3.Controls.Add(label29);
            tabPage3.Controls.Add(label31);
            tabPage3.Controls.Add(panel4);
            tabPage3.Controls.Add(ThresholdNetDownMax);
            tabPage3.Controls.Add(label26);
            tabPage3.Controls.Add(label28);
            tabPage3.Controls.Add(panel2);
            tabPage3.Controls.Add(ThresholdRAMMax);
            tabPage3.Controls.Add(ThresholdRAMMin);
            tabPage3.Controls.Add(label23);
            tabPage3.Controls.Add(label25);
            tabPage3.Controls.Add(panel1);
            tabPage3.Controls.Add(ThresholdGPUMax);
            tabPage3.Controls.Add(ThresholdGPUMin);
            tabPage3.Controls.Add(label19);
            tabPage3.Controls.Add(label22);
            tabPage3.Controls.Add(panel3);
            tabPage3.Controls.Add(ThresholdCPUMax);
            tabPage3.Controls.Add(ThresholdCPUMin);
            tabPage3.Controls.Add(label14);
            tabPage3.Controls.Add(label16);
            tabPage3.Controls.Add(ThresholdPingMin);
            tabPage3.Controls.Add(ThresholdNetUpMin);
            tabPage3.Controls.Add(ThresholdNetDownMin);
            tabPage3.Location = new Point(4, 32);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(534, 354);
            tabPage3.TabIndex = 3;
            tabPage3.Text = "📉 Hardware monitor";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(254, 36);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1, 154);
            groupBox1.TabIndex = 43;
            groupBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(394, 206);
            button2.Name = "button2";
            button2.Size = new Size(125, 29);
            button2.TabIndex = 42;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(12, 8);
            label35.Name = "label35";
            label35.Size = new Size(374, 20);
            label35.TabIndex = 41;
            label35.Text = "Customize hardware monitor indicator color thresholds";
            // 
            // panel6
            // 
            panel6.BackColor = Color.Green;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(label33);
            panel6.Location = new Point(297, 163);
            panel6.Name = "panel6";
            panel6.Size = new Size(64, 27);
            panel6.TabIndex = 37;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.ForeColor = Color.White;
            label33.Location = new Point(23, 1);
            label33.Name = "label33";
            label33.Size = new Size(17, 20);
            label33.TabIndex = 40;
            label33.Text = "0";
            // 
            // ThresholdPingMax
            // 
            ThresholdPingMax.BackColor = Color.Firebrick;
            ThresholdPingMax.ForeColor = Color.White;
            ThresholdPingMax.Location = new Point(430, 163);
            ThresholdPingMax.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            ThresholdPingMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdPingMax.Name = "ThresholdPingMax";
            ThresholdPingMax.Size = new Size(64, 27);
            ThresholdPingMax.TabIndex = 38;
            ThresholdPingMax.Value = new decimal(new int[] { 900, 0, 0, 0 });
            ThresholdPingMax.ValueChanged += Threshold_ValueChanged;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label32.Location = new Point(293, 140);
            label32.Name = "label32";
            label32.Size = new Size(138, 20);
            label32.TabIndex = 35;
            label32.Text = "Ping (miliseconds)";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(498, 165);
            label34.Name = "label34";
            label34.Size = new Size(21, 20);
            label34.TabIndex = 39;
            label34.Text = "∞";
            // 
            // panel5
            // 
            panel5.BackColor = Color.Green;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(label30);
            panel5.Location = new Point(297, 112);
            panel5.Name = "panel5";
            panel5.Size = new Size(64, 27);
            panel5.TabIndex = 31;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.ForeColor = Color.White;
            label30.Location = new Point(23, 1);
            label30.Name = "label30";
            label30.Size = new Size(17, 20);
            label30.TabIndex = 34;
            label30.Text = "0";
            // 
            // ThresholdNetUpMax
            // 
            ThresholdNetUpMax.BackColor = Color.Firebrick;
            ThresholdNetUpMax.ForeColor = Color.White;
            ThresholdNetUpMax.Location = new Point(430, 112);
            ThresholdNetUpMax.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            ThresholdNetUpMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdNetUpMax.Name = "ThresholdNetUpMax";
            ThresholdNetUpMax.Size = new Size(64, 27);
            ThresholdNetUpMax.TabIndex = 32;
            ThresholdNetUpMax.Value = new decimal(new int[] { 50, 0, 0, 0 });
            ThresholdNetUpMax.ValueChanged += Threshold_ValueChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label29.Location = new Point(293, 89);
            label29.Name = "label29";
            label29.Size = new Size(114, 20);
            label29.TabIndex = 29;
            label29.Text = "Upload (mbps)";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(498, 114);
            label31.Name = "label31";
            label31.Size = new Size(21, 20);
            label31.TabIndex = 33;
            label31.Text = "∞";
            // 
            // panel4
            // 
            panel4.BackColor = Color.Green;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label27);
            panel4.Location = new Point(297, 59);
            panel4.Name = "panel4";
            panel4.Size = new Size(64, 27);
            panel4.TabIndex = 25;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.ForeColor = Color.White;
            label27.Location = new Point(23, 1);
            label27.Name = "label27";
            label27.Size = new Size(17, 20);
            label27.TabIndex = 28;
            label27.Text = "0";
            // 
            // ThresholdNetDownMax
            // 
            ThresholdNetDownMax.BackColor = Color.Firebrick;
            ThresholdNetDownMax.ForeColor = Color.White;
            ThresholdNetDownMax.Location = new Point(430, 59);
            ThresholdNetDownMax.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            ThresholdNetDownMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdNetDownMax.Name = "ThresholdNetDownMax";
            ThresholdNetDownMax.Size = new Size(64, 27);
            ThresholdNetDownMax.TabIndex = 26;
            ThresholdNetDownMax.Value = new decimal(new int[] { 50, 0, 0, 0 });
            ThresholdNetDownMax.ValueChanged += Threshold_ValueChanged;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label26.Location = new Point(292, 36);
            label26.Name = "label26";
            label26.Size = new Size(135, 20);
            label26.TabIndex = 23;
            label26.Text = "Download (mbps)";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(498, 61);
            label28.Name = "label28";
            label28.Size = new Size(21, 20);
            label28.TabIndex = 27;
            label28.Text = "∞";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Green;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label24);
            panel2.Location = new Point(20, 163);
            panel2.Name = "panel2";
            panel2.Size = new Size(50, 27);
            panel2.TabIndex = 19;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.ForeColor = Color.White;
            label24.Location = new Point(15, 1);
            label24.Name = "label24";
            label24.Size = new Size(17, 20);
            label24.TabIndex = 22;
            label24.Text = "0";
            // 
            // ThresholdRAMMax
            // 
            ThresholdRAMMax.BackColor = Color.Firebrick;
            ThresholdRAMMax.ForeColor = Color.White;
            ThresholdRAMMax.Location = new Point(125, 163);
            ThresholdRAMMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdRAMMax.Name = "ThresholdRAMMax";
            ThresholdRAMMax.Size = new Size(50, 27);
            ThresholdRAMMax.TabIndex = 20;
            ThresholdRAMMax.Value = new decimal(new int[] { 50, 0, 0, 0 });
            ThresholdRAMMax.ValueChanged += Threshold_ValueChanged;
            // 
            // ThresholdRAMMin
            // 
            ThresholdRAMMin.BackColor = Color.Gold;
            ThresholdRAMMin.ForeColor = Color.Black;
            ThresholdRAMMin.Location = new Point(73, 163);
            ThresholdRAMMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdRAMMin.Name = "ThresholdRAMMin";
            ThresholdRAMMin.Size = new Size(50, 27);
            ThresholdRAMMin.TabIndex = 18;
            ThresholdRAMMin.Value = new decimal(new int[] { 15, 0, 0, 0 });
            ThresholdRAMMin.ValueChanged += Threshold_ValueChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label23.Location = new Point(12, 140);
            label23.Name = "label23";
            label23.Size = new Size(108, 20);
            label23.TabIndex = 17;
            label23.Text = "RAM Usage %";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(174, 165);
            label25.Name = "label25";
            label25.Size = new Size(45, 20);
            label25.TabIndex = 21;
            label25.Text = "100%";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Green;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label20);
            panel1.Location = new Point(20, 112);
            panel1.Name = "panel1";
            panel1.Size = new Size(50, 27);
            panel1.TabIndex = 13;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.ForeColor = Color.White;
            label20.Location = new Point(15, 1);
            label20.Name = "label20";
            label20.Size = new Size(17, 20);
            label20.TabIndex = 16;
            label20.Text = "0";
            // 
            // ThresholdGPUMax
            // 
            ThresholdGPUMax.BackColor = Color.Firebrick;
            ThresholdGPUMax.ForeColor = Color.White;
            ThresholdGPUMax.Location = new Point(125, 112);
            ThresholdGPUMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdGPUMax.Name = "ThresholdGPUMax";
            ThresholdGPUMax.Size = new Size(50, 27);
            ThresholdGPUMax.TabIndex = 14;
            ThresholdGPUMax.Value = new decimal(new int[] { 50, 0, 0, 0 });
            ThresholdGPUMax.ValueChanged += Threshold_ValueChanged;
            // 
            // ThresholdGPUMin
            // 
            ThresholdGPUMin.BackColor = Color.Gold;
            ThresholdGPUMin.ForeColor = Color.Black;
            ThresholdGPUMin.Location = new Point(73, 112);
            ThresholdGPUMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdGPUMin.Name = "ThresholdGPUMin";
            ThresholdGPUMin.Size = new Size(50, 27);
            ThresholdGPUMin.TabIndex = 12;
            ThresholdGPUMin.Value = new decimal(new int[] { 15, 0, 0, 0 });
            ThresholdGPUMin.ValueChanged += Threshold_ValueChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label19.Location = new Point(12, 89);
            label19.Name = "label19";
            label19.Size = new Size(104, 20);
            label19.TabIndex = 11;
            label19.Text = "GPU Usage %";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(174, 114);
            label22.Name = "label22";
            label22.Size = new Size(45, 20);
            label22.TabIndex = 15;
            label22.Text = "100%";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Green;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label17);
            panel3.Location = new Point(20, 59);
            panel3.Name = "panel3";
            panel3.Size = new Size(50, 27);
            panel3.TabIndex = 3;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.ForeColor = Color.White;
            label17.Location = new Point(15, 1);
            label17.Name = "label17";
            label17.Size = new Size(17, 20);
            label17.TabIndex = 10;
            label17.Text = "0";
            // 
            // ThresholdCPUMax
            // 
            ThresholdCPUMax.BackColor = Color.Firebrick;
            ThresholdCPUMax.ForeColor = Color.White;
            ThresholdCPUMax.Location = new Point(125, 59);
            ThresholdCPUMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdCPUMax.Name = "ThresholdCPUMax";
            ThresholdCPUMax.Size = new Size(50, 27);
            ThresholdCPUMax.TabIndex = 5;
            ThresholdCPUMax.Value = new decimal(new int[] { 50, 0, 0, 0 });
            ThresholdCPUMax.ValueChanged += Threshold_ValueChanged;
            // 
            // ThresholdCPUMin
            // 
            ThresholdCPUMin.BackColor = Color.Gold;
            ThresholdCPUMin.ForeColor = Color.Black;
            ThresholdCPUMin.Location = new Point(73, 59);
            ThresholdCPUMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdCPUMin.Name = "ThresholdCPUMin";
            ThresholdCPUMin.Size = new Size(50, 27);
            ThresholdCPUMin.TabIndex = 2;
            ThresholdCPUMin.Value = new decimal(new int[] { 15, 0, 0, 0 });
            ThresholdCPUMin.ValueChanged += Threshold_ValueChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(12, 36);
            label14.Name = "label14";
            label14.Size = new Size(102, 20);
            label14.TabIndex = 0;
            label14.Text = "CPU Usage %";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(174, 61);
            label16.Name = "label16";
            label16.Size = new Size(45, 20);
            label16.TabIndex = 9;
            label16.Text = "100%";
            // 
            // ThresholdPingMin
            // 
            ThresholdPingMin.BackColor = Color.Gold;
            ThresholdPingMin.ForeColor = Color.Black;
            ThresholdPingMin.Location = new Point(364, 163);
            ThresholdPingMin.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            ThresholdPingMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdPingMin.Name = "ThresholdPingMin";
            ThresholdPingMin.Size = new Size(64, 27);
            ThresholdPingMin.TabIndex = 36;
            ThresholdPingMin.Value = new decimal(new int[] { 500, 0, 0, 0 });
            ThresholdPingMin.ValueChanged += Threshold_ValueChanged;
            // 
            // ThresholdNetUpMin
            // 
            ThresholdNetUpMin.BackColor = Color.Gold;
            ThresholdNetUpMin.ForeColor = Color.Black;
            ThresholdNetUpMin.Location = new Point(364, 112);
            ThresholdNetUpMin.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            ThresholdNetUpMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdNetUpMin.Name = "ThresholdNetUpMin";
            ThresholdNetUpMin.Size = new Size(64, 27);
            ThresholdNetUpMin.TabIndex = 30;
            ThresholdNetUpMin.Value = new decimal(new int[] { 2, 0, 0, 0 });
            ThresholdNetUpMin.ValueChanged += Threshold_ValueChanged;
            // 
            // ThresholdNetDownMin
            // 
            ThresholdNetDownMin.BackColor = Color.Gold;
            ThresholdNetDownMin.ForeColor = Color.Black;
            ThresholdNetDownMin.Location = new Point(364, 59);
            ThresholdNetDownMin.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            ThresholdNetDownMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdNetDownMin.Name = "ThresholdNetDownMin";
            ThresholdNetDownMin.Size = new Size(64, 27);
            ThresholdNetDownMin.TabIndex = 24;
            ThresholdNetDownMin.Value = new decimal(new int[] { 2, 0, 0, 0 });
            ThresholdNetDownMin.ValueChanged += Threshold_ValueChanged;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(label37);
            tabPage4.Controls.Add(label15);
            tabPage4.Controls.Add(chkEnableBluetoothMonitoring);
            tabPage4.Controls.Add(label36);
            tabPage4.Controls.Add(button3);
            tabPage4.Controls.Add(gridBluetooth);
            tabPage4.Location = new Point(4, 32);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(534, 354);
            tabPage4.TabIndex = 4;
            tabPage4.Text = "\U0001faab Bluetooth devices";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            label37.Location = new Point(35, 50);
            label37.Name = "label37";
            label37.Size = new Size(194, 17);
            label37.TabIndex = 47;
            label37.Text = "Until fixed, use at your own risk)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(35, 33);
            label15.Name = "label15";
            label15.Size = new Size(435, 17);
            label15.TabIndex = 46;
            label15.Text = "(WARNING: This is known to cause keyboard input lag on some machines";
            // 
            // chkEnableBluetoothMonitoring
            // 
            chkEnableBluetoothMonitoring.AutoSize = true;
            chkEnableBluetoothMonitoring.Location = new Point(16, 11);
            chkEnableBluetoothMonitoring.Name = "chkEnableBluetoothMonitoring";
            chkEnableBluetoothMonitoring.Size = new Size(274, 24);
            chkEnableBluetoothMonitoring.TabIndex = 45;
            chkEnableBluetoothMonitoring.Text = "Enable bluetooth battery monitoring";
            chkEnableBluetoothMonitoring.UseVisualStyleBackColor = true;
            chkEnableBluetoothMonitoring.CheckedChanged += SaveSettings;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new Point(16, 77);
            label36.Name = "label36";
            label36.Size = new Size(250, 20);
            label36.TabIndex = 44;
            label36.Text = "Bluetooth battery indicators to show";
            // 
            // button3
            // 
            button3.Location = new Point(390, 322);
            button3.Name = "button3";
            button3.Size = new Size(125, 29);
            button3.TabIndex = 43;
            button3.Text = "Save";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button2_Click;
            // 
            // gridBluetooth
            // 
            gridBluetooth.AllowUserToAddRows = false;
            gridBluetooth.AllowUserToDeleteRows = false;
            gridBluetooth.AllowUserToResizeRows = false;
            gridBluetooth.BorderStyle = BorderStyle.None;
            gridBluetooth.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridBluetooth.Columns.AddRange(new DataGridViewColumn[] { colSelected, colName, colCharge, colType });
            gridBluetooth.Location = new Point(16, 100);
            gridBluetooth.Name = "gridBluetooth";
            gridBluetooth.RowHeadersVisible = false;
            gridBluetooth.RowHeadersWidth = 51;
            gridBluetooth.RowTemplate.Height = 29;
            gridBluetooth.Size = new Size(499, 216);
            gridBluetooth.TabIndex = 0;
            gridBluetooth.CellValueChanged += gridBluetooth_CellValueChanged;
            // 
            // colSelected
            // 
            colSelected.HeaderText = "Show";
            colSelected.MinimumWidth = 6;
            colSelected.Name = "colSelected";
            colSelected.Width = 65;
            // 
            // colName
            // 
            colName.HeaderText = "Device";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Resizable = DataGridViewTriState.True;
            colName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colName.Width = 165;
            // 
            // colCharge
            // 
            colCharge.HeaderText = "Battery (%)";
            colCharge.MinimumWidth = 6;
            colCharge.Name = "colCharge";
            colCharge.ReadOnly = true;
            colCharge.Width = 110;
            // 
            // colType
            // 
            colType.HeaderText = "Icon";
            colType.Items.AddRange(new object[] { "None", "Audio", "Mouse", "Keyboard", "Gamepad" });
            colType.MinimumWidth = 6;
            colType.Name = "colType";
            colType.Width = 125;
            // 
            // btEditor
            // 
            btEditor.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btEditor.Location = new Point(12, 54);
            btEditor.Name = "btEditor";
            btEditor.Size = new Size(538, 40);
            btEditor.TabIndex = 5;
            btEditor.Text = "✏️ Open deck editor in browser";
            btEditor.UseVisualStyleBackColor = true;
            btEditor.Click += toolstripOpenDeckEditor_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Consolas", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(232, 10);
            label13.Name = "label13";
            label13.Size = new Size(14, 15);
            label13.TabIndex = 8;
            label13.Text = "↓";
            // 
            // lblNetDownload
            // 
            lblNetDownload.AutoSize = true;
            lblNetDownload.Location = new Point(246, 12);
            lblNetDownload.Name = "lblNetDownload";
            lblNetDownload.Size = new Size(84, 15);
            lblNetDownload.TabIndex = 7;
            lblNetDownload.Text = "1000.00Mbps";
            // 
            // lblMemoryUsage
            // 
            lblMemoryUsage.Location = new Point(480, -3);
            lblMemoryUsage.Name = "lblMemoryUsage";
            lblMemoryUsage.Size = new Size(56, 15);
            lblMemoryUsage.TabIndex = 6;
            lblMemoryUsage.Text = "100.00%";
            lblMemoryUsage.TextAlign = ContentAlignment.TopRight;
            // 
            // lblMemoryUsed
            // 
            lblMemoryUsed.AutoSize = true;
            lblMemoryUsed.Location = new Point(424, -3);
            lblMemoryUsed.Name = "lblMemoryUsed";
            lblMemoryUsed.Size = new Size(56, 15);
            lblMemoryUsed.TabIndex = 3;
            lblMemoryUsed.Text = "13.37GB";
            // 
            // lblGPUPower
            // 
            lblGPUPower.AutoSize = true;
            lblGPUPower.Location = new Point(273, -3);
            lblGPUPower.Name = "lblGPUPower";
            lblGPUPower.Size = new Size(49, 15);
            lblGPUPower.TabIndex = 8;
            lblGPUPower.Text = "22.56W";
            // 
            // lblGPUUsage
            // 
            lblGPUUsage.Location = new Point(319, -3);
            lblGPUUsage.Name = "lblGPUUsage";
            lblGPUUsage.Size = new Size(56, 15);
            lblGPUUsage.TabIndex = 6;
            lblGPUUsage.Text = "100.00%";
            lblGPUUsage.TextAlign = ContentAlignment.TopRight;
            // 
            // lblGPUTemp
            // 
            lblGPUTemp.AutoSize = true;
            lblGPUTemp.Location = new Point(231, -3);
            lblGPUTemp.Name = "lblGPUTemp";
            lblGPUTemp.Size = new Size(42, 15);
            lblGPUTemp.TabIndex = 3;
            lblGPUTemp.Text = "110ºC";
            // 
            // lblCPUPower
            // 
            lblCPUPower.AutoSize = true;
            lblCPUPower.Font = new Font("Consolas", 7.20000029F, FontStyle.Bold, GraphicsUnit.Point);
            lblCPUPower.Location = new Point(74, -3);
            lblCPUPower.Name = "lblCPUPower";
            lblCPUPower.Size = new Size(49, 15);
            lblCPUPower.TabIndex = 8;
            lblCPUPower.Text = "22.56W";
            // 
            // lblCPUUsage
            // 
            lblCPUUsage.Font = new Font("Consolas", 7.20000029F, FontStyle.Bold, GraphicsUnit.Point);
            lblCPUUsage.Location = new Point(120, -3);
            lblCPUUsage.Name = "lblCPUUsage";
            lblCPUUsage.Size = new Size(56, 15);
            lblCPUUsage.TabIndex = 6;
            lblCPUUsage.Text = "100.00%";
            lblCPUUsage.TextAlign = ContentAlignment.TopRight;
            // 
            // lblCPUTemp
            // 
            lblCPUTemp.AutoSize = true;
            lblCPUTemp.Font = new Font("Consolas", 7.20000029F, FontStyle.Bold, GraphicsUnit.Point);
            lblCPUTemp.ForeColor = Color.White;
            lblCPUTemp.Location = new Point(32, -3);
            lblCPUTemp.Name = "lblCPUTemp";
            lblCPUTemp.Size = new Size(42, 15);
            lblCPUTemp.TabIndex = 3;
            lblCPUTemp.Text = "110ºC";
            // 
            // timerMonitorHardware
            // 
            timerMonitorHardware.Enabled = true;
            timerMonitorHardware.Interval = 5000;
            timerMonitorHardware.Tick += timerMonitorHardware_Tick;
            // 
            // panelHwMon
            // 
            panelHwMon.BackColor = Color.Black;
            panelHwMon.Controls.Add(lblOBSSocket);
            panelHwMon.Controls.Add(label12);
            panelHwMon.Controls.Add(label11);
            panelHwMon.Controls.Add(lblMemoryUsage);
            panelHwMon.Controls.Add(label13);
            panelHwMon.Controls.Add(label10);
            panelHwMon.Controls.Add(lblGPUPower);
            panelHwMon.Controls.Add(lblNetDownload);
            panelHwMon.Controls.Add(label21);
            panelHwMon.Controls.Add(lblMemoryUsed);
            panelHwMon.Controls.Add(lblCPUUsage);
            panelHwMon.Controls.Add(lblGPUUsage);
            panelHwMon.Controls.Add(lblCPUPower);
            panelHwMon.Controls.Add(lblGPUTemp);
            panelHwMon.Controls.Add(label18);
            panelHwMon.Controls.Add(lblCPUTemp);
            panelHwMon.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            panelHwMon.ForeColor = Color.White;
            panelHwMon.Location = new Point(577, 74);
            panelHwMon.Name = "panelHwMon";
            panelHwMon.Size = new Size(542, 29);
            panelHwMon.TabIndex = 30;
            // 
            // lblOBSSocket
            // 
            lblOBSSocket.AutoSize = true;
            lblOBSSocket.Location = new Point(32, 12);
            lblOBSSocket.Name = "lblOBSSocket";
            lblOBSSocket.Size = new Size(98, 15);
            lblOBSSocket.TabIndex = 13;
            lblOBSSocket.Text = "Not connected";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.MediumOrchid;
            label12.Location = new Point(3, 12);
            label12.Name = "label12";
            label12.Size = new Size(28, 15);
            label12.TabIndex = 12;
            label12.Text = "OBS";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.FromArgb(77, 138, 248);
            label11.Location = new Point(202, 12);
            label11.Name = "label11";
            label11.Size = new Size(28, 15);
            label11.TabIndex = 11;
            label11.Text = "NET";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.FromArgb(24, 160, 94);
            label10.Location = new Point(399, -3);
            label10.Name = "label10";
            label10.Size = new Size(28, 15);
            label10.TabIndex = 10;
            label10.Text = "RAM";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            label21.ForeColor = Color.FromArgb(244, 184, 8);
            label21.Location = new Point(202, -3);
            label21.Name = "label21";
            label21.Size = new Size(28, 15);
            label21.TabIndex = 9;
            label21.Text = "GPU";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            label18.ForeColor = Color.FromArgb(201, 34, 0);
            label18.Location = new Point(3, -3);
            label18.Name = "label18";
            label18.Size = new Size(28, 15);
            label18.TabIndex = 0;
            label18.Text = "CPU";
            // 
            // cmbDeviceList
            // 
            cmbDeviceList.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeviceList.Enabled = false;
            cmbDeviceList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbDeviceList.FormattingEnabled = true;
            cmbDeviceList.Location = new Point(12, 12);
            cmbDeviceList.Name = "cmbDeviceList";
            cmbDeviceList.Size = new Size(321, 36);
            cmbDeviceList.TabIndex = 34;
            cmbDeviceList.SelectedIndexChanged += cmbDevices_SelectedIndexChanged;
            // 
            // btCast
            // 
            btCast.AutoSize = true;
            btCast.Enabled = false;
            btCast.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btCast.Location = new Point(394, 10);
            btCast.Name = "btCast";
            btCast.Size = new Size(156, 38);
            btCast.TabIndex = 35;
            btCast.Text = "📺 Cast";
            btCast.UseVisualStyleBackColor = true;
            btCast.Click += btCast_Click;
            // 
            // bgWorkerRefreshBtDevices
            // 
            bgWorkerRefreshBtDevices.WorkerReportsProgress = true;
            bgWorkerRefreshBtDevices.DoWork += bgWorkerRefreshBtDevices_DoWork;
            bgWorkerRefreshBtDevices.ProgressChanged += bgWorkerRefreshBtDevices_ProgressChanged;
            // 
            // btRefreshCastDevices
            // 
            btRefreshCastDevices.AutoSize = true;
            btRefreshCastDevices.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btRefreshCastDevices.Location = new Point(339, 10);
            btRefreshCastDevices.Name = "btRefreshCastDevices";
            btRefreshCastDevices.Size = new Size(49, 38);
            btRefreshCastDevices.TabIndex = 36;
            btRefreshCastDevices.Text = "🔄";
            btRefreshCastDevices.UseVisualStyleBackColor = true;
            btRefreshCastDevices.Click += toolRefreshDevices_Click;
            // 
            // lnkClearLog
            // 
            lnkClearLog.AutoSize = true;
            lnkClearLog.BackColor = Color.Transparent;
            lnkClearLog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lnkClearLog.LinkColor = Color.FromArgb(201, 34, 0);
            lnkClearLog.Location = new Point(1050, 29);
            lnkClearLog.Name = "lnkClearLog";
            lnkClearLog.Size = new Size(69, 20);
            lnkClearLog.TabIndex = 37;
            lnkClearLog.TabStop = true;
            lnkClearLog.Text = "Clear log";
            lnkClearLog.VisitedLinkColor = Color.FromArgb(201, 34, 0);
            lnkClearLog.Click += toolClearLog_Click;
            // 
            // lblBattery
            // 
            lblBattery.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblBattery.AutoSize = true;
            lblBattery.BackColor = Color.Black;
            lblBattery.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            lblBattery.ForeColor = Color.White;
            lblBattery.Location = new Point(582, 499);
            lblBattery.Name = "lblBattery";
            lblBattery.Size = new Size(341, 15);
            lblBattery.TabIndex = 38;
            lblBattery.Text = "⚡No Bluetooth devices detected for battery info";
            lblBattery.Visible = false;
            // 
            // lnkAbout
            // 
            lnkAbout.AutoSize = true;
            lnkAbout.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lnkAbout.LinkColor = Color.FromArgb(231, 34, 0);
            lnkAbout.Location = new Point(771, 29);
            lnkAbout.Name = "lnkAbout";
            lnkAbout.Size = new Size(28, 20);
            lnkAbout.TabIndex = 39;
            lnkAbout.TabStop = true;
            lnkAbout.Text = "1.0";
            lnkAbout.VisitedLinkColor = Color.FromArgb(231, 34, 0);
            lnkAbout.Click += aboutToolStripMenuItem1_Click;
            // 
            // txtDebugColor
            // 
            txtDebugColor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDebugColor.BackColor = Color.FromArgb(34, 34, 34);
            txtDebugColor.BorderStyle = BorderStyle.None;
            txtDebugColor.Font = new Font("Consolas", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            txtDebugColor.ForeColor = Color.Silver;
            txtDebugColor.Location = new Point(582, 110);
            txtDebugColor.Name = "txtDebugColor";
            txtDebugColor.ReadOnly = true;
            txtDebugColor.ScrollBars = RichTextBoxScrollBars.None;
            txtDebugColor.Size = new Size(537, 386);
            txtDebugColor.TabIndex = 40;
            txtDebugColor.Text = "";
            // 
            // chkVerbose
            // 
            chkVerbose.AutoSize = true;
            chkVerbose.BackColor = Color.FromArgb(34, 34, 34);
            chkVerbose.FlatStyle = FlatStyle.Flat;
            chkVerbose.Font = new Font("Segoe UI", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            chkVerbose.ForeColor = Color.LightGray;
            chkVerbose.Location = new Point(932, 30);
            chkVerbose.Name = "chkVerbose";
            chkVerbose.Size = new Size(112, 21);
            chkVerbose.TabIndex = 41;
            chkVerbose.Text = "Verbose mode";
            chkVerbose.UseVisualStyleBackColor = false;
            chkVerbose.CheckedChanged += chkVerbose_CheckedChanged;
            // 
            // SHARK
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1130, 522);
            Controls.Add(txtDebugColor);
            Controls.Add(chkVerbose);
            Controls.Add(lblBattery);
            Controls.Add(lnkAbout);
            Controls.Add(pictureLogo);
            Controls.Add(btEditor);
            Controls.Add(lnkClearLog);
            Controls.Add(btRefreshCastDevices);
            Controls.Add(btCast);
            Controls.Add(cmbDeviceList);
            Controls.Add(panelHwMon);
            Controls.Add(tabControl1);
            Controls.Add(pbBackground);
            Controls.Add(statusStrip1);
            Controls.Add(numPort);
            Controls.Add(btRestart);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "SHARK";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NestDeck - Windows client";
            Load += ARES_Load;
            Resize += SHARK_Resize;
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbBackground).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numOBSPort).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdPingMax).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetUpMax).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetDownMax).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdRAMMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdRAMMin).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdGPUMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdGPUMin).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdCPUMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdCPUMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdPingMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetUpMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThresholdNetDownMin).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridBluetooth).EndInit();
            panelHwMon.ResumeLayout(false);
            panelHwMon.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timerMonitorProcess;
        private NotifyIcon notifyIcon;
        private Label label1;
        private Button btRestart;
        private NumericUpDown numPort;
        private OpenFileDialog openFileDialog1;
        private ContextMenuStrip contextMenuTray;
        private ToolStripMenuItem toolStripMenuItem_Exit;
        private ToolStripSeparator toolStripSeparator1;
        private PictureBox pbBackground;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusAuth;
        private ToolStripStatusLabel statusData;
        private ToolStripStatusLabel statusCast;
        private PictureBox pictureLogo;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private Label lblOBSStatus;
        private Label label3;
        private NumericUpDown numOBSPort;
        private Label label2;
        private TextBox txtOBSPwd;
        private Label label4;
        private Label lblOBSWSStatus;
        private Label label6;
        private Label label5;
        private Button btOBSWSConnect;
        private Label label9;
        private Label label8;
        private Label label7;
        private System.Windows.Forms.Timer timerMonitorHardware;
        private Label lblCPUUsage;
        private Label lblCPUTemp;
        private Label lblCPUPower;
        private Label lblGPUPower;
        private Label lblGPUUsage;
        private Label lblGPUTemp;
        private Label lblNetDownload;
        private Label lblMemoryUsed;
        private Label lblMemoryUsage;
        private Label label13;
        private Panel panelHwMon;
        private Label label18;
        private Label label21;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label lblOBSSocket;
        private TabPage tabPage1;
        private CheckBox chkMonitorProcess;
        private CheckBox chkStartTray;
        private CheckBox chkWindowsStart;
        private CheckBox chkAutoStartCast;
        private Button btReset;
        private TabPage tabPage3;
        private Label label14;
        private NumericUpDown ThresholdCPUMin;
        private Panel panel3;
        private NumericUpDown ThresholdCPUMax;
        private Label label17;
        private Label label16;
        private Panel panel4;
        private NumericUpDown ThresholdNetDownMax;
        private NumericUpDown ThresholdNetDownMin;
        private Label label26;
        private Label label27;
        private Label label28;
        private Panel panel2;
        private NumericUpDown ThresholdRAMMax;
        private NumericUpDown ThresholdRAMMin;
        private Label label23;
        private Label label24;
        private Label label25;
        private Panel panel1;
        private NumericUpDown ThresholdGPUMax;
        private NumericUpDown ThresholdGPUMin;
        private Label label19;
        private Label label20;
        private Label label22;
        private Panel panel6;
        private NumericUpDown ThresholdPingMax;
        private NumericUpDown ThresholdPingMin;
        private Label label32;
        private Label label33;
        private Label label34;
        private Panel panel5;
        private NumericUpDown ThresholdNetUpMax;
        private NumericUpDown ThresholdNetUpMin;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label label35;
        private Button button2;
        private GroupBox groupBox1;
        private ComboBox cmbDeviceList;
        private Button btCast;
        private TabPage tabPage4;
        private System.ComponentModel.BackgroundWorker bgWorkerRefreshBtDevices;
        private DataGridView gridBluetooth;
        private Button button3;
        private Label label36;
        private Button btRefreshCastDevices;
        private LinkLabel lnkClearLog;
        private Button btEditor;
        private Button btUpdate;
        private CheckBox chkAlwaysOnTop;
        private Label lblBattery;
        private LinkLabel lnkAbout;
        private CheckBox chkPlaySounds;
        private DataGridViewCheckBoxColumn colSelected;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCharge;
        private DataGridViewComboBoxColumn colType;
        private RadioButton rdAudioFeedback_PlayInNH;
        private RadioButton rdAudioFeedback_PlayInPc;
        private CheckBox chkShowTrail;
        private RichTextBox txtDebugColor;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CheckBox chkVerbose;
        private Label label15;
        private CheckBox chkEnableBluetoothMonitoring;
        private Label label37;
    }
}