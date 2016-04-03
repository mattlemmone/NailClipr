using EliteMMO.API;
using NailClipr.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailClipr
{
    class Threads
    {
        public static void Overwrites(EliteAPI api)
        {
            //Constantly write maintenance mode in case it gets overwritten.
            Structs.Status.PreventOverwrite(api);

            /*Speed*/
            //Not initialized.
            if (Player.Speed.expected == 0 && api.Player.Speed <= Structs.Speed.MAX)
            {
                Player.Speed.expected = api.Player.Speed;
            }

            if (Player.Search.isSearching || Structs.settings.playerDetection)
                Functions.PlayersRendered(api);

            Structs.Speed.PreventOverWrite(api);
        }

        public static void Update(EliteAPI api)
        {
            //Update GUI.
            Player.Location.isZoning = api.Player.X == 0 && api.Player.Y == 0 && api.Player.Z == 0;
            if (Player.Location.isZoning && Player.hasDialogue)
                Player.hasDialogue = false;

            Updates.UpdateLabels(api);

            NailClipr.GUI_ACCEPT.Enabled = Player.hasDialogue;
        }


    }
}
