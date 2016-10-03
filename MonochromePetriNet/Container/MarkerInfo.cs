namespace MonochromePetriNet.Container
{
    public struct MarkerInfo
    {
        public int Id;
        public int StateId;

        public MarkerInfo(int id, int stateId)
        {
            Id = id;
            StateId = stateId;
        }
    }
}
