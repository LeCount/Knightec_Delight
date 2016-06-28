namespace Async_TCP_client_networking
{
    partial class LoginWindow
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
            this.lbl_network = new System.Windows.Forms.Label();
            this.lbl_ip_addr = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.lbl_user = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.btn_new_user = new System.Windows.Forms.Button();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.lbl_server_connect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_network
            // 
            this.lbl_network.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_network.AutoSize = true;
            this.lbl_network.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbl_network.Location = new System.Drawing.Point(12, 199);
            this.lbl_network.Name = "lbl_network";
            this.lbl_network.Size = new System.Drawing.Size(81, 13);
            this.lbl_network.TabIndex = 2;
            this.lbl_network.Text = "Network status:";
            // 
            // lbl_ip_addr
            // 
            this.lbl_ip_addr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ip_addr.AutoSize = true;
            this.lbl_ip_addr.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbl_ip_addr.Location = new System.Drawing.Point(12, 239);
            this.lbl_ip_addr.Name = "lbl_ip_addr";
            this.lbl_ip_addr.Size = new System.Drawing.Size(87, 13);
            this.lbl_ip_addr.TabIndex = 3;
            this.lbl_ip_addr.Text = "Local ip address:";
            // 
            // btn_login
            // 
            this.btn_login.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_login.Location = new System.Drawing.Point(278, 9);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(53, 42);
            this.btn_login.TabIndex = 18;
            this.btn_login.Text = "Log in";
            this.btn_login.UseVisualStyleBackColor = true;
            // 
            // lbl_user
            // 
            this.lbl_user.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_user.AutoSize = true;
            this.lbl_user.Location = new System.Drawing.Point(12, 9);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(29, 13);
            this.lbl_user.TabIndex = 15;
            this.lbl_user.Text = "User";
            // 
            // lbl_password
            // 
            this.lbl_password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(147, 9);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(53, 13);
            this.lbl_password.TabIndex = 16;
            this.lbl_password.Text = "Password";
            // 
            // btn_new_user
            // 
            this.btn_new_user.Location = new System.Drawing.Point(121, 114);
            this.btn_new_user.Name = "btn_new_user";
            this.btn_new_user.Size = new System.Drawing.Size(100, 25);
            this.btn_new_user.TabIndex = 17;
            this.btn_new_user.Text = "Register new user";
            this.btn_new_user.UseVisualStyleBackColor = true;
            this.btn_new_user.Click += new System.EventHandler(this.btn_new_user_Click_1);
            // 
            // textBox_user
            // 
            this.textBox_user.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_user.Location = new System.Drawing.Point(15, 31);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(122, 20);
            this.textBox_user.TabIndex = 13;
            // 
            // textBox_password
            // 
            this.textBox_password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_password.Location = new System.Drawing.Point(150, 31);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(122, 20);
            this.textBox_password.TabIndex = 14;
            // 
            // lbl_server_connect
            // 
            this.lbl_server_connect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_server_connect.AutoSize = true;
            this.lbl_server_connect.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbl_server_connect.Location = new System.Drawing.Point(12, 219);
            this.lbl_server_connect.Name = "lbl_server_connect";
            this.lbl_server_connect.Size = new System.Drawing.Size(75, 13);
            this.lbl_server_connect.TabIndex = 19;
            this.lbl_server_connect.Text = "Server status: ";
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 261);
            this.Controls.Add(this.lbl_server_connect);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.lbl_user);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.btn_new_user);
            this.Controls.Add(this.textBox_user);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.lbl_ip_addr);
            this.Controls.Add(this.lbl_network);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User log in";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_network;
        private System.Windows.Forms.Label lbl_ip_addr;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Button btn_new_user;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label lbl_server_connect;
    }
}

