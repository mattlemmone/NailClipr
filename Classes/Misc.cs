using EliteMMO.API;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NailClipr.Classes
{
    public class Misc
    {
        #region Helpers
        public static void CheckUpdate()
        {
            string[] s = Environment.GetCommandLineArgs();
            Download(Structs.Downloads.UPDATER, true);

            //Not Launched by updater, so launch the updater and kill the app.
            if (s.Length < 2 || bool.Parse(s[1]) != true)
            {
                Process.Start(Structs.Downloads.UPDATER.title);
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                bool wasUpdated = bool.Parse(s[2]);
                if (wasUpdated)
                    UpdateComments();
            }
        }
        public static void Download(Structs.File file, bool checkExists = false)
        {
            if (checkExists && File.Exists(file.fullPath)) return;

            WebClient Client = new WebClient();
            try
            {
                string msg = "Downloading " + file.title + ".";
                DialogResult result = MessageBox.Show(new NativeWindow(), msg, "Downloading...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                    Client.DownloadFile(file.downloadUrl, file.fullPath);
                else
                    Process.GetCurrentProcess().Kill();
            }
            catch (WebException)
            {
                MessageBox.Show("Error downloading " + file.title + ".", "Download Error");
                Process.GetCurrentProcess().Kill();
            }
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
        public static string[] MatchToString(MatchCollection MatchColl)
        {
            string[] s = MatchColl.Cast<Match>()
                        .Select(m => m.Value)
                       .ToArray();
            return s;
        }
        public static float[] MidPoint(float A, float A1, float B, float B1)
        {
            float[] ret = { (A + A1) / 2, (B + B1) / 2 };
            return ret;
        }
        private static string ReturnGitResponse(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "application/json";
            httpWebRequest.UserAgent = "pls";

            int numChars = 500;
            char[] block = new char[numChars];
            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                sr.ReadBlock(block, 0, numChars);
            }
            string text = String.Join("", block);
            return text;
        }
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
        public static void SetVer()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Structs.App.ver = fvi.FileVersion;
        }
        public static void UpdateComments()
        {
            string text = ReturnGitResponse(Structs.Commit.URL);
            
            Regex dateRegex = new Regex(Structs.Commit.DATE_REGEX);
            MatchCollection dateMatch = dateRegex.Matches(text);
            string date = dateMatch[0].Groups["date"].Value;

            Regex msgRegex = new Regex(Structs.Commit.MESSAGE_REGEX);
            MatchCollection msgMatch = msgRegex.Matches(text);
            string msg = msgMatch[0].Groups["message"].Value;

            MessageBox.Show(msg, "Change Log v." + Structs.App.ver, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
