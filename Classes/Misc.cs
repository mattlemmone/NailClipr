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
                //App is up to date.
                bool wasUpdated = bool.Parse(s[2]);
                if (wasUpdated)
                    UpdateComments();

                //Get resource files if they don't exist.
                Directory.CreateDirectory("Resources");
                Download(Structs.Downloads.AREAS, true);
                Download(Structs.Downloads.ITEMS, true);
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
        public static DateTime FromUnixTime(long unixTime)
        {
            DateTime referenceTimeZero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return referenceTimeZero.AddSeconds(unixTime);
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
        public static void OpenURL(string url)
        {
            Process.Start(url);
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
            string text = ReturnGitResponse(Structs.Commit.URL),
            date = RegExMatch(text, Structs.Commit.DATE_REGEX),
            msg = RegExMatch(text, Structs.Commit.MESSAGE_REGEX);
            msg = msg.Replace(@"\n", Environment.NewLine);

            MessageBox.Show(msg, "Change Log v." + Structs.App.ver, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static string RegExMatch(string text, string pattern, int index = 0, string group = "match")
        {
            Regex r = new Regex(pattern);
            MatchCollection match = r.Matches(text);
            return match.Count > 0 ? match[index].Groups[group].Value : "null";
        }
        public static MatchCollection RegExMatches(string text, string pattern)
        {
            Regex r = new Regex(pattern);
            return r.Matches(text);
        }
        #endregion
    }
}
