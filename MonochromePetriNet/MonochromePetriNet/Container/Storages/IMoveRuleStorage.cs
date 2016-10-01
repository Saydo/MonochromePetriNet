namespace MonochromePetriNet.Container
{
    public interface IMoveRuleStorage
    {
        Rules.MoveRule this[int index] { get; }
        int Count { get; }
        bool Add(Rules.MoveRule rule);
        bool Remove(int id);
        void Clear();
        Rules.MoveRule Find(int id);
        void Move(StateWrapper outputState, StateWrapper inputState, TransitionWrapper transition);
    }
}
