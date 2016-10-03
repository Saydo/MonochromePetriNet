using System.Data;
using System.Windows.Forms;
using PetriNet = MonochromePetriNet.Container;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class StateInfoForm : Form
    {
        private DataTable _markersTable;
        private DataTable _inputLinksTable;
        private DataTable _outputLinksTable;

        public StateInfoForm()
        {
            InitializeComponent();
            // Markers Table
            _markersTable = new DataTable();
            _markersTable.Columns.Add(new DataColumn("Id", typeof(int)));
            dgvMarkers.DataSource = _markersTable;
            dgvMarkers.ReadOnly = true;
            dgvMarkers.AllowUserToAddRows = false;
            dgvMarkers.ColumnHeadersVisible = false;
            dgvMarkers.RowHeadersVisible = false;
            dgvMarkers.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMarkers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        public void ShowDialog(PetriNet.StateWrapper state)
        {
            txtId.Text = state.State.Id.ToString();
            txtType.Text = "";
            UpdateMarkersTable(state);
            UpdateInputLinksTable(state);
            UpdateOutputLinksTable(state);
            base.ShowDialog();
        }

        private void UpdateMarkersTable(PetriNet.StateWrapper state)
        {
            DataRow newRow;
            _markersTable.Clear();
            if (!ReferenceEquals(state, null))
            {
                for (int i = 0; i < state.Markers.Count; ++i)
                {
                    newRow = _markersTable.NewRow();
                    newRow["Id"] = state.Markers[i];
                    _markersTable.Rows.Add(newRow);
                }
            }
        }

        private void UpdateInputLinksTable(PetriNet.StateWrapper state)
        {
            DataRow newRow;
            _inputLinksTable.Clear();
            if (!ReferenceEquals(state, null))
            {
                for (int i = 0; i < state.InputLinks.Count; ++i)
                {
                    newRow = _inputLinksTable.NewRow();
                    newRow["Id"] = state.InputLinks[i].Transition.Transition.Id;
                    _inputLinksTable.Rows.Add(newRow);
                }
            }
        }

        private void UpdateOutputLinksTable(PetriNet.StateWrapper state)
        {
            DataRow newRow;
            _outputLinksTable.Clear();
            if (!ReferenceEquals(state, null))
            {
                for (int i = 0; i < state.OutputLinks.Count; ++i)
                {
                    newRow = _outputLinksTable.NewRow();
                    newRow["Id"] = state.OutputLinks[i].Transition.Transition.Id;
                    _outputLinksTable.Rows.Add(newRow);
                }
            }
        }
    }
}
