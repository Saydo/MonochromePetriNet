using System.Xml.Serialization;
using System.Collections.Generic;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class PermissibleMoveActionsXml
    {
        [XmlAttribute("access")]
        public string Access { get; set; }
        [XmlElement("Action")]
        public List<ActionXml> Actions;

        public PermissibleMoveActionsXml()
        {
            Access = "Pass";
            Actions = new List<ActionXml>();
        }

        public PermissibleMoveActionsXml(string access)
        {
            Access = access;
            Actions = new List<ActionXml>();
        }
    }
}
