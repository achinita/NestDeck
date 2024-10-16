namespace Nest_Deck
{
    partial class KeyCap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyCap));
            label1 = new Label();
            lblKeys = new Label();
            btOK = new Button();
            pictureBox1 = new PictureBox();
            progressBarClose = new ProgressBar();
            lblAutoClose = new Label();
            timerShutdown = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 42);
            label1.Name = "label1";
            label1.Size = new Size(192, 20);
            label1.TabIndex = 0;
            label1.Text = "Press any key(s) to capture...";
            // 
            // lblKeys
            // 
            lblKeys.AutoSize = true;
            lblKeys.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblKeys.Location = new Point(16, 62);
            lblKeys.Name = "lblKeys";
            lblKeys.Size = new Size(270, 41);
            lblKeys.TabIndex = 1;
            lblKeys.Text = "Shift, Control + N";
            // 
            // btOK
            // 
            btOK.Enabled = false;
            btOK.Location = new Point(246, 117);
            btOK.Name = "btOK";
            btOK.Size = new Size(94, 29);
            btOK.TabIndex = 2;
            btOK.Text = "Capture";
            btOK.UseVisualStyleBackColor = true;
            btOK.Click += btOK_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.NestDeck;
            pictureBox1.Location = new Point(12, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(196, 29);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // progressBarClose
            // 
            progressBarClose.Location = new Point(20, 119);
            progressBarClose.Name = "progressBarClose";
            progressBarClose.Size = new Size(220, 12);
            progressBarClose.TabIndex = 4;
            progressBarClose.Value = 100;
            progressBarClose.Visible = false;
            // 
            // lblAutoClose
            // 
            lblAutoClose.AutoSize = true;
            lblAutoClose.Font = new Font("Segoe UI", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            lblAutoClose.Location = new Point(24, 132);
            lblAutoClose.Name = "lblAutoClose";
            lblAutoClose.Size = new Size(212, 17);
            lblAutoClose.TabIndex = 5;
            lblAutoClose.Text = "This window will close in 5 seconds";
            lblAutoClose.Visible = false;
            // 
            // timerShutdown
            // 
            timerShutdown.Interval = 1000;
            timerShutdown.Tick += timerShutdown_Tick;
            // 
            // KeyCap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 158);
            Controls.Add(progressBarClose);
            Controls.Add(pictureBox1);
            Controls.Add(btOK);
            Controls.Add(lblKeys);
            Controls.Add(label1);
            Controls.Add(lblAutoClose);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KeyCap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Key Capture";
            TopMost = true;
            Activated += KeyCap_Activated;
            Deactivate += KeyCap_Deactivate;
            Load += KeyCap_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblKeys;
        private Button btOK;
        private PictureBox pictureBox1;
        private ProgressBar progressBarClose;
        private Label lblAutoClose;
        private System.Windows.Forms.Timer timerShutdown;
    }
}