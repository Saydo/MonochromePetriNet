using System.Windows.Forms;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class MarkerInfoForm : Form
    {
        public MarkerInfoForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(int id, int stateId, string type)
        {
            txtId.Text = id.ToString();
            txtStateId.Text = stateId.ToString();
            base.ShowDialog();
        }
    }
}
