using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class AdvShipTargeter : ShipTargeter
    {
        private AdvEnemyShipValueCalc aesvc;
        private CoordinateValues coordValueHolder;

        public AdvShipTargeter(Map map, ShipTarget shipTarget, AdvEnemyShipValueCalc aESVC, CoordinateValues cV) : base(map, shipTarget)
        {
            coordValueHolder = cV;
            aesvc = aESVC;
        }
        public void blah()
        {

        }

        
       
        public override Vector2 GetNextShot()
        {

            var ships = GetPossibleShips();


            double[,] spaceValues = new double[10, 10];

            foreach (Ship s in ships)
            {
                var x = coordValueHolder.GetCoordinateValues(s.shipLength);
                int initialRow = s.coordinate.GetRow();
                int initialCol = s.coordinate.GetColumn();
                int ori = s.coordinate.GetOrientation();
                for (int i = 0; i < s.shipLength; i++)
                {
                    if (ori == 0)
                    {
                        spaceValues[initialRow + i, initialCol] += x[initialRow, initialCol, 0];
                    }
                    else
                    {
                        spaceValues[initialRow, initialCol + i] += x[initialRow, initialCol, 1];
                    }
                }

            }

            foreach (var alreadyHitPos in shipTarget.hitPositions)
            {
                spaceValues[alreadyHitPos.x, alreadyHitPos.y] = 0;
            }
            double largestSpaceValue = 0;
            Vector2 bestSpace = new Vector2(0, 0);
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var ss = new SurroundingSpaces(new Vector2(row, col), map);
                    if (ss.GetNumberOfAdjacentOrDiagonalHits() == 1)
                    {
                        if (spaceValues[row, col] > largestSpaceValue)
                        {
                            largestSpaceValue = spaceValues[row, col];
                            bestSpace = new Vector2(row, col);
                        }
                    }
                }
            }
            return bestSpace;

        }

        public List<Ship> GetPossibleShips()
        {
            
            var fakeMap = new FakeMap(map);
            var possibleShips = new List<Ship>();
            foreach(int shipLength in fakeMap.GetUnfoundShipsLengths())
            { 
                LegalShipPositioner lp = new LegalShipPositioner(fakeMap, shipLength);
                var legalPos = lp.getLegalPositions();
                foreach (Coordinate c in GetCoordinatesThatAreOnSpace(shipLength, shipTarget.GetFirstShotPos()))
                {
                    if (legalPos[c.GetRow(), c.GetColumn(), c.GetOrientation()])
                    {
                        if(shipTarget.GetOrientation()==Orientation.unknown)
                        {
                            possibleShips.Add(new Ship(c, shipLength));
                        }
                        else if((int)shipTarget.GetOrientation()!=c.GetOrientation())
                        {
                            possibleShips.Add(new Ship(c, shipLength));
                        }
                    }

                }
            }
            return possibleShips;



        }
        /*
        public bool isShipPossible(Ship ship)
        {
            if (ship.coordinate.GetOrientation() == 0)
            {
                int col = ship.coordinate.GetColumn();                
                int numberOfAdjacentHitsToEachSquare = 0;
                int expectedNumberOfAdjacentHitsToEachSquare = 0;
                int row = ship.coordinate.GetRow();
                if(map.GetHitSpace(row,col)==2)
                {
                    expectedNumberOfAdjacentHitsToEachSquare--;
                }
                if (map.GetHitSpace(row + ship.shipLength, col) == 2)
                {
                    expectedNumberOfAdjacentHitsToEachSquare--;
                }

                for (int shipPos= row; shipPos < ship.coordinate.GetRow()+ ship.shipLength; shipPos++)
                {                   
                    var ss = new SurroundingSpaces(new Vector2(shipPos, col), map);
                    numberOfAdjacentHitsToEachSquare += ss.GetNumberOfAdjacentOrDiagonalHits();                        
                                                                            
                }
                expectedNumberOfAdjacentHitsToEachSquare += 3 * shipTarget.numberOfHits;

                return expectedNumberOfAdjacentHitsToEachSquare == numberOfAdjacentHitsToEachSquare;
            }
            else if (ship.coordinate.GetOrientation() == 1)
            {
                int col = ship.coordinate.GetColumn();
                int numberOfAdjacentHitsToEachSquare = 0;
                int expectedNumberOfAdjacentHitsToEachSquare = 0;
                int row = ship.coordinate.GetRow();
                if (map.GetHitSpace(row, col) == 2)
                {
                    expectedNumberOfAdjacentHitsToEachSquare--;
                }
                if (map.GetHitSpace(row , col + ship.shipLength) == 2)
                {
                    expectedNumberOfAdjacentHitsToEachSquare--;
                }

                for (int shipPos = col; shipPos < ship.coordinate.GetColumn() + ship.shipLength; shipPos++)
                {
                    var ss = new SurroundingSpaces(new Vector2(row, shipPos), map);
                    numberOfAdjacentHitsToEachSquare += ss.GetNumberOfAdjacentOrDiagonalHits();

                }
                expectedNumberOfAdjacentHitsToEachSquare += 3 * shipTarget.numberOfHits;

                return expectedNumberOfAdjacentHitsToEachSquare == numberOfAdjacentHitsToEachSquare;
            }
            return false;
        }
        */
        private List<Coordinate> GetCoordinatesThatAreOnSpace(int ShipLength, Vector2 space)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            for (int shipPos = 0; shipPos < ShipLength; shipPos++)
            {
                if (space.x - shipPos >= 0)
                {
                    coordinates.Add(new Coordinate(space.x - shipPos, space.y, 0));
                }
                if (space.y - shipPos >= 0)
                {
                    coordinates.Add(new Coordinate(space.x, space.y - shipPos, 1));
                }
            }
            return coordinates;
        }


    }
}
