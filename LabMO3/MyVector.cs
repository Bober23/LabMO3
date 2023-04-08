using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LabMO3
{
    class MyVector
    {
        public double x1;
        public double x2;
        public MyVector(double x1, double x2)
        {
            this.x1 = x1;
            this.x2 = x2;
        }
        public static MyVector operator - (MyVector v1, MyVector v2)
        {
            return new MyVector(v1.x1-v2.x1,v1.x2-v2.x2);
        }
    }
}
