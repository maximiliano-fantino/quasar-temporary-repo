using ApiQF.Config;
using ApiQF.Domain;
using ApiQF.Model;
using ApiQF.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;

namespace ApiQF.Controllers
{
    [Route("api/topsecret_split")]
    [ApiController]
    public class TopSecretSplitController : ControllerBase
    {
        private TopSecretSplit TopSecretSplit { get; }

        private SatelliteConfig SatelliteConfig;
        private TopSecretServices TopSecretServices;
        private Logger Log;

        public TopSecretSplitController(TopSecretSplit topSecretSplit, SatelliteConfig config, TopSecretServices topSecretServices, Logger logger)
        {
            TopSecretSplit = topSecretSplit;
            SatelliteConfig = config;
            TopSecretServices = topSecretServices;
            Log = logger;
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Spaceship))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(String))]
        public IActionResult Decode()
        {
            try
            {
                var satelliteMessages = new SatelliteMessages
                {
                    Satellites = TopSecretSplit.Satellites
                };
                return Ok(TopSecretServices.GetSpaceship(satelliteMessages, SatelliteConfig));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Debug(e);
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("{satellite_name}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(String))]
        public IActionResult Decode(string satellite_name, [FromBody] Model.Satellite satellite)
        {
            try
            {
                TopSecretSplit.AddSatellite(SatelliteConfig, satellite, satellite_name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}