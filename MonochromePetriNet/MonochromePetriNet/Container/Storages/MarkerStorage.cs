namespace MonochromePetriNet.Container
{
    public partial class MonochromePetriNet
    {
        public IMarkerStorage Markers;
        private MarkerStorage _markers;
        public delegate void ForEachMarkerFunction(MarkerInfo info);

        private class MarkerStorage : IMarkerStorage
        {
            private MonochromePetriNet _parent;
            private StateStorage _states;

            public MarkerStorage(MonochromePetriNet parent)
            {
                _parent = parent;
                _states = _parent._states;
            }

            public int Count
            {
                get { return _states.GetMarkerCount(); }
            }

            public MarkerInfo this[int id]
            {
                get { return _states.GetMarker(id); }
            }

            public bool IsExist(int id)
            {
                return _states.IsMarkerExist(id);
            }

            public bool Add(int stateId, int markerId)
            {
                return _states.AddMarker(stateId, markerId);
            }

            public bool Remove(int id)
            {
                return _states.RemoveMarker(id);
            }

            public bool RemoveFromState(int stateId)
            {
                return _states.RemoveMarkersFromState(stateId);
            }

            public void Clear()
            {
                _states.ClearMarkers();
            }

            public bool Move(int markerId, int oldStateId, int newStateId)
            {
                return _states.MoveMarker(markerId, oldStateId, newStateId);
            }

            public bool MoveAll(int oldStateId, int newStateId)
            {
                return _states.MoveAllMarkers(oldStateId, newStateId);
            }

            public void MoveByRules()
            {
                _states.MoveMarkersByRules();
            }

            public void ForEachMarker(ForEachMarkerFunction function)
            {
                _states.ForEachMarker(function);
            }
        }
    }
}
