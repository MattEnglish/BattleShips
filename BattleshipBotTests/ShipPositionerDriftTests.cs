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
    public class ShipPositionerDriftTests
    {
        [TestMethod()]
        public void GetMarchCoordinatesTest()
        {
            ShipPositionerDrift.GetMarchCoordinates(0);
        }
    }
}