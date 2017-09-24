using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ForgetfulEnemyMap:EnemyMap
    {
        public override void newBattle()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                        valueOfSpaces[row, col] = valueOfSpaces[row, col]*9/10;                  
                }
            }
            base.newBattle();
        }

        protected override void UpdateValueOfSpaces(Vector2 position)
        {
            base.UpdateValueOfSpaces(position);
            base.UpdateValueOfSpaces(position);
        }
    }
}
