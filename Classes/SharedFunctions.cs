using EliteMMO.API;
using System;

namespace NailClipr.Classes
{
    class SharedFunctions
    {
        delegate void GuiInvoker(EliteAPI api);
        delegate void GuiOptStrInvoker(EliteAPI api, string str);
        delegate void GuiStrOnlyInvoker(string str);

        public static void Abort(EliteAPI api)
        {
            Player.Search.isSearching = false;
            Player.Search.status = Structs.Search.idle;
            Chat.SendEcho(api, Chat.Search.abort);

        }
        public static void Accept(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(Accept), api);
            }
            else {

                if (!NailClipr.GUI_ACCEPT.Enabled) return;
                Chat.SendEcho(api, Chat.Warp.acceptSelfNotify);
                api.ThirdParty.SendString(Chat.Commands.party + Chat.Warp.acceptNotify);
                Player.Warp(api, true);
            }

        }
        public static void DelWarp(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(DelWarp), api);
            }
            else {
                XML.DeleteWarp(api);
            }
        }
        public static void GetWarp(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(GetWarp), api);
            }
            else {
                api.ThirdParty.SendString("/echo Selected: " + NailClipr.GUI_WARP.Text);
            }
        }
        public static void ListWarps(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(ListWarps), api);
            }
            else {
                int count = 0;
                foreach (var item in NailClipr.GUI_WARP.Items)
                {
                    string str = ++count + ": " + item;
                    Chat.SendEcho(api, str);
                }
            }
        }
        public static void MaintenanceToggle(EliteAPI api)
        {
            if (NailClipr.GUI_MAINT.InvokeRequired)
            {
                NailClipr.GUI_MAINT.Invoke(new GuiInvoker(MaintenanceToggle), api);
            }
            else {
                NailClipr.GUI_MAINT.Checked = !NailClipr.GUI_MAINT.Checked;
                Player.MaintenanceMode(api, NailClipr.GUI_MAINT.Checked);
                Chat.SendEcho(api, "Maintenance: " + NailClipr.GUI_MAINT.Checked);
            }
        }
        public static void Request(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(Request), api);
            }
            else {
                string s = Math.Round(api.Player.X, 5) + " " + Math.Round(api.Player.Z, 5) + " " + Math.Round(api.Player.Y, 5) + " " + api.Player.ZoneId;
                api.ThirdParty.SendString(Chat.Commands.party  + s);
            }

        }
        public static void SaveWarp(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(SaveWarp), api);
            }
            else {
                XML.SaveWarp(api);
            }

        }
        public static void SaveWarp(EliteAPI api, string name = "")
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiOptStrInvoker(SaveWarp), api, name);
            }
            else {
                if (name.Length > 0)
                    NailClipr.GUI_WARP.Text = name;
                XML.SaveWarp(api);
            }

        }
        public static void Search(EliteAPI api, string key = "")
        {
            if (NailClipr.GUI_SEARCH_TARGET.InvokeRequired)
            {
                NailClipr.GUI_SEARCH_TARGET.Invoke(new GuiOptStrInvoker(Search), api, key);
            }
            else {
                Chat.SendEcho(api, Chat.Search.begin);
                if (key != "")
                    NailClipr.GUI_SEARCH_TARGET.Text = key;

                if (NailClipr.GUI_SEARCH_TARGET.Text == "") return;

                Player.Search.isSearching = true;
                Player.Search.target = NailClipr.GUI_SEARCH_TARGET.Text;
                Player.Search.status = Structs.Search.searching;
                NailClipr.GUI_ABORT.Enabled = true;
            }
        }
        public static void Select(EliteAPI api, string key = "")
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiOptStrInvoker(Select), api, key);
            }
            else {
                int num;
                bool result = Int32.TryParse(key, out num);
                num--;
                if (!result || NailClipr.GUI_WARP.Items.Count <= num || num < 0)
                {
                    Chat.SendEcho(api, "Invalid selection.");
                    return;
                }
                string selected = NailClipr.GUI_WARP.Items[num].ToString();
                NailClipr.GUI_WARP.Text = selected;
                Chat.SendEcho(api, "Selected Warp: " + selected);
            }
        }
        public static void Speed(EliteAPI api, string op)
        {
            float speed = Player.Speed.expected;
            if (op == "+")
            {
                if (speed < Structs.Speed.MAX)
                    speed += 0.25f;
            }
            else if (op == "-")
            {
                if (speed >= Structs.Speed.NATURAL)
                    speed -= 0.25f;
            }
            Player.Speed.SetSpeed(api, speed);
        }
        public static void Warp(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.InvokeRequired)
            {
                NailClipr.GUI_WARP.Invoke(new GuiInvoker(Warp), api);
            }
            else {
                Player.Warp(api);
            }
        }
    }
}
