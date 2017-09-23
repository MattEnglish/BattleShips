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
    public class UtilityFunctionsTests
    {
        [TestMethod()]
        public void getRandomTrueCoordinateTest()
        {
            bool[,,] bools = new bool[3, 3, 3];
            bools[0, 1, 2] = true;
            Random r = new Random();
            Coordinate c = UtilityFunctions.getRandomTrueCoordinate(bools, r);
            Assert.IsTrue(c.GetRow() == 0 && c.GetColumn() == 1 && c.GetOrientation() == 2);

        }

        [TestMethod()]
        public void getWeightedRandomTrueCoordinateTest()
        {
            int[,,] ints = new int[3, 3, 3];
            ints[0, 1, 2] = 1;
            Random r = new Random();
            Coordinate c = UtilityFunctions.getWeightedRandomTrueCoordinate(ints, r);
            Assert.IsTrue(c.GetRow() == 0 && c.GetColumn() == 1 && c.GetOrientation() == 2);
            ints[2, 2, 2] = 50000;
            c = UtilityFunctions.getWeightedRandomTrueCoordinate(ints, r);
            Assert.IsTrue(c.GetRow() == 2 && c.GetColumn() == 2 && c.GetOrientation() == 2);
        }

        [TestMethod()]
        public void GetInverseWeightedRandomCoordinateTest()
        {
            double[,,] doubles = new double[3, 3, 3];
            doubles[0, 1, 2] = 1;
            Random r = new Random();
            Coordinate c = UtilityFunctions.GetInverseWeightedRandomCoordinate(doubles, r);
            Assert.IsTrue(c.GetRow() == 0 && c.GetColumn() == 1 && c.GetOrientation() == 2);
            doubles[2, 2, 2] = 10000;
            c = UtilityFunctions.GetInverseWeightedRandomCoordinate(doubles, r);
            Assert.IsTrue(c.GetRow() == 0 && c.GetColumn() == 1 && c.GetOrientation() == 2);
            doubles[0, 1, 2] = 10;
            doubles[2, 2, 2] = 0.0002d;
            c = UtilityFunctions.GetInverseWeightedRandomCoordinate(doubles, r);
            Assert.IsTrue(c.GetRow() == 2 && c.GetColumn() == 2 && c.GetOrientation() == 2);
        }

        [TestMethod()]
        public void GetAllShipSymmetriesTest()
        {
            var ship = new Ship(new Coordinate (0, 1, 0), 3);
            var ships = UtilityFunctions.GetAllShipSymmetries(ship);
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate (0, 1, 0)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(1, 0, 1)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(7, 1, 0)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(8, 0, 1)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(1, 7, 1)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(0, 8, 0)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(7, 8, 0)));
            Assert.IsTrue(ships.Any(x => x.coordinate == new Coordinate(8, 7, 1)));
        }
    }
}