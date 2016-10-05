using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class TransitionXml
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("x")]
        public int X { get; set; }
        [XmlAttribute("y")]
        public int Y { get; set; }

        public TransitionXml()
        {
            Id = -1;
            X = Y = 0;
        }

        public TransitionXml(int id, int x, int y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }
    }
}
