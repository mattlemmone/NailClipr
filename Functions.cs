using EliteMMO.API;
using System;

namespace NailClipr
{
    class Functions
    {
        public static void addZonePoint(string title)
        {
            NailClipr.GUI_WARP.Items.Add(title);
        }

        public static void clearZonePoints()
        {
            NailClipr.GUI_WARP.Text = "";
            NailClipr.GUI_WARP.Items.Clear();
            Structs.zonePoints.Clear();
        }
        public static void loadZonePoints(EliteAPI api)
        {
            Structs.warpPoints.ForEach(wp =>
            {
                if (wp.zone == api.Player.ZoneId)
                {
                    Structs.zonePoints.Add(wp);
                    NailClipr.GUI_WARP.Items.Add(wp.title);
                }
            });
        }
        public static bool playersRendered(EliteAPI api)
        {
            int count = 0;
            for (var x = 0; x < 4096; x++)
            {
                var entity = api.Entity.GetEntity(x);

                // Skip invalid entities..
                if (entity.WarpPointer == 0)
                    continue;

                // Skip potentially dead entities..
                if (entity.HealthPercent <= 0)
                    continue;

                // Skip out of range entities..
                if (entity.Distance > 50.0f)
                    continue;

                // Check if the entity is rendered..
                if ((entity.Render0000 & 0x200) != 0x200)
                    continue;

                // Check entity.SpawnFlags here if you wish to check the type of entity it is..
                //
                // 0x0001 - PC 
                // 0x0002 - NPC
                // 0x0010 - Mob
                // 0x000D - Self (Current Player)

                // Skips PC players.. etc.
                if ((entity.SpawnFlags & 0x0001) == 0x0001)
                    count++;

                if (count > 1) return true;

            }
            return false;
        }
        public static void updateLabels(EliteAPI api)
        {
            //Pos. Z and Y write correctly but read each other. Inherent issue.
            NailClipr.GUI_X.Text = Math.Round(api.Player.X, 2) + "";
            NailClipr.GUI_Y.Text = Math.Round(api.Player.Z, 2) + "";
            NailClipr.GUI_Z.Text = Math.Round(api.Player.Y, 2) + "";

            //Zone and Status Label
            NailClipr.GUI_STATUS.Text = api.Player.Status + "";
            NailClipr.GUI_ZONE.Text = api.Player.ZoneId + "";

            /*Speed*/
            //Update labels
            NailClipr.GUI_SPEED.Text = "x" + api.Player.Speed / Structs.Speed.DEFAULT;
            float f = (api.Player.Speed - Structs.Speed.DEFAULT) * Structs.Speed.DIVISOR;
            int barSpeed = (int)Math.Ceiling(f);

            //If we aren't zoning...
            if (!Structs.player.location.isZoning)
            {
                //Load zone points.
                if (Structs.zonePoints.Count == 0 && api.Player.ZoneId != Structs.player.location.old)
                {
                    loadZonePoints(api);
                }

                NailClipr.GUI_SPEED_TRACK.Value = (int)Math.Ceiling(f);
                Structs.player.location.old = api.Player.ZoneId;
            }
            else
            {
                if (Structs.zonePoints.Count > 0)
                    clearZonePoints();
            }
        }
    }
}
