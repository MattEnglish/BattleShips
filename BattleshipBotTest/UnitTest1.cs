using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipBot;
using Battleships.Player.Interface;


namespace BattleshipBotTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestnumToChar()
        {
            Assert.IsTrue('A' == IGridConversions.numToChar(1));
        }
        
        [TestMethod]
        public void TestGetShips()
        {
            int counter = 0;
            METest ME = new METest();
            foreach (IShipPosition sp in ME.GetShipPositions())
            {
                counter++;
            }
            Assert.IsTrue(counter == 5);

            List<int> rowElementsInCoordinates = new List<int>();
            foreach (IShipPosition sp in ME.GetShipPositions())
            {
                Assert.IsTrue((int)sp.StartingSquare.Row>64);
                Assert.IsTrue((int)sp.StartingSquare.Row < 76);
                Assert.IsTrue((int)sp.EndingSquare.Row > 64);
                Assert.IsTrue((int)sp.EndingSquare.Row < 76);
                Assert.IsTrue((int)sp.StartingSquare.Column > 0);
                Assert.IsTrue((int)sp.StartingSquare.Column < 12);
                Assert.IsTrue((int)sp.EndingSquare.Column > 0);
                Assert.IsTrue((int)sp.EndingSquare.Column < 12);
            }
            

        }
        

        [TestMethod]
        public void TestShipsAreViable()
        {
            int counter = 0;
            METest ME = new METest();
            foreach (IShipPosition sp in ME.GetShipPositions())
            {
                counter++;
            }
            Assert.IsTrue(counter == 5);
        }
        
    }
}
