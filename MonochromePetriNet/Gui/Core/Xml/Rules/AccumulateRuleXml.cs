using System.Collections.Generic;
using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("AccumulateRule")]
    public class AccumulateRuleXml
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("state_id")]
        public int StateId { get; set; }
        [XmlAttribute("new_marker_count")]
        public int NewMarkerCount { get; set; }
        [XmlAttribute("updated_marker_count")]
        public int UpdatedMarkerCount { get; set; }
        [XmlAttribute("priority")]
        public int Priority { get; set; }
        [XmlElement("MarkerIdConvert")]
        public List<MarkerIdConvertXml> MarkerIdConverts;
        [XmlElement("RestMarkersIdConvert")]
        public MarkerIdConvertXml RestMarkerIdConvert;

        public AccumulateRuleXml()
            : this(-1, -1)
        {
        }

        public AccumulateRuleXml(int id, int stateId, int updatedMarkerCount = -1, int newMarkerCount = 0, int priority = 1)
        {
            Id = id;
            StateId = stateId;
            UpdatedMarkerCount = updatedMarkerCount;
            NewMarkerCount = newMarkerCount;
            Priority = priority;
            MarkerIdConverts = new List<MarkerIdConvertXml>();
            RestMarkerIdConvert = new MarkerIdConvertXml();
        }
    }
}
