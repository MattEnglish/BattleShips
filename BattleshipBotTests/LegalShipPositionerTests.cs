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
    public class LegalShipPositionerTests
    {
        [TestMethod()]
        public void clearBoardLegalPositionTest()
        {
            Map map = new Map();
            LegalShipPositioner LSP = new LegalShipPositioner(map,2);
            for(int i = 0; i<7; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.IsTrue(LSP.getLegalPositions()[i, j, 0]);
                    Assert.IsTrue(LSP.getLegalPositions()[j, i, 1]);
                }
            }
           
        }
        [TestMethod()]
        public void blockedSpaceTest()
        {
            
            Map map = new Map();
            LegalShipPositioner LSP = new LegalShipPositioner(map,3);
            map.addBlockedSpace(4, 4);
            Assert.IsFalse(LSP.getLegalPositions()[4,4,0]);
            Assert.IsFalse(LSP.getLegalPositions()[2, 4, 0]);
            Assert.IsTrue(LSP.getLegalPositions()[1, 4, 0]);
            Assert.IsFalse(LSP.getLegalPositions()[4, 2, 1]);
        }

        [TestMethod()]
        public void numberOfLegalPosTest()
        {

            Map map = new Map();
            LegalShipPositioner LSP = new LegalShipPositioner(map , 2);
            int legalPos = LSP.getnumberOfLegalPos();
            Assert.IsTrue(legalPos == 180);

            map.addShip(new Coordinate(3, 3, 0), 3);
            legalPos = LSP.getnumberOfLegalPos();
            Assert.IsTrue(legalPos == 200 - 10 - 15 - 3 -10 - 15 - 5);
        }

    }
}