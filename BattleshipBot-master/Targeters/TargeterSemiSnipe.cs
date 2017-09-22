using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterSemiSnipe:Targeter
    {

        private EnemyShipRecord enemyShipRecord;


        public TargeterSemiSnipe(Map map, Random random, EnemyShipRecord enemyShipRecord):base(map,random)
        {
            base.map = map;
            this.random = random;
            lastShotColumn = 0;
            lastShotRow = 0;
            this.enemyShipRecord = enemyShipRecord;
        }


        public override int[] findNewShipM(int shipLength)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            int[,] ConfigCount = LSP.GetNumberOfConfigurationWithAShipOnSpaces();

            double[,] configCountWithShotBias = UtilityFunctions.convertIntArrayToDouble(ConfigCount);
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if (configCountWithShotBias[row, column] != 0)
                    {
                        configCountWithShotBias[row, column] += enemyShipRecord.HitBias[row, column];                       
                        configCountWithShotBias[row, column] += enemyShipRecord.HitBias[row, column];                      
                        configCountWithShotBias[row, column] += enemyShipRecord.HitBias[row, column];
                        configCountWithShotBias[row, column] += enemyShipRecord.HitBias[row, column];
                        configCountWithShotBias[row, column] += enemyShipRecord.edgeBias[row, column];
                        configCountWithShotBias[row, column] += enemyShipRecord.edgeBias[row, column];
                        configCountWithShotBias[row, column] += enemyShipRecord.edgeBias[row, column];
                        configCountWithShotBias[row, column] += enemyShipRecord.edgeBias[row, column];
                    }
                }
            }
            int[] target = new int[2] { 0, 0 };
            double largestCount = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    double thisCount = configCountWithShotBias[row, column];

                    if (thisCount > largestCount)
                    {
                        largestCount = thisCount;
                        target[0] = row;
                        target[1] = column;
                    }

                }
            }
            return target;
        }

        /*
        public override int[] findNewShipM(int shipLength)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            int[,] ConfigCount = LSP.GetNumberOfConfigurationWithAShipOnSpaces();

            double[,] configCountWithShotBias = UtilityFunctions.convertIntArrayToDouble(ConfigCount);
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if (configCountWithShotBias[row, column] != 0)
                    {
                        configCountWithShotBias[row, column] = enemyShipRecord.HitBias[row, column] + 10;
                    }
                }
            }
            int[] target = new int[2] { 0, 0 };
            double largestCount = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    double thisCount = configCountWithShotBias[row, column];

                    if (thisCount > largestCount)
                    {
                        largestCount = thisCount;
                        target[0] = row;
                        target[1] = column;
                    }

                }
            }
            return target;
        }
        */

    }
}
