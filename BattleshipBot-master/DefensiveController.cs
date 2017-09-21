using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public enum DefensiveStrategy { Diversion, Mixed, Edge, Avoid, Shield, SemiRandom, Uniform }
    public class DefensiveController
    {
        
        private DefensiveStrategy defensiveStrategy;
        private int matchNumber = 0;

        public DefensiveStrategy GetDefensiveStrategy()
        {
            matchNumber++;
            defensiveStrategy = DefensiveStrategy.Uniform;
            if (matchNumber > 1)
            {
                defensiveStrategy = DefensiveStrategy.Shield;
            }
            if (matchNumber > 10)
            {
                defensiveStrategy = DefensiveStrategy.Avoid;
            }
            if (matchNumber > 20)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }

            if (matchNumber > 40)
            {
                defensiveStrategy = DefensiveStrategy.Diversion;
            }

            if (matchNumber > 50)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }


            if (matchNumber > 70)
            {
                defensiveStrategy = DefensiveStrategy.Shield;
            }
            if (matchNumber > 80)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }
            if (matchNumber > 85)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }
            if (matchNumber > 95)
            {
                defensiveStrategy = DefensiveStrategy.Edge;
            }

            return defensiveStrategy;
            
        }



    }
}
