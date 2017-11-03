using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterUniformLearnAntiClump : TargeterUniformLearn
    {
        public TargeterUniformLearnAntiClump(Map map, Random random, AdvEnemyShipValueCalc enemyShipCalculatorMem) :
            base(map, random, enemyShipCalculatorMem)
        {
            
        }

        protected override void createCoordValueHolder()
        {
            base.coordValueHolder = new AntiClumpCoordinateValues(base.map,base.AESCV);
        }
    }
}
