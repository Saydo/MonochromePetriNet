using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container.Rules
{
    public abstract class MarkerTransitionRule
    {
        public int Id { get; private set; }
        public int Priority { get; set; }
        public int UpdatedMarkerCount { get; set; }
        public int NewMarkerCount { get; set; }
        public List<MarkerIdConvert> IdConvertRules;
        public MarkerIdConvert RestMarkerIdConvert;

        public MarkerTransitionRule(int id, int updatedMarkerCount, int newMarkerCount, int priority = 1)
        {
            Id = id;
            Priority = priority;
            UpdatedMarkerCount = updatedMarkerCount;
            NewMarkerCount = newMarkerCount;
            IdConvertRules = new List<MarkerIdConvert>();
            RestMarkerIdConvert = new MarkerIdConvert(IdConvertType.Move, 0);
        }

        public bool Equals(MarkerTransitionRule rule)
        {
            return (this.UpdatedMarkerCount == rule.UpdatedMarkerCount);
        }

        public bool IsFit(StateWrapper state)
        {
            return (state.Markers.Count >= this.UpdatedMarkerCount);
        }

        protected bool Transit(IdGenerator idGenerator, StateWrapper outputState, out List<int> restMarkers, out List<int> updatedMarkers, out List<int> newMarkers)
        {
            updatedMarkers = new List<int>();
            newMarkers = new List<int>();
            if ((outputState == null) || (this.UpdatedMarkerCount > outputState.Markers.Count))
            {
                restMarkers = outputState.Markers;
                return false;
            }
            restMarkers = new List<int>();
            // Update Specified Markers Id
            int i;
            for (i = 0; i < this.IdConvertRules.Count; ++i)
            {
                updatedMarkers.Add(outputState.Markers[i]);
                switch (this.IdConvertRules[i].ConvertType)
                {
                case IdConvertType.Move:
                    newMarkers.Add(outputState.Markers[i]);
                    break;
                case IdConvertType.Delete:
                    break;
                case IdConvertType.New:
                    newMarkers.Add(idGenerator.Next());
                    break;
                }
                for (int j = 0; j < this.IdConvertRules[i].NewMarkerCount; ++j)
                {
                    newMarkers.Add(idGenerator.Next());
                }
            }
            // Update Rest Markers
            for (; i < this.UpdatedMarkerCount; ++i)
            {
                updatedMarkers.Add(outputState.Markers[i]);
                switch (this.RestMarkerIdConvert.ConvertType)
                {
                case IdConvertType.Move:
                    newMarkers.Add(outputState.Markers[i]);
                    break;
                case IdConvertType.Delete:
                    break;
                case IdConvertType.New:
                    newMarkers.Add(idGenerator.Next());
                    break;
                }
                for (int j = 0; j < this.RestMarkerIdConvert.NewMarkerCount; ++j)
                {
                    newMarkers.Add(idGenerator.Next());
                }
            }
            // define rest markers
            for (; i < outputState.Markers.Count; ++i)
            {
                restMarkers.Add(outputState.Markers[i]);
            }
            // create new markers id
            for (i = 0; i < this.NewMarkerCount; ++i)
            {
                newMarkers.Add(idGenerator.Next());
            }
            return true;
        }
    }
}
