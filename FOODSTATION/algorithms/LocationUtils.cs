using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.algorithms
{
    public class LocationUtils
    {
        public static double Distance(double lat1, double lon1, double lat2, double lon2) {
        const double R = 6371; // Radius of the earth in km
        double dLat = Deg2Rad(lat2 - lat1); // Deg2Rad method below
        double dLon = Deg2Rad(lon2 - lon1);
        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double d = R * c; // Distance in km
        return d;
    }

    public static double Deg2Rad(double deg) {
        return deg * (Math.PI / 180);
    }

    public static double[] Nearest(double lat, double lon, double[,] pairs) {
        double minDist = double.MaxValue;
        double[] nearestPair = null;

        for (int i = 0; i < pairs.GetLength(0); i++) {
            double[] pair = new double[] { pairs[i, 0], pairs[i, 1] };
            double dist = Distance(lat, lon, pair[0], pair[1]);
            if (dist < minDist) {
                minDist = dist;
                nearestPair = pair;
            }
        }

        return nearestPair;
    }
    }
}