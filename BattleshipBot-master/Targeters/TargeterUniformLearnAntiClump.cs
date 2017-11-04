using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterUniformLearnAntiClump : TargeterUniformLearn
    {
        public TargeterUniformLearnAntiClump(Map map, Random random, AdvEnemyShipValueCalc enemyShipCalculatorMem, CoordinateValues initalCoordinateValuesOnly) :
            base(map, random, enemyShipCalculatorMem, initalCoordinateValuesOnly)
        {
            
        }

        protected override void createCoordValueHolder(CoordinateValues initalCoordinateValueOnly)
        {
            base.coordValueHolder = new AntiClumpCoordinateValues(base.map,base.AESCV,initalCoordinateValueOnly);
        }
    }
}
