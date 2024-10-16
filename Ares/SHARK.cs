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
using System.Windows.Interop;
using WebSocketSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using GoogleCast.Channels;
using Microsoft.Extensions.DependencyInjection;

using NAudio.Wave;
using static SHARK_Deck.VolumeMixer;
using System.Threading;
using NAudio.CoreAudioApi;
using System.Windows.Controls;
using static Nest_Deck.Bluetooth;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System;

namespace Ares
{
    public partial class SHARK : Form
    {
        const string appName = "Nest Deck";
        private const string _urlEditor = "https://achinita.outsystemscloud.com/SHARK2/Config";
        //private const string sseStreamUrl =         "https://achinita.outsystemscloud.com/SHARK_IS/rest/SSE/Subscribe";
        private const string _urlRegisterDevice = "https://achinita.outsystemscloud.com/SHARK2/rest/SynchronizeDevice/RegisterDevice?DeviceId=";
        private const string _urlSendData = "https://achinita.outsystemscloud.com/SHARK2/rest/SynchronizeDevice/SendData?DeviceId=";

        private Options opts = new Options();
        private OBS OBS = new OBS();
        private bool autoSaveSettings = false;
        private bool obsIsRunning = false;

        private KeyboardMonitor keyboardMonitor = new KeyboardMonitor();
        private VolumeMixer volumeMixer = new VolumeMixer();
        private MediaPlayer mediaPlayer = new MediaPlayer();

        private SSE events = new SSE();
        private SSE editorEvents = new SSE();

        List<Bluetooth.BtDevice> btDevices = new List<Bluetooth.BtDevice>();

        public SHARK()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        }
        KeyCap keycap = new KeyCap();

        private async void ARES_Load(object sender, EventArgs e)
        {
            debug("[Reminder] You can use gestures on your Nest Deck.");
            debug(" - Swipe up to open the volume mixer");
            debug(" - Swipe down to open the deck selector");
            debug(" - Swipe left to navigate to the next deck");
            debug(" - Swipe right to navigate to the previous deck");
            debug(" Don't swipe too close to the edges of the screen or");
            debug(" you'll trigger the Nesthub's native swipe.");
            debug(" Start swiping closer to the center of the screen :)");
            debug("");

            debug("Loading...");

            //hw.GetBlutoothInfoAsync();
            Utils.KillOtherInstances(appName);

            lnkAbout.Text = Updater.GetCurrentVersion();

            opts.Load();
            LoadOptionsUI();

            autoSaveSettings = true;

            if (opts.StartMinimized)
            {
                btMinimize_Click(sender, e); //Minimize on start
            }

            timerMonitorHardware_Tick(sender, e); //Monitor hardware

            await GetCastDevices(); //List cast devices

            InitializeCastResources();
            backgroundWorker1.RunWorkerAsync(); //HTTP Server start

            OBS.WebSocket.Connected += WebSocket_Connected;
            OBS.WebSocket.Disconnected += WebSocket_Disconnected;
            OBS.UpdatedData += OBS_UpdatedData;
            OBS.UpdatedRecordingStatus += OBS_UpdatedRecordingStatus;
            OBS.UpdatedStreamingStatus += OBS_UpdatedStreamingStatus;
            mediaPlayer.SongChanged += MediaPlayer_SongChanged;
            hw.VPNChanged += Hw_VPNChanged;

            debug("NestDeck loaded.");
            timerMonitorProcess.Enabled = true;

            Updater.CheckVersion(); //Version check from web service
            if (opts.AutoCast && opts.DeviceId != "" && cmbDeviceList.SelectedIndex >= 0) { btCast_Click(sender, new EventArgs()); } //Auto cast on start


            //mediaPlayer.Play("holygrenade.mp3");
        }

        //Need to restore connections on vpn status changed
        private async void Hw_VPNChanged(object? sender, EventArgs e)
        {
            LogMessage("[VPN Status] " + (hw.HasVPNConnection ? "Connected" : "Disconnected"));

            if (cast_connected) await events.Connect(opts.DeviceId);
            if (opts.Auth != null && opts.Auth != "") await editorEvents.Connect(opts.Auth);
            SendSystemInfo();

        }

        private void MediaPlayer_SongChanged(object? sender, EventArgs e)
        {
            //LogMessage(mediaPlayer.MediaName  + " - " + (mediaPlayer.IsPlaying?"Playing":"Paused"));
            SendSystemInfo();
        }

        private void OBS_UpdatedStreamingStatus(object? sender, EventArgs e)
        {
            if (OBS.isRecording)
            {
                LogMessage("[OBS] Streaming started");
            }
            else
            {
                LogMessage("[OBS] Streaming stopped");
            }
            if (cast_connected)
            {
                SendData(SSE.MessageEnvelope.Build(SSE.Messages.OBSStatus, OBS.Serialize()));
            }
            OBSUIChange();
        }

        private void OBS_UpdatedRecordingStatus(object? sender, EventArgs e)
        {
            if (OBS.isRecording)
            {
                LogMessage("[OBS] Recording started");
            }
            else
            {
                LogMessage("[OBS] Recording stopped");
            }
            if (cast_connected)
            {
                SendData(SSE.MessageEnvelope.Build(SSE.Messages.OBSStatus, OBS.Serialize()));
            }
            OBSUIChange();
        }

        private void Monitor_KeyPress(object? sender, EventArgs e)
        {
            if (keycap.Visible)
            {
                KeyEventArgs keys = (KeyEventArgs)e;
                keycap.SetKeys(keys);
            }
        }

        private void OBS_UpdatedData(object? sender, EventArgs e)
        {
            var obsInfo = OBS.Serialize();
            SSEMessage(opts.Auth, SSE.Messages.OBSData + " " + obsInfo); //Send data to Deck Editor
        }

        private void WebSocket_Disconnected(object? sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            OBSUIChange();
            if (e.WebsocketDisconnectionInfo.Exception != null) LogMessage("OBS WebSocket disconnected: " + e.WebsocketDisconnectionInfo.Exception.Message);
            else LogMessage("OBS WebSocket disconnected");
        }

        private void WebSocket_Connected(object? sender, EventArgs e)
        {
            OBSUIChange();
            LogMessage("OBS WebSocket connected");
        }

        class DeviceItem
        {
            public string Name { get; }
            public IReceiver Receiver { get; }
            public DeviceItem(IReceiver receiver)
            {
                Name = receiver.FriendlyName;
                Receiver = receiver;
            }

            public override string ToString()
            {
                return Name + " (" + Receiver.Model + ")";
            }

        }
        async private Task GetCastDevices()
        {
            btRefreshCastDevices.Enabled = false;
            debug("Looking for cast devices...");
            cmbDeviceList.Items.Clear();
            var receivers = await new DeviceLocator().FindReceiversAsync();

            foreach (var receiver in receivers)
            {
                var r = new DeviceItem(receiver);
                cmbDeviceList.Items.Add(r);
                if (receiver.Id == opts.DeviceId) cmbDeviceList.SelectedIndex = cmbDeviceList.Items.Count - 1;
            }

            if (cmbDeviceList.Items.Count == 0) cmbDeviceList.Enabled = false;
            else cmbDeviceList.Enabled = true;
            btRefreshCastDevices.Enabled = true;

            if (cmbDeviceList.Items.Count > 0) debug(cmbDeviceList.Items.Count + " devices found.");
            else
            {
                debug("[Error] No devices found.", false, true);
                debug("Are you using a VPN?? Nest Deck can't detect your nest hub while you're using some VPNs.", false, true);
                debug("Turn OFF your VPN, click the refresh button, then you can turn it back ON again once there are detected devices.", false, true);
                debug("Is the Nest Hub on the same network? Your PC must be connected to the same network as the Nest Hub.", false, true);
            }

        }
        //******************************** MODS
        public class Action
        {
            public string ActionType { get; set; }
            public string Data { get; set; }
            public string ExtraData { get; set; }
        }
        private string handleAction(Action a)
        {
            string errMsg = "";
            switch (a.ActionType)
            {
                case SSE.Actions.OBSCommand:
                    {
                        if (obsIsRunning)
                        {
                            if (OBS.WebSocket.IsConnected)
                            {
                                OBS.handleCommand(a.Data);
                            }
                            else LogMessage("[Error] Received OBS action but OBS WebSocket is not connected", true);
                        }
                        else LogMessage("[Error] Received OBS action but OBS is not running", true);
                        break;
                    }
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

                        if (a.ExtraData != null && a.ExtraData != "") foreach (string k in a.ExtraData.Split(',')) modifiers.Add((WindowsInput.Native.VirtualKeyCode)int.Parse(k));
                        if (a.Data != null && a.Data != "") foreach (string k in a.Data.Split(',')) keys.Add((WindowsInput.Native.VirtualKeyCode)int.Parse(k));

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
                            errMsg = "[Error] There was an error running file or process. (" + ex.Message + ")";
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
                case SSE.Actions.AudioPlay:
                    {
                        var errorMsg = mediaPlayer.Play(a.Data);
                        if (errorMsg != string.Empty) LogMessage(errorMsg, true);
                        break;
                    }
            }
            return errMsg;
        }

        private System.Windows.Forms.OpenFileDialog findFileDialog;
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;
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
                SSEMessage(authString, SSE.Messages.EditorRespondSync);
                OBS_UpdatedData(sender, e);
                editorEvents.Disconnect();
                editorEvents.Connect(opts.Auth);
            }
            if (msg == "lookForFile")
            {
                findFileDialog = new System.Windows.Forms.OpenFileDialog();
                findFileDialog.Filter = "All Files (*.*)|*.*";
                var result = findFileDialog.ShowDialog();

                if (result == DialogResult.OK) SSEMessage(opts.Auth, "file " + findFileDialog.FileName);
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
            if (msg == SSE.Actions.FindAudio)
            {
                //this.Location = new Point(1, 1);
                findFileDialog = new System.Windows.Forms.OpenFileDialog();

                findFileDialog.Filter = "Audio (*.mp3,*.acc,*wma)|*.acc;*.mp3;*.wma;*.wav|All Files (*.*)|*.*";
                var result = findFileDialog.ShowDialog();
                if (result == DialogResult.OK) SSEMessage(opts.Auth, "file " + findFileDialog.FileName);
                else SSEMessage(opts.Auth, "file");
            }
            //else debug(msg);
        }

        private Utils.ActiveWindow _activeWindow;
        private void timer1_Tick(object sender, EventArgs e)
        {

            var obsProc = Process.GetProcessesByName("obs64");
            var obsWasRunning = obsIsRunning;
            obsIsRunning = obsProc.Length > 0;

            if (obsWasRunning != obsIsRunning)
            {
                LogMessage("OBS has " + (obsIsRunning ? "started" : "shutdown"));
                SetOBSUI();
                if (obsIsRunning) btOBSWSConnect_Click(sender, e); //Auto try connect
            }

            if (opts.MonitorProcess)
            {
                var currentActiveWindow = Utils.GetActiveWindow();
                //Null = First process change
                if (_activeWindow == null || (_activeWindow.isDifferent(currentActiveWindow) && !keyboardMonitor.IsAltPressed))
                {
                    if (_activeWindow != null) SendData(SSE.MessageEnvelope.Build(SSE.Messages.ProcessChanged, currentActiveWindow.Serialize()));
                    _activeWindow = currentActiveWindow;

                }
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
            chromecastSender.Disconnected += ChromecastSender_Disconnected;

            editorEvents.OnMessage += Events_OnMessage;
            editorEvents.OnStatusChange += Events_OnStatusChange;
            events.OnMessage += Events_OnMessage;
            events.OnStatusChange += Events_OnStatusChange;

            if (opts.Auth != "") editorEvents.Connect(opts.Auth);
        }

        private void Events_OnStatusChange(object sender, SSE.SSEConnectionStatus eventArgs)
        {
            switch (eventArgs.Status)
            {
                case EventSource4Net.EventSourceState.CLOSED:
                    {
                        statusData.Image = Resources.icons8_no_network_16;
                        LogMessage("Data connection closed.");
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

        public delegate void debugmt(string line, bool hideTimestamp = false, bool isError = false, bool verboseOnly = false);
        public delegate void SetOBSUImt();
        Dispatcher UserDispatcher = Dispatcher.CurrentDispatcher;
        private void LogMessage(string msg, bool isError = false, bool verboseOnly = false)
        {
            debugmt del = new debugmt(debug);
            UserDispatcher.Invoke(del, msg, false, isError, verboseOnly);
        }
        private void OBSUIChange()
        {
            SetOBSUImt del = new SetOBSUImt(SetOBSUI);
            UserDispatcher.Invoke(del);
        }

        private void StartDetectKeys()
        {
            SetOBSUImt del = new SetOBSUImt(DetectKeys);
            UserDispatcher.Invoke(del);
        }
        private void DetectKeys()
        {
            keycap = new KeyCap();
            var res = keycap.ShowDialog();
            if (res == DialogResult.OK)
            {

                SSE.KeyStroke keystroke = new SSE.KeyStroke();
                foreach (var k in keycap.Modifiers) keystroke.Modifiers.Add((int)k);
                keystroke.Key = (int)keycap.Key.First();

                SSEMessage(opts.Auth, SSE.Messages.KeysDetected + keystroke.Serialize());
            }
        }
        private void Verbose(string message)
        {
            LogMessage(message, false, true); //Verbose
        }
        private string lastSentMessage = "";
        private void Events_OnMessage(object sender, SSE.SSEArguments eventArgs)
        {
            bool blockVerbose = false;
            eventArgs.Message = eventArgs.Message.TrimEnd('\r', '\n');
            try
            {
                SSE.MessageEnvelope sseMsg = SSE.MessageEnvelope.Deserialize(eventArgs.Message);


                if (sseMsg != null) //Standardized Messages
                {
                    switch (sseMsg.type)
                    {
                        case SSE.Actions.FindAudio:
                            {
                                backgroundWorker1.ReportProgress(1, sseMsg.type);
                                break;
                            }
                        case SSE.Actions.AudioPlay:
                            {
                                var errorMsg = mediaPlayer.Play(sseMsg.data);
                                if (errorMsg != string.Empty) LogMessage(errorMsg, true);
                                break;
                            }
                        case SSE.Messages.TestAction:
                            {
                                try
                                {
                                    List<Action> actions = JsonConvert.DeserializeObject<List<Action>>(sseMsg.data);
                                    LogMessage("Test. Running " + actions.Count + " actions");
                                    foreach (Action a in actions)
                                    {
                                        string err = handleAction(a);
                                        if (err != "") LogMessage(err);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogMessage("[Error] Error executing command: " + ex.Message, true);
                                }
                                break;
                            }
                        default: break;
                    }
                    return;
                }
                else
                {
                    //Old message system

                    eventArgs.Message = eventArgs.Message.ReplaceLineEndings("");
                    var command = eventArgs.Message;
                    string[] args = eventArgs.Message.Split(' ');
                    if (args.Length > 0)
                    {
                        string cmd = args[0];
                        var commandData = "";
                        if (args.Length > 1) commandData = command.Substring(cmd.Length, command.Length - cmd.Length).Trim();

                        switch (cmd)
                        {
                            case SSE.Messages.ForceRefresh:
                                {
                                    //If connection to nesthub 
                                    if (cast_connected)
                                    {
                                        //Trigger on device
                                        SendData(SSE.Messages.ForceRefresh_Evt);
                                    }
                                    else
                                    {
                                        LogMessage("[Error] Received refresh command, but cast has not been started", true);
                                    }
                                    break;
                                }
                            case SSE.Messages.OpenKeyDetection:
                                {
                                    StartDetectKeys();
                                    break;
                                }
                            case SSE.Messages.AuthenticationRequest:
                                {
                                    LogMessage("Application initialized. Pairing...");
                                    HttpClient client = new HttpClient();
                                    client.GetAsync(_urlRegisterDevice + opts.DeviceId + "&AuthData=" + opts.Auth);
                                    break;
                                }
                            case SSE.Messages.Ping:
                                {
                                    sendPong(args[1]);
                                    break;
                                }
                            case SSE.Messages.ConnectionSuccess:
                                {
                                    sendPong();
                                    LogMessage("Application paired. Ready.");
                                    break;
                                }
                            case SSE.Messages.Timeout:
                                {
                                    LogMessage("Chromecast application lost connection to Windows. (Timeout)");
                                    if (cast_connected) backgroundWorker1.ReportProgress(1, "forceDisconnect");
                                    break;
                                }
                            case SSE.Messages.ButtonTap:
                                {
                                    try
                                    {
                                        if (opts.PlaySounds && opts.PlayOnPc) mediaPlayer.SystemSound_Switch();

                                        List<Action> actions = JsonConvert.DeserializeObject<List<Action>>(commandData);
                                        LogMessage("Button pressed. Running " + actions.Count + " actions");
                                        foreach (Action a in actions)
                                        {
                                            string err = handleAction(a);
                                            if (err != "") LogMessage(err);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogMessage("[Error] Error executing command: " + ex.Message, true);
                                    }
                                    break;
                                }
                            default:
                                {
                                    //UserDispatcher.Invoke(del, eventArgs.Message);
                                    break;
                                }
                        }
                    }


                    if (command.IndexOf("checkConnection") >= 0) blockVerbose = true; //block connection info received

                    switch (command)
                    {
                        case "reqAudio":
                            {
                                //LogMessage("Audio levels check requested");
                                var audioProcesses = volumeMixer.GetAudioProcesses();
                                //something
                                foreach (var device in OBS.GetAudioDevices())
                                {
                                    AudioProcess proc = new AudioProcess();
                                    proc.isOBS = true;
                                    proc.Name = device.Name;
                                    proc.Volume = device.Volume;
                                    audioProcesses.Add(proc);
                                }

                                string json = JsonConvert.SerializeObject(audioProcesses);

                                LogMessage("Audio levels - Main volume " + audioProcesses.First().Volume + "% | Processes: " + (audioProcesses.Count - 1));
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

                                    if (!proc.isOBS)
                                    {
                                        if (proc.PId == 0) volumeMixer.setGeneralVolume(proc.Volume);
                                        else VolumeMixer.SetApplicationVolume(proc.PId, proc.Volume);
                                    }
                                    else
                                    {
                                        if (OBS.IsConnected)
                                        {
                                            OBS.OBSData cmd = new OBS.OBSData();
                                            cmd.Command = OBS.Commands.InputSetVolume;
                                            cmd.Parameter1 = proc.Name;
                                            cmd.Parameter2 = (-1 * (100 - proc.Volume)).ToString();

                                            OBS.handleCommand(cmd.Serialize());
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
                if (blockVerbose) return;
                if (eventArgs.Message != lastSentMessage) Verbose("RECEIVED: " + eventArgs.Message);
            }
            catch (Exception ex)
            {
                LogMessage("[Error] Error decoding network message: " + eventArgs.Message, true);
            }

        }

        private void SSEMessage(string channel, string data)
        {
            WebClient wc = new WebClient();
            var url = new Uri(_urlSendData + channel);

            wc.Headers[HttpRequestHeader.ContentType] = "text/plain; charset=utf-8";
            wc.UploadStringAsync(url, data);

            lastSentMessage = data;
            Verbose("SENT: " + data);
        }
        private void SendData(string data)
        {
            //events.PostMessage(data);
            SSEMessage(opts.DeviceId, data);
        }
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
            if (cmbDeviceList.SelectedIndex == -1)
            {
                debug("[Error] You must first select a chromecast enabled device on the dropdown", false, true);
                if (cmbDeviceList.Items.Count == 0) debug("[Error] No devices detected. Use the tools menu to refresh the device list.", false, true);
                return;
            }
            btCast.Enabled = false;
            if (!cast_connected)
            {
                try
                {
                    cmbDeviceList.Enabled = false;
                    debug("Connecting to server...");

                    receiver = ((DeviceItem)(cmbDeviceList.SelectedItem)).Receiver;
                    opts.DeviceId = receiver.Id;
                    opts.Save();
                    //debug("Initializing connection...");
                    //Connect data connection
                    await events.Connect(receiver.Id);

                    SendData(SSE.Messages.TerminationSignal); //Close any running instances

                    Thread.Sleep(1000); //Wait 1 Second

                    // Connect to the Chromecast
                    chromecastSender.Connect(receiver);

                    //1A79419F New
                    //8A47528F Old
                    receiverStatus = await chromecastSender.LaunchAsync("1A79419F");

                    debug("Chromecast initialized.");
                    cast_connected = true;
                    statusCast.Image = Resources.icons8_connection_status_on_16;
                    btCast.Text = "Disconnect";
                    cmbDeviceList.Enabled = false;
                    //cmbDevices.Font = new Font(cmbDevices.Font, FontStyle.Bold);

                }
                catch (Exception ex)
                {
                    SendData(SSE.Messages.TerminationSignal);
                    statusCast.Image = Resources.icons8_no_network_16;
                    cast_connected = false;
                    btCast.Text = "📺 Cast";
                    cmbDeviceList.Enabled = true;
                    //cmbDevices.Font = new Font(cmbDevices.Font, FontStyle.Regular);
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
                btCast.Text = "📺 Cast";
                cmbDeviceList.Enabled = true;
                //cmbDevices.Font = new Font(cmbDevices.Font, FontStyle.Regular);

                SendData(SSE.Messages.TerminationSignal);
            }
            btCast.Enabled = true;
        }

        private void ChromecastSender_Disconnected(object? sender, EventArgs e)
        {
            receiver = null;
            cast_connected = false;
            statusCast.Image = Resources.icons8_no_network_16;
            btCast.Text = "📺 Cast";
            cmbDeviceList.Enabled = true;
            //cmbDevices.Font = new Font(cmbDevices.Font, FontStyle.Regular);
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
        private void SaveSettings(object sender, EventArgs e)
        {
            if (autoSaveSettings)
            {
                opts.StartWithWindows = chkWindowsStart.Checked;
                opts.StartMinimized = chkStartTray.Checked;
                opts.MonitorProcess = chkMonitorProcess.Checked;
                opts.AutoCast = chkAutoStartCast.Checked;
                opts.OBSPort = (int)numOBSPort.Value;
                opts.OBSPassword = txtOBSPwd.Text;
                opts.AlwaysOnTop = chkAlwaysOnTop.Checked;
                opts.PlaySounds = chkPlaySounds.Checked;
                opts.PlayOnPc = rdAudioFeedback_PlayInPc.Checked;
                opts.ShowTrail = chkShowTrail.Checked;
                opts.EnableBluetoothMonitoring = chkEnableBluetoothMonitoring.Checked;

                //Update UI
                this.TopMost = chkAlwaysOnTop.Checked;
                this.rdAudioFeedback_PlayInNH.Enabled = chkPlaySounds.Checked;
                this.rdAudioFeedback_PlayInPc.Enabled = chkPlaySounds.Checked;


                opts.Save();
            }
        }
        private void SaveSettingsAndUpdateHub(object sender, EventArgs e)
        {
            SaveSettings(sender, e);
            SendSystemInfo();
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

                Utils.BringWindowToFront(p);
            }
        }
        #endregion
        #region Imports

        #endregion
        #region UI
        private void SetOBSUI()
        {
            lblOBSStatus.Text = (obsIsRunning ? "Running" : "Not Running");
            lblOBSStatus.ForeColor = (obsIsRunning ? Color.Green : Color.Red);

            lblOBSWSStatus.Text = OBS.WebSocket.IsConnected ? (OBS.isRecording || OBS.isRecording ? (OBS.isRecording ? "● Recording" : "📡 Streaming") : "Connected") : "Not connected";
            lblOBSWSStatus.ForeColor = (OBS.WebSocket.IsConnected ? Color.Green : Color.Red);
            lblOBSSocket.Text = lblOBSWSStatus.Text;

            btOBSWSConnect.Enabled = true;
            btOBSWSConnect.Text = OBS.WebSocket.IsConnected ? "Disconnect" : "Connect";
        }
        private void toolClearLog_Click(object sender, EventArgs e)
        {
            txtDebugColor.Text = "";

        }

        private void toolRefreshDevices_Click(object sender, EventArgs e)
        {
            GetCastDevices();
        }

        private void numOBSPort_ValueChanged(object sender, EventArgs e)
        {
            SaveSettings(sender, e);
        }

        private void txtOBSPwd_TextChanged(object sender, EventArgs e)
        {
            SaveSettings(sender, e);
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
                lastDeviceSignature = "";

                LoadOptionsUI();
                timerMonitorHardware_Tick(sender, e);
                debug("Default options set.");
            }
        }
        private void LoadOptionsUI()
        {
            pictureLogo.BackColor = Color.Transparent;
            txtDebugColor.BackColor = Color.FromArgb(34, 34, 34);
            pbBackground.BackColor = txtDebugColor.BackColor;
            pictureLogo.BackColor = txtDebugColor.BackColor;
            panelHwMon.BackColor = txtDebugColor.BackColor;
            lnkClearLog.BackColor = txtDebugColor.BackColor;
            lblBattery.BackColor = txtDebugColor.BackColor;
            lnkAbout.BackColor = txtDebugColor.BackColor;
            /*btCast.BackColor = txtDebug.BackColor;
            cmbDeviceList.BackColor = txtDebug.BackColor;
            btCast.ForeColor = txtDebug.ForeColor;
            cmbDeviceList.ForeColor = txtDebug.ForeColor;*/


            chkWindowsStart.Checked = opts.StartWithWindows;
            chkStartTray.Checked = opts.StartMinimized;
            chkMonitorProcess.Checked = opts.MonitorProcess;
            chkAutoStartCast.Checked = opts.AutoCast;
            txtOBSPwd.Text = opts.OBSPassword;
            numOBSPort.Value = opts.OBSPort;
            chkAlwaysOnTop.Checked = opts.AlwaysOnTop;
            chkPlaySounds.Checked = opts.PlaySounds;
            chkEnableBluetoothMonitoring.Checked = opts.EnableBluetoothMonitoring;

            ThresholdCPUMin.Value = opts.ThresholdCPUMin;
            ThresholdCPUMax.Value = opts.ThresholdCPUMax;
            ThresholdGPUMin.Value = opts.ThresholdGPUMin;
            ThresholdGPUMax.Value = opts.ThresholdGPUMax;
            ThresholdRAMMin.Value = opts.ThresholdRAMMin;
            ThresholdRAMMax.Value = opts.ThresholdRAMMax;
            ThresholdNetDownMin.Value = opts.ThresholdNetDownMin;
            ThresholdNetDownMax.Value = opts.ThresholdNetDownMax;
            ThresholdNetUpMin.Value = opts.ThresholdNetUpMin;
            ThresholdNetUpMax.Value = opts.ThresholdNetUpMax;
            ThresholdPingMin.Value = opts.ThresholdPingMin;
            ThresholdPingMax.Value = opts.ThresholdPingMax;

            this.TopMost = chkAlwaysOnTop.Checked;

            chkPlaySounds.Checked = opts.PlaySounds;
            rdAudioFeedback_PlayInPc.Checked = opts.PlayOnPc;
            rdAudioFeedback_PlayInNH.Checked = !opts.PlayOnPc;
            this.rdAudioFeedback_PlayInNH.Enabled = chkPlaySounds.Checked;
            this.rdAudioFeedback_PlayInPc.Enabled = chkPlaySounds.Checked;

            chkShowTrail.Checked = opts.ShowTrail;

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
            //this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
            this.Focus();
        }

        private void debug(string line, bool hideTimestamp = false, bool isError = false, bool verboseOnly = false)
        {
            if (verboseOnly && !chkVerbose.Checked) return;

            string timestamp = (line != "" ? "[" + DateTime.Now.ToString("HH:mm:ss") + "] " : "");
            string newLine = line + Environment.NewLine;

            txtDebugColor.AppendText((hideTimestamp ? "" : timestamp) + newLine);

            txtDebugColor.SelectionStart = txtDebugColor.Text.Length;
            txtDebugColor.ScrollToCaret();

            txtDebugColor.Select(txtDebugColor.TextLength - newLine.Length, newLine.Length);

            if (isError) txtDebugColor.SelectionColor = Color.Red;
            else if (verboseOnly) txtDebugColor.SelectionColor = Color.LimeGreen;
            else txtDebugColor.SelectionColor = Color.White;



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
                var result = MessageBox.Show(
                    "Do you want to quit NestDeck?" + Environment.NewLine +
                    "Pressing keys won't have any effect until you launch this app again"
                    , "NestDeck", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) e.Cancel = true;
            }
            if (e.Cancel == false && cast_connected) SendData(SSE.Messages.TerminationSignal); //Close any running instances
        }
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                    "Do you want to quit NestDeck?" + Environment.NewLine +
                    "Pressing keys won't have any effect until you launch this app again"
                    , "NestDeck", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (cast_connected) SendData(SSE.Messages.TerminationSignal); //Close any running instances
                System.Windows.Forms.Application.Exit();
            }

        }
        private void lstCastDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btCast.Enabled = cmbDeviceList.SelectedIndex >= 0;
            //btCast.Enabled = (lstCastDevices.SelectedItems.Count > 0);
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

        private bool httpError = false;
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (httpError) { debug("[Error] An error ocurred while starting the HTTP server. Make sure there is no other instance of NestDeck running or port " + numPort.Value + " is not being used by another program.", false, true); }
            else
            {
                debug("Server stopped.");
                backgroundWorker1.RunWorkerAsync();
            }
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
            try
            {
                listener.Start();
                // Handle requests
                Task listenTask = HandleIncomingConnections(backgroundWorker1, effectiveUrl);
                listenTask.GetAwaiter().GetResult();

                // Close the listener
                listener.Close();
            }
            catch (Exception ex)
            {
                httpError = true;
            }
        }
        #endregion
        /********************************************************************************************************/

        private void btOBSWSConnect_Click(object sender, EventArgs e)
        {
            if (obsIsRunning)
            {
                btOBSWSConnect.Enabled = false;

                if (!OBS.WebSocket.IsConnected)
                {
                    debug("Connecting to OBS...");
                    OBS.TryConnect(opts.OBSPort, opts.OBSPassword);
                }
                else OBS.WebSocket.Disconnect();

            }
            else debug("Can't connect. OBS is not running.");
        }

        HWMonitor hw = new HWMonitor();
        private void timerMonitorHardware_Tick(object sender, EventArgs e)
        {
            hw.ReportSystemInfo();

            lblCPUTemp.Text = hw.cpuTemp.ToString() + "ºC";
            lblCPUUsage.Text = hw.cpuUsage.ToString("0.00") + "%";
            lblCPUPower.Text = hw.cpuPowerDrawPackage.ToString("0.00") + "W";

            lblGPUTemp.Text = hw.gpuTemp.ToString() + "ºC";
            lblGPUUsage.Text = hw.gpuUsage.ToString("0.00") + "%";
            lblGPUPower.Text = hw.gpuPowerDrawPackage.ToString("0.00") + "W";

            lblMemoryUsed.Text = hw.memoryUsed.ToString("0.00") + "GB";
            lblMemoryUsage.Text = hw.memoryLoad.ToString("0.00") + "%";

            lblNetDownload.Text = (hw.totalDownloadSpeed).ToString("0.00") + "Mbps ↑ " + (hw.totalUploadSpeed).ToString("0.00") + "Mbps";


            //This is read to be sent with pong payload.
            //Initially it was being ran there, but it increased the ping by a few hundred ms
            hw.Volume = volumeMixer.GetMainVolumeLevel();

            if (chkEnableBluetoothMonitoring.Checked && bgWorkerRefreshBtDevices.IsBusy == false) bgWorkerRefreshBtDevices.RunWorkerAsync();

        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debug("Checking for updates...");
            var version = Updater.CheckVersion();
            if (version == Updater.GetCurrentVersion()) debug("Your version is up to date");
            else if (version != Updater.GetCurrentVersion() && version != "") debug("There's a newer version available. Please update");
            else debug("[Error] There was an error while checking for newer versions", false, true);
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btCast.Enabled = cmbDeviceList.SelectedIndex >= 0;
        }

        private void chkWindowsStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWindowsStart.Checked) Options.AutoStart_Enable();
            else Options.AutoStart_Disable();

            SaveSettings(sender, e);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            var about = new About();
            about.ShowDialog();
        }

        private void Threshold_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                opts.ThresholdCPUMin = ThresholdCPUMin.Value;
                opts.ThresholdCPUMax = ThresholdCPUMax.Value;
                opts.ThresholdGPUMin = ThresholdGPUMin.Value;
                opts.ThresholdGPUMax = ThresholdGPUMax.Value;
                opts.ThresholdRAMMin = ThresholdRAMMin.Value;
                opts.ThresholdRAMMax = ThresholdRAMMax.Value;
                opts.ThresholdNetDownMin = ThresholdNetDownMin.Value;
                opts.ThresholdNetDownMax = ThresholdNetDownMax.Value;
                opts.ThresholdNetUpMin = ThresholdNetUpMin.Value;
                opts.ThresholdNetUpMax = ThresholdNetUpMax.Value;
                opts.ThresholdPingMin = ThresholdPingMin.Value;
                opts.ThresholdPingMax = ThresholdPingMax.Value;

                opts.Save();
                hw.LoadThresholds(opts);
            }
            catch
            {
                debug("Error saving settings", false, true);
            }
        }

        //Save thresholds - It doesn't really save anything, just sends it to the receiver if connected. And forces the user to blur the numUpDown controls
        private void button2_Click(object sender, EventArgs e)
        {
            debug("Hardware settings saved");
            if (cast_connected)
            {
                SendSystemInfo();
            }
        }

        private void sendPong(string argument = "")
        {
            if (receiver != null)
            {

                var hasParsed = double.TryParse(argument, out hw.Ticks);
                if (!hasParsed)
                {
                    hw.Ticks = 0;
                }
                hw.Ping = "<pingValue>";
                //hw.Volume = VolumeMixer.GetMainVolumeLevel(); //Get master audio volume

                string hwInfo = "{}";
                try
                {
                    hw.LoadThresholds(opts);
                    hwInfo = hw.Serialize();
                }
                catch
                {
                    //Restart the hardware monitor
                    hw = new HWMonitor();
                    hw.LoadThresholds(opts);
                    hwInfo = hw.Serialize();
                }

                string json =
                    "{'Model':'" + receiver.Model +
                    "','Capabilities':'" + receiver.Capabilities +
                    "','Version':'" + Updater.GetCurrentVersion() +
                    "', 'hwInfo': " + hwInfo +
                    ", 'Options':" + opts.SerializedNetworkOptions() +
                    ", OBS:" + OBS.Serialize() +
                    ", MediaInfo: " + mediaPlayer.Serialize() +
                    ", BatteryInfo: " + Bluetooth.BtDevice.SerializeList(opts.BluetoothDevices, btDevices) + " }";
                SendData(SSE.Messages.Pong + json);
            }
        }
        private void SendSystemInfo()
        {
            if (receiver != null && cast_connected)
            {
                string hwInfo = "{}";
                try
                {
                    hw.LoadThresholds(opts);
                    hwInfo = hw.Serialize();
                }
                catch
                {
                    //Restart the hardware monitor
                    hw = new HWMonitor();
                    hw.LoadThresholds(opts);
                    hwInfo = hw.Serialize();
                }
                string json = "{'hwInfo': " + hwInfo +
                    ", 'Options':" + opts.SerializedNetworkOptions() +
                    ", 'Version':'" + Updater.GetCurrentVersion() + "'" +
                    ", OBS:" + OBS.Serialize() +
                    ", MediaInfo: " + mediaPlayer.Serialize() +
                    ", BatteryInfo: " + Bluetooth.BtDevice.SerializeList(opts.BluetoothDevices, btDevices) +
                    " }";

                SendData(SSE.MessageEnvelope.Build(SSE.Messages.SystemInfo, json));
            }
        }

        private string lastDeviceSignature = "";
        private void bgWorkerRefreshBtDevices_DoWork(object sender, DoWorkEventArgs e)
        {
            var btData = hw.GetAllDevices();

            if (btData.Error != null && btData.Error != string.Empty) LogMessage("[Error] Bluetooth device retrieval: " + btData.Error, true);
            else
            {
                //Only refresh if data has changed
                if (btData.Signature != lastDeviceSignature)
                {
                    lastDeviceSignature = btData.Signature;
                    btDevices = btData.DeviceList;
                    bgWorkerRefreshBtDevices.ReportProgress(100);
                }
            }
        }

        private void bgWorkerRefreshBtDevices_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            gridBluetooth.Rows.Clear();
            foreach (var btDevice in btDevices)
            {
                var currentOption = opts.BluetoothDevices.Find(x => x.Name.Equals(btDevice.Name));
                if (currentOption != null)
                {
                    btDevice.IsSelected = currentOption.IsSelected;
                    btDevice.Type = currentOption.Type;
                }
                if (btDevice.Type == "") btDevice.Type = "None";
                gridBluetooth.Rows.Add(new object[] { btDevice.IsSelected, btDevice.Name, btDevice.Charge, btDevice.Type });
            }
            RenderBatteryInfo();
        }

        private void RenderBatteryInfo()
        {
            string boltSign = "⚡";
            string btInfo = boltSign;
            foreach (var btDevice in btDevices)
            {
                if (btDevice.IsSelected)
                {
                    btInfo += " " + Bluetooth.BtDevice.GetTypeIcon(btDevice.Type) + " " + btDevice.Charge + "% " + btDevice.Name;
                }
            }

            lblBattery.Text = btInfo;
            if (btInfo == boltSign) lblBattery.Visible = false;
            else lblBattery.Visible = true;
        }
        private void gridBluetooth_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Save options with grid changes
            if (e.RowIndex >= 0)
            {
                var changedRow = gridBluetooth.Rows[e.RowIndex];
                var deviceName = (string)changedRow.Cells[1].Value;

                var optsSetting = opts.BluetoothDevices.Find(x => x.Name.Equals(deviceName));
                var btDevice = btDevices.Find(x => x.Name.Equals(deviceName));


                //Settings exist?   
                if (optsSetting != null)
                {
                    //Change existing
                    optsSetting.IsSelected = (bool)changedRow.Cells[0].Value;
                    optsSetting.Type = (string)changedRow.Cells[3].Value;


                }
                else
                {
                    //Add new
                    optsSetting = new BtDevice(deviceName, 0, false); //Find in device list
                    optsSetting.IsSelected = (bool)changedRow.Cells[0].Value;
                    optsSetting.Type = (string)changedRow.Cells[3].Value;

                    opts.BluetoothDevices.Add(optsSetting);
                }


                if (btDevice != null)
                {
                    var idx = btDevices.IndexOf(btDevice);
                    btDevices[idx].IsSelected = optsSetting.IsSelected;
                    btDevices[idx].Type = optsSetting.Type;
                }

                RenderBatteryInfo();
                opts.Save();
            }
        }

        private void chkVerbose_CheckedChanged(object sender, EventArgs e)
        {
            debug("Verbose: " + (chkVerbose.Checked ? "ON" : "OFF"));
        }

        private void pictureLogo_Click(object sender, EventArgs e)
        {
            //Just to test some random stuff
            string keys = @"[
    {'ActionType':'SendKeystroke','Data':'27','ExtraData':'17,16'},  // Open Task Manager (Ctrl + Shift + Escape)
    {'ActionType':'Wait','Data':'500','ExtraData':''},               // Wait for Task Manager to open
    {'ActionType':'SendKeystroke','Data':'9','ExtraData':''},        // Press Tab to navigate to the tabs
    {'ActionType':'SendKeystroke','Data':'9','ExtraData':''},        // Press Tab to reach the ""Details"" tab
    {'ActionType':'SendKeystroke','Data':'13','ExtraData':''}        // Press Enter to select the ""Details"" tab
]
";
            List<Action> actions = JsonConvert.DeserializeObject<List<Action>>(keys);
            foreach (Action action in actions) { handleAction(action); }
        }
    }
}
