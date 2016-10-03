using System.Drawing;
using MonochromePetriNet.Container.GraphicsItems;

namespace MonochromePetriNet.Gui.Core.Style
{
    public class PetriNetStyle
    {
        public OverlapType SelectionMode;
        public Pen SelectionPen;
        public Pen LinePen;
        public RoundShapeStyle MarkerStyle;
        public RoundShapeStyle StateStyle;
        public RectangleShapeStyle TransitionStyle;

        public PetriNetStyle()
        {
            SelectionMode = OverlapType.Partial;
            SelectionPen = new Pen(Color.FromArgb(0, 0, 0));
            LinePen = new Pen(Color.FromArgb(0, 0, 0));
            MarkerStyle = new RoundShapeStyle();
            StateStyle = new RoundShapeStyle();
            TransitionStyle = new RectangleShapeStyle();
        }
    }
}