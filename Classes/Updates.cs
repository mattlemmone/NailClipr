using EliteMMO.API;
using System;
using System.Drawing;

namespace NailClipr.Classes
{
    class Updates
    {
        public static Structs.PC nearestPC = new Structs.PC();
        private static void UpdateZoneStatus(EliteAPI api)
        {
            NailClipr.GUI_STATUS.Text = api.Player.Status + "";
            NailClipr.GUI_ZONE.Text = Structs.Zones.NameFromID(api.Player.ZoneId);
        }
        private static void UpdateTarget(EliteAPI api)
        {
            EliteAPI.TargetInfo target = api.Target.GetTargetInfo();
            uint targetIdx = target.TargetIndex;
            var entity = api.Entity.GetEntity(Convert.ToInt32(targetIdx));
            string targetText = target.TargetName == "" ? "None" : entity.Name + " (" + entity.HealthPercent + "%) @ " + Math.Round(entity.Distance, 2) + " yalms.";
            NailClipr.GUI_TARGET.Text = targetText;
        }
        private static void UpdateNearestPlayer(EliteAPI api)
        {
            if (Structs.settings.playerDetection)
            {
                string nearestPlayerText = nearestPC.name == "" ? "None" : nearestPC.name + " @ " + Math.Round(nearestPC.distance, 2) + " yalms.";
                NailClipr.GUI_NEAREST_PLAYER.Text = nearestPlayerText;
            }
            else
            {
                NailClipr.GUI_NEAREST_PLAYER.Text = "Disabled";
            }
        }
        private static void UpdateSearch(EliteAPI api)
        {
            NailClipr.GUI_SEARCH.Text = Player.Search.status;
            NailClipr.GUI_ABORT.Enabled = Player.Search.isSearching;
        }
        private static void CheckZone(EliteAPI api)
        {
            if (Player.Location.isZoning)
            {
                if (Structs.zonePoints.Count > 0) Functions.ClearZonePoints();

                Player.Search.isSearching = false;
                Player.Search.status = Structs.Search.idle;
                //NailClipr.GUI_ABORT.Enabled = false;
                return;
            }
        }
        private static void UpdateZonePoints(EliteAPI api)
        {
            if (Structs.zonePoints.Count == 0 && api.Player.ZoneId != Player.Location.old)
            {
                Functions.LoadZonePoints(api);
            }
            Player.Location.old = api.Player.ZoneId;
        }
        private static void UpdateWarp(EliteAPI api)
        {
            if (Player.isWarping) NailClipr.GUI_WARP_BTN.Enabled = false;
            else
            if (!NailClipr.GUI_WARP_BTN.Enabled) NailClipr.GUI_WARP_BTN.Enabled = true;
        }
        public static void UpdateLabels(EliteAPI api)
        {
            UpdateZoneStatus(api);
            UpdateTarget(api);
            UpdateNearestPlayer(api);
            UpdateSearch(api);          
            CheckZone(api);
            UpdateZonePoints(api);
            UpdateTrackSpeed(NailClipr.GUI_SPEED_TRACK, NailClipr.GUI_SPEED, api.Player.Speed, api);
            DisableTrackSpeed();
            UpdateWarp(api);
        }
        public static void DisableTrackSpeed()
        {
            if (Player.isAlone || !Structs.settings.playerDetection)
            {
                if (NailClipr.GUI_SPEED_TRACK.Enabled == false)
                {
                    NailClipr.GUI_SPEED_TRACK.Enabled = true;
                    NailClipr.GUI_SPEED.ForeColor = Color.Black;
                    NailClipr.GUI_SPEED.Font = new Font(NailClipr.GUI_SPEED.Font, FontStyle.Regular);
                }
            }
            else
            {
                if (NailClipr.GUI_SPEED_TRACK.Enabled)
                {
                    NailClipr.GUI_SPEED_TRACK.Enabled = false;
                    NailClipr.GUI_SPEED.ForeColor = Color.MediumVioletRed;
                    NailClipr.GUI_SPEED.Font = new Font(NailClipr.GUI_SPEED.Font, FontStyle.Bold);
                }
            }
        }
        public static void UpdateTrackSpeed(System.Windows.Forms.TrackBar bar, System.Windows.Forms.Label lbl, float speed, EliteAPI api = null)
        {
            //Only update GUI speed if not in combat or CS.
            if (api == null || (api.Player.Status != 1 && api.Player.Status != 4 && api.Player.Speed >= Player.Speed.normal))
            {

                lbl.Text = "x" + speed / Structs.Speed.NATURAL;

                float f = (speed - Structs.Speed.NATURAL) * Structs.Speed.DIVISOR;
                int barSpeed = (int)Math.Ceiling(f);
                bar.Value = barSpeed;
            }
        }

    }
}
