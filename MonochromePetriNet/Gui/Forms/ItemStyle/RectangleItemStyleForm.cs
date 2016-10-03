using System.Windows.Forms;
using System.Drawing;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class RectangleItemStyleForm : Form
    {
        public event System.EventHandler<Core.Events.UpdateItemBorderEventArgs> Changed;

        private Core.Style.RectangleShapeStyle _style;

        public RectangleItemStyleForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(Core.Style.RectangleShapeStyle style)
        {
            _style = style;
            numWidth.Value = _style.Width;
            numHeight.Value = _style.Height;
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
            bool isBorderChanged = ((_style.Width != (int)numWidth.Value)
                || (_style.Height != (int)numHeight.Value)
                || (_style.BorderPen.Width != (float)numBorderWidth.Value));
            _style.Width = (int)numWidth.Value;
            _style.Height = (int)numHeight.Value;
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
