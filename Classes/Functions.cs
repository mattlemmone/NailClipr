using EliteMMO.API;
using NailClipr.Classes;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;

namespace NailClipr
{
    class Functions
    {
        private static string pageDoc;

        public static void AddZonePoint(Structs.WarpPoint wp)
        {
            NailClipr.GUI_WARP.Items.Add(wp.title);
            Structs.zonePoints.Add(wp);
        }
        public static void ClearZonePoints()
        {
            NailClipr.GUI_WARP.Text = "";
            NailClipr.GUI_WARP.Items.Clear();
            Structs.zonePoints.Clear();
        }
        public static void GetPrice(EliteAPI api)
        {
            uint itemID = api.Inventory.SelectedItemId;
            GetPrice(api, itemID);
        }
        public static void GetPrice(EliteAPI api, uint itemID)
        {
            List<Structs.FFXIAH.Sale> sales = new List<Structs.FFXIAH.Sale>();
            
            string itemPage = Structs.FFXIAH.baseUrl + itemID,

            //Get page html.
            html = GetHTML(itemPage);

            //Get itemSale string so that we have less to sift through.
            string itemSaleStr = Misc.RegExMatch(html, Structs.FFXIAH.RegExs.itemSale);

            //Get number of sales and server to ensure we're parsing the correct one.
            int serverValue = int.Parse(Misc.RegExMatch(itemSaleStr, Structs.FFXIAH.RegExs.server));
            int numSales = Misc.RegExMatches(itemSaleStr, Structs.FFXIAH.RegExs.server).Count;

            for (int i = 0; i < numSales; i++)
            {
                var colCount = 0;
                Structs.FFXIAH.Sale sale = new Structs.FFXIAH.Sale();
                foreach (string regExStr in Structs.FFXIAH.RegExs.list)
                {
                    string matchVal = Misc.RegExMatch(itemSaleStr, regExStr, i);

                    if (colCount == 0) sale.date = FromUnixTime(long.Parse(matchVal)).ToShortDateString();
                    else if (colCount == 1) sale.seller = matchVal;
                    else if (colCount == 2) sale.buyer = matchVal;
                    else if (colCount == 3) sale.price = int.Parse(matchVal);
                    if (colCount == 3) { sales.Add(sale); }
                    colCount = (colCount + 1) % 4;
                }
            }

            //Create an item object after all nodes created.
            Structs.FFXIAH.Item item = new Structs.FFXIAH.Item();

            //Set these first
            /*
            item.name = titleText;
            item.id = itemID;
            item.price = ...;
            item.median = ...;
            item.stock = ...;

            //Output - single
            Chat.SendEcho(api, ...);

            */

            //Console Logs for debugging
            foreach (var sale in sales)
            {
                Console.WriteLine("Date: " + sale.date);
                Console.WriteLine("Seller: " + sale.seller);
                Console.WriteLine("Buyer: " + sale.buyer);
                Console.WriteLine("Price: " + sale.price);
            }           

        }
        public static void GetPrice(EliteAPI api, MatchCollection arguments)
        {
            /*
            //Array of strings from //command.
            string[] args = Misc.MatchToString(arguments);

            //Search terms. Gives us 'Serket Ring' from //price Serket Ring.
            string keywords = string.Join(" ", args.Skip(1));

            uint itemID = ...;
            GetPrice(api, itemID);
            */
        }
        public static void GetRendered(EliteAPI api)
        {
            bool findPlayer = Structs.settings.playerDetection;
            int count = 0;

            const Int32
            PC = 0x0001,
            NPC = 0x0002,
            Mob = 0x0010,
            Self = 0x000D;

            for (var x = 0; x < 4096; x++)
            {
                var entity = api.Entity.GetEntity(x);
                bool invalid = entity.WarpPointer == 0,
                    dead = entity.HealthPercent <= 0,
                    outsideRange = entity.Distance > 50.0f || float.IsNaN(entity.Distance) || entity.Distance <= 0,
                    isRendered = (entity.Render0000 & 0x200) == 0x200,
                    isSelf = (entity.SpawnFlags & Self) == Self || entity.Name == api.Player.Name,
                    isMob = (entity.SpawnFlags & Mob) == Mob,
                    isNPC = (entity.SpawnFlags & NPC) == NPC,
                    isPC = (entity.SpawnFlags & PC) == PC,
                    invalidPlayerName = isPC && (entity.Name.Length < Structs.FFXI.Name.MINLENGTH || entity.Name.Length > Structs.FFXI.Name.MAXLENGTH || !Regex.IsMatch(entity.Name, @"^[a-zA-Z]+$")),
                    inWhitelist = isPC && Structs.Speed.whitelist.IndexOf(entity.Name) != -1;

                if (invalid || dead || outsideRange || !isRendered || isSelf || invalidPlayerName)
                    continue;

                if (isPC && findPlayer && !inWhitelist)
                    PlayerFound(entity, ++count);

                if (Player.Search.isSearching) Search(api, entity);

            }
            //Outside of loop
            if (findPlayer)
                PlayerFound(count);
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
        public static void OpenURL(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
        public static void PlayerFound(int numPlayers)
        {
            if (numPlayers > 0) return;

            Updates.nearestPC.name = "";
            Updates.nearestPC.distance = 0;
            Player.isAlone = true;
        }
        public static void PlayerFound(EliteAPI.XiEntity entity, int numPlayers)
        {
            Player.isAlone = false;

            bool closerPC = Updates.nearestPC.distance == 0 || entity.Distance < Updates.nearestPC.distance || entity.Name == Updates.nearestPC.name;
            if (closerPC)
            {
                Updates.nearestPC.name = entity.Name;
                Updates.nearestPC.distance = entity.Distance;
            }
        }
        public static void SaveWarp(EliteAPI api, MatchCollection echoMatch)
        {
            string[] s = Misc.MatchToString(echoMatch);

            string saveName = string.Join(" ", s.Skip(1));
            SharedFunctions.SaveWarp(api, saveName);
        }
        public static void Search(MatchCollection echoMatch, string url)
        {
            string[] s = Misc.MatchToString(echoMatch);
            string term = string.Join(" ", s.Skip(1));
            OpenURL(url + term);
        }
        public static void Search(EliteAPI api, MatchCollection echoMatch)
        {
            string[] s = Misc.MatchToString(echoMatch);

            string target = string.Join(" ", s.Skip(1));
            SharedFunctions.Search(api, target);
        }
        public static void Search(EliteAPI api, EliteAPI.XiEntity entity)
        {
            string target = Player.Search.target.ToLower();
            Console.WriteLine(entity.Name);
            //Found target
            if (entity.Name.ToLower().Contains(target))
            {
                Player.Search.isSearching = false;
                Player.Search.status = Structs.Search.success;
                Chat.SendEcho(api, Chat.Search.success);

                EliteAPI.TargetInfo t = api.Target.GetTargetInfo();
                if (t.TargetIndex != entity.TargetID)
                {
                    //Not targeted, so set target!
                    api.Target.SetTarget(Convert.ToInt32(entity.TargetID));
                }
                return;
            }
        }
        public static string GetHTML(string url)
        {
            WebClient client = new WebClient();
            {
                return client.DownloadString(url);
            }
        }
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}

