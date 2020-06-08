namespace GoBangClient
{
    partial class Login
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
            this.lb_ServerIP = new System.Windows.Forms.Label();
            this.tb_serverIP = new System.Windows.Forms.TextBox();
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.lb_UserName = new System.Windows.Forms.Label();
            this.bt_login = new System.Windows.Forms.Button();
            this.bt_cancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_ServerIP
            // 
            this.lb_ServerIP.AutoSize = true;
            this.lb_ServerIP.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ServerIP.Location = new System.Drawing.Point(28, 33);
            this.lb_ServerIP.Name = "lb_ServerIP";
            this.lb_ServerIP.Size = new System.Drawing.Size(105, 19);
            this.lb_ServerIP.TabIndex = 0;
            this.lb_ServerIP.Text = "服务器IP：";
            // 
            // tb_serverIP
            // 
            this.tb_serverIP.Location = new System.Drawing.Point(139, 33);
            this.tb_serverIP.Name = "tb_serverIP";
            this.tb_serverIP.Size = new System.Drawing.Size(272, 21);
            this.tb_serverIP.TabIndex = 1;
            this.tb_serverIP.Text = "10.21.125.222";
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(139, 109);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(272, 21);
            this.tb_userName.TabIndex = 2;
            // 
            // lb_UserName
            // 
            this.lb_UserName.AutoSize = true;
            this.lb_UserName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lb_UserName.Font = new System.Drawing.Font("宋体", 14.25F);
            this.lb_UserName.Location = new System.Drawing.Point(32, 109);
            this.lb_UserName.Name = "lb_UserName";
            this.lb_UserName.Size = new System.Drawing.Size(95, 19);
            this.lb_UserName.TabIndex = 3;
            this.lb_UserName.Text = "用户名称:";
            // 
            // bt_login
            // 
            this.bt_login.Font = new System.Drawing.Font("宋体", 13.8F);
            this.bt_login.Location = new System.Drawing.Point(36, 191);
            this.bt_login.Name = "bt_login";
            this.bt_login.Size = new System.Drawing.Size(135, 43);
            this.bt_login.TabIndex = 4;
            this.bt_login.Text = "登录";
            this.bt_login.UseVisualStyleBackColor = true;
            this.bt_login.Click += new System.EventHandler(this.bt_login_Click);
            // 
            // bt_cancle
            // 
            this.bt_cancle.Font = new System.Drawing.Font("宋体", 13.8F);
            this.bt_cancle.Location = new System.Drawing.Point(276, 191);
            this.bt_cancle.Name = "bt_cancle";
            this.bt_cancle.Size = new System.Drawing.Size(135, 43);
            this.bt_cancle.TabIndex = 5;
            this.bt_cancle.Text = "取消";
            this.bt_cancle.UseVisualStyleBackColor = true;
            this.bt_cancle.Click += new System.EventHandler(this.bt_cancle_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(482, 261);
            this.Controls.Add(this.bt_cancle);
            this.Controls.Add(this.bt_login);
            this.Controls.Add(this.lb_UserName);
            this.Controls.Add(this.tb_userName);
            this.Controls.Add(this.tb_serverIP);
            this.Controls.Add(this.lb_ServerIP);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接至服务器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_ServerIP;
        private System.Windows.Forms.TextBox tb_serverIP;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.Label lb_UserName;
        private System.Windows.Forms.Button bt_login;
        private System.Windows.Forms.Button bt_cancle;
    }
}