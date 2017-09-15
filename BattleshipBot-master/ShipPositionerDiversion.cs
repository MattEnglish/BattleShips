using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ShipPositionerDiversion
    {
        public List<Ship> GetShipCoordinates(Random r)
        {
            var list = new List<Ship>();
            list.Add(new Ship(new Coordinate(0,0,0), 2));
            list.Add(new Ship(new Coordinate(3, 0, 0), 3));
            list.Add(new Ship(new Coordinate(0, 2, 0), 5));
            list.Add(new Ship(new Coordinate(0, 4, 0), 4));
            list.Add(new Ship(new Coordinate(6, 8, 0), 3));
            int rand = r.Next(0, 8);
            if (rand > 3)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {

                    list[shipNum] = UtilityFunctions.reflectShipXY(list[shipNum]);
                }
            }

            if (rand%4 == 1)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {
                    
                    list[shipNum] = UtilityFunctions.reflectShipRows(list[shipNum]);
                }
            }
            else if (rand%4 == 2)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {
                    list[shipNum] = UtilityFunctions.reflectShipCols(list[shipNum]);
                    
                }
            }
            else if (rand%4 == 3)
            {
                for (int shipNum = 0; shipNum < 5; shipNum++)
                {
                    list[shipNum] = UtilityFunctions.reflectShipCols(list[shipNum]);
                    list[shipNum] = UtilityFunctions.reflectShipRows(list[shipNum]);
                }
            }
            else
            {
                
            }

            return list;
        }


    }
}
