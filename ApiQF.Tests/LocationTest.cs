using ApiQF.Config;
using ApiQF.Exceptions;
using ApiQF.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace ApiQF.Tests
{
    internal class LocationTest
    {
        protected SatelliteConfig SatelliteConfig { get; set; }

        [SetUp]
        public void Setup()
        {
            SatelliteConfig = TestHelper.GetSatelliteConfig();
        }

        [Test]
        public void GetLocationWithoutSatellites()
        {
            var locationService = new LocationServices(null);

            Assert.Throws<InsuficcientSatellitesException>(
                delegate
                {
                    locationService.GetLocation();
                });
        }

        [Test]
        public void GetLocationWithoutLocation()
        {
            var l = new List<Model.Satellite>() {
                new Model.Satellite(null, "Test"),
                new Model.Satellite(null, "Test"),
                new Model.Satellite(null, "Test")
            };
            var locationService = new LocationServices(l);
            Assert.Throws<InvalidLocationException>(
                delegate
                {
                    locationService.GetLocation();
                });
        }

        [Test]
        public void GetLocationWithoutDistances()
        {
            var genericLocation = new Model.Location(1, 1);
            var l = new List<Model.Satellite>() {
                new Model.Satellite(genericLocation, "Test"),
                new Model.Satellite(genericLocation, "Test"),
                new Model.Satellite(genericLocation, "Test") };
            var locationService = new LocationServices(l);
            Assert.Throws<NoResolutionException>(
                delegate
                {
                    locationService.GetLocation();
                });
        }

        [Test]
        public void GetLocation()
        {
            var locationExpected = new Model.Location(-252.57853650241839, 234.49121901450997);
            var kenobi = new Model.Satellite(new Model.Location(-500, -200), "Kenobi");
            kenobi.Distance = 500;
            var skywalker = new Model.Satellite(new Model.Location(100, -100), "Skywalker");
            skywalker.Distance = 486;
            var sato = new Model.Satellite(new Model.Location(500, 100), "Sato");
            sato.Distance = 764.5;
            var l = new List<Model.Satellite>() { kenobi, skywalker, sato };
            var locationService = new LocationServices(l);
            var result = locationService.GetLocation();
            Assert.AreEqual(result.PositionX, locationExpected.PositionX);
            Assert.AreEqual(result.PositionY, locationExpected.PositionY);
        }
    }
}