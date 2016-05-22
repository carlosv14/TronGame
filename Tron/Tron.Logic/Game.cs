using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tron.Test;

namespace Tron.Logic
{
    public class Game
    {
        private readonly List<Coordinates> _playerInitialPosition;
        private readonly List<Player> _players;
        private readonly List<Turn> _turns;
        private readonly Parser _parser;
        private readonly Dictionary<string, Delegate> _movement;
        public Game()
        {
            _movement = new Dictionary<string, Delegate>();
            _playerInitialPosition = new List<Coordinates>
            {
                new Coordinates(0, 0),
                new Coordinates(19, 19),
                new Coordinates(0, 19),
                new Coordinates(19, 0)
            };
            IActionsFile actionsFile = new ActionsFile("moves.txt");
            _parser = new Parser(actionsFile.Read());
            _players = new List<Player>();
            InitializeMovements();
        }

        private void InitializeMovements()
        {
            _movement["L"]= new Action<Player>(MoveLeft);
            _movement["R"] = new Action<Player>(MoveRight);
            _movement["U"] = new Action<Player>(MoveUp);
            _movement["D"] = new Action<Player>(MoveDown);
        }
        public void AddPlayer(string name, int playerNumber)
        {
            _players.Add(new Player(name, _playerInitialPosition[playerNumber]));
        }
        public void InstantiatePlayers(List<string> players)
        {
            for (var i = 0; i < players.Count; i++)
            {
                AddPlayer(players[i], i);
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

        public List<Turn> SetTurns()
        {

            var playerMoves = _parser.TurnsFromFile();
            List<Turn> turns = new List<Turn>();
            foreach (var elem in playerMoves)
            {
                turns.Add(SetTurn(elem));
            }
            return turns;
        }

        public Turn SetTurn(string playerMoves)
        {
            var turnParts = playerMoves.Split(':');
          return (new Turn(GetCurrentPlayer(turnParts[0]), turnParts[1]));
        }
    
        public void Run()
        {
            foreach (var turn in _turns)
            {
                Move(turn);
            }
        }

        public void MoveRight(Player player)
        {
            player.Coordinates.XPosition++;
        }
        private static void MoveLeft(Player player)
        {
            player.Coordinates.XPosition--;
        }

        private static void MoveUp(Player player)
        {
            player.Coordinates.YPosition--;
        }
        private static void MoveDown(Player player)
        {
            player.Coordinates.YPosition++;
        }
        public void Move(Turn turn)
        {
            var player = turn.Player;
            _movement[turn.Movement].DynamicInvoke(player);
        }

    }
}
