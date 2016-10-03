using System.Collections.Generic;

namespace MonochromePetriNet.Gui.Core.Events
{
    public class StateEventArgs : PetriNetNodeEventArgs
    {
        public List<int> Markers;

        public StateEventArgs(int id = -1, int typeId = -1)
            : base(id)
        {
            Markers = new List<int>();
        }

        public StateEventArgs(int id, List<int> markersList)
            : base(id)
        {
            Id = id;
            Markers = markersList;
        }
    }
}
