using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    class AntiClumpCoordinateValues:CoordinateValues
    {
        private int numberOfShips;
        public AntiClumpCoordinateValues(Map map, AdvEnemyShipValueCalc aESVC, CoordinateValues initalCoordValuesOnly) : base(map, aESVC, initalCoordValuesOnly)
        {
            numberOfShips = 5;
        }

        public override double[,,] GetCoordinateValues(int shipLength)
        {
            if (base.map.GetShips().Length != numberOfShips)
            {
                numberOfShips = base.map.GetShips().Length;
                UpdateInitalCoordinateValues();
            }
            return base.GetCoordinateValues(shipLength);
        }

        

        private void UpdateInitalCoordinateValues()
        {
            Map mapOfJustShips = new Map();
            foreach (var ship in map.GetShips())
            {
                mapOfJustShips.addShip(ship.coordinate,ship.shipLength);
            }

                foreach (var unfoundShipsLength in map.GetUnfoundShipsLengths())
                {
                    if (unfoundShipsLength == 5)
                    {
                        base.initalFiveShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(5, mapOfJustShips, 50);
                    }
                    if (unfoundShipsLength == 4)
                    {
                        base.initalFourShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(4, mapOfJustShips, 50);
                    }
                    if (unfoundShipsLength == 3)
                    {
                        base.initalThreeShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(3, mapOfJustShips, 50);
                    }
                    if (unfoundShipsLength == 2)
                    {
                        base.initalTwoShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(2, mapOfJustShips, 50);
                    }

                }
            
            
        }
    }
}
