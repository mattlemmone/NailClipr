using EliteMMO.API;
using System;
using System.Threading.Tasks;

namespace NailClipr.Classes
{
    class Nyzul
    {
        /*
        Auto-saves rune of transfer warp.
        Auto-warps to rune of transfer on clear.
        */
        public const uint zoneID = 77;

        private static int warpDelaySec = 4;
        static bool newFloor, floorDone;
        private static Structs.WarpPoint runeWP;
        private struct regex
        {
            public static string
            newFloor = @"Transfer complete\. Welcome to Floor (?<match>\d+)\.",
            objective = "Objective: (?<match>[A-z|\\s]+)\\.",
            floorDone = "Floor (?<match>\\d)+ complete\\. .*";
        }

        private static async void FinishFloor(EliteAPI api)
        {
            await Task.Delay(warpDelaySec * 1000);
            Player.Warp(api, runeWP);
            floorDone = false;
        }
        private static void NewFloor(EliteAPI api)
        {
            //Clear all points
            Functions.ClearZonePoints();

            //Save waypoint
            runeWP = Functions.GetNyzulWP(api);
            if (runeWP.title == "null")
            {
                Console.WriteLine("Error getting Nyzul WP."); return;
            } else
            Functions.AddZonePoint(runeWP);
            newFloor = false;
        }
        public static void Parse(EliteAPI api, string text)
        {
            string
            floorStr = Misc.RegExMatch(text, regex.newFloor),
            objective = Misc.RegExMatch(text, regex.objective),
            doneStr = Misc.RegExMatch(text, regex.floorDone);

            if (floorStr != null) { newFloor = true; NewFloor(api); return; }
            else if (doneStr != null) { floorDone = true; FinishFloor(api); return; }
            else if (objective != null) SetObjective(objective);
        }
        private static void SetObjective(string obj)
        {

        }
    }
}

