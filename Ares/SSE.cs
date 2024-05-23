using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using EventSource4Net;

namespace SHARK_Deck
{
    internal class SSE
    {
        public static class Messages
        {
            public const string TerminationSignal = "termSignal"; //Sends a signal to the Chromecast application to close itself
            public const string ConnectionSuccess = "ConnectionOK"; //Received when Chromecast Application connects to SSE channel
            public const string Timeout = "timeout"; //Client application timed out waiting for a response from the Windows client
            public const string Ping = "connectionPing"; //Message sent by the chromecast application to check for connection status
            public const string Pong = "checkConnection"; //Response sent to a Ping message. If this fails to arrive, the chromecast application will exit
            public const string AuthenticationRequest = "RequestAuthentication"; //Received from the chromecast application, indicates it's ready to receive the user account paired with the windows client
        }
        private const string serviceUrl = "https://achinita.outsystemscloud.com/SHARK_IS/rest/SSE/Subscribe?Channel=";
        public SSE() { 
            }

        private Stream _stream = null;
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
            //_stream.Write();
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
