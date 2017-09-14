using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class EnemyShipRecord
    {
        
        public double[,] ShotBias { get; set; }
        const double edgeShotBias = 0.55;
        const double nextToEdgeBias = 0.25;
        const double hitBias = 0.30;
        const double missBias = -0.05;
        private const double symmetryshotBias = 0.05;
        private const double aSymmetryshotBias = 0.05;
        private const double symmetrymissBias = -0.01;
        private const double aSymmetryMissBias = -0.01;

        public EnemyShipRecord()
        {
            ShotBias = initalShotBiasSetup();
            
        }

        public double[,] GetInitalShotEdgeBias()
        {
            return initalShotBiasSetup();
        }

        public void addMap(Map m)
        {
            var hitSpaces = m.GetHitSpaces();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    increaseShotBiasSymmetry(row,col,hitSpaces[row,col]);
                    increaseShotBiasASymmetry(row,col, hitSpaces[row, col]);
                }

            }
        }

        private void increaseShotBiasSymmetry(int row, int col, int hit)
        {
            
            if (hit == (int)hitSpace.hit)
            {
                ShotBias[row, col] += symmetryshotBias;
                ShotBias[9-row, col] += symmetryshotBias;
                ShotBias[row, 9-col] += symmetryshotBias;
                ShotBias[9 - row, 9 - col] += symmetryshotBias;
                ShotBias[col, row] += symmetryshotBias;
                ShotBias[9 - col, row] += symmetryshotBias;
                ShotBias[col, 9 - row] += symmetryshotBias;
                ShotBias[9 - col, 9 - row] += symmetryshotBias;
            }

            if (hit == (int)hitSpace.miss)
            {
                ShotBias[row, col] += symmetrymissBias;
                ShotBias[9 - row, col] += symmetrymissBias;
                ShotBias[row, 9 - col] += symmetrymissBias;
                ShotBias[9 - row, 9 - col] += symmetrymissBias;
                ShotBias[col, row] += symmetrymissBias;
                ShotBias[9 - col, row] += symmetrymissBias;
                ShotBias[col, 9 - row] += symmetrymissBias;
                ShotBias[9 - col, 9 - row] += symmetrymissBias;
            }
        }

        

        private void increaseShotBiasASymmetry(int row, int col, int hit )
        {
            if (hit == (int)hitSpace.hit)
            {
                ShotBias[row, col] += aSymmetryshotBias;
            }

            if (hit == (int)hitSpace.miss)
            {
                ShotBias[row, col] += aSymmetryMissBias;
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
