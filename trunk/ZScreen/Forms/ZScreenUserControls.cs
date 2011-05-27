﻿using System;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        private void ProxyAccountTestButton_Click(object sender, EventArgs e)
        {
            ProxyInfo proxy = GetSelectedProxy();
            if (proxy != null)
            {
                Adapter.TestProxyAccount(proxy);
            }
        }

        private void ProxyAccountsRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (ucProxyAccounts.RemoveItem(sel))
            {
                Engine.conf.ProxyList.RemoveAt(sel);
            }
        }

        private void ProxyAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (Engine.conf.ProxyList != null && sel != -1 && sel < Engine.conf.ProxyList.Count && Engine.conf.ProxyList[sel] != null)
            {
                ProxyInfo acc = Engine.conf.ProxyList[sel];
                ucProxyAccounts.SettingsGrid.SelectedObject = acc;
                Engine.conf.ProxyActive = acc;
                Engine.conf.ProxySelected = ucProxyAccounts.AccountsList.SelectedIndex;
            }
            if (IsFormReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void ProxyAccountsAddButton_Click(object sender, EventArgs e)
        {
            ProxyAdd(new ProxyInfo(Environment.UserName, "", Adapter.GetDefaultWebProxyHost(), Adapter.GetDefaultWebProxyPort()));
            cboProxyConfig.SelectedIndex = (int)ProxyConfigType.ManualProxy;
        }
    }
}