using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container.Rules
{
    public delegate void AccumulateMarkersAction(TransitedMarkersInfo markersInfo, StateWrapper state);

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
            var markersInfo = this.Transit(idGenerator, state);
            if (markersInfo == null)
            {
                return false;
            }
            AccumulateFunction(markersInfo, state);
            return true;
        }
    }
}