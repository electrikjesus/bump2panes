using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace Bump_2_Panes
{
    public class SingleInstanceApplication : WindowsFormsApplicationBase
    {
        private SingleInstanceApplication()
        {
            base.IsSingleInstance = true;
            base.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
            base.EnableVisualStyles = true;
        }

        public static void Run(Form f, StartupNextInstanceEventHandler startupHandler)
        {
            SingleInstanceApplication app = new SingleInstanceApplication();
            app.StartupNextInstance += startupHandler;

            Rectangle scrn = Screen.GetWorkingArea(f);
            f.Location = new Point((scrn.Width - f.Width) / 2, (scrn.Height - f.Height) / 2);

            app.MainForm = f;
            app.Run(Environment.GetCommandLineArgs());
        }
    }
}
