using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Tron.Logic;
using Tron.Logic.Interfaces;

namespace Tron.Test
{
    [Binding]
    public class TronSteps
    {
               
        private readonly Board _board = new Board();
        private readonly Game _game = new Game();
        private IActionsFile _file;
        private Turn _turn;
        private Player _looser;
        private Coordinates _coordinates;
        private string[] _coordinateData;
        private string _fileContent;
        private string _movement;
        private string _playerName;
        private string _piece;
        private int _x;
        private int _y;
       

        [Given(@"I have a file named '(.*)'")]
        public void GivenIHaveAFileNamed(string p0)
        {
            _file = new ActionsFile(p0);
        }

        [When(@"I read the file")]
        public void WhenIReadTheFile()
        {
            _fileContent = _file.Read();
        }
        
        [Then(@"contents will be '(.*)'")]
        public void ThenContentsWillBe(string p0)
        {
            Assert.AreEqual(_fileContent, p0);
        }

        [Given(@"I read the player '(.*)'")]
        public void GivenIReadThePlayer(string p0)
        {
           
            _playerName = p0;
           _game.AddPlayer(p0, 0);
            
        }

        [Given(@"I read the movement '(.*)'")]
        public void GivenIReadTheMovement(string p0)
        {
            _movement = p0;
        }

        [When(@"I generate the turn")]
        public void WhenIGenerateTheTurn()
        {
            _turn = _game.SetTurn(_movement);
        }

        [Then(@"The result will be")]
        public void ThenTheResultWillBe(Table table)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                if (table.Rows[i].Values.ToList()[0] != _turn.Player.Name
                    || table.Rows[i].Values.ToList()[1] != _turn.Movement)
                {
                    throw new Exception("Movement parser failed");
                }    
            }
        }

        [When(@"my current position is (.*) and the movement is performed")]
        public void WhenMyCurrentPositionIsAndTheMovementIsPerformed(string p0)
        {
            _coordinateData = p0.Split(' ');
            _x = int.Parse(_coordinateData[0]);
            _y = int.Parse(_coordinateData[1]);
            var player = _game.GetCurrentPlayer(_playerName);
            player.Coordinates.XPosition = _x;
            player.Coordinates.YPosition = _y;
            _turn = _game.SetTurn(_movement);
            _game.Move(_turn);
        }

        [Then(@"My new position will be (.*)")]
        public void ThenMyNewPositionWillBe(string p0)
        {
            _coordinateData = p0.Split(' ');
            string value = _turn.Player.Coordinates.XPosition  + " " + _turn.Player.Coordinates.YPosition;
            Assert.AreEqual(p0,value);              
        }

        [Given(@"he has been in the position (.*)")]
        public void GivenHeHasBeenInThePosition(string p0)
        {
            Player player = _game.GetCurrentPlayer(_playerName);
            _coordinateData = p0.Split(' ');
            _x = int.Parse(_coordinateData[0]);
            _y = int.Parse(_coordinateData[1]);
            player.AddCoordinateToPath(new Coordinates(_x,_y));
        }

        [Given(@"he is or has been in the same positon")]
        public void GivenHeIsOrHasBeenInTheSamePositon()
        {
            Player player = _game.GetCurrentPlayer(_playerName);
            player.AddCoordinateToPath(new Coordinates(_x, _y));
        }

        [When(@"the turn has ended")]
        public void WhenTheTurnHasEnded()
        {
            _looser = _game.FindLoser(_game.GetCurrentPlayer(_playerName), new Coordinates(_x, _y));
        }

        [Then(@"'(.*)' loses the game")]
        public void ThenLosesTheGame(string p0)
        {
            Assert.AreEqual(p0,_looser.Name);
        }

        [Given(@"I search the player '(.*)'")]
        public void GivenISearchThePlayer(string p0)
        {
            _playerName = p0;
        }

        private string _playerError;
        [When(@"I get the current Player base on name")]
        public void WhenIGetTheCurrentPlayerBaseOnName()
        {
            try
            {              
                _game.GetCurrentPlayer(_playerName);
            }
            catch (Exception e)
            {
                _playerError = e.Message;
            }
        }

        [Then(@"the result will be '(.*)'")]
        public void ThenTheResultWillBe(string p0)
        {
            Assert.AreEqual(_playerError,p0);
        }


        [Given(@"I have the cordinates (.*) and (.*)")]
        public void GivenIHaveTheCordinatesAnd(int p0, int p1)
        {
            _coordinates = new Coordinates(p0,p1);
        }

        [Given(@"the piece name '(.*)'")]
        public void GivenThePieceName(string p0)
        {
            _piece = p0;
        }

        [When(@"I add the piece to the bord")]
        public void WhenIAddThePieceToTheBord()
        {

            _board.AddPiece(_coordinates,_piece);
        }

        [Then(@"board in corrdinates will be '(.*)'")]
        public void ThenBoardInCorrdinatesWillBe(string p0)
        {
            Assert.AreEqual(p0,_board.GetPiece(_coordinates));
        }

    }
}