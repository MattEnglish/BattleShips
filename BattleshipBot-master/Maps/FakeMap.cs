using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class FakeMap: Map
    {
        private Map originMap;

        public FakeMap(Map map)
        {
            originMap = map;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    hitSpaces[row,col] = map.GetHitSpaces()[row,col];
                    blockedSpaces[row, col] = map.GetBlockedSpaces()[row, col];
                    occupiedSpaces[row, col] = map.GetOccupiedSpaces()[row, col];
                }
            }
           
            foreach(Ship s in map.GetShips())
            {
                addShip(s.coordinate,s.shipLength);
            }
            shipTarget = map.shipTarget;
            foreach(Vector2 hitPos in shipTarget.hitPositions)
            {
                makeHitSpaceUnkownandUnblocked(hitPos);
                
            }
        }

        public void makeHitSpaceUnkownandUnblocked(Vector2 space)
        {
            hitSpaces[space.x, space.y] = 0;
            blockedSpaces[space.x, space.y] = false;
        }

        public override void shotFired(bool hit, int row, int column)
        {
            addBlockedSpace(row, column);

            if (hit)
            {
                hitSpaces[row, column] = 2;
            }
            else
            {
                hitSpaces[row, column] = 1;
            }
            if (shipTarget != null && hit)
            {
                shipTarget.numberOfHits++;
                shipTarget.hitPositions.Add(new Vector2(row, column));
            }



        }

    }
}
