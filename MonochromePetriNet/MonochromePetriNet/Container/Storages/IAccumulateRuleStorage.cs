namespace MonochromePetriNet.Container
{
    public interface IAccumulateRuleStorage
    {
        Rules.AccumulateRule this[int index] { get; }
        int Count { get; }
        bool Add(Rules.AccumulateRule rule);
        bool Remove(int id);
        void Clear();
        Rules.AccumulateRule Find(int id);
        void Accumulate(StateWrapper state);
    }
}
