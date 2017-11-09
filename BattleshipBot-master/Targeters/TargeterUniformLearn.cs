using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterUniformLearn : Targeter
    {

        private MoreUniformConfigs MUC;
        protected AdvEnemyShipValueCalc AESCV;
        protected CoordinateValues coordValueHolder;

        public TargeterUniformLearn(Map map, Random random, AdvEnemyShipValueCalc enemyShipCalculatorMem, CoordinateValues initalCoordinateValueOnly) : base(map, random)
        {
            base.map = map;
            this.random = random;
            this.AESCV = enemyShipCalculatorMem;
            lastShotColumn = 0;
            lastShotRow = 0;
            MUC = new MoreUniformConfigs();
            createCoordValueHolder(initalCoordinateValueOnly);
        }

        protected virtual void createCoordValueHolder(CoordinateValues initalCoordinateValueOnly)
        {
            coordValueHolder = new CoordinateValues(map, AESCV, initalCoordinateValueOnly);
        }

        public override int[] GetNextTarget(int theLastRowShot, int theLastColumnShot)
        {
            lastShotRow = theLastRowShot;
            lastShotColumn = theLastColumnShot;

            if (shipTarget != null)
            {
                if (shipTarget.isDestroyed())
                {
                    Ship s = shipTarget.GetCurrentShip();
                    shipTarget = null;
                    return findNewShip();
                }
            }
            if (shipTarget == null)
            {
                if (lastShotHit())
                {
                    shipTarget = new ShipTarget(map, lastShotRow, lastShotColumn);
                    shipTargeter = new AdvShipTargeter(map, shipTarget,AESCV,coordValueHolder);
                    //shipTargeter = new ShipTargeter(map, shipTarget);
                    return shipTargeter.GetNextShotmusthandleminus1().ToArray();
                }
                return findNewShip();
            }
            var nextShot = shipTargeter.GetNextShotmusthandleminus1();
            if(nextShot == new Vector2(-1,-1))
            {
                return GetNextTarget(theLastRowShot, theLastColumnShot);
            }
            return nextShot.ToArray();


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
            
            //ProphetService ps = new ProphetService(coordValueHolder);
            //return ps.FindNewShipProphet(map, MUC);
            
        }


        private double[,] GetAllSpaceValues()
        {
            var spaceValues = new double[10, 10];

            foreach (int unfoundShipLength in map.GetUnfoundShipsLengths())
            {
                var shipLengthSpaceValue = MUC.GetSpaceValueSumofCoordValuesGivenLegalPos(coordValueHolder.GetCoordinateValues(unfoundShipLength), unfoundShipLength, map);
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

    }

}


