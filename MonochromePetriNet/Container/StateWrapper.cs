using System.Drawing;
using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public class StateWrapper
    {
        public int Id;
        public GraphicsItems.RoundGraphicsItem State;
        public List<LinkWrapper> OutputLinks;
        public List<LinkWrapper> InputLinks;
        private GraphicsItems.RoundGraphicsItem _markerGraphics;
        private List<int> _markers;
        private Font textFont;
        private Brush textBrush;

        public StateWrapper(GraphicsItems.RoundGraphicsItem state)
        {
            Id = state.Id;
            State = state;
            _markerGraphics = null;
            _markers = new List<int>();
            OutputLinks = new List<LinkWrapper>();
            InputLinks = new List<LinkWrapper>();

            textFont = new Font("Arial", 10);
            textBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        }

        public GraphicsItems.RoundGraphicsItem MarkerGraphics
        {
            get { return _markerGraphics; }
            set
            {
                _markerGraphics = value;
                _markerGraphics.Center = State.Center;
            }
        }
        public List<int> Markers { get { return _markers; } }

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

        public void Draw(Graphics graphics)
        {
            State.Draw(graphics);
            if ((_markerGraphics != null) && (_markers.Count > 0))
            {
                _markerGraphics.Center = State.Center;
                _markerGraphics.Draw(graphics);
                if (_markers.Count > 1)
                {
                    graphics.DrawString(_markers.Count.ToString(), textFont,
                        textBrush, _markerGraphics.Center.X, _markerGraphics.Center.Y);
                }
            }
        }
    }
}
