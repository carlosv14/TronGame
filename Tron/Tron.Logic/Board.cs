using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron.Logic
{
    public class Board
    {
      private readonly string[,] _board;

        public Board()
        {
            _board = new string[20,20];   
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    _board.SetValue("__", i, j);
                }
            }
        }
        public void PrintBoard()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                   Console.Write(_board[i,j]+ "  ");
                }
                Console.WriteLine(" ");
            }
        }

        public void AddPiece(Coordinates coordinates, string piece)
        {
            _board[coordinates.YPosition, coordinates.XPosition]
                = piece;
        }
    }
}
