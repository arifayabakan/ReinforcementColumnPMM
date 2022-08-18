using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Donati
    {
        public int xCoordinate;
        public int yCoordinate;
        public int r;
        public double dg;
        public List<Double> dk;

        public Donati(int x, int y, int r,  double dg, List<Double> dk)
        {
            this.xCoordinate = x;
            this.yCoordinate = y;
            this.r = r;
            this.dk = dk;
            this.dg = dg;
            this.dg = dg;
          
        }

        public Boolean koordinatlarAynı(int x, int y)
        {
            return (this.xCoordinate == x) && (this.yCoordinate == y);
        }

        public String toString()
        {
            return "Donati : " + xCoordinate + "-" + yCoordinate;
        }
    }
}
