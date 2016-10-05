using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    [XmlRoot("MarkerIdConvert")]
    public class MarkerIdConvertXml
    {
        [XmlAttribute("id_convert")]
        public string IdConvert { get; set; }
        [XmlAttribute("new_marker_count")]
        public int NewMarkerCount { get; set; }

        public MarkerIdConvertXml()
        {
            NewMarkerCount = 0;
            IdConvert = "Move";
        }

        public MarkerIdConvertXml(string idConvert, int newMarkerCount)
        {
            IdConvert = idConvert;
            NewMarkerCount = newMarkerCount;
        }
    }
}
