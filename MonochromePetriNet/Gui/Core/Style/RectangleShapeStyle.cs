using System.Drawing;

namespace MonochromePetriNet.Gui.Core.Style
{
    public class RectangleShapeStyle : ShapeStyle
    {
        private int _width;
        private int _height;

        public int Width
        {
            get { return _width; }
            set { _width = (value < 0 ? -value : value); }
        }

        public int Height
        {
            get { return _height; }
            set { _height = (value < 0 ? -value : value); }
        }

        public RectangleShapeStyle() : this(10, 10)
        {
        }

        public RectangleShapeStyle(int width, int height) : base()
        {
            _width = width;
            _height = height;
        }

        public RectangleShapeStyle(int width, int height, Brush fillBrush, Pen borderPen)
            : base(fillBrush, borderPen)
        {
            _width = width;
            _height = height;
        }
    }
}
