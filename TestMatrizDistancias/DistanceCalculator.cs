using System;
using System.Collections.Generic;
using System.Text;

namespace TestMatrizDistancias
{
    public static class DistanceCalculator
    {
        public const float RadioTierraKm = 6378.0F;
        public static double GetKmDistance(GeoPoint origin, GeoPoint destination)
        {
            var deltaLatitude = (destination.Latitude - origin.Latitude).ToRadians();
            var deltaLongitude = (destination.Longitude - origin.Longitude).ToRadians();
            var a = Math.Sin(deltaLatitude / 2).ToSquare() +
                Math.Cos(origin.Latitude.ToRadians()) *
                Math.Cos(destination.Latitude.ToRadians()) *
                Math.Sin(deltaLongitude / 2).ToSquare();

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return RadioTierraKm * Convert.ToSingle(c);
        }

        static double ToRadians(this double value)
        {
            return Convert.ToSingle(Math.PI / 180) * value;
        }
        static double ToSquare(this double value)
        {
            return Math.Pow(value, 2);
        }

    }
}
