using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ShipPositionerSheild:ShipPositioner
    {
        public List<Ship> GetShipSheildCoordinates(Random r)
        {
            
            var list = new List<Ship>();
            int rowOffset = getRowOffset(r);
            int colOffset = getColOffset(r);

            list.Add(new Ship(new Coordinate(0+rowOffset, 0+ colOffset, 1), 4));
            list.Add(new Ship(new Coordinate(0 + rowOffset, 5+ colOffset, 0), 3));
            list.Add(new Ship(new Coordinate(4 + rowOffset, 2+ colOffset, 1), 5));
            list.Add(new Ship(new Coordinate(2 + rowOffset, 0+ colOffset, 0), 3));
            list.Add(new Ship(new Coordinate(2 + rowOffset, 2+ colOffset, 1), 2));
            int rand = r.Next(0, 8);
            if (rand > 3)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {

                    list[shipNum] = UtilityFunctions.reflectShipXY(list[shipNum]);
                }
            }

            if (rand % 4 == 1)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {

                    list[shipNum] = UtilityFunctions.reflectShipRows(list[shipNum]);
                }
            }
            else if (rand % 4 == 2)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {
                    list[shipNum] = UtilityFunctions.reflectShipCols(list[shipNum]);

                }
            }
            else if (rand % 4 == 3)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {
                    list[shipNum] = UtilityFunctions.reflectShipCols(list[shipNum]);
                    list[shipNum] = UtilityFunctions.reflectShipRows(list[shipNum]);
                }
            }


            return list;
        }
        private int getRowOffset(Random r)
        {
            var rand = r.Next(0, 3);
            if (rand % 3 == 0)
            {
                return 0;
            }
            if (rand % 3 == 1)
            {
                return 5;
            }

            return r.Next(1, 5);

        }

        private int getColOffset(Random r)
        {
            var rand = r.Next(0, 3);
            if (rand % 3 == 0)
            {
                return 0;
            }
            if (rand % 3 == 1)
            {
                return 3;
            }

            return r.Next(1, 3);

        }

    }

    
}
