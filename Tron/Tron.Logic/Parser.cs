using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron.Logic
{
    public class Parser
    {
       
        private string _fileContent;
       
        public Parser(string fileContent)
        {
            _fileContent = fileContent;

        }

        public List<string> PlayersFromFile()
        {
            List<string> split  = _fileContent.Split('|').ToList();
            _fileContent = split[1];
            List<string> players = split[0].Split(';').ToList();
            return players;
        }

        public List<string> TurnsFromFile()
        {
            List<string> playerMoves = _fileContent.Split(',').ToList();
            return playerMoves;
        }


       
        //Todo se va a ir a la mierda 9:56 pm
    
    }
}
