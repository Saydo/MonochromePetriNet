using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonochromePetriNet.Container.Rules
{
    public struct MarkerFilter
    {
        public int MarkerType;
        public FilterAction Action;
    }

    public struct StateFilter
    {
        public int StateType;
        public List<MarkerFilter> MarkerFilters;
    }

    public class TransitionFilter
    {
        public int TransitionType;
        public List<StateFilter> InputFilters;
        public List<StateFilter> OutputFilters;
    }
}
