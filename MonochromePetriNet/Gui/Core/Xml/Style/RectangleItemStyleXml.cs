using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class RectangleItemStyleXml : ItemStyleXml
    {
        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        public RectangleItemStyleXml() : base()
        {
            Width = 10;
            Height = 10;
        }

        public RectangleItemStyleXml(string name, int w, int h) : base(name)
        {
            Width = w;
            Height = h;
        }
    }
}
