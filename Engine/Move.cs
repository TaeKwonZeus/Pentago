namespace Engine
{
    public class Move
    {
        public Move(Color color, Square placedPiece, bool rotatedRight)
        {
            Color = color;
            PlacedPiece = placedPiece;
            RotatedRight = rotatedRight;
        }

        public Color Color { get; }
        public Square PlacedPiece { get; }
        public bool RotatedRight { get; }
    }
}