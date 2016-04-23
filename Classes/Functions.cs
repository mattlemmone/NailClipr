using EliteMMO.API;
using NailClipr.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NailClipr
{
    class Functions
    {

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
        public static void GetDesc(EliteAPI api)
        {
            uint itemID = api.Inventory.SelectedItemId;
            Structs.InventoryItem item = XML.GetInvItem(itemID);

            if (!item.success)
            {
                Chat.SendEcho(api, "Couldn't find that item!");
                return;
            }

            //Get page html.
            string itemPage = FFXIAH.baseUrl + itemID;
            string html = FFXIAH.GetHTML(itemPage, 12);
            string itemName = item.name;
            string desc = Misc.RegExMatch(html, FFXIAH.RegExs.desc);
            Chat.SendLinkshell(api, itemName + "\n" + desc);
        }
        public static void GetPrice(EliteAPI api)
        {
            uint itemID = api.Inventory.SelectedItemId;
            Structs.InventoryItem item = XML.GetInvItem(itemID);

            if (item.success) GetPrice(api, item);
            else Chat.SendEcho(api, "Couldn't find that item!");
        }
        public static void GetPrice(EliteAPI api, bool isStack)
        {
            uint itemID = api.Inventory.SelectedItemId;
            Structs.InventoryItem item = XML.GetInvItem(itemID);

            if (item.success) GetPrice(api, item, isStack);
            else Chat.SendEcho(api, "Couldn't find that item!");

        }
        public static void GetPrice(EliteAPI api, Structs.InventoryItem invItem, bool isStack = false)
        {
            string itemPage = FFXIAH.baseUrl + invItem.id;
            if (isStack) itemPage += "/?stack=1";

            //Get page html.
            string html = FFXIAH.GetHTML(itemPage, 12);

            //Get itemSale string so that we have less to sift through.
            string itemSaleStr = Misc.RegExMatch(html, FFXIAH.RegExs.itemSale);
            int numSales = 0, maxSales;

            //Item isn't EX
            if (itemSaleStr != "null")
            {
                //Get number of sales and server to ensure we're parsing the correct one.
                int serverValue = int.Parse(Misc.RegExMatch(itemSaleStr, FFXIAH.RegExs.server));

                //Verify server is correct before parsing.
                /*if (api.Player.ServerId != serverValue)
                {
                   Chat.SendEcho(api, "Wrong server being parsed!");
                    return;
                }*/

                numSales = Misc.RegExMatches(itemSaleStr, FFXIAH.RegExs.server).Count;
                maxSales = FFXIAH.maxSales > numSales ? numSales : FFXIAH.maxSales;

                //Store sales
                for (int i = 0; i < maxSales; i++)
                {
                    var colCount = 0;
                    FFXIAH.Sale sale = new FFXIAH.Sale();
                    foreach (string regExStr in FFXIAH.RegExs.list)
                    {
                        string matchVal = Misc.RegExMatch(itemSaleStr, regExStr, i);
                        if (colCount == 0) sale.date = Misc.FromUnixTime(long.Parse(matchVal)).ToShortDateString();
                        else if (colCount == 1) sale.seller = matchVal;
                        else if (colCount == 2) sale.buyer = matchVal;
                        else if (colCount == 3) sale.price = int.Parse(matchVal);
                        if (colCount == 3) { FFXIAH.sales.Add(sale); }
                        colCount = (colCount + 1) % 4;
                    }
                }
            }

            //Create an xiah item object after all info gathered.
            FFXIAH.Item item = new FFXIAH.Item();

            //Store item info.
            string stockStr = Misc.RegExMatch(html, FFXIAH.RegExs.stock);
            item.stock = stockStr != "null" ? int.Parse(stockStr) : 0;
            item.isStack = isStack;

            //Print sales list.
            FFXIAH.PrintSales(api, invItem, item, numSales);

            //Clear sales list.
            FFXIAH.sales.Clear();
        }
        public static void GetPrice(EliteAPI api, MatchCollection arguments)
        {

            //Array of strings from //command.
            string[] args = Misc.MatchToString(arguments);

            //Current selected item stack check.
            if (args.Length == 2 && args[args.Length - 1].ToLower() == "stack")
            { GetPrice(api, true); return; }

            //Search terms. Gives us 'Serket Ring' from //price Serket Ring.
            bool isStack = false;
            string keywords = string.Join(" ", args.Skip(1));

            if (args.Length >= 2 && args[args.Length - 1].ToLower() == "stack")
            {
                isStack = true;
                Regex r = new Regex(@"\s?stack\s?");
                keywords = r.Replace(keywords, string.Empty);
            }

            Structs.InventoryItem item = XML.GetInvItem(keywords);

            if (item.success)
            {
                uint itemID = item.id;
                GetPrice(api, item, isStack);
            }
            else
            {
                Chat.SendEcho(api, "Couldn't find that item!");
            }

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
        public static Structs.WarpPoint GetNyzulWP(EliteAPI api)
        {

            Structs.WarpPoint WP = new Structs.WarpPoint();
            WP.title = "null";

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

                if (entity.Name.Contains("Transfer"))
                {
                    WP.title = "Rune of Transfer";
                    WP.zone = api.Player.ZoneId;
                    WP.pos.X = entity.X;
                    WP.pos.X = entity.Y;
                    WP.pos.X = entity.Z;
                    return WP;
                }
            }
            return WP;
        }
        public static void ListCommands(EliteAPI api)
        {
            List<string> commands = new List<string>();
            foreach (var entry in Chat.Controller.dictOneParam){
                commands.Add(entry.Key);
            }
            string msg = "Available Commands:\n" + string.Join(", ", commands.ToArray());
            Chat.SendEcho(api, msg);
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
            //Chat.SendEcho(api, "WPs Loaded.");
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
        public static void ReloadZonePoints(EliteAPI api)
        {
            ClearZonePoints();
            LoadZonePoints(api);
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
            Misc.OpenURL(url + term);
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
    }
}

