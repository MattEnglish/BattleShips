using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;


namespace BattleshipBot
{
    public enum hitSpace { unknown, miss, hit }

    public class Map
    {
        protected bool[,] occupiedSpaces;
        protected bool[,] blockedSpaces;
        protected int[,] hitSpaces;
        public ShipTarget shipTarget { get; protected set; } 

        List<Ship> ships;

        public Map()
        {
            shipTarget = null;//THIS NEEDS TO BE SORTED OUT SERIOUSLY
            occupiedSpaces = new bool[10, 10];
            blockedSpaces = new bool[10, 10];
            hitSpaces = new int[10, 10];
            ships = new List<Ship>();
        }

        public Map CreateCopy()
        {
            Map mapCopy = new Map();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    mapCopy.dangerouslySetHitSpace(row, col, hitSpaces[row, col]);
                    foreach (Ship ship in ships)
                    {
                        mapCopy.addShip(ship.coordinate, ship.shipLength);
                    }
                }
            }
            return mapCopy;
        }

        public Ship[] GetShips()
        {
            return ships.ToArray();
        }

        public void dangerouslySetHitSpace(int row, int column, int hit)
        {
            hitSpaces[row, column] = hit;
        }

        public int GetHitSpace(int row, int column)
        {
            return hitSpaces[row, column];
        }

        public int[,] GetHitSpaces()
        {
            return hitSpaces;
        }

        public hitSpace GetHitSpace(Vector2 v)
        {
            return (hitSpace)hitSpaces[v.x, v.y];
        }

        public virtual void shotFired(bool hit, int row, int column)
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
            if(shipTarget!= null && hit)
            {
                shipTarget.numberOfHits++;
                shipTarget.hitPositions.Add(new Vector2(row, column));
            }
            else if(shipTarget == null && hit)
            {
                shipTarget = new ShipTarget(this, row, column);
            }
            if(shipTarget!=null && shipTarget.isDestroyed())
            {
                addShip(shipTarget.GetCurrentShip().coordinate, shipTarget.GetCurrentShip().shipLength);
                shipTarget = null;
            }
            
            
        }

        public bool[,] GetOccupiedSpaces()
        {
            return occupiedSpaces;
        }

        public bool[,] GetBlockedSpaces()
        {
            return blockedSpaces;
        }

        public void addBlockedSpace(int row, int column)
        {
            blockedSpaces[row, column] = true;
        }

        public void addOccupiedSpace(int row, int column)
        {
            occupiedSpaces[row, column] = true;
            blockedSpaces[row, column] = true;
        }

        public void addShip(IShipPosition ship) // THIS FUNCTION IS NOT SAFE!!!
        {
            var startingSquare = IGridConversions.GridToInts((GridSquare)ship.StartingSquare);
            var endingSquare = IGridConversions.GridToInts((GridSquare)ship.EndingSquare);
            int ori;
            int shipLength;
            if(startingSquare[0]!= endingSquare[0])
            {
                ori = 0;
                shipLength = endingSquare[0] -startingSquare[0]+1;
            }
            else
            {
                ori = 1;
                shipLength = endingSquare[1] - startingSquare[1] + 1;
            }
            Coordinate coord = new Coordinate(startingSquare[0], startingSquare[1], ori);
            addShip(coord, shipLength);



        }


        public void addShip(Coordinate startingCoordinate, int shipLength)
        {
            ships.Add(new Ship(startingCoordinate, shipLength));
            if (startingCoordinate.GetOrientation() == 0)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    addOccupiedSpace(startingCoordinate.GetRow() + i, startingCoordinate.GetColumn());
                }
            }

            if (startingCoordinate.GetOrientation() == 1)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    occupiedSpaces[startingCoordinate.GetRow(), startingCoordinate.GetColumn() + i] = true;
                }
            }
        }

        public static bool InBounds(int x, int y)
        {
            if (InBounds(x) && InBounds(y))
            {
                return true;
            }
            return false;
        }

        public static bool InBounds(Vector2 v)
        {
            return InBounds(v.x, v.y);
        }

        public static bool InBounds(int x)
        {
            if (x > -1 && x < 10)
            {
                return true;
            }
            return false;
        }

        public bool SpaceUnknown(Vector2 space)
        {
            if (!Map.InBounds(space))
            {
                return false;
            }
            if (this.GetHitSpace(space) == hitSpace.unknown)
            {
                return true;
            }
            return false;


        }
        public void addShipTarget(ShipTarget shipTarget)
        {
            this.shipTarget = shipTarget;
        }
        public void removeShipTarget()
        {
            this.shipTarget = null;
        }

        public int GetLongestShipLengthNotFound()
        {
            Ship[] ships = GetShips();
            Array.Sort(ships);
            Array.Reverse(ships);

            if (ships.GetLength(0) == 0 || ships[0].shipLength != 5)
            {
                return 5;
            }
            if (ships.GetLength(0) == 1 || ships[1].shipLength != 4)
            {
                return 4;
            }
            if (ships.GetLength(0) == 2 || ships[2].shipLength != 3)
            {
                return 3;
            }
            if (ships.GetLength(0) == 3 || ships[3].shipLength != 3)
            {
                return 3;
            }
            return 2;

        }

        public bool WonMatch()
        {
            if(ships.Count == 5)
            {
                return true;
            }
            return false;
            /*
            int numberOfHits = 0;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (hitSpaces[row, col] == (int)hitSpace.hit)
                    {
                        numberOfHits++;
                    }
                }
            }
            if (numberOfHits >= 17)
            {
                return true;
            }
            return false;
            */
        }

        public List<int> GetUnfoundShipsLengths()
        {
            var unfoundShipLengths = new List<int>();
            if(!ships.Any(x=>x.shipLength==5))
            {
                unfoundShipLengths.Add(5);
            }
            if (!ships.Any(x => x.shipLength == 4))
            {
                unfoundShipLengths.Add(4);
            }
            if (!ships.Any(x => x.shipLength == 3))
            {
                unfoundShipLengths.Add(3);
                unfoundShipLengths.Add(3);
            }
            if (ships.Where(x => x.shipLength==3).Count()==1)
            {
                unfoundShipLengths.Add(3);
            }
            if (!ships.Any(x => x.shipLength == 2))
            {
                unfoundShipLengths.Add(2);
            }
            return unfoundShipLengths;
        }



    }
        public class Ship:IComparable<Ship>
    {
        public Coordinate coordinate { get; }
        public int shipLength { get; }

        public Ship(Coordinate coordinate, int shipLength )
        {
            this.shipLength = shipLength;
            this.coordinate = coordinate;
        }

        public int CompareTo(Ship s)
        {
            return shipLength.CompareTo(s.shipLength);
        }

        public static bool operator ==(Ship ship1, Ship ship2)
        {
            return ship1.shipLength == ship2.shipLength && ship1.coordinate == ship2.coordinate;
        }

        public static bool operator !=(Ship ship1, Ship ship2)
        {
            return !(ship1 == ship2);
        }



    }
}
