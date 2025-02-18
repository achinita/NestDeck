using System.Diagnostics;

namespace Nest_Deck
{
    public partial class About : Form
    {

        private const string url_nestDeck = "https://nestdeck.base64.pt/";
        private const string url_Discord = "https://discord.gg/97K2dwh78f";
        private const string url_Youtube = "https://www.youtube.com/@ProjectH4x";

        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(34, 34, 34);
            this.ForeColor = Color.WhiteSmoke;
            lstThanks.BackColor = this.BackColor;
            lstThanks.ForeColor = this.ForeColor;

            lblVersion.Text = "v" + Updater.GetCurrentVersion();
        }

        private void OpenURL(string url)
        {
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.UseShellExecute = true;
            pi.FileName = url;

            Process.Start(pi);
        }

        private void lnkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURL(url_Discord);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURL(url_nestDeck);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURL(url_Youtube);
        }
    }
}
