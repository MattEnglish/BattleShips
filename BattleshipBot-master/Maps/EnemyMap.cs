using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class EnemyMap
    {
        protected int[,] enemyShotsInOrder = new int[10, 10];
        protected int[,] valueOfSpaces = new int[10, 10];
        public int count = 0;

        public void enemyShot(bool hit, Vector2 position)
        {
            count++;
            enemyShotsInOrder[position.x, position.y] = count;
            UpdateValueOfSpaces(position);
        }

        protected virtual void UpdateValueOfSpaces(Vector2 position)
        {
            if (count < 5)
            {
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.x, position.y] + 6;//was 5
                /*
                 * valueOfSpaces[position.x, position.y] = valueOfSpaces[9-position.x, position.y] + 2;
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.x, 9-position.y] + 2;
                valueOfSpaces[position.x, position.y] = valueOfSpaces[9-position.x, 9-position.y] + 2;
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.y, position.x] + 2;
                valueOfSpaces[position.x, position.y] = valueOfSpaces[9 - position.y, position.x] + 2;
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.y, 9 - position.x] + 2;
                valueOfSpaces[position.x, position.y] = valueOfSpaces[9 - position.y, 9 - position.x] + 2;
                 */
            }
            else if (count < 10)
            {
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.x, position.y] + 4;// was 3
            }
            else if (count < 20)
            {
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.x, position.y] + 2;//was 2
            }
            else
            {
                valueOfSpaces[position.x, position.y] = valueOfSpaces[position.x, position.y] + 1;//was 1
            }
        }
        
        
        public int GetValueOfSpace(Vector2 position)
        {
            return valueOfSpaces[position.x, position.y];
        }

        public virtual void newBattle()
        {
            count = 0;
        }
    }
}
