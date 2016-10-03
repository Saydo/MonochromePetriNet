using System.Collections.Generic;
using System.Drawing;

namespace MonochromePetriNet.Container
{
    public partial class MonochromePetriNet
    {
        public enum ItemType { Link, Marker = 100, Transition = 200, State = 300 };
        private IdGenerator _idGenerator;

        public MonochromePetriNet()
        {
            _idGenerator = new IdGenerator(-1);

            this.States = _states = new StateStorage(this);
            this.Transitions = _transitions = new TransitionStorage(this);
            this.Markers = _markers = new MarkerStorage(this);
            this.Links = _links = new LinkStorage(this);
            this.MoveRules = _moveRules = new MoveRuleStorage(this);
            this.PrevAccumulateRules = _prevAccumulateRules = new AccumulateRuleStorage(this);
            this.NextAccumulateRules = _nextAccumulateRules = new AccumulateRuleStorage(this);
        }

        public int GenerateItemId()
        {
            return _idGenerator.Next();
        }

        public int GetLastItemId()
        {
            return _idGenerator.CurrentId;
        }

        public void Select(int x, int y)
        {
            _states.SelectArea(x, y);
            _transitions.SelectArea(x, y);
            _links.SelectArea(x, y);
        }

        public void Select(int x, int y, int w, int h, GraphicsItems.OverlapType overlap)
        {
            _states.SelectArea(x, y, w, h, overlap);
            _transitions.SelectArea(x, y, w, h, overlap);
            _links.SelectArea(x, y, w, h, overlap);
        }

        public void SelectItems()
        {
            _states.Select();
            _transitions.Select();
            _links.Select();
        }

        public void Deselect(int x, int y)
        {
            _states.DeselectArea(x, y);
            _transitions.DeselectArea(x, y);
            _links.DeselectArea(x, y);
        }

        public void Deselect(int x, int y, int w, int h, GraphicsItems.OverlapType overlap)
        {
            _states.DeselectArea(x, y, w, h, overlap);
            _transitions.DeselectArea(x, y, w, h, overlap);
            _links.DeselectArea(x, y, w, h, overlap);
        }

        public void DeselectItems()
        {
            _states.Deselect();
            _transitions.Deselect();
            _links.Deselect();
        }

        public void RemoveSelectedItems()
        {
            _links.RemoveSelectedLinks();
            _transitions.RemoveSelectedTransitions();
            _states.RemoveSelectedStates();
        }

        public void Move(int dx, int dy)
        {
            _states.Move(dx, dy);
            _transitions.Move(dx, dy);
            _links.Move(dx, dy);
        }

        public bool Move(int dx, int dy, int id)
        {
            if (_states.Move(dx, dy, id))
            {
                return true;
            }
            if (_transitions.Move(dx, dy, id))
            {
                return true;
            }
            if (_links.Move(dx, dy, id))
            {
                return true;
            }
            return false;
        }

        public void MoveSelectedItems(int dx, int dy)
        {
            var movedStates = new List<StateWrapper>(_states.SelectedStates);
            var movedTransitions = new List<TransitionWrapper>(_transitions.SelectedTransitions);
            _links.UpdateMovedItems(movedStates, movedTransitions);
            for (int i = 0; i < movedStates.Count; ++i)
            {
                MoveState(dx, dy, movedStates[i]);
            }
            for (int i = 0; i < movedTransitions.Count; ++i)
            {
                MoveTransition(dx, dy, movedTransitions[i]);
            }
        }

        public void Clear()
        {
            _states.Clear();
            _transitions.Clear();
        }

        public void Draw(Graphics graphics)
        {
            _links.Draw(graphics);
            _transitions.Draw(graphics);
            _states.Draw(graphics);
        }

        #region Helpful Functions
        private static void MoveState(int dx, int dy, StateWrapper state)
        {
            for (int i = 0; i < state.InputLinks.Count; ++i)
            {
                state.InputLinks[i].Link.MovePoint1(dx, dy);
            }
            for (int i = 0; i < state.OutputLinks.Count; ++i)
            {
                state.OutputLinks[i].Link.MovePoint1(dx, dy);
            }
            state.State.Move(dx, dy);
        }

        private static void MoveTransition(int dx, int dy, TransitionWrapper transition)
        {
            for (int i = 0; i < transition.InputLinks.Count; ++i)
            {
                transition.InputLinks[i].Link.MovePoint2(dx, dy);
            }
            for (int i = 0; i < transition.OutputLinks.Count; ++i)
            {
                transition.OutputLinks[i].Link.MovePoint2(dx, dy);
            }
            transition.Transition.Move(dx, dy);
        }

        private static void MoveLink(int dx, int dy, LinkWrapper link)
        {
            MoveTransition(dx, dy, link.Transition);
            MoveState(dx, dy, link.State);
        }

        private static void RemoveFromList<T>(List<T> list, List<int> indexList)
        {
            for (int i = indexList.Count - 1; i >= 0; --i)
            {
                list.RemoveAt(indexList[i]);
                for (int j = i - 1; j >= 0; --j)
                {
                    if (indexList[j] > indexList[i])
                    {
                        --indexList[j];
                    }
                }
            }
        }
        #endregion
    }
}
