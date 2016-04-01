using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NailClipr
{
    public class Player
    {
        public static Structs.Position reqPos;
        public static bool hasDialogue;
        public static bool warpAccepted;
        public static bool isAlone;
        public static bool isWarping;

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
            if (api.Player.Status == Structs.Status.MAINT && !NailClipr.GUI_MAINT.Checked)
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

            //If we're warping to a saved location... not a player.
            if (!toPlayer)
            {
                nextWP = Structs.warpPoints.Find(wp => wp.title == NailClipr.GUI_WARP.Text && wp.zone == api.Player.ZoneId);
                if (nextWP.zone == 0)
                    return;
            }
            else {
                //Warping to a player.
                warpAccepted = true;
                nextWP.pos.X = reqPos.X;
                nextWP.pos.Y = reqPos.Y;
                nextWP.pos.Z = reqPos.Z;
            }

            //Mark flag for status gui text update.
            isWarping = true;
            api.ThirdParty.SendString("/echo " + Structs.Chat.Warp.warmupNotify);

            //Start warp.
            MaintenanceMode(api, true);

            System.Threading.Thread.Sleep(1000);

            api.Player.X = nextWP.pos.X;
            api.Player.Y = nextWP.pos.Y;
            api.Player.Z = nextWP.pos.Z;

            //Finish warp.
            System.Threading.Thread.Sleep(2000);
            MaintenanceMode(api, false);

            if (warpAccepted)
                warpAccepted = false;

            api.ThirdParty.SendString("/echo " + Structs.Chat.Warp.arrivedNotify);
            isWarping = false;
        }
        public static void PartyWarp(EliteAPI api, MatchCollection senderMatch, MatchCollection coordMatch)
        {
            string sender = senderMatch[0] + "";

            Structs.Position p = new Structs.Position();

            p.X = float.Parse(coordMatch[0] + "");
            p.Y = float.Parse(coordMatch[1] + "");
            p.Z = float.Parse(coordMatch[2] + "");
            p.Zone = int.Parse(coordMatch[3] + "");

            int endZoneID = p.Zone;
            string endZone = Structs.Zones.NameFromID(endZoneID);

            int startZoneID = api.Player.ZoneId;
            string startZone = Structs.Zones.NameFromID(startZoneID);

            if (endZoneID == startZoneID)
            {
                string s = "You have been requested by " + sender + " in " + endZone + ". You have until you zone to accept.";
                api.ThirdParty.SendString("/echo " + s);

                Player.reqPos = p;
                Player.hasDialogue = true;
            }
            else
            {
                //endZoneID != startZoneID
                api.ThirdParty.SendString("/echo Cannot warp to " + endZone + " from " + startZone + ".");
            }
        }
    }
}

