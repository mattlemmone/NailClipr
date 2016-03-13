using EliteMMO.API;

namespace WindowsFormsApplication1
{
    partial class NailClipr
    {
        private EliteAPI api;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChkBox_Maint = new System.Windows.Forms.CheckBox();
            this.Btn_ZUp = new System.Windows.Forms.Button();
            this.Btn_ZDown = new System.Windows.Forms.Button();
            this.Bar_Speed = new System.Windows.Forms.TrackBar();
            this.Lbl_Speed = new System.Windows.Forms.Label();
            this.Lbl_SpeedVar = new System.Windows.Forms.Label();
            this.ChkBox_StayTop = new System.Windows.Forms.CheckBox();
            this.Lbl_Player = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed)).BeginInit();
            this.SuspendLayout();
            // 
            // ChkBox_Maint
            // 
            this.ChkBox_Maint.AutoSize = true;
            this.ChkBox_Maint.Location = new System.Drawing.Point(13, 13);
            this.ChkBox_Maint.Name = "ChkBox_Maint";
            this.ChkBox_Maint.Size = new System.Drawing.Size(118, 17);
            this.ChkBox_Maint.TabIndex = 0;
            this.ChkBox_Maint.Text = "Maintenance Mode";
            this.ChkBox_Maint.UseVisualStyleBackColor = true;
            this.ChkBox_Maint.CheckedChanged += new System.EventHandler(this.ChkBox_Maint_CheckedChanged);
            // 
            // Btn_ZUp
            // 
            this.Btn_ZUp.Location = new System.Drawing.Point(13, 37);
            this.Btn_ZUp.Name = "Btn_ZUp";
            this.Btn_ZUp.Size = new System.Drawing.Size(75, 23);
            this.Btn_ZUp.TabIndex = 1;
            this.Btn_ZUp.Text = "+ Z";
            this.Btn_ZUp.UseVisualStyleBackColor = true;
            this.Btn_ZUp.Click += new System.EventHandler(this.Btn_ZUp_Click);
            // 
            // Btn_ZDown
            // 
            this.Btn_ZDown.Location = new System.Drawing.Point(13, 66);
            this.Btn_ZDown.Name = "Btn_ZDown";
            this.Btn_ZDown.Size = new System.Drawing.Size(75, 23);
            this.Btn_ZDown.TabIndex = 2;
            this.Btn_ZDown.Text = "- Z";
            this.Btn_ZDown.UseVisualStyleBackColor = true;
            this.Btn_ZDown.Click += new System.EventHandler(this.Btn_ZDown_Click);
            // 
            // Bar_Speed
            // 
            this.Bar_Speed.Location = new System.Drawing.Point(103, 48);
            this.Bar_Speed.Name = "Bar_Speed";
            this.Bar_Speed.Size = new System.Drawing.Size(169, 45);
            this.Bar_Speed.TabIndex = 3;
            this.Bar_Speed.Scroll += new System.EventHandler(this.Bar_Speed_Scroll);
            // 
            // Lbl_Speed
            // 
            this.Lbl_Speed.AutoSize = true;
            this.Lbl_Speed.Location = new System.Drawing.Point(167, 37);
            this.Lbl_Speed.Name = "Lbl_Speed";
            this.Lbl_Speed.Size = new System.Drawing.Size(41, 13);
            this.Lbl_Speed.TabIndex = 4;
            this.Lbl_Speed.Text = "Speed:";
            // 
            // Lbl_SpeedVar
            // 
            this.Lbl_SpeedVar.AutoSize = true;
            this.Lbl_SpeedVar.Location = new System.Drawing.Point(215, 37);
            this.Lbl_SpeedVar.Name = "Lbl_SpeedVar";
            this.Lbl_SpeedVar.Size = new System.Drawing.Size(22, 13);
            this.Lbl_SpeedVar.TabIndex = 5;
            this.Lbl_SpeedVar.Text = "1.0";
            // 
            // ChkBox_StayTop
            // 
            this.ChkBox_StayTop.AutoSize = true;
            this.ChkBox_StayTop.Location = new System.Drawing.Point(138, 13);
            this.ChkBox_StayTop.Name = "ChkBox_StayTop";
            this.ChkBox_StayTop.Size = new System.Drawing.Size(84, 17);
            this.ChkBox_StayTop.TabIndex = 6;
            this.ChkBox_StayTop.Text = "Stay on Top";
            this.ChkBox_StayTop.UseVisualStyleBackColor = true;
            this.ChkBox_StayTop.CheckedChanged += new System.EventHandler(this.ChkBox_StayTop_CheckedChanged);
            // 
            // Lbl_Player
            // 
            this.Lbl_Player.AutoSize = true;
            this.Lbl_Player.Location = new System.Drawing.Point(236, 83);
            this.Lbl_Player.Name = "Lbl_Player";
            this.Lbl_Player.Size = new System.Drawing.Size(36, 13);
            this.Lbl_Player.TabIndex = 7;
            this.Lbl_Player.Text = "Player";
            // 
            // NailClipr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 105);
            this.Controls.Add(this.Lbl_Player);
            this.Controls.Add(this.ChkBox_StayTop);
            this.Controls.Add(this.Lbl_SpeedVar);
            this.Controls.Add(this.Lbl_Speed);
            this.Controls.Add(this.Bar_Speed);
            this.Controls.Add(this.Btn_ZDown);
            this.Controls.Add(this.Btn_ZUp);
            this.Controls.Add(this.ChkBox_Maint);
            this.Name = "NailClipr";
            this.Text = "NailClipr";
            this.Load += new System.EventHandler(this.NailClipr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkBox_Maint;
        private System.Windows.Forms.Button Btn_ZUp;
        private System.Windows.Forms.Button Btn_ZDown;
        private System.Windows.Forms.TrackBar Bar_Speed;
        private System.Windows.Forms.Label Lbl_Speed;
        private System.Windows.Forms.Label Lbl_SpeedVar;
        private System.Windows.Forms.CheckBox ChkBox_StayTop;
        private System.Windows.Forms.Label Lbl_Player;
    }
}

