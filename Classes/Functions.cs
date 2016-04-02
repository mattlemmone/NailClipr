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
            bool findPlayer = Structs.settings.playerDetection;
            bool found = false;
            int count = 0;

            const Int32 PC = 0x0001;
            const Int32 NPC = 0x0002;
            const Int32 Mob = 0x0010;
            const Int32 Self = 0x000D;

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
                if (entity.Distance > 50.0f || float.IsNaN(entity.Distance) || entity.Distance <= 0)
                    continue;

                bool isRendered = (entity.Render0000 & 0x200) == 0x200;
                bool isSelf = (entity.SpawnFlags & Self) == Self || entity.Name == api.Player.Name;

                //Skip unloaded entities and self.
                if (!isRendered || isSelf)
                    continue;

                bool isMob = (entity.SpawnFlags & Mob) == Mob;
                bool isNPC = (entity.SpawnFlags & NPC) == NPC;
                bool isPC = (entity.SpawnFlags & PC) == PC;

                bool invalidPlayerName = entity.Name.Length < Structs.FFXI.Name.MINLENGTH || entity.Name.Length > Structs.FFXI.Name.MAXLENGTH || !Regex.IsMatch(entity.Name, @"^[a-zA-Z]+$");

                //Is in whitelist
                if (isPC && Structs.Speed.whitelist.IndexOf(entity.Name) != -1)
                    continue;

                if (isPC && invalidPlayerName)
                    continue;

                if (isPC && findPlayer)
                {
                    count++;
                    Player.isAlone = false;

                    if (nearestPC.distance == 0 || entity.Distance < nearestPC.distance || entity.Name == nearestPC.name)
                    {
                        nearestPC.name = entity.Name;
                        nearestPC.distance = entity.Distance;
                    }
                }

                if (Player.Search.isSearching)
                {
                    string target = Player.Search.target.ToLower();
                    //Found target
                    if (entity.Name.ToLower().Contains(target))
                    {
                        Console.WriteLine(entity.Name);
                        Player.Search.isSearching = false;
                        Player.Search.status = Structs.Search.success;

                        EliteAPI.TargetInfo t = api.Target.GetTargetInfo();
                        if (t.TargetIndex != entity.TargetID)
                        {
                            //Not targeted, so set target!
                            api.Target.SetTarget(Convert.ToInt32(entity.TargetID));
                        }
                        return;
                    }
                }

            }
            if (findPlayer)
            {
                if (count > 0) return;

                nearestPC.name = "";
                nearestPC.distance = 0;
                Player.isAlone = true;
            }
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
            string targetText = target.TargetName == "" ? "None" : entity.Name + " (" + entity.HealthPercent + "%) @ " + Math.Round(entity.Distance, 2) + " yalms.";
            NailClipr.GUI_TARGET.Text = targetText;

            //Nearest Player
            if (Structs.settings.playerDetection)
            {
                string nearestPlayerText = nearestPC.name == "" ? "None" : nearestPC.name + " @ " + Math.Round(nearestPC.distance, 2) + " yalms.";
                NailClipr.GUI_NEAREST_PLAYER.Text = nearestPlayerText;
            }
            else
            {
                NailClipr.GUI_NEAREST_PLAYER.Text = "Disabled";
            }

            //Search label
            NailClipr.GUI_SEARCH.Text = Player.Search.status;

            //If we are zoning...
            if (Player.Location.isZoning)
            {
                if (Structs.zonePoints.Count > 0) clearZonePoints();

                Player.Search.isSearching = false;
                NailClipr.GUI_SEARCH.Text = "idle";
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

            if (Player.isWarping) NailClipr.GUI_WARP_BTN.Enabled = false;
            else
            if (!NailClipr.GUI_WARP_BTN.Enabled) NailClipr.GUI_WARP_BTN.Enabled = true;


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
            }
        }

    }
}

