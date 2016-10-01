using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public interface ITransitionStorage
    {
        int SelectedTransitionCount { get; }
        TransitionWrapper this[int id] { get; }
        TransitionWrapper Add(GraphicsItems.RectangleGraphicsItem item);
        void RemoveSelectedTransitions();
        List<TransitionWrapper> Find(int x, int y);
        List<TransitionWrapper> Find(int x, int y, int w, int h,
            GraphicsItems.OverlapType overlap = GraphicsItems.OverlapType.Partial);
        TransitionWrapper GetSelectedTransition(int index);
        TransitionWrapper GetSelectedTransitionById(int id);
        void ForEachTransition(MonochromePetriNet.ForEachTransitionFunction function);
        void ForEachSelectedTransition(MonochromePetriNet.ForEachTransitionFunction function);
        void Select();
        void SelectArea(int x, int y);
        void SelectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap);
        void Deselect();
        void DeselectArea(int x, int y);
        void DeselectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap);
        void Move(int dx, int dy);
        bool Move(int dx, int dy, int id);
        int Count { get; }
        bool IsExist(int id);
        bool Remove(int id);
        void Clear();
    }
}
