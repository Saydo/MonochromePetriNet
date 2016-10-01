using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container.Rules
{
    public delegate void AccumulateMarkersAction(List<int> restMarkers, List<int> updatedMarkers,
        List<int> newMarkers, StateWrapper state);

    public sealed class AccumulateRule : MarkerTransitionRule
    {
        public AccumulateMarkersAction AccumulateFunction;

        public AccumulateRule(int id, int updatedMarkerCount, int newMarkerCount = 0, int priority = 1)
            : base(id, updatedMarkerCount, newMarkerCount, priority)
        {
        }

        public bool Accumulate(IdGenerator idGenerator, StateWrapper state)
        {
            if (state == null)
            {
                return false;
            }
            List<int> restMarkers, updatedMarkers,newMarkers;
            if (!this.Transit(idGenerator, state, out restMarkers, out updatedMarkers, out newMarkers))
            {
                return false;
            }
            AccumulateFunction(restMarkers, updatedMarkers, newMarkers, state);
            return true;
        }
    }
}