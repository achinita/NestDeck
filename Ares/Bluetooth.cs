using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using static Nest_Deck.Bluetooth;

namespace Nest_Deck
{
    public class Bluetooth
    {
        private const string toolPath = "Tools\\BtFinder\\BtFinder.exe";
        
        public static bool CheckTool()
        {
            return File.Exists(toolPath);
        }

        public class BtDevice
        {
            [JsonProperty]
            public string Name { get; set; }
            [JsonProperty]
            public int Charge { get; set; }
            [JsonProperty]
            public bool IsCharging { get; set; }

            [JsonProperty]
            public bool IsSelected { get; set; }

            [JsonProperty]
            public string Type { get; set; }

            public BtDevice(string name, int charge, bool isCharging)
            {
                Name = name;
                Charge = charge;
                IsCharging = isCharging;
            }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }
            public static string SerializeList(List<BtDevice> optsList, List<BtDevice> connectedDevices)
            {
                List<BtDevice> serializable = new List<BtDevice>();
                foreach (BtDevice device in optsList)
                {
                    if (device.IsSelected)
                    {
                        var connectedItem = connectedDevices.Find(x => x.Name.Equals(device.Name));
                        if (connectedItem != null) serializable.Add(connectedItem);
                    }
                }
                return JsonConvert.SerializeObject(serializable);
            }
            public struct BtDeviceData
            {
                public string Signature;
                public List<BtDevice> DeviceList;
                public string Error;
            }
            public static BtDeviceData GetAllDevices() 
            {
                BtDeviceData deviceData = new BtDeviceData();
                deviceData.DeviceList = new List<BtDevice>();

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = toolPath,
                    RedirectStandardOutput = true,  // Redirect standard output
                    RedirectStandardError = true,   // Redirect standard error
                    UseShellExecute = false,        // Required for redirection
                    CreateNoWindow = true           // Optional: prevents creating a new window
                };
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;

                    // Start the process
                    process.Start();

                    // Read the output and error streams
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // Wait for the process to exit
                    process.WaitForExit();

                    if (error != null && error != string.Empty)
                    {
                        deviceData.Error = error;   
                    }
                    else
                    {
                        try
                        {
                            deviceData.Signature = output;
                            deviceData.DeviceList = JsonConvert.DeserializeObject<List<BtDevice>>(output);
                        }
                        catch
                        {
                            //Do nothing
                        }
                    }
                }

                return deviceData;
            }
            public static string GetTypeIcon(string type)
            {
                switch (type)
                {
                    case "Keyboard": return "⌨"; break;
                    case "Mouse": return "🖱️"; break;
                    case "Audio": return "🎧"; break;
                    case "Gamepad": return "🎮"; break;
                    default: return "";
                }
            }
        }
    }
}
