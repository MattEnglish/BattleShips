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
    public class CoordinateCovarianceTests
    {
        [TestMethod()]
        public void GetNumberOfTimesShipHasBeenInAConfiguarationWithAsWellAsAShipInCurrentConfigForEachShipinCurrentConfigBLAHTest()
        {
            var cc = new CoordinateCovariance();
            var ships = new List<Ship>();
            var ships2 = new List<Ship>();
            ships.Add(new Ship(new Coordinate(3, 3, 0), 5));
            ships.Add(new Ship(new Coordinate(3, 5, 0), 4));
            ships.Add(new Ship(new Coordinate(3, 5, 1), 3));
            ships2.Add(new Ship(new Coordinate(3, 5, 0), 4));
            ships2.Add(new Ship(new Coordinate(0, 0, 0), 3));
            ships2.Add(new Ship(new Coordinate(6, 6, 0), 5));
            for (int i = 0; i < 100; i++)
            {
                cc.AddNewConfig(ships);
            }
            //cc.AddNewConfig(ships2);
            var x = cc.GetNumberOfTimesShipHasBeenInAConfiguarationWithAsWellAsAShipInCurrentConfigForEachShipinCurrentConfigBLAH(ships, 3);
            Assert.AreEqual(x[3, 5, 1], 300);
            var y = cc.GetNumberOfTimesShipHasBeenInAConfiguarationWithAsWellAsAShipInCurrentConfigForEachShipinCurrentConfigBLAH(ships2, 3);
            Assert.AreEqual(y[3, 5, 1], 100);
            Assert.AreEqual(y[0, 0, 0], 0);
            cc.AddNewConfig(ships);

        }
    }
}