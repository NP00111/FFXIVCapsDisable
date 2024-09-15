using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FFXIV_Disable_Caps
{
    public partial class Options : Form
    {

        #region Global Declarations

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

        #endregion

        #region Form Functions
        public Options()
        {
            InitializeComponent();

            using (ProcessModule mainModule = Process.GetCurrentProcess().MainModule)
            {
                objKeyboardProcess = new LowLevelKeyboardProc(CaptureKeyPress);
                ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(mainModule.ModuleName), 0U);
            }
        }

        private void Options_Load(object sender, EventArgs e)
        {
            ReadStartup();
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(ptrHook);
        }

        private void Options_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.TrayIcon.Visible = true;
                this.Hide();
            }
            else
                this.TrayIcon.Visible = false;
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        #region KeyPress Functions
        public IntPtr CaptureKeyPress(int nCode, IntPtr wp, IntPtr lp)
        {
            if (Gamerunning && nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                if (objKeyInfo.key == Keys.Capital)
                    return (IntPtr)1;
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

        private void timert_check_process(object sender, EventArgs e)
        {
            CheckProcess();
        }

        #endregion

        #region Registry Functions
        private void SetStartup()
        {
            string AppName = "FFXIV Caps Lock Disabler";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkAutoStart.Checked)
                rk.SetValue(AppName, Application.ExecutablePath);
            else
                rk.DeleteValue(AppName, false);

        }
        private void ReadStartup()
        {
            string AppName = "FFXIV Caps Lock Disabler";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            chkAutoStart.Checked = (rk.GetValue(AppName) == null) ? false : true;
        }
        private void chkAutoStart_Click(object sender, EventArgs e)
        {
            SetStartup();
        }

        #endregion
    }
}
