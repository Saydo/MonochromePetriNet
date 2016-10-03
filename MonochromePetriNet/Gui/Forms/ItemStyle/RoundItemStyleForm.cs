using System.Drawing;
using System.Windows.Forms;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class RoundItemStyleForm : Form
    {
        public event System.EventHandler<Core.Events.UpdateItemBorderEventArgs> Changed;

        private Core.Style.RoundShapeStyle _style;

        public RoundItemStyleForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(Core.Style.RoundShapeStyle style)
        {
            _style = style;
            numRadius.Value = _style.Radius;
            pnlFillColor.BackColor = ((SolidBrush)_style.FillBrush).Color;
            pnlBorderColor.BackColor = _style.BorderPen.Color;
            numBorderWidth.Value = (int)_style.BorderPen.Width;
            base.ShowDialog();
        }

        private void ChooseFillColor()
        {
            dlgColor.Color = pnlFillColor.BackColor;
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                pnlFillColor.BackColor = dlgColor.Color;
            }
        }

        private void ChooseBorderColor()
        {
            dlgColor.Color = pnlBorderColor.BackColor;
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                pnlBorderColor.BackColor = dlgColor.Color;
            }
        }

        private void AcceptChanges()
        {
            bool isBorderChanged = ((_style.Radius != (int)numRadius.Value)
                || (_style.BorderPen.Width != (float)numBorderWidth.Value));
            _style.Radius = (int)numRadius.Value;
            ((SolidBrush)_style.FillBrush).Color = pnlFillColor.BackColor;
            _style.BorderPen.Color = pnlBorderColor.BackColor;
            _style.BorderPen.Width = (float)numBorderWidth.Value;
            if (this.Changed != null)
            {
                this.Changed(this, new Core.Events.UpdateItemBorderEventArgs(isBorderChanged));
            }
            _style = null;
            this.Close();
        }
    }
}
