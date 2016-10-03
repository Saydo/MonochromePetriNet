using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PetriNet = MonochromePetriNet.Container;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class TransitionInfoForm : Form
    {
        private DataTable _inputLinksTable;
        private DataTable _outputLinksTable;

        public TransitionInfoForm()
        {
            InitializeComponent();
            // Input Links Table
            _inputLinksTable = new DataTable();
            _inputLinksTable.Columns.Add(new DataColumn("Id", typeof(int)));
            dgvInputLinks.DataSource = _inputLinksTable;
            dgvInputLinks.ReadOnly = true;
            dgvInputLinks.AllowUserToAddRows = false;
            dgvInputLinks.ColumnHeadersVisible = false;
            dgvInputLinks.RowHeadersVisible = false;
            dgvInputLinks.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInputLinks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Output Links Table
            _outputLinksTable = new DataTable();
            _outputLinksTable.Columns.Add(new DataColumn("Id", typeof(int)));
            dgvOutputLinks.DataSource = _outputLinksTable;
            dgvOutputLinks.ReadOnly = true;
            dgvOutputLinks.AllowUserToAddRows = false;
            dgvOutputLinks.ColumnHeadersVisible = false;
            dgvOutputLinks.RowHeadersVisible = false;
            dgvOutputLinks.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutputLinks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ShowDialog(PetriNet.TransitionWrapper transition)
        {
            txtId.Text = transition.Transition.Id.ToString();
            UpdateInputLinksTable(transition);
            UpdateOutputLinksTable(transition);
            base.ShowDialog();
        }

        private void UpdateInputLinksTable(PetriNet.TransitionWrapper transition)
        {
            DataRow newRow;
            _inputLinksTable.Clear();
            if (!ReferenceEquals(transition, null))
            {
                for (int i = 0; i < transition.InputLinks.Count; ++i)
                {
                    newRow = _inputLinksTable.NewRow();
                    newRow["Id"] = transition.InputLinks[i].State.State.Id;
                    _inputLinksTable.Rows.Add(newRow);
                }
            }
        }

        private void UpdateOutputLinksTable(PetriNet.TransitionWrapper transition)
        {
            DataRow newRow;
            _outputLinksTable.Clear();
            if (!ReferenceEquals(transition, null))
            {
                for (int i = 0; i < transition.OutputLinks.Count; ++i)
                {
                    newRow = _outputLinksTable.NewRow();
                    newRow["Id"] = transition.OutputLinks[i].State.State.Id;
                    _outputLinksTable.Rows.Add(newRow);
                }
            }
        }
    }
}
