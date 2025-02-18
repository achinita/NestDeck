using EventSource4Net;
using Newtonsoft.Json;

namespace SHARK_Deck
{
    internal class SSE
    {
        public class MessageEnvelope
        {
            [JsonProperty]
            public string type = "";
            [JsonProperty]
            public string data = "";
            public MessageEnvelope(string MessageType, string MessageData)
            {
                type = MessageType;
                data = MessageData;
            }

            public static string Build(string MessageType, string MessageData)
            {
                var env = new MessageEnvelope(MessageType, MessageData);
                return env.Serialize();
            }
            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }
            public static MessageEnvelope Deserialize(string json)
            {
                try
                {
                    return JsonConvert.DeserializeObject<MessageEnvelope>(json);
                }
                catch
                {
                    return null;
                }

            }
        }
        public class KeyStroke
        {
            [JsonProperty]
            public List<int> Modifiers = new List<int>();
            [JsonProperty]
            public int Key = 0;

            public KeyStroke()
            {

            }
            public KeyStroke(List<int> mods, int k)
            {
                Modifiers = mods;
                Key = k;
            }

            public string Serialize() { return JsonConvert.SerializeObject(this); }
        }
        public static class Messages
        {
            //v3 - Event received from chromecast
            public const string Evt_AuthenticationRequest = "RequestAuthentication"; //Received from the chromecast application, indicates it's ready to receive the user account paired with the windows client
            public const string Evt_ConnectionSuccess = "ConnectionOK"; //Received when Chromecast Application loads for the first time
            public const string Evt_Ping = "ping"; //Ping
            public const string Evt_RequestAudioData = "RequestAudioData"; //Hub requested audio info
            public const string Evt_SetVolume = "setVolume"; //Set volume of process/source
            public const string Evt_ButtonPress = "ButtonPress"; //A button was pressed on a deck
            public const string Evt_KeepAlive = "keepalive";
            public const string Evt_ForceRefresh = "refreshDecks";

            //v3 - Events to send to chromecast
            public const string Msg_SendAuthentication = "AuthString"; //Send authentication string to the client
            public const string Msg_Pong = "pong"; //Ping? Pong!
            public const string Msg_HardwareValues = "hwValues"; //Hardware monitoring values
            public const string Msg_AudioData = "AudioData";
            public const string Msg_OBSStatus = "OBSStatus"; //Send to nest hub with information on the obs status
            public const string Msg_TerminationSignal = "termSignal"; //Sends a signal to the Chromecast application to close itself
            public const string Msg_ForceRefresh = "forceRefresh"; //Tells the receiver to refresh the decks
            public const string Msg_ProcessChanged = "procChange";

            //v3 - Events received from web editor
            public const string Evt_TestAction = "testCommand";

            //v2 - Older


            public const string Timeout = "timeout"; //Client application timed out waiting for a response from the Windows client
            public const string Ping = "connectionPing"; //Message sent by the chromecast application to check for connection status
            public const string Pong = "checkConnection"; //Response sent to a Ping message. If this fails to arrive, the chromecast application will exit
            public const string ButtonTap = "[tapDeckButton]"; //A button has been tapped on the nest hub
            public const string OBSData = "obsData"; //Sent to Deck Editor with OBS Info (Scenes, Sources, etc)
            public const string EditorRespondSync = "synchOkAuth"; //Sent to the editor to confirm pairing
            public const string OpenKeyDetection = "detectkeys";
            public const string KeysDetected = "[keydetected]";
            public const string ForceRefresh = "forceRefresh"; //Force a refresh command received from the editor

            public const string SystemInfo = "sysInfo";
            
        }

        public static class Actions
        {
            public const string OBSCommand = "OBSCommand"; //An action to be sent to OBS
            public const string AudioPlay = "playAudio"; //Plays an audio file
            public const string FindAudio = "lookForAudioFile"; //Opens a dialog to look for an audio file
        }
        private const string serviceUrl = "https://achinita.outsystemscloud.com/SHARK_IS/rest/SSE/Subscribe?Channel=";
        public SSE()
        {
        }
        EventSource es = null;
        public async Task Connect(string DeviceId)
        {
            if (es != null)
            {
                es.EventReceived -= Es_EventReceived;
                es.StateChanged -= Es_StateChanged;
            }
            es = new EventSource(new Uri(serviceUrl + DeviceId), 60);
            es.EventReceived += Es_EventReceived;
            es.StateChanged += Es_StateChanged;
            es.Start(CancellationToken.None);

        }

        private void Es_StateChanged(object? sender, StateChangedEventArgs e)
        {
            SSEConnectionStatus eventArgs = new SSEConnectionStatus();
            eventArgs.Status = e.State;

            StatusChangeHandler ev = this.OnStatusChange;
            if (ev != null)
            {
                ev(this, eventArgs);
            }

        }

        private void Es_EventReceived(object? sender, ServerSentEventReceivedEventArgs e)
        {
            SSEArguments eventArgs = new SSEArguments();
            eventArgs.Message = e.Message.Data;
            if (eventArgs.Message != null && eventArgs.Message != string.Empty)
            {
                ReturnIntHandler ev = this.OnMessage;
                if (ev != null)
                {
                    ev(this, eventArgs);
                }
            }
        }

        public void PostMessage(string msg)
        {
            throw new NotImplementedException();
        }
        public void Disconnect()
        {

        }
        public delegate void ReturnIntHandler(object sender, SSEArguments eventArgs);
        public event ReturnIntHandler OnMessage;
        public class SSEArguments : EventArgs
        {
            public string Message = string.Empty;
        }

        public delegate void StatusChangeHandler(object sender, SSEConnectionStatus eventArgs);
        public event StatusChangeHandler OnStatusChange;
        public class SSEConnectionStatus : EventArgs
        {
            public EventSourceState Status = EventSourceState.CLOSED;
        }

    }
}
