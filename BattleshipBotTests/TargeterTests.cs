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
    public class TargeterTests
    {
        [TestMethod()]
        public void TargeterTest()
        {
            Map map = new Map();
            Random r = new Random();
            Targeter T = new Targeter(map, r);
            //Assert.IsTrue(T.hitChart[2, 2] == Targeter.fired.unknown);

        }



        [TestMethod()]
        public void findNewShipTest()
        {
            Map map = new Map();
            Random r = new Random();
            Targeter T = new Targeter(map, r);
            Assert.IsTrue(T.findNewShip(2)[0] >= 0);
            Assert.IsTrue(T.findNewShip(2)[0] < 10);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        map.addBlockedSpace(i, j);
                    }

                }
            }
            try { T.findNewShip(2); }
            catch { return; }
            Assert.Fail();
        }

        [TestMethod()]
        public void GetNextTargetTest()
        {


        }

        private static bool HitShip(int[] fired)
        {
            for (int i = 0; i < 5; i++)
            {
                if (fired[0] == i && fired[1] == 6)
                {
                    return true;
                }
            }
            for (int i = 8; i < 10; i++)
            {
                if (fired[0] == i && fired[1] == 6)
                {
                    return true;
                }
            }
            return false;
        }


        [TestMethod()]
        public void GetLongestShipLengthNotFoundTest()
        {
            Map map = new Map();
            Targeter T = new Targeter(map, new Random());
            Assert.IsTrue(map.GetLongestShipLengthNotFound() == 5);
            Coordinate c = new Coordinate(0, 0, 0);
            map.addShip(c, 5);
            Assert.IsTrue(map.GetLongestShipLengthNotFound() == 4);
            map.addShip(c, 3);
            Assert.IsTrue(map.GetLongestShipLengthNotFound() == 4);
            map.addShip(c, 3);
            map.addShip(c, 4);
            Assert.IsTrue(map.GetLongestShipLengthNotFound() == 2);
        }
    }
}