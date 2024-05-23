using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Options() { 
            StartWithWindows = false; StartMinimized = false; MonitorProcess = false; Auth = ""; DeviceId = "";
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
            }
        }

        public void Save()
        {
            File.WriteAllText(settingsPath(), JsonConvert.SerializeObject(this));
        }
    }
}
