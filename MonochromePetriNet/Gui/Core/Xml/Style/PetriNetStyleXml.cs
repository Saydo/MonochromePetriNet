using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public enum PenListItem { SelectionPen, LinePen };

    public class PetriNetStyleXml
    {
        [XmlElement("SelectionMode")]
        public SelectionModeXml SelectionMode;
        [XmlElement("Pen")]
        public PenXml[] PenList;
        [XmlElement("MarkerStyle")]
        public RoundItemStyleXml MarkerStyle;
        [XmlElement("StateStyle")]
        public RoundItemStyleXml StateStyle;
        [XmlElement("TransitionStyle")]
        public RectangleItemStyleXml TransitionStyle;

        public PetriNetStyleXml()
        {
            PenList = new PenXml[2] {
                new PenXml("SelectionPen", System.Drawing.Color.FromArgb(0, 0, 0)),
                new PenXml("LinePen", System.Drawing.Color.FromArgb(0, 0, 0))
            };
            SelectionMode = new SelectionModeXml();
            MarkerStyle = new RoundItemStyleXml();
            StateStyle = new RoundItemStyleXml();
            TransitionStyle = new RectangleItemStyleXml();
        }
    }
}
