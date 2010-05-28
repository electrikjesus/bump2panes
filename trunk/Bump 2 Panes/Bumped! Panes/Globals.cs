using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MinimizeCapture;

using Bump_2_Panes.Generics;

namespace Bump_2_Panes
{
    internal class Globals
    {
        internal static Boolean DesktopDirect = true;
        internal static String PileName = "Applications";
        internal static Boolean TitleBarWindows = true;
        internal static Boolean AllWindows = false;
        internal static float Scale = 1f;
        internal static TimeSpan timeBetweenNagScreens = new TimeSpan(0, 15, 0);
        internal static EventfulDictionary<IntPtr, Boolean> dictCurrentWindows = new EventfulDictionary<IntPtr, bool>();
        internal static WindowSnapCollection WindowCollection = new WindowSnapCollection();
    }
}
