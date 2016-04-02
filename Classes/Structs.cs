using EliteMMO.API;
using System;
using System.Collections.Generic;
using NailClipr.Classes;

namespace NailClipr
{
    public class Structs
    {
        public static Zone Zones = new Zone();
        public static Settings settings = new Settings();
        public static List<WarpPoint> warpPoints = new List<WarpPoint>();
        public static List<WarpPoint> zonePoints = new List<WarpPoint>();
        public static List<Zone> zoneList = new List<Zone>();

        public struct App
        {
            public static string name = "NailClipr";
            private static int[] v = { 1, 3, 1};
            public static string ver = string.Join(".", v);
        }
        public struct Chat
        {
            public static bool loaded;
            public struct Warp
            {
                public static string acceptNotify = "i accept <:'^)";
                public static string acceptSelfNotify = "Accepted.";
                public static string arrivedNotify = "Arrived.";
                public static string warmupNotify = "Warming up...";

                public const string senderRegEx = @"\(([A-Za-z]+)\)";
                public const string coordRegEx = @"[^\[\d:d\]](\-*\d*\.*\d+)+";
                public const int expectedNumCoords = 4;
            }
            public class Controller
            {
                public const string echoRegex = @"([^\s])+";
                public const string
                    //Single param calls
                    accept = "acc",
                    request = "req",
                    maintenance = "m",
                    delWarp = "del",
                    warp = "w",
                    curWarp = "cur",
                    abort = "abrt",

                    //Two param calls
                    saveWarp = "sve",
                    search = "sea",
                    speed = "s";

                public static Dictionary<string, Action<EliteAPI>> dictOneParam =
                new Dictionary<string, Action<EliteAPI>>
                {
                    {accept, SharedFunctions.Accept },
                    {request, SharedFunctions.Request },
                    {abort, SharedFunctions.Abort },
                    {maintenance, SharedFunctions.MaintenanceToggle },
                    {delWarp, SharedFunctions.DelWarp },
                    {warp, SharedFunctions.Warp },
                    {curWarp, SharedFunctions.GetWarp }
                };

                /*
               acc -> Accept
               req -> Request
               abrt -> Abort
               m -> Maintenance toggle
               save -> Save Warp
               del -> Delete Warp
               w -> Warp to Current Selection
               cur -> Get Current Selection

               Variables
               sea ... -> Search ...
               s ... -> Set Speed
                   + -> + 0.5
                   - -> - 0.5
               */
            }
        }
        public class Commit
        {
            string url = "https://api.github.com/repos/mattlemmone/NailClipr/commits";
        }
        public struct Error
        {
            public struct Auth
            {
                public static string title = "Unauthorized.";
                public static string text = "You do not have permission to use this program.";
            }
            public struct Exit
            {
                public static string title = "pol.exe not detected";
                public static string text = "Launch FFXI before prior to launching this program.";
            }
            public struct Warp
            {
                public static string parse = "Error parsing request.";
            }
            public struct Other
            {
                public static string StatusEasterEgg = "Unsupported status type.";
            }
        }
        public class File
        {
            public string title;
            public string url;

            public File(string t, string u)
            {
                title = t;
                url = u;
            }
        }
        public struct FFXI
        {
            public class Name
            {
                public const int MAXLENGTH = 15;
                public const int MINLENGTH = 3;
            }
        }
        public struct PC
        {
            public string name;
            public float distance;
        }
        public struct Position
        {
            public float X;
            public float Y;
            public float Z;
            public int Zone;
        }
        public struct Search
        {
            public static string idle = "idle";
            public static string success = "success!";
            public static string searching = "searching...";
        }
        public struct Settings
        {
            public bool topMostForm;
            public bool playerDetection;
            public const float POS_INC = 5f;
        }
        public class Speed
        {
            public const float NATURAL = 5f;
            public const float DIVISOR = 4f;
            public const float MAX_MULT = 1.5f;
            public const float MAX = 7.5f;
            public static List<String> whitelist;
            public static void PreventOverWrite(EliteAPI api)
            {
                //Adjust current speed.
                if (!Player.isAlone && settings.playerDetection)
                {
                    api.Player.Speed = Player.Speed.normal;
                    if (api.Player.Speed != Player.Speed.normal)
                        api.Player.Speed = Player.Speed.normal;
                }
                else {
                    //Prevent speed overwrite.
                    if (api.Player.Speed != Player.Speed.expected)
                        api.Player.Speed = Player.Speed.expected;
                }
            }
        }
        public class Status
        {
            public const uint NATURAL = 0;
            public const uint MAINT = 31;
            public static void PreventOverwrite(EliteAPI api)
            {
                if (NailClipr.GUI_MAINT.Checked == true || Player.isWarping)
                {
                    if (api.Player.Status != Structs.Status.MAINT)
                        api.Player.Status = Structs.Status.MAINT;
                }
            }
        }
        public class Update
        {
            public static File Updater = new File("Updater.exe", "https://github.com/mattlemmone/NailClipr/raw/master/bin/Release/Updater.exe");
            public const string ver = "https://raw.githubusercontent.com/mattlemmone/NailClipr/master/ver.txt";
            public static File API_DLL = new File("EliteAPI.dll", "http://ext.elitemmonetwork.com/downloads/eliteapi/index.php?v");
            public static File MMO_DLL = new File("EliteMMO.API.dll", "http://ext.elitemmonetwork.com/downloads/elitemmo_api/index.php?v");
        }
        public struct WarpPoint
        {
            public string title;
            public Position pos;
            public int zone;
        }
        public class Zone
        {
            public int id;
            public string name;
            public string NameFromID(int id)
            {
                int zIndex = zoneList.FindIndex(z => z.id == id);
                return zoneList[zIndex].name;
            }
            public int IDfromName(string name)
            {
                int zIndex = zoneList.FindIndex(z => z.name == name);
                return zoneList[zIndex].id;
            }
        }

    }
}
