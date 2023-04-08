using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LabMO3
{
    static class Function
    {
        public static double GetFunction(MyVector x)
        {
            return 6*Math.Pow(x.x1,2)+0.3*x.x1*x.x2+4*Math.Pow(x.x2,2);
        }
        public static MyVector GetGradient(MyVector x)
        {
            var gradient = new MyVector(12 * x.x1 + 0.3 * x.x2, 0.3 * x.x1 + 8 * x.x2);
            return gradient;
        }
        public static double[,] GetGesseMatrix()
        {
            var matrix = new double[2, 2];
            matrix[0, 0] = 12;
            matrix[0, 1] = 0.3;
            matrix[1, 0] = 0.3;
            matrix[1, 1] = 8;
            return matrix;
            
        }
        public static double GetDeterminant(double[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        public static double GetNorma(MyVector vector)
        {
            return Math.Sqrt(Math.Pow(vector.x1,2)+Math.Pow(vector.x2,2));
        }
        public static MyVector GetMinPoint(double x1,double x2, int maxNumberOfIterations, double e1,double e2)
        {
            //det A^-1 = 1/detA
            var answer = new MyVector(0,0);
            MyVector x = new MyVector(x1, x2);
            MyVector d = new MyVector(0,0);
            MyVector xNext = new MyVector(x1,x2);
            int t;
            Console.WriteLine("");
            Console.WriteLine("Начальные значения");
            Console.WriteLine($"X = ({x.x1}; {x.x2})");
            Console.WriteLine($"Значение функции = {GetFunction(x)}");
            Console.WriteLine("");
            for (int k= 0; k < maxNumberOfIterations; k++)
            {
                
                var gradient = GetGradient(x);
                if (GetNorma(gradient) <= e1)
                {
                    Console.WriteLine($"Минимум найден на итерации {k + 1}");
                    break;
                }
                double reversedDet = 1 / GetDeterminant(GetGesseMatrix());
                
                if (reversedDet > 0)
                {
                    d.x1 = -reversedDet * gradient.x1;
                    d.x2 = -reversedDet * gradient.x2;
                    t = 1;
                }
                else
                {
                    d.x1 = -gradient.x1;
                    d.x2 = -gradient.x2;
                    t = 1;
                    
                }
                xNext.x1 = x.x1 + t*d.x1;
                xNext.x2 = x.x2 + t*d.x2;
                if (GetNorma(xNext - x) < e2)
                {
                    if (Math.Abs(GetFunction(xNext) - GetFunction(x)) < e2)
                    {
                        answer = xNext;
                        Console.WriteLine($"Минимум найден на итерации {k + 1}");
                        break;
                    }
                }
                Console.WriteLine("");
                Console.WriteLine($"Конец итерации {k + 1}");
                Console.WriteLine($"X = ({xNext.x1}; {xNext.x2})");
                Console.WriteLine($"Значение функции = {GetFunction(xNext)}");
                Console.WriteLine("");
                x.x1 = xNext.x1;
                x.x2 = xNext.x2;
                answer = x;
            }
            return answer;
        }
    }
}
