using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    /*
    public class TargeterUniform : Targeter
    {
        public TargeterUniform(Map map, Random random) : base(map, random)
        {
            base.map = map;
            this.random = random;
            lastShotColumn = 0;
            lastShotRow = 0;
        }

        public override int[] findNewShipM(int shipLength)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
     
            var uniformConfigs = new MoreUniformConfigs();
            var coordValuesAsThoughNoShipsAreThere = uniformConfigs.GetInitalUniformCoordsValueKinda(shipLength, new Map());//WARNING LOOK INTO THIS!!!!!!!!
            var spaceValues = uniformConfigs.GetSpaceValueSumofCoordValues(,shipLength);

            int[] target = new int[2] {0, 0};
            double largestValue = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    double thisValue = spaceValues[row,column];
                    if (thisValue >= largestValue)
                    {
                        largestValue = thisValue;
                        target[0] = row;
                        target[1] = column;
                    }
                }
            }
            return target;
        }
    }
    */
}
