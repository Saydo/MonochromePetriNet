using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("MoveRule")]
    public class MoveRuleXml
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("priority")]
        public int Priority { get; set; }
        [XmlAttribute("output_state_id")]
        public int OutputStateId { get; set; }
        [XmlAttribute("input_state_id")]
        public int InputStateId { get; set; }
        [XmlAttribute("transition_id")]
        public int TransitionId { get; set; }
        [XmlElement("PermissiblePreviousMoveActions")]
        public PermissibleMoveActionsXml PermissiblePreviousMoveActions;
        [XmlElement("PermissibleIntermediateMoveActions")]
        public PermissibleMoveActionsXml PermissibleIntermediateMoveActions;
        [XmlElement("PermissibleNextMoveActions")]
        public PermissibleMoveActionsXml PermissibleNextMoveActions;
        [XmlElement("PermissiblePostMoveActions")]
        public PermissibleMoveActionsXml PermissiblePostMoveActions;
        [XmlElement("InputStreams")]
        public InputStreamsXml InputStreams;
        [XmlElement("OutputStreams")]
        public OutputStreamsXml OutputStreams;

        public MoveRuleXml()
            : this(-1, -1, -1, -1)
        {
        }

        public MoveRuleXml(int id, int outputState, int inputState, int transition, int priority = 1)
        {
            Id = id;
            Priority = priority;
            OutputStateId = outputState;
            InputStateId = inputState;
            TransitionId = transition;
            PermissiblePreviousMoveActions = new PermissibleMoveActionsXml();
            PermissibleIntermediateMoveActions = new PermissibleMoveActionsXml();
            PermissibleNextMoveActions = new PermissibleMoveActionsXml();
            PermissiblePostMoveActions = new PermissibleMoveActionsXml();
            InputStreams = new InputStreamsXml();
            OutputStreams = new OutputStreamsXml();
        }
    }
}
