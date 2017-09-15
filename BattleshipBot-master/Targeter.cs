using System;
using System.Collections.Generic;
using Battleships.Player.Interface;
using System.Linq;

namespace BattleshipBot
{
    

    public class Targeter
    {

        

        protected Map map = new Map();
        protected int lastShotRow;
        protected int lastShotColumn;
        protected Random random;
        
        
        ShipTargeter shipTargeter;
        protected ShipTarget shipTarget = null;      
         
        public Targeter(Map map, Random random)
        {
            this.map = map;
            this.random = random;
            lastShotColumn = 0;
            lastShotRow = 0;
            
        }

        

        public virtual int[] GetNextTarget(int theLastRowShot,int theLastColumnShot)
        {
            lastShotRow = theLastRowShot;
            lastShotColumn = theLastColumnShot;

            if(shipTarget!=null)
            {
                if(shipTarget.isDestroyed())
                {
                    Ship s = shipTarget.GetCurrentShip();
                    map.addShip(s.coordinate, s.shipLength);
                    shipTarget = null;
                    return findNewShip();
                }
            }
            if (shipTarget == null)
            {
                if (lastShotHit())
                {
                    shipTarget = new ShipTarget(map, lastShotRow, lastShotColumn);
                    shipTargeter = new ShipTargeter(map, shipTarget);
                    return shipTargeter.GetNextShot().ToArray();               
                }
                return findNewShip();
            }
            return shipTargeter.GetNextShot().ToArray();
                
            
        }



        protected bool lastShotHit()
        {
            if(map.GetHitSpace(lastShotRow,lastShotColumn)==2)
            {
                return true;
            }
            return false;
        }

        public int[] findNewShip()
        {
            try
            {
                return findNewShipM(map.GetLongestShipLengthNotFound());
            }
            catch
            {
                return findNewShipM(1);
            }     
        }

        

        public int[] findNewShip(int shipLength)
        {

            Coordinate TargetCoordinate = getRandomTargetCoordinate(shipLength);

            int distanceAlongShip = random.Next(0, shipLength);
            int[] nextTargetPos = new int[2];
            nextTargetPos[0] = TargetCoordinate.GetRow();
            nextTargetPos[1] = TargetCoordinate.GetColumn();            
            if (TargetCoordinate.GetOrientation() == 0)
            {
                nextTargetPos[0] = TargetCoordinate.GetRow() + distanceAlongShip;
            }
            else if(TargetCoordinate.GetOrientation() == 1)
            {
                nextTargetPos[1] = TargetCoordinate.GetColumn() + distanceAlongShip;
            }
            return nextTargetPos;
        }

        
        public Coordinate getRandomTargetCoordinate(int shipLength)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            bool[,,] legalPositions = LSP.getLegalPositions();
            return UtilityFunctions.getRandomTrueCoordinate(legalPositions, random);
        }

        public virtual int[] findNewShipM(int shipLength)
        {           
            return findNewShipMAll(shipLength);
        }

        public int[] findNewShipMAll(int shipLength)
        {
            LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
            int[,] ConfigCount = LSP.GetNumberOfConfigurationWithAShipOnSpaces();
            int[] target = new int[2] { 0, 0 };
            int largestCount = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    int thisCount = ConfigCount[row, column];
                    if (thisCount > largestCount)
                    {
                        largestCount = thisCount;
                        target[0] = row;
                        target[1] = column;
                    }
                }
            }
            return target;
        }

       

    }





    

   
    

}
