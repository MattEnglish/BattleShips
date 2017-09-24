using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class SurroundingSpaces
    {
        private Map map;
        Vector2 middleSpace;
        

        public SurroundingSpaces(Vector2 middleSpace, Map map)
        {
            this.map = map;
            this.middleSpace = middleSpace;
            
        }

        public Vector2 GetAdjacentSpaceOf(hitSpace hitType)
        {
            direction direction = GetDirectionOfAdjacent(hitType);
            return middleSpace + Vector2.getVector(direction);
        }

     

        public direction GetDirectionOfAdjacent(hitSpace hitType)
        {
            if (Map.InBounds(middleSpace + Vector2.up))
            {
                if (hit(middleSpace + Vector2.up) == hitType)
                {
                    return direction.up;
                }
            }
            if (Map.InBounds(middleSpace + Vector2.right))
            {
                if (hit(middleSpace + Vector2.right) == hitType)
                {
                    return direction.right;
                }
            }
            if (Map.InBounds(middleSpace + Vector2.down))
            {
                if (hit(middleSpace + Vector2.down) == hitType)
                {
                    return direction.down;
                }
            }
            if (Map.InBounds(middleSpace + Vector2.left))
            {
                if (hit(middleSpace + Vector2.left) == hitType)
                {
                    return direction.left;
                }
            }
            return direction.unknown;
        }


        private hitSpace hit(Vector2 space)
        {
            int row = space.x;
            int Column = space.y;
            if (Map.InBounds(row))
            {
                return (hitSpace)map.GetHitSpace(row, Column);
            }
           
            throw new Exception();
        }

        public int GetNumberOfAdjacentOrDiagonalHits()
        {
            int numberOfHits = 0;
            int middleRow = middleSpace.x;
            int middleColumn = middleSpace.y;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (Map.InBounds(i + middleRow, j + middleColumn))
                    {
                        if (map.GetHitSpace(new Vector2(i + middleRow, j + middleColumn)) == hitSpace.hit)
                        {
                            numberOfHits++;
                        }
                    }
                }
            }
            return numberOfHits;
        }

        public bool IsSpaceAdjacentToo(Vector2 v)
        {
            if(middleSpace+Vector2.up==v)
            {
                return true;
            }
            if (middleSpace + Vector2.right == v)
            {
                return true;
            }
            if (middleSpace + Vector2.down == v)
            {
                return true;
            }
            if (middleSpace + Vector2.left == v)
            {
                return true;
            }
            return false;
        }


    }
}
