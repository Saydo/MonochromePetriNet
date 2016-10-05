using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public struct UpdatedMarkersInfo
    {
        public List<int> RestMarkers;
        public List<int> UpdatedMarkers;

        public UpdatedMarkersInfo(List<int> restMarkers, List<int> updatedMarkers)
        {
            RestMarkers = restMarkers;
            UpdatedMarkers = updatedMarkers;
        }
    }

    public class TransitedMarkersInfo
    {
        public List<int> RestMarkers;
        public List<int> UpdatedMarkers;
        public List<int> NewMarkers;

        public TransitedMarkersInfo()
        {
            RestMarkers = new List<int>();
            UpdatedMarkers = new List<int>();
            NewMarkers = new List<int>();
        }

        public TransitedMarkersInfo(List<int> restMarkers, List<int> updatedMarkers, List<int> newMarkers)
        {
            RestMarkers = restMarkers;
            UpdatedMarkers = updatedMarkers;
            NewMarkers = newMarkers;
        }
    }

    public struct MovedMarkersInfo
    {
        public List<UpdatedMarkersInfo> StatesMarkers;
        public List<int> NewMarkers;

        public MovedMarkersInfo(List<UpdatedMarkersInfo> statesMarkers, List<int> newMarkers)
        {
            StatesMarkers = statesMarkers;
            NewMarkers = newMarkers;
        }
    }

    // for each transition
    /*
    public struct TransitMarkersInfo
    {
        // TransitionWrapper transition; // find transition from _transitions by index
        //List<System.Tuple<List<int> rest, List<int> updated>> stateMarkers; // find state by index in input links
        List<int> newMarkers;
    }
    */

    /*
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
    */
}
