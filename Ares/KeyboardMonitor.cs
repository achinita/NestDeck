using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
            m_GlobalHook.KeyUp += M_GlobalHook_KeyUp;
        }

        private void M_GlobalHook_KeyUp(object? sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyValue.ToString());
            if (e.KeyValue == 9) IsTabPressed = false;
            if (e.KeyValue == 164) IsAltPressed = false;
        }

        private void M_GlobalHook_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyValue == 9) IsTabPressed= true;
            if (e.KeyValue == 164) IsAltPressed= true;

         
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

    }
}   
