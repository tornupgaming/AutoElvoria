using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace AutoElvoria
{
    public partial class MainForm : Form
    {
        void Log(string s)
        { 
            if(m_LogWindow == null)
                m_LogWindow = new LogWindow();

            m_LogWindow.AddLog(s); 
        }

        Random r = new Random(DateTime.Now.Millisecond);
        LogWindow m_LogWindow;
        string m_CurrentPageTitle = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            IERegEdit.UseIE9();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Login(Constants.TestUsername, Constants.TestPassword);
            
        }

        private void web_GameView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Log("Document completed: " + e.Url.ToString());
        }

        private void PostData(string url, string data)
        {
            System.Text.Encoding textEncoder = System.Text.Encoding.UTF8;

            byte[] dataBytes = textEncoder.GetBytes(data);

            string AdditionalHeaders = "Content-Type: application/x-www-form-urlencoded" + Environment.NewLine;
            web_GameView.Navigate(url, "", dataBytes, AdditionalHeaders);

            //web_GameView.Navigate("login.php", "_SELF", dataBytes, "Content-Type: application/x-www-form-urlencoded");
        }
        private void Login(string user, string pass)
        {
            PostData("http://www.elvoria.com/login.php", "email_username=" + user + "&password=" + pass);
        }

        AttackState m_CurrentMobBattleState;

        public int GetCurrentLevel()
        {
            foreach (HtmlElement spanElement in web_GameView.Document.All)
            {
                if (spanElement.OuterHtml.Contains("page('stats')"))
                {
                    if (spanElement.OuterText.StartsWith(Constants.TestUsername))
                    {
                        if (spanElement.TagName == "A")
                        {
                            string[] split = spanElement.OuterText.Split(':');
                            try
                            {
                                return int.Parse(split[1]);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                        // window.AddLog("found span element: " + spanElement.OuterHtml);
                    }
                }
            }
            return 1;
        }

        private void SwitchStateIfTextDetected(string text, AttackState state)
        {
            if (web_GameView.DocumentText.Contains(text))
                m_CurrentMobBattleState = state;
        }

        private void AttackMobBasedOnLevel(int reduction)
        {
            int level = GetCurrentLevel();
            int mobIndex = level - reduction;
            mobIndex = Math.Max(0, mobIndex);
            mobIndex = Math.Min(mobIndex, MobProfile.Mobs.Length - 1);
            MobProfile mobToKill = MobProfile.Mobs[mobIndex];
            web_GameView.Document.InvokeScript("mob_battle", new object[] { mobToKill.id, 0 });
            Log("Attacking " + mobToKill.name + " (Level: " + mobToKill.level + ")");
            m_CurrentMobBattleState = AttackState.InBattle;
        }

        private void CompleteMobBattle()
        {
            foreach (HtmlElement element in web_GameView.Document.Links)
            {
                if (element.OuterHtml.Contains("complete_mob_battle"))
                {
                    // Get the id
                    string[] jsMobSplit = element.OuterHtml.Split('(');

                    string[] jsMobSplit2 = jsMobSplit[1].Split(')');

                    int jsMob = int.Parse(jsMobSplit2[0]);

                    //window.AddLog("attempting to useJS: complete_mob_battle(" + jsMob + ")");

                    web_GameView.Document.InvokeScript("complete_mob_battle", new object[] { jsMob });
                    MobProfile mobRewardType = MobProfile.GetMobFromID(jsMob);
                    if (mobRewardType != null)
                    {
                        Log("Getting reward from: " + mobRewardType.name);
                    }
                    //window.AddLog("Collecting previous reward from rat");
                    m_CurrentMobBattleState = AttackState.RewardScreen;
                }

            }
        }

        private void DetectPageState()
        {
            // Todo: Check for a login screen - just incase


            // Get the current page title
            string title = GetCurrentPageContentTitle();

            // Check for a blank screen requiring a refresh
            if (title == string.Empty && m_CurrentPageTitle == string.Empty)
            {
                Log("Refreshing page");
                web_GameView.Refresh();
                //Login(Constants.TestUsername, Constants.TestPassword);
                return;
            }

            m_CurrentPageTitle = title;
            if (m_CurrentPageTitle.CompareTo("Mob Battle!") == 0)
            {
                DetectMobBattleState();
            }

            if (m_CurrentPageTitle.CompareTo("News/Announcements") == 0)
            {
                m_CurrentMobBattleState = AttackState.Idle;
            }
        }

        private void DetectMobBattleState()
        {
            foreach (HtmlElement element in HtmlHelper.ElementsByClass(web_GameView.Document, "opponent-current-hp"))
            {
                if (element.OuterText.CompareTo("Slain!") == 0)
                {
                    m_CurrentMobBattleState = AttackState.EndOfBattle;
                }
                else
                {
                    m_CurrentMobBattleState = AttackState.InBattle;
                }
                return;
            }

            foreach (HtmlElement element in web_GameView.Document.Links)
            {
                if (element.OuterText.CompareTo("Battle again") == 0)
                {
                    m_CurrentMobBattleState = AttackState.RewardScreen;
                    return;
                }

                if (element.OuterText.CompareTo("here") == 0)
                {
                    if (element.OuterHtml.Contains("complete_mob_battle"))
                    {
                        m_CurrentMobBattleState = AttackState.ForgottenReward;
                        return;
                    }
                }
            }
        }



        private string GetCurrentPageContentTitle()
        {
            HtmlElement contentElement = web_GameView.Document.GetElementById("content");
            if (contentElement != null)
            {
                return GetStringBetweenTwoStrings(contentElement.OuterHtml, "<h2>", "</h2>");
            }
            else
            {
                Log("No content element detected, maybe bad page load?");
                return string.Empty;
            }
        }

        private string GetStringBetweenTwoStrings(string src, string regA, string regB)
        {
            string[] stringSplit = Regex.Split(src, regA);
            stringSplit = Regex.Split(stringSplit[1], regB);
            return stringSplit[0];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DetectPageState();

            timer1.Interval = (int)(r.NextDouble() * 2000.0f) + 1000;

            // Check for broken routine (e.g. forgot to collect reward)

           
            //if (web_GameView.DocumentText.Contains("You forgot"))
            //{
            //    web_GameView.Document.InvokeScript("complete_mob_battle", new object[] { 1 });
            //    Log("Collecting previous reward from rat");
            //    m_CurrentMobBattleState = AttackState.RewardScreen;
            //}

            switch (m_CurrentMobBattleState)
            {
                case AttackState.Idle:
                    Log("Idle");
                    AttackMobBasedOnLevel(3);
                    break;

                case AttackState.InBattle:
                    
                    break;

                case AttackState.EndOfBattle:
                    CompleteMobBattle();
                    break;

                case AttackState.RewardScreen:
                    AttackMobBasedOnLevel(3);
                    break;

                case AttackState.ForgottenReward:
                    CompleteMobBattle();
                    break;
            }
        }


    }
}
