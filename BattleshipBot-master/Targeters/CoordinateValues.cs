﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class CoordinateValues
    {
        protected double[,,] initalFiveShipCoordValues { get; set; }
        protected double[,,] initalFourShipCoordValues { get; set; }
        protected double[,,] initalThreeShipCoordValues { get; set; }
        protected double[,,] initalTwoShipCoordValues { get; set; }
        protected Map map;
        protected MoreUniformConfigs MUC;
        private AdvEnemyShipValueCalc AESVC;

        public CoordinateValues(Map map, AdvEnemyShipValueCalc aESCV)
        {
            this.map = map;
            MUC = new MoreUniformConfigs();
            initalFiveShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(5, new Map());
            initalFourShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(4, new Map());
            initalThreeShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(3, new Map());
            initalTwoShipCoordValues = MUC.GetInitalUniformCoordsValueKinda(2, new Map());
            this.AESVC = aESCV;
        }

        public CoordinateValues(Map map, AdvEnemyShipValueCalc aESCV, CoordinateValues coordValues)
        {
            this.map = map;
            MUC = new MoreUniformConfigs();
            initalFiveShipCoordValues = coordValues.GetCoordinateValues(5);
            initalFourShipCoordValues = coordValues.GetCoordinateValues(4);
            initalThreeShipCoordValues = coordValues.GetCoordinateValues(3);
            initalTwoShipCoordValues = coordValues.GetCoordinateValues(2);
            this.AESVC = aESCV;
        }

        public double[,,] GetInitalShipCoordinateValues(int shipLength)
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

        public virtual double[,,] GetCoordinateValues(int shipLength)
        {
            var x = GetInitalShipCoordinateValues(shipLength);
            var y = AESVC.GetShipRecordedValuesRememberThreesAreDoubled(shipLength, map.GetShips().ToList());
            var coordinateValues = new double[10, 10, 2];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    for (int ori = 0; ori < 2; ori++)
                    {
                        coordinateValues[row, col, ori] = x[row, col, ori] + y[row, col, ori] / 2;

                        if (shipLength != 3)
                        {
                            coordinateValues[row, col, ori] += y[row, col, ori] / 2;
                        }
                    }
                }
            }
            return coordinateValues;
        }

    }
}
