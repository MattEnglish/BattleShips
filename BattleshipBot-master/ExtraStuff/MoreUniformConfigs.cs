using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class MoreUniformConfigs
    {
        public double[,,] GetInitalUniformCoordsValueKinda(int shipLength)
        {
            return GetInitalUniformCoordsValueKinda(shipLength, new Map());
        }

        public double[,,] GetInitalUniformCoordsValueKinda(int shipLength, Map map)
        {
            
            var lP = new LegalShipPositioner(map, shipLength);
            var emptyMapConfigs = lP.GetNumberOfConfigurationWithAShipOnSpaces();
            var emptyMapLegalPos = lP.getLegalPositions();
            var coordValues = new double[10, 10, 2];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    for (int ori = 0; ori < 2; ori++)
                    {
                        if (emptyMapLegalPos[row, col, ori])
                        {
                            coordValues[row, col, ori] = 1;
                        }
                    }
                }

            }
            coordValues = NormalizedArray(coordValues);
            var spaceValues = GetSpaceValueSumofCoordValues(coordValues, shipLength);
            for (int i = 0; i < 200; i++)
            {
                coordValues = GetMoreUniformCoordsValues(shipLength, spaceValues, coordValues,lP);
                spaceValues = GetSpaceValueSumofCoordValues(coordValues, shipLength);

            }
            return coordValues;
        }



        private double[,,] GetMoreUniformCoordsValues(int shipLength, double[,] spaceValues, double[,,] coordsValues, LegalShipPositioner legalPositioner)
        {
            var moreUniformCoordsValue = coordsValues;
            
            
            var legalPos = legalPositioner.getLegalPositions();
            
            var averageElementSpaceValues = GetAverageNon0ValueOfArray(spaceValues);
            for (int orientation = 0; orientation < 2; orientation++)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        if (legalPos[row, col, orientation])
                        {
                            for (int shipPos = 0; shipPos < shipLength; shipPos++)
                            {
                                if (orientation == 0)
                                {
                                    var difference = averageElementSpaceValues - spaceValues[row + shipPos, col];
                                    int x =  2 * 2 * shipLength;
                                    moreUniformCoordsValue[row, col, orientation] = moreUniformCoordsValue[row, col, orientation] + difference / x;
                                    moreUniformCoordsValue[row, col, orientation] = Math.Max(moreUniformCoordsValue[row, col, orientation], 0.0001);
                                }
                                else
                                {
                                    int x = 2 * 2 * shipLength;
                                    var difference = averageElementSpaceValues - spaceValues[row, col + shipPos];
                                    moreUniformCoordsValue[row, col, orientation] = moreUniformCoordsValue[row, col, orientation] + difference / x;
                                    moreUniformCoordsValue[row, col, orientation] = Math.Max(moreUniformCoordsValue[row, col, orientation], 0.0001);
                                }

                            }
                            
                        }
                    }
                }
            }
            /*
             if (orientation == 0)
                                {
                                    var difference = averageElementSpaceValues - spaceValues[row + shipPos, col];
                                    int x = 10* 2 * shipLength;
                                    moreUniformCoordsValue[row, col, orientation] = moreUniformCoordsValue[row, col, orientation] + difference/x ;
                                }
                                else
                                {
                                    int x =  10 * 2*shipLength;
                                    var difference = averageElementSpaceValues - spaceValues[row , col + shipPos];
                                    moreUniformCoordsValue[row, col, orientation] = moreUniformCoordsValue[row, col, orientation]  +  difference / x;
                                } 

            if (orientation == 0)
                                {
                                    var difference = Math.Abs(averageElementSpaceValues - spaceValues[row + shipPos, col]);
                                    int x = 2 * shipLength;
                                    moreUniformCoordsValue[row, col, orientation] = moreUniformCoordsValue[row, col, orientation]* (1 + difference/x * (-1 + averageElementSpaceValues/ spaceValues[row + shipPos, col]));
                                }
                                else
                                {
                                    int x =  2*shipLength;
                                    var difference = Math.Abs(averageElementSpaceValues - spaceValues[row , col + shipPos]);
                                    moreUniformCoordsValue[row, col, orientation] = moreUniformCoordsValue[row, col, orientation] * (1 + difference / x * (-1 + averageElementSpaceValues / spaceValues[row , col + shipPos]));
                                }

             */
            return NormalizedArray(moreUniformCoordsValue);
        }

        public double[,,] NormalizedArray(double[,,] array)
        {
            double sum = GetSumOfArray(array);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        array[i, j, k] =  array[i, j, k]/sum;
                        
                    }
                }
            }
            return array;
        }

        public double GetSumOfArray(double [,,] array)
        {
            double sum = 0;
            foreach (double element in array)
            {                                              
                sum += element;
            }
            return sum;

        }

        private double GetAverageNon0ValueOfArray(double[,]array)
        {
            double sum = 0;
            int numberOfZeros = 0;
            foreach(double element in array)
            {
                if(element == 0)
                {
                    numberOfZeros++;
                }
                sum += element;
            }
            return sum / (array.Length - numberOfZeros);
        }


        public double[,] GetSpaceValueSumofCoordValuesGivenLegalPos(double[,,] coordinateValues, int shipLength, Map mapOfLegalPos)
        {
            LegalShipPositioner lp = new LegalShipPositioner(mapOfLegalPos, shipLength);
            var legalCoords = lp.getLegalPositions();
            double[,] valueSum = new double[10, 10];
            for (int row = 0; row < 11 - shipLength; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if (legalCoords[row, column, 0])
                    {
                        for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                        {
                            valueSum[row + PosOnShip, column] += coordinateValues[row, column, 0];
                        }
                    }
                }
            }

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 11 - shipLength; column++)
                {
                    if (legalCoords[row, column, 1])
                    {
                        for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                        {

                            valueSum[row, column + PosOnShip] += coordinateValues[row, column, 1];

                        }
                    }
                }
            }

            return valueSum;
        }


        public double[,] GetSpaceValueSumofCoordValues(double[,,]coordinateValues,int shipLength)
        {
            
            double[,] valueSum = new double[10, 10];
            for (int row = 0; row < 11 - shipLength; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                    {
                        valueSum[row + PosOnShip, column] += coordinateValues[row, column, 0];
                    }
                }
            }

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 11 - shipLength; column++)
                {
                    for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                    {
                        
                            valueSum[row, column + PosOnShip] += coordinateValues[row, column,1];
                        
                    }
                }
            }

            return valueSum;

        }

        

    }

}
