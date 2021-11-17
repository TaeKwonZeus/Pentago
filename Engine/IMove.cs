namespace Engine
{
    public interface IMove
    {
        Color Color { get; }
        
        Square PlacedPiece { get; }
        
        Quadrant RotatedQuadrant { get; }
        
        bool RotatedRight { get; }
    }
}