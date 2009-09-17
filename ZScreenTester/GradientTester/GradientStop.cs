﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.ColorsLib;
using System.Drawing;
using System.Globalization;

namespace GradientTester
{
    public class GradientStop
    {
        public Color Color { get; set; }
        public float Offset { get; set; }

        public GradientStop(Color color, float offset)
        {
            Color = color;
            Offset = offset;
        }

        public GradientStop(string color, string offset)
        {
            this.Color = MyColors.ParseColor(color);

            if (this.Color == null)
            {
                throw new Exception("Color is unknown.");
            }

            this.Offset = float.Parse(offset, CultureInfo.InvariantCulture);
        }
    }
}