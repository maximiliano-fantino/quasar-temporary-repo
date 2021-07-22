using ApiQF.Config;
using ApiQF.Domain;
using ApiQF.Exceptions;
using ApiQF.Model;
using MoreLinq;
using System.Linq;

namespace ApiQF.Services
{
    public class TopSecretServices
    {
        public LocationServices LocationServices { get; set; }
        public MessageService MessageService { get; set; }

        public Logger Log { get; }

        public TopSecretServices(Logger log)
        {
            Log = log;
        }

        public Spaceship GetSpaceship(SatelliteMessages satelliteMessages, SatelliteConfig config)
        {
            ValidateSatellites(satelliteMessages, config);
            LocationServices = new LocationServices(satelliteMessages.Satellites);
            var location = LocationServices.GetLocation();
            MessageService = new MessageService();
            var message = MessageService.GetMessage(satelliteMessages.Satellites.Select(x => x.Message.Select(z => z).ToList()).ToList());
            var spaceship = new Spaceship { Message = message, Location = location };
            return spaceship;
        }

        private bool ValidateSatellites(SatelliteMessages satelliteMessages, SatelliteConfig config)
        {
            foreach (var satellite in satelliteMessages.Satellites)
            {
                config.UpdateSatelliteByName(satellite);
            }
            if (satelliteMessages.Satellites.Any(x => x.Location == null))
                throw new MissingLocationException("No se envio la ubicacion de los satelites conocidos");

            if (satelliteMessages.Satellites.GroupBy(n => n.Name).Any(c => c.Count() > 1))
            {
                satelliteMessages.Satellites = satelliteMessages.Satellites.DistinctBy(x => x.Name).ToList();
                Log.Warn("Se enviaron satelites duplicados");
            }

            return true;
        }
    }
}