﻿using System.ComponentModel;
using System.Drawing;
using HelpersLib;
using HelpersLib.GraphicsHelper;
using ImageEffects.IPlugin;

namespace ImageManipulation
{
    public class Reflection : IPluginItem
    {
        public override string Name { get { return "Reflection"; } }

        public override string Description { get { return "Draw reflection bottom of screenshots."; } }

        private int percentage;

        [Description("Reflection height size relative to screenshot height.\nValue need to be between 1 to 100.")]
        public int Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value.Between(1, 100);
            }
        }

        private int transparency;

        [Description("Reflection transparency start from this value to 0.\nValue need to be between 0 to 255.")]
        public int Transparency
        {
            get
            {
                return transparency;
            }
            set
            {
                transparency = value.Between(0, 255);
            }
        }

        private int offset;

        [Description("Reflection position will be start: Screenshot height + Offset")]
        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        private bool skew;

        [Description("Adding skew to reflection from bottom left to bottom right.")]
        public bool Skew
        {
            get
            {
                return skew;
            }
            set
            {
                skew = value;
            }
        }

        private int skewSize;

        [Description("How much pixel skew left to right.")]
        public int SkewSize
        {
            get
            {
                return skewSize;
            }
            set
            {
                skewSize = value;
            }
        }

        public Reflection()
        {
            percentage = 20;
            transparency = 255;
            offset = 0;
            skew = true;
            skewSize = 25;
        }

        public override Image ApplyEffect(Image img)
        {
            return ImageEffectsHelper.DrawReflection(img, percentage, transparency, offset, skew, skewSize);
        }
    }
}