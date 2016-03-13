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
        private Status playerStatus = new Status();

        struct Position
        {
            public const float INC = 2.0f;
        }

        struct Status
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

        struct Speed
        {
            public const float DEFAULT = 5f;
            public const float DIVISOR = 4f;
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

            Lbl_Z.Text = Math.Round(api.Player.Y, 2) + "";

            Lbl_Status.Text = api.Player.Status + "";
            Lbl_Zone.Text = api.Player.ZoneId + "";

            //Speed
            Lbl_SpeedVar.Text = api.Player.Speed / Speed.DEFAULT + "";
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
                    playerStatus.old = Status.DEFAULT;
                }
                else {
                    playerStatus.old = api.Player.Status;
                }

                //Maint on.
                api.Player.Status = Status.MAINT;
            }
            else {
                api.Player.Status = playerStatus.old;
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
            api.Player.Speed = speed;
            Lbl_SpeedVar.Text = api.Player.Speed / Speed.DEFAULT + "";
        }

        private void Btn_ZUp_Click(object sender, EventArgs e)
        {
            float Z = api.Player.Z;
            api.Player.Z = Z - Position.INC;
        }

        private void Btn_ZDown_Click(object sender, EventArgs e)
        {
            float Z = api.Player.Z;
            api.Player.Z = Z + Position.INC;
        }

        private void Btn_XUp_Click(object sender, EventArgs e)
        {
            float X = api.Player.X;
            api.Player.X = X - Position.INC;
        }

        private void Btn_XDown_Click(object sender, EventArgs e)
        {
            float X = api.Player.X;
            api.Player.X = X + Position.INC;

        }

        private void Btn_YUp_Click(object sender, EventArgs e)
        {
            float Y = api.Player.Y;
            api.Player.Y = Y - Position.INC;
        }

        private void Btn_YDown_Click(object sender, EventArgs e)
        {
            float Y = api.Player.Y;
            api.Player.Y = Y + Position.INC;

        }
    }


}
