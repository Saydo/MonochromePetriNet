using System.Xml.Serialization;

namespace MonochromePetriNet.Gui.Core.Xml
{
    public class PenXml
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("color")]
        public string Color { get; set; }
        [XmlAttribute("width")]
        public int Width { get; set; }

        public PenXml()
        {
            this.Name = "";
            this.Width = 1;
            var color = System.Drawing.Color.FromArgb(0, 0, 0);
            SetColor(color);
        }

        public PenXml(string name, System.Drawing.Color color, int width = 1)
        {
            this.Name = name;
            this.Width = width;
            SetColor(color);
        }

        public void SetColor(System.Drawing.Color color)
        {
            this.Color = color.ToArgb().ToString("X");
        }

        public void FromPen(System.Drawing.Pen pen)
        {
            this.Width = (int)pen.Width;
            this.Color = pen.Color.ToArgb().ToString("X");
        }

        public System.Drawing.Pen ToPen()
        {
            int argb = System.Int32.Parse(this.Color, System.Globalization.NumberStyles.HexNumber);
            return new System.Drawing.Pen(System.Drawing.Color.FromArgb(argb), this.Width);
        }
    }
}
