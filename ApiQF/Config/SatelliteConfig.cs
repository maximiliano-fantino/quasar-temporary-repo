using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiQF.Config
{
    public class SatelliteConfig
    {
        public List<Satellite> Satellites { get; set; } = new List<Satellite>();

        public void UpdateSatelliteByName(Model.Satellite satellite)
        {
            var satelliteFound = Satellites.Where(x => x.Name.Equals(satellite.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (satelliteFound != null)
                satellite.SetLocation(new Model.Location(satelliteFound.Location.PositionX, satelliteFound.Location.PositionY));
        }
    }
}