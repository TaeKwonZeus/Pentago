using System;
using System.Linq;

namespace Engine
{
    public class Board
    {
        public Square[][] board { get; private set; } = new Square[4][];

        public Board()
        {
            for (var i = 0; i < 4; i++)
            {
                var quad = new Square[9];
                for (var j = 0; j < 9; j++)
                {
                    quad[j] = new Square((j % 3) + ((i % 2)) * 3,
                        (int) (Math.Floor((float) j / 3) + Math.Floor((float) i / 2) * 3));
                }

                board[i] = quad;
            }
        }

        public Square[][] MakeMove(Move move)
        {
            foreach (var square in board[move.RotateIndex])
            {
                square.Rotate(move.Clockwise);
            }

            board[(int) (Math.Floor((double) move.PlacedPiece.X / 3) + Math.Floor((double) move.PlacedPiece.Y / 3))]
                [move.PlacedPiece.X % 3 + move.PlacedPiece.Y % 3 * 3].State = move.Color;
            
            return board;
        }
    }
}