using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Algorithms.Ranking;
using Algorithms.Others;

namespace Algorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHannuo_Click(object sender, EventArgs e)
        {
            HanNuoRanking hannuo = new HanNuoRanking();
            hannuo.DiskQuantity = 1;
            hannuo.MoveDisk(hannuo.DiskQuantity,"A","B","C");
        }

        private void btnCalPrimeNumber_Click(object sender, EventArgs e)
        {
            PrimeNumber primeNumber = new PrimeNumber();
            primeNumber.CalPrimeNumber(20);
        }

        private void btnPyramid_Click(object sender, EventArgs e)
        {
            Pyramid pyramid = new Pyramid();
            pyramid.Output(8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var moduleA = new Item("A");
            var moduleC = new Item("C",moduleA);
            var moduleB = new Item("B", moduleC);
            var moduleE = new Item("E", moduleB);
            var moduleD = new Item("D", moduleE);
            var unSorted = new[] { moduleE,moduleA,moduleD,moduleB,moduleC};
            var sorted = TopologicalRanking.Sort(unSorted, x => x.Dependencies);
            foreach (var item in sorted)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
