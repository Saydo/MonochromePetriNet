using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PetriNet = MonochromePetriNet.Container;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class RemoveMarkerForm : Form
    {
        public event EventHandler<Core.Events.PetriNetNodeEventArgs> ClearButtonClick;
        public event EventHandler<Core.Events.StateEventArgs> RemoveButtonClick;

        private PetriNet.StateWrapper _selectedState;
        private DataTable _markersTable;
        private PetriNet.MonochromePetriNet _petriNet;

        public RemoveMarkerForm(PetriNet.MonochromePetriNet petriNet)
        {
            InitializeComponent();
            _petriNet = petriNet;
            _markersTable = new DataTable();
            _markersTable.Columns.Add(new DataColumn("Id", typeof(int)));
            dgvMarkers.DataSource = _markersTable;
            dgvMarkers.ReadOnly = true;
            dgvMarkers.AllowUserToAddRows = false;
            dgvMarkers.ColumnHeadersVisible = false;
            dgvMarkers.RowHeadersVisible = false;
            dgvMarkers.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMarkers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ShowDialog(PetriNet.StateWrapper selectedState)
        {
            _selectedState = selectedState;
            UpdateMarkersTable();
            base.ShowDialog();
        }

        new public void Hide()
        {
            _selectedState = null;
            base.Hide();
        }

        private void UpdateMarkersTable()
        {
            DataRow newRow;
            _markersTable.Clear();
            if (!ReferenceEquals(_selectedState, null))
            {
                for (int i = 0; i < _selectedState.Markers.Count; ++i)
                {
                    newRow = _markersTable.NewRow();
                    newRow["Id"] = _selectedState.Markers[i];
                    _markersTable.Rows.Add(newRow);
                }
            }
        }

        private void ClearMarkers()
        {
            _markersTable.Clear();
            if (ClearButtonClick != null)
            {
                ClearButtonClick(this,
                    new Core.Events.PetriNetNodeEventArgs(_selectedState.State.Id));
            }
        }

        private void RemoveMarkerFromTable(int id)
        {
            for (int i = 0; i < _markersTable.Rows.Count; ++i)
            {
                if (id == _markersTable.Rows[i].Field<int>(0))
                {
                    _markersTable.Rows.RemoveAt(i);
                    return;
                }
            }
        }

        private void RemoveMarkers()
        {
            List<int> removedMarkers = new List<int>();
            int id;
            for (int i = dgvMarkers.SelectedRows.Count - 1; i >= 0; --i)
            {
                id = (int)dgvMarkers.SelectedRows[i].Cells[0].Value;
                RemoveMarkerFromTable(id);
                removedMarkers.Add(id);
            }
            if (RemoveButtonClick != null)
            {
                RemoveButtonClick(this,
                    new Core.Events.StateEventArgs(_selectedState.State.Id, removedMarkers));
            }
        }
    }
}
