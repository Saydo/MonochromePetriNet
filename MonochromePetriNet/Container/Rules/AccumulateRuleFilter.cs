using System.Collections.Generic;

namespace MonochromePetriNet.Container.Rules
{
    public enum FilterAction { Pass, Refuse };

    public struct MoveFilterInfo
    {
        public int Id;
        public FilterAction Action;

        public MoveFilterInfo(int id, FilterAction action)
        {
            Id = id;
            Action = action;
        }
    }

    public struct AccumulateRuleFilter
    {
        public int RuleId;
        List<MoveFilterInfo> MoveFilters;
    }
}
