﻿using EliteMMO.API;
using System;
using System.Collections.Generic;
using System.Linq;
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
            xdoc.Element("NailClipr").Element("Settings").Element("PlayerDetection").Value = Structs.settings.playerDetection + "";
            xdoc.Element("NailClipr").Element("Settings").Element("StayOnTop").Value = Structs.settings.topMostForm + "";
            xdoc.Element("NailClipr").Element("Settings").Element("DefaultSpeed").Value = Structs.player.speed.normal + "";
            xdoc.Save(SETTINGS);
        }
        public static void loadSettings()
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
            float speed = DS;
            Structs.player.speed.normal = speed;

            //Update Speed Label
            Functions.updateTrackSpeed(NailClipr.GUI_SPEED_DEFAULT_TRACK, NailClipr.GUI_DEFAULT_SPEED, speed);

        }
        public static void loadAreas()
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
        public static void loadWarps()
        {
            xdoc = XDocument.Load(SETTINGS);
            IEnumerable<XElement> allElements =
            from xEle in xdoc.Descendants("Locations")
            select xEle;

            foreach (XElement result in allElements)
            {
                result.Descendants("Location").Select(t => new
                {
                    title = t.Element("Title").Value,
                    zone = t.Element("Zone").Value,
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
        public static void saveWarp(EliteAPI api)
        {
            //http://www.c-sharpcorner.com/UploadFile/de41d6/learning-linq-made-easy-linq-to-xml-tutorial-3/

            Structs.Position pos = new Structs.Position();
            pos.X = api.Player.X;
            pos.Y = api.Player.Z;
            pos.Z = api.Player.Y;

            Structs.WarpPoint wp = new Structs.WarpPoint();
            string warpText = NailClipr.GUI_WARP.Text;
            if (warpText == "") {  NailClipr.GUI_WARP.Text = "Untitled Location"; wp.title = "Untitled Location"; }
            else { wp.title = warpText; }
            wp.zone = api.Player.ZoneId;
            wp.pos = pos;

            Console.WriteLine("Saving warp.");

            //Updating - delete old and save new.
            int index = Structs.zonePoints.FindIndex(p => p.title == wp.title);
            if (index >= 0)
            {
                Console.WriteLine("Editing Warp.");Console.ReadLine();
                deleteWarp(api);
            }

            Structs.warpPoints.Add(wp);
            Functions.addZonePoint(wp);
            
            string zoneName = Structs.Zones.nameFromID(wp.zone);
            xdoc.Element("NailClipr").Element("Locations").Add(
               new XElement("Location",
               new XElement("ZoneName", zoneName),
               new XElement("Zone", wp.zone),
               new XElement("Title", wp.title),
               new XElement("X", wp.pos.X),
               new XElement("Y", wp.pos.Y),
               new XElement("Z", wp.pos.Z)
            ));
            xdoc.Save(SETTINGS);
        }        
        public static void deleteWarp(EliteAPI api)
        {
            if (NailClipr.GUI_WARP.Text == "") return;

            Console.WriteLine("Deleting Warp."); Console.ReadLine();
            XElement delNode = xdoc.Descendants("Location")
               .Where(a => a.Element("Title").Value == NailClipr.GUI_WARP.Text && a.Element("Zone").Value == api.Player.ZoneId + "")
               .FirstOrDefault();
            delNode.Remove();
            xdoc.Save(SETTINGS);

            Structs.WarpPoint delWP = Structs.zonePoints.Find(wp => wp.title == NailClipr.GUI_WARP.Text);

            Structs.zonePoints.Remove(delWP);
            Structs.warpPoints.Remove(delWP);

            NailClipr.GUI_WARP.Items.Remove(delWP.title);
        }
    }
}