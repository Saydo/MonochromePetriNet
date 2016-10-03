using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace MonochromePetriNet.Gui.Core.Style
{
    public class ShapeStyle
    {
        public Brush FillBrush { get; set; }
        public Pen BorderPen { get; set; }

        public ShapeStyle()
        {
            FillBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
            BorderPen = new Pen(Color.FromArgb(0, 0, 0), 1.0F);
        }

        public ShapeStyle(Brush fillBrush, Pen borderPen)
        {
            FillBrush = fillBrush;
            BorderPen = borderPen;
        }
    }
}
