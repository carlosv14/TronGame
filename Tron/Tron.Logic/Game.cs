using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Tron.Logic
{
    public class Game
    {
        private readonly List<Coordinates> _playerInitialPosition;
        private readonly List<Player> _players;
        private readonly List<Turn> _turns;
        private readonly Parser _parser;
        private readonly Dictionary<string, Delegate> _movement;
        public Board Board;
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
            Board = new Board();
            IActionsFile actionsFile = new ActionsFile("moves.txt");
            _parser = new Parser(actionsFile.Read());
            _players = new List<Player>();
            _turns = new List<Turn>();
            InitializeMovements();
            InstantiatePlayers();
            Board.PrintBoard();
            Thread.Sleep(1000);
            SetTurns();
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
            Board.AddPiece(_playerInitialPosition[playerNumber],name);
            
        }
        public void InstantiatePlayers()
        {
            List<string> players = _parser.PlayersFromFile();
            for (var i = 0; i < players.Count; i++)
            {
                AddPlayer(players[i], i);
            }
        }

        public Player GetCurrentPlayer(string playerName)
        {
            foreach (var player in _players)
            {
                if (playerName.Equals(player.Name))
                    return player;
            }
            throw new UnspecifiedPlayerException("Jugador no especificado en archivo");
        }

        public void SetTurns()
        {

            var playerMoves = _parser.TurnsFromFile();
           foreach (var elem in playerMoves)
            {
                _turns.Add(SetTurn(elem));
            }
            
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
                if (EndGame(turn.Player, turn.Player.GetLastCoordinate()))
                {
                    Console.WriteLine("The Winner is "+ _players.First().Name);
                }
            }
        }

        private static void MoveRight(Player player)
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
            
           if(Console.OpenStandardInput(1) != Stream.Null)
                 Console.Clear();
            var player = turn.Player;
            _movement[turn.Movement].DynamicInvoke(player);
            var coordinate = new Coordinates(player.Coordinates.XPosition,
                player.Coordinates.YPosition);
            player.AddCoordinateToPath(coordinate);
            Board.AddPiece(coordinate,player.Name);
            Board.PrintBoard();
            Thread.Sleep(1000);
        }

        public bool EndGame(Player currentPlayer, Coordinates searchedCoordinates)
        {
            Player looser = FindLoser(currentPlayer, searchedCoordinates);
            if (looser != null)
                _players.Remove(looser);
            return (_players.Count == 1);
        }
        public Player FindLoser(Player currentPlayer, Coordinates searchedCoordinates)
        {
            foreach (var player in _players)
            {
                if (player != currentPlayer)
                {   
                    if(player.FindCoordinateInPath(searchedCoordinates)!=null)
                        return currentPlayer;
                }
            }
            return null;
            
        }

    }
}
