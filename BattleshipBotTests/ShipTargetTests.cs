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
    public class ShipTargetTests
    {
        private ShipTarget GetTestShipTarget(Map map)
        {

            return new ShipTarget(map, 3, 5);
        }
        [TestMethod()]
        public void ShipTargetTest()
        {
            Map map = new Map();
            GetTestShipTarget(map);
        }

        [TestMethod()]
        public void spaceAtEndOfHitsIsUnknownTest()
        {
            Map map = new Map();
            map.shotFired(true, 3, 5);
            ShipTarget shipTarget = new ShipTarget(map, 3, 5);
            Assert.IsTrue(shipTarget.spaceAtEndOfHitsIsUnknown(shipTarget.GetFirstShotPos(), direction.up));
            map.shotFired(false, 4, 5);
            Assert.IsFalse(shipTarget.spaceAtEndOfHitsIsUnknown(shipTarget.GetFirstShotPos(), direction.up));
            map.shotFired(true, 2, 5);
            Assert.IsTrue(shipTarget.spaceAtEndOfHitsIsUnknown(shipTarget.GetFirstShotPos(), direction.down));
            map.shotFired(true, 1, 5);
            map.shotFired(true, 0, 5);
            Assert.IsFalse(shipTarget.spaceAtEndOfHitsIsUnknown(shipTarget.GetFirstShotPos(), direction.down));

        }

        [TestMethod()]
        public void spaceAtEndOfHitsTest()
        {
            Map map = new Map();
            map.shotFired(true, 3, 5);
            ShipTarget shipTarget = new ShipTarget(map, 3, 5);
            Vector2 space = shipTarget.spaceAtEndOfHits(shipTarget.GetFirstShotPos(), direction.left);
            Assert.IsTrue(space == new Vector2(3, 4));
            map.shotFired(true, 3, 4);
            space = shipTarget.spaceAtEndOfHits(shipTarget.GetFirstShotPos(), direction.left);
            Assert.IsTrue(space == new Vector2(3, 3));
            map.shotFired(false, 2, 5);
            space = shipTarget.spaceAtEndOfHits(shipTarget.GetFirstShotPos(), direction.down);
            Assert.IsTrue(space == new Vector2(2, 5));
        }

        [TestMethod()]
        public void GetOrientationTest()
        {
            Map map = new Map();
            ShipTarget shipTarget = new ShipTarget(map, 3, 5);
            Assert.IsTrue(shipTarget.GetOrientation() == Orientation.unknown);

            map.shotFired(true, 4, 5);
            Assert.IsTrue(shipTarget.GetOrientation() == Orientation.vertical);

        }

        [TestMethod()]
        public void GetCurrentShipTest()
        {
            Map map = new Map();
            ShipTarget shipTarget = new ShipTarget(map, 3, 5);
            map.shotFired(true, 3, 5);
            map.shotFired(true, 2, 5);
            map.shotFired(true, 1, 5);
            map.shotFired(true, 0, 5);
            map.shotFired(false, 4, 5);
            Assert.IsTrue(shipTarget.isDestroyed());
            Assert.IsTrue(shipTarget.GetCurrentShip().shipLength == 4);

            map = new Map();
            shipTarget = new ShipTarget(map, 4, 7);
            map.shotFired(false, 4, 5);
            map.shotFired(true, 4, 6);
            map.shotFired(true, 4, 7);
            map.shotFired(true, 4, 8);
            map.shotFired(true, 4, 9);
            Assert.IsTrue(shipTarget.isDestroyed());
            Assert.IsTrue(shipTarget.GetCurrentShip().shipLength == 4);
        }

        [TestMethod()]
        public void isDestroyedTest()
        {
            Map map = new Map();
            ShipTarget shipTarget = new ShipTarget(map, 3, 5);
            map.shotFired(true, 3, 5);
            map.shotFired(true, 2, 5);
            map.shotFired(true, 1, 5);
            map.shotFired(true, 0, 5);
            map.shotFired(false, 4, 5);
            Assert.IsTrue(shipTarget.isDestroyed());

            map = new Map();
            shipTarget = new ShipTarget(map, 1, 5);
                map.shotFired(true, 2, 5);
            map.shotFired(true, 1, 5);
            map.shotFired(true, 0, 5);
            Assert.IsFalse(shipTarget.isDestroyed());

            map = new Map();
            shipTarget = new ShipTarget(map, 3, 5);
            map.shotFired(true, 3, 5);
            map.shotFired(true, 2, 5);
            map.shotFired(true, 1, 5);
            Assert.IsFalse(shipTarget.isDestroyed());
            map.shotFired(true, 0, 5);            
            map.shotFired(true, 4, 5);
            Assert.IsTrue(shipTarget.isDestroyed());
            map.addShip(new Coordinate(0, 0, 0), 4);
            map.addShip(new Coordinate(0, 0, 0), 5);
            shipTarget = new ShipTarget(map, 5, 6);
            map.shotFired(true, 5, 7);
            Assert.IsFalse(shipTarget.isDestroyed());
            map.shotFired(true, 5, 8);
            
            
            Assert.IsTrue(shipTarget.isDestroyed());
        }

        [TestMethod()]
        public void GetShipLengthTest()
        {
            Map map = new Map();
            ShipTarget shipTarget = new ShipTarget(map, 2, 5);
            map.shotFired(true, 3, 5);
            map.shotFired(true, 2, 5);
            map.shotFired(true, 1, 5);
            map.shotFired(true, 0, 5);
            map.shotFired(false, 4, 5);
            Assert.IsTrue(shipTarget.GetShipLength(Orientation.vertical,new Vector2(0,5)) == 4);

            map = new Map();
            shipTarget = new ShipTarget(map, 4, 7);
            map.shotFired(false, 4, 5);
            map.shotFired(true, 4, 6);
            map.shotFired(true, 4, 7);
            map.shotFired(true, 4, 8);
            map.shotFired(true, 4, 9);
            Assert.IsTrue(shipTarget.GetShipLength(Orientation.horizontal, new Vector2(4, 6)) == 4);

        }
    }
}