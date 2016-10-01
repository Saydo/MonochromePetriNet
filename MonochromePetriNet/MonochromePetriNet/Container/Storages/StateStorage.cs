using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public partial class MonochromePetriNet
    {
        public IStateStorage States;
        private StateStorage _states;
        public delegate void ForEachStateFunction(StateWrapper state);

        private class StateStorage : IStateStorage
        {
            public List<StateWrapper> States;
            public List<StateWrapper> SelectedStates;
            private MonochromePetriNet _parent;

            public StateStorage(MonochromePetriNet parent)
            {
                _parent = parent;
                States = new List<StateWrapper>();
                SelectedStates = new List<StateWrapper>();
            }

            public int Count { get { return States.Count; } }
            public int SelectedStateCount { get { return SelectedStates.Count; } }

            public StateWrapper this[int id]
            {
                get
                {
                    for (int i = 0; i < States.Count; ++i)
                    {
                        if (States[i].Id == id)
                        {
                            return States[i];
                        }
                    }
                    return null;
                }
            }

            public bool IsExist(int id)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].Id == id)
                    {
                        return true;
                    }
                }
                return false;
            }

            public StateWrapper Add(GraphicsItems.RoundGraphicsItem item, int markerGraphicsId, int markerRadius)
            {
                if (ReferenceEquals(item, null) || IsExist(item.Id))
                {
                    return null;
                }
                if (_parent._idGenerator.CurrentId < item.Id)
                {
                    _parent._idGenerator.Reset(item.Id);
                }
                var state = new StateWrapper(item, markerGraphicsId, markerRadius);
                States.Add(state);
                return state;
            }

            public bool Remove(int id)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].Id == id)
                    {
                        PrepareStateToRemove(States[i]);
                        States.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }

            public void RemoveSelectedStates()
            {
                for (int i = SelectedStates.Count - 1; i >= 0; --i)
                {
                    Remove(SelectedStates[i].Id);
                }
            }

            public void Clear()
            {
                _parent._links.Clear();
                SelectedStates.Clear();
                States.Clear();
            }

            public List<StateWrapper> Find(int x, int y)
            {
                var foundStates = new List<StateWrapper>();
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].State.IsCollision(x, y))
                    {
                        foundStates.Add(States[i]);
                    }
                }
                return foundStates;
            }

            public List<StateWrapper> Find(int x, int y, int w, int h,
                GraphicsItems.OverlapType overlap = GraphicsItems.OverlapType.Partial)
            {
                var foundStates = new List<StateWrapper>();
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].State.IsCollision(x, y, w, h, overlap))
                    {
                        foundStates.Add(States[i]);
                    }
                }
                return foundStates;
            }

            public StateWrapper GetSelectedState(int index)
            {
                return SelectedStates[index];
            }

            public StateWrapper GetSelectedStateById(int id)
            {
                for (int i = 0; i < SelectedStates.Count; ++i)
                {
                    if (SelectedStates[i].Id == id)
                    {
                        return SelectedStates[i];
                    }
                }
                return null;
            }

            public void ForEachState(ForEachStateFunction function)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    function(States[i]);
                }
            }

            public void ForEachSelectedState(ForEachStateFunction function)
            {
                for (int i = 0; i < SelectedStates.Count; ++i)
                {
                    function(SelectedStates[i]);
                }
            }

            public void Select()
            {
                SelectedStates.Clear();
                for (int i = 0; i < States.Count; ++i)
                {
                    States[i].State.Select();
                    SelectedStates.Add(States[i]);
                }
            }

            public void SelectArea(int x, int y)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if ((!States[i].State.IsSelected()) && States[i].State.IsCollision(x, y))
                    {
                        States[i].State.Select();
                        SelectedStates.Add(States[i]);
                    }
                }
            }

            public void SelectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if ((!States[i].State.IsSelected()) &&
                        States[i].State.IsCollision(x, y, w, h, overlap))
                    {
                        States[i].State.Select();
                        SelectedStates.Add(States[i]);
                    }
                }
            }

            public void Deselect()
            {
                SelectedStates.Clear();
                for (int i = 0; i < States.Count; ++i)
                {
                    States[i].State.Deselect();
                }
            }

            public void DeselectArea(int x, int y)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].State.IsSelected() && States[i].State.IsCollision(x, y))
                    {
                        States[i].State.Deselect();
                        SelectedStates.Remove(States[i]);
                    }
                }
            }

            public void DeselectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].State.IsSelected()
                        && States[i].State.IsCollision(x, y, w, h, overlap))
                    {
                        States[i].State.Deselect();
                        SelectedStates.Remove(States[i]);
                    }
                }
            }

            public void Move(int dx, int dy)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    MoveState(dx, dy, States[i]);
                }
            }

            public bool Move(int dx, int dy, int id)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].Id == id)
                    {
                        MoveState(dx, dy, States[i]);
                        return true;
                    }
                }
                return false;
            }

            #region Marker Storage Functions
            public MarkerInfo GetMarker(int id)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    for (int j = 0; j < States[i].Markers.Count; ++j)
                    {
                        if (States[i].Markers[j] == id)
                        {
                            return new MarkerInfo(id, States[i].Id);
                        }
                    }
                }
                return new MarkerInfo(-1, -1);
            }

            public bool IsMarkerExist(int id)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    for (int j = 0; j < States[i].Markers.Count; ++j)
                    {
                        if (States[i].Markers[j] == id)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            public int GetMarkerCount()
            {
                int counter = 0;
                for (int i = 0; i < States.Count; ++i)
                {
                    counter += States[i].Markers.Count;
                }
                return counter;
            }

            public bool AddMarker(int stateId, int markerId)
            {
                var state = this[stateId];
                if (ReferenceEquals(state, null))
                {
                    return false;
                }
                if (_parent._idGenerator.CurrentId < markerId)
                {
                    _parent._idGenerator.Reset(markerId);
                }
                state.AddMarker(markerId);
                return true;
            }

            public bool RemoveMarker(int id)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].RemoveMarker(id))
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool RemoveMarkersFromState(int stateId)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    if (States[i].Id == stateId)
                    {
                        States[i].ClearMarkers();
                        return true;
                    }
                }
                return false;
            }

            public void ClearMarkers()
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    States[i].ClearMarkers();
                }
            }

            public bool MoveMarker(int markerId, int oldStateId, int newStateId)
            {
                var oldState = this[oldStateId];
                if (ReferenceEquals(oldState, null))
                {
                    return false;
                }
                var newState = this[newStateId];
                if (ReferenceEquals(newState, null))
                {
                    return false;
                }
                for (int i = 0; i < oldState.Markers.Count; ++i)
                {
                    if (oldState.Markers[i] == markerId)
                    {
                        newState.AddMarker(markerId);
                        oldState.RemoveMarkerAt(i);
                        return true;
                    }
                }
                return false;
            }

            public bool MoveAllMarkers(int oldStateId, int newStateId)
            {
                var oldState = this[oldStateId];
                if (ReferenceEquals(oldState, null))
                {
                    return false;
                }
                var newState = this[newStateId];
                if (ReferenceEquals(newState, null))
                {
                    return false;
                }
                for (int i = 0; i < oldState.Markers.Count; ++i)
                {
                    newState.AddMarker(oldState.Markers[i]);
                }
                oldState.ClearMarkers();
                return true;
            }

            public void MoveMarkersByRules()
            {
                TransitionWrapper transition;
                StateWrapper state;
                var prevAccRules = _parent._prevAccumulateRules;
                var nextAccRules = _parent._nextAccumulateRules;
                var moveRules = _parent._moveRules;
                // sequence: accumulate(prev) -> move -> accumulate(next)
                for (int i = 0; i < States.Count; ++i)
                {
                    prevAccRules.Accumulate(States[i]);
                    for (int j = 0; j < States[i].OutputLinks.Count; ++j)
                    {
                        transition = States[i].OutputLinks[j].Transition;
                        for (int k = 0; k < transition.OutputLinks.Count; ++k)
                        {
                            state = transition.OutputLinks[k].State;
                            moveRules.Move(States[i], state, transition);
                            nextAccRules.Accumulate(state);
                        }
                    }
                }
            }

            public void ForEachMarker(ForEachMarkerFunction function)
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    for (int j = 0; j < States[i].Markers.Count; ++j)
                    {
                        function(new MarkerInfo(States[i].Markers[j], States[i].Id));
                    }
                }
            }
            #endregion

            #region Helpful Functions
            public int GetSelectedStateIndex(StateWrapper state)
            {
                for (int i = 0; i < SelectedStates.Count; ++i)
                {
                    if (SelectedStates[i] == state)
                    {
                        return i;
                    }
                }
                return -1;
            }

            public void RemoveAllLinks()
            {
                for (int i = 0; i < States.Count; ++i)
                {
                    States[i].InputLinks.Clear();
                    States[i].OutputLinks.Clear();
                }
            }

            public void PrepareStateToRemove(StateWrapper state)
            {
                RemoveLinks(state);
                int index = GetSelectedStateIndex(state);
                if (index >= 0)
                {
                    SelectedStates.RemoveAt(index);
                }
            }

            public void RemoveLinks(StateWrapper state)
            {
                for (int i = state.InputLinks.Count - 1; i >= 0; --i)
                {
                    _parent._links.Remove(state.InputLinks[i].Id);
                }
                for (int i = state.OutputLinks.Count - 1; i >= 0; --i)
                {
                    _parent._links.Remove(state.OutputLinks[i].Id);
                }
            }

            public void Draw(System.Drawing.Graphics graphics)
            {
                /*
                for (int i = 0; i < States.Count; ++i)
                {
                    States[i].Draw(graphics);
                }
                */
            }
            #endregion
        }
    }
}
