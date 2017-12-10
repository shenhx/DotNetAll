using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplicationDemo
{
    public class FindPrimesInput
    {
        public int From { get; set; }
        public int To { get; set; }

        public FindPrimesInput(int from, int to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
