// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Launcher
// LAST MODIFICATION: 24/04/2014 @ 20:23
// **********************************************

namespace Updater
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Simple and primitive game launcher.
    /// </summary>
    public static class Launcher
    {
        // Kernel32: Create Process - http://www.pinvoke.net/default.aspx/kernel32/createprocess.html

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CreateProcess(string lpApplicationName,
           string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
           ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles,
           uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
           [In] ref STARTUPINFO lpStartupInfo,
           out PROCESS_INFORMATION lpProcessInformation);
        
        /// <summary>
        /// Runs the 
        /// </summary>
        public static void Run()
        {
            const uint NORMAL_PRIORITY_CLASS = 0x0020;
            string Path = Environment.CurrentDirectory + @"\" + (Program.projectName.ToString() == "Ultimatium" ? "BlackOpsMP.exe" : "iw5mp.exe");
            string Arguments = Program.Arguments;
            PROCESS_INFORMATION pInfo = new PROCESS_INFORMATION();
            STARTUPINFO sInfo = new STARTUPINFO();
            SECURITY_ATTRIBUTES pSec = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES tSec = new SECURITY_ATTRIBUTES();
            pSec.nLength = Marshal.SizeOf(pSec);
            tSec.nLength = Marshal.SizeOf(tSec);
            bool Initialized = CreateProcess(Path, Arguments,
            ref pSec, ref tSec, false, NORMAL_PRIORITY_CLASS,
            IntPtr.Zero, null, ref sInfo, out pInfo);
            if (Initialized)
            {
                Log.Write("Launching " + Program.projectName + "...");
                Log.Write("Successful launch. PID: " + pInfo.dwProcessId);
                Log.Write("Happy gaming!");
                Environment.Exit(0x0);
            }
            else
            {
                Log.Write("ERROR: Could not launch " + Program.projectName + ". Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0x3);
            }
        }
    }
}
