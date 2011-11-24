/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
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
	partial class ImageEditorForm {
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
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditorForm));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new GreenshotPlugin.Controls.NonJumpingPanel();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.btnCursor = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnRect = new System.Windows.Forms.ToolStripButton();
			this.btnEllipse = new System.Windows.Forms.ToolStripButton();
			this.btnLine = new System.Windows.Forms.ToolStripButton();
			this.btnArrow = new System.Windows.Forms.ToolStripButton();
			this.btnText = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.btnHighlight = new System.Windows.Forms.ToolStripButton();
			this.btnObfuscate = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.btnCrop = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.objectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addEllipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.drawLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.drawArrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.arrangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.upToTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.upOneLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downOneLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downToBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.btnClipboard = new System.Windows.Forms.ToolStripButton();
			this.btnPrint = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnCut = new System.Windows.Forms.ToolStripButton();
			this.btnCopy = new System.Windows.Forms.ToolStripButton();
			this.btnPaste = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.btnSettings = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.btnHelp = new System.Windows.Forms.ToolStripButton();
			this.propertiesToolStrip = new System.Windows.Forms.ToolStrip();
			this.obfuscateModeButton = new Greenshot.Controls.BindableToolStripDropDownButton();
			this.pixelizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.blurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.highlightModeButton = new Greenshot.Controls.BindableToolStripDropDownButton();
			this.textHighlightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.areaHighlightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.grayscaleHighlightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.magnifyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnFillColor = new Greenshot.Controls.ToolStripColorButton();
			this.btnLineColor = new Greenshot.Controls.ToolStripColorButton();
			this.lineThicknessLabel = new System.Windows.Forms.ToolStripLabel();
			this.lineThicknessUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.fontFamilyComboBox = new Greenshot.Controls.FontFamilyComboBox();
			this.fontSizeLabel = new System.Windows.Forms.ToolStripLabel();
			this.fontSizeUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.fontBoldButton = new Greenshot.Controls.BindableToolStripButton();
			this.fontItalicButton = new Greenshot.Controls.BindableToolStripButton();
			this.blurRadiusLabel = new System.Windows.Forms.ToolStripLabel();
			this.blurRadiusUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.brightnessLabel = new System.Windows.Forms.ToolStripLabel();
			this.brightnessUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.previewQualityLabel = new System.Windows.Forms.ToolStripLabel();
			this.previewQualityUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.magnificationFactorLabel = new System.Windows.Forms.ToolStripLabel();
			this.magnificationFactorUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.pixelSizeLabel = new System.Windows.Forms.ToolStripLabel();
			this.pixelSizeUpDown = new Greenshot.Controls.ToolStripNumericUpDown();
			this.arrowHeadsLabel = new System.Windows.Forms.ToolStripLabel();
			this.arrowHeadsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.arrowHeadStartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arrowHeadEndMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arrowHeadBothMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arrowHeadNoneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shadowButton = new Greenshot.Controls.BindableToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.btnConfirm = new Greenshot.Controls.BindableToolStripButton();
			this.btnCancel = new Greenshot.Controls.BindableToolStripButton();
			this.btnLineColor_ = new Greenshot.Controls.ToolStripColorButton();
			this.btnFillColor_ = new Greenshot.Controls.ToolStripColorButton();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.fileSavedStatusContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyPathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnEmail = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.insert_window_toolstripmenuitem = new System.Windows.Forms.ToolStripMenuItem();
			this.dimensionsLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.autoCropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.propertiesToolStrip.SuspendLayout();
			this.fileSavedStatusContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(761, 387);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer1.LeftToolStripPanel
			// 
			this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStrip2);
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(785, 485);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.propertiesToolStrip);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.dimensionsLabel, this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(785, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.statusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 17);
			this.statusLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StatusLabelClicked);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 412F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(761, 387);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(755, 406);
			this.panel1.TabIndex = 2;
			// 
			// toolStrip2
			// 
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btnCursor,
									this.toolStripSeparator1,
									this.btnRect,
									this.btnEllipse,
									this.btnLine,
									this.btnArrow,
									this.btnText,
									this.toolStripSeparator14,
									this.btnHighlight,
									this.btnObfuscate,
									this.toolStripSeparator13,
									this.btnCrop});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(24, 387);
			this.toolStrip2.Stretch = true;
			this.toolStrip2.TabIndex = 0;
			// 
			// btnCursor
			// 
			this.btnCursor.Checked = true;
			this.btnCursor.CheckOnClick = true;
			this.btnCursor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCursor.Image = ((System.Drawing.Image)(resources.GetObject("btnCursor.Image")));
			this.btnCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCursor.Name = "btnCursor";
			this.btnCursor.Size = new System.Drawing.Size(22, 20);
			this.btnCursor.Text = "Selection tool";
			this.btnCursor.Click += new System.EventHandler(this.BtnCursorClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(22, 6);
			// 
			// btnRect
			// 
			this.btnRect.CheckOnClick = true;
			this.btnRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRect.Image = ((System.Drawing.Image)(resources.GetObject("btnRect.Image")));
			this.btnRect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRect.Name = "btnRect";
			this.btnRect.Size = new System.Drawing.Size(22, 20);
			this.btnRect.Text = "Draw rectangle";
			this.btnRect.Click += new System.EventHandler(this.BtnRectClick);
			// 
			// btnEllipse
			// 
			this.btnEllipse.CheckOnClick = true;
			this.btnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipse.Image")));
			this.btnEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEllipse.Name = "btnEllipse";
			this.btnEllipse.Size = new System.Drawing.Size(22, 20);
			this.btnEllipse.Text = "Draw ellipse";
			this.btnEllipse.Click += new System.EventHandler(this.BtnEllipseClick);
			// 
			// btnLine
			// 
			this.btnLine.CheckOnClick = true;
			this.btnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
			this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnLine.Name = "btnLine";
			this.btnLine.Size = new System.Drawing.Size(22, 20);
			this.btnLine.Text = "Draw line";
			this.btnLine.Click += new System.EventHandler(this.BtnLineClick);
			// 
			// btnArrow
			// 
			this.btnArrow.CheckOnClick = true;
			this.btnArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnArrow.Image = ((System.Drawing.Image)(resources.GetObject("btnArrow.Image")));
			this.btnArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnArrow.Name = "btnArrow";
			this.btnArrow.Size = new System.Drawing.Size(22, 20);
			this.btnArrow.Text = "Draw arrow";
			this.btnArrow.Click += new System.EventHandler(this.BtnArrowClick);
			// 
			// btnText
			// 
			this.btnText.CheckOnClick = true;
			this.btnText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnText.Image = ((System.Drawing.Image)(resources.GetObject("btnText.Image")));
			this.btnText.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnText.Name = "btnText";
			this.btnText.Size = new System.Drawing.Size(22, 20);
			this.btnText.Text = "Add text";
			this.btnText.Click += new System.EventHandler(this.BtnTextClick);
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(22, 6);
			// 
			// btnHighlight
			// 
			this.btnHighlight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnHighlight.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlight.Image")));
			this.btnHighlight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHighlight.Name = "btnHighlight";
			this.btnHighlight.Size = new System.Drawing.Size(22, 20);
			this.btnHighlight.Text = "Highlight";
			this.btnHighlight.Click += new System.EventHandler(this.BtnHighlightClick);
			// 
			// btnObfuscate
			// 
			this.btnObfuscate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnObfuscate.Image = ((System.Drawing.Image)(resources.GetObject("btnObfuscate.Image")));
			this.btnObfuscate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnObfuscate.Name = "btnObfuscate";
			this.btnObfuscate.Size = new System.Drawing.Size(22, 20);
			this.btnObfuscate.Text = "Obfuscate";
			this.btnObfuscate.Click += new System.EventHandler(this.BtnObfuscateClick);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(22, 6);
			// 
			// btnCrop
			// 
			this.btnCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCrop.Image = ((System.Drawing.Image)(resources.GetObject("btnCrop.Image")));
			this.btnCrop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCrop.Name = "btnCrop";
			this.btnCrop.Size = new System.Drawing.Size(22, 20);
			this.btnCrop.Text = "Crop";
			this.btnCrop.Click += new System.EventHandler(this.BtnCropClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileStripMenuItem,
									this.editToolStripMenuItem,
									this.objectToolStripMenuItem,
									this.pluginToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(785, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileStripMenuItem
			// 
			this.fileStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveToolStripMenuItem,
									this.saveAsToolStripMenuItem,
									this.copyImageToClipboardToolStripMenuItem,
									this.printToolStripMenuItem,
									this.emailToolStripMenuItem,
									this.toolStripSeparator9,
									this.closeToolStripMenuItem});
			this.fileStripMenuItem.Name = "fileStripMenuItem";
			this.fileStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileStripMenuItem.Text = "File";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
			this.saveAsToolStripMenuItem.Text = "Save as...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
			// 
			// copyImageToClipboardToolStripMenuItem
			// 
			this.copyImageToClipboardToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyImageToClipboardToolStripMenuItem.Image")));
			this.copyImageToClipboardToolStripMenuItem.Name = "copyImageToClipboardToolStripMenuItem";
			this.copyImageToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.C)));
			this.copyImageToClipboardToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
			this.copyImageToClipboardToolStripMenuItem.Text = "Copy image to clipboard";
			this.copyImageToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyImageToClipboardToolStripMenuItemClick);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
			this.printToolStripMenuItem.Text = "Print...";
			this.printToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripMenuItemClick);
			// 
			// emailToolStripMenuItem
			// 
			this.emailToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("emailToolStripMenuItem.Image")));
			this.emailToolStripMenuItem.Name = "emailToolStripMenuItem";
			this.emailToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.emailToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
			this.emailToolStripMenuItem.Text = "EMail";
			this.emailToolStripMenuItem.Click += new System.EventHandler(this.EmailToolStripMenuItemClick);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(304, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripMenuItem.Image")));
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
			// 
			// autoCropToolStripMenuItem
			// 
			this.autoCropToolStripMenuItem.Name = "autoCropToolStripMenuItem";
			this.autoCropToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.autoCropToolStripMenuItem.Text = "Auto Crop";
			this.autoCropToolStripMenuItem.Click += new System.EventHandler(this.AutoCropToolStripMenuItemClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.cutToolStripMenuItem,
									this.copyToolStripMenuItem,
									this.pasteToolStripMenuItem,
									this.toolStripSeparator4,
									this.duplicateToolStripMenuItem,
									this.toolStripSeparator12,
									this.preferencesToolStripMenuItem,
									this.toolStripSeparator5,
									this.autoCropToolStripMenuItem,
									this.insert_window_toolstripmenuitem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "Edit";
			this.editToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItemClick);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Enabled = false;
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItemClick);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Enabled = false;
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Enabled = false;
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItemClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(157, 6);
			// 
			// duplicateToolStripMenuItem
			// 
			this.duplicateToolStripMenuItem.Enabled = false;
			this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			this.duplicateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.duplicateToolStripMenuItem.Text = "Duplicate";
			this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.DuplicateToolStripMenuItemClick);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(157, 6);
			// 
			// preferencesToolStripMenuItem
			// 
			this.preferencesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("preferencesToolStripMenuItem.Image")));
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.preferencesToolStripMenuItem.Text = "Preferences...";
			this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.PreferencesToolStripMenuItemClick);
			// 
			// objectToolStripMenuItem
			// 
			this.objectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.addRectangleToolStripMenuItem,
									this.addEllipseToolStripMenuItem,
									this.drawLineToolStripMenuItem,
									this.drawArrowToolStripMenuItem,
									this.addTextBoxToolStripMenuItem,
									this.toolStripSeparator8,
									this.selectAllToolStripMenuItem,
									this.removeObjectToolStripMenuItem,
									this.toolStripSeparator7,
									this.arrangeToolStripMenuItem,
									this.saveElementsToolStripMenuItem,
									this.loadElementsToolStripMenuItem});
			this.objectToolStripMenuItem.Name = "objectToolStripMenuItem";
			this.objectToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.objectToolStripMenuItem.Text = "Object";
			// 
			// addRectangleToolStripMenuItem
			// 
			this.addRectangleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addRectangleToolStripMenuItem.Image")));
			this.addRectangleToolStripMenuItem.Name = "addRectangleToolStripMenuItem";
			this.addRectangleToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.addRectangleToolStripMenuItem.Text = "Draw rectangle";
			this.addRectangleToolStripMenuItem.Click += new System.EventHandler(this.AddRectangleToolStripMenuItemClick);
			// 
			// addEllipseToolStripMenuItem
			// 
			this.addEllipseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addEllipseToolStripMenuItem.Image")));
			this.addEllipseToolStripMenuItem.Name = "addEllipseToolStripMenuItem";
			this.addEllipseToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.addEllipseToolStripMenuItem.Text = "Draw ellipse";
			this.addEllipseToolStripMenuItem.Click += new System.EventHandler(this.AddEllipseToolStripMenuItemClick);
			// 
			// drawLineToolStripMenuItem
			// 
			this.drawLineToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("drawLineToolStripMenuItem.Image")));
			this.drawLineToolStripMenuItem.Name = "drawLineToolStripMenuItem";
			this.drawLineToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.drawLineToolStripMenuItem.Text = "Draw line";
			this.drawLineToolStripMenuItem.Click += new System.EventHandler(this.DrawLineToolStripMenuItemClick);
			// 
			// drawArrowToolStripMenuItem
			// 
			this.drawArrowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("drawArrowToolStripMenuItem.Image")));
			this.drawArrowToolStripMenuItem.Name = "drawArrowToolStripMenuItem";
			this.drawArrowToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.drawArrowToolStripMenuItem.Text = "Draw arrow";
			this.drawArrowToolStripMenuItem.Click += new System.EventHandler(this.DrawArrowToolStripMenuItemClick);
			// 
			// addTextBoxToolStripMenuItem
			// 
			this.addTextBoxToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addTextBoxToolStripMenuItem.Image")));
			this.addTextBoxToolStripMenuItem.Name = "addTextBoxToolStripMenuItem";
			this.addTextBoxToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.addTextBoxToolStripMenuItem.Text = "Add text box";
			this.addTextBoxToolStripMenuItem.Click += new System.EventHandler(this.AddTextBoxToolStripMenuItemClick);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(174, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.selectAllToolStripMenuItem.Text = "Select all";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItemClick);
			// 
			// removeObjectToolStripMenuItem
			// 
			this.removeObjectToolStripMenuItem.Enabled = false;
			this.removeObjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeObjectToolStripMenuItem.Image")));
			this.removeObjectToolStripMenuItem.Name = "removeObjectToolStripMenuItem";
			this.removeObjectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.removeObjectToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.removeObjectToolStripMenuItem.Text = "Remove Object";
			this.removeObjectToolStripMenuItem.Click += new System.EventHandler(this.RemoveObjectToolStripMenuItemClick);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(174, 6);
			// 
			// arrangeToolStripMenuItem
			// 
			this.arrangeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.upToTopToolStripMenuItem,
									this.upOneLevelToolStripMenuItem,
									this.downOneLevelToolStripMenuItem,
									this.downToBottomToolStripMenuItem});
			this.arrangeToolStripMenuItem.Enabled = false;
			this.arrangeToolStripMenuItem.Name = "arrangeToolStripMenuItem";
			this.arrangeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.arrangeToolStripMenuItem.Text = "Arrange";
			// 
			// upToTopToolStripMenuItem
			// 
			this.upToTopToolStripMenuItem.Enabled = false;
			this.upToTopToolStripMenuItem.Name = "upToTopToolStripMenuItem";
			this.upToTopToolStripMenuItem.ShortcutKeyDisplayString = "Home";
			this.upToTopToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.upToTopToolStripMenuItem.Text = "Up to top";
			this.upToTopToolStripMenuItem.Click += new System.EventHandler(this.UpToTopToolStripMenuItemClick);
			// 
			// upOneLevelToolStripMenuItem
			// 
			this.upOneLevelToolStripMenuItem.Enabled = false;
			this.upOneLevelToolStripMenuItem.Name = "upOneLevelToolStripMenuItem";
			this.upOneLevelToolStripMenuItem.ShortcutKeyDisplayString = "PgUp";
			this.upOneLevelToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.upOneLevelToolStripMenuItem.Text = "Up one level";
			this.upOneLevelToolStripMenuItem.Click += new System.EventHandler(this.UpOneLevelToolStripMenuItemClick);
			// 
			// downOneLevelToolStripMenuItem
			// 
			this.downOneLevelToolStripMenuItem.Enabled = false;
			this.downOneLevelToolStripMenuItem.Name = "downOneLevelToolStripMenuItem";
			this.downOneLevelToolStripMenuItem.ShortcutKeyDisplayString = "PgDn";
			this.downOneLevelToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.downOneLevelToolStripMenuItem.Text = "Down one level";
			this.downOneLevelToolStripMenuItem.Click += new System.EventHandler(this.DownOneLevelToolStripMenuItemClick);
			// 
			// downToBottomToolStripMenuItem
			// 
			this.downToBottomToolStripMenuItem.Enabled = false;
			this.downToBottomToolStripMenuItem.Name = "downToBottomToolStripMenuItem";
			this.downToBottomToolStripMenuItem.ShortcutKeyDisplayString = "End";
			this.downToBottomToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.downToBottomToolStripMenuItem.Text = "Down to bottom";
			this.downToBottomToolStripMenuItem.Click += new System.EventHandler(this.DownToBottomToolStripMenuItemClick);
			// 
			// saveElementsToolStripMenuItem
			// 
			this.saveElementsToolStripMenuItem.Name = "saveElementsToolStripMenuItem";
			this.saveElementsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.saveElementsToolStripMenuItem.Text = "Save objects to file";
			this.saveElementsToolStripMenuItem.Click += new System.EventHandler(this.SaveElementsToolStripMenuItemClick);
			// 
			// loadElementsToolStripMenuItem
			// 
			this.loadElementsToolStripMenuItem.Name = "loadElementsToolStripMenuItem";
			this.loadElementsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.loadElementsToolStripMenuItem.Text = "Load objects from file";
			this.loadElementsToolStripMenuItem.Click += new System.EventHandler(this.LoadElementsToolStripMenuItemClick);
			// 
			// pluginToolStripMenuItem
			// 
			this.pluginToolStripMenuItem.Name = "pluginToolStripMenuItem";
			this.pluginToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.pluginToolStripMenuItem.Text = "Plugins";
			this.pluginToolStripMenuItem.Visible = false;
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.helpToolStripMenuItem1,
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem1.Image")));
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
			this.helpToolStripMenuItem1.Text = "Help";
			this.helpToolStripMenuItem1.Click += new System.EventHandler(this.HelpToolStripMenuItem1Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btnSave,
									this.btnClipboard,
									this.btnPrint,
									this.btnEmail,
									this.toolStripSeparator2,
									this.btnDelete,
									this.toolStripSeparator3,
									this.btnCut,
									this.btnCopy,
									this.btnPaste,
									this.toolStripSeparator6,
									this.btnSettings,
									this.toolStripSeparator11,
									this.btnHelp});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(785, 25);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 0;
			// 
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "Save to default location";
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// btnClipboard
			// 
			this.btnClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnClipboard.Image")));
			this.btnClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnClipboard.Name = "btnClipboard";
			this.btnClipboard.Size = new System.Drawing.Size(23, 22);
			this.btnClipboard.Text = "Copy to clipboard";
			this.btnClipboard.Click += new System.EventHandler(this.BtnClipboardClick);
			// 
			// btnPrint
			// 
			this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
			this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(23, 22);
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.BtnPrintClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnDelete
			// 
			this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDelete.Enabled = false;
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(23, 22);
			this.btnDelete.Text = "Remove";
			this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnCut
			// 
			this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCut.Enabled = false;
			this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
			this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCut.Name = "btnCut";
			this.btnCut.Size = new System.Drawing.Size(23, 22);
			this.btnCut.Text = "Cut selected element to clipboard";
			this.btnCut.Click += new System.EventHandler(this.BtnCutClick);
			// 
			// btnCopy
			// 
			this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCopy.Enabled = false;
			this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
			this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(23, 22);
			this.btnCopy.Text = "Copy selected element to clipboard";
			this.btnCopy.Click += new System.EventHandler(this.BtnCopyClick);
			// 
			// btnPaste
			// 
			this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPaste.Enabled = false;
			this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
			this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPaste.Name = "btnPaste";
			this.btnPaste.Size = new System.Drawing.Size(23, 22);
			this.btnPaste.Text = "Paste element from clipboard";
			this.btnPaste.Click += new System.EventHandler(this.BtnPasteClick);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// btnSettings
			// 
			this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
			this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(23, 22);
			this.btnSettings.Text = "Preferences";
			this.btnSettings.Click += new System.EventHandler(this.BtnSettingsClick);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
			// 
			// btnHelp
			// 
			this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(23, 22);
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
			// 
			// propertiesToolStrip
			// 
			this.propertiesToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.propertiesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.propertiesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.obfuscateModeButton,
									this.highlightModeButton,
									this.btnFillColor,
									this.btnLineColor,
									this.lineThicknessLabel,
									this.lineThicknessUpDown,
									this.fontFamilyComboBox,
									this.fontSizeLabel,
									this.fontSizeUpDown,
									this.fontBoldButton,
									this.fontItalicButton,
									this.blurRadiusLabel,
									this.blurRadiusUpDown,
									this.brightnessLabel,
									this.brightnessUpDown,
									this.previewQualityLabel,
									this.previewQualityUpDown,
									this.magnificationFactorLabel,
									this.magnificationFactorUpDown,
									this.pixelSizeLabel,
									this.pixelSizeUpDown,
									this.arrowHeadsLabel,
									this.arrowHeadsDropDownButton,
									this.shadowButton,
									this.toolStripSeparator,
									this.toolStripSeparator10,
									this.btnConfirm,
									this.btnCancel});
			this.propertiesToolStrip.Location = new System.Drawing.Point(0, 49);
			this.propertiesToolStrip.MinimumSize = new System.Drawing.Size(0, 27);
			this.propertiesToolStrip.Name = "propertiesToolStrip";
			this.propertiesToolStrip.Size = new System.Drawing.Size(785, 27);
			this.propertiesToolStrip.Stretch = true;
			this.propertiesToolStrip.TabIndex = 2;
			// 
			// obfuscateModeButton
			// 
			this.obfuscateModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.obfuscateModeButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.pixelizeToolStripMenuItem,
									this.blurToolStripMenuItem});
			this.obfuscateModeButton.Image = ((System.Drawing.Image)(resources.GetObject("obfuscateModeButton.Image")));
			this.obfuscateModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.obfuscateModeButton.Name = "obfuscateModeButton";
			this.obfuscateModeButton.SelectedTag = Greenshot.Drawing.FilterContainer.PreparedFilter.BLUR;
			this.obfuscateModeButton.Size = new System.Drawing.Size(29, 24);
			this.obfuscateModeButton.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.BLUR;
			this.obfuscateModeButton.Text = "Obfuscate";
			// 
			// pixelizeToolStripMenuItem
			// 
			this.pixelizeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pixelizeToolStripMenuItem.Image")));
			this.pixelizeToolStripMenuItem.Name = "pixelizeToolStripMenuItem";
			this.pixelizeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.pixelizeToolStripMenuItem.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.PIXELIZE;
			this.pixelizeToolStripMenuItem.Text = "Pixelate";
			// 
			// blurToolStripMenuItem
			// 
			this.blurToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("blurToolStripMenuItem.Image")));
			this.blurToolStripMenuItem.Name = "blurToolStripMenuItem";
			this.blurToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.blurToolStripMenuItem.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.BLUR;
			this.blurToolStripMenuItem.Text = "Blur";
			// 
			// highlightModeButton
			// 
			this.highlightModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.highlightModeButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.textHighlightMenuItem,
									this.areaHighlightMenuItem,
									this.grayscaleHighlightMenuItem,
									this.magnifyMenuItem});
			this.highlightModeButton.Image = ((System.Drawing.Image)(resources.GetObject("highlightModeButton.Image")));
			this.highlightModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.highlightModeButton.Name = "highlightModeButton";
			this.highlightModeButton.SelectedTag = Greenshot.Drawing.FilterContainer.PreparedFilter.TEXT_HIGHTLIGHT;
			this.highlightModeButton.Size = new System.Drawing.Size(29, 24);
			this.highlightModeButton.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.TEXT_HIGHTLIGHT;
			this.highlightModeButton.Text = "Highlight";
			// 
			// textHighlightMenuItem
			// 
			this.textHighlightMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("textHighlightMenuItem.Image")));
			this.textHighlightMenuItem.Name = "textHighlightMenuItem";
			this.textHighlightMenuItem.Size = new System.Drawing.Size(140, 22);
			this.textHighlightMenuItem.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.TEXT_HIGHTLIGHT;
			this.textHighlightMenuItem.Text = "Text highlight";
			// 
			// areaHighlightMenuItem
			// 
			this.areaHighlightMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("areaHighlightMenuItem.Image")));
			this.areaHighlightMenuItem.Name = "areaHighlightMenuItem";
			this.areaHighlightMenuItem.Size = new System.Drawing.Size(140, 22);
			this.areaHighlightMenuItem.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.AREA_HIGHLIGHT;
			this.areaHighlightMenuItem.Text = "Area highlight";
			// 
			// grayscaleHighlightMenuItem
			// 
			this.grayscaleHighlightMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("grayscaleHighlightMenuItem.Image")));
			this.grayscaleHighlightMenuItem.Name = "grayscaleHighlightMenuItem";
			this.grayscaleHighlightMenuItem.Size = new System.Drawing.Size(140, 22);
			this.grayscaleHighlightMenuItem.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.GRAYSCALE;
			this.grayscaleHighlightMenuItem.Text = "Grayscale";
			// 
			// magnifyMenuItem
			// 
			this.magnifyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("magnifyMenuItem.Image")));
			this.magnifyMenuItem.Name = "magnifyMenuItem";
			this.magnifyMenuItem.Size = new System.Drawing.Size(140, 22);
			this.magnifyMenuItem.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.MAGNIFICATION;
			this.magnifyMenuItem.Text = "Magnify";
			// 
			// btnFillColor
			// 
			this.btnFillColor.AutoSize = false;
			this.btnFillColor.BackColor = System.Drawing.Color.Transparent;
			this.btnFillColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnFillColor.Image = ((System.Drawing.Image)(resources.GetObject("btnFillColor.Image")));
			this.btnFillColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnFillColor.Margin = new System.Windows.Forms.Padding(0);
			this.btnFillColor.Name = "btnFillColor";
			this.btnFillColor.SelectedColor = System.Drawing.Color.Transparent;
			this.btnFillColor.Size = new System.Drawing.Size(23, 24);
			// 
			// btnLineColor
			// 
			this.btnLineColor.AutoSize = false;
			this.btnLineColor.BackColor = System.Drawing.Color.Transparent;
			this.btnLineColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnLineColor.Image = ((System.Drawing.Image)(resources.GetObject("btnLineColor.Image")));
			this.btnLineColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnLineColor.Name = "btnLineColor";
			this.btnLineColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(222)))), ((int)(((byte)(250)))));
			this.btnLineColor.Size = new System.Drawing.Size(23, 24);
			// 
			// lineThicknessLabel
			// 
			this.lineThicknessLabel.Name = "lineThicknessLabel";
			this.lineThicknessLabel.Size = new System.Drawing.Size(73, 24);
			this.lineThicknessLabel.Text = "Line thickness";
			// 
			// lineThicknessUpDown
			// 
			this.lineThicknessUpDown.DecimalPlaces = 0;
			this.lineThicknessUpDown.Increment = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.lineThicknessUpDown.Maximum = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.lineThicknessUpDown.Minimum = new decimal(new int[] {
									0,
									0,
									0,
									0});
			this.lineThicknessUpDown.Name = "lineThicknessUpDown";
			this.lineThicknessUpDown.Size = new System.Drawing.Size(41, 24);
			this.lineThicknessUpDown.Text = "0";
			this.lineThicknessUpDown.Value = new decimal(new int[] {
									0,
									0,
									0,
									0});
			this.lineThicknessUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.lineThicknessUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// fontFamilyComboBox
			// 
			this.fontFamilyComboBox.AutoSize = false;
			this.fontFamilyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fontFamilyComboBox.MaxDropDownItems = 20;
			this.fontFamilyComboBox.Name = "fontFamilyComboBox";
			this.fontFamilyComboBox.Size = new System.Drawing.Size(200, 21);
			this.fontFamilyComboBox.Text = "Aharoni";
			this.fontFamilyComboBox.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.fontFamilyComboBox.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// fontSizeLabel
			// 
			this.fontSizeLabel.Name = "fontSizeLabel";
			this.fontSizeLabel.Size = new System.Drawing.Size(50, 24);
			this.fontSizeLabel.Text = "Font size";
			// 
			// fontSizeUpDown
			// 
			this.fontSizeUpDown.DecimalPlaces = 0;
			this.fontSizeUpDown.Increment = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.fontSizeUpDown.Maximum = new decimal(new int[] {
									500,
									0,
									0,
									0});
			this.fontSizeUpDown.Minimum = new decimal(new int[] {
									7,
									0,
									0,
									0});
			this.fontSizeUpDown.Name = "fontSizeUpDown";
			this.fontSizeUpDown.Size = new System.Drawing.Size(41, 24);
			this.fontSizeUpDown.Text = "12";
			this.fontSizeUpDown.Value = new decimal(new int[] {
									12,
									0,
									0,
									0});
			this.fontSizeUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.fontSizeUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// fontBoldButton
			// 
			this.fontBoldButton.CheckOnClick = true;
			this.fontBoldButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fontBoldButton.Image = ((System.Drawing.Image)(resources.GetObject("fontBoldButton.Image")));
			this.fontBoldButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.fontBoldButton.Name = "fontBoldButton";
			this.fontBoldButton.Size = new System.Drawing.Size(23, 24);
			this.fontBoldButton.Text = "Bold";
			this.fontBoldButton.Click += new System.EventHandler(this.FontBoldButtonClick);
			// 
			// fontItalicButton
			// 
			this.fontItalicButton.CheckOnClick = true;
			this.fontItalicButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fontItalicButton.Image = ((System.Drawing.Image)(resources.GetObject("fontItalicButton.Image")));
			this.fontItalicButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.fontItalicButton.Name = "fontItalicButton";
			this.fontItalicButton.Size = new System.Drawing.Size(23, 24);
			this.fontItalicButton.Text = "Italic";
			this.fontItalicButton.Click += new System.EventHandler(this.FontItalicButtonClick);
			// 
			// blurRadiusLabel
			// 
			this.blurRadiusLabel.Name = "blurRadiusLabel";
			this.blurRadiusLabel.Size = new System.Drawing.Size(57, 24);
			this.blurRadiusLabel.Text = "Blur radius";
			// 
			// blurRadiusUpDown
			// 
			this.blurRadiusUpDown.DecimalPlaces = 0;
			this.blurRadiusUpDown.Increment = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.blurRadiusUpDown.Maximum = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.blurRadiusUpDown.Minimum = new decimal(new int[] {
									0,
									0,
									0,
									0});
			this.blurRadiusUpDown.Name = "blurRadiusUpDown";
			this.blurRadiusUpDown.Size = new System.Drawing.Size(41, 24);
			this.blurRadiusUpDown.Text = "0";
			this.blurRadiusUpDown.Value = new decimal(new int[] {
									0,
									0,
									0,
									0});
			this.blurRadiusUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.blurRadiusUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// brightnessLabel
			// 
			this.brightnessLabel.Name = "brightnessLabel";
			this.brightnessLabel.Size = new System.Drawing.Size(57, 24);
			this.brightnessLabel.Text = "Brightness";
			// 
			// brightnessUpDown
			// 
			this.brightnessUpDown.DecimalPlaces = 0;
			this.brightnessUpDown.Increment = new decimal(new int[] {
									5,
									0,
									0,
									0});
			this.brightnessUpDown.Maximum = new decimal(new int[] {
									200,
									0,
									0,
									0});
			this.brightnessUpDown.Minimum = new decimal(new int[] {
									0,
									0,
									0,
									0});
			this.brightnessUpDown.Name = "brightnessUpDown";
			this.brightnessUpDown.Size = new System.Drawing.Size(41, 24);
			this.brightnessUpDown.Text = "100";
			this.brightnessUpDown.Value = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.brightnessUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.brightnessUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// previewQualityLabel
			// 
			this.previewQualityLabel.Name = "previewQualityLabel";
			this.previewQualityLabel.Size = new System.Drawing.Size(80, 13);
			this.previewQualityLabel.Text = "Preview quality";
			// 
			// previewQualityUpDown
			// 
			this.previewQualityUpDown.DecimalPlaces = 0;
			this.previewQualityUpDown.Increment = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.previewQualityUpDown.Maximum = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.previewQualityUpDown.Minimum = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.previewQualityUpDown.Name = "previewQualityUpDown";
			this.previewQualityUpDown.Size = new System.Drawing.Size(41, 21);
			this.previewQualityUpDown.Text = "50";
			this.previewQualityUpDown.Value = new decimal(new int[] {
									50,
									0,
									0,
									0});
			this.previewQualityUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.previewQualityUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// magnificationFactorLabel
			// 
			this.magnificationFactorLabel.Name = "magnificationFactorLabel";
			this.magnificationFactorLabel.Size = new System.Drawing.Size(45, 13);
			this.magnificationFactorLabel.Tag = Greenshot.Drawing.FilterContainer.PreparedFilter.MAGNIFICATION;
			this.magnificationFactorLabel.Text = "Magnify";
			// 
			// magnificationFactorUpDown
			// 
			this.magnificationFactorUpDown.DecimalPlaces = 0;
			this.magnificationFactorUpDown.Increment = new decimal(new int[] {
									2,
									0,
									0,
									0});
			this.magnificationFactorUpDown.Maximum = new decimal(new int[] {
									8,
									0,
									0,
									0});
			this.magnificationFactorUpDown.Minimum = new decimal(new int[] {
									2,
									0,
									0,
									0});
			this.magnificationFactorUpDown.Name = "magnificationFactorUpDown";
			this.magnificationFactorUpDown.Size = new System.Drawing.Size(29, 21);
			this.magnificationFactorUpDown.Text = "2";
			this.magnificationFactorUpDown.Value = new decimal(new int[] {
									2,
									0,
									0,
									0});
			this.magnificationFactorUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.magnificationFactorUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// pixelSizeLabel
			// 
			this.pixelSizeLabel.Name = "pixelSizeLabel";
			this.pixelSizeLabel.Size = new System.Drawing.Size(51, 13);
			this.pixelSizeLabel.Text = "Pixel Size";
			// 
			// pixelSizeUpDown
			// 
			this.pixelSizeUpDown.DecimalPlaces = 0;
			this.pixelSizeUpDown.Increment = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.pixelSizeUpDown.Maximum = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.pixelSizeUpDown.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.pixelSizeUpDown.Name = "pixelSizeUpDown";
			this.pixelSizeUpDown.Size = new System.Drawing.Size(41, 21);
			this.pixelSizeUpDown.Text = "5";
			this.pixelSizeUpDown.Value = new decimal(new int[] {
									5,
									0,
									0,
									0});
			this.pixelSizeUpDown.LostFocus += new System.EventHandler(this.ToolBarFocusableElementLostFocus);
			this.pixelSizeUpDown.GotFocus += new System.EventHandler(this.ToolBarFocusableElementGotFocus);
			// 
			// arrowHeadsLabel
			// 
			this.arrowHeadsLabel.Name = "arrowHeadsLabel";
			this.arrowHeadsLabel.Size = new System.Drawing.Size(63, 13);
			this.arrowHeadsLabel.Text = "Arrow head";
			// 
			// arrowHeadsDropDownButton
			// 
			this.arrowHeadsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.arrowHeadsDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.arrowHeadStartMenuItem,
									this.arrowHeadEndMenuItem,
									this.arrowHeadBothMenuItem,
									this.arrowHeadNoneMenuItem});
			this.arrowHeadsDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("arrowHeadsDropDownButton.Image")));
			this.arrowHeadsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.arrowHeadsDropDownButton.Name = "arrowHeadsDropDownButton";
			this.arrowHeadsDropDownButton.Size = new System.Drawing.Size(29, 20);
			this.arrowHeadsDropDownButton.Text = "Arrow heads";
			// 
			// arrowHeadStartMenuItem
			// 
			this.arrowHeadStartMenuItem.Name = "arrowHeadStartMenuItem";
			this.arrowHeadStartMenuItem.Size = new System.Drawing.Size(125, 22);
			this.arrowHeadStartMenuItem.Tag = Greenshot.Drawing.ArrowContainer.ArrowHeadCombination.START_POINT;
			this.arrowHeadStartMenuItem.Text = "Start point";
			this.arrowHeadStartMenuItem.Click += new System.EventHandler(this.ArrowHeadsToolStripMenuItemClick);
			// 
			// arrowHeadEndMenuItem
			// 
			this.arrowHeadEndMenuItem.Name = "arrowHeadEndMenuItem";
			this.arrowHeadEndMenuItem.Size = new System.Drawing.Size(125, 22);
			this.arrowHeadEndMenuItem.Tag = Greenshot.Drawing.ArrowContainer.ArrowHeadCombination.END_POINT;
			this.arrowHeadEndMenuItem.Text = "End point";
			this.arrowHeadEndMenuItem.Click += new System.EventHandler(this.ArrowHeadsToolStripMenuItemClick);
			// 
			// arrowHeadBothMenuItem
			// 
			this.arrowHeadBothMenuItem.Name = "arrowHeadBothMenuItem";
			this.arrowHeadBothMenuItem.Size = new System.Drawing.Size(125, 22);
			this.arrowHeadBothMenuItem.Tag = Greenshot.Drawing.ArrowContainer.ArrowHeadCombination.BOTH;
			this.arrowHeadBothMenuItem.Text = "Both";
			this.arrowHeadBothMenuItem.Click += new System.EventHandler(this.ArrowHeadsToolStripMenuItemClick);
			// 
			// arrowHeadNoneMenuItem
			// 
			this.arrowHeadNoneMenuItem.Name = "arrowHeadNoneMenuItem";
			this.arrowHeadNoneMenuItem.Size = new System.Drawing.Size(125, 22);
			this.arrowHeadNoneMenuItem.Tag = Greenshot.Drawing.ArrowContainer.ArrowHeadCombination.NONE;
			this.arrowHeadNoneMenuItem.Text = "None";
			this.arrowHeadNoneMenuItem.Click += new System.EventHandler(this.ArrowHeadsToolStripMenuItemClick);
			// 
			// shadowButton
			// 
			this.shadowButton.CheckOnClick = true;
			this.shadowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.shadowButton.Image = ((System.Drawing.Image)(resources.GetObject("shadowButton.Image")));
			this.shadowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.shadowButton.Name = "shadowButton";
			this.shadowButton.Size = new System.Drawing.Size(23, 20);
			this.shadowButton.Text = "Shadow";
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 27);
			// 
			// btnConfirm
			// 
			this.btnConfirm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
			this.btnConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(23, 20);
			this.btnConfirm.Text = "Confirm";
			this.btnConfirm.Click += new System.EventHandler(this.BtnConfirmClick);
			// 
			// btnCancel
			// 
			this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(23, 20);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnLineColor_
			// 
			this.btnLineColor_.BackColor = System.Drawing.SystemColors.Control;
			this.btnLineColor_.Image = ((System.Drawing.Image)(resources.GetObject("btnLineColor_.Image")));
			this.btnLineColor_.Name = "btnLineColor_";
			this.btnLineColor_.SelectedColor = System.Drawing.SystemColors.Control;
			this.btnLineColor_.Size = new System.Drawing.Size(23, 23);
			// 
			// btnFillColor_
			// 
			this.btnFillColor_.BackColor = System.Drawing.SystemColors.Control;
			this.btnFillColor_.Image = ((System.Drawing.Image)(resources.GetObject("btnFillColor_.Image")));
			this.btnFillColor_.Name = "btnFillColor_";
			this.btnFillColor_.SelectedColor = System.Drawing.SystemColors.Control;
			this.btnFillColor_.Size = new System.Drawing.Size(23, 23);
			// 
			// printDialog1
			// 
			this.printDialog1.UseEXDialog = true;
			// 
			// fileSavedStatusContextMenu
			// 
			this.fileSavedStatusContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.copyPathMenuItem,
									this.openDirectoryMenuItem});
			this.fileSavedStatusContextMenu.Name = "contextMenuStrip1";
			this.fileSavedStatusContextMenu.Size = new System.Drawing.Size(247, 48);
			// 
			// copyPathMenuItem
			// 
			this.copyPathMenuItem.Name = "copyPathMenuItem";
			this.copyPathMenuItem.Size = new System.Drawing.Size(246, 22);
			this.copyPathMenuItem.Text = "Copy path to clipboard";
			this.copyPathMenuItem.Click += new System.EventHandler(this.CopyPathMenuItemClick);
			// 
			// openDirectoryMenuItem
			// 
			this.openDirectoryMenuItem.Name = "openDirectoryMenuItem";
			this.openDirectoryMenuItem.Size = new System.Drawing.Size(246, 22);
			this.openDirectoryMenuItem.Text = "Open directory in Windows Explorer";
			this.openDirectoryMenuItem.Click += new System.EventHandler(this.OpenDirectoryMenuItemClick);
			// 
			// btnEmail
			// 
			this.btnEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnEmail.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail.Image")));
			this.btnEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEmail.Name = "btnEmail";
			this.btnEmail.Size = new System.Drawing.Size(23, 22);
			this.btnEmail.Text = "Email";
			this.btnEmail.Click += new System.EventHandler(this.BtnEmailClick);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(154, 6);
			// 
			// insert_window_toolstripmenuitem
			// 
			this.insert_window_toolstripmenuitem.Name = "insert_window_toolstripmenuitem";
			this.insert_window_toolstripmenuitem.Size = new System.Drawing.Size(157, 22);
			this.insert_window_toolstripmenuitem.Text = "Insert window";
			this.insert_window_toolstripmenuitem.MouseEnter += new System.EventHandler(this.Insert_window_toolstripmenuitemMouseEnter);
			// 
			// dimensionsLabel
			//
			this.dimensionsLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.dimensionsLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.dimensionsLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.dimensionsLabel.Name = "dimensionsLabel";
			this.dimensionsLabel.Size = new System.Drawing.Size(48, 17);
			this.dimensionsLabel.Text = "123x321";
			// 
			// ImageEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(785, 485);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = GreenshotPlugin.Core.GreenshotResources.getGreenshotIcon();
			this.KeyPreview = true;
			this.Name = "ImageEditorForm";
			this.Activated += new System.EventHandler(this.ImageEditorFormActivated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageEditorFormFormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageEditorFormKeyDown);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.propertiesToolStrip.ResumeLayout(false);
			this.propertiesToolStrip.PerformLayout();
			this.fileSavedStatusContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ToolStripStatusLabel dimensionsLabel;
		private System.Windows.Forms.ToolStripMenuItem insert_window_toolstripmenuitem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton btnEmail;
		private System.Windows.Forms.ToolStripMenuItem grayscaleHighlightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem areaHighlightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem textHighlightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem magnifyMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrowHeadStartMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrowHeadEndMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrowHeadBothMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrowHeadNoneMenuItem;
		private Greenshot.Controls.BindableToolStripButton btnCancel;
		private Greenshot.Controls.BindableToolStripButton btnConfirm;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private Greenshot.Controls.BindableToolStripDropDownButton highlightModeButton;
		private System.Windows.Forms.ToolStripMenuItem pixelizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem blurToolStripMenuItem;
		private Greenshot.Controls.BindableToolStripDropDownButton obfuscateModeButton;
		private System.Windows.Forms.ToolStripButton btnHighlight;
		private System.Windows.Forms.ToolStripMenuItem emailToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadElementsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveElementsToolStripMenuItem;
		private Greenshot.Controls.FontFamilyComboBox fontFamilyComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private Greenshot.Controls.BindableToolStripButton shadowButton;
		private Greenshot.Controls.BindableToolStripButton fontItalicButton;
		private Greenshot.Controls.BindableToolStripButton fontBoldButton;
		private Greenshot.Controls.ToolStripNumericUpDown fontSizeUpDown;
		private System.Windows.Forms.ToolStripLabel fontSizeLabel;
		private Greenshot.Controls.ToolStripNumericUpDown brightnessUpDown;
		private System.Windows.Forms.ToolStripLabel brightnessLabel;
		private System.Windows.Forms.ToolStripMenuItem pluginToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton arrowHeadsDropDownButton;
		private System.Windows.Forms.ToolStripLabel arrowHeadsLabel;
		private Greenshot.Controls.ToolStripNumericUpDown pixelSizeUpDown;
		private System.Windows.Forms.ToolStripLabel pixelSizeLabel;
		private Greenshot.Controls.ToolStripNumericUpDown magnificationFactorUpDown;
		private System.Windows.Forms.ToolStripLabel magnificationFactorLabel;
		private Greenshot.Controls.ToolStripNumericUpDown previewQualityUpDown;
		private System.Windows.Forms.ToolStripLabel previewQualityLabel;
		private Greenshot.Controls.ToolStripNumericUpDown blurRadiusUpDown;
		private System.Windows.Forms.ToolStripLabel blurRadiusLabel;
		private Greenshot.Controls.ToolStripColorButton btnFillColor_;
		private Greenshot.Controls.ToolStripColorButton btnLineColor_;
		private System.Windows.Forms.ToolStrip propertiesToolStrip;
		private System.Windows.Forms.ToolStripLabel lineThicknessLabel;
		private Greenshot.Controls.ToolStripNumericUpDown lineThicknessUpDown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.ToolStripButton btnObfuscate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripButton btnCrop;
		private System.Windows.Forms.ToolStripMenuItem openDirectoryMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyPathMenuItem;
		private System.Windows.Forms.ContextMenuStrip fileSavedStatusContextMenu;
		private System.Windows.Forms.ToolStripMenuItem downToBottomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem upToTopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem downOneLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem upOneLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrangeToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnCursor;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton btnArrow;
		private System.Windows.Forms.ToolStripMenuItem drawArrowToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnText;
		private System.Windows.Forms.ToolStripMenuItem drawLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnLine;
		private System.Windows.Forms.ToolStripButton btnSettings;
		private System.Windows.Forms.ToolStripButton btnHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton btnPrint;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem fileStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeObjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addTextBoxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addEllipseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addRectangleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem objectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyImageToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripButton btnCut;
		private System.Windows.Forms.ToolStripButton btnPaste;
		private System.Windows.Forms.ToolStripButton btnCopy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton btnClipboard;
		private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnEllipse;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripButton btnRect;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private GreenshotPlugin.Controls.NonJumpingPanel panel1;
		private Greenshot.Controls.ToolStripColorButton btnFillColor;
		private Greenshot.Controls.ToolStripColorButton btnLineColor;
		private System.Windows.Forms.ToolStripMenuItem autoCropToolStripMenuItem;
	}
}
