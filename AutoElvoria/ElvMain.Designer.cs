namespace AutoElvoria
{
    partial class ElvMain
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.web_PrimaryWindow = new System.Windows.Forms.WebBrowser();
            this.tab_UserControl = new System.Windows.Forms.TabControl();
            this.tab_UserData = new System.Windows.Forms.TabPage();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.tab_Debug = new System.Windows.Forms.TabPage();
            this.chk_IsRunning = new System.Windows.Forms.CheckBox();
            this.btn_DumpContentHTML = new System.Windows.Forms.Button();
            this.btn_dumphtml = new System.Windows.Forms.Button();
            this.timer_Update = new System.Windows.Forms.Timer(this.components);
            this.list_EventLog = new System.Windows.Forms.ListBox();
            this.tab_Log = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tab_UserControl.SuspendLayout();
            this.tab_UserData.SuspendLayout();
            this.tab_Debug.SuspendLayout();
            this.tab_Log.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.web_PrimaryWindow);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tab_UserControl);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 682);
            this.splitContainer1.SplitterDistance = 980;
            this.splitContainer1.TabIndex = 0;
            // 
            // web_PrimaryWindow
            // 
            this.web_PrimaryWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web_PrimaryWindow.Location = new System.Drawing.Point(0, 0);
            this.web_PrimaryWindow.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_PrimaryWindow.Name = "web_PrimaryWindow";
            this.web_PrimaryWindow.Size = new System.Drawing.Size(980, 682);
            this.web_PrimaryWindow.TabIndex = 0;
            // 
            // tab_UserControl
            // 
            this.tab_UserControl.Controls.Add(this.tab_UserData);
            this.tab_UserControl.Controls.Add(this.tab_Log);
            this.tab_UserControl.Controls.Add(this.tab_Debug);
            this.tab_UserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_UserControl.Location = new System.Drawing.Point(0, 0);
            this.tab_UserControl.Name = "tab_UserControl";
            this.tab_UserControl.SelectedIndex = 0;
            this.tab_UserControl.Size = new System.Drawing.Size(280, 682);
            this.tab_UserControl.TabIndex = 0;
            // 
            // tab_UserData
            // 
            this.tab_UserData.Controls.Add(this.txt_Password);
            this.tab_UserData.Controls.Add(this.lbl_Password);
            this.tab_UserData.Controls.Add(this.lbl_UserName);
            this.tab_UserData.Controls.Add(this.txt_UserName);
            this.tab_UserData.Controls.Add(this.btn_Login);
            this.tab_UserData.Location = new System.Drawing.Point(4, 22);
            this.tab_UserData.Name = "tab_UserData";
            this.tab_UserData.Padding = new System.Windows.Forms.Padding(3);
            this.tab_UserData.Size = new System.Drawing.Size(272, 656);
            this.tab_UserData.TabIndex = 0;
            this.tab_UserData.Text = "User";
            this.tab_UserData.UseVisualStyleBackColor = true;
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(70, 31);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(194, 20);
            this.txt_Password.TabIndex = 4;
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Location = new System.Drawing.Point(6, 34);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(56, 13);
            this.lbl_Password.TabIndex = 3;
            this.lbl_Password.Text = "Password:";
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Location = new System.Drawing.Point(6, 9);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(58, 13);
            this.lbl_UserName.TabIndex = 2;
            this.lbl_UserName.Text = "Username:";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(70, 6);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(194, 20);
            this.txt_UserName.TabIndex = 1;
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(9, 57);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(255, 23);
            this.btn_Login.TabIndex = 0;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // tab_Debug
            // 
            this.tab_Debug.Controls.Add(this.chk_IsRunning);
            this.tab_Debug.Controls.Add(this.btn_DumpContentHTML);
            this.tab_Debug.Controls.Add(this.btn_dumphtml);
            this.tab_Debug.Location = new System.Drawing.Point(4, 22);
            this.tab_Debug.Name = "tab_Debug";
            this.tab_Debug.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Debug.Size = new System.Drawing.Size(272, 656);
            this.tab_Debug.TabIndex = 1;
            this.tab_Debug.Text = "Debug";
            this.tab_Debug.UseVisualStyleBackColor = true;
            // 
            // chk_IsRunning
            // 
            this.chk_IsRunning.AutoSize = true;
            this.chk_IsRunning.Checked = true;
            this.chk_IsRunning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_IsRunning.Location = new System.Drawing.Point(6, 6);
            this.chk_IsRunning.Name = "chk_IsRunning";
            this.chk_IsRunning.Size = new System.Drawing.Size(66, 17);
            this.chk_IsRunning.TabIndex = 7;
            this.chk_IsRunning.Text = "Running";
            this.chk_IsRunning.UseVisualStyleBackColor = true;
            // 
            // btn_DumpContentHTML
            // 
            this.btn_DumpContentHTML.Location = new System.Drawing.Point(6, 69);
            this.btn_DumpContentHTML.Name = "btn_DumpContentHTML";
            this.btn_DumpContentHTML.Size = new System.Drawing.Size(110, 34);
            this.btn_DumpContentHTML.TabIndex = 6;
            this.btn_DumpContentHTML.Text = "Dump Content Html";
            this.btn_DumpContentHTML.UseVisualStyleBackColor = true;
            this.btn_DumpContentHTML.Click += new System.EventHandler(this.btn_DumpContentHTML_Click);
            // 
            // btn_dumphtml
            // 
            this.btn_dumphtml.Location = new System.Drawing.Point(6, 29);
            this.btn_dumphtml.Name = "btn_dumphtml";
            this.btn_dumphtml.Size = new System.Drawing.Size(110, 34);
            this.btn_dumphtml.TabIndex = 5;
            this.btn_dumphtml.Text = "dump html";
            this.btn_dumphtml.UseVisualStyleBackColor = true;
            this.btn_dumphtml.Click += new System.EventHandler(this.btn_dumphtml_Click);
            // 
            // timer_Update
            // 
            this.timer_Update.Enabled = true;
            this.timer_Update.Tick += new System.EventHandler(this.timer_Update_Tick);
            // 
            // list_EventLog
            // 
            this.list_EventLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_EventLog.FormattingEnabled = true;
            this.list_EventLog.Location = new System.Drawing.Point(0, 0);
            this.list_EventLog.Margin = new System.Windows.Forms.Padding(0);
            this.list_EventLog.Name = "list_EventLog";
            this.list_EventLog.Size = new System.Drawing.Size(272, 656);
            this.list_EventLog.TabIndex = 8;
            // 
            // tab_Log
            // 
            this.tab_Log.Controls.Add(this.list_EventLog);
            this.tab_Log.Location = new System.Drawing.Point(4, 22);
            this.tab_Log.Name = "tab_Log";
            this.tab_Log.Size = new System.Drawing.Size(272, 656);
            this.tab_Log.TabIndex = 2;
            this.tab_Log.Text = "Log";
            this.tab_Log.UseVisualStyleBackColor = true;
            // 
            // ElvMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ElvMain";
            this.Text = "ElvMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ElvMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tab_UserControl.ResumeLayout(false);
            this.tab_UserData.ResumeLayout(false);
            this.tab_UserData.PerformLayout();
            this.tab_Debug.ResumeLayout(false);
            this.tab_Debug.PerformLayout();
            this.tab_Log.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser web_PrimaryWindow;
        private System.Windows.Forms.TabControl tab_UserControl;
        private System.Windows.Forms.TabPage tab_UserData;
        private System.Windows.Forms.TabPage tab_Debug;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_dumphtml;
        private System.Windows.Forms.Timer timer_Update;
        private System.Windows.Forms.Button btn_DumpContentHTML;
        private System.Windows.Forms.CheckBox chk_IsRunning;
        private System.Windows.Forms.ListBox list_EventLog;
        private System.Windows.Forms.TabPage tab_Log;
    }
}