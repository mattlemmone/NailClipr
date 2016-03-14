using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using EliteMMO.API;
using System.ComponentModel;

namespace WindowsFormsApplication1
{
    public partial class NailClipr : Form
    {
        private static EliteAPI api;
        private BackgroundWorker bw = new BackgroundWorker();
        public Player player = new Player();

        public struct Position
        {
            public const float INC = 5.0f;
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
        }

        public NailClipr()
        {
            InitializeComponent();
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
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Pos. Z and Y write correctly but read each other. Inherent issue.
            Lbl_X.Text = Math.Round(api.Player.X, 2) + "";
            Lbl_Y.Text = Math.Round(api.Player.Z, 2) + "";
            Lbl_Z.Text = Math.Round(api.Player.Y, 2) + "";

            //Zone and Status Label
            Lbl_Status.Text = api.Player.Status + "";
            Lbl_Zone.Text = api.Player.ZoneId + "";

            /*Speed*/
            //Not initialized.
            if (player.speed.expected == 0)
                player.speed.expected = api.Player.Speed;

            //Prevent overwrite
            if (api.Player.Speed != player.speed.expected)
                api.Player.Speed = player.speed.expected;

            //Update labels
            Lbl_SpeedVar.Text = "x" + api.Player.Speed / Speed.DEFAULT;
            float f = (api.Player.Speed - Speed.DEFAULT) * Speed.DIVISOR;
            int barSpeed = (int)Math.Ceiling(f);

            //If we aren't zoning...
            if (barSpeed > 0)
                Bar_Speed.Value = (int)Math.Ceiling(f);

        }

        private void ChkBox_Maint_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBox_Maint.Checked)
            {
                //Save status before switching.
                if (api.Player.Status == Status.MAINT)
                {
                    player.status.old = Status.DEFAULT;
                }
                else {
                    player.status.old = api.Player.Status;
                }

                //Maint on.
                api.Player.Status = Status.MAINT;
            }
            else {
                api.Player.Status = player.status.old;
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
            api.Player.X = api.Player.X + Position.INC;
        }

        private void Btn_Minus_X_Click(object sender, EventArgs e)
        {

            api.Player.X = api.Player.X - Position.INC;
        }

        private void Btn_Plus_Y_Click(object sender, EventArgs e)
        {

            api.Player.Y = api.Player.Z + Position.INC;
        }

        private void Btn_Minus_Y_Click(object sender, EventArgs e)
        {

            api.Player.Y = api.Player.Z - Position.INC;
        }

        private void Btn_Plus_Z_Click(object sender, EventArgs e)
        {

            api.Player.Z = api.Player.Y + Position.INC;
        }

        private void Btn_Minus_Z_Click(object sender, EventArgs e)
        {

            api.Player.Z = api.Player.Y - Position.INC;
        }
    }


}
