using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class Coordinate
    {
        private int i;
        private int j;
        private int k;


        public int GetRow()
        {
            return i;
        }

        public int GetColumn()
        {
            return j;
        }

        public int GetOrientation()
        {
            return k;
        }

        public Coordinate(int row, int column, int orientation)
        {
            i = row;
            j = column;
            k = orientation;

        }

    }
}
