using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public static class ExtraSpaceInfo
    {
        public static List<Coordinate> GetCoordinatesThatAreOnSpace(int ShipLength, Vector2 space)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            for (int shipPos = 0; shipPos < ShipLength; shipPos++)
            {
                if (space.x - shipPos >= 0)
                {
                    coordinates.Add(new Coordinate(space.x - shipPos, space.y, 0));
                }
                if (space.y - shipPos >= 0)
                {
                    coordinates.Add(new Coordinate(space.x, space.y - shipPos, 1));
                }
            }
            return coordinates;
        }


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
