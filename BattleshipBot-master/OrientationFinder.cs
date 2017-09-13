using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class OrientationFinder
    {
        Map map;
        ShipTarget shipTarget;

        public OrientationFinder(Map map, ShipTarget shipTarget)
        {
            this.map = map;
            this.shipTarget = shipTarget;
        }

        public Vector2 GetNextShotPos()
        {
            return shipTarget.GetFirstShotPos() + Vector2.getVector(GetMostLikelyDirection());
        }

            public direction GetMostLikelyDirection()
        {
            int possibleDistUp = possibleDist(direction.up);
            int possibleDistDown = possibleDist(direction.down);
            int possibleDistLeft = possibleDist(direction.left);
            int possibleDistRight = possibleDist(direction.right);

            if ((possibleDistUp+possibleDistDown)>(possibleDistLeft+possibleDistRight))
            {
                if(possibleDistUp> possibleDistDown)
                {
                    return direction.up;
                }
                else
                {
                    return direction.down;
                }
            }
            else if((possibleDistUp + possibleDistDown) < (possibleDistLeft + possibleDistRight))
            {
                if (possibleDistRight > possibleDistLeft)
                {
                    return direction.right;
                }
                else
                {
                    return direction.left;
                }
            }
            else
            {
                if (possibleDistUp > possibleDistDown && possibleDistUp > possibleDistRight && possibleDistUp > possibleDistLeft)
                {
                    return direction.up;
                }
                else if(possibleDistDown > possibleDistRight && possibleDistDown > possibleDistLeft)
                {
                    return direction.down;
                }
                else if ( possibleDistRight > possibleDistLeft)
                {
                    return direction.right;
                }
                else
                {
                    return direction.left;
                }
            }
        }
        
        private int possibleDist(direction theDirection)
        {
            Vector2 space = shipTarget.GetFirstShotPos() + Vector2.getVector(theDirection);
            int possibleDist = 0;
            if(isAdjacentSpacePossibleHit(space))
            {
                space = space + Vector2.getVector(theDirection);
                possibleDist++;
            }
            else
            {
                return 0;
            }
            while (isNonAdjacentSpacePossibleHit(space))
            {
                space = space + Vector2.getVector(theDirection);
                possibleDist++;
            }
            return possibleDist;
        }

        public bool isAdjacentSpacePossibleHit(Vector2 space)
        {
            if(!Map.InBounds(space.x,space.y))
            {
                return false;
            }
            if(map.GetHitSpace(space)==hitSpace.miss)
            {
                return false;
            }
            SurroundingSpaces sS = new SurroundingSpaces(space, map);

            if (sS.GetNumberOfAdjacentOrDiagonalHits() > 1)
            {
                return false;
            }
            return true;
            
        }

        public bool isNonAdjacentSpacePossibleHit(Vector2 space)
        {
            if (!Map.InBounds(space.x, space.y))
            {
                return false;
            }
            if (map.GetHitSpace(space) == hitSpace.miss)
            {
                return false;
            }
            SurroundingSpaces sS = new SurroundingSpaces(space,map);

            if (sS.GetNumberOfAdjacentOrDiagonalHits() > 0)
            {
                return false;
            }
            return true;
        }


    }
}
