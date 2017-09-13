using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class Vector2
    {
        public int x;
        public int y;
        public static Vector2 up { get { return new Vector2(1,0); } }
        public static Vector2 right { get { return new Vector2(0, 1); } }
        public static Vector2 down { get { return new Vector2(-1, 0); } }
        public static Vector2 left { get { return new Vector2(0, -1); } }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator+(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return(v1.x == v2.x && v1.y == v2.y);
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        public int[] ToArray()
        {
            return new int[2] { x, y };
        }

        public static Vector2 getVector(direction direction)
        {
            switch (direction)
            {
                case direction.up:
                    return ( Vector2.up);
                case direction.right:
                    return (Vector2.right);
                case direction.down:
                    return (Vector2.down);
                case direction.left:
                    return (Vector2.left);
                default:
                    throw new Exception();
            }
        }
    }
}
