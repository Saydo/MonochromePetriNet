using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class RoundItemStyleXml : ItemStyleXml
    {
        [XmlAttribute("radius")]
        public int Radius { get; set; }

        public RoundItemStyleXml() : base()
        {
            Radius = 1;
        }

        public RoundItemStyleXml(string name, int radius) : base(name)
        {
            Radius = radius;
        }
    }
}
