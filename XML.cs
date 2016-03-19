using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NailClipr
{
    class XML
    {
        public const string SETTINGS = "Resources/Settings.xml";
        public const string AREAS = "Resources/areas.xml";

        public static XDocument xdoc;

        public static void create()
        {

            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("NailClipr",
                    new XElement("Locations"),

                    new XElement("Settings",
                        new XElement("PlayerDetection", Structs.settings.playerDetection),
                        new XElement("StayOnTop", Structs.settings.topMostForm),
                        new XElement("DefaultSpeed", Structs.player.speed.normal)
                        )));

            xmlDocument.Save(SETTINGS);
            xdoc = XDocument.Load(SETTINGS);
        }
        public static void saveSettings()
        {
            try
            {
                xdoc.Element("NailClipr").Element("Settings").Element("PlayerDetection").Value = Structs.settings.playerDetection + "";
                xdoc.Element("NailClipr").Element("Settings").Element("StayOnTop").Value = Structs.settings.topMostForm + "";
                xdoc.Element("NailClipr").Element("Settings").Element("DefaultSpeed").Value = Structs.player.speed.normal + "";
                xdoc.Save(SETTINGS);
            }
            catch (FileNotFoundException)
            {
                XML.create();
                XML.saveSettings();
            }
        }
        public static void loadSettings()
        {
            try
            {
                xdoc = XDocument.Load(SETTINGS);
                bool PD = Convert.ToBoolean(xdoc.Element("NailClipr").Element("Settings").Element("PlayerDetection").Value);
                bool SOT = Convert.ToBoolean(xdoc.Element("NailClipr").Element("Settings").Element("StayOnTop").Value);
                float DS = float.Parse(xdoc.Element("NailClipr").Element("Settings").Element("DefaultSpeed").Value);

                //Update checkboxes
                NailClipr.GUI_PLAYER_DETECT.Checked = PD;
                Structs.settings.playerDetection = PD;

                NailClipr.GUI_TOPMOST.Checked = SOT;
                Structs.settings.topMostForm = PD;

                //Update Speed
                float speed = DS == 0 ? Structs.Speed.NATURAL : DS;
                Structs.player.speed.normal = speed;

                //Update Speed Label
                Functions.updateTrackSpeed(NailClipr.GUI_SPEED_DEFAULT_TRACK, NailClipr.GUI_DEFAULT_SPEED, speed);
            }
            catch (FileNotFoundException)
            {
                XML.create();
            }
        }
        public static void loadAreas()
        {
            try
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
                        Structs.zones.Add(z);
                    });
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is FileNotFoundException)
                    Directory.CreateDirectory("Resources");
                FNFerror("areas.xml");
            }
        }
        public static void loadWarps()
        {
            try
            {
                xdoc = XDocument.Load(SETTINGS);
                IEnumerable<XElement> allElements =
                from xEle in xdoc.Descendants("Locations")
                select xEle;

                foreach (XElement result in allElements)
                {
                    result.Descendants("WarpPoint").Select(t => new
                    {
                        zone = t.Parent.Attribute("id").Value,
                        title = t.Element("Title").Value,
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
                XML.create();
            }

        }
        public static void saveWarp(EliteAPI api)
        {
            try
            {
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

                Console.WriteLine("Saving warp.");

                //Updating - delete old and save new.
                int index = Structs.zonePoints.FindIndex(p => p.title == wp.title);
                if (index >= 0)
                {
                    Console.WriteLine("Editing Warp."); Console.ReadLine();
                    deleteWarp(api);
                }

                Structs.warpPoints.Add(wp);
                Functions.addZonePoint(wp);

                string zoneName = Structs.Zones.nameFromID(wp.zone);

                try
                {
                    string s = xdoc.Element("NailClipr").Element("Locations").Elements("Zone").Single(z => z.Attribute("id").Value == wp.zone + "").Value;
                }
                catch (System.InvalidOperationException)
                {
                    xdoc.Element("NailClipr").Element("Locations").Add(new XElement("Zone"));
                    xdoc.Element("NailClipr").Element("Locations").Element("Zone").Add(new XAttribute("id", wp.zone));
                    xdoc.Element("NailClipr").Element("Locations").Element("Zone").Add(new XAttribute("title", zoneName));
                    xdoc.Save(SETTINGS);
                }


                xdoc.Element("NailClipr").Elements("Locations").Elements("Zone").Single(z => z.Attribute("id").Value == wp.zone + "")
                    .Add(
                       new XElement("WarpPoint",
                       new XAttribute("title", wp.title),
                       new XElement("X", wp.pos.X),
                       new XElement("Y", wp.pos.Y),
                       new XElement("Z", wp.pos.Z)
                    ));
                xdoc.Save(SETTINGS);
            }
            catch (FileNotFoundException)
            {
                XML.create();
                XML.saveWarp(api);
            }
        }
        public static void deleteWarp(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.Text == "") return;

            Console.WriteLine("Deleting Warp.");

            Structs.WarpPoint delWP = Structs.zonePoints.Find(wp => wp.title == NailClipr.GUI_WARP.Text);

            if (delWP.Equals(default(Structs.WarpPoint)))
            {
                Console.WriteLine("No warp point with that name found.");
                return;
            }

            XElement delNode = xdoc.Descendants("Location")
               .Where(a => a.Element("Title").Value == NailClipr.GUI_WARP.Text && a.Element("Zone").Value == api.Player.ZoneId + "")
               .FirstOrDefault();
            delNode.Remove();
            xdoc.Save(SETTINGS);
            Structs.zonePoints.Remove(delWP);
            Structs.warpPoints.Remove(delWP);

            NailClipr.GUI_WARP.Items.Remove(delWP.title);
        }
        public static void FNFerror(string file)
        {
            MessageBox.Show("Please add " + file + " to the Resources directory.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
