﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib
{
    public partial class ProxyConfigUI : Form
    {
        #region 0 Properties

        private ProxyConfig Config = null;

        #endregion 0 Properties

        private void cboProxyConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ProxyConfigType = (EProxyConfigType)cboProxyConfig.SelectedIndex;
        }

        private ProxyInfo GetSelectedProxy()
        {
            ProxyInfo acc = null;
            if (ucProxyAccounts.AccountsList.SelectedIndex != -1 &&
                Config.ProxyList.Count >= ucProxyAccounts.AccountsList.Items.Count)
            {
                acc = Config.ProxyList[ucProxyAccounts.AccountsList.SelectedIndex];
            }

            return acc;
        }

        private void ProxyAccountsAddButton_Click(object sender, EventArgs e)
        {
            ProxyAdd(new ProxyInfo(Environment.UserName, "", ZAppHelper.GetDefaultWebProxyHost(), ZAppHelper.GetDefaultWebProxyPort()));
            cboProxyConfig.SelectedIndex = (int)EProxyConfigType.ManualProxy;
        }

        private void ProxyAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (Config.ProxyList != null && sel != -1 && sel < Config.ProxyList.Count && Config.ProxyList[sel] != null)
            {
                ProxyInfo acc = Config.ProxyList[sel];
                ucProxyAccounts.SettingsGrid.SelectedObject = acc;
                Config.ProxyActive = acc;
                Config.ProxySelected = ucProxyAccounts.AccountsList.SelectedIndex;
            }
        }

        private void ProxyAccountsRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (ucProxyAccounts.RemoveItem(sel))
            {
                Config.ProxyList.RemoveAt(sel);
            }
        }

        private void ProxyAccountTestButton_Click(object sender, EventArgs e)
        {
            ProxyInfo proxy = GetSelectedProxy();
            if (proxy != null)
            {
                TestProxyAccount(proxy);
            }
        }

        public void ProxyAdd(ProxyInfo acc)
        {
            Config.ProxyList.Add(acc);
            ucProxyAccounts.AccountsList.Items.Add(acc);
            ucProxyAccounts.AccountsList.SelectedIndex = ucProxyAccounts.AccountsList.Items.Count - 1;
        }

        public ProxyConfigUI(ProxyConfig config)
        {
            InitializeComponent();
            this.Config = config;

            // Options - Proxy
            ucProxyAccounts.btnAdd.Click += new EventHandler(ProxyAccountsAddButton_Click);
            ucProxyAccounts.btnRemove.Click += new EventHandler(ProxyAccountsRemoveButton_Click);
            ucProxyAccounts.btnTest.Click += new EventHandler(ProxyAccountTestButton_Click);
            ucProxyAccounts.AccountsList.SelectedIndexChanged += new EventHandler(ProxyAccountsList_SelectedIndexChanged);

            // Proxy
            if (cboProxyConfig.Items.Count == 0)
            {
                cboProxyConfig.Items.AddRange(typeof(EProxyConfigType).GetEnumDescriptions());
            }
            cboProxyConfig.SelectedIndex = (int)config.ProxyConfigType;

            ProxySetup(config.ProxyList);
            if (ucProxyAccounts.AccountsList.Items.Count > 0)
            {
                ucProxyAccounts.AccountsList.SelectedIndex = config.ProxySelected;
            }
        }

        private void ProxyConfigUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ProxyConfigUI_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Proxy Configurator";
        }

        private void ProxySetup(IEnumerable<ProxyInfo> accs)
        {
            if (accs != null)
            {
                ucProxyAccounts.AccountsList.Items.Clear();
                Config.ProxyList = new List<ProxyInfo>();
                Config.ProxyList.AddRange(accs);
                foreach (ProxyInfo acc in Config.ProxyList)
                {
                    ucProxyAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        public static void TestProxyAccount(ProxyInfo acc)
        {
            string msg = "Success!";

            try
            {
                NetworkCredential cred = new NetworkCredential(acc.UserName, acc.Password);
                WebProxy wp = new WebProxy(acc.GetAddress(), true, null, cred);
                WebClient wc = new WebClient { Proxy = wp };
                wc.DownloadString("http://www.google.com");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}