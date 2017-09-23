using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterController
    {
        private TargetStrategy targetStrategy;
        enum TargetStrategy {UniformLearn}
        private Targeter targeter;
        private Map currentMap;
        private Random random;
        private EnemyShipRecord enemyShipRecord;
        private int lostStreak = -1;
        private AdvEnemyShipValueCalc aesvc;

        public TargeterController(Pugwash pugwash, Random random, EnemyShipRecord enemyShipRecord, AdvEnemyShipValueCalc aesvc)
        {
            this.random = random;
            pugwash.newGame += new Pugwash.NewGameHandler(NewGame);
            this.enemyShipRecord = enemyShipRecord;
            this.aesvc = aesvc;
        }


        public void NewGame(NewGameEventArgs info)
        {
            
            if (!info.WonLastBattle)
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

            currentMap = info.NewMap;
            /*
            if (targetStrategy == TargetStrategy.ClusterBomb)
            {
                targeter = new TargeterClusterBomb(currentMap, random, enemyShipRecord);
            }
            */
             if (targetStrategy == TargetStrategy.UniformLearn)
            {
                targeter = new TargeterUniformLearn(currentMap, random, aesvc);
            }

            /*
            else if (targetStrategy == TargetStrategy.SemiSnipe)
            {
                targeter = new TargeterSemiSnipe(currentMap, random, enemyShipRecord);
            }
            
            else
            {
                targeter = new TargeterUniform(currentMap, random);
            }
            */
        }

        public void ChangeTargetStrategy()
        {

            if (Enum.GetNames(typeof(TargetStrategy)).Length == 1 + (int)targetStrategy)
            {
                targetStrategy = 0;
            }

            else
            {
                targetStrategy++;
            }
        }


        public int [] GetNextTarget(int lastRow,int lastColumn)
        {
            return targeter.GetNextTarget(lastRow, lastColumn);
        }
    }
}
