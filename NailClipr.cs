using System;
using System.Diagnostics;
using System.Windows.Forms;
using EliteMMO.API;
using System.ComponentModel;
using System.Threading;
using NailClipr.Classes;

namespace NailClipr
{
    public partial class NailClipr : Form
    {
        private static EliteAPI api;
        private BackgroundWorker bw = new BackgroundWorker();
        private BackgroundWorker cw = new BackgroundWorker();

        public static Player Player = new Player();

        public static ComboBox GUI_WARP;
        public static Button GUI_WARP_BTN;
        public static CheckBox GUI_MAINT;
        public static CheckBox GUI_TOPMOST;
        public static CheckBox GUI_PLAYER_DETECT;
        public static Button GUI_ACCEPT;

        public static Label GUI_TARGET;
        public static Label GUI_NEAREST_PLAYER;
        public static Label GUI_STATUS;
        public static Label GUI_ZONE;
        public static Label GUI_DEFAULT_SPEED;
        public static Label GUI_SPEED;
        public static TrackBar GUI_SPEED_DEFAULT_TRACK;
        public static TrackBar GUI_SPEED_TRACK;

        public static Label GUI_SEARCH;
        public static Button GUI_FIND;
        public static Button GUI_ABORT;
        public static TextBox GUI_SEARCH_TARGET;

        public static WebBrowser GUI_WEB;

        public NailClipr()
        {
            Misc.SetVer();
            if (!Debugger.IsAttached) Misc.CheckUpdate();

            InitializeComponent();
            AssignControls();
            PostInit();

            api = Misc.SelectProcess(api);
            Text = Structs.App.name + " v." + Structs.App.ver + " - " + api.Player.Name;

            // Start the background worker..
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();

            // Start the background worker..
            cw.DoWork += new DoWorkEventHandler(cw_DoWork);
            cw.WorkerSupportsCancellation = true;
            cw.RunWorkerAsync();
        }

        private void PostInit()
        {
            XML.LoadAreas();
            XML.LoadWarps();
            XML.LoadSettings();
        }
        public void AssignControls()
        {
            GUI_WARP = CB_Warp;
            GUI_WARP_BTN = Btn_Warp;
            GUI_MAINT = ChkBox_Maint;
            GUI_TOPMOST = ChkBox_StayTop;
            GUI_PLAYER_DETECT = ChkBox_PlayerDetect;
            GUI_ACCEPT = Btn_Accept;

            GUI_TARGET = Lbl_TargetInfo;
            GUI_NEAREST_PLAYER = Lbl_NearestPlayer;
            GUI_STATUS = Lbl_Status;
            GUI_ZONE = Lbl_Zone;
            GUI_SPEED = Lbl_SpeedVar;
            GUI_DEFAULT_SPEED = Lbl_DefaultSpeed;
            GUI_SPEED_DEFAULT_TRACK = Bar_Speed_Default;
            GUI_SPEED_TRACK = Bar_Speed;

            GUI_SEARCH = Lbl_Search;
            GUI_SEARCH_TARGET = Txt_Search;
            GUI_FIND = Btn_Find;
            GUI_ABORT = Btn_Abort;

        }

        #region Threads
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending != true)
            {
                Thread.Sleep(100);
                bw.ReportProgress(0);

                Threads.Overwrites(api);
            }
        }
        private void cw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!worker.CancellationPending)
            {
                Thread.Sleep(100);
                Chat.Parse(api);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Threads.Update(api);
        }
        #endregion
        #region GUI
        #region CheckBoxes
        private void ChkBox_Maint_CheckedChanged(object sender, EventArgs e)
        {
            Player.MaintenanceMode(api, NailClipr.GUI_MAINT.Checked);
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
        #endregion
        #region Scrollbars
        private void Bar_Speed_Default_Scroll(object sender, EventArgs e)
        {
            float barVal = GUI_SPEED_DEFAULT_TRACK.Value / Structs.Speed.DIVISOR;
            float speed = barVal + Structs.Speed.NATURAL;
            Player.Speed.normal = speed;
            GUI_DEFAULT_SPEED.Text = "x" + speed / Structs.Speed.NATURAL;
        }
        private void Bar_Speed_Scroll(object sender, EventArgs e)
        {
            float barVal = GUI_SPEED_TRACK.Value / Structs.Speed.DIVISOR;
            float speed = barVal + Structs.Speed.NATURAL;

            //Fallback. Never can be too safe with speed mods.
            if (speed <= Structs.Speed.MAX)
            {
                Player.Speed.SetSpeed(api, speed);
                GUI_SPEED.Text = "x" + speed / Structs.Speed.NATURAL;
            }
        }
        #endregion
        #region PosBtns
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
        private void Btn_NW_Click(object sender, EventArgs e)
        {
            float PtX = api.Player.X;
            float PtX1 = PtX - Structs.Settings.POS_INC;
            float PtY = api.Player.Z;
            float PtY1 = PtY + Structs.Settings.POS_INC;
            float[] pts = Misc.MidPoint(PtX, PtX1, PtY, PtY1);
            api.Player.X = pts[0];
            api.Player.Y = pts[1];
        }
        private void Btn_SW_Click(object sender, EventArgs e)
        {
            float PtX = api.Player.X;
            float PtX1 = PtX - Structs.Settings.POS_INC;
            float PtY = api.Player.Z;
            float PtY1 = PtY - Structs.Settings.POS_INC;
            float[] pts = Misc.MidPoint(PtX, PtX1, PtY, PtY1);
            api.Player.X = pts[0];
            api.Player.Y = pts[1];
        }
        private void Btn_NE_Click(object sender, EventArgs e)
        {
            float PtX = api.Player.X;
            float PtX1 = PtX + Structs.Settings.POS_INC;
            float PtY = api.Player.Z;
            float PtY1 = PtY + Structs.Settings.POS_INC;
            float[] pts = Misc.MidPoint(PtX, PtX1, PtY, PtY1);
            api.Player.X = pts[0];
            api.Player.Y = pts[1];
        }
        private void Btn_SE_Click(object sender, EventArgs e)
        {
            float PtX = api.Player.X;
            float PtX1 = PtX + Structs.Settings.POS_INC;
            float PtY = api.Player.Z;
            float PtY1 = PtY - Structs.Settings.POS_INC;
            float[] pts = Misc.MidPoint(PtX, PtX1, PtY, PtY1);
            api.Player.X = pts[0];
            api.Player.Y = pts[1];
        }
        #endregion
        #region WarpBtns
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SharedFunctions.SaveWarp(api);
        }
        private void Btn_Warp_Click(object sender, EventArgs e)
        {
            SharedFunctions.Warp(api);
        }
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            SharedFunctions.DelWarp(api);
        }
        #endregion
        #region Settings
        private void Btn_SaveSettings_Click(object sender, EventArgs e)
        {
            XML.SaveSettings();
        }
        private void Btn_Accept_Click(object sender, EventArgs e)
        {
            SharedFunctions.Accept(api);
        }
        private void Btn_Req_Click(object sender, EventArgs e)
        {
            SharedFunctions.Request(api);
        }
        #endregion
        #region Search
        private void Btn_Find_Click(object sender, EventArgs e)
        {
            SharedFunctions.Search(api);
        }

        private void Btn_Abort_Click(object sender, EventArgs e)
        {
            SharedFunctions.Abort(api);
        }

        private void Txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Btn_Find_Click(this, new EventArgs());
            }
        }
        #endregion

        #endregion

    }
}



