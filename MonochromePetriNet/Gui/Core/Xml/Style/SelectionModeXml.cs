using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class SelectionModeXml
    {
        private string _value;
        [XmlAttribute("value")]
        public string Value
        {
            get { return _value; }
            set { _value = (value.Equals("Full") ? value : "Partial"); }
        }

        public SelectionModeXml()
        {
            Value = "Partial";
        }

        public SelectionModeXml(string mode)
        {
            Value = mode;
        }
    }
}
