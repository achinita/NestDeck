using GoogleCast;
using GoogleCast.Models.Receiver;
using Microsoft.Win32;
using Newtonsoft.Json;
using SHARK_Deck;
using Nest_Deck;
//using static System.Net.WebRequestMethods;

using Nest_Deck.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;
using WindowsInput;
using System.Xml;
using System.Xml.Linq;
using System.DirectoryServices.ActiveDirectory;

namespace Ares
{
    public partial class SHARK : Form
    {
        private const string _urlEditor =           "https://achinita.outsystemscloud.com/SHARK2/Config";
        //private const string sseStreamUrl =         "https://achinita.outsystemscloud.com/SHARK_IS/rest/SSE/Subscribe";
        private const string _urlRegisterDevice =   "https://achinita.outsystemscloud.com/SHARK2/rest/SynchronizeDevice/RegisterDevice?DeviceId=";
        private const string _urlSendData =         "https://achinita.outsystemscloud.com/SHARK2/rest/SynchronizeDevice/SendData?DeviceId=";

        private Options opts = new Options();
        private bool autoSaveSettings = false;
        public SHARK()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
        KeyboardMonitor monitor = new KeyboardMonitor();

        private async void ARES_Load(object sender, EventArgs e)
        {
            debug("Loading...");
            toolStripMenuItem_AutoSwitch.Click += ToolStripMenuItem_AutoSwitch_Click;
            opts.Load();
            LoadOptionsUI();

            autoSaveSettings = true;

            if (opts.StartMinimized)
            {
                btMinimize_Click(sender, e); //Minimize on start
            }

            Updater.CheckVersion(); //Version check from web service
            await GetCastDevices(); //List cast devices

            InitializeCastResources();
            debug("NestDeck loaded.");
            backgroundWorker1.RunWorkerAsync(); //HTTP Server start
            if (opts.AutoCast && opts.DeviceId != "" && lstCastDevices.SelectedIndices.Count > 0) { btCast_Click(sender, new EventArgs()); } //Auto cast on start
        }
        async private Task GetCastDevices()
        {
            debug("Looking for cast devices...");
            lstCastDevices.Items.Clear();
            var receivers = await new DeviceLocator().FindReceiversAsync();

            foreach (var receiver in receivers)
            {
                var itm = new System.Windows.Forms.ListViewItem(receiver.FriendlyName);
                itm.SubItems.Add(receiver.Model);
                itm.Tag = receiver;
                itm.Selected = (receiver.Id == opts.DeviceId);
                lstCastDevices.Items.Add(itm);
            }
            lstCastDevices.Focus();
            debug(lstCastDevices.Items.Count + " devices found.");
        }
        //******************************** MODS
        public class Action
        {
            public string ActionType { get; set; }
            public string Data { get; set; }
            public string ExtraData { get; set; }
        }



        private static string handleAction(Action a)
        {
            string errMsg = "";
            switch (a.ActionType)
            {
                case "ActivateProcess":
                    {
                        activateProcess(a.Data);
                        break;
                    }
                case "SendKeystroke":
                    {
                        InputSimulator inp = new InputSimulator();
                        List<WindowsInput.Native.VirtualKeyCode> modifiers = new List<WindowsInput.Native.VirtualKeyCode>();
                        List<WindowsInput.Native.VirtualKeyCode> keys = new List<WindowsInput.Native.VirtualKeyCode>();

                        if (a.ExtraData != null) foreach (string k in a.ExtraData.Split(',')) modifiers.Add((WindowsInput.Native.VirtualKeyCode)int.Parse(k));
                        if (a.Data != null) foreach (string k in a.Data.Split(',')) keys.Add((WindowsInput.Native.VirtualKeyCode)int.Parse(k));

                        inp.Keyboard.ModifiedKeyStroke(modifiers, keys);
                        break;
                    }
                case "Run":
                    {
                        try
                        {
                            string runPath = DecodeJS(a.Data);
                            string parameters = DecodeJS(a.ExtraData);

                            ProcessStartInfo pi = new ProcessStartInfo();
                            pi.UseShellExecute = true;
                            pi.FileName = runPath;
                            pi.Arguments = parameters;

                            Process.Start(pi);

                        }
                        catch (Exception ex)
                        {
                            errMsg = "There was an error running file or process. (" + ex.Message + ")";
                        }
                        break;
                    }
                case "Wait":
                    {
                        Thread.Sleep(int.Parse(a.Data));
                        break;
                    }
                case "Deck":
                    {
                        break;
                    }
            }
            return errMsg;
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = (string)e.UserState;
            if (msg.IndexOf("auth") == 0)
            {
                string authString = msg.Substring(4, msg.Length - 4);
                opts.Auth = authString;
                opts.Save();
                setAuthColor();
                var debugMessage = "NestDeck has sucessfully paired with the editor.";
                debug(debugMessage);
                notifyIcon.ShowBalloonTip(10, "Pair success", debugMessage, ToolTipIcon.None);
                SSEMessage(authString, "synchOkAuth");
                editorEvents.Disconnect();
                editorEvents.Connect(opts.Auth);
            }
            if (msg == "lookForFile")
            {
                var f = new System.Windows.Forms.OpenFileDialog();
                var result = f.ShowDialog();

                if (result == DialogResult.OK) SSEMessage(opts.Auth, "file " + f.FileName);
                else SSEMessage(opts.Auth, "file");
            }
            if (msg == "lookForProcess")
            {
                ProcessFinder pf = new ProcessFinder();
                var result = pf.ShowDialog();
                if (result == DialogResult.OK) SSEMessage(opts.Auth, "process " + pf.ProcessName);
                else SSEMessage(opts.Auth, "process");
            }
            if (msg.IndexOf("forceDisconnect") == 0)
            {
                btCast_Click(sender, new EventArgs());
            }
            //else debug(msg);
        }

        private string _activeProcess = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            string currentActiveProcess = GetActiveProcess();
            if (_activeProcess != currentActiveProcess && !monitor.IsAltPressed)
            {
                if (_activeProcess != "") SendData("activeProcess|" + currentActiveProcess);
                _activeProcess = currentActiveProcess;

            }
        }


        #region Cast

        Sender chromecastSender = new Sender();
        ReceiverStatus receiverStatus = new ReceiverStatus();
        bool cast_connected = false;
        IReceiver receiver = null;


        private void InitializeCastResources()
        {
            bool cast_connected = false;
            events.OnMessage += Events_OnMessage;
            events.OnStatusChange += Events_OnStatusChange;
            chromecastSender.Disconnected += ChromecastSender_Disconnected;


            editorEvents.OnMessage += Events_OnMessage;
            editorEvents.OnStatusChange += Events_OnStatusChange;

            if (opts.Auth != "") editorEvents.Connect(opts.Auth);
        }

        private void Events_OnStatusChange(object sender, SSE.SSEConnectionStatus eventArgs)
        {
            switch (eventArgs.Status)
            {
                case EventSource4Net.EventSourceState.CLOSED:
                    {
                        statusData.Image = Resources.icons8_no_network_16;
                        debug("Data connection closed.");
                        break;
                    }
                case EventSource4Net.EventSourceState.OPEN:
                    {
                        statusData.Image = Resources.icons8_connection_status_on_16;
                        break;
                    }
                default: break;
            }
        }

        Dispatcher UserDispatcher = Dispatcher.CurrentDispatcher;
        private void LogMessage(string msg)
        {
            debugmt del = new debugmt(debug);
            UserDispatcher.Invoke(del, msg, false);
        }
        [STAThread]
        private void Events_OnMessage(object sender, SSE.SSEArguments eventArgs)
        {
            
            eventArgs.Message = eventArgs.Message.ReplaceLineEndings("");
            var command = eventArgs.Message;
            string[] args = eventArgs.Message.Split(' ');
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case SSE.Messages.AuthenticationRequest:
                        {
                            LogMessage("Application initialized. Pairing...");

                            HttpClient client = new HttpClient();
                            client.GetAsync(_urlRegisterDevice + opts.DeviceId + "&AuthData=" + opts.Auth);
                            break;
                        }
                    case SSE.Messages.Ping:
                        {
                            string json = "{'Model':'"+receiver.Model+"','Capabilities':'"+receiver.Capabilities+"'}";
                            SendData(SSE.Messages.Pong + json);
                            break;
                        }
                    case SSE.Messages.ConnectionSuccess:
                        {
                            LogMessage("Application paired. Ready.");
                            break;
                        }
                    case SSE.Messages.Timeout:
                        {
                            LogMessage("Chromecast application lost connection to Windows. (Timeout)");
                            if (cast_connected) backgroundWorker1.ReportProgress(1, "forceDisconnect");
                            break;
                        }
                    default:
                        {
                            //UserDispatcher.Invoke(del, eventArgs.Message);
                            break;
                        }
                }
            }

            switch (command)
            {
                case "reqAudio":
                    {
                        string json = JsonConvert.SerializeObject(VolumeMixer.GetAudioProcesses());
                        LogMessage("Audio levels check");
                        SendData("audioData|" + json);
                        break;
                    }
                case "lookForFile":
                    {
                        backgroundWorker1.ReportProgress(1, "lookForFile");
                        break;
                    }
                case "lookForProcess":
                    {
                        backgroundWorker1.ReportProgress(1, "lookForProcess");
                        break;
                    }
                default:
                    {
                        if (command.IndexOf("setVol|") == 0)
                        {
                            command = command.Replace("setVol|", "");
                            VolumeMixer.AudioProcess proc = JsonConvert.DeserializeObject<VolumeMixer.AudioProcess>(command);

                            LogMessage("Setting volume of " + proc.Name + " to " + proc.Volume + "%");

                            if (proc.PId == 0) VolumeMixer.setGeneralVolume(proc.Volume);
                            else VolumeMixer.SetApplicationVolume(proc.PId, proc.Volume);
                        }
                        else
                        {
                            try
                            {
                                if (command.IndexOf("[tapDeckButton]") == 0)
                                {
                                    command = command.Replace("[tapDeckButton]", "");
                                    List<Action> actions = JsonConvert.DeserializeObject<List<Action>>(command);
                                    LogMessage("Button pressed. Running " + actions.Count + " actions");
                                    foreach (Action a in actions)
                                    {
                                        string err = handleAction(a);
                                        if (err != "") LogMessage(err);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogMessage("Error handling request. " + ex.Message);
                            }
                        }
                        break;
                    }
            }

        }

        private void SSEMessage(string channel, string data)
        {
            WebClient wc = new WebClient();
            string url = _urlSendData + channel;

            wc.Headers[HttpRequestHeader.ContentType] = "text/plain; charset=utf-8";
            var x = wc.UploadString(url, data);
        }
        private void SendData(string data)
        {
            SSEMessage(opts.DeviceId, data);
        }
        private SSE events = new SSE();
        private SSE editorEvents = new SSE();
        

        private async void btCast_Click(object sender, EventArgs e)
        {
            if (opts.Auth == "")
            {
                var response = MessageBox.Show(
                    "This application has never been paired with the web client." + Environment.NewLine +
                    "Please login in the web client." + Environment.NewLine + Environment.NewLine +
                    "Do you wish to open the client in a new browser?",

                    "Not paired with web client", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (response == DialogResult.Yes) toolstripOpenDeckEditor_Click(sender, e);

                return;
            }
            btCast.Enabled = false;
            if (!cast_connected)
            {
                try
                {

                    debug("Connecting to server...");

                    receiver = (IReceiver)lstCastDevices.SelectedItems[0].Tag;
                    opts.DeviceId = receiver.Id;
                    opts.Save();
                    //debug("Initializing connection...");
                    //Connect data connection
                    await events.Connect(receiver.Id);

                    SendData(SSE.Messages.TerminationSignal); //Close any running instances

                    Thread.Sleep(1000); //Wait 1 Second

                    // Connect to the Chromecast
                    chromecastSender.Connect(receiver);

                    //var mediaChannel = chromecastSender.GetChannel<IApplicationChannel>();


                    //1A79419F New
                    //8A47528F Old
                    receiverStatus = await chromecastSender.LaunchAsync("1A79419F");


                    debug("Chromecast initialized.");
                    cast_connected = true;
                    //chromecastSender.SendAsync("", new GoogleCast.Messages.Message(), "receiver");
                    statusCast.Image = Resources.icons8_connection_status_on_16;
                    btCast.Text = "Disconnect";

                }
                catch (Exception ex)
                {
                    SendData(SSE.Messages.TerminationSignal);
                    statusCast.Image = Resources.icons8_no_network_16;
                    cast_connected = false;
                    btCast.Text = "Cast";
                    debug(ex.Message);
                }
            }
            else
            {
                receiver = null;
                cast_connected = false;
                debug("Disconnecting...");
                chromecastSender.Disconnect();
                events.Disconnect();
                statusCast.Image = Resources.icons8_no_network_16;
                btCast.Text = "Cast";

                SendData(SSE.Messages.TerminationSignal);
            }
            btCast.Enabled = true;
        }

        private void ChromecastSender_Disconnected(object? sender, EventArgs e)
        {
            receiver = null;
            cast_connected = false;
            statusCast.Image = Resources.icons8_no_network_16;
            btCast.Text = "Cast";
            debug("Chromecast connection closed");
        }
        #endregion


        /**********************************************************************************************************/
        #region Options
        private void toolstripOpenDeckEditor_Click(object sender, EventArgs e)
        {
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.UseShellExecute = true;
            pi.FileName = _urlEditor;

            Process.Start(pi);
        }

        private void optWindowsStart_Click(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (optWindowsStart.Checked)
                rk.SetValue("SharkDeck", System.Windows.Forms.Application.ExecutablePath);
            else
                rk.DeleteValue("SharkDeck", false);

            SaveSettings(sender, e);
        }

        private void optAutoSwitchDeck_Click(object sender, EventArgs e)
        {
            timerMonitorProcess.Enabled = optAutoSwitchDeck.Checked;
            toolStripMenuItem_AutoSwitch.Checked = optAutoSwitchDeck.Checked;
            toolStripMenuItem_AutoSwitch.Checked = optAutoSwitchDeck.Checked;
            SaveSettings(sender, e);
        }
        private void SaveSettings(object sender, EventArgs e)
        {
            if (autoSaveSettings)
            {
                opts.StartWithWindows = optWindowsStart.Checked;
                opts.StartMinimized = optSystray.Checked;
                opts.MonitorProcess = optAutoSwitchDeck.Checked;
                opts.AutoCast = optAutoCast.Checked;

                opts.Save();
            }
        }
        #endregion
        #region Utils
        static int HexToInt(char hexChar)
        {
            hexChar = char.ToUpper(hexChar);

            return (int)hexChar < (int)'A' ?
                ((int)hexChar - (int)'0') :
                10 + ((int)hexChar - (int)'A');
        }

        public static string DecodeJS(string s)
        {
            if (s == null) s = "";
            s = s.Replace("\\x", "&#x");
            var result = new StringBuilder();



            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if (ch == '&')
                {
                    ch = Convert.ToChar(HexToInt(s[i + 3]) * 16 +
                                         HexToInt(s[i + 4]));
                    i += 4;
                }
                result.Append(ch);
            }
            return result.ToString();
        }

        private static void activateProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                Process p = new Process();
                foreach (Process proc in processes) p = proc;

                BringWindowToFront(p);
            }
        }
        #endregion
        #region Imports
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private static string GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        private static void BringWindowToFront(Process p)
        {
            const int SW_HIDE = 0;
            const int SW_SHOWNORMAL = 1;
            const int SW_SHOWMINIMIZED = 2;
            const int SW_SHOWMAXIMIZED = 3;
            const int SW_SHOWNOACTIVATE = 4;
            const int SW_RESTORE = 9;
            const int SW_SHOWDEFAULT = 10;

            IntPtr hWnd = p.MainWindowHandle;
            // if iconic, we need to restore the window
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, SW_RESTORE);
                Thread.Sleep(200);
            }
            // bring it to the foreground
            SetForegroundWindow(hWnd);

        }
        #endregion
        #region UI
        private void ToolStripMenuItem_AutoSwitch_Click(object? sender, EventArgs e)
        {
            optAutoSwitchDeck.Checked = toolStripMenuItem_AutoSwitch.Checked;
            optAutoSwitchDeck_Click(sender, e);
        }
        private void resetAllOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dResult = MessageBox.Show("Reseting will revert all options to their default values." + Environment.NewLine +
                "It will also clear any authentication data, meaning you will have to pair with the deck editor again." + Environment.NewLine + Environment.NewLine +
                "Are you sure you want to reset?", "Confirm reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dResult == DialogResult.Yes)
            {
                opts = new Options();
                opts.Save();

                LoadOptionsUI();
                debug("Default options set.");
            }
        }
        private void LoadOptionsUI()
        {
            pictureLogo.BackColor = Color.Transparent;
            txtDebug.BackColor = Color.FromArgb(34, 34, 34);
            pbBackground.BackColor = txtDebug.BackColor;
            pictureLogo.BackColor = txtDebug.BackColor;
            lstCastDevices.TileSize = new Size(lstCastDevices.Size.Width, 24);

            optWindowsStart.Checked = opts.StartWithWindows;
            optSystray.Checked = opts.StartMinimized;
            optAutoSwitchDeck.Checked = opts.MonitorProcess;
            toolStripMenuItem_AutoSwitch.Checked = opts.MonitorProcess;
            optAutoCast.Checked = opts.AutoCast;

            timerMonitorProcess.Enabled = optAutoSwitchDeck.Checked;

            setAuthColor();
        }
        private void SHARK_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(2, "NestDeck", "NestDeck is running in the background.", ToolTipIcon.None);
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        //public delegate void debugmt(string line);
        public delegate void debugmt(string line, bool hideTimestamp = false);
        private void debug(string line, bool hideTimestamp = false)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            txtDebug.Text += (hideTimestamp ? "" : "[" + timestamp + "] ") + line + Environment.NewLine;
            txtDebug.SelectionStart = txtDebug.Text.Length;
            txtDebug.ScrollToCaret();


        }
        private void setAuthColor()
        {
            if (opts.Auth != "" && opts.Auth != null) statusAuth.Image = Resources.icons8_connection_status_on_16;
            else statusAuth.Image = Resources.icons8_no_network_16;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("You won't be able to interact with your computer through NestDeck anymore. Are you sure you want to exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) e.Cancel = true;
            }
            if (e.Cancel == false && cast_connected) SendData(SSE.Messages.TerminationSignal); //Close any running instances
        }
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit NestDeck?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) System.Windows.Forms.Application.Exit();
        }
        private void lstCastDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btCast.Enabled = (lstCastDevices.SelectedItems.Count > 0);
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
        }
        #endregion
        #region HTTP Server
        public static HttpListener listener;
        public static string url = "http://localhost:{port}/";
        public static int pageViews = 0;
        public static int requestCount = 0;


        public static async Task HandleIncomingConnections(BackgroundWorker bgw, string mainUrl)
        {
            bool runServer = true;
            bgw.ReportProgress(1, "Server started.");
            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;



                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);

                string url = req.Url.ToString();

                Console.WriteLine(url);
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                if (req.HttpMethod == "GET")
                {
                    if (req.HttpMethod == "GET")
                    {
                        var command = url.Replace(mainUrl, "");
                        if (command.IndexOf("auth") == 0)
                        {
                            bgw.ReportProgress(1, command);
                            bgw.ReportProgress(1, "Authentication received");
                        }
                    }
                }
                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";

                byte[] data = File.ReadAllBytes("Resources/server.htm");
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
        private void btRestart_Click(object sender, EventArgs e)
        {
            debug("Stopping server...");
            WebClient c = new WebClient();
            c.UploadString(url.Replace("{port}", runningPort.ToString()) + "shutdown", "");
            c.Dispose();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            debug("Server stopped.");
            backgroundWorker1.RunWorkerAsync();
        }
        decimal runningPort = 0;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            runningPort = numPort.Value;
            string effectiveUrl = url.Replace("{port}", numPort.Value.ToString());
            listener.Prefixes.Add(effectiveUrl);


            backgroundWorker1.ReportProgress(1, "Starting server on port " + numPort.Value + "...");
            listener.Start();
            // Handle requests
            Task listenTask = HandleIncomingConnections(backgroundWorker1, effectiveUrl);
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
        #endregion
        /********************************************************************************************************/
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuTray_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolClearLog_Click(object sender, EventArgs e)
        {
            txtDebug.Text = "";
            /** ProcessFinder pf = new ProcessFinder();
            pf.ShowDialog();*/
        }

        private void toolRefreshDevices_Click(object sender, EventArgs e)
        {
            GetCastDevices();
        }
    }
}
