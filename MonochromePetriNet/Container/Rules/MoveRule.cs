using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container.Rules
{
    public delegate void MoveMarkersAction(TransitedMarkersInfo markersInfo, StateWrapper outputState, StateWrapper inputState,
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
            var markerInfo = this.Transit(idGenerator, outputState);
            if (markerInfo == null)
            {
                return false;
            }
            MoveFunction(markerInfo, outputState, inputState, transition);
            return true;
        }
    }
}
