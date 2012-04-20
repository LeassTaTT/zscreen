﻿/*
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
namespace Greenshot {
	partial class SettingsForm : GreenshotPlugin.Controls.GreenshotForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.textbox_storagelocation = new System.Windows.Forms.TextBox();
			this.label_storagelocation = new GreenshotPlugin.Controls.GreenshotLabel();
			this.settings_cancel = new GreenshotPlugin.Controls.GreenshotButton();
			this.settings_okay = new GreenshotPlugin.Controls.GreenshotButton();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.browse = new System.Windows.Forms.Button();
			this.label_screenshotname = new GreenshotPlugin.Controls.GreenshotLabel();
			this.textbox_screenshotname = new GreenshotPlugin.Controls.GreenshotTextBox();
			this.label_language = new GreenshotPlugin.Controls.GreenshotLabel();
			this.combobox_language = new System.Windows.Forms.ComboBox();
			this.combobox_primaryimageformat = new GreenshotPlugin.Controls.GreenshotComboBox();
			this.label_primaryimageformat = new GreenshotPlugin.Controls.GreenshotLabel();
			this.groupbox_preferredfilesettings = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.btnPatternHelp = new System.Windows.Forms.Button();
			this.checkbox_copypathtoclipboard = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.groupbox_applicationsettings = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_autostartshortcut = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.groupbox_jpegsettings = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_alwaysshowjpegqualitydialog = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.label_jpegquality = new GreenshotPlugin.Controls.GreenshotLabel();
			this.textBoxJpegQuality = new System.Windows.Forms.TextBox();
			this.trackBarJpegQuality = new System.Windows.Forms.TrackBar();
			this.groupbox_destination = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_picker = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.destinationsListView = new System.Windows.Forms.ListView();
			this.destination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabcontrol = new System.Windows.Forms.TabControl();
			this.tab_general = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.groupbox_network = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.numericUpDown_daysbetweencheck = new System.Windows.Forms.NumericUpDown();
			this.label_checkperiod = new GreenshotPlugin.Controls.GreenshotLabel();
			this.checkbox_usedefaultproxy = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.groupbox_hotkeys = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.label_lastregion_hotkey = new GreenshotPlugin.Controls.GreenshotLabel();
			this.lastregion_hotkeyControl = new GreenshotPlugin.Controls.HotkeyControl();
			this.label_ie_hotkey = new GreenshotPlugin.Controls.GreenshotLabel();
			this.ie_hotkeyControl = new GreenshotPlugin.Controls.HotkeyControl();
			this.label_region_hotkey = new GreenshotPlugin.Controls.GreenshotLabel();
			this.label_window_hotkey = new GreenshotPlugin.Controls.GreenshotLabel();
			this.label_fullscreen_hotkey = new GreenshotPlugin.Controls.GreenshotLabel();
			this.region_hotkeyControl = new GreenshotPlugin.Controls.HotkeyControl();
			this.window_hotkeyControl = new GreenshotPlugin.Controls.HotkeyControl();
			this.fullscreen_hotkeyControl = new GreenshotPlugin.Controls.HotkeyControl();
			this.tab_capture = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.groupbox_editor = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_editor_match_capture_size = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.groupbox_iecapture = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_ie_capture = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.groupbox_windowscapture = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.colorButton_window_background = new Greenshot.Controls.ColorButton();
			this.label_window_capture_mode = new GreenshotPlugin.Controls.GreenshotLabel();
			this.checkbox_capture_windows_interactive = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.combobox_window_capture_mode = new System.Windows.Forms.ComboBox();
			this.groupbox_capture = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_playsound = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkbox_capture_mousepointer = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.numericUpDownWaitTime = new System.Windows.Forms.NumericUpDown();
			this.label_waittime = new GreenshotPlugin.Controls.GreenshotLabel();
			this.tab_output = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.tab_destinations = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.tab_printer = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.groupbox_printoptions = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkboxPrintInverted = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkbox_alwaysshowprintoptionsdialog = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkboxTimestamp = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkboxAllowCenter = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkboxAllowRotate = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkboxAllowEnlarge = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.checkboxAllowShrink = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.tab_plugins = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.groupbox_plugins = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.listview_plugins = new System.Windows.Forms.ListView();
			this.button_pluginconfigure = new System.Windows.Forms.Button();
			this.tab_expert = new GreenshotPlugin.Controls.GreenshotTabPage();
			this.groupbox_expert = new GreenshotPlugin.Controls.GreenshotGroupBox();
			this.checkbox_autoreducecolors = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.label_clipboardformats = new GreenshotPlugin.Controls.GreenshotLabel();
			this.checkbox_enableexpert = new GreenshotPlugin.Controls.GreenshotCheckBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.groupbox_preferredfilesettings.SuspendLayout();
			this.groupbox_applicationsettings.SuspendLayout();
			this.groupbox_jpegsettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarJpegQuality)).BeginInit();
			this.groupbox_destination.SuspendLayout();
			this.tabcontrol.SuspendLayout();
			this.tab_general.SuspendLayout();
			this.groupbox_network.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_daysbetweencheck)).BeginInit();
			this.groupbox_hotkeys.SuspendLayout();
			this.tab_capture.SuspendLayout();
			this.groupbox_editor.SuspendLayout();
			this.groupbox_iecapture.SuspendLayout();
			this.groupbox_windowscapture.SuspendLayout();
			this.groupbox_capture.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).BeginInit();
			this.tab_output.SuspendLayout();
			this.tab_destinations.SuspendLayout();
			this.tab_printer.SuspendLayout();
			this.groupbox_printoptions.SuspendLayout();
			this.tab_plugins.SuspendLayout();
			this.groupbox_plugins.SuspendLayout();
			this.tab_expert.SuspendLayout();
			this.groupbox_expert.SuspendLayout();
			this.SuspendLayout();
			// 
			// textbox_storagelocation
			// 
			this.textbox_storagelocation.Location = new System.Drawing.Point(138, 18);
			this.textbox_storagelocation.Name = "textbox_storagelocation";
			this.textbox_storagelocation.Size = new System.Drawing.Size(233, 20);
			this.textbox_storagelocation.TabIndex = 12;
			// 
			// label_storagelocation
			// 
			this.label_storagelocation.LanguageKey = "settings_storagelocation";
			this.label_storagelocation.Location = new System.Drawing.Point(6, 21);
			this.label_storagelocation.Name = "label_storagelocation";
			this.label_storagelocation.Size = new System.Drawing.Size(126, 23);
			this.label_storagelocation.TabIndex = 11;
			this.label_storagelocation.Text = "Storage Location";
			// 
			// settings_cancel
			// 
			this.settings_cancel.LanguageKey = "editor_cancel";
			this.settings_cancel.Location = new System.Drawing.Point(367, 366);
			this.settings_cancel.Name = "settings_cancel";
			this.settings_cancel.Size = new System.Drawing.Size(75, 23);
			this.settings_cancel.TabIndex = 7;
			this.settings_cancel.Text = "Cancel";
			this.settings_cancel.UseVisualStyleBackColor = true;
			this.settings_cancel.Click += new System.EventHandler(this.Settings_cancelClick);
			// 
			// settings_okay
			// 
			this.settings_okay.LanguageKey = "editor_confirm";
			this.settings_okay.Location = new System.Drawing.Point(286, 366);
			this.settings_okay.Name = "settings_okay";
			this.settings_okay.Size = new System.Drawing.Size(75, 23);
			this.settings_okay.TabIndex = 6;
			this.settings_okay.Text = "OK";
			this.settings_okay.UseVisualStyleBackColor = true;
			this.settings_okay.Click += new System.EventHandler(this.Settings_okayClick);
			// 
			// browse
			// 
			this.browse.Location = new System.Drawing.Point(371, 17);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(35, 23);
			this.browse.TabIndex = 1;
			this.browse.Text = "...";
			this.browse.UseVisualStyleBackColor = true;
			this.browse.Click += new System.EventHandler(this.BrowseClick);
			// 
			// label_screenshotname
			// 
			this.label_screenshotname.LanguageKey = "settings_filenamepattern";
			this.label_screenshotname.Location = new System.Drawing.Point(6, 44);
			this.label_screenshotname.Name = "label_screenshotname";
			this.label_screenshotname.Size = new System.Drawing.Size(126, 23);
			this.label_screenshotname.TabIndex = 9;
			this.label_screenshotname.Text = "Filename pattern";
			// 
			// textbox_screenshotname
			// 
			this.textbox_screenshotname.Location = new System.Drawing.Point(138, 41);
			this.textbox_screenshotname.Name = "textbox_screenshotname";
			this.textbox_screenshotname.PropertyName = "OutputFileFilenamePattern";
			this.textbox_screenshotname.Size = new System.Drawing.Size(233, 20);
			this.textbox_screenshotname.TabIndex = 2;
			// 
			// label_language
			// 
			this.label_language.LanguageKey = "settings_language";
			this.label_language.Location = new System.Drawing.Point(6, 20);
			this.label_language.Name = "label_language";
			this.label_language.Size = new System.Drawing.Size(181, 23);
			this.label_language.TabIndex = 10;
			this.label_language.Text = "Language";
			// 
			// combobox_language
			// 
			this.combobox_language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox_language.FormattingEnabled = true;
			this.combobox_language.Location = new System.Drawing.Point(193, 17);
			this.combobox_language.MaxDropDownItems = 15;
			this.combobox_language.Name = "combobox_language";
			this.combobox_language.Size = new System.Drawing.Size(213, 21);
			this.combobox_language.TabIndex = 0;
			// 
			// combobox_primaryimageformat
			// 
			this.combobox_primaryimageformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox_primaryimageformat.FormattingEnabled = true;
			this.combobox_primaryimageformat.Location = new System.Drawing.Point(138, 64);
			this.combobox_primaryimageformat.Name = "combobox_primaryimageformat";
			this.combobox_primaryimageformat.PropertyName = "OutputFileFormat";
			this.combobox_primaryimageformat.Size = new System.Drawing.Size(268, 21);
			this.combobox_primaryimageformat.TabIndex = 4;
			// 
			// label_primaryimageformat
			// 
			this.label_primaryimageformat.LanguageKey = "settings_primaryimageformat";
			this.label_primaryimageformat.Location = new System.Drawing.Point(6, 67);
			this.label_primaryimageformat.Name = "label_primaryimageformat";
			this.label_primaryimageformat.Size = new System.Drawing.Size(126, 19);
			this.label_primaryimageformat.TabIndex = 8;
			this.label_primaryimageformat.Text = "Primary image format";
			// 
			// groupbox_preferredfilesettings
			// 
			this.groupbox_preferredfilesettings.Controls.Add(this.btnPatternHelp);
			this.groupbox_preferredfilesettings.Controls.Add(this.checkbox_copypathtoclipboard);
			this.groupbox_preferredfilesettings.Controls.Add(this.combobox_primaryimageformat);
			this.groupbox_preferredfilesettings.Controls.Add(this.label_primaryimageformat);
			this.groupbox_preferredfilesettings.Controls.Add(this.label_storagelocation);
			this.groupbox_preferredfilesettings.Controls.Add(this.browse);
			this.groupbox_preferredfilesettings.Controls.Add(this.textbox_storagelocation);
			this.groupbox_preferredfilesettings.Controls.Add(this.textbox_screenshotname);
			this.groupbox_preferredfilesettings.Controls.Add(this.label_screenshotname);
			this.groupbox_preferredfilesettings.LanguageKey = "settings_preferredfilesettings";
			this.groupbox_preferredfilesettings.Location = new System.Drawing.Point(2, 6);
			this.groupbox_preferredfilesettings.Name = "groupbox_preferredfilesettings";
			this.groupbox_preferredfilesettings.Size = new System.Drawing.Size(412, 122);
			this.groupbox_preferredfilesettings.TabIndex = 13;
			this.groupbox_preferredfilesettings.TabStop = false;
			this.groupbox_preferredfilesettings.Text = "Preferred Output File Settings";
			// 
			// btnPatternHelp
			// 
			this.btnPatternHelp.Location = new System.Drawing.Point(371, 39);
			this.btnPatternHelp.Name = "btnPatternHelp";
			this.btnPatternHelp.Size = new System.Drawing.Size(35, 23);
			this.btnPatternHelp.TabIndex = 19;
			this.btnPatternHelp.Text = "?";
			this.btnPatternHelp.UseVisualStyleBackColor = true;
			this.btnPatternHelp.Click += new System.EventHandler(this.BtnPatternHelpClick);
			// 
			// checkbox_copypathtoclipboard
			// 
			this.checkbox_copypathtoclipboard.LanguageKey = "settings_copypathtoclipboard";
			this.checkbox_copypathtoclipboard.Location = new System.Drawing.Point(12, 89);
			this.checkbox_copypathtoclipboard.Name = "checkbox_copypathtoclipboard";
			this.checkbox_copypathtoclipboard.PropertyName = "OutputFileCopyPathToClipboard";
			this.checkbox_copypathtoclipboard.Size = new System.Drawing.Size(394, 24);
			this.checkbox_copypathtoclipboard.TabIndex = 18;
			this.checkbox_copypathtoclipboard.Text = "Copy file path to clipboard every time an image is saved";
			this.checkbox_copypathtoclipboard.UseVisualStyleBackColor = true;
			// 
			// groupbox_applicationsettings
			// 
			this.groupbox_applicationsettings.Controls.Add(this.checkbox_autostartshortcut);
			this.groupbox_applicationsettings.Controls.Add(this.label_language);
			this.groupbox_applicationsettings.Controls.Add(this.combobox_language);
			this.groupbox_applicationsettings.LanguageKey = "settings_applicationsettings";
			this.groupbox_applicationsettings.Location = new System.Drawing.Point(2, 6);
			this.groupbox_applicationsettings.Name = "groupbox_applicationsettings";
			this.groupbox_applicationsettings.Size = new System.Drawing.Size(412, 68);
			this.groupbox_applicationsettings.TabIndex = 14;
			this.groupbox_applicationsettings.TabStop = false;
			this.groupbox_applicationsettings.Text = "Application Settings";
			// 
			// checkbox_autostartshortcut
			// 
			this.checkbox_autostartshortcut.LanguageKey = "settings_autostartshortcut";
			this.checkbox_autostartshortcut.Location = new System.Drawing.Point(8, 39);
			this.checkbox_autostartshortcut.Name = "checkbox_autostartshortcut";
			this.checkbox_autostartshortcut.Size = new System.Drawing.Size(397, 25);
			this.checkbox_autostartshortcut.TabIndex = 15;
			this.checkbox_autostartshortcut.Text = "Launch Greenshot on startup";
			this.checkbox_autostartshortcut.UseVisualStyleBackColor = true;
			// 
			// groupbox_jpegsettings
			// 
			this.groupbox_jpegsettings.Controls.Add(this.checkbox_alwaysshowjpegqualitydialog);
			this.groupbox_jpegsettings.Controls.Add(this.label_jpegquality);
			this.groupbox_jpegsettings.Controls.Add(this.textBoxJpegQuality);
			this.groupbox_jpegsettings.Controls.Add(this.trackBarJpegQuality);
			this.groupbox_jpegsettings.LanguageKey = "settings_jpegsettings";
			this.groupbox_jpegsettings.Location = new System.Drawing.Point(2, 156);
			this.groupbox_jpegsettings.Name = "groupbox_jpegsettings";
			this.groupbox_jpegsettings.Size = new System.Drawing.Size(412, 83);
			this.groupbox_jpegsettings.TabIndex = 14;
			this.groupbox_jpegsettings.TabStop = false;
			this.groupbox_jpegsettings.Text = "JPEG Settings";
			// 
			// checkbox_alwaysshowjpegqualitydialog
			// 
			this.checkbox_alwaysshowjpegqualitydialog.LanguageKey = "settings_alwaysshowjpegqualitydialog";
			this.checkbox_alwaysshowjpegqualitydialog.Location = new System.Drawing.Point(12, 50);
			this.checkbox_alwaysshowjpegqualitydialog.Name = "checkbox_alwaysshowjpegqualitydialog";
			this.checkbox_alwaysshowjpegqualitydialog.PropertyName = "OutputFilePromptQuality";
			this.checkbox_alwaysshowjpegqualitydialog.Size = new System.Drawing.Size(394, 25);
			this.checkbox_alwaysshowjpegqualitydialog.TabIndex = 16;
			this.checkbox_alwaysshowjpegqualitydialog.Text = "Show quality dialog every time a JPEG image is saved";
			this.checkbox_alwaysshowjpegqualitydialog.UseVisualStyleBackColor = true;
			// 
			// label_jpegquality
			// 
			this.label_jpegquality.LanguageKey = "settings_jpegquality";
			this.label_jpegquality.Location = new System.Drawing.Point(6, 24);
			this.label_jpegquality.Name = "label_jpegquality";
			this.label_jpegquality.Size = new System.Drawing.Size(116, 23);
			this.label_jpegquality.TabIndex = 13;
			this.label_jpegquality.Text = "JPEG Quality";
			// 
			// textBoxJpegQuality
			// 
			this.textBoxJpegQuality.Location = new System.Drawing.Point(371, 21);
			this.textBoxJpegQuality.Name = "textBoxJpegQuality";
			this.textBoxJpegQuality.ReadOnly = true;
			this.textBoxJpegQuality.Size = new System.Drawing.Size(35, 20);
			this.textBoxJpegQuality.TabIndex = 13;
			this.textBoxJpegQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// trackBarJpegQuality
			// 
			this.trackBarJpegQuality.LargeChange = 10;
			this.trackBarJpegQuality.Location = new System.Drawing.Point(138, 21);
			this.trackBarJpegQuality.Maximum = 100;
			this.trackBarJpegQuality.Name = "trackBarJpegQuality";
			this.trackBarJpegQuality.Size = new System.Drawing.Size(233, 45);
			this.trackBarJpegQuality.TabIndex = 0;
			this.trackBarJpegQuality.TickFrequency = 10;
			this.trackBarJpegQuality.Scroll += new System.EventHandler(this.TrackBarJpegQualityScroll);
			// 
			// groupbox_destination
			// 
			this.groupbox_destination.Controls.Add(this.checkbox_picker);
			this.groupbox_destination.Controls.Add(this.destinationsListView);
			this.groupbox_destination.LanguageKey = "settings_destination";
			this.groupbox_destination.Location = new System.Drawing.Point(2, 6);
			this.groupbox_destination.Name = "groupbox_destination";
			this.groupbox_destination.Size = new System.Drawing.Size(412, 311);
			this.groupbox_destination.TabIndex = 16;
			this.groupbox_destination.TabStop = false;
			this.groupbox_destination.Text = "Screenshot Destination";
			// 
			// checkbox_picker
			// 
			this.checkbox_picker.Location = new System.Drawing.Point(6, 14);
			this.checkbox_picker.Name = "checkbox_picker";
			this.checkbox_picker.Size = new System.Drawing.Size(394, 24);
			this.checkbox_picker.TabIndex = 19;
			this.checkbox_picker.Text = "Destination picker";
			this.checkbox_picker.UseVisualStyleBackColor = true;
			this.checkbox_picker.CheckStateChanged += new System.EventHandler(this.DestinationsCheckStateChanged);
			// 
			// destinationsListView
			// 
			this.destinationsListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.destinationsListView.AutoArrange = false;
			this.destinationsListView.CheckBoxes = true;
			this.destinationsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.destination});
			this.destinationsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.destinationsListView.LabelWrap = false;
			this.destinationsListView.Location = new System.Drawing.Point(6, 38);
			this.destinationsListView.Name = "destinationsListView";
			this.destinationsListView.ShowGroups = false;
			this.destinationsListView.Size = new System.Drawing.Size(401, 267);
			this.destinationsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.destinationsListView.TabIndex = 0;
			this.destinationsListView.UseCompatibleStateImageBehavior = false;
			this.destinationsListView.View = System.Windows.Forms.View.Details;
			// 
			// destination
			// 
			this.destination.Text = "Destination";
			this.destination.Width = 380;
			// 
			// tabcontrol
			// 
			this.tabcontrol.Controls.Add(this.tab_general);
			this.tabcontrol.Controls.Add(this.tab_capture);
			this.tabcontrol.Controls.Add(this.tab_output);
			this.tabcontrol.Controls.Add(this.tab_destinations);
			this.tabcontrol.Controls.Add(this.tab_printer);
			this.tabcontrol.Controls.Add(this.tab_plugins);
			this.tabcontrol.Controls.Add(this.tab_expert);
			this.tabcontrol.Location = new System.Drawing.Point(12, 13);
			this.tabcontrol.Name = "tabcontrol";
			this.tabcontrol.SelectedIndex = 0;
			this.tabcontrol.Size = new System.Drawing.Size(431, 346);
			this.tabcontrol.TabIndex = 17;
			// 
			// tab_general
			// 
			this.tab_general.BackColor = System.Drawing.Color.Transparent;
			this.tab_general.Controls.Add(this.groupbox_network);
			this.tab_general.Controls.Add(this.groupbox_hotkeys);
			this.tab_general.Controls.Add(this.groupbox_applicationsettings);
			this.tab_general.LanguageKey = "settings_general";
			this.tab_general.Location = new System.Drawing.Point(4, 22);
			this.tab_general.Name = "tab_general";
			this.tab_general.Padding = new System.Windows.Forms.Padding(3);
			this.tab_general.Size = new System.Drawing.Size(423, 320);
			this.tab_general.TabIndex = 0;
			this.tab_general.Text = "General";
			this.tab_general.UseVisualStyleBackColor = true;
			// 
			// groupbox_network
			// 
			this.groupbox_network.Controls.Add(this.numericUpDown_daysbetweencheck);
			this.groupbox_network.Controls.Add(this.label_checkperiod);
			this.groupbox_network.Controls.Add(this.checkbox_usedefaultproxy);
			this.groupbox_network.LanguageKey = "settings_network";
			this.groupbox_network.Location = new System.Drawing.Point(3, 232);
			this.groupbox_network.Name = "groupbox_network";
			this.groupbox_network.Size = new System.Drawing.Size(412, 72);
			this.groupbox_network.TabIndex = 54;
			this.groupbox_network.TabStop = false;
			this.groupbox_network.Text = "Network";
			// 
			// numericUpDown_daysbetweencheck
			// 
			this.numericUpDown_daysbetweencheck.Location = new System.Drawing.Point(345, 37);
			this.numericUpDown_daysbetweencheck.Name = "numericUpDown_daysbetweencheck";
			this.numericUpDown_daysbetweencheck.Size = new System.Drawing.Size(57, 20);
			this.numericUpDown_daysbetweencheck.TabIndex = 25;
			this.numericUpDown_daysbetweencheck.ThousandsSeparator = true;
			// 
			// label_checkperiod
			// 
			this.label_checkperiod.LanguageKey = "settings_checkperiod";
			this.label_checkperiod.Location = new System.Drawing.Point(5, 39);
			this.label_checkperiod.Name = "label_checkperiod";
			this.label_checkperiod.Size = new System.Drawing.Size(334, 23);
			this.label_checkperiod.TabIndex = 19;
			this.label_checkperiod.Text = "Update check interval";
			// 
			// checkbox_usedefaultproxy
			// 
			this.checkbox_usedefaultproxy.LanguageKey = "settings_usedefaultproxy";
			this.checkbox_usedefaultproxy.Location = new System.Drawing.Point(7, 11);
			this.checkbox_usedefaultproxy.Name = "checkbox_usedefaultproxy";
			this.checkbox_usedefaultproxy.PropertyName = "UseProxy";
			this.checkbox_usedefaultproxy.Size = new System.Drawing.Size(397, 25);
			this.checkbox_usedefaultproxy.TabIndex = 17;
			this.checkbox_usedefaultproxy.Text = "Use default proxy";
			this.checkbox_usedefaultproxy.UseVisualStyleBackColor = true;
			// 
			// groupbox_hotkeys
			// 
			this.groupbox_hotkeys.Controls.Add(this.label_lastregion_hotkey);
			this.groupbox_hotkeys.Controls.Add(this.lastregion_hotkeyControl);
			this.groupbox_hotkeys.Controls.Add(this.label_ie_hotkey);
			this.groupbox_hotkeys.Controls.Add(this.ie_hotkeyControl);
			this.groupbox_hotkeys.Controls.Add(this.label_region_hotkey);
			this.groupbox_hotkeys.Controls.Add(this.label_window_hotkey);
			this.groupbox_hotkeys.Controls.Add(this.label_fullscreen_hotkey);
			this.groupbox_hotkeys.Controls.Add(this.region_hotkeyControl);
			this.groupbox_hotkeys.Controls.Add(this.window_hotkeyControl);
			this.groupbox_hotkeys.Controls.Add(this.fullscreen_hotkeyControl);
			this.groupbox_hotkeys.LanguageKey = "hotkeys";
			this.groupbox_hotkeys.Location = new System.Drawing.Point(2, 76);
			this.groupbox_hotkeys.Name = "groupbox_hotkeys";
			this.groupbox_hotkeys.Size = new System.Drawing.Size(412, 152);
			this.groupbox_hotkeys.TabIndex = 15;
			this.groupbox_hotkeys.TabStop = false;
			this.groupbox_hotkeys.Text = "Hotkeys";
			// 
			// label_lastregion_hotkey
			// 
			this.label_lastregion_hotkey.LanguageKey = "contextmenu_capturelastregion";
			this.label_lastregion_hotkey.Location = new System.Drawing.Point(6, 94);
			this.label_lastregion_hotkey.Name = "label_lastregion_hotkey";
			this.label_lastregion_hotkey.Size = new System.Drawing.Size(212, 20);
			this.label_lastregion_hotkey.TabIndex = 53;
			this.label_lastregion_hotkey.Text = "Capture last region";
			// 
			// lastregion_hotkeyControl
			// 
			this.lastregion_hotkeyControl.Hotkey = System.Windows.Forms.Keys.None;
			this.lastregion_hotkeyControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
			this.lastregion_hotkeyControl.Location = new System.Drawing.Point(224, 94);
			this.lastregion_hotkeyControl.Name = "lastregion_hotkeyControl";
			this.lastregion_hotkeyControl.PropertyName = "LastregionHotkey";
			this.lastregion_hotkeyControl.Size = new System.Drawing.Size(179, 20);
			this.lastregion_hotkeyControl.TabIndex = 52;
			this.lastregion_hotkeyControl.Text = "None";
			// 
			// label_ie_hotkey
			// 
			this.label_ie_hotkey.LanguageKey = "contextmenu_captureie";
			this.label_ie_hotkey.Location = new System.Drawing.Point(6, 120);
			this.label_ie_hotkey.Name = "label_ie_hotkey";
			this.label_ie_hotkey.Size = new System.Drawing.Size(212, 20);
			this.label_ie_hotkey.TabIndex = 51;
			this.label_ie_hotkey.Text = "Capture IE";
			// 
			// ie_hotkeyControl
			// 
			this.ie_hotkeyControl.Hotkey = System.Windows.Forms.Keys.None;
			this.ie_hotkeyControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
			this.ie_hotkeyControl.Location = new System.Drawing.Point(224, 120);
			this.ie_hotkeyControl.Name = "ie_hotkeyControl";
			this.ie_hotkeyControl.PropertyName = "IEHotkey";
			this.ie_hotkeyControl.Size = new System.Drawing.Size(179, 20);
			this.ie_hotkeyControl.TabIndex = 50;
			this.ie_hotkeyControl.Text = "None";
			// 
			// label_region_hotkey
			// 
			this.label_region_hotkey.LanguageKey = "contextmenu_capturearea";
			this.label_region_hotkey.Location = new System.Drawing.Point(6, 68);
			this.label_region_hotkey.Name = "label_region_hotkey";
			this.label_region_hotkey.Size = new System.Drawing.Size(212, 20);
			this.label_region_hotkey.TabIndex = 49;
			this.label_region_hotkey.Text = "Capture region";
			// 
			// label_window_hotkey
			// 
			this.label_window_hotkey.LanguageKey = "contextmenu_capturewindow";
			this.label_window_hotkey.Location = new System.Drawing.Point(6, 42);
			this.label_window_hotkey.Name = "label_window_hotkey";
			this.label_window_hotkey.Size = new System.Drawing.Size(212, 23);
			this.label_window_hotkey.TabIndex = 48;
			this.label_window_hotkey.Text = "Capture window";
			// 
			// label_fullscreen_hotkey
			// 
			this.label_fullscreen_hotkey.LanguageKey = "contextmenu_capturefullscreen";
			this.label_fullscreen_hotkey.Location = new System.Drawing.Point(6, 16);
			this.label_fullscreen_hotkey.Name = "label_fullscreen_hotkey";
			this.label_fullscreen_hotkey.Size = new System.Drawing.Size(212, 23);
			this.label_fullscreen_hotkey.TabIndex = 47;
			this.label_fullscreen_hotkey.Text = "Capture fullscreen";
			// 
			// region_hotkeyControl
			// 
			this.region_hotkeyControl.Hotkey = System.Windows.Forms.Keys.None;
			this.region_hotkeyControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
			this.region_hotkeyControl.Location = new System.Drawing.Point(224, 68);
			this.region_hotkeyControl.Name = "region_hotkeyControl";
			this.region_hotkeyControl.PropertyName = "RegionHotkey";
			this.region_hotkeyControl.Size = new System.Drawing.Size(179, 20);
			this.region_hotkeyControl.TabIndex = 46;
			this.region_hotkeyControl.Text = "None";
			// 
			// window_hotkeyControl
			// 
			this.window_hotkeyControl.Hotkey = System.Windows.Forms.Keys.None;
			this.window_hotkeyControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
			this.window_hotkeyControl.Location = new System.Drawing.Point(224, 42);
			this.window_hotkeyControl.Name = "window_hotkeyControl";
			this.window_hotkeyControl.PropertyName = "WindowHotkey";
			this.window_hotkeyControl.Size = new System.Drawing.Size(179, 20);
			this.window_hotkeyControl.TabIndex = 45;
			this.window_hotkeyControl.Text = "None";
			// 
			// fullscreen_hotkeyControl
			// 
			this.fullscreen_hotkeyControl.Hotkey = System.Windows.Forms.Keys.None;
			this.fullscreen_hotkeyControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
			this.fullscreen_hotkeyControl.Location = new System.Drawing.Point(224, 16);
			this.fullscreen_hotkeyControl.Name = "fullscreen_hotkeyControl";
			this.fullscreen_hotkeyControl.PropertyName = "FullscreenHotkey";
			this.fullscreen_hotkeyControl.Size = new System.Drawing.Size(179, 20);
			this.fullscreen_hotkeyControl.TabIndex = 44;
			this.fullscreen_hotkeyControl.Text = "None";
			// 
			// tab_capture
			// 
			this.tab_capture.Controls.Add(this.groupbox_editor);
			this.tab_capture.Controls.Add(this.groupbox_iecapture);
			this.tab_capture.Controls.Add(this.groupbox_windowscapture);
			this.tab_capture.Controls.Add(this.groupbox_capture);
			this.tab_capture.LanguageKey = "settings_capture";
			this.tab_capture.Location = new System.Drawing.Point(4, 22);
			this.tab_capture.Name = "tab_capture";
			this.tab_capture.Size = new System.Drawing.Size(423, 320);
			this.tab_capture.TabIndex = 3;
			this.tab_capture.Text = "Capture";
			this.tab_capture.UseVisualStyleBackColor = true;
			// 
			// groupbox_editor
			// 
			this.groupbox_editor.Controls.Add(this.checkbox_editor_match_capture_size);
			this.groupbox_editor.LanguageKey = "settings_editor";
			this.groupbox_editor.Location = new System.Drawing.Point(4, 260);
			this.groupbox_editor.Name = "groupbox_editor";
			this.groupbox_editor.Size = new System.Drawing.Size(416, 50);
			this.groupbox_editor.TabIndex = 27;
			this.groupbox_editor.TabStop = false;
			this.groupbox_editor.Text = "Editor";
			// 
			// checkbox_editor_match_capture_size
			// 
			this.checkbox_editor_match_capture_size.LanguageKey = "editor_match_capture_size";
			this.checkbox_editor_match_capture_size.Location = new System.Drawing.Point(6, 19);
			this.checkbox_editor_match_capture_size.Name = "checkbox_editor_match_capture_size";
			this.checkbox_editor_match_capture_size.PropertyName = "MatchSizeToCapture";
			this.checkbox_editor_match_capture_size.SectionName = "Editor";
			this.checkbox_editor_match_capture_size.Size = new System.Drawing.Size(397, 24);
			this.checkbox_editor_match_capture_size.TabIndex = 26;
			this.checkbox_editor_match_capture_size.Text = "Match capture size";
			this.checkbox_editor_match_capture_size.UseVisualStyleBackColor = true;
			// 
			// groupbox_iecapture
			// 
			this.groupbox_iecapture.Controls.Add(this.checkbox_ie_capture);
			this.groupbox_iecapture.LanguageKey = "settings_iecapture";
			this.groupbox_iecapture.Location = new System.Drawing.Point(4, 204);
			this.groupbox_iecapture.Name = "groupbox_iecapture";
			this.groupbox_iecapture.Size = new System.Drawing.Size(416, 50);
			this.groupbox_iecapture.TabIndex = 2;
			this.groupbox_iecapture.TabStop = false;
			this.groupbox_iecapture.Text = "IE Capture settings";
			// 
			// checkbox_ie_capture
			// 
			this.checkbox_ie_capture.LanguageKey = "settings_iecapture";
			this.checkbox_ie_capture.Location = new System.Drawing.Point(6, 19);
			this.checkbox_ie_capture.Name = "checkbox_ie_capture";
			this.checkbox_ie_capture.PropertyName = "IECapture";
			this.checkbox_ie_capture.Size = new System.Drawing.Size(404, 24);
			this.checkbox_ie_capture.TabIndex = 26;
			this.checkbox_ie_capture.Text = "IE capture";
			this.checkbox_ie_capture.UseVisualStyleBackColor = true;
			// 
			// groupbox_windowscapture
			// 
			this.groupbox_windowscapture.Controls.Add(this.colorButton_window_background);
			this.groupbox_windowscapture.Controls.Add(this.label_window_capture_mode);
			this.groupbox_windowscapture.Controls.Add(this.checkbox_capture_windows_interactive);
			this.groupbox_windowscapture.Controls.Add(this.combobox_window_capture_mode);
			this.groupbox_windowscapture.LanguageKey = "settings_windowscapture";
			this.groupbox_windowscapture.Location = new System.Drawing.Point(4, 117);
			this.groupbox_windowscapture.Name = "groupbox_windowscapture";
			this.groupbox_windowscapture.Size = new System.Drawing.Size(416, 80);
			this.groupbox_windowscapture.TabIndex = 1;
			this.groupbox_windowscapture.TabStop = false;
			this.groupbox_windowscapture.Text = "Window capture settings";
			// 
			// colorButton_window_background
			// 
			this.colorButton_window_background.AutoSize = true;
			this.colorButton_window_background.Image = ((System.Drawing.Image)(resources.GetObject("colorButton_window_background.Image")));
			this.colorButton_window_background.Location = new System.Drawing.Point(374, 37);
			this.colorButton_window_background.Name = "colorButton_window_background";
			this.colorButton_window_background.SelectedColor = System.Drawing.Color.White;
			this.colorButton_window_background.Size = new System.Drawing.Size(29, 30);
			this.colorButton_window_background.TabIndex = 45;
			this.colorButton_window_background.UseVisualStyleBackColor = true;
			// 
			// label_window_capture_mode
			// 
			this.label_window_capture_mode.LanguageKey = "settings_window_capture_mode";
			this.label_window_capture_mode.Location = new System.Drawing.Point(6, 46);
			this.label_window_capture_mode.Name = "label_window_capture_mode";
			this.label_window_capture_mode.Size = new System.Drawing.Size(205, 23);
			this.label_window_capture_mode.TabIndex = 26;
			this.label_window_capture_mode.Text = "Window capture mode";
			// 
			// checkbox_capture_windows_interactive
			// 
			this.checkbox_capture_windows_interactive.LanguageKey = "settings_capture_windows_interactive";
			this.checkbox_capture_windows_interactive.Location = new System.Drawing.Point(9, 19);
			this.checkbox_capture_windows_interactive.Name = "checkbox_capture_windows_interactive";
			this.checkbox_capture_windows_interactive.PropertyName = "CaptureWindowsInteractive";
			this.checkbox_capture_windows_interactive.Size = new System.Drawing.Size(394, 18);
			this.checkbox_capture_windows_interactive.TabIndex = 19;
			this.checkbox_capture_windows_interactive.Text = "Interactiv window capture";
			this.checkbox_capture_windows_interactive.UseVisualStyleBackColor = true;
			// 
			// combobox_window_capture_mode
			// 
			this.combobox_window_capture_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox_window_capture_mode.FormattingEnabled = true;
			this.combobox_window_capture_mode.Location = new System.Drawing.Point(217, 43);
			this.combobox_window_capture_mode.MaxDropDownItems = 15;
			this.combobox_window_capture_mode.Name = "combobox_window_capture_mode";
			this.combobox_window_capture_mode.Size = new System.Drawing.Size(151, 21);
			this.combobox_window_capture_mode.TabIndex = 27;
			this.combobox_window_capture_mode.SelectedIndexChanged += new System.EventHandler(this.Combobox_window_capture_modeSelectedIndexChanged);
			// 
			// groupbox_capture
			// 
			this.groupbox_capture.Controls.Add(this.checkbox_playsound);
			this.groupbox_capture.Controls.Add(this.checkbox_capture_mousepointer);
			this.groupbox_capture.Controls.Add(this.numericUpDownWaitTime);
			this.groupbox_capture.Controls.Add(this.label_waittime);
			this.groupbox_capture.LanguageKey = "settings_capture";
			this.groupbox_capture.Location = new System.Drawing.Point(4, 4);
			this.groupbox_capture.Name = "groupbox_capture";
			this.groupbox_capture.Size = new System.Drawing.Size(416, 106);
			this.groupbox_capture.TabIndex = 0;
			this.groupbox_capture.TabStop = false;
			this.groupbox_capture.Text = "General Capture settings";
			// 
			// checkbox_playsound
			// 
			this.checkbox_playsound.LanguageKey = "settings_playsound";
			this.checkbox_playsound.Location = new System.Drawing.Point(11, 39);
			this.checkbox_playsound.Name = "checkbox_playsound";
			this.checkbox_playsound.PropertyName = "PlayCameraSound";
			this.checkbox_playsound.Size = new System.Drawing.Size(399, 24);
			this.checkbox_playsound.TabIndex = 18;
			this.checkbox_playsound.Text = "Play camera sound";
			this.checkbox_playsound.UseVisualStyleBackColor = true;
			// 
			// checkbox_capture_mousepointer
			// 
			this.checkbox_capture_mousepointer.LanguageKey = "settings_capture_mousepointer";
			this.checkbox_capture_mousepointer.Location = new System.Drawing.Point(11, 19);
			this.checkbox_capture_mousepointer.Name = "checkbox_capture_mousepointer";
			this.checkbox_capture_mousepointer.PropertyName = "CaptureMousepointer";
			this.checkbox_capture_mousepointer.Size = new System.Drawing.Size(394, 24);
			this.checkbox_capture_mousepointer.TabIndex = 17;
			this.checkbox_capture_mousepointer.Text = "Capture mousepointer";
			this.checkbox_capture_mousepointer.UseVisualStyleBackColor = true;
			// 
			// numericUpDownWaitTime
			// 
			this.numericUpDownWaitTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDownWaitTime.Location = new System.Drawing.Point(11, 69);
			this.numericUpDownWaitTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownWaitTime.Name = "numericUpDownWaitTime";
			this.numericUpDownWaitTime.Size = new System.Drawing.Size(57, 20);
			this.numericUpDownWaitTime.TabIndex = 24;
			this.numericUpDownWaitTime.ThousandsSeparator = true;
			// 
			// label_waittime
			// 
			this.label_waittime.LanguageKey = "settings_waittime";
			this.label_waittime.Location = new System.Drawing.Point(74, 71);
			this.label_waittime.Name = "label_waittime";
			this.label_waittime.Size = new System.Drawing.Size(331, 16);
			this.label_waittime.TabIndex = 25;
			this.label_waittime.Text = "Wait before capture (ms)";
			// 
			// tab_output
			// 
			this.tab_output.BackColor = System.Drawing.Color.Transparent;
			this.tab_output.Controls.Add(this.groupbox_preferredfilesettings);
			this.tab_output.Controls.Add(this.groupbox_jpegsettings);
			this.tab_output.LanguageKey = "settings_output";
			this.tab_output.Location = new System.Drawing.Point(4, 22);
			this.tab_output.Name = "tab_output";
			this.tab_output.Padding = new System.Windows.Forms.Padding(3);
			this.tab_output.Size = new System.Drawing.Size(423, 320);
			this.tab_output.TabIndex = 1;
			this.tab_output.Text = "Output";
			this.tab_output.UseVisualStyleBackColor = true;
			// 
			// tab_destinations
			// 
			this.tab_destinations.Controls.Add(this.groupbox_destination);
			this.tab_destinations.LanguageKey = "settings_destination";
			this.tab_destinations.Location = new System.Drawing.Point(4, 22);
			this.tab_destinations.Name = "tab_destinations";
			this.tab_destinations.Size = new System.Drawing.Size(423, 320);
			this.tab_destinations.TabIndex = 4;
			this.tab_destinations.Text = "Destinations";
			this.tab_destinations.UseVisualStyleBackColor = true;
			// 
			// tab_printer
			// 
			this.tab_printer.Controls.Add(this.groupbox_printoptions);
			this.tab_printer.LanguageKey = "settings_printer";
			this.tab_printer.Location = new System.Drawing.Point(4, 22);
			this.tab_printer.Name = "tab_printer";
			this.tab_printer.Padding = new System.Windows.Forms.Padding(3);
			this.tab_printer.Size = new System.Drawing.Size(423, 320);
			this.tab_printer.TabIndex = 2;
			this.tab_printer.Text = "Printer";
			this.tab_printer.UseVisualStyleBackColor = true;
			// 
			// groupbox_printoptions
			// 
			this.groupbox_printoptions.Controls.Add(this.checkboxPrintInverted);
			this.groupbox_printoptions.Controls.Add(this.checkbox_alwaysshowprintoptionsdialog);
			this.groupbox_printoptions.Controls.Add(this.checkboxTimestamp);
			this.groupbox_printoptions.Controls.Add(this.checkboxAllowCenter);
			this.groupbox_printoptions.Controls.Add(this.checkboxAllowRotate);
			this.groupbox_printoptions.Controls.Add(this.checkboxAllowEnlarge);
			this.groupbox_printoptions.Controls.Add(this.checkboxAllowShrink);
			this.groupbox_printoptions.LanguageKey = "settings_printoptions";
			this.groupbox_printoptions.Location = new System.Drawing.Point(2, 6);
			this.groupbox_printoptions.Name = "groupbox_printoptions";
			this.groupbox_printoptions.Size = new System.Drawing.Size(412, 227);
			this.groupbox_printoptions.TabIndex = 18;
			this.groupbox_printoptions.TabStop = false;
			this.groupbox_printoptions.Text = "Print options";
			// 
			// checkboxPrintInverted
			// 
			this.checkboxPrintInverted.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxPrintInverted.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxPrintInverted.LanguageKey = "printoptions_inverted";
			this.checkboxPrintInverted.Location = new System.Drawing.Point(12, 144);
			this.checkboxPrintInverted.Name = "checkboxPrintInverted";
			this.checkboxPrintInverted.PropertyName = "OutputPrintInverted";
			this.checkboxPrintInverted.Size = new System.Drawing.Size(394, 16);
			this.checkboxPrintInverted.TabIndex = 31;
			this.checkboxPrintInverted.Text = "Print inverted";
			this.checkboxPrintInverted.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxPrintInverted.UseVisualStyleBackColor = true;
			// 
			// checkbox_alwaysshowprintoptionsdialog
			// 
			this.checkbox_alwaysshowprintoptionsdialog.LanguageKey = "settings_alwaysshowprintoptionsdialog";
			this.checkbox_alwaysshowprintoptionsdialog.Location = new System.Drawing.Point(12, 167);
			this.checkbox_alwaysshowprintoptionsdialog.Name = "checkbox_alwaysshowprintoptionsdialog";
			this.checkbox_alwaysshowprintoptionsdialog.PropertyName = "OutputPrintPromptOptions";
			this.checkbox_alwaysshowprintoptionsdialog.Size = new System.Drawing.Size(394, 19);
			this.checkbox_alwaysshowprintoptionsdialog.TabIndex = 17;
			this.checkbox_alwaysshowprintoptionsdialog.Text = "Show print options dialog every time an image is printed";
			this.checkbox_alwaysshowprintoptionsdialog.UseVisualStyleBackColor = true;
			// 
			// checkboxTimestamp
			// 
			this.checkboxTimestamp.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxTimestamp.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxTimestamp.LanguageKey = "printoptions_timestamp";
			this.checkboxTimestamp.Location = new System.Drawing.Point(12, 121);
			this.checkboxTimestamp.Name = "checkboxTimestamp";
			this.checkboxTimestamp.PropertyName = "OutputPrintFooter";
			this.checkboxTimestamp.Size = new System.Drawing.Size(394, 16);
			this.checkboxTimestamp.TabIndex = 30;
			this.checkboxTimestamp.Text = "Print date / time at bottom of page";
			this.checkboxTimestamp.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxTimestamp.UseVisualStyleBackColor = true;
			// 
			// checkboxAllowCenter
			// 
			this.checkboxAllowCenter.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowCenter.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowCenter.LanguageKey = "printoptions_allowcenter";
			this.checkboxAllowCenter.Location = new System.Drawing.Point(12, 96);
			this.checkboxAllowCenter.Name = "checkboxAllowCenter";
			this.checkboxAllowCenter.PropertyName = "OutputPrintCenter";
			this.checkboxAllowCenter.Size = new System.Drawing.Size(394, 18);
			this.checkboxAllowCenter.TabIndex = 29;
			this.checkboxAllowCenter.Text = "Center printout on page";
			this.checkboxAllowCenter.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowCenter.UseVisualStyleBackColor = true;
			// 
			// checkboxAllowRotate
			// 
			this.checkboxAllowRotate.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowRotate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowRotate.LanguageKey = "printoptions_allowrotate";
			this.checkboxAllowRotate.Location = new System.Drawing.Point(12, 72);
			this.checkboxAllowRotate.Name = "checkboxAllowRotate";
			this.checkboxAllowRotate.PropertyName = "OutputPrintAllowRotate";
			this.checkboxAllowRotate.Size = new System.Drawing.Size(394, 17);
			this.checkboxAllowRotate.TabIndex = 28;
			this.checkboxAllowRotate.Text = "Rotate printouts to page orientation.";
			this.checkboxAllowRotate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowRotate.UseVisualStyleBackColor = true;
			// 
			// checkboxAllowEnlarge
			// 
			this.checkboxAllowEnlarge.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowEnlarge.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowEnlarge.LanguageKey = "printoptions_allowenlarge";
			this.checkboxAllowEnlarge.Location = new System.Drawing.Point(12, 47);
			this.checkboxAllowEnlarge.Name = "checkboxAllowEnlarge";
			this.checkboxAllowEnlarge.PropertyName = "OutputPrintAllowEnlarge";
			this.checkboxAllowEnlarge.Size = new System.Drawing.Size(394, 19);
			this.checkboxAllowEnlarge.TabIndex = 27;
			this.checkboxAllowEnlarge.Text = "Enlarge small printouts to paper size.";
			this.checkboxAllowEnlarge.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowEnlarge.UseVisualStyleBackColor = true;
			// 
			// checkboxAllowShrink
			// 
			this.checkboxAllowShrink.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowShrink.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowShrink.LanguageKey = "printoptions_allowshrink";
			this.checkboxAllowShrink.Location = new System.Drawing.Point(12, 22);
			this.checkboxAllowShrink.Name = "checkboxAllowShrink";
			this.checkboxAllowShrink.PropertyName = "OutputPrintAllowShrink";
			this.checkboxAllowShrink.Size = new System.Drawing.Size(394, 17);
			this.checkboxAllowShrink.TabIndex = 26;
			this.checkboxAllowShrink.Text = "Shrink large printouts to paper size.";
			this.checkboxAllowShrink.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkboxAllowShrink.UseVisualStyleBackColor = true;
			// 
			// tab_plugins
			// 
			this.tab_plugins.Controls.Add(this.groupbox_plugins);
			this.tab_plugins.LanguageKey = "settings_plugins";
			this.tab_plugins.Location = new System.Drawing.Point(4, 22);
			this.tab_plugins.Name = "tab_plugins";
			this.tab_plugins.Size = new System.Drawing.Size(423, 320);
			this.tab_plugins.TabIndex = 2;
			this.tab_plugins.Text = "Plugins";
			this.tab_plugins.UseVisualStyleBackColor = true;
			// 
			// groupbox_plugins
			// 
			this.groupbox_plugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupbox_plugins.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.groupbox_plugins.Controls.Add(this.listview_plugins);
			this.groupbox_plugins.Controls.Add(this.button_pluginconfigure);
			this.groupbox_plugins.Location = new System.Drawing.Point(0, 0);
			this.groupbox_plugins.Name = "groupbox_plugins";
			this.groupbox_plugins.Size = new System.Drawing.Size(423, 314);
			this.groupbox_plugins.TabIndex = 0;
			this.groupbox_plugins.TabStop = false;
			this.groupbox_plugins.Text = "Plugin settings";
			// 
			// listview_plugins
			// 
			this.listview_plugins.Dock = System.Windows.Forms.DockStyle.Top;
			this.listview_plugins.FullRowSelect = true;
			this.listview_plugins.Location = new System.Drawing.Point(3, 16);
			this.listview_plugins.Name = "listview_plugins";
			this.listview_plugins.Size = new System.Drawing.Size(417, 263);
			this.listview_plugins.TabIndex = 2;
			this.listview_plugins.UseCompatibleStateImageBehavior = false;
			this.listview_plugins.View = System.Windows.Forms.View.Details;
			this.listview_plugins.SelectedIndexChanged += new System.EventHandler(this.Listview_pluginsSelectedIndexChanged);
			this.listview_plugins.Click += new System.EventHandler(this.Listview_pluginsSelectedIndexChanged);
			// 
			// button_pluginconfigure
			// 
			this.button_pluginconfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_pluginconfigure.AutoSize = true;
			this.button_pluginconfigure.Enabled = false;
			this.button_pluginconfigure.Location = new System.Drawing.Point(6, 285);
			this.button_pluginconfigure.Name = "button_pluginconfigure";
			this.button_pluginconfigure.Size = new System.Drawing.Size(75, 23);
			this.button_pluginconfigure.TabIndex = 1;
			this.button_pluginconfigure.Text = "Configure";
			this.button_pluginconfigure.UseVisualStyleBackColor = true;
			this.button_pluginconfigure.Click += new System.EventHandler(this.Button_pluginconfigureClick);
			// 
			// tab_expert
			// 
			this.tab_expert.Controls.Add(this.groupbox_expert);
			this.tab_expert.Location = new System.Drawing.Point(4, 22);
			this.tab_expert.Name = "tab_expert";
			this.tab_expert.Size = new System.Drawing.Size(423, 320);
			this.tab_expert.TabIndex = 5;
			this.tab_expert.Text = "Expert";
			this.tab_expert.UseVisualStyleBackColor = true;
			// 
			// groupbox_expert
			// 
			this.groupbox_expert.Controls.Add(this.checkbox_autoreducecolors);
			this.groupbox_expert.Controls.Add(this.label_clipboardformats);
			this.groupbox_expert.Controls.Add(this.checkbox_enableexpert);
			this.groupbox_expert.Controls.Add(this.listView1);
			this.groupbox_expert.Location = new System.Drawing.Point(5, 5);
			this.groupbox_expert.Name = "groupbox_expert";
			this.groupbox_expert.Size = new System.Drawing.Size(412, 311);
			this.groupbox_expert.TabIndex = 17;
			this.groupbox_expert.TabStop = false;
			this.groupbox_expert.Text = "Expert settings";
			// 
			// checkbox_autoreducecolors
			// 
			this.checkbox_autoreducecolors.LanguageKey = "application_title";
			this.checkbox_autoreducecolors.Location = new System.Drawing.Point(9, 143);
			this.checkbox_autoreducecolors.Name = "checkbox_autoreducecolors";
			this.checkbox_autoreducecolors.PropertyName = "OutputFileAutoReduceColors";
			this.checkbox_autoreducecolors.Size = new System.Drawing.Size(394, 24);
			this.checkbox_autoreducecolors.TabIndex = 21;
			this.checkbox_autoreducecolors.Text = "Auto generate 8-Bit output files if less than 256 colors are used";
			this.checkbox_autoreducecolors.UseVisualStyleBackColor = true;
			// 
			// label_clipboardformats
			// 
			this.label_clipboardformats.AutoSize = true;
			this.label_clipboardformats.Location = new System.Drawing.Point(7, 45);
			this.label_clipboardformats.Name = "label_clipboardformats";
			this.label_clipboardformats.Size = new System.Drawing.Size(88, 13);
			this.label_clipboardformats.TabIndex = 20;
			this.label_clipboardformats.Text = "Clipboard formats";
			// 
			// checkbox_enableexpert
			// 
			this.checkbox_enableexpert.Location = new System.Drawing.Point(6, 14);
			this.checkbox_enableexpert.Name = "checkbox_enableexpert";
			this.checkbox_enableexpert.Size = new System.Drawing.Size(394, 24);
			this.checkbox_enableexpert.TabIndex = 19;
			this.checkbox_enableexpert.Text = "I know what I am doing!";
			this.checkbox_enableexpert.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.listView1.AutoArrange = false;
			this.listView1.CheckBoxes = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.LabelWrap = false;
			this.listView1.Location = new System.Drawing.Point(109, 44);
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.Size = new System.Drawing.Size(291, 82);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Destination";
			this.columnHeader1.Width = 290;
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(451, 396);
			this.Controls.Add(this.tabcontrol);
			this.Controls.Add(this.settings_okay);
			this.Controls.Add(this.settings_cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.LanguageKey = "settings_title";
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.groupbox_preferredfilesettings.ResumeLayout(false);
			this.groupbox_preferredfilesettings.PerformLayout();
			this.groupbox_applicationsettings.ResumeLayout(false);
			this.groupbox_jpegsettings.ResumeLayout(false);
			this.groupbox_jpegsettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarJpegQuality)).EndInit();
			this.groupbox_destination.ResumeLayout(false);
			this.tabcontrol.ResumeLayout(false);
			this.tab_general.ResumeLayout(false);
			this.groupbox_network.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_daysbetweencheck)).EndInit();
			this.groupbox_hotkeys.ResumeLayout(false);
			this.groupbox_hotkeys.PerformLayout();
			this.tab_capture.ResumeLayout(false);
			this.groupbox_editor.ResumeLayout(false);
			this.groupbox_iecapture.ResumeLayout(false);
			this.groupbox_windowscapture.ResumeLayout(false);
			this.groupbox_windowscapture.PerformLayout();
			this.groupbox_capture.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).EndInit();
			this.tab_output.ResumeLayout(false);
			this.tab_destinations.ResumeLayout(false);
			this.tab_printer.ResumeLayout(false);
			this.groupbox_printoptions.ResumeLayout(false);
			this.tab_plugins.ResumeLayout(false);
			this.groupbox_plugins.ResumeLayout(false);
			this.groupbox_plugins.PerformLayout();
			this.tab_expert.ResumeLayout(false);
			this.groupbox_expert.ResumeLayout(false);
			this.groupbox_expert.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ColumnHeader destination;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_picker;
		private System.Windows.Forms.ListView destinationsListView;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_editor;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_editor_match_capture_size;
		private System.Windows.Forms.NumericUpDown numericUpDown_daysbetweencheck;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_network;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_usedefaultproxy;
		private GreenshotPlugin.Controls.GreenshotLabel label_checkperiod;
		private GreenshotPlugin.Controls.HotkeyControl fullscreen_hotkeyControl;
		private GreenshotPlugin.Controls.HotkeyControl window_hotkeyControl;
		private GreenshotPlugin.Controls.HotkeyControl region_hotkeyControl;
		private GreenshotPlugin.Controls.GreenshotLabel label_fullscreen_hotkey;
		private GreenshotPlugin.Controls.GreenshotLabel label_window_hotkey;
		private GreenshotPlugin.Controls.GreenshotLabel label_region_hotkey;
		private GreenshotPlugin.Controls.HotkeyControl ie_hotkeyControl;
		private GreenshotPlugin.Controls.GreenshotLabel label_ie_hotkey;
		private GreenshotPlugin.Controls.HotkeyControl lastregion_hotkeyControl;
		private GreenshotPlugin.Controls.GreenshotLabel label_lastregion_hotkey;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_hotkeys;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkboxPrintInverted;
		private Greenshot.Controls.ColorButton colorButton_window_background;
		private GreenshotPlugin.Controls.GreenshotLabel label_window_capture_mode;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_ie_capture;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_capture;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_windowscapture;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_iecapture;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_capture;
		private System.Windows.Forms.ComboBox combobox_window_capture_mode;
		private System.Windows.Forms.NumericUpDown numericUpDownWaitTime;
		private GreenshotPlugin.Controls.GreenshotLabel label_waittime;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_capture_windows_interactive;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_capture_mousepointer;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_printer;
		private System.Windows.Forms.ListView listview_plugins;
		private System.Windows.Forms.Button button_pluginconfigure;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_plugins;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_plugins;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkboxTimestamp;
		private System.Windows.Forms.Button btnPatternHelp;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_copypathtoclipboard;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowShrink;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowEnlarge;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowRotate;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowCenter;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_alwaysshowprintoptionsdialog;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_printoptions;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_output;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_general;
		private System.Windows.Forms.TabControl tabcontrol;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_autostartshortcut;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_destination;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_alwaysshowjpegqualitydialog;
		private System.Windows.Forms.TextBox textBoxJpegQuality;
		private GreenshotPlugin.Controls.GreenshotLabel label_jpegquality;
		private System.Windows.Forms.TrackBar trackBarJpegQuality;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_jpegsettings;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_applicationsettings;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_preferredfilesettings;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_playsound;
		private GreenshotPlugin.Controls.GreenshotLabel label_primaryimageformat;
		private GreenshotPlugin.Controls.GreenshotComboBox combobox_primaryimageformat;
		private System.Windows.Forms.ComboBox combobox_language;
		private GreenshotPlugin.Controls.GreenshotLabel label_language;
		private GreenshotPlugin.Controls.GreenshotTextBox textbox_screenshotname;
		private GreenshotPlugin.Controls.GreenshotLabel label_screenshotname;
		private System.Windows.Forms.Button browse;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private GreenshotPlugin.Controls.GreenshotButton settings_cancel;
		private GreenshotPlugin.Controls.GreenshotButton settings_okay;
		private System.Windows.Forms.TextBox textbox_storagelocation;
		private GreenshotPlugin.Controls.GreenshotLabel label_storagelocation;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_destinations;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_expert;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_expert;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_autoreducecolors;
		private GreenshotPlugin.Controls.GreenshotLabel label_clipboardformats;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_enableexpert;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}
