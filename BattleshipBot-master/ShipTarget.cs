using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ShipTarget
    {
        private Vector2 firstShotPos;
        Orientation orientation = Orientation.unknown;
        Map map;
        public int numberOfHits = 1;
        public List<Vector2> hitPositions;

        public ShipTarget(Map map, int row, int column)
        {

            firstShotPos = new Vector2(row, column);
            hitPositions = new List<Vector2>();
            hitPositions.Add(firstShotPos);
            this.map = map;
            map.addShipTarget(this);
        }

        public Ship GetCurrentShip()
        {
            if (!this.isDestroyed())
            {
                return null;
            }
            if(GetOrientation()==Orientation.vertical)
            {
                Vector2 startPos =  spaceAtEndOfHits(firstShotPos, direction.down) + Vector2.up ;
                Coordinate c = new Coordinate(startPos.x, startPos.y, 0);
                int shipLength = GetShipLength(GetOrientation(), startPos);
                return new Ship(c, shipLength);
            }
            if (GetOrientation() == Orientation.horizontal)
            {
                Vector2 startPos = spaceAtEndOfHits(firstShotPos, direction.left) + Vector2.right;
                Coordinate c = new Coordinate(startPos.x, startPos.y, 1);
                int shipLength = GetShipLength(GetOrientation(), startPos);
                return new Ship(c, shipLength);
            }
            throw new Exception();
        }

        public int GetShipLength(Orientation orientation, Vector2 startingPosOfShip)
        {
            
            if(orientation == Orientation.vertical)
            {
                return spaceAtEndOfHits(startingPosOfShip, direction.up).x - startingPosOfShip.x;
            }
            if (orientation == Orientation.horizontal)
            {
                return spaceAtEndOfHits(startingPosOfShip, direction.right).y - startingPosOfShip.y  ;
            }

            return 1;
        }


        public Vector2 GetFirstShotPos()
        {
            return firstShotPos;
        }

        /*
        public List<int[]> GetHitPositions()
        {
            List<int[]> ints = new List<int[]>();
            foreach (Vector2 v in hitPositions)
            {
                ints.Add(v.ToArray());
            }
            return ints;
        }
        */



        public Orientation GetOrientation()
        {
            if(orientation == Orientation.unknown)
            {
                SurroundingSpaces sS = new SurroundingSpaces(firstShotPos, map);
                direction d = sS.GetDirectionOfAdjacent(hitSpace.hit);
                if (d == direction.up || d == direction.down)
                {
                    return orientation = Orientation.vertical;
                }
                else if (d == direction.right || d == direction.left)
                {
                    return orientation = Orientation.horizontal;
                }
            }
            return orientation;
        }

        public bool isDestroyed()
        {
            if(numberOfHits>=map.GetLongestShipLengthNotFound())
            {
                return true;
            }
            orientation = GetOrientation();
            if (orientation == Orientation.unknown)
            {
                return false;
            }
            if(orientation == Orientation.vertical)
            {
                if (!spaceAtEndOfHitsIsPossibleHit(firstShotPos, direction.up))
                {
                    if (!spaceAtEndOfHitsIsPossibleHit(firstShotPos, direction.down))
                    { return true; }
                }

            }        
            else
            {
                if (!spaceAtEndOfHitsIsPossibleHit(firstShotPos, direction.right))
                {
                    if (!spaceAtEndOfHitsIsPossibleHit(firstShotPos, direction.left))
                    { return true; }
                }

            }
            return false;
        }

        public bool spaceAtEndOfHitsIsUnknown(Vector2 startingSpace, direction direction)
        {

            Vector2 endSpace = spaceAtEndOfHits(startingSpace, direction);
            return map.SpaceUnknown(endSpace);
        }

        public bool spaceAtEndOfHitsIsPossibleHit(Vector2 startingSpace, direction direction)
        {
            Vector2 endSpace = spaceAtEndOfHits(startingSpace, direction);
            return ExtraSpaceInfo.isAdjacentSpacePossibleHit(endSpace,map);
        }
       
        

        public Vector2 spaceAtEndOfHits(Vector2 startingSpace, direction direction)
        {
            Vector2 nextSpace = startingSpace + Vector2.getVector(direction);
            if (!Map.InBounds(nextSpace))
            {
                return nextSpace;
            }
            if (map.GetHitSpace(nextSpace) != hitSpace.hit)
            {
                return nextSpace;
            }
            else
            {
                return spaceAtEndOfHits(nextSpace, direction);
            }
        }

         

    }
}
