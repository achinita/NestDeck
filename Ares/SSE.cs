using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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

            public string Serialize() {  return JsonConvert.SerializeObject(this); }
        }
        public static class Messages
        {
            public const string TerminationSignal = "termSignal"; //Sends a signal to the Chromecast application to close itself
            public const string ConnectionSuccess = "ConnectionOK"; //Received when Chromecast Application connects to SSE channel
            public const string Timeout = "timeout"; //Client application timed out waiting for a response from the Windows client
            public const string Ping = "connectionPing"; //Message sent by the chromecast application to check for connection status
            public const string Pong = "checkConnection"; //Response sent to a Ping message. If this fails to arrive, the chromecast application will exit
            public const string AuthenticationRequest = "RequestAuthentication"; //Received from the chromecast application, indicates it's ready to receive the user account paired with the windows client
            public const string ButtonTap = "[tapDeckButton]"; //A button has been tapped on the nest hub
            public const string OBSData = "obsData"; //Sent to Deck Editor with OBS Info (Scenes, Sources, etc)
            public const string EditorRespondSync = "synchOkAuth"; //Sent to the editor to confirm pairing
            public const string OpenKeyDetection = "detectkeys";
            public const string KeysDetected = "[keydetected]";
            public const string ForceRefresh = "forceRefresh"; //Force a refresh command received from the editor
            public const string ForceRefresh_Evt = "evt_forceRefresh";
            public const string OBSStatus = "OBSStatus"; //Send to nest hub with information on the obs status
            public const string TestAction = "testCommand";
            public const string SystemInfo = "sysInfo";
            public const string ProcessChanged = "procChange";
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
            es = new EventSource(new Uri(serviceUrl + DeviceId),60);
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
