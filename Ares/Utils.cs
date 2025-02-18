using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Nest_Deck
{
    internal class Utils
    {
        public class MsgPayload
        {
            public string type = "";
            public string text = "";

            public static string GetMessage(string basetext) 
            {
                var payload = JsonConvert.DeserializeObject<MsgPayload>(basetext);
                return payload.text;
            }

            public MsgPayload (string msg)
            {
                type = "message";
                text = msg;
            }
            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
        // RECT structure to store window position and size
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        public static void CenterOverActiveWindow(Form frm)
        {
            // Get the handle of the currently active window
            IntPtr activeWindowHandle = GetForegroundWindow();

            if (activeWindowHandle != IntPtr.Zero)
            {
                // Get the active window's position and size
                RECT activeWindowRect;
                if (GetWindowRect(activeWindowHandle, out activeWindowRect))
                {
                    // Calculate the width and height of the active window
                    int activeWindowWidth = activeWindowRect.Right - activeWindowRect.Left;
                    int activeWindowHeight = activeWindowRect.Bottom - activeWindowRect.Top;

                    // Calculate the center position of the active window
                    int centerX = activeWindowRect.Left + (activeWindowWidth - frm.Width) / 2;
                    int centerY = activeWindowRect.Top + (activeWindowHeight - frm.Height) / 2;

                    // Set the form's location to the calculated center position
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.Location = new Point(centerX, centerY);
                }
            }
        }
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
