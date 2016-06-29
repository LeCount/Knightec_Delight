namespace Async_TCP_client_networking
{
    partial class AddUserWindow
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
            this.btn_submit = new System.Windows.Forms.Button();
            this.lbl_validate_username = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.lbl_mail = new System.Windows.Forms.Label();
            this.textBox_mail = new System.Windows.Forms.TextBox();
            this.lbl_validate_password = new System.Windows.Forms.Label();
            this.lbl_validate_mail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_submit
            // 
            this.btn_submit.Enabled = false;
            this.btn_submit.Location = new System.Drawing.Point(296, 21);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(150, 40);
            this.btn_submit.TabIndex = 14;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // lbl_validate_username
            // 
            this.lbl_validate_username.AutoSize = true;
            this.lbl_validate_username.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_validate_username.Location = new System.Drawing.Point(236, 9);
            this.lbl_validate_username.Name = "lbl_validate_username";
            this.lbl_validate_username.Size = new System.Drawing.Size(54, 13);
            this.lbl_validate_username.TabIndex = 8;
            this.lbl_validate_username.Text = "ok/not ok";
            // 
            // lbl_password
            // 
            this.lbl_password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(12, 35);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(53, 13);
            this.lbl_password.TabIndex = 9;
            this.lbl_password.Text = "Password";
            // 
            // lbl_username
            // 
            this.lbl_username.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(12, 9);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(55, 13);
            this.lbl_username.TabIndex = 0;
            this.lbl_username.Text = "Username";
            // 
            // textBox_password
            // 
            this.textBox_password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_password.Location = new System.Drawing.Point(73, 32);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(157, 20);
            this.textBox_password.TabIndex = 10;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_username_TextChanged);
            // 
            // textBox_username
            // 
            this.textBox_username.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_username.Location = new System.Drawing.Point(73, 6);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(157, 20);
            this.textBox_username.TabIndex = 4;
            this.textBox_username.TextChanged += new System.EventHandler(this.textBox_username_TextChanged);
            // 
            // lbl_mail
            // 
            this.lbl_mail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_mail.AutoSize = true;
            this.lbl_mail.Location = new System.Drawing.Point(12, 61);
            this.lbl_mail.Name = "lbl_mail";
            this.lbl_mail.Size = new System.Drawing.Size(26, 13);
            this.lbl_mail.TabIndex = 3;
            this.lbl_mail.Text = "Mail";
            // 
            // textBox_mail
            // 
            this.textBox_mail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_mail.Location = new System.Drawing.Point(73, 58);
            this.textBox_mail.Name = "textBox_mail";
            this.textBox_mail.Size = new System.Drawing.Size(157, 20);
            this.textBox_mail.TabIndex = 13;
            this.textBox_mail.TextChanged += new System.EventHandler(this.textBox_username_TextChanged);
            // 
            // lbl_validate_password
            // 
            this.lbl_validate_password.AutoSize = true;
            this.lbl_validate_password.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_validate_password.Location = new System.Drawing.Point(236, 35);
            this.lbl_validate_password.Name = "lbl_validate_password";
            this.lbl_validate_password.Size = new System.Drawing.Size(54, 13);
            this.lbl_validate_password.TabIndex = 11;
            this.lbl_validate_password.Text = "ok/not ok";
            // 
            // lbl_validate_mail
            // 
            this.lbl_validate_mail.AutoSize = true;
            this.lbl_validate_mail.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_validate_mail.Location = new System.Drawing.Point(236, 61);
            this.lbl_validate_mail.Name = "lbl_validate_mail";
            this.lbl_validate_mail.Size = new System.Drawing.Size(54, 13);
            this.lbl_validate_mail.TabIndex = 12;
            this.lbl_validate_mail.Text = "ok/not ok";
            // 
            // AddUserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 86);
            this.Controls.Add(this.lbl_validate_mail);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.lbl_validate_password);
            this.Controls.Add(this.textBox_mail);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.lbl_mail);
            this.Controls.Add(this.lbl_validate_username);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.textBox_password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register new user";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label lbl_validate_username;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label lbl_mail;
        private System.Windows.Forms.TextBox textBox_mail;
        private System.Windows.Forms.Label lbl_validate_password;
        private System.Windows.Forms.Label lbl_validate_mail;
    }
}