﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shutdown
{
    class C_Alimentation
    {
        [DllImport("advapi32.dll")]
        static extern int OpenProcessToken(IntPtr ProcessHandle,
                     int DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            UInt32 BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);

        [DllImport("advapi32.dll")]
        static extern int LookupPrivilegeValue(string lpSystemName,
                               string lpName, out LUID lpLuid);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int ExitWindowsEx(uint uFlags, uint dwReason);

        public void Shutdown()
        {
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            const short SE_PRIVILEGE_ENABLED = 2;
            const uint EWX_SHUTDOWN = 1;
            const short TOKEN_ADJUST_PRIVILEGES = 32;
            const short TOKEN_QUERY = 8;
            IntPtr hToken;
            TOKEN_PRIVILEGES tkp;

            // Get shutdown privileges...
            OpenProcessToken(Process.GetCurrentProcess().Handle,
                TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken);
            tkp.PrivilegeCount = 1;
            tkp.Privileges.Attributes = SE_PRIVILEGE_ENABLED;
            LookupPrivilegeValue("", SE_SHUTDOWN_NAME, out tkp.Privileges.pLuid);
            AdjustTokenPrivileges(hToken, false, ref tkp, 0U, IntPtr.Zero,
                  IntPtr.Zero);

            // Now we have the privileges, shutdown Windows
            ExitWindowsEx(EWX_SHUTDOWN, 0);
        }

        // Structures needed for the API calls
        private struct LUID
        {
            public int LowPart;
            public int HighPart;
        }
        private struct LUID_AND_ATTRIBUTES
        {
            public LUID pLuid;
            public int Attributes;
        }
        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public LUID_AND_ATTRIBUTES Privileges;
        }
    }
}
