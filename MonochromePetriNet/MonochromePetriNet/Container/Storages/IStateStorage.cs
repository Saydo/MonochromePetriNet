using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public interface IStateStorage
    {
        int SelectedStateCount { get; }
        StateWrapper this[int id] { get; }
        StateWrapper Add(GraphicsItems.RoundGraphicsItem item, int markerGraphicsId, int markerRadius);
        void RemoveSelectedStates();
        List<StateWrapper> Find(int x, int y);
        List<StateWrapper> Find(int x, int y, int w, int h,
            GraphicsItems.OverlapType overlap = GraphicsItems.OverlapType.Partial);
        StateWrapper GetSelectedState(int index);
        StateWrapper GetSelectedStateById(int id);
        void ForEachState(MonochromePetriNet.ForEachStateFunction function);
        void ForEachSelectedState(MonochromePetriNet.ForEachStateFunction function);
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
