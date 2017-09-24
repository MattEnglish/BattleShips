using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class DefenseTestController
    {
        public DefensiveStrategy GetDefensiveStrategy(bool nothingHere, int dontcare, int whatever)
        {
            return DefensiveStrategy.Drift;
        }
    }
}
