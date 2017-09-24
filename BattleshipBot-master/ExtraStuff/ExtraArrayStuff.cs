using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public static class ExtraArrayStuff
    {
        public static double[,,] NormalizedArray(double[,,] array)
        {
            double sum = GetSumOfArray(array);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        array[i, j, k] = array[i, j, k] / sum;

                    }
                }
            }
            return array;
        }

        public static double GetSumOfArray(double[,,] array)
        {
            double sum = 0;
            foreach (double element in array)
            {
                sum += element;
            }
            return sum;

        }

        public static double GetAverageNon0ValueOfArray(double[,] array)
        {
            double sum = 0;
            int numberOfZeros = 0;
            foreach (double element in array)
            {
                if (element == 0)
                {
                    numberOfZeros++;
                }
                sum += element;
            }
            return sum / (array.Length - numberOfZeros);
        }

    }
}
