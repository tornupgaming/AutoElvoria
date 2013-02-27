namespace AutoElvoria
{
    partial class LogWindow
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
            this.list_MessageBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // list_MessageBox
            // 
            this.list_MessageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_MessageBox.FormattingEnabled = true;
            this.list_MessageBox.Location = new System.Drawing.Point(0, 0);
            this.list_MessageBox.Name = "list_MessageBox";
            this.list_MessageBox.Size = new System.Drawing.Size(484, 212);
            this.list_MessageBox.TabIndex = 0;
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 212);
            this.Controls.Add(this.list_MessageBox);
            this.Name = "LogWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LogWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_MessageBox;
    }
}