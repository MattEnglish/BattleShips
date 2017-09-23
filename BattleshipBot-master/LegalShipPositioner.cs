using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class LegalShipPositioner
    {
        private int shipLength;
        private int numberOfLegalPos;
        private bool[,,] legalPositions;
        Map map;


        public LegalShipPositioner(Map map, int shipLength)
        {
            this.map = map;
            this.shipLength = shipLength;
        }

        

        public bool[,,] getLegalPositions()
        {
            
            ConstructLegalPos();
            return legalPositions;
        }

        public int getnumberOfLegalPos()
        {
            
            ConstructLegalPos();
            return numberOfLegalPos;
        }

        private void ConstructLegalPos()
        {
            legalPositions = new bool[10, 10, 2];
            numberOfLegalPos =  2 * 10 * 10;
            ConstructLegalPosForOrientation(0);
            ConstructLegalPosForOrientation(1);
        }

        private void ConstructLegalPosForOrientation(int orientation)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {                   
                    legalPositions[i, j, orientation] = true;

                    Coordinate c = new Coordinate(i, j, orientation);

                    if (shipTailOffGrid(c, shipLength))
                    {
                        legalPositions[i, j, orientation] = false;
                    }

                    else if (shipOnBlockedSpace(c, shipLength))
                    {
                        legalPositions[i, j, orientation] = false;
                    }

                    if (shipNextTooOtherShip(c, shipLength))
                    {
                        legalPositions[i, j, orientation] = false;
                    }

                    if (legalPositions[i, j, orientation] == false)
                    {
                        numberOfLegalPos--;
                        //Console.Write(0);
                    }
                    else {
                        //Console.Write(1);
                    }
                }
                //Console.WriteLine("");
            }
            //Console.WriteLine("");
        }

        private bool shipOnBlockedSpace(Coordinate startingCoordinate, int shipLength)
        {
            int i = startingCoordinate.GetRow();
            int j = startingCoordinate.GetColumn();
            bool[,] blockedSpaces = map.GetBlockedSpaces();
            if (startingCoordinate.GetOrientation() == 0)
            {
                for (int l = 0; l < shipLength; l++)
                {
                    if (blockedSpaces[i + l, j])
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int l = 0; l < shipLength; l++)
                {
                    if (blockedSpaces[i, j+l])
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        private bool shipTailOffGrid(Coordinate startingCoordinate, int shipLength)
        {
            if (startingCoordinate.GetOrientation()==0)
            {

                if(startingCoordinate.GetRow()+shipLength -1 >=10)
                {
                    return true;
                }
                return false;
            }
            if (startingCoordinate.GetColumn() + shipLength -1  >= 10)
            {
                return true;
            }
            return false;

        }

        private bool shipNextTooOtherShip(Coordinate startingCoordinate, int shipLength)
        {
            int i = startingCoordinate.GetRow();
            int j = startingCoordinate.GetColumn();
            bool[,] occupiedSpaces = map.GetOccupiedSpaces();

            for (int l = -1; l < shipLength + 1; l++)
            {
                for (int m = -1; m < 2; m++)
                {
                    if (startingCoordinate.GetOrientation() == 0)
                    {
                        if (!offGrid(i + l) && !offGrid(j+m))
                        {
                            if (occupiedSpaces[i + l, j + m])
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!offGrid(j + l) && !offGrid(i + m))
                        {
                            if (occupiedSpaces[i + m, j + l])
                            {
                                return true;
                            }
                        }
                    }


                }
            }
            return false;
        }



        private bool offGrid(int x, int y)
        {
            return offGrid(x) || offGrid(y);
            
        }
        private bool offGrid(int x)
        {
            if (x > 9 || x < 0)
            {
                return true;
            }
            return false;
        }

        public int[,] GetNumberOfConfigurationWithAShipOnSpaces()
        {
            bool[,,] legalPos = getLegalPositions();
            int[,] frequency = new int[10,10];
            for (int row = 0; row < 11-shipLength; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    for (int PosOnShip = 0; PosOnShip < shipLength; PosOnShip++)
                    {
                        if (legalPos[row, column, 0])
                        {
                            frequency[row + PosOnShip, column]++;
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
                            frequency[row, column + PosOnShip]++;
                        }
                    }
                }
            }

            return frequency;

        }

    }

    


}
