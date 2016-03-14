using EliteMMO.API;
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
            this.Bar_Speed = new System.Windows.Forms.TrackBar();
            this.Lbl_Speed = new System.Windows.Forms.Label();
            this.Lbl_SpeedVar = new System.Windows.Forms.Label();
            this.ChkBox_StayTop = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_Status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Lbl_Zone = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Lbl_X = new System.Windows.Forms.Label();
            this.Btn_Plus_X = new System.Windows.Forms.Button();
            this.Btn_Minus_X = new System.Windows.Forms.Button();
            this.Btn_Minus_Y = new System.Windows.Forms.Button();
            this.Btn_Plus_Y = new System.Windows.Forms.Button();
            this.Lbl_Y = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Btn_Minus_Z = new System.Windows.Forms.Button();
            this.Btn_Plus_Z = new System.Windows.Forms.Button();
            this.Lbl_Z = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkBox_Maint
            // 
            this.ChkBox_Maint.AutoSize = true;
            this.ChkBox_Maint.Location = new System.Drawing.Point(154, 18);
            this.ChkBox_Maint.Name = "ChkBox_Maint";
            this.ChkBox_Maint.Size = new System.Drawing.Size(118, 17);
            this.ChkBox_Maint.TabIndex = 0;
            this.ChkBox_Maint.Text = "Maintenance Mode";
            this.ChkBox_Maint.UseVisualStyleBackColor = true;
            this.ChkBox_Maint.CheckedChanged += new System.EventHandler(this.ChkBox_Maint_CheckedChanged);
            // 
            // Bar_Speed
            // 
            this.Bar_Speed.Location = new System.Drawing.Point(13, 142);
            this.Bar_Speed.Name = "Bar_Speed";
            this.Bar_Speed.Size = new System.Drawing.Size(259, 45);
            this.Bar_Speed.TabIndex = 3;
            this.Bar_Speed.Scroll += new System.EventHandler(this.Bar_Speed_Scroll);
            // 
            // Lbl_Speed
            // 
            this.Lbl_Speed.AutoSize = true;
            this.Lbl_Speed.Location = new System.Drawing.Point(108, 190);
            this.Lbl_Speed.Name = "Lbl_Speed";
            this.Lbl_Speed.Size = new System.Drawing.Size(41, 13);
            this.Lbl_Speed.TabIndex = 4;
            this.Lbl_Speed.Text = "Speed:";
            // 
            // Lbl_SpeedVar
            // 
            this.Lbl_SpeedVar.AutoSize = true;
            this.Lbl_SpeedVar.Location = new System.Drawing.Point(155, 190);
            this.Lbl_SpeedVar.Name = "Lbl_SpeedVar";
            this.Lbl_SpeedVar.Size = new System.Drawing.Size(22, 13);
            this.Lbl_SpeedVar.TabIndex = 5;
            this.Lbl_SpeedVar.Text = "1.0";
            // 
            // ChkBox_StayTop
            // 
            this.ChkBox_StayTop.AutoSize = true;
            this.ChkBox_StayTop.Location = new System.Drawing.Point(154, 41);
            this.ChkBox_StayTop.Name = "ChkBox_StayTop";
            this.ChkBox_StayTop.Size = new System.Drawing.Size(84, 17);
            this.ChkBox_StayTop.TabIndex = 6;
            this.ChkBox_StayTop.Text = "Stay on Top";
            this.ChkBox_StayTop.UseVisualStyleBackColor = true;
            this.ChkBox_StayTop.CheckedChanged += new System.EventHandler(this.ChkBox_StayTop_CheckedChanged);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "X:";
            // 
            // Lbl_X
            // 
            this.Lbl_X.AutoSize = true;
            this.Lbl_X.Location = new System.Drawing.Point(21, 20);
            this.Lbl_X.Name = "Lbl_X";
            this.Lbl_X.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Lbl_X.Size = new System.Drawing.Size(25, 13);
            this.Lbl_X.TabIndex = 21;
            this.Lbl_X.Text = "123";
            // 
            // Btn_Plus_X
            // 
            this.Btn_Plus_X.Location = new System.Drawing.Point(60, 15);
            this.Btn_Plus_X.Name = "Btn_Plus_X";
            this.Btn_Plus_X.Size = new System.Drawing.Size(24, 23);
            this.Btn_Plus_X.TabIndex = 22;
            this.Btn_Plus_X.Text = "+";
            this.Btn_Plus_X.UseVisualStyleBackColor = true;
            this.Btn_Plus_X.Click += new System.EventHandler(this.Btn_Plus_X_Click);
            // 
            // Btn_Minus_X
            // 
            this.Btn_Minus_X.Location = new System.Drawing.Point(90, 15);
            this.Btn_Minus_X.Name = "Btn_Minus_X";
            this.Btn_Minus_X.Size = new System.Drawing.Size(24, 23);
            this.Btn_Minus_X.TabIndex = 23;
            this.Btn_Minus_X.Text = "-";
            this.Btn_Minus_X.UseVisualStyleBackColor = true;
            this.Btn_Minus_X.Click += new System.EventHandler(this.Btn_Minus_X_Click);
            // 
            // Btn_Minus_Y
            // 
            this.Btn_Minus_Y.Location = new System.Drawing.Point(90, 41);
            this.Btn_Minus_Y.Name = "Btn_Minus_Y";
            this.Btn_Minus_Y.Size = new System.Drawing.Size(24, 23);
            this.Btn_Minus_Y.TabIndex = 27;
            this.Btn_Minus_Y.Text = "-";
            this.Btn_Minus_Y.UseVisualStyleBackColor = true;
            this.Btn_Minus_Y.Click += new System.EventHandler(this.Btn_Minus_Y_Click);
            // 
            // Btn_Plus_Y
            // 
            this.Btn_Plus_Y.Location = new System.Drawing.Point(60, 41);
            this.Btn_Plus_Y.Name = "Btn_Plus_Y";
            this.Btn_Plus_Y.Size = new System.Drawing.Size(24, 23);
            this.Btn_Plus_Y.TabIndex = 26;
            this.Btn_Plus_Y.Text = "+";
            this.Btn_Plus_Y.UseVisualStyleBackColor = true;
            this.Btn_Plus_Y.Click += new System.EventHandler(this.Btn_Plus_Y_Click);
            // 
            // Lbl_Y
            // 
            this.Lbl_Y.AutoSize = true;
            this.Lbl_Y.Location = new System.Drawing.Point(21, 46);
            this.Lbl_Y.Name = "Lbl_Y";
            this.Lbl_Y.Size = new System.Drawing.Size(25, 13);
            this.Lbl_Y.TabIndex = 25;
            this.Lbl_Y.Text = "123";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Y:";
            // 
            // Btn_Minus_Z
            // 
            this.Btn_Minus_Z.Location = new System.Drawing.Point(90, 67);
            this.Btn_Minus_Z.Name = "Btn_Minus_Z";
            this.Btn_Minus_Z.Size = new System.Drawing.Size(24, 23);
            this.Btn_Minus_Z.TabIndex = 31;
            this.Btn_Minus_Z.Text = "-";
            this.Btn_Minus_Z.UseVisualStyleBackColor = true;
            this.Btn_Minus_Z.Click += new System.EventHandler(this.Btn_Minus_Z_Click);
            // 
            // Btn_Plus_Z
            // 
            this.Btn_Plus_Z.Location = new System.Drawing.Point(60, 67);
            this.Btn_Plus_Z.Name = "Btn_Plus_Z";
            this.Btn_Plus_Z.Size = new System.Drawing.Size(24, 23);
            this.Btn_Plus_Z.TabIndex = 30;
            this.Btn_Plus_Z.Text = "+";
            this.Btn_Plus_Z.UseVisualStyleBackColor = true;
            this.Btn_Plus_Z.Click += new System.EventHandler(this.Btn_Plus_Z_Click);
            // 
            // Lbl_Z
            // 
            this.Lbl_Z.AutoSize = true;
            this.Lbl_Z.Location = new System.Drawing.Point(21, 72);
            this.Lbl_Z.Name = "Lbl_Z";
            this.Lbl_Z.Size = new System.Drawing.Size(25, 13);
            this.Lbl_Z.TabIndex = 29;
            this.Lbl_Z.Text = "123";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Z:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lbl_X);
            this.groupBox1.Controls.Add(this.Btn_Minus_Z);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Btn_Plus_Z);
            this.groupBox1.Controls.Add(this.Btn_Plus_X);
            this.groupBox1.Controls.Add(this.Lbl_Z);
            this.groupBox1.Controls.Add(this.Btn_Minus_X);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Btn_Minus_Y);
            this.groupBox1.Controls.Add(this.Lbl_Y);
            this.groupBox1.Controls.Add(this.Btn_Plus_Y);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 100);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
            // 
            // NailClipr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 209);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Lbl_Zone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Lbl_Status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ChkBox_StayTop);
            this.Controls.Add(this.Lbl_SpeedVar);
            this.Controls.Add(this.Lbl_Speed);
            this.Controls.Add(this.Bar_Speed);
            this.Controls.Add(this.ChkBox_Maint);
            this.Name = "NailClipr";
            this.Text = "NailClipr";
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkBox_Maint;
        private System.Windows.Forms.TrackBar Bar_Speed;
        private System.Windows.Forms.Label Lbl_Speed;
        private System.Windows.Forms.Label Lbl_SpeedVar;
        private System.Windows.Forms.CheckBox ChkBox_StayTop;
        private Label Lbl_Status;
        private Label label2;
        private Label label3;
        private Label Lbl_Zone;
        private Label label1;
        private Label Lbl_X;
        private Button Btn_Plus_X;
        private Button Btn_Minus_X;
        private Button Btn_Minus_Y;
        private Button Btn_Plus_Y;
        private Label Lbl_Y;
        private Label label6;
        private Button Btn_Minus_Z;
        private Button Btn_Plus_Z;
        private Label Lbl_Z;
        private Label label8;
        private GroupBox groupBox1;
    }
}

