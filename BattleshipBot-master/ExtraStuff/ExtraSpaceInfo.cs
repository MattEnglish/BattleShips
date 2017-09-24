using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public static class ExtraSpaceInfo
    {

        public static bool isAdjacentSpacePossibleHit(Vector2 space, Map map)
        {
            if (!Map.InBounds(space.x, space.y))
            {
                return false;
            }
            if (map.GetHitSpace(space) == hitSpace.miss)
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

        public static bool isNonAdjacentSpacePossibleHit(Vector2 space, Map map)
        {
            if (!Map.InBounds(space.x, space.y))
            {
                return false;
            }
            if (map.GetHitSpace(space) == hitSpace.miss)
            {
                return false;
            }
            SurroundingSpaces sS = new SurroundingSpaces(space, map);

            if (sS.GetNumberOfAdjacentOrDiagonalHits() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
