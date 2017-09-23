using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleshipBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot.Tests
{
    [TestClass()]
    public class MapTests
    {
        private void AddShips(Map map)
        {

            Coordinate c1 = new Coordinate(0, 0, 0);
            Coordinate c2 = new Coordinate(7, 7, 1);
            Coordinate c3 = new Coordinate(5, 5, 0);
            map.addShip(c1, 5);
            map.addShip(c2, 3);
            map.addShip(c3, 5);
        }


        [TestMethod()]
        public void GetShipsTest()
        {
            Map map = new Map();
            AddShips(map);
            Ship[] ships = map.GetShips();
            System.Diagnostics.Trace.WriteLine(ships[0].coordinate.GetOrientation());
            Assert.IsTrue(ships[1].coordinate.GetRow() == 7);
            Assert.IsTrue(ships[1].coordinate.GetColumn() == 7);
            Assert.IsTrue(ships[1].coordinate.GetOrientation() == 1);
            Assert.IsTrue(ships.Length == 3);
        }


        [TestMethod()]
        public void GetHitSpacesTest()
        {
            Map map = new Map();
            map.shotFired(true, 3, 3);
            map.shotFired(false, 4, 5);
            /*
            Assert.IsTrue(map.GetHitSpaces()[3, 3] == 2);
            Assert.IsTrue(map.GetHitSpaces()[4,5] == 1);
            Assert.IsTrue(map.GetHitSpaces()[5, 5] == 0);
            */
        }

        [TestMethod()]
        public void GetHitSpaceTest()
        {
            Map map = new Map();
            map.shotFired(true, 3, 3);
            map.shotFired(false, 4, 5);
            Assert.IsTrue(map.GetHitSpace(3, 3) == 2);
            Assert.IsTrue(map.GetHitSpace(4, 5) == 1);
            Assert.IsTrue(map.GetHitSpace(5, 5) == 0);
        }


        [TestMethod()]
        public void GetOccupiedSpacesTest()
        {
            Map map = new Map();
            AddShips(map);
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(map.GetOccupiedSpaces()[i, 0]);
            }
            Assert.IsFalse(map.GetOccupiedSpaces()[5, 0]);

            for (int i = 0; i < 3; i++)
            {
                Assert.IsTrue(map.GetOccupiedSpaces()[7, 7 + i]);
            }
            Assert.IsFalse(map.GetOccupiedSpaces()[7, 6]);

            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(map.GetOccupiedSpaces()[5 + i, 5]);
            }


        }

        [TestMethod()]
        public void GetBlockedSpacesTest()
        {
            Map map = new Map();
            map.addBlockedSpace(0, 0);
            map.shotFired(false, 3, 3);
            map.shotFired(true, 5, 4);
            map.addOccupiedSpace(6, 6);
            map.addShip(new Coordinate(8, 8, 0), 2);


            bool[,] blockedSpaces;
            blockedSpaces = map.GetBlockedSpaces();
            Assert.IsTrue(blockedSpaces[0, 0]);
            Assert.IsTrue(blockedSpaces[3, 3]);
            Assert.IsTrue(blockedSpaces[5, 4]);
            Assert.IsTrue(blockedSpaces[6, 6]);
            Assert.IsTrue(blockedSpaces[8, 8]);
            Assert.IsTrue(blockedSpaces[9, 8]);
            Assert.IsFalse(blockedSpaces[7, 8]);

        }

        [TestMethod()]
        public void GetUnfoundShipsLengthsTest()
        {
            var map = new Map();
            var x = map.GetUnfoundShipsLengths();
            Assert.AreEqual(5, map.GetUnfoundShipsLengths().Count);
            map.addShip(new Coordinate(0, 0, 0), 3);
            x = map.GetUnfoundShipsLengths();
            Assert.AreEqual(4, map.GetUnfoundShipsLengths().Count);
            map.addShip(new Coordinate(0, 0, 0), 3);
            x = map.GetUnfoundShipsLengths();
            Assert.AreEqual(3, map.GetUnfoundShipsLengths().Count);
            map.addShip(new Coordinate(0, 0, 0), 5);
            x = map.GetUnfoundShipsLengths();
            Assert.AreEqual(2, map.GetUnfoundShipsLengths().Count);
        }
    }
}