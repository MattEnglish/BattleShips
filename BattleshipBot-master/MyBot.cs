using System.Collections.Generic;
using System;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class METest : IBattleshipsBot
    {
        enum TargetStrategy {ConfigLearn, ClusterBomb, Config, SemiSnipe }


        private TargetStrategy targetStrategy;
        private Map currentMap = new Map();
        private Random random;
        private Targeter targeter;
        private int lastRow;
        private int lastColumn;
        private EnemyMap enemyMap = new EnemyMap();
        private EnemyShipRecord enemyShipRecord = new EnemyShipRecord();
        private int matchNumber = 0;
        private int lostStreak = -1;

        public IEnumerable<IShipPosition> GetShipPositions()
        {
            matchNumber++;
            if (matchNumber > 101)// reset after 101 matches
            {
                enemyShipRecord = new EnemyShipRecord();
                enemyMap = new EnemyMap();
                matchNumber = 0;
            }
            if (!currentMap.WonMatch())
            {
                lostStreak++;
            }
            else
            {
                lostStreak = 0;
            }
            if (lostStreak >= 3)
            {
                if(targetStrategy == TargetStrategy.ConfigLearn)
                {
                    targetStrategy = TargetStrategy.ClusterBomb;
                }
                
                else if(targetStrategy == TargetStrategy.ClusterBomb)
                {
                    targetStrategy = TargetStrategy.Config;
                }
                else if(targetStrategy == TargetStrategy.Config)
                {
                    targetStrategy = TargetStrategy.SemiSnipe;
                }
                else if(targetStrategy == TargetStrategy.SemiSnipe)
                {
                    targetStrategy = TargetStrategy.ConfigLearn;
                }
            }



            lastRow = 0;
            lastColumn = 0;
            enemyShipRecord.addMap(currentMap);
            currentMap = new Map();

            if (targetStrategy == TargetStrategy.ClusterBomb)
            {
                targeter = new TargeterClusterBomb(currentMap, random, enemyShipRecord);
            }
            else if (targetStrategy == TargetStrategy.ConfigLearn)
            {
                targeter = new TargeterLearn(currentMap, random, enemyShipRecord);
            }
            else if (targetStrategy == TargetStrategy.SemiSnipe)
            {
                targeter = new TargeterSemiSnipe(currentMap, random, enemyShipRecord);
            }
            else
            {
                targeter = new Targeter(currentMap, random);
            }

            

            random = new Random();
            

            if (enemyMap == null)
            {
                enemyMap = new EnemyMap();
            }
            enemyMap.newBattle();


            ShipPositionerControl spc = new ShipPositionerControl(enemyMap);
            return spc.GetShipPositions();
        }

        private static ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {

            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

        public IGridSquare SelectTarget()
        {
            int[] newTarget = targeter.GetNextTarget(lastRow, lastColumn);
            lastRow = newTarget[0];
            lastColumn = newTarget[1];
            return IGridConversions.intsToGrid(newTarget);
        }

        public void HandleShotResult(IGridSquare square, bool wasHit)
        {

            currentMap.shotFired(wasHit, lastRow, lastColumn);
        }

        public void HandleOpponentsShot(IGridSquare square)
        {
            int x = IGridConversions.charToNum(square.Row) - 1;
            int y = square.Column - 1;
            Vector2 pos = new Vector2(x, y);
            enemyMap.enemyShot(false, pos);
        }

        public string Name => "Test Pugwash"; //Includes Counter to 100 !!!!

        


    }

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
        public List<IShipPosition> GetShipPositions()
        {


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

            /*
            shipPositions.Add(GetShipOnEdge(5));
            shipPositions.Add(GetShipOnEdge(4));
            shipPositions.Add(GetShipOnEdge(3));
            shipPositions.Add(GetShipOnEdge(3));
            shipPositions.Add(GetShipOnEdge(2));
            */

            /*
            shipPositions.Add(GetShipsToAvoidShotsPosition(5));
            shipPositions.Add(GetShipsToAvoidShotsPosition(4));
            shipPositions.Add(GetShipsToAvoidShotsPosition(3));
            shipPositions.Add(GetShipsToAvoidShotsPosition(3));
            shipPositions.Add(GetShipsToAvoidShotsPosition(2));
            */
            shipPositions.Add(GetShipOnEdge(5));
            shipPositions.Add(GetShipsToAvoidShotsPosition(4));
            shipPositions.Add(GetShipRandomWeightedPosition(3));
            shipPositions.Add(GetShipsToAvoidShotsPosition(3));
            shipPositions.Add(GetShipsToAvoidShotsPosition(2));

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
    public class IGridConversions
    {
        public static Char numToChar(int num)
        {
            return (Char)(num + 64);
        }

        public static int charToNum(char c)
        {
            return (int)(c - 64);
        }


        public static GridSquare intsToGrid(int[] ints)
        {
            char row = numToChar(ints[0] + 1);
            int column = ints[1] + 1;
            return new GridSquare(row, column);
        }

        public static int[] GridToInts(GridSquare grid)
        {
            int row = charToNum(grid.Row) - 1;
            int column = grid.Column - 1;
            return new int[2] { row, column };
        }

    }

   


}
