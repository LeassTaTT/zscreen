/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2012  Thomas Braun, Jens Klingen, Robin Krom
 *
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Greenshot.Configuration;
using Greenshot.Controls;
using Greenshot.Destinations;
using Greenshot.Helpers;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using GreenshotPlugin.UnmanagedHelpers;

namespace Greenshot
{
    /// <summary>
    /// Description of SettingsForm.
    /// </summary>
    public partial class SettingsForm : GreenshotForm
    {
        private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(SettingsForm));
        private static CoreConfiguration coreConfiguration = IniConfig.GetIniSection<CoreConfiguration>();
        private static EditorConfiguration editorConfiguration = IniConfig.GetIniSection<EditorConfiguration>();
        private ToolTip toolTip = new ToolTip();

        public SettingsForm()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Icon = GreenshotPlugin.Core.GreenshotResources.getGreenshotIcon();

            // Fix for Vista/XP differences
            if (Environment.OSVersion.Version.Major >= 6)
            {
                this.trackBarJpegQuality.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                this.trackBarJpegQuality.BackColor = System.Drawing.SystemColors.Control;
            }

            DisplayPluginTab();
            UpdateUI();
            DisplaySettings();
            CheckSettings();
        }

        /// <summary>
        /// This is a method to popululate the ComboBox
        /// with the items from the enumeration
        /// </summary>
        /// <param name="comboBox">ComboBox to populate</param>
        /// <param name="enumeration">Enum to populate with</param>
        private void PopulateComboBox<ET>(ComboBox comboBox)
        {
            ET[] availableValues = (ET[])Enum.GetValues(typeof(ET));
            PopulateComboBox<ET>(comboBox, availableValues, availableValues[0]);
        }

        /// <summary>
        /// This is a method to popululate the ComboBox
        /// with the items from the enumeration
        /// </summary>
        /// <param name="comboBox">ComboBox to populate</param>
        /// <param name="enumeration">Enum to populate with</param>
        private void PopulateComboBox<ET>(ComboBox comboBox, ET[] availableValues, ET selectedValue)
        {
            comboBox.Items.Clear();
            string enumTypeName = typeof(ET).Name;
            foreach (ET enumValue in availableValues)
            {
                string translation = Language.GetString(enumTypeName + "." + enumValue.ToString());
                comboBox.Items.Add(translation);
            }
            comboBox.SelectedItem = Language.GetString(enumTypeName + "." + selectedValue.ToString());
        }

        /// <summary>
        /// Get the selected enum value from the combobox, uses generics
        /// </summary>
        /// <param name="comboBox">Combobox to get the value from</param>
        /// <returns>The generics value of the combobox</returns>
        private ET GetSelected<ET>(ComboBox comboBox)
        {
            string enumTypeName = typeof(ET).Name;
            string selectedValue = comboBox.SelectedItem as string;
            ET[] availableValues = (ET[])Enum.GetValues(typeof(ET));
            ET returnValue = availableValues[0];
            foreach (ET enumValue in availableValues)
            {
                string translation = Language.GetString(enumTypeName + "." + enumValue.ToString());
                if (translation.Equals(selectedValue))
                {
                    returnValue = enumValue;
                    break;
                }
            }
            return returnValue;
        }

        private void SetWindowCaptureMode(WindowCaptureMode selectedWindowCaptureMode)
        {
            WindowCaptureMode[] availableModes;
            if (!DWM.isDWMEnabled())
            {
                // Remove DWM from configuration, as DWM is disabled!
                if (coreConfiguration.WindowCaptureMode == WindowCaptureMode.Aero || coreConfiguration.WindowCaptureMode == WindowCaptureMode.AeroTransparent)
                {
                    coreConfiguration.WindowCaptureMode = WindowCaptureMode.GDI;
                }
                availableModes = new WindowCaptureMode[] { WindowCaptureMode.Auto, WindowCaptureMode.Screen, WindowCaptureMode.GDI };
            }
            else
            {
                availableModes = new WindowCaptureMode[] { WindowCaptureMode.Auto, WindowCaptureMode.Screen, WindowCaptureMode.GDI, WindowCaptureMode.Aero, WindowCaptureMode.AeroTransparent };
            }
            PopulateComboBox<WindowCaptureMode>(combobox_window_capture_mode, availableModes, selectedWindowCaptureMode);
        }

        private void DisplayPluginTab()
        {
            if (!PluginHelper.instance.HasPlugins())
            {
                this.tabcontrol.TabPages.Remove(tab_plugins);
            }
            else
            {
                // Draw the Plugin listview
                listview_plugins.BeginUpdate();
                listview_plugins.Items.Clear();
                listview_plugins.Columns.Clear();
                string[] columns = { "Name", "Version", "Created by", "DLL path" };
                foreach (string column in columns)
                {
                    listview_plugins.Columns.Add(column);
                }
                PluginHelper.instance.FillListview(this.listview_plugins);
                // Maximize Column size!
                for (int i = 0; i < listview_plugins.Columns.Count; i++)
                {
                    listview_plugins.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                    int width = listview_plugins.Columns[i].Width;
                    listview_plugins.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
                    if (width > listview_plugins.Columns[i].Width)
                    {
                        listview_plugins.Columns[i].Width = width;
                    }
                }
                listview_plugins.EndUpdate();
                listview_plugins.Refresh();

                // Disable the configure button, it will be enabled when a plugin is selected AND isConfigurable
                button_pluginconfigure.Enabled = false;
            }
        }

        /// <summary>
        /// Update the UI to reflect the language and other text settings
        /// </summary>
        private void UpdateUI()
        {
            toolTip.SetToolTip(label_language, Language.GetString(LangKey.settings_tooltip_language));
            toolTip.SetToolTip(label_storagelocation, Language.GetString(LangKey.settings_tooltip_storagelocation));
            toolTip.SetToolTip(label_screenshotname, Language.GetString(LangKey.settings_tooltip_filenamepattern));
            toolTip.SetToolTip(label_primaryimageformat, Language.GetString(LangKey.settings_tooltip_primaryimageformat));

            // Removing, otherwise we keep getting the event multiple times!
            this.combobox_language.SelectedIndexChanged -= new System.EventHandler(this.Combobox_languageSelectedIndexChanged);

            // Initialize the Language ComboBox
            this.combobox_language.DisplayMember = "Description";
            this.combobox_language.ValueMember = "Ietf";
            // Set datasource last to prevent problems
            // See: http://www.codeproject.com/KB/database/scomlistcontrolbinding.aspx?fid=111644
            this.combobox_language.DataSource = Language.SupportedLanguages;
            if (Language.CurrentLanguage != null)
            {
                this.combobox_language.SelectedValue = Language.CurrentLanguage;
            }

            // Delaying the SelectedIndexChanged events untill all is initiated
            this.combobox_language.SelectedIndexChanged += new System.EventHandler(this.Combobox_languageSelectedIndexChanged);
            UpdateDestinationDescriptions();
        }

        // Check the settings and somehow visibly mark when something is incorrect
        private bool CheckSettings()
        {
            bool settingsOk = true;
            if (!Directory.Exists(FilenameHelper.FillVariables(textbox_storagelocation.Text, false)))
            {
                textbox_storagelocation.BackColor = Color.Red;
                settingsOk = false;
            }
            else
            {
                textbox_storagelocation.BackColor = Control.DefaultBackColor;
            }
            return settingsOk;
        }

        /// <summary>
        /// Show all destination descriptions in the current language
        /// </summary>
        private void UpdateDestinationDescriptions()
        {
            foreach (ListViewItem item in destinationsListView.Items)
            {
                IDestination destination = item.Tag as IDestination;
                item.Text = destination.Description;
            }
        }

        /// <summary>
        /// Build the view with all the destinations
        /// </summary>
        private void DisplayDestinations()
        {
            checkbox_picker.Checked = false;

            destinationsListView.Items.Clear();
            destinationsListView.ListViewItemSorter = new ListviewWithDestinationComparer();
            ImageList imageList = new ImageList();
            destinationsListView.SmallImageList = imageList;
            int imageNr = -1;
            foreach (IDestination destination in DestinationHelper.GetAllDestinations())
            {
                Image destinationImage = destination.DisplayIcon;
                if (destinationImage != null)
                {
                    imageList.Images.Add(destination.DisplayIcon);
                    imageNr++;
                }
                if (PickerDestination.DESIGNATION.Equals(destination.Designation))
                {
                    checkbox_picker.Checked = coreConfiguration.OutputDestinations.Contains(destination.Designation);
                    checkbox_picker.Text = destination.Description;
                }
                else
                {
                    ListViewItem item;
                    if (destinationImage != null)
                    {
                        item = destinationsListView.Items.Add(destination.Description, imageNr);
                    }
                    else
                    {
                        item = destinationsListView.Items.Add(destination.Description);
                    }
                    item.Tag = destination;
                    item.Checked = coreConfiguration.OutputDestinations.Contains(destination.Designation);
                }
            }
            if (checkbox_picker.Checked)
            {
                destinationsListView.Enabled = false;
                foreach (int index in destinationsListView.CheckedIndices)
                {
                    ListViewItem item = destinationsListView.Items[index];
                    item.Checked = false;
                }
            }
        }

        private void DisplaySettings()
        {
            colorButton_window_background.SelectedColor = coreConfiguration.DWMBackgroundColor;

            if (Language.CurrentLanguage != null)
            {
                combobox_language.SelectedValue = Language.CurrentLanguage;
            }
            textbox_storagelocation.Text = FilenameHelper.FillVariables(coreConfiguration.OutputFilePath, false);

            SetWindowCaptureMode(coreConfiguration.WindowCaptureMode);

            trackBarJpegQuality.Value = coreConfiguration.OutputFileJpegQuality;
            textBoxJpegQuality.Text = coreConfiguration.OutputFileJpegQuality + "%";

            DisplayDestinations();

            numericUpDownWaitTime.Value = coreConfiguration.CaptureDelay >= 0 ? coreConfiguration.CaptureDelay : 0;

            // If the run for all is set we disable and set the checkbox
            if (StartupHelper.checkRunAll())
            {
                checkbox_autostartshortcut.Enabled = false;
                checkbox_autostartshortcut.Checked = true;
            }
            else
            {
                // No run for all, enable the checkbox and set it to true if the current user has a key
                checkbox_autostartshortcut.Enabled = true;
                checkbox_autostartshortcut.Checked = StartupHelper.checkRunUser();
            }

            numericUpDown_daysbetweencheck.Value = coreConfiguration.UpdateCheckInterval;
            CheckDestinationSettings();
        }

        private void SaveSettings()
        {
            if (combobox_language.SelectedItem != null)
            {
                string newLang = combobox_language.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(newLang))
                {
                    coreConfiguration.Language = combobox_language.SelectedValue.ToString();
                }
            }

            coreConfiguration.WindowCaptureMode = GetSelected<WindowCaptureMode>(combobox_window_capture_mode);
            if (!FilenameHelper.FillVariables(coreConfiguration.OutputFilePath, false).Equals(textbox_storagelocation.Text))
            {
                coreConfiguration.OutputFilePath = textbox_storagelocation.Text;
            }
            coreConfiguration.OutputFileJpegQuality = trackBarJpegQuality.Value;

            List<string> destinations = new List<string>();
            if (checkbox_picker.Checked)
            {
                destinations.Add(PickerDestination.DESIGNATION);
            }
            foreach (int index in destinationsListView.CheckedIndices)
            {
                ListViewItem item = destinationsListView.Items[index];

                IDestination destination = item.Tag as IDestination;
                if (item.Checked)
                {
                    destinations.Add(destination.Designation);
                }
            }
            coreConfiguration.OutputDestinations = destinations;
            coreConfiguration.CaptureDelay = (int)numericUpDownWaitTime.Value;
            coreConfiguration.DWMBackgroundColor = colorButton_window_background.SelectedColor;
            coreConfiguration.UpdateCheckInterval = (int)numericUpDown_daysbetweencheck.Value;

            IniConfig.Save();

            // Make sure the current language & settings are reflected in the Main-context menu
            MainForm.instance.UpdateUI();

            try
            {
                // Check if the Run for all is set
                if (!StartupHelper.checkRunAll())
                {
                    // If not set the registry according to the settings
                    if (checkbox_autostartshortcut.Checked)
                    {
                        StartupHelper.setRunUser();
                    }
                    else
                    {
                        StartupHelper.deleteRunUser();
                    }
                }
                else
                {
                    // The run key for Greenshot is set for all users, delete the local version!
                    StartupHelper.deleteRunUser();
                }
            }
            catch (Exception e)
            {
                LOG.Warn("Problem checking registry, ignoring for now: ", e);
            }
        }

        private void Settings_cancelClick(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Settings_okayClick(object sender, System.EventArgs e)
        {
            if (CheckSettings())
            {
                SaveSettings();
                DialogResult = DialogResult.OK;
            }
        }

        private void BrowseClick(object sender, System.EventArgs e)
        {
            // Get the storage location and replace the environment variables
            this.folderBrowserDialog1.SelectedPath = FilenameHelper.FillVariables(this.textbox_storagelocation.Text, false);
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Only change if there is a change, otherwise we might overwrite the environment variables
                if (this.folderBrowserDialog1.SelectedPath != null && !this.folderBrowserDialog1.SelectedPath.Equals(FilenameHelper.FillVariables(this.textbox_storagelocation.Text, false)))
                {
                    this.textbox_storagelocation.Text = this.folderBrowserDialog1.SelectedPath;
                }
            }
            CheckSettings();
        }

        private void TrackBarJpegQualityScroll(object sender, System.EventArgs e)
        {
            textBoxJpegQuality.Text = trackBarJpegQuality.Value.ToString();
        }

        private void BtnPatternHelpClick(object sender, EventArgs e)
        {
            string filenamepatternText = Language.GetString(LangKey.settings_message_filenamepattern);
            // Convert %NUM% to ${NUM} for old language files!
            filenamepatternText = Regex.Replace(filenamepatternText, "%([a-zA-Z_0-9]+)%", @"${$1}");
            MessageBox.Show(filenamepatternText, Language.GetString(LangKey.settings_filenamepattern));
        }

        private void Listview_pluginsSelectedIndexChanged(object sender, EventArgs e)
        {
            button_pluginconfigure.Enabled = PluginHelper.instance.isSelectedItemConfigurable(listview_plugins);
        }

        private void Button_pluginconfigureClick(object sender, EventArgs e)
        {
            PluginHelper.instance.ConfigureSelectedItem(listview_plugins);
        }

        private void Combobox_languageSelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the combobox values BEFORE changing the language
            //EmailFormat selectedEmailFormat = GetSelected<EmailFormat>(combobox_emailformat);
            WindowCaptureMode selectedWindowCaptureMode = GetSelected<WindowCaptureMode>(combobox_window_capture_mode);
            if (combobox_language.SelectedItem != null)
            {
                LOG.Debug("Setting language to: " + (string)combobox_language.SelectedValue);
                Language.CurrentLanguage = (string)combobox_language.SelectedValue;
            }
            // Reflect language changes to the settings form
            UpdateUI();

            // Reflect Language changes form
            ApplyLanguage();

            // Update the email & windows capture mode
            //SetEmailFormat(selectedEmailFormat);
            SetWindowCaptureMode(selectedWindowCaptureMode);
        }

        private void Combobox_window_capture_modeSelectedIndexChanged(object sender, EventArgs e)
        {
            int windowsVersion = Environment.OSVersion.Version.Major;
            WindowCaptureMode mode = GetSelected<WindowCaptureMode>(combobox_window_capture_mode);
            if (windowsVersion >= 6)
            {
                switch (mode)
                {
                    case WindowCaptureMode.Aero:
                    case WindowCaptureMode.Auto:
                        colorButton_window_background.Visible = true;
                        return;
                }
            }
            colorButton_window_background.Visible = false;
        }

        /// <summary>
        /// Check the destination settings
        /// </summary>
        private void CheckDestinationSettings()
        {
            bool clipboardDestinationChecked = false;
            bool pickerSelected = checkbox_picker.Checked;
            destinationsListView.Enabled = true;

            foreach (int index in destinationsListView.CheckedIndices)
            {
                ListViewItem item = destinationsListView.Items[index];
                IDestination destination = item.Tag as IDestination;
                if (destination.Designation.Equals(ClipboardDestination.DESIGNATION))
                {
                    clipboardDestinationChecked = true;
                    break;
                }
            }

            if (pickerSelected)
            {
                destinationsListView.Enabled = false;
                foreach (int index in destinationsListView.CheckedIndices)
                {
                    ListViewItem item = destinationsListView.Items[index];
                    item.Checked = false;
                }
            }
            else
            {
                // Prevent multiple clipboard settings at once, see bug #3435056
                if (clipboardDestinationChecked)
                {
                    checkbox_copypathtoclipboard.Checked = false;
                    checkbox_copypathtoclipboard.Enabled = false;
                }
                else
                {
                    checkbox_copypathtoclipboard.Enabled = true;
                }
            }
        }

        private void DestinationsCheckStateChanged(object sender, EventArgs e)
        {
            CheckDestinationSettings();
        }
    }

    public class ListviewWithDestinationComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is ListViewItem))
            {
                return (0);
            }
            if (!(y is ListViewItem))
            {
                return (0);
            }

            ListViewItem l1 = (ListViewItem)x;
            ListViewItem l2 = (ListViewItem)y;

            IDestination firstDestination = l1.Tag as IDestination;
            IDestination secondDestination = l2.Tag as IDestination;

            if (secondDestination == null)
            {
                return 1;
            }
            if (firstDestination.Priority == secondDestination.Priority)
            {
                return firstDestination.Description.CompareTo(secondDestination.Description);
            }
            return firstDestination.Priority - secondDestination.Priority;
        }
    }
}