using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Bump_2_Panes.Diagnostics
{
    public class ProcessRunning
    {
        public static bool isProcessRunning(string name)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
