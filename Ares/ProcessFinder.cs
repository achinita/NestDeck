using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace SHARK_Deck
{
    public partial class ProcessFinder : Form
    {
        public ProcessFinder()
        {
            InitializeComponent();
        }
        public string ProcessName = "";
        private void ProcessFinder_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(34, 34, 34);
            label1.ForeColor = Color.WhiteSmoke;
            this.ActiveControl = textBox1;
            processList();
        }

        List<string> addedProcs = new List<string>();
        private void processList(string search = "")
        {
            addedProcs.Clear();
            lstProcesses.Items.Clear();

            foreach (Process p in Process.GetProcesses())
            {
                if (!addedProcs.Contains(p.ProcessName)) //Avoid repeats
                {
                    if (search != "" && search.Length >= 3)
                    {
                        if (p.ProcessName.ToLower().Contains(search.ToLower()) || p.MainWindowTitle.ToLower().Contains(search.ToLower())) lstProcesses.Items.Add(ProcessItem(p));
                    }
                    else lstProcesses.Items.Add(ProcessItem(p));
                    addedProcs.Add(p.ProcessName);
                }
            }
        }
        private ListViewItem ProcessItem(Process process)
        {
            ListViewItem ProcessItem = new ListViewItem(process.ProcessName);
            ProcessItem.SubItems.Add(process.MainWindowTitle);
            return ProcessItem;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            processList(textBox1.Text);
        }

        private void lstProcesses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = lstProcesses.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                ProcessName = item.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.lstProcesses.SelectedItems.Clear();
            }
        }
    }
}
