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

        private List<Square> Board { get; }

        private Color ToMove { get; }
    }
}