using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using EliteMMO.API;

namespace WindowsFormsApplication1
{
    public partial class NailClipr : Form
    {
        private EliteAPI api;
        public uint oldStatus = 0;
        public const float Z_INC = 5.0f;
        public const float defaultSpeed = 5f;

        public struct Statuses
        {
            public const uint DEFAULT = 31;
            public const uint MAINT = 31;
        }

        public NailClipr(EliteAPI core)
        {
            this.api = core;
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
