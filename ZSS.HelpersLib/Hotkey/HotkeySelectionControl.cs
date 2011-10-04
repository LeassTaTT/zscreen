﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelpersLib.Hotkey
{
    public partial class HotkeySelectionControl : UserControl
    {
        public event EventHandler HotkeyChanged;

        public HotkeySetting Setting { get; set; }

        public HotkeySelectionControl(HotkeySetting setting, ZAppType host)
        {
            InitializeComponent();
            Setting = setting;
            switch (host)
            {
                case ZAppType.ZScreen:
                    lblHotkeyDescription.Text = ((ZScreenHotkey)Setting.Tag).GetDescription();
                    break;
                default:
                    lblHotkeyDescription.Text = ((ZUploaderHotkey)Setting.Tag).GetDescription();
                    break;
            }
            btnSetHotkey.Text = new KeyInfo(Setting.Hotkey).ToString();
            UpdateHotkeyStatus();
        }

        private void btnSetHotkey_Click(object sender, EventArgs e)
        {
            using (HotkeyInputForm inputForm = new HotkeyInputForm(Setting.Hotkey, Setting.HotkeyDefault))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    Setting.Hotkey = inputForm.SelectedKey;
                    btnSetHotkey.Text = new KeyInfo(Setting.Hotkey).ToString();
                    OnHotkeyChanged();
                    UpdateHotkeyStatus();
                }
            }
        }

        private void UpdateHotkeyStatus()
        {
            switch (Setting.IsActive)
            {
                case HotkeyStatus.Failed:
                    lblIsHotkeyActive.BackColor = Color.IndianRed;
                    break;
                case HotkeyStatus.NotConfigured:
                    lblIsHotkeyActive.BackColor = Color.LightGoldenrodYellow;
                    break;
                case HotkeyStatus.Registered:
                    lblIsHotkeyActive.BackColor = Color.PaleGreen;
                    break;
            }
        }

        protected void OnHotkeyChanged()
        {
            if (HotkeyChanged != null)
            {
                HotkeyChanged(this, null);
            }
        }
    }
}