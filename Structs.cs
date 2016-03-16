using EliteMMO.API;
using System.Collections.Generic;

namespace NailClipr
{
    class Structs
    {

        public static Player player = new Player();
        public static List<WarpPoint> warpPoints = new List<WarpPoint>();
        public static List<WarpPoint> zonePoints = new List<WarpPoint>();

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
            public const uint DEFAULT = 0;
            public const uint MAINT = 31;
        }

        public struct Speed
        {
            private float e;
            public float expected
            {
                get { return e; }
                set { e = value; }
            }
            public const float DEFAULT = 5f;
            public const float DIVISOR = 4f;
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
                    status.old = Status.DEFAULT;
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
                NailClipr.GUI_STATUS.Text = api.Player.Status + ""; //Sleeping will otherwise block it.
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
