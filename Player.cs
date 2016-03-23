using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailClipr
{
    public class Player
    {
        public static Structs.Position reqPos;
        public static bool hasDialogue;
        public static bool warpAccepted;
        public static bool isAlone;

        //Structs
        public class Speed
        {
            private static float exp, norm;
            public static float expected
            {
                get { return exp; }
                set { exp = value; }
            }
            public static float normal
            {
                get { return norm; }
                set { norm = value; }
            }
            public void SetSpeed(EliteAPI api, float speed)
            {
                api.Player.Speed = speed;
            }
        }
        public class Status
        {
            public static uint old;
            public void SetStatus(EliteAPI api, uint status)
            {
                api.Player.Status = status;
            }
        }
        public struct Location
        {
            public static int old;
            public static bool isZoning;
        }

        //Functions
        public void MaintenanceMode(EliteAPI api, bool on)
        {
            if (!on)
            {
                api.Player.Status = Status.old;
                return;
            }
            //Save status before switching.
            if (api.Player.Status == Structs.Status.MAINT)
            {
                Status.old = Structs.Status.NATURAL;
            }
            else
            {
                Status.old = api.Player.Status;
            }

            //Maint on.
            api.Player.Status = Structs.Status.MAINT;
        }
        public void Warp(EliteAPI api, bool toPlayer = false)
        {
            Structs.WarpPoint nextWP;
            if (!toPlayer)
            {
                nextWP = Structs.warpPoints.Find(wp => wp.title == NailClipr.GUI_WARP.Text && wp.zone == api.Player.ZoneId);
                if (nextWP.zone == 0)
                    return;
            } else
            {
                warpAccepted = true;
                nextWP.pos.X = reqPos.X;
                nextWP.pos.Y = reqPos.Y;
                nextWP.pos.Z = reqPos.Z;
            }

            MaintenanceMode(api, true);
            NailClipr.GUI_STATUS.Text = Structs.Status.MAINT + ""; //Sleeping will otherwise block it.
            System.Threading.Thread.Sleep(1000);

            api.Player.X = nextWP.pos.X;
            api.Player.Y = nextWP.pos.Y;
            api.Player.Z = nextWP.pos.Z;

            System.Threading.Thread.Sleep(2000);
            MaintenanceMode(api, false);
            if (warpAccepted)
            {
                warpAccepted = false;
                api.ThirdParty.SendString("/echo Arrived.");
            }
        }
    }
}

