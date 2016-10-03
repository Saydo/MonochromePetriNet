﻿using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public partial class MonochromePetriNet
    {
        public ITransitionStorage Transitions;
        private TransitionStorage _transitions;
        public delegate void ForEachTransitionFunction(TransitionWrapper transition);

        private class TransitionStorage : ITransitionStorage
        {
            public List<TransitionWrapper> Transitions;
            public List<TransitionWrapper> SelectedTransitions;
            private MonochromePetriNet _parent;

            public TransitionStorage(MonochromePetriNet parent)
            {
                _parent = parent;
                Transitions = new List<TransitionWrapper>();
                SelectedTransitions = new List<TransitionWrapper>();
            }

            public int Count { get { return Transitions.Count; } }
            public int SelectedTransitionCount { get { return SelectedTransitions.Count; } }

            public TransitionWrapper this[int id]
            {
                get
                {
                    for (int i = 0; i < Transitions.Count; ++i)
                    {
                        if (Transitions[i].Id == id)
                        {
                            return Transitions[i];
                        }
                    }
                    return null;
                }
            }

            public bool IsExist(int id)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Id == id)
                    {
                        return true;
                    }
                }
                return false;
            }

            public TransitionWrapper Add(GraphicsItems.RectangleGraphicsItem item)
            {
                if (ReferenceEquals(item, null) || IsExist(item.Id))
                {
                    return null;
                }
                if (_parent._idGenerator.CurrentId < item.Id)
                {
                    _parent._idGenerator.Reset(item.Id);
                }
                var transition = new TransitionWrapper(item);
                Transitions.Add(transition);
                return transition;
            }

            public bool Remove(int id)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Id == id)
                    {
                        PrepareTransitionToRemove(Transitions[i]);
                        Transitions.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }

            public void RemoveSelectedTransitions()
            {
                for (int i = SelectedTransitions.Count - 1; i >= 0; --i)
                {
                    this.Remove(SelectedTransitions[i].Id);
                }
            }

            public void Clear()
            {
                _parent._links.Clear();
                SelectedTransitions.Clear();
                Transitions.Clear();
            }

            public List<TransitionWrapper> Find(int x, int y)
            {
                var foundTransitions = new List<TransitionWrapper>();
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Transition.IsCollision(x, y))
                    {
                        foundTransitions.Add(Transitions[i]);
                    }
                }
                return foundTransitions;
            }

            public List<TransitionWrapper> Find(int x, int y, int w, int h,
                GraphicsItems.OverlapType overlap = GraphicsItems.OverlapType.Partial)
            {
                var foundTransitions = new List<TransitionWrapper>();
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Transition.IsCollision(x, y, w, h, overlap))
                    {
                        foundTransitions.Add(Transitions[i]);
                    }
                }
                return foundTransitions;
            }

            public TransitionWrapper GetSelectedTransition(int index)
            {
                return SelectedTransitions[index];
            }

            public TransitionWrapper GetSelectedTransitionById(int id)
            {
                for (int i = 0; i < SelectedTransitions.Count; ++i)
                {
                    if (SelectedTransitions[i].Id == id)
                    {
                        return SelectedTransitions[i];
                    }
                }
                return null;
            }

            public void ForEachTransition(ForEachTransitionFunction function)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    function(Transitions[i]);
                }
            }

            public void ForEachSelectedTransition(ForEachTransitionFunction function)
            {
                for (int i = 0; i < SelectedTransitions.Count; ++i)
                {
                    function(SelectedTransitions[i]);
                }
            }

            public void Select()
            {
                SelectedTransitions.Clear();
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    Transitions[i].Transition.Select();
                    SelectedTransitions.Add(Transitions[i]);
                }
            }

            public void SelectArea(int x, int y)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if ((!Transitions[i].Transition.IsSelected())
                        && Transitions[i].Transition.IsCollision(x, y))
                    {
                        Transitions[i].Transition.Select();
                        SelectedTransitions.Add(Transitions[i]);
                    }
                }
            }

            public void SelectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if ((!Transitions[i].Transition.IsSelected())
                        && Transitions[i].Transition.IsCollision(x, y, w, h, overlap))
                    {
                        Transitions[i].Transition.Select();
                        SelectedTransitions.Add(Transitions[i]);
                    }
                }
            }

            public void Deselect()
            {
                SelectedTransitions.Clear();
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    Transitions[i].Transition.Deselect();
                }
            }

            public void DeselectArea(int x, int y)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Transition.IsSelected()
                        && Transitions[i].Transition.IsCollision(x, y))
                    {
                        Transitions[i].Transition.Deselect();
                        SelectedTransitions.Remove(Transitions[i]);
                    }
                }
            }

            public void DeselectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Transition.IsSelected()
                        && Transitions[i].Transition.IsCollision(x, y, w, h, overlap))
                    {
                        Transitions[i].Transition.Deselect();
                        SelectedTransitions.Remove(Transitions[i]);
                    }
                }
            }

            public void Move(int dx, int dy)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    Transitions[i].Transition.Move(dx, dy);
                }
            }

            public bool Move(int dx, int dy, int id)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    if (Transitions[i].Id == id)
                    {
                        Transitions[i].Transition.Move(dx, dy);
                        return true;
                    }
                }
                return false;
            }

            #region Helpful Functions
            public int GetSelectedTransitionIndex(TransitionWrapper transition)
            {
                for (int i = 0; i < SelectedTransitions.Count; ++i)
                {
                    if (SelectedTransitions[i] == transition)
                    {
                        return i;
                    }
                }
                return -1;
            }

            public void RemoveAllLinks()
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    Transitions[i].InputLinks.Clear();
                    Transitions[i].OutputLinks.Clear();
                }
            }

            public void PrepareTransitionToRemove(TransitionWrapper transition)
            {
                this.RemoveLinks(transition);
                int index = GetSelectedTransitionIndex(transition);
                if (index >= 0)
                {
                    SelectedTransitions.RemoveAt(index);
                }
            }

            public void RemoveLinks(TransitionWrapper transition)
            {
                for (int i = transition.InputLinks.Count - 1; i >= 0; --i)
                {
                    _parent._links.Remove(transition.InputLinks[i].Id);
                }
                for (int i = transition.OutputLinks.Count - 1; i >= 0; --i)
                {
                    _parent._links.Remove(transition.OutputLinks[i].Id);
                }
            }

            public void Draw(System.Drawing.Graphics graphics)
            {
                for (int i = 0; i < Transitions.Count; ++i)
                {
                    Transitions[i].Transition.Draw(graphics);
                }
            }
            #endregion
        }
    }
}
