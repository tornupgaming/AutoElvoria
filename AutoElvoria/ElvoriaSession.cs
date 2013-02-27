using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AutoElvoria
{
    class ElvoriaSession
    {
        private Form m_Form;
        private WebBrowser m_WebView;
        public static ListBox EventLog;
        private UserStats m_Stats = new UserStats();

        private bool m_HasLoggedExpForBattle = false;
        private int m_ExpThisSession = 0;
        private float m_TotalSecondsAtStart = 0.0f;
        private Stopwatch m_StopWatch;

        public ElvoriaSession(Form formView, WebBrowser webView)
        {
            m_Form = formView;
            m_WebView = webView;
            m_WebView.Navigate("http://www.elvoria.com/");
            m_WebView.ScriptErrorsSuppressed = true;
        }

        public static void Log(string str)
        {
            System.Diagnostics.Debug.WriteLine(str);
            EventLog.Items.Add("[" + DateTime.Now.ToShortTimeString() + "] " + str);
        }

        public bool Login(string username, string password)
        {
            if (!IsLoggedIn())
            {
                m_TotalSecondsAtStart = (float)(DateTime.Now - Process.GetCurrentProcess().StartTime).TotalSeconds;
                Log("Logging in account: " + username);                
                HtmlHelper.PostDataToWebBrowser(
                    m_WebView,
                    "http://www.elvoria.com/login.php",
                    "email_username=" + username + "&password=" + password);
            }
            return false;
        }

        public bool IsLoggedIn()
        {
            return false;
        }

        private HtmlElement GetContentElement()
        {
            try
            {
                return m_WebView.Document.GetElementById("content");
            }
            catch
            {
                return null;
            }
        }

        private bool DoesContentElementContain(string str)
        {
            HtmlElement content = GetContentElement();
            if (content != null)
            {
                if (content.InnerHtml != null)
                {
                    if (content.InnerHtml.Contains(str))
                        return true;
                }
            }
            return false;
        }

        public void OnUpdate()
        {
            TimeSpan timeSinceStart = DateTime.Now - Process.GetCurrentProcess().StartTime;
            //Log("Seconds since start: " + (timeSinceStart.TotalSeconds - m_TotalSecondsAtStart));
            if (m_WebView.Document == null)
                return;

            if (IsOnFailedLoginPage())
                return;

            if (IsOnLoginPage())
            {
                Login(Constants.TestUsername, Constants.TestPassword);
            }

            if (IsLightboxOpen())
            {
                Log("Closing popup box");
                m_WebView.Document.InvokeScript("clightbox");
                return;
            }

            switch (DetectAntiBotSecurity())
            {
                case SecurityType.NONE:
                    break;

                case SecurityType.CHECKLEVEL:
                    Log("AntiBot security: Checking level");

                    AttackMob();
                    return;

                case SecurityType.RECAPTCHA:
                    if (!shownUnknownSecurity)
                    {
                        Log("AntiBot security: Recaptcha");
                        shownUnknownSecurity = true;
                        FlashWindow.Flash(m_Form);
                        if (MessageBox.Show("AntiBot Detection", "Recaptcha Found", MessageBoxButtons.OK) == DialogResult.OK)
                        {

                        }
                    }
                    return;
            }

            if (IsOnStatsPage())
            {
                shownUnknownSecurity = false;
                //AttackMob();
                if (RequiresStatSpend())
                {
                    AllocateAbilityPoints();
                }
                else
                {
                    // Fight some shit
                    m_WebView.Document.InvokeScript("page", new Object[] { "areas" });
                }


                //m_Stats.GrabStatsFromWebView(m_WebView);
                return;
            }

            if (IsOnTheWoodsPage())
            {
                shownUnknownSecurity = false;
                AttackMob();
                return;
            }

            if (IsFighting())
            {
                shownUnknownSecurity = false;
                if (DoesContentElementContain("Your previous battle has not finished") ||
                    DoesContentElementContain("That Mob you're looking for does not exist"))
                {
                    OpenStats();
                    return;
                }

                foreach (HtmlElement element in m_WebView.Document.Links)
                {
                    if (element.OuterText != null && element.OuterText.Contains("Repeat last battle"))
                    {
                        // Log the exp
                        if (!m_HasLoggedExpForBattle)
                        {
                            m_HasLoggedExpForBattle = true;
                            m_ExpThisSession += GetExpForBattle();

                            double seconds = (timeSinceStart.TotalSeconds - m_TotalSecondsAtStart);
                            float minutes = (float)seconds / 60.0f;
                            float hours = minutes / 60.0f;

                            //Log("Active for " + seconds + " seconds, " + minutes + " minutes, " + hours + " hours");

                            Log("Session Exp: " + m_ExpThisSession.ToString() + " (Exp p/h: " + ((float)m_ExpThisSession/hours) + ")");
                        }

                        if (RequiresStatSpend())
                        {
                            Log("Found AP to spend");
                            OpenStats();
                        }
                        else
                        {
                            AttackMob();
                        }
                    }
                }
                return;
            }
        }

        private int GetExpForBattle()
        {
            HtmlElement[] elements = HtmlHelper.GetHTMLElements(m_WebView, "td", "green");
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].OuterHtml.Contains("Total EXP"))
                {
                    try
                    {
                        return Int32.Parse(elements[i + 1].OuterText.Replace("=", "").Replace(" ", "").Replace(",",""));
                    }
                    catch (Exception e) { Log("Exception caught parsing exp string " + e.Message); }
                }
            }

            return 0;
        }

        private void AllocateAbilityPoints()
        {
            // Spend points on agi (will later allow user to select which stats to spend on)
            AllocateDexterityPoint();
        }

        private void AllocateStrengthPoint()
        {
            Log("Allocating strength point");
            m_WebView.Document.InvokeScript("allocate_ap", new Object[] { "1" });
        }

        private void AllocateDexterityPoint()
        {
            Log("Allocating dexterity point");
            m_WebView.Document.InvokeScript("allocate_ap", new Object[] { "2" });
        }

        private void AllocateAgilityPoint()
        {
            Log("Allocating agility point");
            m_WebView.Document.InvokeScript("allocate_ap", new Object[] { "3" });
        }

        private void AllocateEndurancePoint()
        {
            Log("Allocating endurance point");
            m_WebView.Document.InvokeScript("allocate_ap", new Object[] { "4" });
        }

        private void OpenStats()
        {
            Log("Opening stats page");
            m_WebView.Document.InvokeScript("page", new Object[] { "stats" });
        }

        private void AttackMob()
        {
            int currentLevel = GetCurrentLevel();
            MobProfile mobToAttack = MobProfile.GetMobByLevel(Math.Max(1, currentLevel - Constants.MobLevelOffset));
            Log("Auto-attacking mob: " + mobToAttack.name + "(Level: " + mobToAttack.level + ")");
            m_WebView.Document.InvokeScript("attack_encounters", new Object[] { "auto", mobToAttack.id }); // Rat for now
            m_HasLoggedExpForBattle = false;
        }

        private void DetectCurrentPage()
        {
            //if (IsOnFailedLoginPage()) System.Diagnostics.Debug.WriteLine("FAILED LOGIN PAGE");
            //if (IsOnLoginPage()) System.Diagnostics.Debug.WriteLine("LOGIN PAGE");
            //if (IsOnStatsPage()) System.Diagnostics.Debug.WriteLine("STAT PAGE");
            //if (IsOnInventoryPage()) System.Diagnostics.Debug.WriteLine("inventory PAGE");
        }

        private bool IsOnFailedLoginPage()
        {
            if (m_WebView.Url == null) return false;
            return (m_WebView.Url.ToString() == "http://www.elvoria.com/index.php?error=noaccount");
        }

        private bool IsOnLoginPage()
        {
            if (m_WebView.Url == null) return false;
            if (m_WebView.Url.ToString() == "http://www.elvoria.com/index.php") return true;
            if (m_WebView.Url.ToString() == "http://www.elvoria.com/") return true;
            return false;
        }

        private bool IsOnStatsPage()
        {
            if (HtmlHelper.SearchForStringInBrowser(m_WebView, "Player Battles Left") <= 0) return false;
            if (HtmlHelper.SearchForStringInBrowser(m_WebView, "Mob Record") <= 0) return false;
            return true;
        }
        private bool shownUnknownSecurity = false;
        private SecurityType DetectAntiBotSecurity()
        {
            if (DoesContentElementContain("just a standard procedure!"))
            {
                if (DoesContentElementContain("recaptcha_table") && DoesContentElementContain("recaptcha_image"))
                {
                    return SecurityType.RECAPTCHA;
                }

                if (DoesContentElementContain("Please tell me your character level"))
                {
                    return SecurityType.CHECKLEVEL;
                }

                if (!shownUnknownSecurity)
                {
                    shownUnknownSecurity = true;
                    FlashWindow.Flash(m_Form);
                    if (MessageBox.Show("AntiBot Detection", "Unknown AntiBot Detection Procedure Found", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        shownUnknownSecurity = false;
                    }
                    HtmlHelper.DumpContentHTMLToOutput(m_WebView);
                }

            }

            return SecurityType.NONE;
        }

        private bool RequiresStatSpend()
        {
            try
            {
                HtmlElement apElement = HtmlHelper.GetHTMLElementByClassName(m_WebView, "stat-ap green");
                int apToSpend = Int32.Parse(apElement.InnerText);
                if (apToSpend > 0)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        private bool IsOnInventoryPage()
        {
            return DoesContentElementContain("<h2>Inventory</h2>");
        }

        private bool IsOnSkillsPage()
        {
            return DoesContentElementContain("<h2>Skills</h2>");
        }

        private bool IsOnAchievementsPage()
        {
            return DoesContentElementContain("<h2>Achievements</h2>");
        }

        private bool IsOnQuestsPage()
        {
            return DoesContentElementContain("<h2>Quests</h2>");
        }

        private bool IsOnCraftingPage()
        {
            return DoesContentElementContain("<h2>Crafting</h2>");
        }

        private bool IsOnTheWoodsPage()
        {
            return DoesContentElementContain("<h2>Area X</h2>");
        }

        private bool IsFighting()
        {
            return DoesContentElementContain("<h2>Mob Battle!</h2>");
        }

        private int GetCurrentLevel()
        {
            try
            {
                HtmlElementCollection spans = m_WebView.Document.GetElementsByTagName("span");
                foreach (HtmlElement element in spans)
                {
                    string cls = element.GetAttribute("className");
                    if (String.IsNullOrEmpty(cls) || !cls.Equals("stat-level"))
                        continue;

                    return Int32.Parse(element.InnerText.Replace(" ", ""));
                }
            }
            catch
            {
            }
            return 0;
        }



        private bool IsLightboxOpen()
        {
            if (m_WebView.Document != null)
            {
                HtmlElement lightboxElement = m_WebView.Document.GetElementById("lightbox");
                if (lightboxElement != null)
                {
                    if (lightboxElement.OuterHtml != null)
                    {
                        if (lightboxElement.OuterHtml.Contains("display: block"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
