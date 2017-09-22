using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class TargeterClusterBomb:TargeterLearn
    {
        bool clusterBomb = false;
        List<Vector2> ClusterBombMoves = new List<Vector2>();
        int clusterBombCount = 0;
        Vector2 clusterBombOrigin;

        public TargeterClusterBomb(Map map, Random random, EnemyShipRecord enemyShipRecord):base(map,random,enemyShipRecord)
        {
            base.map = map;
            this.random = random;
            lastShotColumn = 0;
            lastShotRow = 0;
            this.enemyShipRecord = enemyShipRecord;
            ClusterBombMovesSetup();
            
        }

        private void ClusterBombMovesSetup()
        {
            ClusterBombMoves.Add(new Vector2(0, 2));
            ClusterBombMoves.Add(new Vector2(2, 2));
            ClusterBombMoves.Add(new Vector2(2, 0));
            ClusterBombMoves.Add(new Vector2(2, -2));
            ClusterBombMoves.Add(new Vector2(0, -2));
            ClusterBombMoves.Add(new Vector2(-2, -2));
            ClusterBombMoves.Add(new Vector2(-2, 0));
            ClusterBombMoves.Add(new Vector2(-2, 2));

            ClusterBombMoves.Add(new Vector2(1, 3));
            ClusterBombMoves.Add(new Vector2(-1, 3));
            ClusterBombMoves.Add(new Vector2(1, -3));
            ClusterBombMoves.Add(new Vector2(-1, -3));
            ClusterBombMoves.Add(new Vector2(3, 1));
            ClusterBombMoves.Add(new Vector2(3, -1));
            ClusterBombMoves.Add(new Vector2(-3, 1));
            ClusterBombMoves.Add(new Vector2(-3, -1));
            
        }

        public override int[] GetNextTarget(int thisLastRowShot, int thisLastColumnShot)
        {
            if (base.shipTarget != null)
            {
                clusterBombCount = 0;
                clusterBomb = true;
                
                
                clusterBombOrigin = new Vector2(thisLastRowShot, thisLastColumnShot);
            }
                return base.GetNextTarget(thisLastRowShot, thisLastColumnShot);
        }

        public override int[] findNewShipM(int shipLength)
        {
            if(clusterBomb)
            {
                
                LegalShipPositioner LSP = new LegalShipPositioner(map, shipLength);
                int[,] ConfigCount = LSP.GetNumberOfConfigurationWithAShipOnSpaces();
                return GetNextClusterShot(ConfigCount, shipLength);
                
            }
            return base.findNewShipM(shipLength);
        }

        private int[] GetNextClusterShot(int[,] ConfigCount, int theShipLength)
        {
            var ship = map.GetShips()[map.GetShips().GetLength(0) - 1];
            if (ship.coordinate.GetOrientation() == 0)
            {
                clusterBombOrigin = new Vector2(ship.coordinate.GetRow() + ship.shipLength / 2 - random.Next(0, 2), ship.coordinate.GetColumn());
            }
            else
            {
                clusterBombOrigin = new Vector2(ship.coordinate.GetRow(), ship.coordinate.GetColumn() + ship.shipLength / 2 - random.Next(0, 2));
            }

            if (clusterBombCount>= ClusterBombMoves.Count)
            {
                clusterBombCount = 0;
                clusterBomb = false;
                return findNewShipM(theShipLength);
            }

            var target = ClusterBombMoves [clusterBombCount] + clusterBombOrigin;

            if(!Map.InBounds(target.x,target.y) || ConfigCount[target.x, target.y]==0)
            {
                clusterBombCount++;
                return GetNextClusterShot(ConfigCount,theShipLength);               
            }
            clusterBombCount++;
            return new int[] { target.x, target.y };

        }

        

    }
}
