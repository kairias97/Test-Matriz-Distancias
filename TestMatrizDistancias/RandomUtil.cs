using System;
using System.Collections.Generic;
using System.Text;

namespace TestMatrizDistancias
{
    public static class RandomUtil
    {
        private static readonly Random random = new Random();

        public static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }
    }
}
