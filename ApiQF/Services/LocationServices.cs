using ApiQF.Exceptions;
using ApiQF.Extensions;
using ApiQF.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiQF.Services
{
    public class LocationServices
    {
        private readonly static double s_precision = 0.01;
        public List<Satellite> SatelliteList { get; set; }
        public double DifferenceX { get; set; }

        public double DifferenceY { get; set; }

        public double DistanceBetweenPoints { get; set; }

        public LocationServices(List<Satellite> satellites)
        {
            SatelliteList = satellites;
        }

        public Location GetLocation()
        {
            if (SatelliteList == null || SatelliteList.Count == 0)
                throw new InsuficcientSatellitesException("No existen satelites disponibles para obtener la localizacion");
            return GetLocation(SatelliteList.Select(x => x.Distance).ToArray());
        }

        /// <summary>
        /// Function to get de location based on 3 satellites and 3 distances
        /// Inspired by http://paulbourke.net/geometry/circlesphere/
        /// Trilateration
        /// </summary>
        /// <param name="distances"></param>
        /// <returns></returns>
        public Location GetLocation(double[] distances)
        {
            if (!IsValidSatelliteStates())
                throw new InsuficcientSatellitesException("No hay suficientes satelites activos para obtener la localizacion");
            if (!IsValidDistances(distances))
                throw new InvalidDistancesException("Las distancias deben ser iguales a la cantidad de satelites disponibles");
            if (!IsValidSatellitePositions(distances))
                throw new NoResolutionException("No es posible determinar una interseccion");

            var distanceToPointTwo = ((distances[0] * distances[0]) - (distances[1] * distances[1]) + (DistanceBetweenPoints * DistanceBetweenPoints)) / (2.0 * DistanceBetweenPoints);
            var pointTwoX = SatelliteList.GetLocationX(0) + (DifferenceX * distanceToPointTwo / DistanceBetweenPoints);
            var pointTwoY = SatelliteList.GetLocationY(0) + (DifferenceY * distanceToPointTwo / DistanceBetweenPoints);

            /* Determine the distance from point 2 to either of the
            * intersection points.
            */
            var pointH = Math.Sqrt((distances[0] * distances[0]) - (distanceToPointTwo * distanceToPointTwo));

            /* Now determine the offsets of the intersection points from
            * point 2.
            */
            var rx = -DifferenceY * (pointH / DistanceBetweenPoints);
            var ry = DifferenceX * (pointH / DistanceBetweenPoints);

            /* Determine the absolute intersection points. */
            double intersectionPointOneX = pointTwoX + rx;
            double intersectionPointTwoX = pointTwoX - rx;
            double intersectionPointOneY = pointTwoY + ry;
            double intrsectionPointTwoY = pointTwoY - ry;
            DifferenceX = intersectionPointOneX - SatelliteList.GetLocationX(2);
            DifferenceY = intersectionPointOneY - SatelliteList.GetLocationY(2);
            double d1 = Math.Sqrt((DifferenceY * DifferenceY) + (DifferenceX * DifferenceX));

            DifferenceX = intersectionPointTwoX - SatelliteList.GetLocationX(2);
            DifferenceY = intrsectionPointTwoY - SatelliteList.GetLocationY(2);
            double d2 = Math.Sqrt((DifferenceY * DifferenceY) + (DifferenceX * DifferenceX));

            Location Location = null;
            if (Math.Abs(d1 - distances[2]) < s_precision)
            {
                Location = new Location(intersectionPointOneX, intersectionPointOneY);
            }
            else if (Math.Abs(d2 - distances[2]) < s_precision)
            {
                Location = new Location(intersectionPointTwoX, intrsectionPointTwoY);
            }

            if (Location == null)
                throw new NoResolutionException("No es posible determinar una interseccion entre los 3 satelites");

            return Location;
        }

        private List<Satellite> GetActiveSatellites()
        {
            return SatelliteList.Where(x => x.Active()).ToList();
        }

        private bool IsValidSatelliteStates()
        {
            return GetActiveSatellites().Count() >= 3;
        }

        private bool IsValidDistances(double[] distances)
        {
            return distances.Count() == GetActiveSatellites().Count();
        }

        private bool IsValidSatellitePositions(double[] distances)
        {
            if (SatelliteList.Any(x => x.Location == null))
                throw new InvalidLocationException("Falta la ubicacion de alguno de los satelites");
            DifferenceX = SatelliteList.GetFirstTwoXDifference();
            DifferenceY = SatelliteList.GetFirstTwoYDifference();
            DistanceBetweenPoints = Math.Sqrt((DifferenceY * DifferenceY) + (DifferenceX * DifferenceX));
            return !(
                (DistanceBetweenPoints > (distances[0] + distances[1])) ||
                (DistanceBetweenPoints < Math.Abs(distances[0] - distances[1])) ||
                (DistanceBetweenPoints == 0 && distances[0] == distances[1])
                );
        }
    }
}