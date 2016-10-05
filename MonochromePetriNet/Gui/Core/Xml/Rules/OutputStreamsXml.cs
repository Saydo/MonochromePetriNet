using System.Collections.Generic;
using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("OutputStreams")]
    public class OutputStreamsXml
    {
        [XmlAttribute("capacity")]
        public int Capacity { get; set; }
        [XmlAttribute("strategy")]
        public string Strategy { get; set; }
        [XmlElement("OutputStream")]
        public List<StreamXml> OutputStreams;

        public OutputStreamsXml()
            : this(-1, "Clone")
        {
        }

        public OutputStreamsXml(int capacity, string strategy)
        {
            Capacity = capacity;
            Strategy = strategy;
            OutputStreams = new List<StreamXml>();
        }
    }
}
