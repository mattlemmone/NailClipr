using EliteMMO.API;
using System;
using System.Collections.Generic;

namespace NailClipr
{
    class Structs
    {
        public static Zone Zones = new Zone();
        public static Settings settings = new Settings();
        public static List<WarpPoint> warpPoints = new List<WarpPoint>();
        public static List<WarpPoint> zonePoints = new List<WarpPoint>();
        public static List<Zone> zoneList = new List<Zone>();

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
        public struct PC
        {
            public string name;
            public float distance;
        }
        public struct Settings
        {
            public bool topMostForm;
            public bool playerDetection;
            public const float POS_INC = 5f;
        }
        public struct Position
        {
            public float X;
            public float Y;
            public float Z;
        }
        public struct WarpPoint
        {
            public string title;
            public Position pos;
            public int zone;
        }
        public struct Status
        {           
            public const uint NATURAL = 0;
            public const uint MAINT = 31;
        }
        public struct Speed
        {
            public const float NATURAL = 5f;
            public const float DIVISOR = 4f;
            public const float MAX_MULT = 1.5f;
            public const float MAX = 10f;
        }
    }
    
}
