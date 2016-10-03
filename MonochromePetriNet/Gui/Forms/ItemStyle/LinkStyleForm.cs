using System.Drawing;
using System.Windows.Forms;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class LinkStyleForm : Form
    {
        public event System.EventHandler Changed;

        private Pen _pen;

        public LinkStyleForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(Pen pen)
        {
            _pen = pen;
            numWidth.Value = (int)pen.Width;
            pnlColor.BackColor = _pen.Color;
            base.ShowDialog();
        }

        private void ChooseColor()
        {
            dlgColor.Color = pnlColor.BackColor;
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                pnlColor.BackColor = dlgColor.Color;
            }
        }

        private void AcceptChanges()
        {
            _pen.Width = (float)numWidth.Value;
            _pen.Color = pnlColor.BackColor;
            if (this.Changed != null)
            {
                this.Changed(this, System.EventArgs.Empty);
            }
            _pen = null;
            this.Close();
        }
    }
}
