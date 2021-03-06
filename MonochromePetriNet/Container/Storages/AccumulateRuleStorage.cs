﻿using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public partial class MonochromePetriNet
    {
        public IAccumulateRuleStorage PreviousMoveActions;
        public IAccumulateRuleStorage IntermediateMoveActions;
        public IAccumulateRuleStorage NextMoveActions;
        private AccumulateRuleStorage _previousMoveActions;
        private AccumulateRuleStorage _intermediateMoveActions;
        private AccumulateRuleStorage _nextMoveActions;

        private class AccumulateRuleStorage : IAccumulateRuleStorage
        {
            public List<Rules.AccumulateRule> Rules;
            private MonochromePetriNet _parent;

            public AccumulateRuleStorage(MonochromePetriNet parent)
            {
                _parent = parent;
                Rules = new List<Rules.AccumulateRule>();
            }

            public int Count { get { return Rules.Count; } }

            public Rules.AccumulateRule this[int index]
            {
                get { return Rules[index]; }
            }

            public bool Add(Rules.AccumulateRule rule)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    if (Rules[i].Equals(rule))
                    {
                        return false;
                    }
                }
                Rules.Insert(GetNewRuleIndex(rule), rule);               
                return true;
            }

            public bool Remove(int id)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    if (Rules[i].Id == id)
                    {
                        Rules.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }

            public void Clear()
            {
                Rules.Clear();
            }

            public Rules.AccumulateRule Find(int id)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    if (Rules[i].Id == id)
                    {
                        return Rules[i];
                    }
                }
                return null;
            }

            public void Accumulate(StateWrapper state)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    while (Rules[i].Accumulate(_parent._idGenerator, state));
                }
            }

            public TransitedMarkersInfo ForeAccumulate(StateWrapper state)
            {
                TransitedMarkersInfo markersInfo = new TransitedMarkersInfo();
                foreach (int m in state.Markers)
                {
                    markersInfo.RestMarkers.Add(m);
                }
                for (int i = 0; i < Rules.Count; ++i)
                {
                    while (Rules[i].Transit(_parent._idGenerator, markersInfo));
                }
                return markersInfo;
            }

            public void ForeAccumulate(TransitedMarkersInfo markersInfo)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    while (Rules[i].Transit(_parent._idGenerator, markersInfo)) ;
                }
            }

            private int GetNewRuleIndex(Rules.AccumulateRule rule)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    if ((Rules[i].Priority < rule.Priority)
                        || ((Rules[i].Priority == rule.Priority)
                            && (Rules[i].UpdatedMarkerCount < rule.UpdatedMarkerCount)))
                    {
                        return i;
                    }
                }
                return Rules.Count;
            }
        }
    }
}
