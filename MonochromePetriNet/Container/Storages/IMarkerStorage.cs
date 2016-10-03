namespace MonochromePetriNet.Container
{
    public interface IMarkerStorage
    {
        MarkerInfo this[int id] { get; }
        bool Add(int stateId, GraphicsItems.RoundGraphicsItem item);
        bool RemoveFromState(int stateId);
        bool Move(int markerId, int oldStateId, int newStateId);
        bool MoveAll(int oldStateId, int newStateId);
        void MoveByRules();
        void ForEachMarker(MonochromePetriNet.ForEachMarkerFunction function);
        int Count { get; }
        bool IsExist(int id);
        bool Remove(int id);
        void Clear();
    }
}
