using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NailClipr.Classes
{
    class Chat
    {
        public static bool loaded;
        public static string loadStr = "Chat Loaded!";

        public class Controller
        {
            public const string echoRegex = @"([^\s])+";
            public const string
                //Dict Calls
                accept = "accept",
                request = "request",
                maintenance = "mntn",
                delWarp = "delete",
                warp = "warp",
                curWarp = "getwarp",
                abort = "abort",
                listWarps = "getwarps",
                desc = "desc",
                reload = "reload",
                help = "help",

                //Search Calls
                searchBG = "bg",
                searchWiki = "wiki",
                searchAH = "xiah",

                //Two param calls
                getPrice = "price",
                saveWarp = "save",
                search = "search",
                select = "select",
                speed = "speed";

            public static Dictionary<string, Action<EliteAPI>> dictOneParam =
            new Dictionary<string, Action<EliteAPI>>
            {
                    {abort, SharedFunctions.Abort },
                    {accept, SharedFunctions.Accept },
                    {curWarp, SharedFunctions.GetWarp },
                    {delWarp, SharedFunctions.DelWarp },
                    {desc, Functions.GetDesc },
                    {getPrice, Functions.GetPrice },
                    {help, Functions.ListCommands },
                    {listWarps, SharedFunctions.ListWarps },
                    {maintenance, SharedFunctions.MaintenanceToggle },
                    {reload, Functions.ReloadZonePoints },
                    {request, SharedFunctions.Request },
                    {saveWarp, SharedFunctions.SaveWarp },
                    {warp, SharedFunctions.Warp }
            };

        }
        public struct Commands
        {
            public const string echo = "/echo ";
            public const string party = "/p ";
            public const string linkshell = "/l ";
        }
        public struct Search
        {
            public static string begin = "Searching...";
            public static string success = "Search success!";
            public static string abort = "Search aborted.";
        }
        public struct Types
        {
            public const int partyOut = 13;
            public const int echo = 206;
        }
        public struct Warp
        {
            public static string acceptNotify = "i accept <:'^)";
            public static string acceptSelfNotify = "Accepted.";
            public static string arrivedNotify = "Arrived.";
            public static string warmupNotify = "Warming up...";

            public const string senderRegEx = @"\(([A-Za-z]+)\)";
            public const string coordRegEx = @"[^\[\d:d\]](\-*\d*\.*\d+)+";
            public const int expectedNumCoords = 4;
        }

        public static void Parse(EliteAPI api)
        {
            EliteAPI.ChatEntry c = api.Chat.GetNextChatLine();
            if (c == null && !Chat.loaded)
            { Chat.loaded = true; Chat.SendEcho(api, Chat.loadStr); }
            
            if (c == null || !Chat.loaded)
                return;

            const int party = Chat.Types.partyOut,
                echo = Chat.Types.echo;

            int chatType = c.ChatType;

            if (party == chatType) ProcessParty(api, c.Text);
            else if (echo == chatType) ProcessEcho(api, c.Text);
            else if (api.Player.ZoneId == Nyzul.zoneID /* && systemType == chatType */) ProcessNyzul(api, c.Text);

        }
        private static void ProcessEcho(EliteAPI api, string text)
        {
            if (!loaded) return;
            MatchCollection echoMatch = Regex.Matches(text, Chat.Controller.echoRegex);
            if (echoMatch.Count == 1)
            {
                Console.WriteLine(text);
                if (Chat.Controller.dictOneParam.ContainsKey(text)) { Chat.Controller.dictOneParam[text](api); }
                else { Console.WriteLine("No single param match."); }
                return;
            }
            string firstMatch = echoMatch[0].ToString();
            switch (firstMatch)
            {
                case Controller.getPrice:
                    Functions.GetPrice(api, echoMatch);
                    break;
                case Controller.saveWarp:
                    Functions.SaveWarp(api, echoMatch);
                    break;
                case Controller.search:
                    Functions.Search(api, echoMatch);
                    break;
                case Controller.speed:
                    SharedFunctions.Speed(api, echoMatch[1].Value);
                    break;
                case Controller.select:
                    SharedFunctions.Select(api, echoMatch[1].Value);
                    break;
                case Controller.searchBG:
                    Functions.Search(echoMatch, Structs.URL.blueGartr);
                    break;
                case Controller.searchWiki:
                    Functions.Search(echoMatch, Structs.URL.wiki);
                    break;
                case Controller.searchAH:
                    Functions.Search(echoMatch, Structs.URL.AH);
                    break;
            }
        }
        private static void ProcessParty(EliteAPI api, string text)
        {
            MatchCollection senderMatch = Regex.Matches(text, Chat.Warp.senderRegEx);
            MatchCollection coordMatch = Regex.Matches(text, Chat.Warp.coordRegEx);

            if (coordMatch.Count == Chat.Warp.expectedNumCoords)
                Player.PartyWarp(api, senderMatch, coordMatch);
        }
        private static void ProcessNyzul(EliteAPI api, string text)
        {
            Nyzul.Parse(api, text);
        }
        public static void SendEcho(EliteAPI api, string msg)
        {
            api.ThirdParty.SendString(Commands.echo + Structs.App.name + " - " + msg);
        }
        public static void SendParty(EliteAPI api, string msg)
        {
            api.ThirdParty.SendString(Commands.party + msg);
        }
        public static async void SendLinkshell(EliteAPI api, string msg)
        {
            msg = msg.Replace(Environment.NewLine, " ");
            msg = msg.Replace("<br>", "\n");
            int len = msg.Length;
            int readChars = 0;
            const int maxChars = 100;

            while (len != readChars)
            {
                int nextMsgLen = Math.Min(len - readChars, maxChars);
                string strOut = msg.Substring(readChars, nextMsgLen);
                readChars += nextMsgLen;
                api.ThirdParty.SendString(Commands.linkshell + strOut);
                await Task.Delay(1500);
            }
        }


    }
}
