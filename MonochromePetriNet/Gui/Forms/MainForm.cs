using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using PetriNet = MonochromePetriNet.Container;
using MonochromePetriNet.Container.GraphicsItems;

namespace MonochromePetriNet.Gui.Forms
{
    public partial class MainForm : Form
    {
        private enum ItemMapMode
        {
            View, Move, AddState, AddTransition, AddMarker, AddLink, Remove, RemoveMarker
        };

        private PetriNet.MonochromePetriNet _petriNet;
        private Core.SelectionArea _selectionArea;
        private Core.Style.PetriNetStyle _style;
        private bool _mousePressed;
        private bool _itemSelected;
        private Point _lastMousePosition;
        private ItemMapMode _mapMode;
        private PetriNet.StateWrapper _selectedState;
        private PetriNet.TransitionWrapper _selectedTransition;
        private string _currentFile;

        public MainForm()
        {
            _petriNet = new PetriNet.MonochromePetriNet();
            _style = new Core.Style.PetriNetStyle();
            InitializeComponent();
            SetDefaultStyle();
            _selectionArea = new Core.SelectionArea();
            _mousePressed = false;
            _itemSelected = false;
            _selectedState = null;
            _selectedTransition = null;
            _lastMousePosition = new Point();
            _mapMode = ItemMapMode.View;
            _currentFile = null;
            UpdateStatus(GetCurrentMapModeName());
        }

        public void UpdateMarkersStyle(bool isBorderChanged)
        {
            if (isBorderChanged)
            {
                _petriNet.States.ForEachState((state) => {
                    if (state.MarkerGraphics != null)
                    {
                        state.MarkerGraphics = new RoundGraphicsItem(state.MarkerGraphics.Id, state.MarkerGraphics.Center, _style.MarkerStyle.Radius);
                        state.MarkerGraphics.BorderPen = _style.MarkerStyle.BorderPen;
                        state.MarkerGraphics.FillBrush = _style.MarkerStyle.FillBrush;
                        state.MarkerGraphics.SelectionPen= _style.SelectionPen;
                    }
                });
            }
            pbMap.Refresh();
        }

        public void UpdateStatesStyle(bool isBorderChanged)
        {
            if (isBorderChanged)
            {
                _petriNet.States.ForEachState((state) => {
                    state.State = new RoundGraphicsItem(state.State.Id, state.State.Center, _style.StateStyle.Radius);
                    state.State.BorderPen = _style.StateStyle.BorderPen;
                    state.State.FillBrush = _style.StateStyle.FillBrush;
                    state.State.SelectionPen = _style.SelectionPen;
                });
            }
            pbMap.Refresh();
        }

        public void UpdateTransitionsStyle(bool isBorderChanged)
        {
            if (isBorderChanged)
            {
                _petriNet.Transitions.ForEachTransition((transition) => {
                transition.Transition = new RectangleGraphicsItem(transition.Id,
                    transition.Transition.Center, _style.TransitionStyle.Width,
                        _style.TransitionStyle.Height, 2);
                    transition.Transition.BorderPen = _style.TransitionStyle.BorderPen;
                    transition.Transition.FillBrush = _style.TransitionStyle.FillBrush;
                    transition.Transition.SelectionPen = _style.SelectionPen;
                });
            }
            pbMap.Refresh();
        }

        public void UpdateMap()
        {
            pbMap.Refresh();
        }

        private void SetSelectionArea(int x, int y, int w, int h)
        {
            _selectionArea.X = x;
            _selectionArea.Y = y;
            _selectionArea.Width = w;
            _selectionArea.Height = h;
            _selectionArea.Visible = true;
            _selectionArea.HorizontalDirection = Core.HorizontalDirection.Right;
            _selectionArea.VerticalDirection = Core.VerticalDirection.Top;
            _petriNet.Select(x, y, w, h, _style.SelectionMode);
        }

        private void UpdateSelectionAreaByPos(int x, int y)
        {
            int dx = x - _selectionArea.X;
            int dy = y - _selectionArea.Y;
            _selectionArea.Width = System.Math.Abs(dx);
            _selectionArea.Height = System.Math.Abs(dy);
            if (dx < 0)
            {
                _selectionArea.HorizontalDirection = Core.HorizontalDirection.Left;
                if (dy < 0)
                {
                    _selectionArea.VerticalDirection = Core.VerticalDirection.Bottom;
                    _petriNet.Select(x, y, _selectionArea.Width, _selectionArea.Height, _style.SelectionMode);
                }
                else
                {
                    _selectionArea.VerticalDirection = Core.VerticalDirection.Top;
                    _petriNet.Select(x, _selectionArea.Y, _selectionArea.Width, _selectionArea.Height, _style.SelectionMode);
                }
            }
            else
            {
                _selectionArea.HorizontalDirection = Core.HorizontalDirection.Right;
                if (dy < 0)
                {
                    _selectionArea.VerticalDirection = Core.VerticalDirection.Bottom;
                    _petriNet.Select(_selectionArea.X, y, _selectionArea.Width, _selectionArea.Height, _style.SelectionMode);
                }
                else
                {
                    _selectionArea.VerticalDirection = Core.VerticalDirection.Top;
                    _petriNet.Select(_selectionArea.X, _selectionArea.Y, _selectionArea.Width,
                        _selectionArea.Height, _style.SelectionMode);
                }
            }
        }

        private void UpdateSelectionArea(int w, int h)
        {
            _selectionArea.Width = w;
            _selectionArea.Height = h;
            _petriNet.Select(_selectionArea.X, _selectionArea.Y, w, h, _style.SelectionMode);
        }

        private void SetDefaultStyle()
        {
            _style.StateStyle = new Core.Style.RoundShapeStyle(10,
                new SolidBrush(Color.FromArgb(0, 240, 0)),
                new Pen(Color.FromArgb(0, 0, 0), 1.0F));
            _style.MarkerStyle = new Core.Style.RoundShapeStyle(4, new SolidBrush(Color.FromArgb(150, 0, 220)),
                new Pen(Color.FromArgb(0, 0, 0), 1.0F));
            _style.TransitionStyle = new Core.Style.RectangleShapeStyle(10, 20,
                new SolidBrush(Color.FromArgb(220, 220, 0)), new Pen(Color.FromArgb(0, 0, 0), 1.0F));
        }

        private void MoveMarkers()
        {
            System.Console.WriteLine("[MoveMarker]");
        }

        private void StartMoveMarkerLoop()
        {
            System.Console.WriteLine("[StartMoveMarkerLoop]");
        }

        private void StopMoveMarkerLoop()
        {
            System.Console.WriteLine("[StopMoveMarkerLoop]");
        }

        private void MainFormLoad(object sender, System.EventArgs e)
        {
            UpdateSelectionModeGui();
        }

        private void ItemMapPaint(object sender, PaintEventArgs e)
        {
            _petriNet.Draw(e.Graphics);
            _selectionArea.Draw(e.Graphics);
        }

        private void AddNewState(Point location)
        {
            int id = _petriNet.GenerateItemId();
            var item = new RoundGraphicsItem(id, location, _style.StateStyle.Radius, 2);
            item.BorderPen = _style.StateStyle.BorderPen;
            item.FillBrush = _style.StateStyle.FillBrush;
            item.SelectionPen = _style.SelectionPen;
            _petriNet.States.Add(item);
            TreeNode treeNode = new TreeNode("State " + id.ToString(), 0, 0);
            treeNode.Tag = id;
            trvStates.Nodes.Add(treeNode);
            pbMap.Refresh();
        }

        private void AddNewTransition(Point location)
        {
            int id = _petriNet.GenerateItemId();
            var item = new RectangleGraphicsItem(id, location,
                _style.TransitionStyle.Width, _style.TransitionStyle.Height, 2);
            item.BorderPen = _style.TransitionStyle.BorderPen;
            item.FillBrush = _style.TransitionStyle.FillBrush;
            item.SelectionPen = _style.SelectionPen;
            _petriNet.Transitions.Add(item);
            TreeNode treeNode = new TreeNode("Transition " + id.ToString(), 0, 0);
            treeNode.Tag = id;
            trvTransitions.Nodes.Add(treeNode);
            pbMap.Refresh();
        }

        private void AddNewMarker(Point location)
        {
            var selectedStates = _petriNet.States.Find(location.X, location.Y);
            if (selectedStates.Count > 0)
            {
                var state = selectedStates[selectedStates.Count - 1];
                int markerId = _petriNet.GenerateItemId();
                var item = new RoundGraphicsItem(markerId, location, _style.MarkerStyle.Radius, 3);
                item.BorderPen = _style.MarkerStyle.BorderPen;
                item.FillBrush = _style.MarkerStyle.FillBrush;
                item.SelectionPen = _style.SelectionPen;
                _petriNet.Markers.Add(state.Id, item);
                int stateIndex = FindStateIndexInTreeView(state.Id);
                if (stateIndex >= 0)
                {
                    TreeNode treeNode = new TreeNode("Marker " + markerId.ToString(), 1, 1);
                    treeNode.Tag = markerId;
                    trvStates.Nodes[stateIndex].Nodes.Add(treeNode);
                }
            }
            pbMap.Refresh();
        }

        private int FindStateIndexInTreeView(int id)
        {
            for (int i = 0; i < trvStates.Nodes.Count; ++i)
            {
                if ((int)trvStates.Nodes[i].Tag == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private void AddNewLink(Point location)
        {
            if (_itemSelected)
            {
                if (!ReferenceEquals(_selectedState, null))
                {
                    var chosenTransitions = _petriNet.Transitions.Find(location.X, location.Y);
                    if (chosenTransitions.Count > 0)
                    {
                        var link = _petriNet.Links.Add(_selectedState.State.Id, chosenTransitions[0].Transition.Id,
                            PetriNet.LinkDirection.FromStateToTransition);
                        link.Link.Pen = _style.LinePen;
                        link.Link.SelectionPen = _style.SelectionPen;
                    }
                }
                else
                {
                    var chosenStates = _petriNet.States.Find(location.X, location.Y);
                    if (chosenStates.Count > 0)
                    {
                        _petriNet.Links.Add(chosenStates[0].State.Id, _selectedTransition.Transition.Id,
                            PetriNet.LinkDirection.FromTransitionToState);
                    }
                }
                _petriNet.DeselectItems();
                _selectedState = null;
                _selectedTransition = null;
                _itemSelected = false;
            }
            else
            {
                var chosenStates = _petriNet.States.Find(location.X, location.Y);
                if (chosenStates.Count > 0)
                {
                    _selectedState = chosenStates[0];
                    _itemSelected = true;
                    _selectedState.State.Select();
                }
                else
                {
                    var chosenTransitions = _petriNet.Transitions.Find(location.X, location.Y);
                    if (chosenTransitions.Count > 0)
                    {
                        _selectedTransition = chosenTransitions[0];
                        _itemSelected = true;
                        _selectedTransition.Transition.Select();
                    }
                }
            }
            pbMap.Refresh();
        }

        private void ItemMapMouseClick(object sender, MouseEventArgs e)
        {
            //System.Console.WriteLine("ItemMapMouseClick");
            switch (_mapMode)
            {
            case ItemMapMode.AddState:
                //System.Console.WriteLine("Add state");
                AddNewState(e.Location);
                break;
            case ItemMapMode.AddTransition:
                AddNewTransition(e.Location);
                break;
            case ItemMapMode.AddMarker:
                AddNewMarker(e.Location);
                break;
            case ItemMapMode.AddLink:
                AddNewLink(e.Location);
                break;
            case ItemMapMode.RemoveMarker:
                var chosenStates2 = _petriNet.States.Find(e.X, e.Y);
                if (chosenStates2.Count > 0)
                {
                    dlgRemoveMarker.ShowDialog(chosenStates2[0]);
                }
                break;
            }
        }

        private void ItemMapMouseDown(object sender, MouseEventArgs e)
        {
            if ((_mapMode == ItemMapMode.AddState) || (_mapMode == ItemMapMode.AddTransition)
                || (_mapMode == ItemMapMode.AddMarker) || (_mapMode == ItemMapMode.AddLink)
                || (_mapMode == ItemMapMode.RemoveMarker))
            {
                return;
            }
            //System.Console.WriteLine("ItemMapMouseDown");
            _mousePressed = true;
            _lastMousePosition = e.Location;
            if (_mapMode == ItemMapMode.Move)
            {
                var chosenStates = _petriNet.States.Find(e.X, e.Y);
                if (chosenStates.Count > 0)
                {
                    var selectedState = GetSelectedState(chosenStates);
                    if (ReferenceEquals(selectedState, null))
                    {
                        _petriNet.DeselectItems();
                        SetSelectionArea(e.X, e.Y, 1, 1);
                        _selectionArea.Visible = false;
                    }
                    _itemSelected = true;
                }
                else
                {
                    var chosenTransitions = _petriNet.Transitions.Find(e.X, e.Y);
                    if (chosenTransitions.Count > 0)
                    {
                        var selectedTransition = GetSelectedTransition(chosenTransitions);
                        if (ReferenceEquals(selectedTransition, null))
                        {
                            _petriNet.DeselectItems();
                            SetSelectionArea(e.X, e.Y, 1, 1);
                            _selectionArea.Visible = false;
                        }
                        _itemSelected = true;
                    }
                    else
                    {
                        var chosenLinks = _petriNet.Links.Find(e.X, e.Y);
                        if (chosenLinks.Count > 0)
                        {
                            var selectedLink = GetSelectedLink(chosenLinks);
                            if (ReferenceEquals(selectedLink, null))
                            {
                                _petriNet.DeselectItems();
                                SetSelectionArea(e.X, e.Y, 1, 1);
                                _selectionArea.Visible = true;
                            }
                            _itemSelected = true;
                        }
                        else
                        {
                            _petriNet.DeselectItems();
                            SetSelectionArea(e.X, e.Y, 1, 1);
                        }
                    }
                }
            }
            else
            {

                _petriNet.DeselectItems();
                SetSelectionArea(e.X, e.Y, 1, 1);
            }
            this.pbMap.Refresh();
        }

        private void ItemMapMouseMove(object sender, MouseEventArgs e)
        {
            if (_mousePressed)
            {
                //System.Console.WriteLine("ItemMapMouseMove");
                if ((_mapMode == ItemMapMode.Move) && (_itemSelected))
                {
                    _petriNet.MoveSelectedItems(e.X - _lastMousePosition.X, e.Y - _lastMousePosition.Y);
                    _lastMousePosition = e.Location;
                }
                else
                {
                    UpdateSelectionAreaByPos(e.X, e.Y);
                }
                this.pbMap.Refresh();
            }
        }

        private void ItemMapMouseUp(object sender, MouseEventArgs e)
        {
            if (!_mousePressed)
            {
                return;
            }
            //System.Console.WriteLine("ItemMapMouseUp");
            _mousePressed = false;
            if (_mapMode != ItemMapMode.AddLink)
            {
                _itemSelected = false;
            }
            _selectionArea.Visible = false;
            this.pbMap.Refresh();
        }

        private void MainFormKeyDown(object sender, KeyEventArgs e)
        {
            if ((_mapMode == ItemMapMode.Remove) && (e.KeyCode == Keys.Delete))
            {
                _petriNet.States.ForEachSelectedState(RemoveSelectedStateFromTree);
                _petriNet.Transitions.ForEachSelectedTransition(RemoveSelectedTransitionFromTree);
                _petriNet.RemoveSelectedItems();
                pbMap.Refresh();
            }
        }

        private void RemoveSelectedStateFromTree(PetriNet.StateWrapper state)
        {
            for (int j = trvStates.Nodes.Count - 1; j >= 0; --j)
            {
                if ((int)trvStates.Nodes[j].Tag == state.Id)
                {
                    trvStates.Nodes.RemoveAt(j);
                    break;
                }
            }
        }

        private void RemoveSelectedTransitionFromTree(PetriNet.TransitionWrapper transition)
        {
            for (int j = trvTransitions.Nodes.Count - 1; j >= 0; --j)
            {
                if ((int)trvTransitions.Nodes[j].Tag == transition.Id)
                {
                    trvTransitions.Nodes.RemoveAt(j);
                    break;
                }
            }
        }

        private void SetItemMapMode(ItemMapMode mode)
        {
            _petriNet.DeselectItems();
            _selectionArea.Visible = false;
            _mousePressed = false;
            _itemSelected = false;
            _selectedState = null;
            _selectedTransition = null;
            pbMap.Refresh();
            if (_mapMode != mode)
            {
                _mapMode = mode;
                UpdateStatus(GetCurrentMapModeName());
            }
        }

        private void SetSelectionMode(OverlapType overlap)
        {
            _style.SelectionMode = overlap;
        }

        private PetriNet.StateWrapper GetSelectedState(List<PetriNet.StateWrapper> stateList)
        {
            for (int i = 0; i < stateList.Count; ++i)
            {
                if (stateList[i].State.IsSelected())
                {
                    return stateList[i];
                }
            }
            return null;
        }

        private PetriNet.TransitionWrapper GetSelectedTransition(List<PetriNet.TransitionWrapper> transitionList)
        {
            for (int i = 0; i < transitionList.Count; ++i)
            {
                if (transitionList[i].Transition.IsSelected())
                {
                    return transitionList[i];
                }
            }
            return null;
        }

        private PetriNet.LinkWrapper GetSelectedLink(List<PetriNet.LinkWrapper> linkList)
        {
            for (int i = 0; i < linkList.Count; ++i)
            {
                if (linkList[i].Link.IsSelected())
                {
                    return linkList[i];
                }
            }
            return null;
        }

        private void UpdateSelectionModeGui()
        {
            if (_style.SelectionMode == OverlapType.Full)
            {
                this.mniSelectionModeFull.Checked = true;
            }
            else
            {
                this.mniSelectionModePartial.Checked = true;
            }
        }

        private void LoadFromFile()
        {
            /*
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                ClearMap();
                if (_petriNet.Deserialize(dlgOpenFile.FileName))
                {
                    _currentFile = dlgOpenFile.FileName;
                    pbMap.Refresh();
                }
                else
                {
                    pbMap.Refresh();
                    MessageBox.Show("Error: Could not load file!");
                }
            }
            */
        }

        private void SaveToFile()
        {
            /*
            if (_currentFile == null)
            {
                SaveFileAs();
            }
            else
            {
                if (!_petriNet.Serialize(_currentFile))
                {
                    MessageBox.Show("Error: Could not save file!");
                }
            }
            */
        }

        private void SaveFileAs()
        {
            /*
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                System.Console.WriteLine("File:{0}", dlgSaveFile.FileName);
                if (_petriNet.Serialize(dlgSaveFile.FileName))
                {
                    _currentFile = dlgSaveFile.FileName;
                }
                else
                {
                    MessageBox.Show("Error: Could not save file!");
                }
            }
            */
        }

        private void ClearMarkers(int stateId)
        {
            ClearMarkersFromStateTree(stateId);
            _petriNet.Markers.RemoveFromState(stateId);
            pbMap.Refresh();
        }

        private void RemoveMarkers(int stateId, List<int> markers)
        {
            for (int i = 0; i < markers.Count; ++i)
            {
                RemoveMarkerFromStateTree(markers[i], stateId);
                _petriNet.Markers.Remove(markers[i]);
                pbMap.Refresh();
            }
        }

        private void ClearMarkersFromStateTree(int stateId)
        {
            for (int i = 0; i < trvStates.Nodes.Count; ++i)
            {
                if ((int)trvStates.Nodes[i].Tag == stateId)
                {
                    trvStates.Nodes[i].Nodes.Clear();
                    return;
                }
            }
        }

        private void RemoveMarkerFromStateTree(int id, int stateId)
        {
            for (int i = 0; i < trvStates.Nodes.Count; ++i)
            {
                if ((int)trvStates.Nodes[i].Tag == stateId)
                {
                    for (int j = 0; j < trvStates.Nodes[i].Nodes.Count; ++j)
                    {
                        if ((int)trvStates.Nodes[i].Nodes[j].Tag == id)
                        {
                            trvStates.Nodes[i].Nodes.RemoveAt(j);
                            return;
                        }
                    }
                    return;
                }
            }
        }

        private void UpdateStatus(string operation)
        {
            lblStatusText.Text = string.Format("Current operation: \"{0}\"", operation);
            stsStatus.Refresh();
        }

        private string GetCurrentMapModeName()
        {
            switch (_mapMode)
            {
                case ItemMapMode.AddLink:
                    return "Add Link";
                case ItemMapMode.RemoveMarker:
                    return "Remove Marker";
                case ItemMapMode.AddState:
                    return "Add State";
                case ItemMapMode.AddTransition:
                    return "Add Transition";
                case ItemMapMode.AddMarker:
                    return "Add Marker";
                default:
                    return _mapMode.ToString();
            }
        }

        private void ClearMap()
        {
            trvStates.Nodes.Clear();
            trvTransitions.Nodes.Clear();
            _petriNet.Clear();
        }

        private void FindItemInfo(object sender, Core.Events.ShowInfoEventArgs e)
        {
            switch (e.Type)
            {
                case Core.Events.ShowInfoEventArgs.ItemType.State:
                    ShowStateInfoForm(_petriNet.States[e.Id]);
                    break;
                case Core.Events.ShowInfoEventArgs.ItemType.Transition:
                    ShowTransitionInfoForm(_petriNet.Transitions[e.Id]);
                    break;
                case Core.Events.ShowInfoEventArgs.ItemType.Marker:
                    ShowMarkerInfoForm(_petriNet.Markers[e.Id]);
                    break;
            }
        }

        private void ShowStateInfoForm(PetriNet.StateWrapper state)
        {
            if (state == null)
            {
                MessageBox.Show("Error: State with this id not found!");
            }
            else
            {
                dlgShowItemInfo.Hide();
                dlgStateInfo.ShowDialog(state);
            }
        }

        private void ShowTransitionInfoForm(PetriNet.TransitionWrapper transition)
        {
            if (transition == null)
            {
                MessageBox.Show("Error: Transition with this id not found!");
            }
            else
            {
                dlgShowItemInfo.Hide();
                dlgTransitionInfo.ShowDialog(transition);
            }
        }

        private void ShowMarkerInfoForm(PetriNet.MarkerInfo marker)
        {
            if (marker.Id < 0)
            {
                MessageBox.Show("Error: Marker with this id not found!");
            }
            else
            {
                dlgShowItemInfo.Hide();
                dlgMarkerInfo.ShowDialog(marker.Id, marker.StateId, "");
                //dlgMarkerInfo.ShowDialog(marker.Id, marker.StateId,
                //    Core.PetriNetItemInfo.GetMarkerTypeName(marker.Type));
            }
        }

        private void OpenMoveRulesDialog()
        {
            //dlgRules.ShowDialog();
        }

        private void OpenPrevAccumulateRulesDialog()
        {
            //
        }

        private void OpenNextAccumulateRulesDialog()
        {
            //
        }
    }
}
