using EliteMMO.API;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NailClipr
{
    class XML
    {
        public const string SETTINGS = "Settings.xml";
        public static XDocument xdoc;
        public static void create()
        {
            var xmlNode = new XElement("Locations");
            xmlNode.Save(SETTINGS);
            xdoc = XDocument.Load(SETTINGS);
        }

        public static void load()
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
        public static void save(EliteAPI api)
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

            Structs.warpPoints.Add(wp);
            Functions.addZonePoint(wp.title);
            XML.xdoc.Element("Locations").Add(
               new XElement("Location",
               new XElement("Zone", wp.zone),
               new XElement("Title", wp.title),
               new XElement("X", wp.pos.X),
               new XElement("Y", wp.pos.Y),
               new XElement("Z", wp.pos.Z)
            ));
            XML.xdoc.Save(SETTINGS);
        }
    }
}
