using Nest_Deck;
using Newtonsoft.Json;
using SHARK_Deck;
using Sharpcaster.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SHARK_Deck.VolumeMixer;
using System.Web;

namespace Ares
{
    public partial class SHARK : Form
    {
        private async void HandleChromecastMessage(string msgJSON)
        {
            SSE.MessageEnvelope sseMsg = SSE.MessageEnvelope.Deserialize(msgJSON);

            if (sseMsg != null)
            {
                switch (sseMsg.type)
                {
                    //Chromecast requests authentication string
                    case SSE.Messages.Evt_AuthenticationRequest:
                        {
                            LogMessage("Application initialized. Pairing...");
                            var authString = HttpUtility.UrlDecode(opts.Auth);
                            SendMessageToChromecast(SSE.Messages.Msg_SendAuthentication, authString);
                            break;
                        }
                    //Application fully loaded
                    case SSE.Messages.Evt_ConnectionSuccess:
                        {
                            LogMessage("Application paired. Ready.");
                            SendClientAndHardwareData();
                            break;
                        }
                    case SSE.Messages.Evt_Ping:
                        {
                            //Pong
                            SendMessageToChromecast(SSE.Messages.Msg_Pong, sseMsg.data);
                            break;
                        }
                    case SSE.Messages.Evt_RequestAudioData:
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
                            SendMessageToChromecast(SSE.Messages.Msg_AudioData, json);

                            KeepAlive();
                            break;
                        }
                    case SSE.Messages.Evt_SetVolume:
                        {
                            VolumeMixer.AudioProcess proc = JsonConvert.DeserializeObject<VolumeMixer.AudioProcess>(sseMsg.data);

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
                            break;
                        }
                    case SSE.Messages.Evt_ButtonPress:
                        {
                            try
                            {
                                if (opts.PlaySounds)
                                {
                                    if (opts.PlayOnPc) mediaPlayer.SystemSound_Switch();
                                    else
                                    {
                                        var media = new Media
                                        {
                                            ContentUrl = "https://nestdeck.base64.pt/assets/switchon.mp3"
                                        };
                                        chromecastClient.MediaChannel.LoadAsync(media);
                                    }
                                }

                                List<Action> actions = JsonConvert.DeserializeObject<List<Action>>(sseMsg.data);
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
                    case SSE.Messages.Evt_KeepAlive:
                        {
                            KeepAlive();
                            break;
                        }
                    default:
                        {
                            LogMessage("Unhandled event received: " + msgJSON, true);
                            break;
                        }
                }
            }
            else LogMessage("Unhandled message: " + msgJSON, true);
        }
    }
}
