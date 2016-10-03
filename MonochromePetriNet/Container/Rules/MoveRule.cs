using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container.Rules
{
    public delegate void MoveMarkersAction(List<int> restMarkers, List<int> updatedMarkers,
        List<int> newMarkers, StateWrapper outputState, StateWrapper inputState,
        TransitionWrapper transition);

    public sealed class MoveRule : MarkerTransitionRule
    {
        public MoveMarkersAction MoveFunction;

        public MoveRule(int id, int updatedMarkerCount, int newMarkerCount = 0, int priority = 1)
            : base(id, updatedMarkerCount, newMarkerCount, priority)
        {
        }

        public bool Move(IdGenerator idGenerator, StateWrapper outputState, StateWrapper inputState, TransitionWrapper transition)
        {
            if ((outputState == null) || (inputState == null) || (transition == null))
            {
                return false;
            }
            List<int> restMarkers, updatedMarkers, newMarkers;
            if (!this.Transit(idGenerator, outputState, out restMarkers, out updatedMarkers, out newMarkers))
            {
                return false;
            }
            MoveFunction(restMarkers, updatedMarkers, newMarkers, outputState, inputState, transition);
            return true;
        }
    }
}
