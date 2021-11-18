using System.Collections.Generic;

namespace Engine
{
    public class Position
    {
        public Position(List<Square> board, Color toMove)
        {
            Board = board;
            ToMove = toMove;
        }

        List<Square> Board { get; }

        Color ToMove { get; }
    }
}