using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Updater
{
    class Program
    {

        public const string appName = "NailClipr",
            url = "https://github.com/mattlemmone/NailClipr/raw/master/bin/Release/NailClipr.exe";

        public string fullPath = AppDomain.CurrentDomain.BaseDirectory + appName;

        private static string GetStringFromUrl(string location)
        {
            WebRequest request = WebRequest.Create(location);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }

        public static void CheckUpdate()
        {
            //Get Main files if they don't exist.
            Download(Structs.Downloads.API_DLL, true);
            Download(Structs.Downloads.MMO_DLL, true);
            Download(Structs.Downloads.APP, true);

            Directory.CreateDirectory("Resources");
            Download(Structs.Downloads.AREAS, true);

            //Check Versions
            Structs.File API_DLL = Structs.Downloads.API_DLL;
            Structs.File MMO_DLL = Structs.Downloads.MMO_DLL;
            Structs.File APP = Structs.Downloads.APP;

            CheckVersion(API_DLL);
            CheckVersion(MMO_DLL);
            CheckVersion(APP);
        }

        private static void CheckVersion(Structs.File file)
        {

            //Assign version
            file.ver = FileVersionInfo.GetVersionInfo(file.fullPath).FileVersion;
            if (file != Structs.Downloads.APP)
                file.expectedVer = GetStringFromUrl(file.verUrl);
            else
                file.expectedVer = Regex.Replace(GetStringFromUrl(file.verUrl), @"\t|\n|\r", "");

            string
            fileVer = String.Join("", file.ver.Split('.')),
            expVer = String.Join("", file.expectedVer.Split('.'));

            int
            num_fileVer = int.Parse(fileVer),
            num_expVer = int.Parse(expVer);

            if (num_fileVer < num_expVer) Download(file);

        }

        public static void Download(Structs.File file, bool checkExists = false)
        {

            if (checkExists && File.Exists(file.fullPath)) return;
            WebClient Client = new WebClient();
            try
            {
                string msg = (!string.IsNullOrEmpty(file.expectedVer)) ? "Downloading " + file.title + " v." + file.expectedVer + "." : "Downloading " + file.title + ".";
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

        private static void Launch()
        {
            Console.WriteLine("Launching " + appName + ".exe...");
            Process.Start(appName + ".exe", "updated");
            Process.GetCurrentProcess().Kill();
        }

        static void Main(string[] args)
        {
            CheckUpdate();
            Launch();
        }

        public struct Structs
        {
            public class Downloads
            {
                public static File APP = new File(
                     appName + ".exe",
                    "https://github.com/mattlemmone/NailClipr/raw/master/bin/Release/NailClipr.exe",
                    "https://raw.githubusercontent.com/mattlemmone/NailClipr/master/ver.txt"
                );
                public static File API_DLL = new File(
                    "EliteAPI.dll",
                    "http://ext.elitemmonetwork.com/downloads/eliteapi/EliteAPI.dll",
                    "http://ext.elitemmonetwork.com/downloads/eliteapi/index.php?v"
                );

                public static File MMO_DLL = new File(
                    "EliteMMO.API.dll",
                    "http://ext.elitemmonetwork.com/downloads/elitemmo_api/EliteMMO.API.dll",
                    "http://ext.elitemmonetwork.com/downloads/elitemmo_api/index.php?v"
                );
                public static File AREAS = new File(
                    "areas.xml",
                    "https://github.com/mattlemmone/NailClipr/raw/master/bin/Release/Resources/areas.xml",
                    "",
                    true
                );
            }
            public class File
            {
                public string
                title,
                downloadUrl,
                fullPath,
                expectedVer,
                ver,
                verUrl;

                public File(string t, string down, string verU)
                {
                    title = t;
                    downloadUrl = down;
                    verUrl = verU;
                    fullPath = fullPath + t;
                }
                public File(string t, string down, string verU, bool isResource = false)
                {
                    title = t;
                    downloadUrl = down;
                    verUrl = verU;
                    if (isResource)
                        fullPath = fullPath + @"Resources\" + t;
                    else
                        fullPath = fullPath + t;
                }

            }
        }

    }
}
