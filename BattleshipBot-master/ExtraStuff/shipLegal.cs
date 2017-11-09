using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class shipLegal
    {
        private int xS, yS, xE, yE;
        Map map;

        public bool isShipLegal(Map map, Ship ship, List<Vector2> extraMisses)
        {
            this.map = map;
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

            return ShipInBounds()
                && shipNotOnMiss(extraMisses)
                && shipNotNextToHit();
        }

        private bool ShipInBounds()
        {
            return xS >= 0
                && yS >= 0
                && xE < 10
                && yE < 10;
        }

        private bool shipNotOnMiss(List<Vector2> extraMisses)
        {
            for (int row = xS; row < xE + 1; row++)
            {
                for (int col = yS; col < yE + 1; col++)
                {
                    var v = new Vector2(row, col);
                    if (map.GetHitSpace(v) == hitSpace.miss)
                    {
                        return false;
                    }
                    if (extraMisses.Contains(v))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool shipNotNextToHit()
        {
            for (int row = xS - 1; row < xE + 2; row++)
            {
                for (int col = yS - 1; col < yE + 2; col++)
                {
                    if (Map.InBounds(row, col))
                    {
                        if (map.GetHitSpace(row, col) == 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

    }
}
