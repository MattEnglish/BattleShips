using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class EnemyShipRecord
    {
        
        public double[,] ShotBias { get; set; }
        const double edgeShotBias = 0.55;
        const double nextToEdgeBias = 0.25;
        const double hitBias = 0.6;
        const double missBias = -0.05;


        public EnemyShipRecord()
        {
            ShotBias = initalShotBiasSetup();
            
        }

        public void addMap(Map m)
        {
            var hitSpaces = m.GetHitSpaces();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if(hitSpaces[row,col] == (int)hitSpace.hit)
                    {
                        ShotBias[row, col] =+ hitBias;
                    }

                    if (hitSpaces[row, col] == (int)hitSpace.miss)
                    {
                        ShotBias[row, col] =+ missBias;
                    }
                }

            }
        }
     

        private double[,] initalShotBiasSetup()
        {
            double[,] initalShotBias= new double[10,10];
            for (int i = 0; i < 10; i++)
            {
                initalShotBias[i,0] = edgeShotBias;
                initalShotBias[i, 9] = edgeShotBias;
                initalShotBias[0, i] = edgeShotBias;
                initalShotBias[9, i] = edgeShotBias;
            }
            for (int i = 1; i < 9; i++)
            {
                initalShotBias[i, 1] = nextToEdgeBias;
                initalShotBias[i, 8] = nextToEdgeBias;
                initalShotBias[1, i] = nextToEdgeBias;
                initalShotBias[8, i] = nextToEdgeBias;
            }

            return initalShotBias;
        }


    }
}
