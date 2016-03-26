using EliteMMO.API;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace NailClipr
{
    class Functions
    {
        private static Structs.PC nearestPC = new Structs.PC();

        public static void AddZonePoint(Structs.WarpPoint wp)
        {
            NailClipr.GUI_WARP.Items.Add(wp.title);
            Structs.zonePoints.Add(wp);
        }
        public static void clearZonePoints()
        {
            NailClipr.GUI_WARP.Text = "";
            NailClipr.GUI_WARP.Items.Clear();
            Structs.zonePoints.Clear();
        }
        public static void LoadZonePoints(EliteAPI api)
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
        public static void PlayersRendered(EliteAPI api)
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
                if ((entity.Render0000 & 0x200) != 0x200 || (entity.SpawnFlags & 0x0001) != 0x0001)
                    continue;

                // Check entity.SpawnFlags here if you wish to check the type of entity it is..
                //
                // 0x0001 - PC 
                // 0x0002 - NPC
                // 0x0010 - Mob
                // 0x000D - Self (Current Player)

                //Self
                if (entity.Name == api.Player.Name)
                    continue;

                //Is in whitelist
                if (Structs.Speed.whitelist.IndexOf(entity.Name) != -1)
                    continue;

                count++;
                Player.isAlone = false;
                if (nearestPC.distance == 0 || entity.Distance < nearestPC.distance || entity.Name == nearestPC.name)
                {
                    if (!float.IsNaN(entity.Distance))
                    {
                        nearestPC.name = entity.Name;
                        nearestPC.distance = entity.Distance;
                    }
                }

            }
            if (count > 0) return;

            nearestPC.name = "";
            nearestPC.distance = 0;
            Player.isAlone = true;
        }
        public static void UpdateLabels(EliteAPI api)
        {

            //Zone and Status Label
            NailClipr.GUI_STATUS.Text = api.Player.Status + "";
            NailClipr.GUI_ZONE.Text = Structs.Zones.NameFromID(api.Player.ZoneId);

            //Target Info
            EliteAPI.TargetInfo target = api.Target.GetTargetInfo();
            uint targetIdx = target.TargetIndex;
            var entity = api.Entity.GetEntity(Convert.ToInt32(targetIdx));
            string s = target.TargetName == "" ? "None" : entity.Name + " (" + entity.HealthPercent + "%) @ " + Math.Round(entity.Distance, 2) + " yalms.";
            NailClipr.GUI_TARGET.Text = s;

            //Nearest Player
            if (Structs.settings.playerDetection)
            {
                s = nearestPC.name == "" ? "None" : nearestPC.name + " @ " + Math.Round(nearestPC.distance, 2) + " yalms.";
                NailClipr.GUI_NEAREST_PLAYER.Text = s;
            }
            else
            {
                NailClipr.GUI_NEAREST_PLAYER.Text = "Disabled";
            }

            //If we aren't zoning...
            if (Player.Location.isZoning)
            {
                if (Structs.zonePoints.Count > 0) clearZonePoints();
                return;
            }

            //Load zone points.
            if (Structs.zonePoints.Count == 0 && api.Player.ZoneId != Player.Location.old)
            {
                LoadZonePoints(api);
            }
            Player.Location.old = api.Player.ZoneId;

            //Speed labels
            UpdateTrackSpeed(NailClipr.GUI_SPEED_TRACK, NailClipr.GUI_SPEED, api.Player.Speed, api);

            //Disable track bar, highlight speed. Visual cue.
            DisableTrackSpeed();
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
        public static void ParseChat(EliteAPI api)
        {
            EliteAPI.ChatEntry c = api.Chat.GetNextChatLine();
            if (string.IsNullOrEmpty(c?.Text)) return;
            const int partyType = 13; //Incoming party
            int chatType = c.ChatType;
            string t = c.Text;
            Console.WriteLine(t);
            if (partyType == chatType)
            {
                string text = c.Text;

                MatchCollection senderMatch = Regex.Matches(text, Structs.Chat.Warp.senderRegEx);
                MatchCollection coordMatch = Regex.Matches(text, Structs.Chat.Warp.coordRegEx);

                if (coordMatch.Count == Structs.Chat.Warp.expectedNumCoords)
                {
                    Player.PartyWarp(api, senderMatch, coordMatch);
                }
                else
                {
                    //!(senderMatch.Count == 1 && coordMatch.Count == 4)
                    foreach (var k in coordMatch)
                        Console.WriteLine(k);
                }
            }
        }

    }
}

