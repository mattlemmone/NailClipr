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
        public uint oldStatus = 0;
        public const float Z_INC = 3.0f;
        public const float defaultSpeed = 5f;
        private BackgroundWorker bw = new BackgroundWorker();

        public struct Statuses
        {
            public const uint DEFAULT = 0;
            public const uint MAINT = 31;
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
            while(worker.CancellationPending != true)
            {
                System.Threading.Thread.Sleep(100);
                bw.ReportProgress(1);

                if (ChkBox_Maint.Checked == true)
                {
                    if (api.Player.Status != Statuses.MAINT)
                        api.Player.Status = Statuses.MAINT;
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Lbl_Z.Text = api.Player.Y + "";
            Lbl_Status.Text = api.Player.Status + "";
        }

        private void ChkBox_Maint_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBox_Maint.Checked)
            {
                //Save status before switching.
                if (api.Player.Status == Statuses.MAINT)
                {
                    oldStatus = Statuses.DEFAULT;
                }
                else {
                    oldStatus = api.Player.Status;
                }

                //Maint on.
                api.Player.Status = Statuses.MAINT;
            }
            else {
                api.Player.Status = oldStatus;
            }
        }

        private void ChkBox_StayTop_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBox_StayTop.Checked)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

        private void Btn_ZUp_Click(object sender, EventArgs e)
        {
            float Z = api.Player.Z;
            api.Player.Z = Z - Z_INC;
        }

        private void Btn_ZDown_Click(object sender, EventArgs e)
        {
            float Z = api.Player.Z;
            api.Player.Z = Z + Z_INC;
        }

        private void Bar_Speed_Scroll(object sender, EventArgs e)
        {
            float barVal = Bar_Speed.Value / 4.0f;
            float speed = barVal + defaultSpeed;
            api.Player.Speed = speed;
            Lbl_SpeedVar.Text = api.Player.Speed / defaultSpeed + "";
        }


    }


}
