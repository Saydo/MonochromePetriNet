using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class BrushXml
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("color")]
        public string Color { get; set; }

        public BrushXml()
        {
            var color = System.Drawing.Color.FromArgb(0, 0, 0);
            SetColor(color);
        }

        public BrushXml(string name, System.Drawing.Color color)
        {
            this.Name = name;
            SetColor(color);
        }

        public void SetColor(System.Drawing.Color color)
        {
            this.Color = color.ToArgb().ToString("X");
        }

        public void FromBrush(System.Drawing.SolidBrush brush)
        {
            this.Color = brush.Color.ToArgb().ToString("X");
        }

        public System.Drawing.SolidBrush ToBrush()
        {
            int argb = System.Int32.Parse(this.Color, System.Globalization.NumberStyles.HexNumber);
            return new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(argb));
        }
    }
}
