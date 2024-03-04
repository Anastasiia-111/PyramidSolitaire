namespace pyramid
{
    public class SelectionParams
    {
        public bool IsSpareCard { get; private set; }

        public int IndexX { get; private set; }
        public int IndexY { get; private set; }


        public SelectionParams(bool isSpareCard, int indexX = 0, int indexY = 0)
        {
            IsSpareCard = isSpareCard;
            IndexX = indexX;
            IndexY = indexY;
        }

        public override string ToString()
        {
            return $"{nameof(IsSpareCard)} = {IsSpareCard}, {nameof(IndexX)} = {IndexX}, {nameof(IndexY)} = {IndexY}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not SelectionParams selection)
                return false;

            return IndexX == selection.IndexX && IndexY == selection.IndexY && IsSpareCard == selection.IsSpareCard;
        }
    }
}
