namespace SHARK_Deck
{
    partial class ProcessFinder
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
            lstProcesses = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lstProcesses
            // 
            lstProcesses.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            lstProcesses.FullRowSelect = true;
            lstProcesses.Location = new Point(12, 92);
            lstProcesses.Name = "lstProcesses";
            lstProcesses.Size = new Size(776, 441);
            lstProcesses.TabIndex = 0;
            lstProcesses.UseCompatibleStateImageBehavior = false;
            lstProcesses.View = View.Details;
            lstProcesses.MouseDoubleClick += lstProcesses_MouseDoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Process";
            columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Title";
            columnHeader2.Width = 500;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Nest_Deck.Properties.Resources.NestDeck;
            pictureBox1.Location = new Point(9, 17);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(196, 29);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 59);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(776, 27);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(556, 33);
            label1.Name = "label1";
            label1.Size = new Size(232, 20);
            label1.TabIndex = 3;
            label1.Text = "Double-click a process to select it";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // ProcessFinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 545);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(lstProcesses);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProcessFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select a process";
            TopMost = true;
            Load += ProcessFinder_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lstProcesses;
        private PictureBox pictureBox1;
        private ColumnHeader columnHeader1;
        private TextBox textBox1;
        private ColumnHeader columnHeader2;
        private Label label1;
    }
}