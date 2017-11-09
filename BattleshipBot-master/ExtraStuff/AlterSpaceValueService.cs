using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class AlterSpaceValueService
    {

        
        public static double[,] alterSpaceValues(double[,] spaceValues, double[,,]coordinateValues, Vector2 missPos, Map map, List<Vector2> otherMissesNotOnMap, int unfoundShipLength)
        {
            int col = missPos.y;
            var newSpaceValues = spaceValues;
            var sLegal = new shipLegal();
            int row;
            for (row = missPos.x - unfoundShipLength + 1; row <= missPos.x; row++)
            {
                Ship s = new Ship(new Coordinate(row, col, 0), unfoundShipLength);
                if (sLegal.isShipLegal(map,s,otherMissesNotOnMap))
                    {
                    newSpaceValues = AlterSpaceValues(newSpaceValues, coordinateValues[row, col, 0], s);
                }
            }

            row = missPos.x;
            for (col = missPos.y - unfoundShipLength + 1; col <= missPos.y; col++)
            {
                Ship s = new Ship(new Coordinate(row, col, 1), unfoundShipLength);
                if (sLegal.isShipLegal(map, s, otherMissesNotOnMap))
                {
                    newSpaceValues = AlterSpaceValues(newSpaceValues, coordinateValues[row, col, 1], s);
                }
            }
            return newSpaceValues;



        }

        private static double[,] AlterSpaceValues(double[,] spaceValues, double coordinateValue, Ship ship)
        {
            var newSpaceValues = spaceValues;
            int xS, yS, xE, yE;
            xS = ship.coordinate.GetRow();
            yS = ship.coordinate.GetColumn();
            int ori = ship.coordinate.GetOrientation();
            int shipLength = ship.shipLength;
            if (ori == 0)
            {
                xE = xS + shipLength - 1;
                yE = yS;
            }
            else
            {
                xE = xS;
                yE = yS + shipLength - 1;
            }

            for(int row = xS; row <= xE; row++)
            {
                for (int col = yS; col <=yE; col++)
                {
                    newSpaceValues[row, col] -= coordinateValue;
                }
            }

            return newSpaceValues;

        }
        
    }
}
