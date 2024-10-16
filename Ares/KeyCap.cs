using SHARK_Deck;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SHARK_Deck.KeyboardMonitor;

using WindowsInput.Native;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Nest_Deck
{
    public partial class KeyCap : Form
    {
        public KeyCap()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(34, 34, 34);
            label1.ForeColor = Color.WhiteSmoke;
            lblKeys.ForeColor = Color.WhiteSmoke;
            lblAutoClose.ForeColor = Color.WhiteSmoke;
            PressedKeys = null;
        }

        private void KeyCap_Load(object sender, EventArgs e)
        {
            lblKeys.Text = "";
        }
        public void SetKeys(KeyEventArgs e)
        {
            if (e.KeyData != Keys.None)
            {
                PressedKeys = e;

                string modString = string.Empty;
                if (e.Modifiers != Keys.None) modString = e.Modifiers.ToString();

                lblKeys.Text = (modString != string.Empty ? modString + " + " : string.Empty) + e.KeyCode.ToString();

                btOK.Enabled = true;
            }
        }
        public List<VirtualKeyCode> Modifiers = new List<VirtualKeyCode>();
        public List<VirtualKeyCode> Key = new List<VirtualKeyCode>();
        private void btOK_Click(object sender, EventArgs e)
        {
            Modifiers.Clear();
            if (PressedKeys.Modifiers != null)
            {
                if (PressedKeys.Modifiers.HasFlag(Keys.Shift)) Modifiers.Add(VirtualKeyCode.SHIFT);
                if (PressedKeys.Modifiers.HasFlag(Keys.LShiftKey)) Modifiers.Add(VirtualKeyCode.LSHIFT);
                if (PressedKeys.Modifiers.HasFlag(Keys.RShiftKey)) Modifiers.Add(VirtualKeyCode.RSHIFT);

                if (PressedKeys.Modifiers.HasFlag(Keys.Control)) Modifiers.Add(VirtualKeyCode.CONTROL);
                if (PressedKeys.Modifiers.HasFlag(Keys.LControlKey)) Modifiers.Add(VirtualKeyCode.LCONTROL);
                if (PressedKeys.Modifiers.HasFlag(Keys.RControlKey)) Modifiers.Add(VirtualKeyCode.RCONTROL);

                if (PressedKeys.Modifiers.HasFlag(Keys.Alt)) Modifiers.Add(VirtualKeyCode.MENU);
                if (PressedKeys.Modifiers.HasFlag(Keys.Menu)) Modifiers.Add(VirtualKeyCode.MENU);
            }

            Key.Clear();
            var kc = ((int)PressedKeys.KeyCode);
            Key.Add((VirtualKeyCode)kc);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        int countDown = 0;
        private void KeyCap_Deactivate(object sender, EventArgs e)
        {
            countDown = 6;

            progressBarClose.Maximum = countDown;
            timerShutdown.Enabled = true;

            progressBarClose.Value = countDown;
            progressBarClose.Visible = true;
            lblAutoClose.Text = "This window will close in " + countDown + " seconds";
            lblAutoClose.Visible = true;
        }

        private void timerShutdown_Tick(object sender, EventArgs e)
        {
            countDown--;

            lblAutoClose.Text = "This window will close in " + countDown + " seconds";
            if (countDown >= 0) progressBarClose.Value = countDown;
            else this.Close();
        }

        private void KeyCap_Activated(object sender, EventArgs e)
        {
            timerShutdown.Enabled = false;
            progressBarClose.Visible = false;
            lblAutoClose.Visible = false;
        }

        public KeyEventArgs PressedKeys { get; set; }
    }
}
