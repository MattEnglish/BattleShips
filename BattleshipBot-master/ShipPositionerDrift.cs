using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ShipPositionerDrift
    {
        public static List<Ship> GetMarchCoordinates(int callNumber)
        {
            var list = new List<Ship>();

            list.Add(new Ship(new Coordinate(0, loop(callNumber), 0), 3));
            list.Add(new Ship(new Coordinate(4, loop(callNumber), 0), 2));
            list.Add(new Ship(new Coordinate(7, loop(callNumber), 0), 3));
            list.Add(new Ship(new Coordinate(0, loop(callNumber + 2), 0), 5));
            list.Add(new Ship(new Coordinate(6, loop(callNumber + 2), 0), 4));

            var ships = new List<Ship>();

            if ((callNumber / 10) % 4 == 0)
            {
                ships = list;
            }
            if ((callNumber / 10) % 4 == 1)
            {
                
                foreach (var ship in list)
                {
                    ships.Add(UtilityFunctions.reflectShipXY(ship));
                }
                
            }
            if ((callNumber / 10) % 4 == 2)
            {

                foreach (var ship in list)
                {
                    ships.Add(UtilityFunctions.reflectShipCols(ship));
                }

            }

            if ((callNumber / 10) % 4 == 3)
            {

                foreach (var ship in list)
                {
                    ships.Add(UtilityFunctions.reflectShipRows(UtilityFunctions.reflectShipXY(ship)));
                }

            }

            return ships;
        }

        private static int loop(int x)
        {
            if(x>9)
            {
                return loop(x - 10);
            }
            if(x<0)
            {
                return loop(x + 10);
            }
            return x;
        }
    }
}
