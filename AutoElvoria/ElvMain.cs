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
    public partial class ElvMain : Form
    {
        private ElvoriaSession m_MainSession = null;
        private Random m_TimerRandomizer;

        public ElvMain() { InitializeComponent(); IERegEdit.UseIE9(); }

        private void ElvMain_Load(object sender, EventArgs e)
        {
            m_TimerRandomizer = new Random();
            m_MainSession = new ElvoriaSession(this, web_PrimaryWindow);
            ElvoriaSession.EventLog = list_EventLog;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if(txt_UserName.Text != string.Empty)
                m_MainSession.Login(txt_UserName.Text, txt_Password.Text);
            else
                m_MainSession.Login(Constants.TestUsername, Constants.TestPassword);
        }

        private void btn_dumphtml_Click(object sender, EventArgs e)
        {
            HtmlHelper.DumpHTMLToOutput(web_PrimaryWindow);
        }

        private void timer_Update_Tick(object sender, EventArgs e)
        {
            if (chk_IsRunning.Checked)
            {
                m_MainSession.OnUpdate();
                timer_Update.Interval = m_TimerRandomizer.Next(8000) + 1000;
            }
        }

        private void btn_DumpContentHTML_Click(object sender, EventArgs e)
        {
            HtmlHelper.DumpContentHTMLToOutput(web_PrimaryWindow);
        }
    }
}
