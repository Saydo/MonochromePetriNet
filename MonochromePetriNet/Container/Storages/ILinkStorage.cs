using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public enum LinkDirection { FromStateToTransition, FromTransitionToState };

    public interface ILinkStorage
    {
        int Count { get; }
        int SelectedLinkCount { get; }
        LinkWrapper this[int id] { get; }
        GraphicsItems.LinkGraphicsItem GetItem(int id);
        bool Contains(int stateId, int transitionId);
        bool Contains(int stateId, int transitionId, LinkDirection direction);
        int GetCount(int stateId, int transitionId);
        LinkWrapper Add(int stateId, int transitionId, LinkDirection direction);
        bool Remove(int id);
        bool Remove(int stateId, int transitionId);
        bool Remove(int stateId, int transitionId, LinkDirection direction);
        void RemoveSelectedLinks();
        void Clear();
        List<LinkWrapper> Find(int x, int y);
        List<LinkWrapper> Find(int x, int y, LinkDirection direction);
        List<LinkWrapper> Find(int x, int y, int w, int h,
            GraphicsItems.OverlapType overlap = GraphicsItems.OverlapType.Partial);
        List<LinkWrapper> Find(int x, int y, int w, int h, LinkDirection direction,
            GraphicsItems.OverlapType overlap = GraphicsItems.OverlapType.Partial);
        LinkWrapper GetSelectedLink(int index);
        LinkWrapper GetSelectedLinkById(int id);
        void ForEachLink(MonochromePetriNet.ForEachLinkFunction function);
        void ForEachSelectedLink(MonochromePetriNet.ForEachLinkFunction function);
        void Select();
        void SelectArea(int x, int y);
        void SelectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap);
        void Select(int stateId, int transitionId);
        void Select(int stateId, int transitionId, LinkDirection direction);
        void Deselect();
        void DeselectArea(int x, int y);
        void DeselectArea(int x, int y, int w, int h, GraphicsItems.OverlapType overlap);
        void Deselect(int stateId, int transitionId);
        void Deselect(int stateId, int transitionId, LinkDirection direction);
        void Move(int dx, int dy);
        bool Move(int dx, int dy, int id);
    }
}
