namespace GoBangClient
{
    partial class CreatRoom
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_room = new System.Windows.Forms.TextBox();
            this.bt_create = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F);
            this.label1.Location = new System.Drawing.Point(34, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "房间名称：";
            // 
            // tb_room
            // 
            this.tb_room.Location = new System.Drawing.Point(144, 48);
            this.tb_room.Name = "tb_room";
            this.tb_room.Size = new System.Drawing.Size(270, 21);
            this.tb_room.TabIndex = 1;
            // 
            // bt_create
            // 
            this.bt_create.BackColor = System.Drawing.SystemColors.Info;
            this.bt_create.Location = new System.Drawing.Point(133, 139);
            this.bt_create.Name = "bt_create";
            this.bt_create.Size = new System.Drawing.Size(200, 47);
            this.bt_create.TabIndex = 2;
            this.bt_create.Text = "创建房间";
            this.bt_create.UseVisualStyleBackColor = false;
            this.bt_create.Click += new System.EventHandler(this.bt_create_Click);
            // 
            // CreatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(466, 214);
            this.Controls.Add(this.bt_create);
            this.Controls.Add(this.tb_room);
            this.Controls.Add(this.label1);
            this.Name = "CreatRoom";
            this.Text = "创建房间";
            this.Load += new System.EventHandler(this.CreatRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_room;
        private System.Windows.Forms.Button bt_create;
    }
}