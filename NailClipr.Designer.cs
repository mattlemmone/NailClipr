using EliteMMO.API;
using System.Windows.Forms;
using System;

namespace NailClipr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NailClipr));
            this.ChkBox_Maint = new System.Windows.Forms.CheckBox();
            this.Bar_Speed = new System.Windows.Forms.TrackBar();
            this.Lbl_Speed = new System.Windows.Forms.Label();
            this.Lbl_SpeedVar = new System.Windows.Forms.Label();
            this.ChkBox_PlayerDetect = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_Status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Lbl_Zone = new System.Windows.Forms.Label();
            this.Btn_Plus_X = new System.Windows.Forms.Button();
            this.Btn_Minus_X = new System.Windows.Forms.Button();
            this.Btn_Minus_Y = new System.Windows.Forms.Button();
            this.Btn_Plus_Y = new System.Windows.Forms.Button();
            this.Btn_Minus_Z = new System.Windows.Forms.Button();
            this.Btn_Plus_Z = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_SE = new System.Windows.Forms.Button();
            this.Btn_NE = new System.Windows.Forms.Button();
            this.Btn_SW = new System.Windows.Forms.Button();
            this.Btn_NW = new System.Windows.Forms.Button();
            this.CB_Warp = new System.Windows.Forms.ComboBox();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_Warp = new System.Windows.Forms.Button();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.ChkBox_StayTop = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Lbl_DefaultSpeed = new System.Windows.Forms.Label();
            this.Btn_SaveSettings = new System.Windows.Forms.Button();
            this.Bar_Speed_Default = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.Lbl_TargetInfo = new System.Windows.Forms.Label();
            this.Lbl_NearestPlayer = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Btn_Accept = new System.Windows.Forms.Button();
            this.Btn_Req = new System.Windows.Forms.Button();
            this.Txt_Search = new System.Windows.Forms.TextBox();
            this.Btn_Find = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Lbl_Search = new System.Windows.Forms.Label();
            this.Btn_Abort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed_Default)).BeginInit();
            this.SuspendLayout();
            // 
            // ChkBox_Maint
            // 
            this.ChkBox_Maint.AutoSize = true;
            this.ChkBox_Maint.Location = new System.Drawing.Point(180, 29);
            this.ChkBox_Maint.Name = "ChkBox_Maint";
            this.ChkBox_Maint.Size = new System.Drawing.Size(126, 18);
            this.ChkBox_Maint.TabIndex = 0;
            this.ChkBox_Maint.Text = "Maintenance Mode";
            this.ChkBox_Maint.UseVisualStyleBackColor = true;
            this.ChkBox_Maint.CheckedChanged += new System.EventHandler(this.ChkBox_Maint_CheckedChanged);
            // 
            // Bar_Speed
            // 
            this.Bar_Speed.Location = new System.Drawing.Point(15, 293);
            this.Bar_Speed.Name = "Bar_Speed";
            this.Bar_Speed.Size = new System.Drawing.Size(302, 45);
            this.Bar_Speed.TabIndex = 3;
            this.Bar_Speed.Scroll += new System.EventHandler(this.Bar_Speed_Scroll);
            // 
            // Lbl_Speed
            // 
            this.Lbl_Speed.AutoSize = true;
            this.Lbl_Speed.Location = new System.Drawing.Point(126, 333);
            this.Lbl_Speed.Name = "Lbl_Speed";
            this.Lbl_Speed.Size = new System.Drawing.Size(45, 14);
            this.Lbl_Speed.TabIndex = 4;
            this.Lbl_Speed.Text = "Speed:";
            // 
            // Lbl_SpeedVar
            // 
            this.Lbl_SpeedVar.AutoSize = true;
            this.Lbl_SpeedVar.Location = new System.Drawing.Point(177, 333);
            this.Lbl_SpeedVar.Name = "Lbl_SpeedVar";
            this.Lbl_SpeedVar.Size = new System.Drawing.Size(31, 14);
            this.Lbl_SpeedVar.TabIndex = 5;
            this.Lbl_SpeedVar.Text = "x1.0";
            // 
            // ChkBox_PlayerDetect
            // 
            this.ChkBox_PlayerDetect.AutoSize = true;
            this.ChkBox_PlayerDetect.Location = new System.Drawing.Point(180, 47);
            this.ChkBox_PlayerDetect.Name = "ChkBox_PlayerDetect";
            this.ChkBox_PlayerDetect.Size = new System.Drawing.Size(114, 18);
            this.ChkBox_PlayerDetect.TabIndex = 6;
            this.ChkBox_PlayerDetect.Text = "Player Detection";
            this.ChkBox_PlayerDetect.UseVisualStyleBackColor = true;
            this.ChkBox_PlayerDetect.CheckedChanged += new System.EventHandler(this.ChkBox_DetectDisable_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(260, 449);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "Status:";
            // 
            // Lbl_Status
            // 
            this.Lbl_Status.AutoSize = true;
            this.Lbl_Status.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Status.Location = new System.Drawing.Point(303, 449);
            this.Lbl_Status.Name = "Lbl_Status";
            this.Lbl_Status.Size = new System.Drawing.Size(14, 14);
            this.Lbl_Status.TabIndex = 10;
            this.Lbl_Status.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Zone:";
            // 
            // Lbl_Zone
            // 
            this.Lbl_Zone.AutoSize = true;
            this.Lbl_Zone.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Zone.Location = new System.Drawing.Point(48, 449);
            this.Lbl_Zone.Name = "Lbl_Zone";
            this.Lbl_Zone.Size = new System.Drawing.Size(69, 14);
            this.Lbl_Zone.TabIndex = 12;
            this.Lbl_Zone.Text = "Zone Name";
            // 
            // Btn_Plus_X
            // 
            this.Btn_Plus_X.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Plus_X.Location = new System.Drawing.Point(110, 48);
            this.Btn_Plus_X.Name = "Btn_Plus_X";
            this.Btn_Plus_X.Size = new System.Drawing.Size(41, 25);
            this.Btn_Plus_X.TabIndex = 22;
            this.Btn_Plus_X.Text = "E";
            this.Btn_Plus_X.UseVisualStyleBackColor = true;
            this.Btn_Plus_X.Click += new System.EventHandler(this.Btn_Plus_X_Click);
            // 
            // Btn_Minus_X
            // 
            this.Btn_Minus_X.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Minus_X.Location = new System.Drawing.Point(9, 48);
            this.Btn_Minus_X.Name = "Btn_Minus_X";
            this.Btn_Minus_X.Size = new System.Drawing.Size(41, 25);
            this.Btn_Minus_X.TabIndex = 23;
            this.Btn_Minus_X.Text = "W";
            this.Btn_Minus_X.UseVisualStyleBackColor = true;
            this.Btn_Minus_X.Click += new System.EventHandler(this.Btn_Minus_X_Click);
            // 
            // Btn_Minus_Y
            // 
            this.Btn_Minus_Y.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Minus_Y.Location = new System.Drawing.Point(59, 79);
            this.Btn_Minus_Y.Name = "Btn_Minus_Y";
            this.Btn_Minus_Y.Size = new System.Drawing.Size(41, 25);
            this.Btn_Minus_Y.TabIndex = 27;
            this.Btn_Minus_Y.Text = "S";
            this.Btn_Minus_Y.UseVisualStyleBackColor = true;
            this.Btn_Minus_Y.Click += new System.EventHandler(this.Btn_Minus_Y_Click);
            // 
            // Btn_Plus_Y
            // 
            this.Btn_Plus_Y.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Plus_Y.Location = new System.Drawing.Point(59, 16);
            this.Btn_Plus_Y.Name = "Btn_Plus_Y";
            this.Btn_Plus_Y.Size = new System.Drawing.Size(41, 25);
            this.Btn_Plus_Y.TabIndex = 26;
            this.Btn_Plus_Y.Text = "N";
            this.Btn_Plus_Y.UseVisualStyleBackColor = true;
            this.Btn_Plus_Y.Click += new System.EventHandler(this.Btn_Plus_Y_Click);
            // 
            // Btn_Minus_Z
            // 
            this.Btn_Minus_Z.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Minus_Z.Location = new System.Drawing.Point(59, 48);
            this.Btn_Minus_Z.Margin = new System.Windows.Forms.Padding(1);
            this.Btn_Minus_Z.Name = "Btn_Minus_Z";
            this.Btn_Minus_Z.Size = new System.Drawing.Size(41, 12);
            this.Btn_Minus_Z.TabIndex = 31;
            this.Btn_Minus_Z.Text = "+";
            this.Btn_Minus_Z.UseVisualStyleBackColor = true;
            this.Btn_Minus_Z.Click += new System.EventHandler(this.Btn_Minus_Z_Click);
            // 
            // Btn_Plus_Z
            // 
            this.Btn_Plus_Z.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Plus_Z.Location = new System.Drawing.Point(59, 61);
            this.Btn_Plus_Z.Name = "Btn_Plus_Z";
            this.Btn_Plus_Z.Size = new System.Drawing.Size(41, 12);
            this.Btn_Plus_Z.TabIndex = 30;
            this.Btn_Plus_Z.Text = "-";
            this.Btn_Plus_Z.UseVisualStyleBackColor = true;
            this.Btn_Plus_Z.Click += new System.EventHandler(this.Btn_Plus_Z_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_SE);
            this.groupBox1.Controls.Add(this.Btn_NE);
            this.groupBox1.Controls.Add(this.Btn_SW);
            this.groupBox1.Controls.Add(this.Btn_NW);
            this.groupBox1.Controls.Add(this.Btn_Minus_Z);
            this.groupBox1.Controls.Add(this.Btn_Plus_Z);
            this.groupBox1.Controls.Add(this.Btn_Plus_X);
            this.groupBox1.Controls.Add(this.Btn_Minus_X);
            this.groupBox1.Controls.Add(this.Btn_Minus_Y);
            this.groupBox1.Controls.Add(this.Btn_Plus_Y);
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 109);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
            // 
            // Btn_SE
            // 
            this.Btn_SE.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_SE.Location = new System.Drawing.Point(110, 79);
            this.Btn_SE.Name = "Btn_SE";
            this.Btn_SE.Size = new System.Drawing.Size(41, 25);
            this.Btn_SE.TabIndex = 35;
            this.Btn_SE.Text = "SE";
            this.Btn_SE.UseVisualStyleBackColor = true;
            this.Btn_SE.Click += new System.EventHandler(this.Btn_SE_Click);
            // 
            // Btn_NE
            // 
            this.Btn_NE.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_NE.Location = new System.Drawing.Point(110, 16);
            this.Btn_NE.Name = "Btn_NE";
            this.Btn_NE.Size = new System.Drawing.Size(41, 25);
            this.Btn_NE.TabIndex = 34;
            this.Btn_NE.Text = "NE";
            this.Btn_NE.UseVisualStyleBackColor = true;
            this.Btn_NE.Click += new System.EventHandler(this.Btn_NE_Click);
            // 
            // Btn_SW
            // 
            this.Btn_SW.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_SW.Location = new System.Drawing.Point(9, 79);
            this.Btn_SW.Name = "Btn_SW";
            this.Btn_SW.Size = new System.Drawing.Size(41, 25);
            this.Btn_SW.TabIndex = 33;
            this.Btn_SW.Text = "SW";
            this.Btn_SW.UseVisualStyleBackColor = true;
            this.Btn_SW.Click += new System.EventHandler(this.Btn_SW_Click);
            // 
            // Btn_NW
            // 
            this.Btn_NW.Font = new System.Drawing.Font("Meiryo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_NW.Location = new System.Drawing.Point(9, 16);
            this.Btn_NW.Name = "Btn_NW";
            this.Btn_NW.Size = new System.Drawing.Size(41, 25);
            this.Btn_NW.TabIndex = 32;
            this.Btn_NW.Text = "NW";
            this.Btn_NW.UseVisualStyleBackColor = true;
            this.Btn_NW.Click += new System.EventHandler(this.Btn_NW_Click);
            // 
            // CB_Warp
            // 
            this.CB_Warp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CB_Warp.FormattingEnabled = true;
            this.CB_Warp.Location = new System.Drawing.Point(14, 128);
            this.CB_Warp.Name = "CB_Warp";
            this.CB_Warp.Size = new System.Drawing.Size(303, 22);
            this.CB_Warp.TabIndex = 33;
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(14, 188);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(66, 25);
            this.Btn_Save.TabIndex = 34;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Btn_Warp
            // 
            this.Btn_Warp.Location = new System.Drawing.Point(14, 157);
            this.Btn_Warp.Name = "Btn_Warp";
            this.Btn_Warp.Size = new System.Drawing.Size(303, 25);
            this.Btn_Warp.TabIndex = 35;
            this.Btn_Warp.Text = "Warp";
            this.Btn_Warp.UseVisualStyleBackColor = true;
            this.Btn_Warp.Click += new System.EventHandler(this.Btn_Warp_Click);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.Location = new System.Drawing.Point(93, 188);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(66, 25);
            this.Btn_Delete.TabIndex = 36;
            this.Btn_Delete.Text = "Delete";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // ChkBox_StayTop
            // 
            this.ChkBox_StayTop.AutoSize = true;
            this.ChkBox_StayTop.Location = new System.Drawing.Point(180, 66);
            this.ChkBox_StayTop.Name = "ChkBox_StayTop";
            this.ChkBox_StayTop.Size = new System.Drawing.Size(91, 18);
            this.ChkBox_StayTop.TabIndex = 37;
            this.ChkBox_StayTop.Text = "Stay on Top";
            this.ChkBox_StayTop.UseVisualStyleBackColor = true;
            this.ChkBox_StayTop.CheckedChanged += new System.EventHandler(this.ChkBox_StayTop_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 260);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(87, 14);
            this.label4.TabIndex = 39;
            this.label4.Text = "Default Speed:";
            // 
            // Lbl_DefaultSpeed
            // 
            this.Lbl_DefaultSpeed.AutoSize = true;
            this.Lbl_DefaultSpeed.Location = new System.Drawing.Point(194, 260);
            this.Lbl_DefaultSpeed.Name = "Lbl_DefaultSpeed";
            this.Lbl_DefaultSpeed.Size = new System.Drawing.Size(31, 14);
            this.Lbl_DefaultSpeed.TabIndex = 40;
            this.Lbl_DefaultSpeed.Text = "x1.0";
            // 
            // Btn_SaveSettings
            // 
            this.Btn_SaveSettings.Location = new System.Drawing.Point(180, 97);
            this.Btn_SaveSettings.Name = "Btn_SaveSettings";
            this.Btn_SaveSettings.Size = new System.Drawing.Size(138, 25);
            this.Btn_SaveSettings.TabIndex = 41;
            this.Btn_SaveSettings.Text = "Save Settings";
            this.Btn_SaveSettings.UseVisualStyleBackColor = true;
            this.Btn_SaveSettings.Click += new System.EventHandler(this.Btn_SaveSettings_Click);
            // 
            // Bar_Speed_Default
            // 
            this.Bar_Speed_Default.Location = new System.Drawing.Point(14, 220);
            this.Bar_Speed_Default.Name = "Bar_Speed_Default";
            this.Bar_Speed_Default.Size = new System.Drawing.Size(303, 45);
            this.Bar_Speed_Default.TabIndex = 38;
            this.Bar_Speed_Default.Scroll += new System.EventHandler(this.Bar_Speed_Default_Scroll);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 435);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 14);
            this.label11.TabIndex = 52;
            this.label11.Text = "Nearest Player:";
            // 
            // Lbl_TargetInfo
            // 
            this.Lbl_TargetInfo.AutoSize = true;
            this.Lbl_TargetInfo.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_TargetInfo.Location = new System.Drawing.Point(56, 421);
            this.Lbl_TargetInfo.Name = "Lbl_TargetInfo";
            this.Lbl_TargetInfo.Size = new System.Drawing.Size(114, 14);
            this.Lbl_TargetInfo.TabIndex = 49;
            this.Lbl_TargetInfo.Text = "Name (HPP) @ 123";
            // 
            // Lbl_NearestPlayer
            // 
            this.Lbl_NearestPlayer.AutoSize = true;
            this.Lbl_NearestPlayer.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_NearestPlayer.Location = new System.Drawing.Point(100, 435);
            this.Lbl_NearestPlayer.Name = "Lbl_NearestPlayer";
            this.Lbl_NearestPlayer.Size = new System.Drawing.Size(78, 14);
            this.Lbl_NearestPlayer.TabIndex = 53;
            this.Lbl_NearestPlayer.Text = "Name @ 123";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 421);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(46, 14);
            this.label5.TabIndex = 48;
            this.label5.Text = "Target:";
            // 
            // Btn_Accept
            // 
            this.Btn_Accept.Enabled = false;
            this.Btn_Accept.Location = new System.Drawing.Point(251, 188);
            this.Btn_Accept.Name = "Btn_Accept";
            this.Btn_Accept.Size = new System.Drawing.Size(66, 25);
            this.Btn_Accept.TabIndex = 54;
            this.Btn_Accept.Text = "Accept";
            this.Btn_Accept.UseVisualStyleBackColor = true;
            this.Btn_Accept.Click += new System.EventHandler(this.Btn_Accept_Click);
            // 
            // Btn_Req
            // 
            this.Btn_Req.Location = new System.Drawing.Point(172, 188);
            this.Btn_Req.Name = "Btn_Req";
            this.Btn_Req.Size = new System.Drawing.Size(66, 25);
            this.Btn_Req.TabIndex = 55;
            this.Btn_Req.Text = "Request";
            this.Btn_Req.UseVisualStyleBackColor = true;
            this.Btn_Req.Click += new System.EventHandler(this.Btn_Req_Click);
            // 
            // Txt_Search
            // 
            this.Txt_Search.Location = new System.Drawing.Point(66, 363);
            this.Txt_Search.Name = "Txt_Search";
            this.Txt_Search.Size = new System.Drawing.Size(180, 21);
            this.Txt_Search.TabIndex = 57;
            this.Txt_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Search_KeyDown);
            // 
            // Btn_Find
            // 
            this.Btn_Find.Location = new System.Drawing.Point(259, 363);
            this.Btn_Find.Name = "Btn_Find";
            this.Btn_Find.Size = new System.Drawing.Size(55, 21);
            this.Btn_Find.TabIndex = 58;
            this.Btn_Find.Text = "Find";
            this.Btn_Find.UseVisualStyleBackColor = true;
            this.Btn_Find.Click += new System.EventHandler(this.Btn_Find_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 59;
            this.label1.Text = "Search:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 387);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 14);
            this.label6.TabIndex = 60;
            this.label6.Text = "Search Status:";
            // 
            // Lbl_Search
            // 
            this.Lbl_Search.AutoSize = true;
            this.Lbl_Search.Location = new System.Drawing.Point(100, 387);
            this.Lbl_Search.Name = "Lbl_Search";
            this.Lbl_Search.Size = new System.Drawing.Size(26, 14);
            this.Lbl_Search.TabIndex = 61;
            this.Lbl_Search.Text = "idle";
            // 
            // Btn_Abort
            // 
            this.Btn_Abort.Enabled = false;
            this.Btn_Abort.Location = new System.Drawing.Point(259, 384);
            this.Btn_Abort.Name = "Btn_Abort";
            this.Btn_Abort.Size = new System.Drawing.Size(55, 21);
            this.Btn_Abort.TabIndex = 62;
            this.Btn_Abort.Text = "Abort";
            this.Btn_Abort.UseVisualStyleBackColor = true;
            this.Btn_Abort.Click += new System.EventHandler(this.Btn_Abort_Click);
            // 
            // NailClipr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 472);
            this.Controls.Add(this.Btn_Abort);
            this.Controls.Add(this.Lbl_Search);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Find);
            this.Controls.Add(this.Txt_Search);
            this.Controls.Add(this.Btn_Req);
            this.Controls.Add(this.Btn_Accept);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Lbl_NearestPlayer);
            this.Controls.Add(this.Btn_SaveSettings);
            this.Controls.Add(this.Lbl_TargetInfo);
            this.Controls.Add(this.Lbl_DefaultSpeed);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Bar_Speed_Default);
            this.Controls.Add(this.ChkBox_StayTop);
            this.Controls.Add(this.Btn_Delete);
            this.Controls.Add(this.Btn_Warp);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.CB_Warp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Lbl_Zone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Lbl_Status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ChkBox_PlayerDetect);
            this.Controls.Add(this.Lbl_SpeedVar);
            this.Controls.Add(this.Lbl_Speed);
            this.Controls.Add(this.Bar_Speed);
            this.Controls.Add(this.ChkBox_Maint);
            this.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NailClipr";
            this.Text = "NailClipr";
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Speed_Default)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.CheckBox ChkBox_Maint;
        private System.Windows.Forms.TrackBar Bar_Speed;
        private System.Windows.Forms.Label Lbl_Speed;
        private System.Windows.Forms.Label Lbl_SpeedVar;
        private System.Windows.Forms.CheckBox ChkBox_PlayerDetect;
        private Label Lbl_Status;
        private Label label2;
        private Label label3;
        private Label Lbl_Zone;
        private Button Btn_Plus_X;
        private Button Btn_Minus_X;
        private Button Btn_Minus_Y;
        private Button Btn_Plus_Y;
        private Button Btn_Minus_Z;
        private Button Btn_Plus_Z;
        private GroupBox groupBox1;
        private ComboBox CB_Warp;
        private Button Btn_Save;
        private Button Btn_Warp;
        private Button Btn_Delete;
        private CheckBox ChkBox_StayTop;
        private Label label4;
        private Label Lbl_DefaultSpeed;
        private Button Btn_SaveSettings;
        private TrackBar Bar_Speed_Default;
        private Label label11;
        private Label Lbl_TargetInfo;
        private Label Lbl_NearestPlayer;
        private Label label5;
        private Button Btn_Accept;
        private Button Btn_Req;
        private Button Btn_SE;
        private Button Btn_NE;
        private Button Btn_SW;
        private Button Btn_NW;
        private TextBox Txt_Search;
        private Button Btn_Find;
        private Label label1;
        private Label label6;
        private Label Lbl_Search;
        private Button Btn_Abort;
    }
}

