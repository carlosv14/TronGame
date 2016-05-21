using System;
using System.Collections.Generic;
using System.Linq;
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
        private Game _game;

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
        [Given(@"I read the player '(.*)'")]
        public void GivenIReadThePlayer(string p0)
        {
            _game = new Game();
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

        private int x;
        private int y;
        private string[] coordinateData;

        [When(@"my current position is (.*) and the movement is performed")]
        public void WhenMyCurrentPositionIsAndTheMovementIsPerformed(string p0)
        {
            coordinateData = p0.Split(' ');
            x = int.Parse(coordinateData[0]);
            y = int.Parse(coordinateData[1]);
            _turn = _game.SetTurn(_movement);
            _game.Move(_turn);
        }

        [Then(@"My new position will be (.*)")]
        public void ThenMyNewPositionWillBe(string p0)
        {
            coordinateData = p0.Split(' ');
            string value = _turn.Player.Coordinates.XPosition  + " " + _turn.Player.Coordinates.YPosition;
            Assert.AreEqual(p0,value);              
        }

    }
}