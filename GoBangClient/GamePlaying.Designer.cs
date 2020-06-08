namespace GoBangClient
{
    partial class GamePlaying
    {
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
            this.label_myName = new System.Windows.Forms.Label();
            this.label_myScore = new System.Windows.Forms.Label();
            this.gb_myInfo = new System.Windows.Forms.GroupBox();
            this.bt_confess = new System.Windows.Forms.Button();
            this.bt_reconcile = new System.Windows.Forms.Button();
            this.lb_myScore = new System.Windows.Forms.Label();
            this.lb_myName = new System.Windows.Forms.Label();
            this.bt_start = new System.Windows.Forms.Button();
            this.gb_roomInfo = new System.Windows.Forms.GroupBox();
            this.lb_roomStatus = new System.Windows.Forms.Label();
            this.label_roomStatus = new System.Windows.Forms.Label();
            this.lb_roomMasterName = new System.Windows.Forms.Label();
            this.lb_roomName = new System.Windows.Forms.Label();
            this.label_roomMasterName = new System.Windows.Forms.Label();
            this.label_roomName = new System.Windows.Forms.Label();
            this.gb_opponentInfo = new System.Windows.Forms.GroupBox();
            this.lb_opponentScore = new System.Windows.Forms.Label();
            this.lb_opponentName = new System.Windows.Forms.Label();
            this.label_opponentScore = new System.Windows.Forms.Label();
            this.label_opponentName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gb_chat = new System.Windows.Forms.GroupBox();
            this.bt_send = new System.Windows.Forms.Button();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.rtb_message = new System.Windows.Forms.RichTextBox();
            this.label_myColor = new System.Windows.Forms.Label();
            this.label_opponentColor = new System.Windows.Forms.Label();
            this.lb_myCorlor = new System.Windows.Forms.Label();
            this.lb_opponentColor = new System.Windows.Forms.Label();
            this.gb_myInfo.SuspendLayout();
            this.gb_roomInfo.SuspendLayout();
            this.gb_opponentInfo.SuspendLayout();
            this.gb_chat.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_myName
            // 
            this.label_myName.AutoSize = true;
            this.label_myName.Location = new System.Drawing.Point(15, 49);
            this.label_myName.Name = "label_myName";
            this.label_myName.Size = new System.Drawing.Size(66, 19);
            this.label_myName.TabIndex = 3;
            this.label_myName.Text = "昵称：";
            // 
            // label_myScore
            // 
            this.label_myScore.AutoSize = true;
            this.label_myScore.Location = new System.Drawing.Point(15, 126);
            this.label_myScore.Name = "label_myScore";
            this.label_myScore.Size = new System.Drawing.Size(66, 19);
            this.label_myScore.TabIndex = 4;
            this.label_myScore.Text = "分数：";
            // 
            // gb_myInfo
            // 
            this.gb_myInfo.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.gb_myInfo.Controls.Add(this.lb_myCorlor);
            this.gb_myInfo.Controls.Add(this.label_myColor);
            this.gb_myInfo.Controls.Add(this.bt_confess);
            this.gb_myInfo.Controls.Add(this.bt_reconcile);
            this.gb_myInfo.Controls.Add(this.lb_myScore);
            this.gb_myInfo.Controls.Add(this.lb_myName);
            this.gb_myInfo.Controls.Add(this.bt_start);
            this.gb_myInfo.Controls.Add(this.label_myScore);
            this.gb_myInfo.Controls.Add(this.label_myName);
            this.gb_myInfo.Font = new System.Drawing.Font("宋体", 13.8F);
            this.gb_myInfo.Location = new System.Drawing.Point(12, 229);
            this.gb_myInfo.Name = "gb_myInfo";
            this.gb_myInfo.Size = new System.Drawing.Size(266, 286);
            this.gb_myInfo.TabIndex = 0;
            this.gb_myInfo.TabStop = false;
            this.gb_myInfo.Text = "我方";
            // 
            // bt_confess
            // 
            this.bt_confess.Location = new System.Drawing.Point(141, 240);
            this.bt_confess.Name = "bt_confess";
            this.bt_confess.Size = new System.Drawing.Size(75, 29);
            this.bt_confess.TabIndex = 10;
            this.bt_confess.Text = "认输";
            this.bt_confess.UseVisualStyleBackColor = true;
            this.bt_confess.Click += new System.EventHandler(this.bt_confess_Click);
            // 
            // bt_reconcile
            // 
            this.bt_reconcile.Location = new System.Drawing.Point(18, 240);
            this.bt_reconcile.Name = "bt_reconcile";
            this.bt_reconcile.Size = new System.Drawing.Size(75, 29);
            this.bt_reconcile.TabIndex = 9;
            this.bt_reconcile.Text = "求和";
            this.bt_reconcile.UseVisualStyleBackColor = true;
            this.bt_reconcile.Click += new System.EventHandler(this.bt_reconcile_Click);
            // 
            // lb_myScore
            // 
            this.lb_myScore.AutoSize = true;
            this.lb_myScore.Location = new System.Drawing.Point(87, 126);
            this.lb_myScore.Name = "lb_myScore";
            this.lb_myScore.Size = new System.Drawing.Size(69, 19);
            this.lb_myScore.TabIndex = 8;
            this.lb_myScore.Text = "label1";
            // 
            // lb_myName
            // 
            this.lb_myName.AutoSize = true;
            this.lb_myName.Location = new System.Drawing.Point(87, 49);
            this.lb_myName.Name = "lb_myName";
            this.lb_myName.Size = new System.Drawing.Size(69, 19);
            this.lb_myName.TabIndex = 7;
            this.lb_myName.Text = "label1";
            // 
            // bt_start
            // 
            this.bt_start.Location = new System.Drawing.Point(57, 174);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(126, 39);
            this.bt_start.TabIndex = 5;
            this.bt_start.Text = "开始游戏";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.bt_start_Click);
            // 
            // gb_roomInfo
            // 
            this.gb_roomInfo.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.gb_roomInfo.Controls.Add(this.lb_roomStatus);
            this.gb_roomInfo.Controls.Add(this.label_roomStatus);
            this.gb_roomInfo.Controls.Add(this.lb_roomMasterName);
            this.gb_roomInfo.Controls.Add(this.lb_roomName);
            this.gb_roomInfo.Controls.Add(this.label_roomMasterName);
            this.gb_roomInfo.Controls.Add(this.label_roomName);
            this.gb_roomInfo.Font = new System.Drawing.Font("宋体", 13.8F);
            this.gb_roomInfo.Location = new System.Drawing.Point(12, 12);
            this.gb_roomInfo.Name = "gb_roomInfo";
            this.gb_roomInfo.Size = new System.Drawing.Size(266, 203);
            this.gb_roomInfo.TabIndex = 1;
            this.gb_roomInfo.TabStop = false;
            this.gb_roomInfo.Text = "房间信息";
            // 
            // lb_roomStatus
            // 
            this.lb_roomStatus.AutoSize = true;
            this.lb_roomStatus.Location = new System.Drawing.Point(125, 165);
            this.lb_roomStatus.Name = "lb_roomStatus";
            this.lb_roomStatus.Size = new System.Drawing.Size(69, 19);
            this.lb_roomStatus.TabIndex = 5;
            this.lb_roomStatus.Text = "label1";
            // 
            // label_roomStatus
            // 
            this.label_roomStatus.AutoSize = true;
            this.label_roomStatus.Location = new System.Drawing.Point(15, 165);
            this.label_roomStatus.Name = "label_roomStatus";
            this.label_roomStatus.Size = new System.Drawing.Size(104, 19);
            this.label_roomStatus.TabIndex = 4;
            this.label_roomStatus.Text = "房间状态：";
            // 
            // lb_roomMasterName
            // 
            this.lb_roomMasterName.AutoSize = true;
            this.lb_roomMasterName.Location = new System.Drawing.Point(125, 116);
            this.lb_roomMasterName.Name = "lb_roomMasterName";
            this.lb_roomMasterName.Size = new System.Drawing.Size(69, 19);
            this.lb_roomMasterName.TabIndex = 3;
            this.lb_roomMasterName.Text = "label1";
            // 
            // lb_roomName
            // 
            this.lb_roomName.AutoSize = true;
            this.lb_roomName.Location = new System.Drawing.Point(125, 44);
            this.lb_roomName.Name = "lb_roomName";
            this.lb_roomName.Size = new System.Drawing.Size(69, 19);
            this.lb_roomName.TabIndex = 2;
            this.lb_roomName.Text = "label1";
            // 
            // label_roomMasterName
            // 
            this.label_roomMasterName.AutoSize = true;
            this.label_roomMasterName.Location = new System.Drawing.Point(15, 116);
            this.label_roomMasterName.Name = "label_roomMasterName";
            this.label_roomMasterName.Size = new System.Drawing.Size(104, 19);
            this.label_roomMasterName.TabIndex = 1;
            this.label_roomMasterName.Text = "房主名称：";
            // 
            // label_roomName
            // 
            this.label_roomName.AutoSize = true;
            this.label_roomName.Location = new System.Drawing.Point(15, 44);
            this.label_roomName.Name = "label_roomName";
            this.label_roomName.Size = new System.Drawing.Size(104, 19);
            this.label_roomName.TabIndex = 0;
            this.label_roomName.Text = "房间名称：";
            // 
            // gb_opponentInfo
            // 
            this.gb_opponentInfo.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.gb_opponentInfo.Controls.Add(this.lb_opponentColor);
            this.gb_opponentInfo.Controls.Add(this.label_opponentColor);
            this.gb_opponentInfo.Controls.Add(this.lb_opponentScore);
            this.gb_opponentInfo.Controls.Add(this.lb_opponentName);
            this.gb_opponentInfo.Controls.Add(this.label_opponentScore);
            this.gb_opponentInfo.Controls.Add(this.label_opponentName);
            this.gb_opponentInfo.Font = new System.Drawing.Font("宋体", 13.8F);
            this.gb_opponentInfo.Location = new System.Drawing.Point(12, 535);
            this.gb_opponentInfo.Name = "gb_opponentInfo";
            this.gb_opponentInfo.Size = new System.Drawing.Size(266, 180);
            this.gb_opponentInfo.TabIndex = 2;
            this.gb_opponentInfo.TabStop = false;
            this.gb_opponentInfo.Text = "对手";
            // 
            // lb_opponentScore
            // 
            this.lb_opponentScore.AutoSize = true;
            this.lb_opponentScore.Location = new System.Drawing.Point(87, 125);
            this.lb_opponentScore.Name = "lb_opponentScore";
            this.lb_opponentScore.Size = new System.Drawing.Size(69, 19);
            this.lb_opponentScore.TabIndex = 8;
            this.lb_opponentScore.Text = "label1";
            // 
            // lb_opponentName
            // 
            this.lb_opponentName.AutoSize = true;
            this.lb_opponentName.Location = new System.Drawing.Point(86, 48);
            this.lb_opponentName.Name = "lb_opponentName";
            this.lb_opponentName.Size = new System.Drawing.Size(69, 19);
            this.lb_opponentName.TabIndex = 7;
            this.lb_opponentName.Text = "label1";
            // 
            // label_opponentScore
            // 
            this.label_opponentScore.AutoSize = true;
            this.label_opponentScore.Location = new System.Drawing.Point(14, 125);
            this.label_opponentScore.Name = "label_opponentScore";
            this.label_opponentScore.Size = new System.Drawing.Size(66, 19);
            this.label_opponentScore.TabIndex = 6;
            this.label_opponentScore.Text = "分数：";
            // 
            // label_opponentName
            // 
            this.label_opponentName.AutoSize = true;
            this.label_opponentName.Location = new System.Drawing.Point(14, 48);
            this.label_opponentName.Name = "label_opponentName";
            this.label_opponentName.Size = new System.Drawing.Size(66, 19);
            this.label_opponentName.TabIndex = 5;
            this.label_opponentName.Text = "昵称：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel1.Location = new System.Drawing.Point(297, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 705);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // gb_chat
            // 
            this.gb_chat.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.gb_chat.Controls.Add(this.bt_send);
            this.gb_chat.Controls.Add(this.tb_message);
            this.gb_chat.Controls.Add(this.rtb_message);
            this.gb_chat.Location = new System.Drawing.Point(1004, 13);
            this.gb_chat.Name = "gb_chat";
            this.gb_chat.Size = new System.Drawing.Size(213, 706);
            this.gb_chat.TabIndex = 4;
            this.gb_chat.TabStop = false;
            this.gb_chat.Text = "聊天消息";
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(63, 670);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(75, 23);
            this.bt_send.TabIndex = 2;
            this.bt_send.Text = "发送";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(7, 626);
            this.tb_message.Multiline = true;
            this.tb_message.Name = "tb_message";
            this.tb_message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_message.Size = new System.Drawing.Size(194, 28);
            this.tb_message.TabIndex = 1;
            // 
            // rtb_message
            // 
            this.rtb_message.Location = new System.Drawing.Point(7, 21);
            this.rtb_message.Name = "rtb_message";
            this.rtb_message.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_message.Size = new System.Drawing.Size(194, 590);
            this.rtb_message.TabIndex = 0;
            this.rtb_message.Text = "";
            // 
            // label_myColor
            // 
            this.label_myColor.AutoSize = true;
            this.label_myColor.Location = new System.Drawing.Point(15, 87);
            this.label_myColor.Name = "label_myColor";
            this.label_myColor.Size = new System.Drawing.Size(66, 19);
            this.label_myColor.TabIndex = 11;
            this.label_myColor.Text = "棋子：";
            // 
            // label_opponentColor
            // 
            this.label_opponentColor.AutoSize = true;
            this.label_opponentColor.Location = new System.Drawing.Point(14, 86);
            this.label_opponentColor.Name = "label_opponentColor";
            this.label_opponentColor.Size = new System.Drawing.Size(66, 19);
            this.label_opponentColor.TabIndex = 9;
            this.label_opponentColor.Text = "棋子：";
            // 
            // lb_myCorlor
            // 
            this.lb_myCorlor.AutoSize = true;
            this.lb_myCorlor.Location = new System.Drawing.Point(87, 87);
            this.lb_myCorlor.Name = "lb_myCorlor";
            this.lb_myCorlor.Size = new System.Drawing.Size(69, 19);
            this.lb_myCorlor.TabIndex = 12;
            this.lb_myCorlor.Text = "label1";
            // 
            // lb_opponentColor
            // 
            this.lb_opponentColor.AutoSize = true;
            this.lb_opponentColor.Location = new System.Drawing.Point(86, 86);
            this.lb_opponentColor.Name = "lb_opponentColor";
            this.lb_opponentColor.Size = new System.Drawing.Size(69, 19);
            this.lb_opponentColor.TabIndex = 10;
            this.lb_opponentColor.Text = "label2";
            // 
            // GamePlaying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1217, 731);
            this.Controls.Add(this.gb_chat);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gb_opponentInfo);
            this.Controls.Add(this.gb_roomInfo);
            this.Controls.Add(this.gb_myInfo);
            this.Name = "GamePlaying";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GamePlaying";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GamePlaying_FormClosing);
            this.gb_myInfo.ResumeLayout(false);
            this.gb_myInfo.PerformLayout();
            this.gb_roomInfo.ResumeLayout(false);
            this.gb_roomInfo.PerformLayout();
            this.gb_opponentInfo.ResumeLayout(false);
            this.gb_opponentInfo.PerformLayout();
            this.gb_chat.ResumeLayout(false);
            this.gb_chat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_myName;
        private System.Windows.Forms.Label label_myScore;
        private System.Windows.Forms.GroupBox gb_myInfo;
        private System.Windows.Forms.GroupBox gb_roomInfo;
        private System.Windows.Forms.Label label_roomMasterName;
        private System.Windows.Forms.Label label_roomName;
        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.GroupBox gb_opponentInfo;
        private System.Windows.Forms.Label label_opponentScore;
        private System.Windows.Forms.Label label_opponentName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb_myScore;
        private System.Windows.Forms.Label lb_myName;
        private System.Windows.Forms.Label lb_roomMasterName;
        private System.Windows.Forms.Label lb_roomName;
        private System.Windows.Forms.Label lb_opponentScore;
        private System.Windows.Forms.Label lb_opponentName;
        private System.Windows.Forms.Label lb_roomStatus;
        private System.Windows.Forms.Label label_roomStatus;
        private System.Windows.Forms.GroupBox gb_chat;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.RichTextBox rtb_message;
        private System.Windows.Forms.Button bt_confess;
        private System.Windows.Forms.Button bt_reconcile;
        private System.Windows.Forms.Label label_myColor;
        private System.Windows.Forms.Label label_opponentColor;
        private System.Windows.Forms.Label lb_myCorlor;
        private System.Windows.Forms.Label lb_opponentColor;
    }
}