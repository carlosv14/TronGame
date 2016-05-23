using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Tron.Logic;

namespace Tron.Test
{
    [Binding]
    public class TronSteps
    {
        private IActionsFile _file;
        private string _fileContent;
        private readonly Game _game = new Game();

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
        //*************************************************************

  
        private Turn _turn;
        private string _movement;
        private string _playerName;
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
        //***************************************************************************

        private int _x;
        private int _y;
        private string[] _coordinateData;
        private Player _looser;
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
            _looser = _game.FindLooser(_game.GetCurrentPlayer(_playerName), new Coordinates(_x, _y));
        }

        [Then(@"'(.*)' loses the game")]
        public void ThenLosesTheGame(string p0)
        {
            Assert.AreEqual(p0,_looser.Name);
        }

    }
}