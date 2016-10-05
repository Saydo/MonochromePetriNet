using System.Collections.Generic;
using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("InputStreams")]
    public class InputStreamsXml
    {
        [XmlAttribute("capacity")]
        public int Capacity { get; set; }
        [XmlElement("InputStream")]
        public List<StreamXml> InputStreams;

        public InputStreamsXml()
            : this(-1)
        {
        }

        public InputStreamsXml(int capacity)
        {
            Capacity = capacity;
            InputStreams = new List<StreamXml>();
        }
    }
}
