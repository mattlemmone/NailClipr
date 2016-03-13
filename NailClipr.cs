using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using EliteMMO.API;

namespace WindowsFormsApplication1
{
    public partial class NailClipr : Form
    {
        private EliteAPI core;

        public void MainWindow(EliteAPI core)
        {
            InitializeComponent();
            api = core;

            #region Final Fantasy XI [POL]
            var data = Process.GetProcessesByName("pol");

            if (data.Count() != 0)
            {
                var proc = Process.GetProcessesByName("pol").First().Id;
                api = new EliteAPI(proc);

                Lbl_Player.Text = api.Entity.GetLocalPlayer().Name;
            }
            else
            {
                Lbl_Player.Text = "N/A";
            }
            #endregion

            
        }

        public NailClipr(EliteAPI core)
        {
            this.core = core;
            MainWindow(core);
            InitializeComponent();
        }

        private void ChkBox_Maint_CheckedChanged(object sender, EventArgs e)
        {
            
           // Console.Write(api.Player.Status);
        }

        private void Btn_ZUp_Click(object sender, EventArgs e)
        {

        }

        private void NailClipr_Load(object sender, EventArgs e)
        {

        }

        private void Btn_ZDown_Click(object sender, EventArgs e)
        {

        }

        private void Bar_Speed_Scroll(object sender, EventArgs e)
        {

        }

        private void ChkBox_StayTop_CheckedChanged(object sender, EventArgs e)
        {

        }


    }

  
}
