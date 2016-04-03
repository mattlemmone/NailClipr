using EliteMMO.API;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace NailClipr.Classes
{
    public class Misc
    {
        #region Helpers
        public static EliteAPI SelectProcess(EliteAPI api)
        {
            #region Final Fantasy XI [POL]
            var data = Process.GetProcessesByName("pol");

            if (data.Count() != 0)
            {
                var proc = Process.GetProcessesByName("pol").First().Id;
                api = new EliteAPI(proc);
                string p = api.Entity.GetLocalPlayer().Name;
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://github.com/mattlemmone/NailClipr/raw/master/auth.txt");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();

                if (content.Length == 0 || p.Length == 0 || !content.Contains(p))
                {
                    MessageBox.Show(Structs.Error.Auth.text, Structs.Error.Auth.title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ExitApp(api);
                }
                Structs.Speed.whitelist = content.Split(',').ToList();
                return api;

            }
            else
            {
                MessageBox.Show(Structs.Error.Exit.text, Structs.Error.Exit.title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitApp(api);
                return null;
            }
            #endregion
        }
        public static void CheckUpdate()
        {

            if (!File.Exists(Application.StartupPath + @"\" + Structs.Downloads.UPDATER.title))
            {
                Download(Structs.Downloads.UPDATER.title, Structs.Downloads.UPDATER.url);
            }

            Process.Start(Application.StartupPath + @"\" + Structs.Downloads.UPDATER.title);
            Process.GetCurrentProcess().Kill();
        }
        public static void UpdateComments()
        {
            //TODO
        }
        public static string GetStringFromUrl(string location)
        {
            WebRequest request = WebRequest.Create(location);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }
        public static void ExitApp(EliteAPI api)
        {
            api.Player.Speed = Player.Speed.normal;
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
        public static void Download(string title, string url)
        {
            WebClient Client = new WebClient();
            try
            {
                DialogResult result = MessageBox.Show("Downloading " + title + ".", "Downloading...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                    Client.DownloadFile(url, Application.StartupPath + @"\" + title);
                else
                    Process.GetCurrentProcess().Kill();
            }
            catch (WebException)
            {
                MessageBox.Show("Error downloading " + title + ".", "Download Error");
                Process.GetCurrentProcess().Kill();
            }
        }
        public static float[] MidPoint(float A, float A1, float B, float B1)
        {
            float[] ret = { (A + A1) / 2, (B + B1) / 2 };
            return ret;
        }
        #endregion
    }
}
