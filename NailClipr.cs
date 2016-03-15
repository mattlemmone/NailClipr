using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using EliteMMO.API;
using System.ComponentModel;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public partial class NailClipr : Form
    {
        private static EliteAPI api;
        private BackgroundWorker bw = new BackgroundWorker();
        public Player player = new Player();
        public const string SETTINGS = "Settings.xml";
        public const float INC = 5.0f;
        public XDocument xdoc;
        public List<WarpPoint> warpPoints = new List<WarpPoint>();
        public List<WarpPoint> zonePoints = new List<WarpPoint>();
        public bool isZoning = false;

        public struct Position
        {
            public float X;
            public float Y;
            public float Z;
        }

        public struct WarpPoint
        {
            public string title;
            public Position pos;
            public int zone;
        }

        public struct Status
        {
            private uint oldStatus;
            public uint old
            {
                get { return oldStatus; }
                set { oldStatus = value; }
            }
            public const uint DEFAULT = 0;
            public const uint MAINT = 31;
        }

        public struct Speed
        {
            private float e;
            public float expected
            {
                get { return e; }
                set { e = value; }
            }
            public const float DEFAULT = 5f;
            public const float DIVISOR = 4f;
        }

        public class Player
        {
            public Speed speed = new Speed();
            public Status status = new Status();

            public void maintenanceMode(bool on)
            {
                if (!on)
                {
                    api.Player.Status = status.old;
                    return;

                }
                //Save status before switching.
                if (api.Player.Status == Status.MAINT)
                {
                    status.old = Status.DEFAULT;
                }
                else
                {
                    status.old = api.Player.Status;
                }

                //Maint on.
                api.Player.Status = Status.MAINT;
            }
            public void warp(Position p)
            {
                api.Player.X = p.X;
                api.Player.Y = p.Y;
                api.Player.Z = p.Z;
            }
        }

        public void createXML()
        {
            var xmlNode = new XElement("Locations");
            xmlNode.Save(SETTINGS);
            xdoc = XDocument.Load(SETTINGS);
        }

        public void loadXML()
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
                    Position pos = new Position();
                    pos.X = float.Parse(t.x);
                    pos.Y = float.Parse(t.y);
                    pos.Z = float.Parse(t.z);

                    WarpPoint wp = new WarpPoint();
                    wp.title = t.title;
                    wp.zone = int.Parse(t.zone);
                    wp.pos = pos;
                    warpPoints.Add(wp);
                });

            }

        }

        public void loadZonePoints()
        {
            CB_Warp.Items.Clear();
            zonePoints.Clear();
            warpPoints.ForEach(wp =>
            {
                if (wp.zone == api.Player.ZoneId)
                {
                    zonePoints.Add(wp);
                    CB_Warp.Items.Add(wp.title);
                }
            });
        }

        public NailClipr()
        {
            InitializeComponent();
            try
            {
                loadXML();
            }
            catch (FileNotFoundException)
            {
                createXML();
            }
            #region Final Fantasy XI [POL]
            var data = Process.GetProcessesByName("pol");

            if (data.Count() != 0)
            {
                var proc = Process.GetProcessesByName("pol").First().Id;
                api = new EliteAPI(proc);

                this.Text = "NailClipr - " + api.Entity.GetLocalPlayer().Name;
                
            }
            else
            {
                this.Text = "N/A";
            }
            #endregion

            loadZonePoints();
            // Start the background worker..
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (worker.CancellationPending != true)
            {
                System.Threading.Thread.Sleep(100);
                bw.ReportProgress(0);
                //Constantly write maintenance mode in case it gets overwritten.
                if (ChkBox_Maint.Checked == true)
                {
                    if (api.Player.Status != Status.MAINT)
                        api.Player.Status = Status.MAINT;
                }

                /*Speed*/
                //Not initialized.
                if (player.speed.expected == 0)
                    player.speed.expected = api.Player.Speed;

                //Prevent overwrite.
                if (api.Player.Speed != player.speed.expected)
                    api.Player.Speed = player.speed.expected;
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Update GUI.

            isZoning = api.Player.ZoneId == 0;            

            //Pos. Z and Y write correctly but read each other. Inherent issue.
            Lbl_X.Text = Math.Round(api.Player.X, 2) + "";
            Lbl_Y.Text = Math.Round(api.Player.Z, 2) + "";
            Lbl_Z.Text = Math.Round(api.Player.Y, 2) + "";

            //Zone and Status Label
            Lbl_Status.Text = api.Player.Status + "";
            Lbl_Zone.Text = api.Player.ZoneId + "";

            /*Speed*/
            //Update labels
            Lbl_SpeedVar.Text = "x" + api.Player.Speed / Speed.DEFAULT;
            float f = (api.Player.Speed - Speed.DEFAULT) * Speed.DIVISOR;
            int barSpeed = (int)Math.Ceiling(f);

            //If we aren't zoning...
            if (!isZoning)
                Bar_Speed.Value = (int)Math.Ceiling(f);
            else {
                while (isZoning)
                {
                    isZoning = api.Player.ZoneId == 0;
                    System.Threading.Thread.Sleep(100);
                }
                loadZonePoints();
            }

        }

        private void ChkBox_Maint_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBox_Maint.Checked)
            {
                player.maintenanceMode(true);
            }
            else
            {
                player.maintenanceMode(false);
            }
        }

        private void ChkBox_StayTop_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkBox_StayTop.Checked)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

        private void Bar_Speed_Scroll(object sender, EventArgs e)
        {
            float barVal = Bar_Speed.Value / Speed.DIVISOR;
            float speed = barVal + Speed.DEFAULT;
            player.speed.expected = speed;
            api.Player.Speed = speed;
        }


        private void Btn_Plus_X_Click(object sender, EventArgs e)
        {
            api.Player.X = api.Player.X + INC;
        }

        private void Btn_Minus_X_Click(object sender, EventArgs e)
        {

            api.Player.X = api.Player.X - INC;
        }

        private void Btn_Plus_Y_Click(object sender, EventArgs e)
        {

            api.Player.Y = api.Player.Z + INC;
        }

        private void Btn_Minus_Y_Click(object sender, EventArgs e)
        {

            api.Player.Y = api.Player.Z - INC;
        }

        private void Btn_Plus_Z_Click(object sender, EventArgs e)
        {

            api.Player.Z = api.Player.Y + INC;
        }

        private void Btn_Minus_Z_Click(object sender, EventArgs e)
        {

            api.Player.Z = api.Player.Y - INC;
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //http://www.c-sharpcorner.com/UploadFile/de41d6/learning-linq-made-easy-linq-to-xml-tutorial-3/
            
            Position pos = new Position();
            pos.X = api.Player.X;
            pos.Y = api.Player.Z;
            pos.Z = api.Player.Y;

            WarpPoint wp = new WarpPoint();
            if (CB_Warp.Text == "") { CB_Warp.Text = "Untitled Location"; wp.title = "Untitled Location"; }
            else { wp.title = CB_Warp.Text; }
            wp.zone = api.Player.ZoneId;
            wp.pos = pos;

            warpPoints.Add(wp);
            CB_Warp.Items.Add(wp.title);
            xdoc.Element("Locations").Add(
               new XElement("Location",
               new XElement("Zone", wp.zone),
               new XElement("Title", wp.title),
               new XElement("X", wp.pos.X),
               new XElement("Y", wp.pos.Y),
               new XElement("Z", wp.pos.Z)
            ));
            xdoc.Save(SETTINGS);

        }

        private void Btn_Warp_Click(object sender, EventArgs e)
        {
            WarpPoint nextWP = warpPoints.Find(wp => wp.title == CB_Warp.Text && wp.zone == api.Player.ZoneId);
            if (nextWP.zone == 0)
                return;

            player.maintenanceMode(true);
            System.Threading.Thread.Sleep(1000);
            
            player.warp(nextWP.pos);

            System.Threading.Thread.Sleep(1000);
            player.maintenanceMode(false);

        }
    }
}



