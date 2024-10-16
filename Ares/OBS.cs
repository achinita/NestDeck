using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using System.Threading;
using Newtonsoft.Json;
using static SHARK_Deck.SSE;
using static SHARK_Deck.VolumeMixer;

namespace Nest_Deck
{
    public class Scene : SceneBasicInfo
    {
        [JsonProperty]
        public List<SceneItemDetails> Items = new List<SceneItemDetails>();

        public Scene()
    : base()
        {
        }
    }
    internal class OBS
    {   
        public OBSWebsocket WebSocket = new OBSWebsocket();
        public bool IsConnected { get; set; }
        [JsonProperty]
        public List<InputBasicInfo> Inputs = new List<InputBasicInfo>();
        [JsonProperty]
        List<Scene> Scenes = new List<Scene>();
        public OBS() {
            WebSocket.Connected += Socket_Connected;
            WebSocket.Disconnected += Socket_Disconnected;
            WebSocket.InputNameChanged += WebSocket_InputNameChanged;
            WebSocket.SceneListChanged += WebSocket_SceneListChanged;
            WebSocket.SceneNameChanged += WebSocket_SceneNameChanged;
            WebSocket.RecordStateChanged += WebSocket_RecordStateChanged;
            WebSocket.StreamStateChanged += WebSocket_StreamStateChanged;
        }
        [JsonProperty]
        public bool isStreaming = false;
        public event EventHandler UpdatedStreamingStatus;
        protected virtual void OnUpdatedStreamingStatus()
        {
            UpdatedStreamingStatus?.Invoke(this, null);
        }
        private void WebSocket_StreamStateChanged(object? sender, OBSWebsocketDotNet.Types.Events.StreamStateChangedEventArgs e)
        {
            switch (e.OutputState.State)
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED: isStreaming = true; OnUpdatedStreamingStatus(); break;
                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED: isStreaming = false; OnUpdatedStreamingStatus(); break;
                default: break;
            }
        }

        public event EventHandler UpdatedRecordingStatus;

        protected virtual void OnUpdatedRecordingStatus()
        {

            UpdatedRecordingStatus?.Invoke(this, null);
        }
        [JsonProperty]
        public bool isRecording = false;
        private void WebSocket_RecordStateChanged(object? sender, OBSWebsocketDotNet.Types.Events.RecordStateChangedEventArgs e)
        {
            switch (e.OutputState.State)
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED: isRecording = true; OnUpdatedRecordingStatus(); break;
                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED: isRecording = false; OnUpdatedRecordingStatus(); break;
                default: break;
            }
        }

        private void WebSocket_SceneNameChanged(object? sender, OBSWebsocketDotNet.Types.Events.SceneNameChangedEventArgs e)
        {
            OnUpdatedData();
        }

        private void WebSocket_SceneListChanged(object? sender, OBSWebsocketDotNet.Types.Events.SceneListChangedEventArgs e)
        {
            OnUpdatedData();
        }

        private void WebSocket_InputNameChanged(object? sender, OBSWebsocketDotNet.Types.Events.InputNameChangedEventArgs e)
        {
            OnUpdatedData();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        private void Socket_Disconnected(object? sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            if (Inputs != null) Inputs.Clear();
            if (Scenes != null) Scenes.Clear();

            IsConnected = false;
        }

        private void Socket_Connected(object? sender, EventArgs e)
        {
            Thread.Sleep(1000); //Wait 1s - OBS might just have started and not have loaded data into the WS server

            var wasRecording = isRecording; var wasStreaming = isStreaming;
            var recordingStatus = WebSocket.GetRecordStatus();
            var streamingStatus = WebSocket.GetStreamStatus();
            isRecording = recordingStatus.IsRecording;
            isStreaming = streamingStatus.Duration > 0;

            if (wasRecording != isRecording) OnUpdatedRecordingStatus();
            if (wasStreaming != isStreaming) OnUpdatedStreamingStatus();

            IsConnected = true;

            OnUpdatedData();
        }

        public void TryConnect(int port, string password)
        {
            WebSocket.ConnectAsync("ws://127.0.0.1:" + port, password);
        }
        public event EventHandler UpdatedData;

        protected virtual void OnUpdatedData()
        {

            Inputs = WebSocket.GetInputList();
            var basicScenes = WebSocket.ListScenes();

            Scenes = new List<Scene>();
            foreach (var scene in basicScenes)
            {
                Scene c = new Scene();
                c.Name = scene.Name;
                c.Index = scene.Index;

                foreach (var item in WebSocket.GetSceneItemList(scene.Name)) c.Items.Add(item);

                Scenes.Add(c);
            }
            //UpdatedData?.Invoke(this, e);
            UpdatedData?.Invoke(this, null);
        }
        public class SimplifiedData
        {
            public List<string> Scenes { get; set; }

            public SimplifiedData () 
            {
                Scenes = new List<string>();
            }
        }
        public SimplifiedData SimplifyData ()
        {
            var sdata = new SimplifiedData();
            foreach (var s in Scenes) sdata.Scenes.Add(s.Name);

            return sdata;
        }
        public class OBSData
        {
            public string Command = "";
            public string Parameter1 = "";
            public string Parameter2 = "";
            public OBSData () 
            { 
            }
            public void LoadData(string JSON)
            {
                OBSData tmp = JsonConvert.DeserializeObject<OBSData>(JSON);
                this.Command = tmp.Command;
                this.Parameter1 = tmp.Parameter1;
                this.Parameter2 = tmp.Parameter2;
            }
            public string Serialize() {  return JsonConvert.SerializeObject(this); }
        }
        public static class Commands
        {
            //Stream
            public const string StartStream = "startstream"; //Starts a stream
            public const string StopStream = "stopstream"; //Stop a stream
            public const string ToggleStream = "togglestream"; //Toggle stream

            //Record
            public const string StartRecording = "startrec"; //Starts recording
            public const string StopRecording = "stoprec"; //Stop recording
            public const string ToggleRecording = "togglerec"; //Toggle Recording (Start/Stop)

            //Scene
            public const string SwitchScene = "switchscene"; //Switch scene

            //Mixer
            public const string InputToggle = "toggleinput";
            public const string InputMute = "muteinput";
            public const string InputUnmute = "unmuteinput";
            public const string InputSetVolume = "setvolume";

            //Show hide elements
            public const string SourceToggle = "sceneitemtoggle";
            public const string SourceHide = "sceneitemhide";
            public const string SourceShow = "sceneitemshow";

            //OBS
            public const string StudioToggle = "togglestudiomode";
            
        }
        public class Device
        {
            public string Name;
            public int Volume;
            public Device(string name, int volume)
            {
                Name = name;
                Volume = volume;
            }
            public static class Types
            {
                public const string AudioOutput = "wasapi_output_capture";
                public const string AudioInput = "wasapi_input_capture";
            }
        }
        public List<Device> GetAudioDevices()
        {
            var _out = new List<Device>();
            if (this.IsConnected)
            {
                foreach (var device in this.Inputs)
                {
                    if (device.InputKind == Device.Types.AudioOutput || device.InputKind == Device.Types.AudioInput)
                    {
                        var vol = this.WebSocket.GetInputVolume(device.InputName);
                        _out.Add(new Device(device.InputName, 100 + (int)(vol.VolumeDb)));
                    }
                }
            }
            return _out;
        }
        public void handleCommand(string JSONData)
        {
            OBSData command = new OBSData();
            command.LoadData(JSONData);

            var obs = this.WebSocket;
            switch (command.Command)
            {
                //Stream
                case OBS.Commands.StartStream:
                    {
                        obs.StartStream();
                        break;
                    }
                case OBS.Commands.StopStream:
                    {
                        obs.StopStream();
                        break;
                    }
                case OBS.Commands.ToggleStream:
                    {
                        obs.ToggleStream();
                        break;
                    }

                //Recording
                case OBS.Commands.StartRecording:
                    {
                        obs.StartRecord();
                        break;
                    }
                case OBS.Commands.StopRecording:
                    {
                        obs.StopRecord();
                        break;
                    }
                case OBS.Commands.ToggleRecording:
                    {
                        obs.ToggleRecord();
                        break;
                    }
                //Scene control
                case OBS.Commands.SwitchScene:
                    {
                        obs.SetCurrentProgramScene(command.Parameter1);
                        break;
                    }
                case OBS.Commands.InputToggle:
                    {
                        obs.ToggleInputMute(command.Parameter1);
                        break;
                    }
                case OBS.Commands.InputMute:
                    {
                        obs.SetInputMute(command.Parameter1, true);
                        break;
                    }
                case OBS.Commands.InputUnmute:
                    {
                        obs.SetInputMute(command.Parameter1, false);
                        break;
                    }
                case OBS.Commands.InputSetVolume:
                    {
                        float val = float.Parse(command.Parameter2);
                        obs.SetInputVolume(command.Parameter1, val , true);
                        break;
                    }
                case OBS.Commands.SourceToggle:
                    {
                        int sceneItemId = obs.GetSceneItemId(command.Parameter1, command.Parameter2, 0);
                        bool currentVisibility = obs.GetSceneItemEnabled(command.Parameter1, sceneItemId);
                        obs.SetSceneItemEnabled(command.Parameter1, sceneItemId, !currentVisibility);
                        break;
                    }
                case OBS.Commands.SourceHide:
                    {
                        int sceneItemId = obs.GetSceneItemId(command.Parameter1, command.Parameter2, 0);
                        obs.SetSceneItemEnabled(command.Parameter1, sceneItemId, false);
                        break;
                    }
                case OBS.Commands.SourceShow:
                    {
                        int sceneItemId = obs.GetSceneItemId(command.Parameter1, command.Parameter2, 0);
                        obs.SetSceneItemEnabled(command.Parameter1, sceneItemId, true);
                        break;
                    }
                case OBS.Commands.StudioToggle:
                    {
                        obs.SetStudioModeEnabled(!obs.GetStudioModeEnabled());
                        break;
                    }
                default: break;
            }
        }
    }
}
