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
        enum TargetStrategy {UniformLearn, UniformLearnCluster,UniformAntiClump}
        private Targeter targeter;
        private Map currentMap;
        private Random random;
        private EnemyShipRecord enemyShipRecord;
        private int lostStreak = -1;
        private AdvEnemyShipValueCalc aesvc;
        private int counter = 0;
        private double TargetAntiClumpUniformLearnAverage = 30;
        private double TargetUniformLearnAverage = 30;
        int Counter = 0;

        public TargeterController(Pugwash pugwash, Random random, EnemyShipRecord enemyShipRecord, AdvEnemyShipValueCalc aesvc)
        {
            this.random = random;
            pugwash.newGame += new Pugwash.NewGameHandler(NewGame);
            this.enemyShipRecord = enemyShipRecord;
            this.aesvc = aesvc;
        }


        public void NewGame(NewGameEventArgs info)
        {
            /*
            RecordAverages(info);
            counter++;
            if(counter <= 10)
            {
                targetStrategy = counter % 2 == 0 ? TargetStrategy.UniformLearn : TargetStrategy.UniformAntiClump; 
            }
            else
            {
                targetStrategy = TargetUniformLearnAverage > TargetAntiClumpUniformLearnAverage ? TargetStrategy.UniformAntiClump : TargetStrategy.UniformLearn;
            }
            */
            targetStrategy = TargetStrategy.UniformAntiClump;
            currentMap = info.NewMap;
            SelectTargeter();

            

            

        }

        public void RecordAverages(NewGameEventArgs info)
        {
            if (info.WonLastBattle)
            {
                if (targetStrategy == TargetStrategy.UniformLearn)
                {
                    TargetUniformLearnAverage = TargetUniformLearnAverage * 0.75d + 0.25d * (info.numberOfEnemyShots + 1);
                }
                else
                {
                    TargetAntiClumpUniformLearnAverage = TargetAntiClumpUniformLearnAverage * 0.75d + 0.25d * (info.numberOfEnemyShots + 1);
                }
            }
            if (!info.WonLastBattle)
            {
                if (targetStrategy == TargetStrategy.UniformLearn && info.numberOfEnemyShots > TargetUniformLearnAverage)
                {
                    TargetUniformLearnAverage = TargetUniformLearnAverage * 0.75d + 0.2d * (info.numberOfEnemyShots + 1);
                }
                else if (targetStrategy == TargetStrategy.UniformAntiClump && info.numberOfEnemyShots > TargetAntiClumpUniformLearnAverage)
                {
                    TargetAntiClumpUniformLearnAverage = TargetAntiClumpUniformLearnAverage * 0.75d + 0.2d * (info.numberOfEnemyShots + 1);
                }
            }
        }

        private void SelectTargeter()
        {

            if (targetStrategy == TargetStrategy.UniformAntiClump)
            {
                targeter = new TargeterUniformLearnAntiClump(currentMap, random, aesvc);
            }

            if (targetStrategy == TargetStrategy.UniformLearn)
            {
                targeter = new TargeterUniformLearn(currentMap, random, aesvc);
            }

            if (targetStrategy == TargetStrategy.UniformLearnCluster)
            {
                targeter = new TargeterUniLearnCluster(currentMap, random, aesvc, enemyShipRecord);
            }

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
