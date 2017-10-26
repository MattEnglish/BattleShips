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
    public class ShipTargeterTests
    {
        [TestMethod()]
        public void GetNextShotPositionTest()
        {
            Map map = new Map();
            

            map.shotFired(true, 0, 4);
            map.shotFired(false, 0, 3);
            map.shotFired(false, 1, 4);
            ShipTarget shipTarget = new ShipTarget(map, 0, 4);
            ShipTargeter ST = new ShipTargeter(map, shipTarget);
            Assert.IsTrue(ST.GetNextShotmusthandleminus1()==new Vector2(0,5));
            map.shotFired(true, 0, 5);
            Assert.IsTrue(ST.GetNextShotmusthandleminus1() == new Vector2(0, 6));
            map.shotFired(false, 0, 6);

        }
        

        [TestMethod()]
        public void GetOrientationFindingShotTest()
        {
            Map map = new Map();
            map.shotFired(true, 9, 4);
            map.shotFired(false, 8, 4);
            map.shotFired(false, 9, 5);
            ShipTarget shipTarget = new ShipTarget(map, 9, 4);
            ShipTargeter ST = new ShipTargeter(map, shipTarget);
            Vector2 v = ST.GetOrientationFindingShot();
            Assert.IsTrue(v == new Vector2(9, 3));
            Assert.IsTrue(shipTarget.GetOrientation() == Orientation.unknown);
            map.shotFired(true, 9, 3);
            Assert.IsTrue(shipTarget.GetOrientation() == Orientation.horizontal);
        }

        [TestMethod()]
        public void GetAlongShipShotTest()
        {
            Map map = new Map();
            map.shotFired(true, 9, 4);
            map.shotFired(false, 8, 4);
            map.shotFired(false, 9, 5);
            map.shotFired(true, 9, 3);
            ShipTarget shipTarget = new ShipTarget(map, 9, 4);
            ShipTargeter ST = new ShipTargeter(map, shipTarget);
            Vector2 space = ST.GetAlongShipShot(Orientation.horizontal);
            Assert.IsTrue(space == new Vector2(9, 2));
        }
    }
}