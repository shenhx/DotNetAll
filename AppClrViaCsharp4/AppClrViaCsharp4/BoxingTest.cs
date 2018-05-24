using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClrViaCsharp4
{
    public struct Point
    {
        private Int32 _x, _y;

        public Point(Int32 x,Int32 y)
        {
            _x = x;
            _y = y;
        }

        public void Change(Int32 x,Int32 y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return string.Format("{0}，{1}", _x, _y);
        }
    }
}
