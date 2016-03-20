using EliteMMO.API;
using System;
using System.Collections.Generic;

namespace NailClipr
{
    class Structs
    {

        public static Player player = new Player();
        public static Zone Zones = new Zone();
        public static Settings settings = new Settings();
        public static List<WarpPoint> warpPoints = new List<WarpPoint>();
        public static List<WarpPoint> zonePoints = new List<WarpPoint>();
        public static List<Zone> zones = new List<Zone>();

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

        public class Zone
        {
            public int id;
            public string name;
            public string nameFromID(int id)
            {
                int zIndex = zones.FindIndex(z => z.id == id);
                return zones[zIndex].name;
            }
            public int IDfromName(string name)
            {
                int zIndex = zones.FindIndex(z => z.name == name);
                return zones[zIndex].id;
            }
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
            private uint oldStatus;
            public uint old
            {
                get { return oldStatus; }
                set { oldStatus = value; }
            }
            public const uint NATURAL = 0;
            public const uint MAINT = 31;
        }

        public struct Speed
        {
            private float exp, norm;
            public float expected
            {
                get { return exp; }
                set { exp = value; }
            }
            public float normal
            {
                get { return norm; }
                set { norm = value; }
            }
            public const float NATURAL = 5f;
            public const float DIVISOR = 4f;
            public const float MAX_MULT = 1.5f;
            public const float MAX = 10f;
        }

        public struct Location
        {
            public int old;
            public bool isZoning;
        }

        public class Player
        {
            public Speed speed = new Speed();
            public Status status = new Status();
            public Location location = new Location();
            public bool isAlone;
            //Functions
            public void maintenanceMode(EliteAPI api, bool on)
            {
                if (!on)
                {
                    api.Player.Status = status.old;
                    return;
                }
                //Save status before switching.
                if (api.Player.Status == Status.MAINT)
                {
                    status.old = Status.NATURAL;
                }
                else
                {
                    status.old = api.Player.Status;
                }

                //Maint on.
                api.Player.Status = Status.MAINT;
            }
            public void warp(EliteAPI api)
            {
                Structs.WarpPoint nextWP = Structs.warpPoints.Find(wp => wp.title == NailClipr.GUI_WARP.Text && wp.zone == api.Player.ZoneId);
                if (nextWP.zone == 0)
                    return;

                Structs.player.maintenanceMode(api, true);
                NailClipr.GUI_STATUS.Text = Structs.Status.MAINT + ""; //Sleeping will otherwise block it.
                System.Threading.Thread.Sleep(1000);

                api.Player.X = nextWP.pos.X;
                api.Player.Y = nextWP.pos.Y;
                api.Player.Z = nextWP.pos.Z;

                System.Threading.Thread.Sleep(2000);
                Structs.player.maintenanceMode(api, false);
            }
        }


    }
}
