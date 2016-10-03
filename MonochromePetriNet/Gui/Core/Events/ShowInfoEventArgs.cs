namespace MonochromePetriNet.Gui.Core.Events
{
    public class ShowInfoEventArgs : System.EventArgs
    {
        public enum ItemType { State, Transition, Marker };

        public int Id;
        public ItemType Type;

        public ShowInfoEventArgs(int id = -1)
            : base()
        {
            Id = id;
            Type = ItemType.State;
        }

        public ShowInfoEventArgs(int id, ItemType type)
            : base()
        {
            Id = id;
            Type = type;
        }
    }
}
