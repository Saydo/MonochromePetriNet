namespace MonochromePetriNet.Gui.Core.Events
{
    public class PetriNetNodeEventArgs : System.EventArgs
    {
        public int Id;

        public PetriNetNodeEventArgs(int id = -1)
            : base()
        {
            Id = id;
        }
    }
}
