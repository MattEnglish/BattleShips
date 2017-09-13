using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public enum direction { unknown, up, right, down, left }
    public enum Orientation {horizontal, vertical, unknown}

public class ShipTargeter
    {
        private Map map;
        private ShipTarget shipTarget;        

        public ShipTargeter(Map map, ShipTarget shipTarget)
        {
            this.map = map;
            this.shipTarget = shipTarget;
        }

        public Vector2 GetNextShot()
        {
            Orientation orientation = shipTarget.GetOrientation();
            
            if (orientation == Orientation.unknown)
            {
                return GetOrientationFindingShot();
            }

            return GetAlongShipShot(orientation);
            
        }

        public Vector2 GetOrientationFindingShot()
        {
            /*
            Vector2 firstShotPos = shipTarget.GetFirstShotPos();
            SurroundingSpaces sS = new SurroundingSpaces(firstShotPos, map);
            return sS.GetAdjacentSpaceOf(hitSpace.unknown);
            */
            OrientationFinder oF = new OrientationFinder(map, shipTarget);
            return oF.GetNextShotPos();

        }



        public Vector2 GetAlongShipShot(Orientation orientation)
        {
            Vector2 firstShotPos = shipTarget.GetFirstShotPos();
            Vector2 space;
            if (orientation == Orientation.vertical)
            {
                space = shipTarget.spaceAtEndOfHits(firstShotPos, direction.up);
                if(map.SpaceUnknown(space))
                {
                    return space;
                }
                space = shipTarget.spaceAtEndOfHits(firstShotPos, direction.down);
                if (map.SpaceUnknown(space))
                {
                    return space;
                }

                throw new Exception();
            }
            if(orientation == Orientation.horizontal)
            {
                space = shipTarget.spaceAtEndOfHits(firstShotPos, direction.right);
                if (map.SpaceUnknown(space))
                {
                    return space;
                }
                space = shipTarget.spaceAtEndOfHits(firstShotPos, direction.left);
                if (map.SpaceUnknown(space))
                {
                    return space;
                }

                throw new Exception();
            }
            throw new Exception();
        }
        


        /*
        private int[] GetNextShotPosition()
        {
            if (currentSpaceIsHit())
            {
                moveCurrentPosInCurrentDirection();
                if (CurrentPosWithinBoundaries())
                {
                    return new int[2] { currentRow, currentColumn };
                }
            }

            if (directionToTryNext == direction.down && shipLength > 1)
            {
                Coordinate shipCoordinate = new Coordinate(currentRow, currentColumn - shipLength, 1);
                map.addShip(shipCoordinate, shipLength);
                return new int[0];
            }

            if (directionToTryNext == direction.left && shipLength > 1)
            {
                Coordinate shipCoordinate = new Coordinate(currentRow + 1, currentColumn, 0);
                map.addShip(shipCoordinate, shipLength);
                return new int[0];
            }

            resetCurrentPosition();

            if (shipLength > 1)
            {
                tryOppositeDirection();
                return GetNextShotPosition();
            }
            tryClockwiseDirection();
            return GetNextShotPosition();

        }




        private bool currentSpaceIsHit()
        {
            return map.GetHitSpace(currentRow, currentColumn) == 2;
        }

        private void moveCurrentPosInCurrentDirection()
        {
            if (directionToTryNext == direction.up)
            {
                currentColumn--;
            }

            if (directionToTryNext == direction.down)
            {
                currentColumn++;
            }

            if (directionToTryNext == direction.right)
            {
                currentRow++;
            }

            if (directionToTryNext == direction.left)
            {
                currentRow--;
            }
        }

        private bool CurrentPosWithinBoundaries()
        {
            if(InBounds(currentColumn) && InBounds(currentRow))
            {
                return true;
            }
            return false;
        }

        private bool InBounds(int x)
        {
            if(x>-1 && x<10)
            {
                return true;
            }
            return false;
        }

        private void resetCurrentPosition()
        {
            currentRow = initalRow;
            currentColumn = initalColumn;
        }

        private void tryOppositeDirection()
        {
            directionToTryNext = directionToTryNext + 2;
        }

        private void tryClockwiseDirection()
        {
            directionToTryNext = directionToTryNext + 1;
        }
        */
    }
}
