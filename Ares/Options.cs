using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;
using Nest_Deck;
using Newtonsoft.Json;

namespace SHARK_Deck
{
    public class Options
    {
        private const string settingsFile = "settings.conf";
        public bool StartWithWindows { get; set; }
        public bool StartMinimized { get; set; }

        public bool MonitorProcess { get; set; }
        public string Auth { get; set; }

        public string DeviceId { get; set; }

        public bool AutoCast { get; set; }

        public int OBSPort { get; set; }
        public string OBSPassword { get; set; }

        public decimal ThresholdCPUMin { get; set; }
        public decimal ThresholdCPUMax { get; set; }
        public decimal ThresholdGPUMin { get; set; }
        public decimal ThresholdGPUMax { get; set; }
        public decimal ThresholdRAMMin { get; set; }
        public decimal ThresholdRAMMax { get; set; }
        public decimal ThresholdNetUpMin { get; set; }
        public decimal ThresholdNetUpMax { get; set; }
        public decimal ThresholdNetDownMin { get; set; }
        public decimal ThresholdNetDownMax { get; set; }
        public decimal ThresholdPingMin { get; set; }
        public decimal ThresholdPingMax { get; set; }

        public List<Bluetooth.BtDevice> BluetoothDevices { get; set; }

        public bool AlwaysOnTop { get; set; }

        public bool PlaySounds { get; set; }
        public bool PlayOnPc { get; set; }

        public bool ShowTrail { get; set; }

        public bool EnableBluetoothMonitoring { get; set; }

        public Options() { 
            StartWithWindows = false; StartMinimized = false; MonitorProcess = false; Auth = ""; DeviceId = ""; OBSPort = 4445; OBSPassword = string.Empty;
            ThresholdCPUMin = 15; ThresholdCPUMax = 50;
            ThresholdGPUMin = 15; ThresholdGPUMax = 50;
            ThresholdRAMMin = 25; ThresholdRAMMax = 70;
            ThresholdNetUpMin = 2; ThresholdNetUpMax = 25;
            ThresholdNetDownMin = 2; ThresholdNetDownMax = 30;
            ThresholdPingMin = 500; ThresholdPingMax = 800;
            BluetoothDevices = new List<Bluetooth.BtDevice>();
            PlaySounds = true;
            PlayOnPc = true;
            ShowTrail = true;
        }
        private static string settingsPath ()
        {
            string appData = System.Windows.Forms.Application.UserAppDataPath;
            return appData + "\\" + settingsFile;
        }

        public void Load() 
        {
            if (File.Exists(settingsPath()))
            {
                Options o = JsonConvert.DeserializeObject<Options>(File.ReadAllText(settingsPath()));
                this.StartMinimized = o.StartMinimized;
                this.StartWithWindows = o.StartWithWindows;
                this.MonitorProcess= o.MonitorProcess;
                this.Auth = o.Auth;
                this.DeviceId = o.DeviceId;
                this.AutoCast = o.AutoCast;
                this.OBSPort = o.OBSPort;
                this.OBSPassword = o.OBSPassword;

                this.ThresholdCPUMin = o.ThresholdCPUMin; this.ThresholdCPUMax = o.ThresholdCPUMax;
                this.ThresholdGPUMin = o.ThresholdGPUMin; this.ThresholdGPUMax = o.ThresholdGPUMax;
                this.ThresholdRAMMin = o.ThresholdRAMMin; this.ThresholdRAMMax = o.ThresholdRAMMax;
                this.ThresholdNetUpMin = o.ThresholdNetUpMin; this.ThresholdNetUpMax = o.ThresholdNetUpMax;
                this.ThresholdNetDownMin = o.ThresholdNetDownMin; this.ThresholdNetDownMax = o.ThresholdNetDownMax;
                this.ThresholdPingMin = o.ThresholdPingMin; this.ThresholdPingMax = o.ThresholdPingMax;

                this.BluetoothDevices = o.BluetoothDevices;
                this.PlaySounds = o.PlaySounds;
                this.PlayOnPc = o.PlayOnPc;
                this.ShowTrail = o.ShowTrail;
                this.EnableBluetoothMonitoring = o.EnableBluetoothMonitoring;
            }
        }

        public void Save()
        {
            File.WriteAllText(settingsPath(), JsonConvert.SerializeObject(this));
        }

        //A simplified version of the main Options class, to be sent serialized over the network
        public class OptionsNetwork
        {
            public bool PlayAudioNestHub { get; set; }
            public bool ShowTrail { get; set; }
            
            public OptionsNetwork(Options opts)
            {
                PlayAudioNestHub = opts.PlaySounds && opts.PlayOnPc == false;
                ShowTrail = opts.ShowTrail;
            }
        }
        public string SerializedNetworkOptions()
        {
            return JsonConvert.SerializeObject(new OptionsNetwork(this));
        }
        public static void AutoStart_Enable()
        {
            string taskName = Application.ProductName;
            string exePath = Application.ExecutablePath;

            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Starts Nest Deck with Windows";
                td.Principal.RunLevel = TaskRunLevel.Highest;

                // Create a trigger that will fire at logon
                td.Triggers.Add(new LogonTrigger());

                // Create an action that will launch the application
                td.Actions.Add(new ExecAction(exePath, null, null));

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(taskName, td);
            }
        }
        public static void AutoStart_Disable()
        {
            string taskName = Application.ProductName;

            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(taskName, false);
            }
        }
    }
}
