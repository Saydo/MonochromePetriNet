using System.Collections.Generic;
using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class StateXml
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("x")]
        public int X { get; set; }
        [XmlAttribute("y")]
        public int Y { get; set; }
        [XmlElement("Marker")]
        public string Markers;

        public StateXml()
        {
            Id = -1;
            X = Y = 0;
            Markers = "";
        }

        public StateXml(int id, int x, int y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Markers = "";
        }

        public void SetMarkers(List<int> markers)
        {
            if (markers.Count == 0) return;
            this.Markers = markers[0].ToString();
            for (int i = 1; i < markers.Count; ++i)
            {
                this.Markers += " " + markers[i].ToString();
            }
        }
    }
}
