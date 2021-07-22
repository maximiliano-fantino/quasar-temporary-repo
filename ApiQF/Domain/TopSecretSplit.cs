using ApiQF.Model;
using System.Collections.Generic;

namespace ApiQF.Domain
{
    public class TopSecretSplit
    {
        public List<Satellite> Satellites { get; set; } = new List<Satellite>();

        public void AddSatellite(Config.SatelliteConfig config, Satellite satellite, string name)
        {
            satellite.Name = name;
            config.UpdateSatelliteByName(satellite);
            Satellites.Add(satellite);
        }
    }
}