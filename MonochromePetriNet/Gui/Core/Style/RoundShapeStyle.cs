using System.Drawing;

namespace MonochromePetriNet.Gui.Core.Style
{
    public class RoundShapeStyle : ShapeStyle
    {
        private int _radius;

        public int Radius
        {
            get { return _radius; }
            set { _radius = (value < 0 ? -value : value); }
        }

        public RoundShapeStyle(int radius = 10) : base()
        {
            Radius = radius;
        }

        public RoundShapeStyle(int radius, Brush fillBrush, Pen borderPen)
            : base(fillBrush, borderPen)
        {
            Radius = radius;
        }
    }
}
