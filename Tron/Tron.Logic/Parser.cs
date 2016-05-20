using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron.Logic
{
    public class Parser
    {
        private readonly List<Player> _players;
        private readonly string _fileContent; 
        public Parser(string fileContent)
        {
            _fileContent = fileContent;
            _players = new List<Player>();
        }

        private void AddPlayer(string name)
        {
            _players.Add(new Player(name));
        }

        private void InstantiatePlayers(List<string> players)
        {
            foreach (var player in players)
            {
                AddPlayer(player);
            }
        }

        private Player GetCurrentPlayer(string playerName)
        {
            foreach (var player in _players)
            {
                if (playerName.Equals(player.Name))
                    return player;
            }
            throw new Exception("Jugador no especificado en archivo");
        }
        public List<Turn> Parse()
        {
            List<string> players = _fileContent.Split('|').ToList()[0].Split(';').ToList();
            InstantiatePlayers(players);
            List<Turn> turns = new List<Turn>();
            List<string> playerMoves = _fileContent.Split('|').ToList()[1].Split(',').ToList();

            foreach (var elem in playerMoves)
            {
                var turnParts = elem.Split(':');
                turns.Add(new Turn(GetCurrentPlayer(turnParts[0]), turnParts[1]));
            }
            return turns;


        }
    }
}
