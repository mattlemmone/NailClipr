using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NailClipr.Classes;

namespace NailClipr
{
    class XML
    {
        public const string SETTINGS = "Resources/Settings.xml";
        public const string AREAS = "Resources/areas.xml";
        public const string ITEMS = "Resources/ffxiah_items.xml";

        private static XDocument itemsDoc;
        private static XDocument settingsDoc;
        public static Structs.InventoryItem GetInvItem(uint itemId)
        {
            Structs.InventoryItem item = new Structs.InventoryItem();
            try
            {
                XElement itemNode = itemsDoc.Descendants("ffxiah")
                .Elements("item")
                .Where(x => x.Element("int_id").Value == itemId + "").FirstOrDefault();

                item.description = itemNode.Element("en_description").Value;
                item.id = itemId;
                item.name = itemNode.Element("en_name").Value;
                item.singleName = itemNode.Element("log_name_singular").Value;
                item.stackName = itemNode.Element("log_name_plural").Value;
                item.stackSize = int.Parse(itemNode.Element("stack_size").Value);
                item.success = true;
            }
            catch (Exception e)
            {
                item.success = false;
                Console.WriteLine("Couldn't parse by id.");
                Console.WriteLine(e.Message);
            }
            return item;
        }
        public static Structs.InventoryItem GetInvItem(string itemName)
        {
            Structs.InventoryItem item = new Structs.InventoryItem();

            try
            {
                XElement itemNode = itemsDoc.Descendants("ffxiah")
                    .Elements("item")
                    .Where(x =>
                    x.Element("en_name").Value == itemName
                    || x.Element("log_name_singular").Value == itemName
                    || x.Element("log_name_plural").Value == itemName
                    ).FirstOrDefault();

                item.description = itemNode.Element("en_description").Value;
                item.id = uint.Parse(itemNode.Element("int_id").Value);
                item.name = itemNode.Element("en_name").Value;
                item.singleName = itemNode.Element("log_name_singular").Value;
                item.stackName = itemNode.Element("log_name_plural").Value;
                item.stackSize = int.Parse(itemNode.Element("stack_size").Value);
                item.success = true;
            }
            catch (Exception e)
            {
                item.success = false;
                Console.WriteLine("Couldn't parse by string.");
                Console.WriteLine(e.Message);
            }
            return item;
        }
        public static void Create()
        {

            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("NailClipr",
                    new XElement("Zones"),

                    new XElement("Settings",
                        new XElement("PlayerDetection", Structs.settings.playerDetection),
                        new XElement("StayOnTop", Structs.settings.topMostForm),
                        new XElement("DefaultSpeed", Player.Speed.normal)
                        )));

            xmlDocument.Save(SETTINGS);
            settingsDoc = XDocument.Load(SETTINGS);
        }
        public static void LoadSettings()
        {
            try
            {
                settingsDoc = XDocument.Load(SETTINGS);
                itemsDoc = XDocument.Load(ITEMS);
                bool PD = Convert.ToBoolean(settingsDoc.Element("NailClipr").Element("Settings").Element("PlayerDetection").Value);
                bool SOT = Convert.ToBoolean(settingsDoc.Element("NailClipr").Element("Settings").Element("StayOnTop").Value);
                float DS = float.Parse(settingsDoc.Element("NailClipr").Element("Settings").Element("DefaultSpeed").Value);

                //Update checkboxes
                NailClipr.GUI_PLAYER_DETECT.Checked = PD;
                Structs.settings.playerDetection = PD;

                NailClipr.GUI_TOPMOST.Checked = SOT;
                Structs.settings.topMostForm = PD;

                //Update Speed
                float speed = DS == 0 ? Structs.Speed.NATURAL : DS;
                Player.Speed.normal = speed;

                //Update Speed Label
                Updates.UpdateTrackSpeed(NailClipr.GUI_SPEED_DEFAULT_TRACK, NailClipr.GUI_DEFAULT_SPEED, speed);
            }
            catch (FileNotFoundException)
            {
                XML.Create();
            }
        }
        public static void LoadAreas()
        {

            XDocument adoc = XDocument.Load(AREAS);

            IEnumerable<XElement> allElements =
            from xEle in adoc.Descendants("areas")
            select xEle;
            foreach (XElement result in allElements)
            {
                result.Elements().Select(t => new
                {
                    id = t.Attribute("id").Value,
                    name = t.Value,
                }).ToList().ForEach(t =>
                {
                    Structs.Zone z = new Structs.Zone();
                    z.id = int.Parse(t.id);
                    z.name = t.name;
                    Structs.zoneList.Add(z);
                });
            }
        }
        public static void LoadWarps()
        {
            try
            {
                settingsDoc = XDocument.Load(SETTINGS);
                IEnumerable<XElement> allElements =
                from xEle in settingsDoc.Descendants("Zones")
                select xEle;

                foreach (XElement result in allElements)
                {
                    result.Descendants("WarpPoint").Select(t => new
                    {
                        zone = t.Parent.Attribute("id").Value,
                        title = t.Attribute("title").Value,
                        x = t.Element("X").Value,
                        y = t.Element("Y").Value,
                        z = t.Element("Z").Value,
                    }).ToList().ForEach(t =>
                    {
                        Structs.Position pos = new Structs.Position();
                        pos.X = float.Parse(t.x);
                        pos.Y = float.Parse(t.y);
                        pos.Z = float.Parse(t.z);

                        Structs.WarpPoint wp = new Structs.WarpPoint();
                        wp.title = t.title;
                        wp.zone = int.Parse(t.zone);
                        wp.pos = pos;
                        Structs.warpPoints.Add(wp);
                    });

                }
            }
            catch (FileNotFoundException)
            {
                XML.Create();
            }

        }
        public static void SaveSettings()
        {
            try
            {
                settingsDoc = XDocument.Load(SETTINGS);
                settingsDoc.Element("NailClipr").Element("Settings").Element("PlayerDetection").Value = Structs.settings.playerDetection + "";
                settingsDoc.Element("NailClipr").Element("Settings").Element("StayOnTop").Value = Structs.settings.topMostForm + "";
                settingsDoc.Element("NailClipr").Element("Settings").Element("DefaultSpeed").Value = Player.Speed.normal + "";
                settingsDoc.Save(SETTINGS);
            }
            catch (FileNotFoundException)
            {
                XML.Create();
                XML.SaveSettings();
            }
        }
        public static void SaveWarp(EliteAPI api)
        {
            try
            {
                settingsDoc = XDocument.Load(SETTINGS);
                //http://www.c-sharpcorner.com/UploadFile/de41d6/learning-linq-made-easy-linq-to-xml-tutorial-3/

                Structs.Position pos = new Structs.Position();
                pos.X = api.Player.X;
                pos.Y = api.Player.Z;
                pos.Z = api.Player.Y;

                Structs.WarpPoint wp = new Structs.WarpPoint();
                string warpText = NailClipr.GUI_WARP.Text;
                if (warpText == "") { NailClipr.GUI_WARP.Text = "Untitled Location"; wp.title = "Untitled Location"; }
                else { wp.title = warpText; }
                wp.zone = api.Player.ZoneId;
                wp.pos = pos;
                string zoneName = Structs.Zones.NameFromID(wp.zone);

                //Updating - delete old and save new.
                int index = Structs.zonePoints.FindIndex(p => p.title == wp.title);
                if (index >= 0)
                {
                    Chat.SendEcho(api, " Updating warp point: " + zoneName + " - " + NailClipr.GUI_WARP.Text + ".");
                    DeleteWarp(api, true);
                }
                else
                {
                    Chat.SendEcho(api, " Saving new warp point: " + zoneName + " - " + NailClipr.GUI_WARP.Text + ".");
                }

                Structs.warpPoints.Add(wp);
                Functions.AddZonePoint(wp);

                try
                {
                    string s = settingsDoc.Element("NailClipr").Element("Zones").Elements("Zone").Single(z => z.Attribute("id").Value == wp.zone + "").Value;
                }
                catch (InvalidOperationException)
                {
                    settingsDoc.Element("NailClipr").Element("Zones").Add(new XElement("Zone",
                        new XAttribute("id", wp.zone),
                        new XAttribute("title", zoneName)));
                    settingsDoc.Save(SETTINGS);
                }


                settingsDoc.Element("NailClipr").Elements("Zones").Elements("Zone").Single(z => z.Attribute("id").Value == wp.zone + "")
                    .Add(
                       new XElement("WarpPoint",
                       new XAttribute("title", wp.title),
                       new XElement("X", wp.pos.X),
                       new XElement("Y", wp.pos.Y),
                       new XElement("Z", wp.pos.Z)
                    ));
                settingsDoc.Save(SETTINGS);
            }
            catch (FileNotFoundException)
            {
                Create();
                SaveWarp(api);
            }
        }
        public static void DeleteWarp(EliteAPI api, bool updating = false)
        {
            settingsDoc = XDocument.Load(SETTINGS);
            if (NailClipr.GUI_WARP.Text == "") return;
            int zone = api.Player.ZoneId;
            string zoneName = Structs.Zones.NameFromID(zone);
            string delName = NailClipr.GUI_WARP.Text;

            if (!updating)
            {
                Chat.SendEcho(api, " Deleting warp point: " + zoneName + " - " + delName + ".");
                NailClipr.GUI_WARP.Text = "";
            }

            Structs.WarpPoint delWP = Structs.zonePoints.Find(wp => wp.title == delName);

            if (delWP.Equals(default(Structs.WarpPoint)))
            {
                Chat.SendEcho(api, "No warp point with that name found.");
                return;
            }

            XElement delNode = settingsDoc.Descendants("WarpPoint")
               .Where(a => a.Parent.Attribute("id").Value == zone + "" && a.Attribute("title").Value == delName)
               .FirstOrDefault();

            delNode.Remove();
            settingsDoc.Save(SETTINGS);
            Structs.zonePoints.Remove(delWP);
            Structs.warpPoints.Remove(delWP);

            NailClipr.GUI_WARP.Items.Remove(delWP.title);
        }
    }
}
