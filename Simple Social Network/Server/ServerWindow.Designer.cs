namespace Async_TCP_server_networking
{
    partial class ServerWindow
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox_requests = new System.Windows.Forms.ListBox();
            this.listbox_server_log = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 261);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listbox_server_log);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox_requests);
            this.splitContainer1.Size = new System.Drawing.Size(657, 261);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBox_requests
            // 
            this.listBox_requests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_requests.FormattingEnabled = true;
            this.listBox_requests.Location = new System.Drawing.Point(0, 0);
            this.listBox_requests.Name = "listBox_requests";
            this.listBox_requests.ScrollAlwaysVisible = true;
            this.listBox_requests.Size = new System.Drawing.Size(253, 261);
            this.listBox_requests.TabIndex = 0;
            // 
            // listbox_server_log
            // 
            this.listbox_server_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbox_server_log.FormattingEnabled = true;
            this.listbox_server_log.Location = new System.Drawing.Point(0, 0);
            this.listbox_server_log.Name = "listbox_server_log";
            this.listbox_server_log.ScrollAlwaysVisible = true;
            this.listbox_server_log.Size = new System.Drawing.Size(400, 261);
            this.listbox_server_log.TabIndex = 1;
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 261);
            this.Controls.Add(this.panel1);
            this.Name = "ServerWindow";
            this.Text = "Server";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBox_requests;
        private System.Windows.Forms.ListBox listbox_server_log;
    }
}

