using EliteMMO.API;
using System;
using System.Collections.Generic;
using NailClipr.Classes;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace NailClipr
{
    public class Structs
    {
        public static Settings settings = new Settings();
        public static List<WarpPoint> warpPoints = new List<WarpPoint>();
        public static List<WarpPoint> zonePoints = new List<WarpPoint>();
        public static List<Zone> zoneList = new List<Zone>();
        public static Zone Zones = new Zone();

        public struct App
        {
            public static string name = "NailClipr";
            public static string ver;
            public static bool updated;
        }        
        public struct Commit
        {
            public const string
            URL = "https://api.github.com/repos/mattlemmone/NailClipr/commits",
            DATE_REGEX = "\"date\":\"(?<date>[A-z0-9\\-\\:]+)",
            MESSAGE_REGEX = "\"message\":\"(?<message>.+)\",\"tree\"";
        }
        public class Downloads
        {
            public static File UPDATER = new File(
                "Updater.exe",
                "https://github.com/mattlemmone/NailClipr/raw/master/bin/Release/Updater.exe"
            );

            public static string BASE_PATH = Application.StartupPath + @"\";

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
        }
        public class File
        {
            public string
            title,
            downloadUrl,
            fullPath,
            ver;

            public File(string t, string down)
            {
                title = t;
                downloadUrl = down;
                fullPath = Structs.Downloads.BASE_PATH + t;
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
        public struct FFXIAH
        {
            public const string baseUrl = "http://www.ffxiah.com/item/";
            public class Item
            {
                public string name;
                public bool canStack;
                public uint id;
                public Single single;
                public Stack stack;

                public class Price
                {
                    public int
                    price, median, stock;
                }
                public class Single : Price
                {                  
                }
                public class Stack : Price
                {
                }
            }
            public class Sale
            {
                public string
                date,
                seller,
                buyer;
                public int price;
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
        public class URL
        {
            public static string
            blueGartr = "https://www.bg-wiki.com/index.php?search=",
            wiki = "http://ffxiclopedia.wikia.com/wiki/Special:Search?search=",
            AH = "http://www.ffxiah.com/search/item?name=";
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
