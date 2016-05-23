﻿using System.Collections.Generic;
using System.Linq;

namespace Tron.Logic
{
    public class Parser : IParser
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
    }
}
