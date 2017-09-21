using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player;
using Battleships.Player.Interface;

namespace BattleshipBot
{

    public class ShipPositionerControl
    {
        private ShipPositioner SP = new ShipPositioner();
        private List<IShipPosition> shipPositions;
        private EnemyMap enemyMap;

        public ShipPositionerControl(EnemyMap enemyMap)
        {
            ShipPositioner SP = new ShipPositioner();
            this.enemyMap = enemyMap;
        }


        Random r = new Random();
        public IEnumerable<IShipPosition> GetShipPositions(DefensiveStrategy defensiveStrategy, Random r)
        {
            if (defensiveStrategy == DefensiveStrategy.SemiRandom)
            {
                var rand = r.Next(0, 10);
                if (rand % 10 == 1)
                {
                    defensiveStrategy = DefensiveStrategy.Avoid;
                }
                else if (rand % 10 == 2)
                {
                    defensiveStrategy = DefensiveStrategy.Diversion;
                }
                else if (rand % 10 == 3)
                {
                    defensiveStrategy = DefensiveStrategy.Edge;
                }
                else if (rand % 10 == 4)
                {
                    defensiveStrategy = DefensiveStrategy.Shield;
                }
                else if (rand % 10 == 5)
                {
                    defensiveStrategy = DefensiveStrategy.Shield;
                }
                else
                {
                    defensiveStrategy = DefensiveStrategy.Mixed;
                }
            }


            shipPositions = new List<IShipPosition> { };

            /*
            shipPositions.Add(GetShipRandomPosition(5));
            shipPositions.Add(GetShipRandomPosition(2));
            shipPositions.Add(GetShipRandomPosition(4));
            shipPositions.Add(GetShipRandomPosition(3));
            shipPositions.Add(GetShipRandomPosition(3));  
             */
            /*
           shipPositions.Add(GetShipRandomWeightedPosition(5));
           shipPositions.Add(GetShipRandomWeightedPosition(2));
           shipPositions.Add(GetShipRandomWeightedPosition(4));
           shipPositions.Add(GetShipRandomWeightedPosition(3));
           shipPositions.Add(GetShipRandomWeightedPosition(3));
           */
            /*
            
            shipPositions.Add(GetShipRandomWeightedPosition(5));
            shipPositions.Add(GetShipRandomWeightedPosition(4));
            shipPositions.Add(GetShipRandomWeightedPosition(3));
            shipPositions.Add(GetShipRandomWeightedPosition(3));
            shipPositions.Add(GetShipRandomWeightedPosition(2));
            */

            if (defensiveStrategy == DefensiveStrategy.Edge)
            {
                shipPositions.Add(GetShipsToAvoidShotsPosition(5));
                shipPositions.Add(GetShipOnEdge(4));
                shipPositions.Add(GetShipOnEdge(3));
                shipPositions.Add(GetShipOnEdge(3));
                shipPositions.Add(GetShipOnEdge(2));
            }

            if (defensiveStrategy == DefensiveStrategy.Avoid)
            {
                shipPositions.Add(GetShipsToAvoidShotsPosition(5));
                shipPositions.Add(GetShipsToAvoidShotsPosition(4));
                shipPositions.Add(GetShipsToAvoidShotsPosition(3));
                shipPositions.Add(GetShipsToAvoidShotsPosition(3));
                shipPositions.Add(GetShipsToAvoidShotsPosition(2));
            }

            else if (defensiveStrategy == DefensiveStrategy.Mixed)
            {
                shipPositions.Add(GetShipOnEdge(5));
                shipPositions.Add(GetShipOnEdge(4));
                shipPositions.Add(GetShipsToBeUniformish(3));
                shipPositions.Add(GetShipsToAvoidShotsPosition(3));
                shipPositions.Add(GetShipsToAvoidShotsPosition(2));
            }
            /*
            shipPositions.Add(GetShipsToBeUniformish(5));
            shipPositions.Add(GetShipsToBeUniformish(4));
            shipPositions.Add(GetShipsToBeUniformish(3));
            shipPositions.Add(GetShipsToBeUniformish(3));
            shipPositions.Add(GetShipsToBeUniformish(2));
            */
            else if (defensiveStrategy == DefensiveStrategy.Diversion)
            {
                return GetDiversionShips();
            }
            else if (defensiveStrategy == DefensiveStrategy.Shield)
            {
                return GetShieldShips();
            }
            else if (defensiveStrategy == DefensiveStrategy.Uniform)
            {
                shipPositions.Add(GetShipsToBeUniformish(5));
                shipPositions.Add(GetShipsToBeUniformish(4));
                shipPositions.Add(GetShipsToBeUniformish(3));
                shipPositions.Add(GetShipsToBeUniformish(3));
                shipPositions.Add(GetShipsToBeUniformish(2));
            }
            return shipPositions;

        }

        private ShipPosition GetShipRandomPosition(int shipLength)
        {
            Coordinate c = SP.placeShipAtRandomPosition(shipLength, r);
            return CoordinateToShipPosition(c, shipLength);
        }

        private ShipPosition GetShipRandomWeightedPosition(int shipLength)
        {

            Coordinate c = SP.placeShipRandomlyToAvoidShots(shipLength, r, enemyMap);
            return CoordinateToShipPosition(c, shipLength);
        }

        private ShipPosition GetShipOnEdge(int shipLength)
        {

            Coordinate c = SP.placeShipRandomlyOnEdgesToAvoidShots(shipLength, r, enemyMap);
            return CoordinateToShipPosition(c, shipLength);
        }

        private ShipPosition GetShipsToAvoidShotsPosition(int shipLength)
        {

            Coordinate c = SP.placeShipToAvoidShots(shipLength, enemyMap);
            return CoordinateToShipPosition(c, shipLength);
        }
        
        private ShipPosition GetShipsToBeUniformish(int shipLength)
        {
            Coordinate c = SP.placeShipRandomlyUniformly(shipLength,r);
            return CoordinateToShipPosition(c,shipLength);
        }

        private IEnumerable<ShipPosition> GetDiversionShips()
        {
            var spD = new ShipPositionerDiversion();
            var shipList = spD.GetShipCoordinates(r);
            foreach (var ship in shipList)
            {
                yield return CoordinateToShipPosition(ship.coordinate, ship.shipLength);
            }
            
        }

        private IEnumerable<ShipPosition> GetShieldShips()
        {
            var spD = new ShipPositionerSheild();
            var shipList = spD.GetShipSheildCoordinates(r);
            foreach (var ship in shipList)
            {
                yield return CoordinateToShipPosition(ship.coordinate, ship.shipLength);
            }

        }

        private ShipPosition CoordinateToShipPosition(Coordinate coord, int shipLength)
        {

            char startRow = IGridConversions.numToChar(coord.GetRow() + 1);
            int startColumn = coord.GetColumn() + 1;
            char endRow;
            int endColumn;

            if (coord.GetOrientation() == 0)
            {
                endRow = IGridConversions.numToChar(coord.GetRow() + shipLength - 1 + 1);
                endColumn = coord.GetColumn() + 1;
            }
            else
            {
                endRow = IGridConversions.numToChar(coord.GetRow() + 1);
                endColumn = coord.GetColumn() + shipLength - 1 + 1;
            }
            return GetShipPosition(startRow, startColumn, endRow, endColumn);
        }

        private ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {
            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

    }
}
