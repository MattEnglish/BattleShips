using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class UtilityFunctions
    {
        public static Coordinate GetInverseWeightedRandomCoordinate(double[,,] array, Random r)
        {
            double[,,] inverseArray = new Double[array.GetLength(0), array.GetLength(1), array.GetLength(2)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        if (array[i, j, k] != 0.0d)
                        {
                            inverseArray[i, j, k] = 1 / array[i, j, k];
                        }
                    }
                }
            }

            double trueValueCount = 0.0d;
            foreach(double d in inverseArray)
            {
                trueValueCount = trueValueCount + d;
            }
            if (trueValueCount == 0.0d)
            {
                throw new Exception();
            }
            double trueValueChosen = trueValueCount*r.NextDouble();

            for (int i = 0; i < inverseArray.GetLength(0); i++)
            {
                for (int j = 0; j < inverseArray.GetLength(1); j++)
                {
                    for (int k = 0; k < inverseArray.GetLength(2); k++)
                    {
                        trueValueChosen = trueValueChosen - inverseArray[i, j, k];
                        if (trueValueChosen <= 0)
                        {
                            return new Coordinate(i, j, k);
                        }
                    }

                }
            }
            throw new Exception();

        }
        public static Coordinate getWeightedRandomTrueCoordinate(int[,,] array, Random r)
        {
            int trueValueCount = 0;
            foreach (int i in array)
            {
                trueValueCount = trueValueCount + i;
            }
            if (trueValueCount == 0)
            {
                throw new Exception();
            }
            int trueValueChosen = r.Next(1, trueValueCount + 1);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {                        
                            trueValueChosen = trueValueChosen - array[i,j,k];
                            if (trueValueChosen <= 0)
                            {
                                return new Coordinate(i, j, k);
                            }                        
                    }

                }
            }
            throw new Exception();
        }

        public static Coordinate getRandomTrueCoordinate(bool[,,] array, Random r)
        {
            int trueValueCount = 0;
            foreach (bool b in array)
            {
                if (b)
                {
                    trueValueCount++;
                }
            }
            if (trueValueCount == 0)
            {
                throw new Exception();
            }

            int trueValueChosen = r.Next(1, trueValueCount + 1);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        if (array[i, j, k])
                        {

                            trueValueChosen--;
                            if (trueValueChosen == 0)
                            {
                                return new Coordinate(i, j, k);
                            }
                        }
                    }

                }
            }
            throw new Exception();

        }

        public static double[,] convertIntArrayToDouble(int[,] ints)
        {
            var doubles = new Double[ints.GetLength(0), ints.GetLength(1)];
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    
                        doubles[row, column] = (double)ints[row, column];
                    
                }
            }
            return doubles;
        }
    }
}
