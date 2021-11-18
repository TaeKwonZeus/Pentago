namespace Engine
{
    public class Move
    {
        public Move(Color color, Square placedPiece, Quadrant rotatedQuadrant, bool rotatedRight)
        {
            Color = color;
            PlacedPiece = placedPiece;
            RotatedQuadrant = rotatedQuadrant;
            RotatedRight = rotatedRight;
        }

        public Color Color { get; }
        public Square PlacedPiece { get; }
        public Quadrant RotatedQuadrant { get; }
        public bool RotatedRight { get; }
    }
}