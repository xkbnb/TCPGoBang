namespace GoBangClient
{
    partial class GameHall
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bt_createRoom = new System.Windows.Forms.Button();
            this.lv_room = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lv_player = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.bt_createRoom);
            this.groupBox1.Controls.Add(this.lv_room);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 416);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "房间列表";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(182, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "加入房间";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt_createRoom
            // 
            this.bt_createRoom.BackColor = System.Drawing.Color.White;
            this.bt_createRoom.Location = new System.Drawing.Point(34, 368);
            this.bt_createRoom.Name = "bt_createRoom";
            this.bt_createRoom.Size = new System.Drawing.Size(80, 34);
            this.bt_createRoom.TabIndex = 1;
            this.bt_createRoom.Text = "创建房间";
            this.bt_createRoom.UseVisualStyleBackColor = false;
            this.bt_createRoom.Click += new System.EventHandler(this.bt_createRoom_Click);
            // 
            // lv_room
            // 
            this.lv_room.BackColor = System.Drawing.Color.White;
            this.lv_room.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv_room.FullRowSelect = true;
            this.lv_room.GridLines = true;
            this.lv_room.HideSelection = false;
            this.lv_room.Location = new System.Drawing.Point(6, 20);
            this.lv_room.Name = "lv_room";
            this.lv_room.ShowItemToolTips = true;
            this.lv_room.Size = new System.Drawing.Size(283, 335);
            this.lv_room.TabIndex = 0;
            this.lv_room.UseCompatibleStateImageBehavior = false;
            this.lv_room.View = System.Windows.Forms.View.Details;
            this.lv_room.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_room_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "房间名称";
            this.columnHeader1.Width = 114;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "房主名称";
            this.columnHeader2.Width = 77;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "现有人数";
            this.columnHeader3.Width = 88;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_player);
            this.groupBox2.Location = new System.Drawing.Point(337, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 416);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "玩家列表";
            // 
            // lv_player
            // 
            this.lv_player.BackColor = System.Drawing.Color.White;
            this.lv_player.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lv_player.GridLines = true;
            this.lv_player.HideSelection = false;
            this.lv_player.Location = new System.Drawing.Point(7, 21);
            this.lv_player.Name = "lv_player";
            this.lv_player.Size = new System.Drawing.Size(187, 389);
            this.lv_player.TabIndex = 0;
            this.lv_player.UseCompatibleStateImageBehavior = false;
            this.lv_player.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "玩家名称";
            this.columnHeader4.Width = 182;
            // 
            // GameHall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(555, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GameHall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "房间";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameHall_FormClosing);
            this.Load += new System.EventHandler(this.GameHall_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bt_createRoom;
        private System.Windows.Forms.ListView lv_room;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lv_player;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}