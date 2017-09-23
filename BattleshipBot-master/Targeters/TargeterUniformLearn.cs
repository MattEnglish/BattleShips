using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterUniformLearn : Targeter
    {

        private double[,,] initalFiveShipCoordValues;
        private double[,,] initalFourShipCoordValues;
        private double[,,] initalThreeShipCoordValues;
        private double[,,] initalTwoShipCoordValues;
        private MoreUniformConfigs MUC;
        private AdvEnemyShipValueCalc AESCV;


        public TargeterUniformLearn(Map map, Random random,AdvEnemyShipValueCalc enemyShipCalculatorMem) : base(map, random)
        {
            base.map = map;
            this.random = random;
            this.AESCV = enemyShipCalculatorMem;
            lastShotColumn = 0;
            lastShotRow = 0;
            MUC = new MoreUniformConfigs();

            initalFiveShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(5, new Map());
            initalFourShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(4, new Map());
            initalThreeShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(3, new Map());
            initalTwoShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(2, new Map());
            //
            //
            //
            //
            //WARNING EVERY TIME MAKE NEW TARGETUNIFORM MASSIVE PERFOMRANCE PROBLEMS
            //
            //
            //
            //

        }
        public override int[] findNewShip()
        {

            var spaceValues = GetAllSpaceValues();

            int[] target = new int[2] { 0, 0 };
            double largestValue = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    double thisValue = spaceValues[row, column];
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


        private double[,] GetAllSpaceValues()
        {
            var spaceValues = new double[10, 10];
            
            foreach (int unfoundShipLength in map.GetUnfoundShipsLengths())
            {
                var shipLengthSpaceValue = MUC.GetSpaceValueSumofCoordValuesGivenLegalPos(GetCoordinateValues(unfoundShipLength), unfoundShipLength, map);
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        spaceValues[row, col] += unfoundShipLength * shipLengthSpaceValue[row, col];
                    }
                }

            }
            return spaceValues;
        }

        private double[,,] GetCoordinateValues(int shipLength)
        {
            var x = GetInitalShipCoordinateValues(shipLength);
            var y = AESCV.GetShipRecordedValuesRememberThreesAreDoubled(shipLength,map.GetShips().ToList());
            var coordinateValues = new double[10, 10, 2];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    for (int ori = 0; ori < 2; ori++)
                    {
                        coordinateValues[row, col, ori] = x[row, col, ori] + y[row, col, ori]/2;

                        if(shipLength!=3)
                        {
                            coordinateValues[row, col, ori] += y[row, col, ori] / 2;
                        }
                    }
                }
            }
            return coordinateValues;
        }
        /*
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
        */

        private double[,,] GetInitalShipCoordinateValues(int shipLength)
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

