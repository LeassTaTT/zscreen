﻿using System.Drawing;
using GraphicsMgrLib;
using Plugins;

namespace ImageManipulation
{
    public class Rotate : IPluginItem
    {
        public override string Name { get { return "Rotate"; } }

        public override string Description { get { return "Rotate"; } }

        private float angle;

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                OnPreviewTextChanged(angle.ToString());
            }
        }

        public override Image ApplyEffect(Image img)
        {
            return GraphicsMgr.RotateImage(img, angle);
        }
    }
}