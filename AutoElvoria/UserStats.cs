using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoElvoria
{
    class UserStats
    {
        public int AP;
        public float Strength;
        public float Dexterity;
        public float Agility;
        public float Endurance;
        public float Luck;

        public void GrabStatsFromWebView(WebBrowser webBrowser)
        {
            HtmlElement apElement = HtmlHelper.GetHTMLElementByClassName(webBrowser, "stat-ap green");
            AP = Int32.Parse(apElement.InnerText);
            //System.Diagnostics.Debug.WriteLine("AP Remaining: " + AP.ToString());


        }
    }
}
