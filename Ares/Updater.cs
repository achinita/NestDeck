using Newtonsoft.Json;
using System.Diagnostics;

namespace Nest_Deck
{
    public partial class Updater : Form
    {
        public const string currentVersion = "3.0";
        public static string GetCurrentVersion()
        {
            return currentVersion;
        }
        private bool downloadComplete = false;
        private class VersionInfo
        {
            public string Version = "";
            public string URL = "";
            public string Features = "";
            public VersionInfo() { }
        }
        public Updater()
        {
            InitializeComponent();
        }
        public string DownloadUrl { get; private set; }
        public string DownloadPath { get; private set; }
        private void Updater_Load(object sender, EventArgs e)
        {
            DownloadPath = Path.GetFileName(DownloadUrl);

            var client = new HttpClientDownloadWithProgress(DownloadUrl, DownloadPath);

            client.ProgressChanged += Client_ProgressChanged;
            client.StartDownload();
        }

        private void Client_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage)
        {
            progressBar.Value = (int)progressPercentage;

            if (progressPercentage == 100)
            {
                downloadComplete = true;
                this.Close();

            }
        }

        public class HttpClientDownloadWithProgress : IDisposable
        {
            private readonly string _downloadUrl;
            private readonly string _destinationFilePath;

            private HttpClient _httpClient;

            public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

            public event ProgressChangedHandler ProgressChanged;

            public HttpClientDownloadWithProgress(string downloadUrl, string destinationFilePath)
            {
                _downloadUrl = downloadUrl;
                _destinationFilePath = destinationFilePath;
            }

            public async Task StartDownload()
            {
                _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

                using (var response = await _httpClient.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                    await DownloadFileFromHttpResponseMessage(response);
            }

            private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                    await ProcessContentStream(totalBytes, contentStream);
            }

            private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
            {
                var totalBytesRead = 0L;
                var readCount = 0L;
                var buffer = new byte[8192];
                var isMoreToRead = true;

                using (var fileStream = new FileStream(_destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    do
                    {
                        var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            isMoreToRead = false;
                            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                            continue;
                        }

                        await fileStream.WriteAsync(buffer, 0, bytesRead);

                        totalBytesRead += bytesRead;
                        readCount += 1;

                        if (readCount % 100 == 0)
                            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                    }
                    while (isMoreToRead);
                }
            }

            private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
            {
                if (ProgressChanged == null)
                    return;

                double? progressPercentage = null;
                if (totalDownloadSize.HasValue)
                    progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

                ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
            }

            public void Dispose()
            {
                _httpClient?.Dispose();
            }
        }
        private const string _checkVersionWs = "https://achinita.outsystemscloud.com/NestDeckAPI/rest/VersionInfo/Latest";
        public static string CheckVersion()
        {
            string version = "";
            try
            {
                HttpClient client = new HttpClient();
                var task = client.GetStringAsync(_checkVersionWs);
                task.Wait();

                VersionInfo info = new VersionInfo();
                info = JsonConvert.DeserializeObject<VersionInfo>(task.Result);

                version = info.Version;
                //New version available? 
                if (currentVersion != info.Version)
                {
                    var answer = MessageBox.Show("There's a new version of NestDeck available." + Environment.NewLine +
                        Environment.NewLine +
                        "Would you like to download and install it now?", "NestDeck - New version", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        Updater frmDownload = new Updater();
                        frmDownload.DownloadUrl = info.URL;
                        frmDownload.ShowDialog();
                    }

                }

            }
            catch { }
            return version;
        }

        private void Updater_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (downloadComplete)
            {
                this.DialogResult = DialogResult.OK;
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.UseShellExecute = true;
                info.FileName = DownloadPath;

                p.StartInfo = info;

                p.Start();

                Application.Exit();
            }
        }
    }
}
