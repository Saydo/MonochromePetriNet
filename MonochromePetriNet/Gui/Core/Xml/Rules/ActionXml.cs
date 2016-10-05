using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("Action")]
    public class ActionXml
    {
        [XmlAttribute("rule_id")]
        public int RuleId { get; set; }
        [XmlAttribute("access")]
        public string Access { get; set; }

        public ActionXml()
        {
            RuleId = -1;
            Access = "Pass";
        }

        public ActionXml(int ruleId, string access)
        {
            RuleId = ruleId;
            Access = access;
        }
    }
}
