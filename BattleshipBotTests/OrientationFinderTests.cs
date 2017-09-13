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
    public class OrientationFinderTests
    {
        [TestMethod()]
        public void GetNextShotPosTest()
        {
            Map map = new Map();
            map.shotFired(true, 2, 1);
            ShipTarget shipTarget = new ShipTarget(map, 2, 1);
            OrientationFinder oF = new OrientationFinder(map, shipTarget);
            map.shotFired(false, 5, 1);
            Assert.IsTrue(oF.GetNextShotPos() == new Vector2(2, 2));
        }
    }
}