using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public struct MovedMarkersInfo
    {
        public StateWrapper State;
        public List<int> RestMarkers;
        public List<int> UpdatedMarkers;
        public List<int> NewMarkers;

        public MovedMarkersInfo(StateWrapper state, List<int> restMarkers,
            List<int> updatedMarkers, List<int> newMarkers)
        {
            State = state;
            RestMarkers = restMarkers;
            UpdatedMarkers = updatedMarkers;
            NewMarkers = newMarkers;
        }
    }
}
