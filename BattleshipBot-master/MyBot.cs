using System.Collections.Generic;
using System;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class METest : IBattleshipsBot
    {
        enum TargetStrategy {ConfigUniform,ConfigLearn, ClusterBomb }
        public enum DefensiveStrategy {Diversion,Mixed,Edge,Avoid,Shield,SemiRandom}

        private DefensiveStrategy defensiveStrategy;
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
            if (matchNumber > 0)
            {
                defensiveStrategy = DefensiveStrategy.Shield;
            }
            if (matchNumber > 10)
            {
                defensiveStrategy = DefensiveStrategy.Avoid;
            }
            if (matchNumber > 20)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }          
            
            if (matchNumber > 40)
            {
                defensiveStrategy = DefensiveStrategy.Diversion;
            }

            if (matchNumber > 50)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }


            if (matchNumber > 70)
            {
                defensiveStrategy = DefensiveStrategy.Shield;
            }
            if (matchNumber > 80)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }
            if (matchNumber > 85)
            {
                defensiveStrategy = DefensiveStrategy.Mixed;
            }
            if (matchNumber > 95)
            {
                defensiveStrategy = DefensiveStrategy.Edge;
            }
            defensiveStrategy = DefensiveStrategy.SemiRandom;
            
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
                ChangeTargetStrategy();
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
            
            /*
            else if (targetStrategy == TargetStrategy.SemiSnipe)
            {
                targeter = new TargeterSemiSnipe(currentMap, random, enemyShipRecord);
            }
            */
            else
            {
                targeter = new TargeterUniform(currentMap, random);
            }

            

            random = new Random();
            

            if (enemyMap == null)
            {
                enemyMap = new EnemyMap();
            }
            enemyMap.newBattle();


            ShipPositionerControl spc = new ShipPositionerControl(enemyMap);
            return spc.GetShipPositions(defensiveStrategy,random);
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

        public string Name => "Foolish Pugwash"; //Includes Counter to 100 !!!!

        

        private void ChangeTargetStrategy()
        {

            if (Enum.GetNames(typeof(TargetStrategy)).Length == 1 + (int) targetStrategy)
            {
                targetStrategy = 0;
            }

            else
            {
                targetStrategy++;
            }
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
