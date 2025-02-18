using Gma.System.MouseKeyHook;

namespace SHARK_Deck
{
    internal class KeyboardMonitor
    {

        private bool isAltPressed = false;
        private bool isTabPressed = false;

        public KeyboardMonitor()
        {
            Subscribe();
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public bool AltTabPressed { get => (IsAltPressed && IsTabPressed); }
        public bool IsAltPressed { get => isAltPressed; set => isAltPressed = value; }
        public bool IsTabPressed { get => isTabPressed; set => isTabPressed = value; }

        private void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
            m_GlobalHook.KeyUp += M_GlobalHook_KeyUp;
        }

        private enum EventType
        {
            KeyDown, KeyUp
        }
        private EventType _lastEvent;
        private void M_GlobalHook_KeyUp(object? sender, KeyEventArgs e)
        {

            if (e.KeyValue == 9) IsTabPressed = false;
            if (e.KeyValue == 164) IsAltPressed = false;

            if (_lastEvent == EventType.KeyDown) OnKeyPress(this, e);
            _lastEvent = EventType.KeyUp;
            //if (_pressedsKeys.Contains(e.KeyValue)) _pressedsKeys.Remove(e.KeyValue);v
        }

        private void M_GlobalHook_KeyDown(object? sender, KeyEventArgs e)
        {

            if (e.KeyValue == 9) IsTabPressed = true;
            if (e.KeyValue == 164) IsAltPressed = true;
            _lastEvent = EventType.KeyDown;

            // if (!_pressedsKeys.Contains(e.KeyValue)) _pressedsKeys.Add(e.KeyValue);
        }

        //List<int> _pressedsKeys = new List<int>();
        public event EventHandler KeyPress;

        protected virtual void OnKeyPress(object? sender, EventArgs e)
        {
            KeyPress?.Invoke(this, e);
        }
    }
}
