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

        private Parser _parser;
        private List<Turn> turns;
        [Given(@"I read the string '(.*)'")]
        public void GivenIReadTheString(string p0)
        {
            _parser = new Parser(p0);
        }

        [When(@"I parse the movements")]
        public void WhenIParseTheMovements()
        {
            turns = _parser.Parse();
        }

        [Then(@"The result will be")]
        public void ThenTheResultWillBe(Table table)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                if (table.Rows[i].Values.ToList()[0] != turns[i].Player.Name
                    || table.Rows[i].Values.ToList()[1] != turns[i].Movement)
                {
                    throw new Exception("Movement parser failed");
                }    
            }
        }
        //***************************************************************************


        [Given(@"I have the movement '(.*)'")]
        public void GivenIHaveTheMovement(string p0)
        {
            
        }

        [Given(@"My x position is (.*)")]
        public void GivenMyXPositionIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"My y position is (.*)")]
        public void GivenMyYPositionIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the movement is performed")]
        public void WhenTheMovementIsPerformed()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"My current position will be")]
        public void ThenMyCurrentPositionWillBe(Table table)
        {
            ScenarioContext.Current.Pending();
        }


    }
}
