using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships.Player.Interface;
using BattleshipBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot.Tests
{
    [TestClass()]
    public class IGridConversionsTests
    {
        [TestMethod()]
        public void numToCharTest()
        {
            Assert.IsTrue(IGridConversions.numToChar(3) == 'C');
        }

        [TestMethod()]
        public void charToNumTest()
        {
            Assert.IsTrue(IGridConversions.charToNum('C') == 3);
        }

        [TestMethod()]
        public void intsToGridTest()
        {
            int[] ints = new int[2] { 3, 3 };
            GridSquare Grid = IGridConversions.intsToGrid(ints);
            Assert.IsTrue(Grid.Row=='D');
            Assert.IsTrue(Grid.Column == 4);
        }

        [TestMethod()]
        public void GridToIntsTest()
        {
            GridSquare Grid = new GridSquare('D', 4);
            int[] ints = IGridConversions.GridToInts(Grid);
            Assert.IsTrue(ints[0] == 3);
            Assert.IsTrue(ints[1] == 3);
        }
    }
}