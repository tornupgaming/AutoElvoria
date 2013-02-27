using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoElvoria
{
    public partial class LogWindow : Form
    {
        public LogWindow()
        {
            InitializeComponent();
            Show();
            
        }

        public void AddLog(string msg)
        {
            list_MessageBox.Items.Add("[" + DateTime.Now.ToString() + "] - " + msg);

            while (list_MessageBox.Items.Count > list_MessageBox.Size.Height / 13)
            {
                list_MessageBox.Items.RemoveAt(0);
            }
        }
    }
}
