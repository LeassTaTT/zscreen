﻿namespace GradientTester
{
    partial class GradientMaker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.txtStartPointX = new System.Windows.Forms.TextBox();
            this.txtStartPointY = new System.Windows.Forms.TextBox();
            this.txtEndPointX = new System.Windows.Forms.TextBox();
            this.txtEndPointY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.btnBrowseColor = new System.Windows.Forms.Button();
            this.rtbCodes = new System.Windows.Forms.RichTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(8, 400);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Preview";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Location = new System.Drawing.Point(208, 8);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(184, 48);
            this.pbPreview.TabIndex = 2;
            this.pbPreview.TabStop = false;
            // 
            // txtStartPointX
            // 
            this.txtStartPointX.Location = new System.Drawing.Point(96, 8);
            this.txtStartPointX.Name = "txtStartPointX";
            this.txtStartPointX.Size = new System.Drawing.Size(48, 20);
            this.txtStartPointX.TabIndex = 3;
            // 
            // txtStartPointY
            // 
            this.txtStartPointY.Location = new System.Drawing.Point(152, 8);
            this.txtStartPointY.Name = "txtStartPointY";
            this.txtStartPointY.Size = new System.Drawing.Size(48, 20);
            this.txtStartPointY.TabIndex = 4;
            // 
            // txtEndPointX
            // 
            this.txtEndPointX.Location = new System.Drawing.Point(96, 32);
            this.txtEndPointX.Name = "txtEndPointX";
            this.txtEndPointX.Size = new System.Drawing.Size(48, 20);
            this.txtEndPointX.TabIndex = 5;
            // 
            // txtEndPointY
            // 
            this.txtEndPointY.Location = new System.Drawing.Point(152, 32);
            this.txtEndPointY.Name = "txtEndPointY";
            this.txtEndPointY.Size = new System.Drawing.Size(48, 20);
            this.txtEndPointY.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start point (x, y):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End point (x, y):";
            // 
            // btnAddColor
            // 
            this.btnAddColor.Location = new System.Drawing.Point(296, 368);
            this.btnAddColor.Name = "btnAddColor";
            this.btnAddColor.Size = new System.Drawing.Size(99, 23);
            this.btnAddColor.TabIndex = 9;
            this.btnAddColor.Text = "Add / Update";
            this.btnAddColor.UseVisualStyleBackColor = true;
            this.btnAddColor.Click += new System.EventHandler(this.btnAddColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Color:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Offset:";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(48, 368);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(112, 20);
            this.txtColor.TabIndex = 12;
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(248, 368);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(40, 20);
            this.txtOffset.TabIndex = 13;
            // 
            // btnBrowseColor
            // 
            this.btnBrowseColor.Location = new System.Drawing.Point(168, 368);
            this.btnBrowseColor.Name = "btnBrowseColor";
            this.btnBrowseColor.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseColor.TabIndex = 14;
            this.btnBrowseColor.Text = "...";
            this.btnBrowseColor.UseVisualStyleBackColor = true;
            this.btnBrowseColor.Click += new System.EventHandler(this.btnBrowseColor_Click);
            // 
            // rtbCodes
            // 
            this.rtbCodes.AcceptsTab = true;
            this.rtbCodes.Location = new System.Drawing.Point(8, 64);
            this.rtbCodes.Name = "rtbCodes";
            this.rtbCodes.Size = new System.Drawing.Size(384, 296);
            this.rtbCodes.TabIndex = 15;
            this.rtbCodes.Text = "";
            this.rtbCodes.SelectionChanged += new System.EventHandler(this.rtbCodes_SelectionChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(240, 400);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(320, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(88, 400);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 18;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // GradientMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 432);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rtbCodes);
            this.Controls.Add(this.btnBrowseColor);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEndPointY);
            this.Controls.Add(this.txtEndPointX);
            this.Controls.Add(this.txtStartPointY);
            this.Controls.Add(this.txtStartPointX);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.btnTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GradientMaker";
            this.Text = "Gradient maker";
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.TextBox txtStartPointX;
        private System.Windows.Forms.TextBox txtStartPointY;
        private System.Windows.Forms.TextBox txtEndPointX;
        private System.Windows.Forms.TextBox txtEndPointY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Button btnBrowseColor;
        private System.Windows.Forms.RichTextBox rtbCodes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHelp;
    }
}

