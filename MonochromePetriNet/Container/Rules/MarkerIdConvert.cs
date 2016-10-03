namespace MonochromePetriNet.Container.Rules
{
    public enum IdConvertType { New, Move, Delete };

    public struct MarkerIdConvert
    {
        public IdConvertType ConvertType;
        public int NewMarkerCount;

        public MarkerIdConvert(IdConvertType convertType, int newMarkerCount)
        {
            ConvertType = convertType;
            NewMarkerCount = newMarkerCount;
        }
    }
}
