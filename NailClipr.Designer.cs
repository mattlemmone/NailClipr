﻿using EliteMMO.API;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    partial class NailClipr
    {
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
            this.label1 = new System.Windows.Forms.Label();
            this.Lbl_Z = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_Status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Lbl_Zone = new System.Windows.Forms.Label();
            this.Btn_YDown = new System.Windows.Forms.Button();
            this.Btn_YUp = new System.Windows.Forms.Button();
            this.Btn_XDown = new System.Windows.Forms.Button();
            this.Btn_XUp = new System.Windows.Forms.Button();
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
            this.Btn_ZUp.Location = new System.Drawing.Point(197, 45);
            this.Btn_ZUp.Name = "Btn_ZUp";
            this.Btn_ZUp.Size = new System.Drawing.Size(75, 23);
            this.Btn_ZUp.TabIndex = 1;
            this.Btn_ZUp.Text = "+ Z";
            this.Btn_ZUp.UseVisualStyleBackColor = true;
            this.Btn_ZUp.Click += new System.EventHandler(this.Btn_ZUp_Click);
            // 
            // Btn_ZDown
            // 
            this.Btn_ZDown.Location = new System.Drawing.Point(197, 74);
            this.Btn_ZDown.Name = "Btn_ZDown";
            this.Btn_ZDown.Size = new System.Drawing.Size(75, 23);
            this.Btn_ZDown.TabIndex = 2;
            this.Btn_ZDown.Text = "- Z";
            this.Btn_ZDown.UseVisualStyleBackColor = true;
            this.Btn_ZDown.Click += new System.EventHandler(this.Btn_ZDown_Click);
            // 
            // Bar_Speed
            // 
            this.Bar_Speed.Location = new System.Drawing.Point(13, 117);
            this.Bar_Speed.Name = "Bar_Speed";
            this.Bar_Speed.Size = new System.Drawing.Size(259, 45);
            this.Bar_Speed.TabIndex = 3;
            this.Bar_Speed.Scroll += new System.EventHandler(this.Bar_Speed_Scroll);
            // 
            // Lbl_Speed
            // 
            this.Lbl_Speed.AutoSize = true;
            this.Lbl_Speed.Location = new System.Drawing.Point(117, 149);
            this.Lbl_Speed.Name = "Lbl_Speed";
            this.Lbl_Speed.Size = new System.Drawing.Size(41, 13);
            this.Lbl_Speed.TabIndex = 4;
            this.Lbl_Speed.Text = "Speed:";
            // 
            // Lbl_SpeedVar
            // 
            this.Lbl_SpeedVar.AutoSize = true;
            this.Lbl_SpeedVar.Location = new System.Drawing.Point(161, 149);
            this.Lbl_SpeedVar.Name = "Lbl_SpeedVar";
            this.Lbl_SpeedVar.Size = new System.Drawing.Size(22, 13);
            this.Lbl_SpeedVar.TabIndex = 5;
            this.Lbl_SpeedVar.Text = "1.0";
            // 
            // ChkBox_StayTop
            // 
            this.ChkBox_StayTop.AutoSize = true;
            this.ChkBox_StayTop.Location = new System.Drawing.Point(188, 13);
            this.ChkBox_StayTop.Name = "ChkBox_StayTop";
            this.ChkBox_StayTop.Size = new System.Drawing.Size(84, 17);
            this.ChkBox_StayTop.TabIndex = 6;
            this.ChkBox_StayTop.Text = "Stay on Top";
            this.ChkBox_StayTop.UseVisualStyleBackColor = true;
            this.ChkBox_StayTop.CheckedChanged += new System.EventHandler(this.ChkBox_StayTop_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Z:";
            // 
            // Lbl_Z
            // 
            this.Lbl_Z.AutoSize = true;
            this.Lbl_Z.Location = new System.Drawing.Point(145, 190);
            this.Lbl_Z.Name = "Lbl_Z";
            this.Lbl_Z.Size = new System.Drawing.Size(13, 13);
            this.Lbl_Z.TabIndex = 8;
            this.Lbl_Z.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Status:";
            // 
            // Lbl_Status
            // 
            this.Lbl_Status.AutoSize = true;
            this.Lbl_Status.Location = new System.Drawing.Point(54, 190);
            this.Lbl_Status.Name = "Lbl_Status";
            this.Lbl_Status.Size = new System.Drawing.Size(13, 13);
            this.Lbl_Status.TabIndex = 10;
            this.Lbl_Status.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Zone:";
            // 
            // Lbl_Zone
            // 
            this.Lbl_Zone.AutoSize = true;
            this.Lbl_Zone.Location = new System.Drawing.Point(247, 190);
            this.Lbl_Zone.Name = "Lbl_Zone";
            this.Lbl_Zone.Size = new System.Drawing.Size(25, 13);
            this.Lbl_Zone.TabIndex = 12;
            this.Lbl_Zone.Text = "123";
            // 
            // Btn_YDown
            // 
            this.Btn_YDown.Location = new System.Drawing.Point(104, 74);
            this.Btn_YDown.Name = "Btn_YDown";
            this.Btn_YDown.Size = new System.Drawing.Size(75, 23);
            this.Btn_YDown.TabIndex = 14;
            this.Btn_YDown.Text = "- Y";
            this.Btn_YDown.UseVisualStyleBackColor = true;
            this.Btn_YDown.Click += new System.EventHandler(this.Btn_YDown_Click);
            // 
            // Btn_YUp
            // 
            this.Btn_YUp.Location = new System.Drawing.Point(104, 45);
            this.Btn_YUp.Name = "Btn_YUp";
            this.Btn_YUp.Size = new System.Drawing.Size(75, 23);
            this.Btn_YUp.TabIndex = 13;
            this.Btn_YUp.Text = "+ Y";
            this.Btn_YUp.UseVisualStyleBackColor = true;
            this.Btn_YUp.Click += new System.EventHandler(this.Btn_YUp_Click);
            // 
            // Btn_XDown
            // 
            this.Btn_XDown.Location = new System.Drawing.Point(12, 74);
            this.Btn_XDown.Name = "Btn_XDown";
            this.Btn_XDown.Size = new System.Drawing.Size(75, 23);
            this.Btn_XDown.TabIndex = 16;
            this.Btn_XDown.Text = "- X";
            this.Btn_XDown.UseVisualStyleBackColor = true;
            this.Btn_XDown.Click += new System.EventHandler(this.Btn_XDown_Click);
            // 
            // Btn_XUp
            // 
            this.Btn_XUp.Location = new System.Drawing.Point(12, 45);
            this.Btn_XUp.Name = "Btn_XUp";
            this.Btn_XUp.Size = new System.Drawing.Size(75, 23);
            this.Btn_XUp.TabIndex = 15;
            this.Btn_XUp.Text = "+ X";
            this.Btn_XUp.UseVisualStyleBackColor = true;
            this.Btn_XUp.Click += new System.EventHandler(this.Btn_XUp_Click);
            // 
            // NailClipr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 209);
            this.Controls.Add(this.Btn_XDown);
            this.Controls.Add(this.Btn_XUp);
            this.Controls.Add(this.Btn_YDown);
            this.Controls.Add(this.Btn_YUp);
            this.Controls.Add(this.Lbl_Zone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Lbl_Status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lbl_Z);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChkBox_StayTop);
            this.Controls.Add(this.Lbl_SpeedVar);
            this.Controls.Add(this.Lbl_Speed);
            this.Controls.Add(this.Bar_Speed);
            this.Controls.Add(this.Btn_ZDown);
            this.Controls.Add(this.Btn_ZUp);
            this.Controls.Add(this.ChkBox_Maint);
            this.Name = "NailClipr";
            this.Text = "NailClipr";
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
        private System.Windows.Forms.Label label1;
        private Label Lbl_Z;
        private Label Lbl_Status;
        private Label label2;
        private Label label3;
        private Label Lbl_Zone;
        private Button Btn_YDown;
        private Button Btn_YUp;
        private Button Btn_XDown;
        private Button Btn_XUp;
    }
}

