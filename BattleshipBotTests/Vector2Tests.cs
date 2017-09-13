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
    public class Vector2Tests
    {
       

        [TestMethod()]
        public void ToArrayTest()
        {

            Vector2 v1 = new Vector2(-3, 4);
            int[] ints = v1.ToArray();
            Assert.IsTrue(ints[0] == -3);
            Assert.IsTrue(ints[1] == 4);
        }
    }
}