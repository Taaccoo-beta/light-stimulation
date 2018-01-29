using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XControl
{
    class LeastSquareMethod
    {
        private List<double> x;
        private List<double> y;
        private int count;

        private double x_temp;
        private double y_temp;

        public double X_temp
        {
            private set { x_temp = value; }
            get { return x_temp; }
        }

        public double Y_temp
        {
            private set { y_temp = value; }
            get { return y_temp; }
        }

        public int Count
        {
            private set { count = value; }
            get { return count; }
        }

        public LeastSquareMethod()
        {
            x = new List<double>();
            y = new List<double>();
            Count = 0;
        }

        public void AddValueToX(double value)
        {
            x.Add(value);
            X_temp = value;
            Count++;
        }

        public void AddValueToY(double value)
        {
            y.Add(value);
            Y_temp = value;
        }

        public void getParameter(ref double a, ref double b)
        {
            double x_mean = x.Average();
            double y_mean = y.Average();

            double b_father = 0, b_son = 0;
            for (int i = 0; i != x.Count; i++)
            {
                b_son += (x[i] - x_mean) * (y[i] - y_mean);
                b_father += (x[i] - x_mean) * (x[i] - x_mean);
            }

            try
            {
                b = b_son / b_father;
            }
            catch
            {
                b = b_son / 1;
            }

            a = y_mean - b * x_mean;
            
            double temp = a;
            a = b;
            b = temp;

        }

    }
}
