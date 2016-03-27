using EliteMMO.API;
using System;
using System.Collections.Generic;

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
            private static int[] v = { 1, 0, 10 };
            public static string ver = string.Join(".", v);
        }
        public struct Chat
        {
            public struct Warp
            {
                public static string acceptNotify = "i accept <:'^)";
                public static string acceptSelfNotify = "Accepted.";
                public static string arrived = "Arrived.";
                public const string senderRegEx = @"\(([A-Za-z]+)\)";
                public const string coordRegEx = @"(\-*\d*\.*\d+)+";
                public const int expectedNumCoords = 4;
            }
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
            public const float MAX = 10f;
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
                if (NailClipr.GUI_MAINT.Checked == true)
                {
                    if (api.Player.Status != Structs.Status.MAINT)
                        api.Player.Status = Structs.Status.MAINT;
                }
            }
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
