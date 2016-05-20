using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron.Logic
{
    public class Board
    {
        private List<Turn> _turns;
        private int _boardSize = 20;

        public Board(List<Turn> turns)
        {
            _turns = turns;
        }


        public void PerformMove()
        {
            var currentPlayer = _turns.First().Player;
            switch (_turns.First().Movement)
            {
                case "L":
                    currentPlayer.PositionX--;
                    break;
                case "R":
                    currentPlayer.PositionX++;
                    break;
                case "U":
                    currentPlayer.PositionY--;
                    break;
                case "D":
                    currentPlayer.PositionY++;
                    break;
            }
            _turns.RemoveAt(0);
        }
    }
}
