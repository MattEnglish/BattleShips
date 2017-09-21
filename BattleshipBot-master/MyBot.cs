using System.Collections.Generic;
using System;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class NewGameEventArgs
    {
        public Map OldMap;
        public Map NewMap;
        public bool WonLastBattle;
        public int numberOfEnemyShots;
        public int numberOfThisShots;

        public NewGameEventArgs(Map oldMap, Map newMap, bool wonLastBattle)
        {
            OldMap = oldMap;
            NewMap = newMap;
            WonLastBattle = wonLastBattle;
        }

    }

    //To Do add Constructor, this first repair Targeting lol, NewGame Subscribe!!!.
    public class Pugwash : IBattleshipsBot
    {
        
        
        private Map currentMap = new Map();
        private Random random = new Random();
        private TargeterController targeterC;
        private DefensiveController defensiveC = new DefensiveController();
        private int lastRow;
        private int lastColumn;
        private EnemyMap enemyMap = new EnemyMap();
        private EnemyShipRecord enemyShipRecord = new EnemyShipRecord();
        

       public Pugwash()
        {
            targeterC = new TargeterController(this,random, enemyShipRecord);
            enemyMap = new EnemyMap();
        }

        public delegate void NewGameHandler(NewGameEventArgs info);

        public event NewGameHandler newGame;

        public IEnumerable<IShipPosition> GetShipPositions()
        {           
            Map newMap = new Map();
            enemyShipRecord.addMap(currentMap);
            newGame(new NewGameEventArgs(currentMap,newMap,currentMap.WonMatch()));
            currentMap = newMap;
            lastRow = 0;
            lastColumn = 0;                                
            enemyMap.newBattle();
            ShipPositionerControl spc = new ShipPositionerControl(enemyMap);
            return spc.GetShipPositions(defensiveC.GetDefensiveStrategy(), random);
        }

        private static ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {

            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

        public IGridSquare SelectTarget()
        {
            int[] newTarget = targeterC.GetNextTarget(lastRow, lastColumn);
            lastRow = newTarget[0];
            lastColumn = newTarget[1];
            return IGridConversions.intsToGrid(newTarget);
        }

        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            var row = IGridConversions.charToNum(square.Row) -1;
            var col = square.Column - 1;
            
            currentMap.shotFired(wasHit, row, col);
        }

        public void HandleOpponentsShot(IGridSquare square)
        {
            int x = IGridConversions.charToNum(square.Row) - 1;
            int y = square.Column - 1;
            Vector2 pos = new Vector2(x, y);
            enemyMap.enemyShot(false, pos);
        }

        public string Name => "Needs To be Refactored Pugwash"; //Includes Counter to 100 !!!!

        

        


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
