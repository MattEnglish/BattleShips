using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class MoreUniformConfigs
    {
        
        public double[,] GetSpaceValue(int shipLength, bool[,,]legalPos)
        {
            var spaceValue = new double[10, 10];
            var coordValue = GetInitalUniformCoordsValueKinda(shipLength);
            for (int row = 0; row < 11 - shipLength; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                    {
                        if (legalPos[row, column, 0])
                        {
                            spaceValue[row + PosOnShip, column] += coordValue[row,column,0];
                        }
                    }
                }
            }

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 11 - shipLength; column++)
                {
                    for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                    {
                        if (legalPos[row, column, 1])
                        {
                            spaceValue[row, column + PosOnShip] += coordValue[row, column, 1];
                        }
                    }
                }
            }
            return spaceValue;
        }

        public double[,,] GetInitalUniformCoordsValueKinda(int shipLength)
        {
            var lP = new LegalShipPositioner(new Map(), shipLength);
            var emptyMapConfigs = lP.GetNumberOfConfigurationWithAShipOnSpaces();
            var emptyMapLegalPos = lP.getLegalPositions();
            double[,,] uniformCoordsValue = new double[10, 10, 2];

            for (int orientation = 0; orientation < 2; orientation++)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        if (emptyMapLegalPos[row, col, orientation])
                        {
                            for (int shipPos = 0; shipPos < shipLength; shipPos++)
                            {
                                if (orientation == 0)
                                {
                                    uniformCoordsValue[row, col, orientation] += emptyMapConfigs[row + shipPos, col];
                                }
                                else
                                {
                                    uniformCoordsValue[row, col, orientation] += emptyMapConfigs[row, col + shipPos];
                                }
                            }
                            uniformCoordsValue[row, col, orientation] = 1 / uniformCoordsValue[row, col, orientation];
                        }
                    }
                }
            }
            return uniformCoordsValue;
        }



    }

}
