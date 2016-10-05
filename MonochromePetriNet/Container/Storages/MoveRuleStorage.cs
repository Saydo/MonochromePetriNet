using System;
using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public partial class MonochromePetriNet
    {
        public IMoveRuleStorage MoveRules;
        private MoveRuleStorage _moveRules;

        private class MoveRuleStorage : IMoveRuleStorage
        {
            public List<Rules.MoveRule> Rules;
            private MonochromePetriNet _parent;

            public MoveRuleStorage(MonochromePetriNet parent)
            {
                _parent = parent;
                Rules = new List<Rules.MoveRule>();
            }

            public int Count { get { return Rules.Count; } }

            public Rules.MoveRule this[int index]
            {
                get { return Rules[index]; }
            }

            public bool Add(Rules.MoveRule rule)
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

            public Rules.MoveRule Find(int id)
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

            public void Move(StateWrapper outputState, StateWrapper inputState, TransitionWrapper transition)
            {
                for (int i = 0; i < Rules.Count; ++i)
                {
                    while (Rules[i].Move(_parent._idGenerator, outputState, inputState, transition));
                }
            }

            public TransitedMarkersInfo ForeMove(StateWrapper outputState)
            {
                TransitedMarkersInfo markersInfo = new TransitedMarkersInfo();
                foreach (int m in outputState.Markers)
                {
                    markersInfo.RestMarkers.Add(m);
                }
                for (int i = 0; i < Rules.Count; ++i)
                {
                    while (Rules[i].Transit(_parent._idGenerator, markersInfo)) ;
                }
                return markersInfo;
            }

            private int GetNewRuleIndex(Rules.MoveRule rule)
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
