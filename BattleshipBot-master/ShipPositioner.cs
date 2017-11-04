using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ShipPositioner
    {
        private Map map;

        public ShipPositioner()
        {
            map = new Map();           
        }



         public Coordinate placeShipAtRandomPosition(int shipLength, Random r)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            bool[,,] legalPos = LSP.getLegalPositions();

          
            Coordinate coordinateChoson = UtilityFunctions.getRandomTrueCoordinate(legalPos,r);
            map.addShip(coordinateChoson,shipLength);
            return coordinateChoson;

        }

        public Coordinate placeShipRandomlyToAvoidShots(int shipLength, Random r,EnemyMap enemyMap)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            bool[,,] legalPos = LSP.getLegalPositions();
            int[,,] shipCost = new int[10, 10, 2];
            for (int orientation = 0; orientation < 2; orientation++)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        if(legalPos[row,column,orientation])
                        {
                            for (int shipPos = 0; shipPos < shipLength; shipPos++)
                            {
                                int x = 0;
                                int y = 0;
                                if(orientation ==0)
                                {
                                    x = shipPos;
                                }
                                if(orientation == 1)
                                {
                                    y = shipPos;
                                }
                                shipCost[row, column, orientation] = shipCost[row, column, orientation] 
                                    +  1 
                                    + enemyMap.GetValueOfSpace(new Vector2(row + x, column+y));
                            }
                        }

                    }
                }
            }

            double[,,] shipCostDouble = new double[10, 10, 2];
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        shipCostDouble[row, column, k] = (double)shipCost[row, column, k];
                    }
                }
            }


            Coordinate coordinateChoson = UtilityFunctions.GetInverseWeightedRandomCoordinate(shipCostDouble, r);
            map.addShip(coordinateChoson, shipLength);
            return coordinateChoson;
            

        }
    
        public Coordinate placeShipToAvoidShots(int shipLength, EnemyMap enemyMap)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            bool[,,] legalPos = LSP.getLegalPositions();
            int[,,] shipCost = new int[10, 10, 2];
            for (int orientation = 0; orientation < 2; orientation++)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        if (legalPos[row, column, orientation])
                        {
                            for (int shipPos = 0; shipPos < shipLength; shipPos++)
                            {
                                int x = 0;
                                int y = 0;
                                if (orientation == 0)
                                {
                                    x = shipPos;
                                }
                                if (orientation == 1)
                                {
                                    y = shipPos;
                                }
                                shipCost[row, column, orientation] = shipCost[row, column, orientation]
                                    + 1
                                    + enemyMap.GetValueOfSpace(new Vector2(row + x, column + y));
                            }
                        }

                    }
                }
            }

            int smallestCount = int.MaxValue;

            Coordinate place = new Coordinate(0, 0, 0);
            
            for (int orientation = 0; orientation < 2; orientation++)
            {


                for (int row = 0; row < 10; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        int thisCount = shipCost[row, column,orientation];

                        if (thisCount <= smallestCount && thisCount!=0)
                        {
                            smallestCount = thisCount;
                            place = new Coordinate(row, column, orientation);
                        }
                    }
                }
            }
            map.addShip(place, shipLength);
            return place;

        }
        
        public Coordinate placeShipRandomlyUniformly(int shipLength, Random r, CoordinateValues initalCoordValuesOnly)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            var legalPos = LSP.getLegalPositions();
            var uniformConfigs = new MoreUniformConfigs();
            var spaceValues = uniformConfigs.GetInitalUniformCoordsValueKinda(shipLength,map);

            for (int orientation = 0; orientation < 2; orientation++)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        if (!legalPos[row, column, orientation])
                        {
                            spaceValues[row,column,orientation] = 0;
                        }
                    }
                }
            }
        
            var c = UtilityFunctions.GetWeightedRandomCoordinate(spaceValues,r);
            map.addShip(c,shipLength);
            return c;

        }
        

        public Coordinate placeShipRandomlyOnEdgesToAvoidShots(int shipLength, Random r, EnemyMap enemyMap)
        {
            try
            {
                LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
                bool[,,] legalPos = LSP.getLegalPositions();



                for (int row = 0; row < 10; row++)
                {
                    for (int column = 1; column < 9; column++)
                    {
                        legalPos[row, column, 0] = false;
                    }
                }

                for (int row = 1; row < 9; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        legalPos[row, column, 1] = false;
                    }
                }
                


                int[,,] shipCost = new int[10, 10, 2];
                for (int orientation = 0; orientation < 2; orientation++)
                {
                    for (int row = 0; row < 10; row++)
                    {
                        for (int column = 0; column < 10; column++)
                        {
                            if (legalPos[row, column, orientation])
                            {
                                for (int shipPos = 0; shipPos < shipLength; shipPos++)
                                {
                                    int x = 0;
                                    int y = 0;
                                    if (orientation == 0)
                                    {
                                        x = shipPos;
                                    }
                                    if (orientation == 1)
                                    {
                                        y = shipPos;
                                    }
                                    shipCost[row, column, orientation] = shipCost[row, column, orientation]
                                        + 1
                                        + enemyMap.GetValueOfSpace(new Vector2(row + x, column + y));
                                }
                            }

                        }
                    }
                }

                double[,,] shipCostDouble = new double[10, 10, 2];
                for (int row = 0; row < 10; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            shipCostDouble[row, column, k] = (double)shipCost[row, column, k];
                        }
                    }
                }


                Coordinate coordinateChoson = UtilityFunctions.GetInverseWeightedRandomCoordinate(shipCostDouble, r);
                map.addShip(coordinateChoson, shipLength);
                return coordinateChoson;
            }
            catch
            {
                return placeShipRandomlyToAvoidShots(shipLength, r, enemyMap);
            }
        }

    }
}
