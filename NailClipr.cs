using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using EliteMMO.API;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;

namespace NailClipr
{
    public partial class NailClipr : Form
    {
        private static EliteAPI api;
        private BackgroundWorker bw = new BackgroundWorker();

        public static ComboBox GUI_WARP;
        public static CheckBox GUI_TOPMOST;
        public static CheckBox GUI_PLAYER_DETECT;
        public static Label GUI_X;
        public static Label GUI_Y;
        public static Label GUI_Z;
        public static Label GUI_STATUS;
        public static Label GUI_ZONE;
        public static Label GUI_DEFAULT_SPEED;
        public static Label GUI_SPEED;
        public static TrackBar GUI_SPEED_DEFAULT_TRACK;
        public static TrackBar GUI_SPEED_TRACK;

        public NailClipr()
        {
            InitializeComponent();
            AssignControls();
            PostInit();
            selectProcess();

            // Start the background worker..
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();
        }
        public void PostInit()
        {
            try
            {
                XML.loadWarps();
                XML.loadSettings();
            }
            catch (FileNotFoundException)
            {
                XML.create();
            }
        }

        public void AssignControls()
        {
            GUI_WARP = CB_Warp;
            GUI_TOPMOST = ChkBox_StayTop;
            GUI_PLAYER_DETECT = ChkBox_PlayerDetect;
            GUI_X = Lbl_X;
            GUI_Y = Lbl_Y;
            GUI_Z = Lbl_Z;
            GUI_STATUS = Lbl_Status;
            GUI_ZONE = Lbl_Zone;
            GUI_SPEED = Lbl_SpeedVar;
            GUI_DEFAULT_SPEED = LBL_DefaultSpeed;
            GUI_SPEED_DEFAULT_TRACK = Bar_Speed_Default;
            GUI_SPEED_TRACK = Bar_Speed;
        }

        public void selectProcess()
        {
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
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending != true)
            {
                System.Threading.Thread.Sleep(100);
                bw.ReportProgress(0);

                workerOverwrites();
            }
        }

        public void workerOverwrites()
        {

            //Constantly write maintenance mode in case it gets overwritten.
            if (ChkBox_Maint.Checked == true)
            {
                if (api.Player.Status != Structs.Status.MAINT)
                    api.Player.Status = Structs.Status.MAINT;
            }

            /*Speed*/
            //Not initialized.
            if (Structs.player.speed.expected == 0)
            {
                Structs.player.speed.expected = api.Player.Speed;
            }

            //Turn speed off around other players.
            if (Structs.settings.playerDetection) Functions.playersRendered(api);

            //Adjust current speed.
            if (!Structs.player.isAlone && Structs.settings.playerDetection)
            {
                api.Player.Speed = Structs.player.speed.normal;
                if (api.Player.Speed != Structs.player.speed.normal)
                    api.Player.Speed = Structs.player.speed.normal;
            }
            else {
                //Prevent speed overwrite.
                if (api.Player.Speed != Structs.player.speed.expected)
                    api.Player.Speed = Structs.player.speed.expected;
            }

        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Update GUI.
            Structs.player.location.isZoning = api.Player.X == 0 && api.Player.Y == 0 && api.Player.Z == 0;

            Functions.updateLabels(api);
        }

        private void ChkBox_Maint_CheckedChanged(object sender, EventArgs e)
        {
            Structs.player.maintenanceMode(api, ChkBox_Maint.Checked);
        }

        private void ChkBox_DetectDisable_CheckedChanged(object sender, EventArgs e)
        {
            Structs.settings.playerDetection = ChkBox_PlayerDetect.Checked;
        }

        private void ChkBox_StayTop_CheckedChanged(object sender, EventArgs e)
        {
            Structs.settings.topMostForm = ChkBox_StayTop.Checked;
            this.TopMost = ChkBox_StayTop.Checked;
        }

        private void Bar_Speed_Default_Scroll(object sender, EventArgs e)
        {
            float barVal = GUI_SPEED_DEFAULT_TRACK.Value / Structs.Speed.DIVISOR;
            float speed = barVal + Structs.Speed.NATURAL;
            Structs.player.speed.normal = speed;
            GUI_DEFAULT_SPEED.Text = "x" + speed / Structs.Speed.NATURAL;
        }

        private void Bar_Speed_Scroll(object sender, EventArgs e)
        {
            float barVal = GUI_SPEED_TRACK.Value / Structs.Speed.DIVISOR;
            float speed = barVal + Structs.Speed.NATURAL;
            Structs.player.speed.expected = speed;
            api.Player.Speed = speed;
        }


        private void Btn_Plus_X_Click(object sender, EventArgs e)
        {
            api.Player.X = api.Player.X + Structs.Settings.POS_INC;
        }

        private void Btn_Minus_X_Click(object sender, EventArgs e)
        {

            api.Player.X = api.Player.X - Structs.Settings.POS_INC;
        }

        private void Btn_Plus_Y_Click(object sender, EventArgs e)
        {

            api.Player.Y = api.Player.Z + Structs.Settings.POS_INC;
        }

        private void Btn_Minus_Y_Click(object sender, EventArgs e)
        {

            api.Player.Y = api.Player.Z - Structs.Settings.POS_INC;
        }

        private void Btn_Plus_Z_Click(object sender, EventArgs e)
        {

            api.Player.Z = api.Player.Y + Structs.Settings.POS_INC;
        }

        private void Btn_Minus_Z_Click(object sender, EventArgs e)
        {

            api.Player.Z = api.Player.Y - Structs.Settings.POS_INC;
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            XML.saveWarp(api);
        }

        private void Btn_Warp_Click(object sender, EventArgs e)
        {
            Structs.player.warp(api);
        }

        private void NailClipr_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            XML.deleteWarp(api);
        }

        private void Btn_SaveSettings_Click(object sender, EventArgs e)
        {
            XML.saveSettings();
        }
    }
}



