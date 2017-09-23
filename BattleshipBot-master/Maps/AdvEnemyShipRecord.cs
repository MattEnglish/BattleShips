using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class CoordinateCovariance// TO Do add symmetry stuff. add memory for misses?
    {
        private List<List<Ship>> previousShipConfigs = new List<List<Ship>>();

        public void AddNewConfig(List<Ship> newConfig)
        {
            previousShipConfigs.Add(newConfig);
        }

        public double[,,] GetNumberOfTimesShipHasBeenInAConfiguarationWithAsWellAsAShipInCurrentConfigForEachShipinCurrentConfigBLAH(List<Ship> currentConfig, int shipLengthOfplaceholder)
        {
            var x = new double[10, 10, 2];
            foreach (var ship in currentConfig)
            {
                foreach (var shipList in previousShipConfigs)
                {
                    foreach (var ship2 in shipList)
                    {
                        if (ship2.shipLength == ship.shipLength && ship.coordinate == ship2.coordinate)
                        {
                            foreach (var ship3 in shipList)
                            {
                                if (ship3.shipLength == shipLengthOfplaceholder)
                                {
                                    var coord = ship3.coordinate;
                                    x[coord.GetRow(), coord.GetColumn(), coord.GetOrientation()] += 1;
                                }
                            }
                        }
                    }
                }
            }

            return x;
        }


    }

    public class Forget
    {

    }
    public class AdvEnemyShipValueCalc
    {
        public const double forgetValue = 1;//We Do Not Forget!!!
        private const double shipDataPointValue = 0.01;

        public double[,,] FiveShipRecordedValues { get; private set; }
        public double[,,] FourShipRecordedValues { get; private set; }
        public double[,,] ThreeShipRecordedValues { get; private set; }
        public double[,,] TwoShipRecordedValues { get; private set; }

        public AdvEnemyShipValueCalc()
        {
            FiveShipRecordedValues = new double[10, 10, 2];
            FourShipRecordedValues = new double[10, 10, 2];
            ThreeShipRecordedValues = new double[10, 10, 2];
            TwoShipRecordedValues = new double[10, 10, 2];
        }


        public void AddMap(Map map)
        {
            //forget();
            foreach (Ship ship in map.GetShips())
            {
                var row = ship.coordinate.GetRow();
                var col = ship.coordinate.GetColumn();
                var ori = ship.coordinate.GetOrientation();

                switch (ship.shipLength)
                {
                    case 5:
                        FiveShipRecordedValues[row,col,ori] += shipDataPointValue;
                        break;
                    case 4:
                        FourShipRecordedValues[row, col, ori] += shipDataPointValue;
                        break;
                    case 3:
                        ThreeShipRecordedValues[row, col, ori] += shipDataPointValue;
                        break;
                    case 2:
                        TwoShipRecordedValues[row, col, ori] += shipDataPointValue;
                        break;
                    default:
                        throw new Exception();
                }

            }
        }

        private void forget()//DEACTIVATED THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            forget(FiveShipRecordedValues);
            forget(FourShipRecordedValues);
            forget(ThreeShipRecordedValues);
            forget(ThreeShipRecordedValues);
            forget(TwoShipRecordedValues);
        }
        private void forget(double[,,] shipRecordedValues)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        shipRecordedValues[i, j, k] = shipRecordedValues[i, j, k] * forgetValue;
                    }

                }
            }

        }

        public double[,,] GetShipRecordedValuesRememberThreesAreDoubled(int shipLength)
        {
            switch (shipLength)
            {
                case 5:
                    return FiveShipRecordedValues;                   
                case 4:
                    return FourShipRecordedValues;
                case 3:
                    return ThreeShipRecordedValues;
                case 2:
                    return TwoShipRecordedValues;
                default:
                    throw new Exception();
            }
        }
    }
}

