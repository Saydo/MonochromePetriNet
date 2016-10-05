using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class StreamXml
    {
        [XmlAttribute("state_id")]
        public int StateId { get; set; }
        [XmlAttribute("action")]
        public string Action { get; set; }
        [XmlAttribute("priority")]
        public int Priority { get; set; }
        [XmlAttribute("capacity")]
        public int Capacity { get; set; }

        public StreamXml()
            : this(-1, "Pass")
        {
        }

        public StreamXml(int stateId, string action, int priority = 1, int capacity = -1)
        {
            StateId = stateId;
            Action = action;
            Priority = priority;
            Capacity = capacity;
        }
    }
}
