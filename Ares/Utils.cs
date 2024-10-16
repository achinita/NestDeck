using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nest_Deck
{
    internal class Utils
    {
        public static void KillOtherInstances(string appName) 
        {
            var curProc = Process.GetCurrentProcess();
            foreach (var p in Process.GetProcessesByName(appName))
            {
                if (p.Id != curProc.Id) p.Kill();
            }
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public class ActiveWindow
        {
            [JsonProperty]
            public string ProcessName;
            [JsonProperty]
            public string Title;

            public ActiveWindow(string processName, string windowTitle)
            {
                ProcessName = processName;
                Title = windowTitle;
            }

            public bool isDifferent(ActiveWindow otherWindow)
            {
                return (this.ProcessName != otherWindow.ProcessName || this.Title != otherWindow.Title);
            }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }


            public bool isExcluded()
            {
                //Alt Tab
                if (this.ProcessName == "explorer" && this.Title.Contains("Task Switching")) return true;
                else return false;

            }
        }
        public static ActiveWindow GetActiveWindow()
        {
            ActiveWindow activeWindow = new ActiveWindow(GetActiveProcess(), GetActiveWindowTitle());

            return activeWindow;
        }
        public static string GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        public static void BringWindowToFront(Process p)
        {
            const int SW_HIDE = 0;
            const int SW_SHOWNORMAL = 1;
            const int SW_SHOWMINIMIZED = 2;
            const int SW_SHOWMAXIMIZED = 3;
            const int SW_SHOWNOACTIVATE = 4;
            const int SW_RESTORE = 9;
            const int SW_SHOWDEFAULT = 10;

            IntPtr hWnd = p.MainWindowHandle;
            // if iconic, we need to restore the window
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, SW_RESTORE);
                Thread.Sleep(200);
            }
            // bring it to the foreground
            SetForegroundWindow(hWnd);

        }
    }
}
