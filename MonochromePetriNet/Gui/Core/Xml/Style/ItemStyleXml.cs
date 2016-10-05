using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class ItemStyleXml
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("Pen")]
        public PenXml BorderPen;
        [XmlElement("Brush")]
        public BrushXml FillBrush;

        public ItemStyleXml()
        {
            Name = "";
            BorderPen = new PenXml("BorderPen", System.Drawing.Color.FromArgb(0, 0, 0));
            FillBrush = new BrushXml("FillBrush", System.Drawing.Color.FromArgb(0, 0, 0));
        }

        public ItemStyleXml(string name) : this()
        {
            Name = name;
        }
    }
}
