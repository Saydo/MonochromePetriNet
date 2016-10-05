using System.Collections.Generic;
using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("MonochromePetriNet")]
    public class MonochromePetriNetXml
    {
        [XmlElement("Style")]
        public PetriNetStyleXml Style;
        [XmlArray("PreviousMoveActions")]
        [XmlArrayItem("AccumulateRule")]
        public List<AccumulateRuleXml> PreviousMoveActions;
        [XmlArray("IntermediateMoveActions")]
        [XmlArrayItem("AccumulateRule")]
        public List<AccumulateRuleXml> IntermediateMoveActions;
        [XmlArray("NextMoveActions")]
        [XmlArrayItem("AccumulateRule")]
        public List<AccumulateRuleXml> NextMoveActions;
        [XmlArray("PostMoveActions")]
        [XmlArrayItem("AccumulateRule")]
        public List<AccumulateRuleXml> PostMoveActions;
        [XmlArray("MoveRules")]
        [XmlArrayItem("MoveRule")]
        public List<MoveRuleXml> MoveRules;
        [XmlArray("States")]
        [XmlArrayItem("State")]
        public List<StateXml> States;
        [XmlArray("Transitions")]
        [XmlArrayItem("Transition")]
        public List<TransitionXml> Transitions;
        [XmlArray("Links")]
        [XmlArrayItem("Link")]
        public List<LinkXml> Links;

        public MonochromePetriNetXml()
        {
            Style = new PetriNetStyleXml();
            PreviousMoveActions = new List<AccumulateRuleXml>();
            IntermediateMoveActions = new List<AccumulateRuleXml>();
            NextMoveActions = new List<AccumulateRuleXml>();
            PostMoveActions = new List<AccumulateRuleXml>();
            MoveRules = new List<MoveRuleXml>();
            States = new List<StateXml>();
            Transitions = new List<TransitionXml>();
            Links = new List<LinkXml>();
        }
    }
}
