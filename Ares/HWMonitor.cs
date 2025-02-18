using LibreHardwareMonitor.Hardware;
using Newtonsoft.Json;
using SHARK_Deck;
using System.Net.NetworkInformation;

namespace Nest_Deck
{
    internal class HWMonitor
    {
        Computer c = new Computer();

        // CPU Temp
        public float cpuTemp;
        // CPU Usage
        public float cpuUsage;
        // CPU Power Draw (Package)
        public float cpuPowerDrawPackage;
        // CPU Frequency
        public float cpuFrequency;

        // GPU Temperature
        public float gpuTemp;
        // GPU Usage
        public float gpuUsage;
        // GPU Core Frequency
        public float gpuCoreFrequency;
        // GPU Memory Frequency
        public float gpuMemoryFrequency;
        public float gpuPowerDrawPackage;

        public float memoryUsed;
        public float memoryLoad;

        public double totalUploadSpeed;
        public double totalDownloadSpeed;

        public double Ticks;
        public string Ping;

        public int Volume;

        //Thresholds for indicators
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

        public bool HasVPNConnection { get; set; }

        public void LoadThresholds(Options opts)
        {
            ThresholdCPUMin = opts.ThresholdCPUMin;
            ThresholdCPUMax = opts.ThresholdCPUMax;
            ThresholdGPUMin = opts.ThresholdGPUMin;
            ThresholdGPUMax = opts.ThresholdGPUMax;
            ThresholdRAMMin = opts.ThresholdRAMMin;
            ThresholdRAMMax = opts.ThresholdRAMMax;
            ThresholdNetDownMin = opts.ThresholdNetDownMin;
            ThresholdNetDownMax = opts.ThresholdNetDownMax;
            ThresholdNetUpMin = opts.ThresholdNetUpMin;
            ThresholdNetUpMax = opts.ThresholdNetUpMax;
            ThresholdPingMin = opts.ThresholdPingMin;
            ThresholdPingMax = opts.ThresholdPingMax;
        }

        public HWMonitor()
        {

            c = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsNetworkEnabled = true,
                IsBatteryEnabled = true
            };
            c.Open();
        }
        public void CheckVPNConnection()
        {
            var previousVPNStatus = HasVPNConnection;

            HasVPNConnection = false;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface Interface in interfaces)
                {

                    if (Interface.OperationalStatus == OperationalStatus.Up && Interface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        if ((Interface.NetworkInterfaceType == NetworkInterfaceType.Ppp || (int)Interface.NetworkInterfaceType == 53))
                        {
                            HasVPNConnection = true;
                        }
                    }
                }
                if (previousVPNStatus != HasVPNConnection)
                {
                    OnVPNChanged();
                }
            }
        }
        public event EventHandler VPNChanged;

        protected virtual void OnVPNChanged()
        {

            VPNChanged?.Invoke(this, null);
        }
        public async void ReportSystemInfo()
        {
            double _totalUploadSpeed = 0;
            double _totalDownloadSpeed = 0;

            foreach (var hardware in c.Hardware)
            {

                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    // only fire the update when found
                    hardware.Update();

                    // loop through the data
                    foreach (var sensor in hardware.Sensors)
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("CPU Package"))
                        {
                            // store
                            cpuTemp = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("CPU Total"))
                        {
                            // store
                            cpuUsage = sensor.Value.GetValueOrDefault();

                        }
                        else if (sensor.SensorType == SensorType.Power && sensor.Name.Contains("CPU Package"))
                        {
                            // store
                            cpuPowerDrawPackage = sensor.Value.GetValueOrDefault();


                        }
                        else if (sensor.SensorType == SensorType.Clock && sensor.Name.Contains("CPU Core #1"))
                        {
                            // store
                            cpuFrequency = sensor.Value.GetValueOrDefault();
                        }
                }


                // Targets AMD & Nvidia GPUS
                if (hardware.HardwareType == HardwareType.GpuAmd || hardware.HardwareType == HardwareType.GpuNvidia)
                {
                    // only fire the update when found
                    hardware.Update();

                    // loop through the data
                    foreach (var sensor in hardware.Sensors)
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("GPU Core"))
                        {
                            // store
                            gpuTemp = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("GPU Core"))
                        {
                            // store
                            gpuUsage = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Clock && sensor.Name.Contains("GPU Core"))
                        {
                            // store
                            gpuCoreFrequency = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Clock && sensor.Name.Contains("GPU Memory"))
                        {
                            // store
                            gpuMemoryFrequency = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Power && sensor.Name.Contains("GPU Package"))
                        {
                            gpuPowerDrawPackage = sensor.Value.GetValueOrDefault();
                        }

                }

                //Memory
                if (hardware.HardwareType == HardwareType.Memory)
                {
                    // only fire the update when found
                    hardware.Update();

                    // loop through the data
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Data && sensor.Name.ToLower() == "memory used")
                        {
                            memoryUsed = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.ToLower() == "memory")
                        {
                            memoryLoad = sensor.Value.GetValueOrDefault();
                        }
                    }
                }

                //Network
                if (hardware.HardwareType == HardwareType.Network)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Throughput && sensor.Value > 0)
                        {
                            if (sensor.Name.ToLower().Contains("upload"))
                            {
                                _totalUploadSpeed += sensor.Value.GetValueOrDefault();
                            }
                            else if (sensor.Name.ToLower().Contains("download"))
                            {
                                _totalDownloadSpeed += sensor.Value.GetValueOrDefault();
                            }

                        }
                    }
                }

                if (hardware.HardwareType == HardwareType.Battery)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        Console.WriteLine(sensor.Name);
                    }
                }
            }
            totalDownloadSpeed = _totalDownloadSpeed * 0.000008;
            totalUploadSpeed = _totalUploadSpeed * 0.000008;

            CheckVPNConnection();
            //GetBlutoothInfoAsync();
        }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public Bluetooth.BtDevice.BtDeviceData GetAllDevices()
        {
            return Bluetooth.BtDevice.GetAllDevices();
        }
    }
}
