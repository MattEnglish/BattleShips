using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    class MapOfEnemyShipsAllTime
    {
        int[,] numberOfShipsOnSpace;

        MapOfEnemyShipsAllTime()
        {
            numberOfShipsOnSpace = new int[10, 10];
        }
        
        public void addShips(Ship[] ships)
        {
            foreach(Ship ship in ships)
            {
                int row = ship.coordinate.GetRow();
                int column = ship.coordinate.GetColumn();
                int k = ship.coordinate.GetOrientation();
                for (int posOnShip = 0; posOnShip < ship.shipLength; posOnShip++)
                {
                    if (k == 0)
                    {
                        numberOfShipsOnSpace[row + posOnShip, column]++;
                    }
                    else
                    {
                        numberOfShipsOnSpace[row , column + posOnShip]++;
                    }
                }
                
            }
        }


    }
}
