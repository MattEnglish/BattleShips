using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    
    public class TargeterUniform : Targeter
    {
        private double[,,] initalFiveShipCoordValues;
        private double[,,] initalFourShipCoordValues;
        private double[,,] initalThreeShipCoordValues;
        private double[,,] initalTwoShipCoordValues;
        private MoreUniformConfigs MUC;


        public TargeterUniform(Map map, Random random) : base(map, random)
        {
            base.map = map;
            this.random = random;
            lastShotColumn = 0;
            lastShotRow = 0;
            MUC = new MoreUniformConfigs();
            
            initalFiveShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(5, map);
            initalFourShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(4, map);
            initalThreeShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(3, map);
            initalTwoShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(2, map);
            

        }

        public override int[] findNewShipM(int shipLength)
        {

            var spaceValues = MUC.GetSpaceValueSumofCoordValuesGivenLegalPos(GetInitalShipCoordinates(shipLength),shipLength,map);

            int[] target = new int[2] {0, 0};
            double largestValue = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    double thisValue = spaceValues[row,column];
                    if (thisValue >= largestValue)
                    {
                        largestValue = thisValue;
                        target[0] = row;
                        target[1] = column;
                    }
                }
            }
            return target;
        }

        private double[,,] GetInitalShipCoordinates(int shipLength)
        {
            switch (shipLength)
            {
                case 5:
                    return initalFiveShipCoordValues;
                case 4:
                    return initalFourShipCoordValues;
                case 3:
                    return initalThreeShipCoordValues;
                case 2:
                    return initalTwoShipCoordValues;
                default:
                    throw new Exception();
            }
            
        }
    }
    
}
