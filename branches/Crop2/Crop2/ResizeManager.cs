﻿using System.Drawing;
using System.Windows.Forms;
using GraphicsMgrLib;
using System;

namespace Crop
{
    public class ResizeManager
    {
        public bool IsMouseDown { get; private set; }
        public bool IsVisible { get; private set; }

        private Crop crop;
        private RegionManager region;
        private Label[] resizers;
        private int mx, my, keyboardMove;
        private Rectangle tempRect;

        public ResizeManager(Crop crop, RegionManager region)
        {
            this.crop = crop;
            this.region = region;
            resizers = new Label[8];
            keyboardMove = 5;

            crop.KeyDown += new KeyEventHandler(crop_KeyDown);

            for (int i = 0; i < resizers.Length; i++)
            {
                resizers[i] = new Label();
                resizers[i].Tag = i;
                resizers[i].Width = resizers[i].Height = 10;
                resizers[i].BackColor = Color.White;
                resizers[i].BorderStyle = BorderStyle.FixedSingle;
                resizers[i].MouseDown += new MouseEventHandler(ResizeManager_MouseDown);
                resizers[i].MouseUp += new MouseEventHandler(ResizeManager_MouseUp);
                resizers[i].MouseMove += new MouseEventHandler(ResizeManager_MouseMove);
                resizers[i].Visible = false;
                crop.Controls.Add(resizers[i]);
            }

            resizers[0].Cursor = Cursors.SizeNWSE;
            resizers[1].Cursor = Cursors.SizeNS;
            resizers[2].Cursor = Cursors.SizeNESW;
            resizers[3].Cursor = Cursors.SizeWE;
            resizers[4].Cursor = Cursors.SizeNWSE;
            resizers[5].Cursor = Cursors.SizeNS;
            resizers[6].Cursor = Cursors.SizeNESW;
            resizers[7].Cursor = Cursors.SizeWE;
        }

        private void crop_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsVisible)
            {
                Rectangle rect = region.Rectangle;

                switch (e.KeyCode)
                {
                    case Keys.Left:
                        rect.X -= keyboardMove;
                        break;
                    case Keys.Right:
                        rect.X += keyboardMove;
                        break;
                    case Keys.Up:
                        rect.Y -= keyboardMove;
                        break;
                    case Keys.Down:
                        rect.Y += keyboardMove;
                        break;
                }

                region.Rectangle = rect;
                Update();
            }
        }

        private void ResizeManager_MouseDown(object sender, MouseEventArgs e)
        {
            mx = e.X;
            my = e.Y;
            tempRect = region.Rectangle;
            IsMouseDown = true;
        }

        private void ResizeManager_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void ResizeManager_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                Label resizer = (Label)sender;
                int index = (int)resizer.Tag;

                if (index <= 2)
                { // top row
                    tempRect.Y += e.Y - my;
                    tempRect.Height -= e.Y - my;
                }
                else if (index >= 4 && index <= 6)
                { // bottom row
                    tempRect.Height += e.Y - my;
                }

                if (index >= 2 && index <= 4)
                { // right row
                    tempRect.Width += e.X - mx;
                }
                else if (index >= 6 || index == 0)
                { // left row
                    tempRect.X += e.X - mx;
                    tempRect.Width -= e.X - mx;
                }

                region.Rectangle = tempRect;
                Update(tempRect);
            }
        }

        public void Show()
        {
            ChangeVisible(true);
        }

        public void Hide()
        {
            ChangeVisible(false);
        }

        private void ChangeVisible(bool visible)
        {
            IsVisible = visible;

            for (int i = 0; i < resizers.Length; i++)
            {
                resizers[i].Visible = visible;
            }
        }

        public void Update()
        {
            Update(region.Rectangle);
        }

        public void Update(Rectangle rect)
        {
            int pos = resizers[0].Width / 2;

            int[] xChoords = new int[] { rect.Left - pos, rect.Left + rect.Width / 2 - pos, rect.Left + rect.Width - pos };
            int[] yChoords = new int[] { rect.Top - pos, rect.Top + rect.Height / 2 - pos, rect.Top + rect.Height - pos };

            resizers[0].Left = xChoords[0]; resizers[0].Top = yChoords[0];
            resizers[1].Left = xChoords[1]; resizers[1].Top = yChoords[0];
            resizers[2].Left = xChoords[2]; resizers[2].Top = yChoords[0];
            resizers[3].Left = xChoords[2]; resizers[3].Top = yChoords[1];
            resizers[4].Left = xChoords[2]; resizers[4].Top = yChoords[2];
            resizers[5].Left = xChoords[1]; resizers[5].Top = yChoords[2];
            resizers[6].Left = xChoords[0]; resizers[6].Top = yChoords[2];
            resizers[7].Left = xChoords[0]; resizers[7].Top = yChoords[1];

            crop.Refresh();
        }
    }
}