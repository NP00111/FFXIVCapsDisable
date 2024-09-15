using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;


namespace FFXIV_Disable_Caps
{
    public partial class Options : Form
    {

        private const int WH_KEYBOARD_LL = 13;
        private const int VK_CONTROL = 17;
        private static bool Gamerunning;

        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, Options.LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        public Options()
        {
            InitializeComponent();

            using (ProcessModule mainModule = Process.GetCurrentProcess().MainModule)
            {
                objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
                ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(mainModule.ModuleName), 0U);
            }
        }

        public IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (Gamerunning && nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                if (objKeyInfo.key == Keys.Capital)
                {
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        private void CheckProcess()
        {
            if (Process.GetProcessesByName("ffxiv_dx11").Length == 0)
                Gamerunning = false;
            else
                Gamerunning = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckProcess();
        }
        private void Options_Load(object sender, EventArgs e)
        {

        }



    }
}
