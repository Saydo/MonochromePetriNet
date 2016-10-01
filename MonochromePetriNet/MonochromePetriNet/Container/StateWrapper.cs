using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public class StateWrapper
    {
        public int Id;
        public GraphicsItems.RoundGraphicsItem State;
        public List<LinkWrapper> OutputLinks;
        public List<LinkWrapper> InputLinks;
        public GraphicsItems.RoundGraphicsItem MarkerGraphics;
        private List<int> _markers;

        public List<int> Markers { get { return _markers; } }

        public StateWrapper(GraphicsItems.RoundGraphicsItem state, int markerGraphicsId, int markerRadius)
        {
            Id = state.Id;
            State = state;
            MarkerGraphics = new GraphicsItems.RoundGraphicsItem(markerGraphicsId, markerRadius, state.Z + 1);
            _markers = new List<int>();
            OutputLinks = new List<LinkWrapper>();
            InputLinks = new List<LinkWrapper>();
        }

        public bool AddMarker(int id)
        {
            for (int i = 0; i < _markers.Count; ++i)
            {
                if (_markers[i] == id)
                {
                    return false;
                }
            }
            _markers.Add(id);
            return true;
        }

        public void AddMarkers(List<int> listId)
        {
            int j = 0;
            for (int i = 0; i < listId.Count; ++i)
            {
                for (j = 0; j < _markers.Count; ++j)
                {
                    if (_markers[j] == listId[i])
                    {
                        break;
                    }
                }
                if (j == _markers.Count)
                {
                    _markers.Add(listId[i]);
                }
            }
        }

        public void ClearMarkers()
        {
            _markers.Clear();
        }

        public void RemoveMarkerAt(int index)
        {
            _markers.RemoveAt(index);
        }

        public bool RemoveMarker(int id)
        {
            for (int i = 0; i < _markers.Count; ++i)
            {
                if (_markers[i] == id)
                {
                    _markers.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveMarkers(List<int> listId)
        {
            for (int i = 0; i < listId.Count; ++i)
            {
                for (int j = 0; j < _markers.Count; ++j)
                {
                    if (_markers[j] == listId[i])
                    {
                        _markers.RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }
}
