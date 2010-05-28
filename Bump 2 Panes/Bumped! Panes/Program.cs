using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Bump_2_Panes.Diagnostics;
using System.Configuration;

namespace Bump_2_Panes
{
    static class Program
    {
        static frmBumpedPanes mf;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (ProcessRunning.isProcessRunning("BumpTop"))
            {
                mf = new frmBumpedPanes(args);

                SingleInstanceApplication.Run(mf, StartupNextInstanceHandler);
            }
        }

        static void StartupNextInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            if (e.CommandLine.Contains("-edit"))
            {
                e.BringToForeground = true;
                if (!mf.Visible)
                    mf.Show();
                else
                    mf.BringToFront();
            }
            else
            {
                List<string> commands = e.CommandLine.ToList();
                string command = commands.Single(com => com.Contains("-win"));
                if (!String.IsNullOrEmpty(command))
                    mf.ShowWindow(new IntPtr(Convert.ToInt32(command.Substring(5))));
            }
        }
    }
}
