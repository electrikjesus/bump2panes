using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using MinimizeCapture;

using JsonFx.Json;
using Bump_2_Panes.ComInterop;
using Bump_2_Panes.Generics;
using Bump_2_Panes.Diagnostics;
using System.Configuration;

namespace Bump_2_Panes
{
    public partial class frmBumpedPanes : Form
    {
        private string DataDirectory = Environment.CurrentDirectory;
        private string AppDirectory;

        private Dictionary<string, object> widgetJson;
        private Dictionary<string, object> appIcon = new Dictionary<string,object>();

        private EventfulDictionary<IntPtr, bool> initialWindowLoad = new EventfulDictionary<IntPtr, bool>();
        private Dictionary<IntPtr, string> windowNames = new Dictionary<IntPtr, string>();

        public frmBumpedPanes(string[] args)
        {
            InitializeComponent();

            Globals.dictCurrentWindows.ChangedEvent += new EventfulDictionary<IntPtr, bool>.ChangedEventHandler(dictCurrentWindows_ChangedEvent);
            Globals.dictCurrentWindows.AddedEvent += new EventfulDictionary<IntPtr, bool>.AddedEventHandler(dictCurrentWindows_AddedEvent);
            Globals.dictCurrentWindows.LoadedEvent += new EventfulDictionary<IntPtr, bool>.LoadedEventHandler(dictCurrentWindows_LoadedEvent);
            Globals.dictCurrentWindows.RemovedEvent += new EventfulDictionary<IntPtr, bool>.RemovedEventHandler(dictCurrentWindows_RemovedEvent);

            if (args.Length < 1)
            {
                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
                AppDirectory = di.Parent.FullName;

                if (!AppDirectory.EndsWith("B2P.widget"))
                {
                    AppDirectory = AppDirectory + "\\B2P.widget";
                }

                if (!DataDirectory.EndsWith("data"))
                {
                    DataDirectory = DataDirectory + "\\data";
                }

                StreamReader sr = File.OpenText(AppDirectory + "\\widget.json");
                string widget = sr.ReadToEnd();
                sr.Close();

                widget = stripComments(widget);

                JsonReader jr = new JsonReader(widget);

                widgetJson = jr.Deserialize() as Dictionary<string, object>;

                appIcon.Add("filename", "Bump 2 Panes.png");
                appIcon.Add("label", "Bump 2 Panes");
                Dictionary<string, object> applicationSettings = new Dictionary<string, object>();
                applicationSettings.Add("path", "Bump 2 Panes.exe");
                applicationSettings.Add("args", "-edit");
                appIcon.Add("launchApplication", applicationSettings);
                appIcon.Add("scale", Convert.ToSingle(1));

                User32.EnumWindows(InitialCallback, 0);

                FileInfo file = new FileInfo(AppDirectory + "\\settings.json");
                if (file.Exists)
                    ReadSettings();
            }
        }

        void SaveSettings()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("desktopDirect", Globals.DesktopDirect);
            settings.Add("pileName", Globals.PileName);
            settings.Add("titleBarWindows", Globals.TitleBarWindows);
            settings.Add("allWindows", Globals.AllWindows);
            settings.Add("scale", Globals.Scale);

            Boolean fileopened = true;
            StreamWriter sw = null;
            while (fileopened)
            {
                try
                {
                    sw = new StreamWriter(AppDirectory + "\\settings.json", false, Encoding.ASCII);
                    fileopened = false;
                }
                catch (IOException exc)
                {
                    fileopened = true;
                }
            }
            JsonWriter jw = new JsonWriter(sw);
            jw.PrettyPrint = true;
            jw.Write(settings);
            sw.Close();
        }

        void ReadSettings()
        {
            StreamReader sr = new StreamReader(AppDirectory + "\\settings.json");
            JsonReader jsonReader = new JsonReader(sr);
            sr.Close();

            Dictionary<string, object> settings = jsonReader.Deserialize() as Dictionary<string, object>;
            Globals.DesktopDirect = Convert.ToBoolean(settings["desktopDirect"]);
            Globals.PileName = settings["pileName"].ToString();
            Globals.TitleBarWindows = Convert.ToBoolean(settings["titleBarWindows"]);
            Globals.AllWindows = Convert.ToBoolean(settings["allWindows"]);
            Globals.Scale = Convert.ToSingle(settings["scale"]);

            if (Globals.DesktopDirect)
            {
                rbDesktop.Checked = true;
                rbPiled.Checked = false;
            }
            else
            {
                rbDesktop.Checked = false;
                rbPiled.Checked = true;
            }

            txtPileName.Text = Globals.PileName;

            //if (Globals.TitleBarWindows)
            //{
            //    rbMinimizeToDesktopAndTaskbar.Checked = true;
            //    rbMinimizeToDesktop.Checked = false;
            //}
            //else
            //{
            //    rbMinimizeToDesktopAndTaskbar.Checked = false;
            //    rbMinimizeToDesktop.Checked = true;
            //}

            chkAllWindows.Checked = Globals.AllWindows;

            tbScale.Value = Convert.ToInt32(Globals.Scale * 10);
            lblScale.Text = String.Format("{0}x Scale", Globals.Scale.ToString());
            
            this.Hide();
        }

        void dictCurrentWindows_RemovedEvent(EventfulDictionary<IntPtr, bool>.DictionaryEventArgs removedEventArgs)
        {
            FileInfo file;
            if (Globals.DesktopDirect)
                file = new FileInfo(DataDirectory + "\\" + removedEventArgs.Key.ToString() + ".png");
            else
                file = new FileInfo(DataDirectory + "\\" + Globals.PileName + "\\" + removedEventArgs.Key.ToString() + ".png");

            file.Delete();

            SaveNewJson();
        }

        public void ShowWindow(IntPtr hWnd)
        {
            User32.ShowWindow(hWnd, Convert.ToInt32(User32.WindowStyle.Restore));
        }

        private Boolean InitialCallback(IntPtr hWnd, IntPtr lParams)
        {
            if (User32.IsWindowVisible(hWnd.ToInt32()))
            {
                initialWindowLoad.Add(hWnd, User32.IsIconic(hWnd));
                StringBuilder sb = new StringBuilder(200);
                User32.GetWindowText(hWnd.ToInt32(), sb, sb.Capacity);
                windowNames.Add(hWnd, sb.ToString());
            }

            return true;
        }

        private void filterHWndNames()
        {
            foreach (IntPtr hWnd in windowNames.Keys)
            {
                if (Exclusions.ExcludedList.Contains(windowNames[hWnd]) || String.IsNullOrEmpty(windowNames[hWnd].Trim()))
                    initialWindowLoad.Remove(hWnd);
            }
        }

        void dictCurrentWindows_AddedEvent(EventfulDictionary<IntPtr, bool>.DictionaryEventArgs addedEventArgs)
        {
            if (addedEventArgs.Value)
            {
                WindowSnap window = WindowSnap.GetWindowSnap(addedEventArgs.Key, true);
                if (Globals.DesktopDirect)
                    window.Thumbnail.Save(DataDirectory + "\\" + addedEventArgs.Key.ToString() + ".png", ImageFormat.Png);
                else
                    window.Thumbnail.Save(DataDirectory + "\\" + Globals.PileName + "\\" + addedEventArgs.Key.ToString() + ".png", ImageFormat.Png);
            }
            else
            {
                if (Globals.AllWindows)
                {
                    WindowSnap window = WindowSnap.GetWindowSnap(addedEventArgs.Key, false);
                    if (Globals.DesktopDirect)
                        window.Thumbnail.Save(DataDirectory + "\\" + addedEventArgs.Key.ToString() + ".png", ImageFormat.Png);
                    else
                        window.Thumbnail.Save(DataDirectory + "\\" + Globals.PileName + "\\" + addedEventArgs.Key.ToString() + ".png", ImageFormat.Png);
                }
            }

            SaveNewJson();
        }

        void dictCurrentWindows_ChangedEvent(EventfulDictionary<IntPtr, bool>.DictionaryEventArgs pChangedEventArgs)
        {
            if (pChangedEventArgs.Value)
            {
                WindowSnap window = WindowSnap.GetWindowSnap(pChangedEventArgs.Key, true);
                if (Globals.DesktopDirect)
                    window.Thumbnail.Save(DataDirectory + "\\" + pChangedEventArgs.Key.ToString() + ".png", ImageFormat.Png);
                else
                    window.Thumbnail.Save(DataDirectory + "\\" + Globals.PileName + "\\" + pChangedEventArgs.Key.ToString() + ".png", ImageFormat.Png);
            }
            else
            {
                if (!Globals.AllWindows)
                {
                    FileInfo file;
                    if (Globals.DesktopDirect)
                        file = new FileInfo(DataDirectory + "\\" + pChangedEventArgs.Key.ToString() + ".png");
                    else
                        file = new FileInfo(DataDirectory + "\\" + Globals.PileName + "\\" + pChangedEventArgs.Key.ToString() + ".png");

                    file.Delete();
                }
            }

            SaveNewJson();
        }

        void dictCurrentWindows_LoadedEvent(EventfulDictionary<IntPtr, bool>.DictionaryEventArgs pLoadedEventArgs)
        {
            foreach (IntPtr hWnd in pLoadedEventArgs.Dict.Keys)
            {
                if (!Globals.AllWindows)
                {
                    if (User32.IsIconic(hWnd))
                    {
                        WindowSnap window = WindowSnap.GetWindowSnap(hWnd, true);
                        if (Globals.DesktopDirect)
                            window.Thumbnail.Save(DataDirectory + "\\" + hWnd + ".png", ImageFormat.Png);
                        else
                            window.Thumbnail.Save(DataDirectory + "\\" + Globals.PileName + "\\" + hWnd.ToString() + ".png", ImageFormat.Png);
                    }
                }
                else
                {
                    WindowSnap window;

                    if (User32.IsIconic(hWnd))
                        window = WindowSnap.GetWindowSnap(hWnd, true);
                    else
                        window = WindowSnap.GetWindowSnap(hWnd, false);

                    if (Globals.DesktopDirect)
                        window.Thumbnail.Save(DataDirectory + "\\" + hWnd + ".png", ImageFormat.Png);
                    else
                        window.Thumbnail.Save(DataDirectory + "\\" + Globals.PileName + "\\" + hWnd.ToString() + ".png", ImageFormat.Png);
                }
            }

            SaveNewJson();
        }

        private void SaveNewJson()
        {
            List<Dictionary<string, object>> icons = new List<Dictionary<string, object>>();

            icons.Add(appIcon);

            foreach (IntPtr hWnd in Globals.dictCurrentWindows.Keys)
            {
                if (Globals.AllWindows)
                {
                    icons.Add(GetIconFromHWnd(hWnd));
                }
                else
                {
                    if (Globals.dictCurrentWindows[hWnd])
                    {
                        icons.Add(GetIconFromHWnd(hWnd));
                    }
                }

                SaveNewIconList(icons);
            }
        }

        private Dictionary<string, object> GetIconFromHWnd(IntPtr hWnd)
        {
            Dictionary<string, object> icon = new Dictionary<string, object>();
            string filename;

            if (Globals.DesktopDirect)
                filename = hWnd.ToString() + ".png";
            else
                filename = Globals.PileName + "\\" + hWnd.ToString() + ".png";

            icon.Add("filename", filename);
            StringBuilder sb = new StringBuilder(200);
            User32.GetWindowText(hWnd.ToInt32(), sb, sb.Capacity);
            icon.Add("label", sb.ToString());
            Dictionary<string, object> applicationSettings = new Dictionary<string, object>();
            applicationSettings.Add("path", "Bump 2 Panes.exe");
            applicationSettings.Add("args", "-win:" + hWnd.ToString());
            icon.Add("launchApplication", applicationSettings);
            icon.Add("scale", Globals.Scale);

            return icon;
        }

        private string stripComments(string text)
        {
            if (text.Contains("//"))
            {
                int commentBeginIndex = text.IndexOf("//");
                if (commentBeginIndex > 5)
                {
                    if (text.Substring(commentBeginIndex - 5, 5) != "http:")
                    {
                        int commentEndIndex = text.IndexOf("\r\n", commentBeginIndex);
                        text = text.Remove(commentBeginIndex, commentEndIndex - commentBeginIndex);
                    }
                    else
                    {
                        text = text.Insert(commentBeginIndex + 1, "d");
                    }
                }
                else
                {
                    int commentEndIndex = text.IndexOf("\r\n", commentBeginIndex);
                    text = text.Remove(commentBeginIndex, commentEndIndex - commentBeginIndex);
                }

                return stripComments(text);
            }
            else
            {
                return text.Replace("/d/", "//");
            }
        }

        private void TimerFunction()
        {
            if (!ProcessRunning.isProcessRunning("BumpTop"))
                Application.Exit();
            initialWindowLoad.Clear();
            User32.EnumWindows(WindowCallback, 0);
            Dictionary<IntPtr, bool> missing = DictionaryComparer<IntPtr, bool>.CompareSimpleDictionary(Globals.dictCurrentWindows, initialWindowLoad);
            foreach (IntPtr key in missing.Keys)
            {
                Globals.dictCurrentWindows.Remove(key);
            }

            foreach (IntPtr hWnd in initialWindowLoad.Keys)
            {
                Boolean minimized = User32.IsIconic(hWnd);

                if (Globals.dictCurrentWindows.ContainsKey(hWnd))
                {
                    if (Globals.dictCurrentWindows[hWnd] != minimized)
                    {
                        Globals.dictCurrentWindows.Change(hWnd, minimized);
                    }
                }
                else
                {
                    Globals.dictCurrentWindows.Add(hWnd, minimized);
                }
            }
        }

        private Boolean WindowCallback(IntPtr hWnd, IntPtr lParams)
        {
            if (User32.IsWindowVisible(hWnd.ToInt32()))
            {
                StringBuilder sb = new StringBuilder(200);
                User32.GetWindowText(hWnd, sb, sb.Capacity);
                if (Exclusions.ExcludedList.Contains(sb.ToString()) || String.IsNullOrEmpty(sb.ToString().Trim()))
                    return true;
                initialWindowLoad.Add(hWnd, User32.IsIconic(hWnd));
            }

            return true;
        }

        private List<Dictionary<string, object>> GetIconList()
        {
            if (widgetJson.ContainsKey("widget"))
            {
                Dictionary<string, object> iconsObject = widgetJson["widget"] as Dictionary<string, object>;

                if (iconsObject.ContainsKey("icons"))
                {
                    return iconsObject["icons"] as List<Dictionary<string, object>>;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void SaveNewIconList(List<Dictionary<string, object>> icons)
        {
            if (widgetJson.ContainsKey("widget"))
            {
                Dictionary<string, object> iconsObject = widgetJson["widget"] as Dictionary<string, object>;

                if (iconsObject.ContainsKey("icons"))
                {
                    iconsObject["icons"] = icons;
                    widgetJson["widget"] = iconsObject;
                }
                else
                {
                    MessageBox.Show("icons not found in widget.json");
                }
            }
            else
            {
                MessageBox.Show("widget not found in widget.json");
            }

            Boolean fileopened = true;
            StreamWriter sw = null;
            while (fileopened)
            {
                try
                {
                    sw = new StreamWriter(AppDirectory + "\\widget.json", false, Encoding.ASCII);
                    fileopened = false;
                }
                catch (IOException exc)
                {
                    fileopened = true;
                }
            }
            JsonWriter jw = new JsonWriter(sw);
            jw.PrettyPrint = true;
            jw.Write(widgetJson);
            sw.Close();
        }

        private void tmeTick_Tick(object sender, EventArgs e)
        {
            TimerFunction();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Run")
            {
                btnRun.Text = "Stop";
                this.Hide();
                filterHWndNames();
                Globals.dictCurrentWindows.Load(initialWindowLoad);
                TimerFunction();
                tmeTick.Enabled = true;

                SaveSettings();
            }
            else
            {
                btnRun.Text = "Run";
                tmeTick.Enabled = false;
            }
        }

        private void rbDesktop_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDesktop.Checked)
            {
                if (!Globals.DesktopDirect)
                {
                    DirectoryInfo di = new DirectoryInfo(DataDirectory + "\\" + Globals.PileName + "\\");
                    if (di.Exists)
                        foreach (FileInfo fi in di.GetFiles())
                        {
                            fi.Delete();
                        }
                    List<Dictionary<string, object>> clearJson = new List<Dictionary<string, object>>();
                    clearJson.Add(appIcon);
                    SaveNewIconList(clearJson);
                    tmeDeleteDirectory.Enabled = true;
                }
                Globals.DesktopDirect = true;
                if (tmeTick.Enabled)
                {
                    Globals.dictCurrentWindows.Load(initialWindowLoad);
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(DataDirectory + "\\" + Globals.PileName + "\\");
                if (!di.Exists)
                    di.Create();
                di = new DirectoryInfo(DataDirectory);
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Name != "Bump 2 Panes.png")
                    {
                        fi.Delete();
                    }
                }
                Globals.DesktopDirect = false;
                if (tmeTick.Enabled)
                {
                    Globals.dictCurrentWindows.Load(initialWindowLoad);
                }
            }

            SaveSettings();
        }

        private void txtPileName_TextChanged(object sender, EventArgs e)
        {
            if (txtPileName.Text != Globals.PileName)
            {
                DirectoryInfo di = new DirectoryInfo(DataDirectory + "\\" + Globals.PileName);
                if (di.Exists)
                {
                    di.MoveTo(DataDirectory + "\\" + txtPileName.Text);
                }
                Globals.PileName = txtPileName.Text;
                if (tmeTick.Enabled)
                {
                    SaveNewJson();
                }

                SaveSettings();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FormClosingEventArgs closeEvent = new FormClosingEventArgs(CloseReason.UserClosing, true);
            frmBumpedPanes_FormClosing(sender, closeEvent);
            SaveSettings();
        }

        private void frmBumpedPanes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void frmBumpedPanes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32.ReleaseCapture();
                User32.SendMessage(this.Handle, 0xa1, 0x2, 0);
            }
        }

        private void chkAllWindows_CheckedChanged(object sender, EventArgs e)
        {
            Globals.AllWindows = chkAllWindows.Checked;
            SaveSettings();
        }

        private void tmeDeleteDirectory_Tick(object sender, EventArgs e)
        {
            tmeDeleteDirectory.Enabled = false;
            DirectoryInfo di = new DirectoryInfo(DataDirectory + "\\" + Globals.PileName + "\\");
            di.Delete();
        }

        private void tbScale_Scroll(object sender, EventArgs e)
        {
            Globals.Scale = Convert.ToSingle(tbScale.Value) / 10;
            lblScale.Text = String.Format("{0}x Scale", Globals.Scale.ToString());
            SaveSettings();
        }

        //private void rbMinimizeToDesktopAndTaskbar_CheckedChanged(object sender, EventArgs e)
        //{
        //    Globals.TitleBarWindows = rbMinimizeToDesktopAndTaskbar.Checked;
        //    SaveSettings();
        //}
    }
}
