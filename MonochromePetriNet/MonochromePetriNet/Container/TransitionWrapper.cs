using System.Collections.Generic;

namespace MonochromePetriNet.Container
{
    public class TransitionWrapper
    {
        public int Id;
        public GraphicsItems.RectangleGraphicsItem Transition;
        public List<LinkWrapper> OutputLinks;
        public List<LinkWrapper> InputLinks;

        public TransitionWrapper(GraphicsItems.RectangleGraphicsItem transition)
        {
            Id = transition.Id;
            Transition = transition;
            OutputLinks = new List<LinkWrapper>();
            InputLinks = new List<LinkWrapper>();
        }
    }
}
