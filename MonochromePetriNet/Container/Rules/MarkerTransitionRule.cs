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

        protected TransitedMarkersInfo Transit(IdGenerator idGenerator, StateWrapper outputState)
        {
            var markersInfo = new TransitedMarkersInfo();
            if ((outputState == null) || (this.UpdatedMarkerCount > outputState.Markers.Count))
            {
                return null;
            }
            // Update Specified Markers Id
            int i;
            for (i = 0; i < this.IdConvertRules.Count; ++i)
            {
                markersInfo.UpdatedMarkers.Add(outputState.Markers[i]);
                switch (this.IdConvertRules[i].ConvertType)
                {
                case IdConvertType.Move:
                    markersInfo.NewMarkers.Add(outputState.Markers[i]);
                    break;
                case IdConvertType.Delete:
                    break;
                case IdConvertType.New:
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                    break;
                }
                for (int j = 0; j < this.IdConvertRules[i].NewMarkerCount; ++j)
                {
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                }
            }
            // Update Rest Markers
            for (; i < this.UpdatedMarkerCount; ++i)
            {
                markersInfo.UpdatedMarkers.Add(outputState.Markers[i]);
                switch (this.RestMarkerIdConvert.ConvertType)
                {
                case IdConvertType.Move:
                    markersInfo.NewMarkers.Add(outputState.Markers[i]);
                    break;
                case IdConvertType.Delete:
                    break;
                case IdConvertType.New:
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                    break;
                }
                for (int j = 0; j < this.RestMarkerIdConvert.NewMarkerCount; ++j)
                {
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                }
            }
            // define rest markers
            for (; i < outputState.Markers.Count; ++i)
            {
                markersInfo.RestMarkers.Add(outputState.Markers[i]);
            }
            // create new markers id
            for (i = 0; i < this.NewMarkerCount; ++i)
            {
                markersInfo.NewMarkers.Add(idGenerator.Next());
            }
            return markersInfo;
        }

        public bool Transit(IdGenerator idGenerator, TransitedMarkersInfo markersInfo)
        {
            if ((markersInfo == null) || (this.UpdatedMarkerCount > markersInfo.RestMarkers.Count))
            {
                return false;
            }
            // Update Specified Markers Id
            int i;
            for (i = 0; i < this.IdConvertRules.Count; ++i)
            {
                markersInfo.UpdatedMarkers.Add(markersInfo.RestMarkers[0]);
                switch (this.IdConvertRules[i].ConvertType)
                {
                case IdConvertType.Move:
                    markersInfo.NewMarkers.Add(markersInfo.RestMarkers[0]);
                    break;
                case IdConvertType.Delete:
                    break;
                case IdConvertType.New:
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                    break;
                }
                for (int j = 0; j < this.IdConvertRules[i].NewMarkerCount; ++j)
                {
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                }
                markersInfo.RestMarkers.RemoveAt(0);
            }
            // Update Rest Markers
            for (int j = 0; i < this.UpdatedMarkerCount; ++j, ++i)
            {
                markersInfo.UpdatedMarkers.Add(markersInfo.RestMarkers[j]);
                switch (this.RestMarkerIdConvert.ConvertType)
                {
                case IdConvertType.Move:
                    markersInfo.NewMarkers.Add(markersInfo.RestMarkers[j]);
                    break;
                case IdConvertType.Delete:
                    break;
                case IdConvertType.New:
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                    break;
                }
                for (int k = 0; k < this.RestMarkerIdConvert.NewMarkerCount; ++k)
                {
                    markersInfo.NewMarkers.Add(idGenerator.Next());
                }
            }
            // create new markers id
            for (i = 0; i < this.NewMarkerCount; ++i)
            {
                markersInfo.NewMarkers.Add(idGenerator.Next());
            }
            return true;
        }
    }
}
