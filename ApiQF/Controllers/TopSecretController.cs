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
    [Route("api/[controller]")]
    [ApiController]
    public class TopSecretController : ControllerBase
    {
        private SatelliteConfig SatelliteConfig { get; }
        private TopSecretServices TopSecretServices { get; }
        private Logger Log { get; }

        public TopSecretController(SatelliteConfig Config, TopSecretServices _ts, Logger _log)
        {
            SatelliteConfig = Config;
            TopSecretServices = _ts;
            Log = _log;
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Spaceship))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Decode([FromBody] SatelliteMessages _s)
        {
            try
            {
                return Ok(TopSecretServices.GetSpaceship(_s, SatelliteConfig));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Debug(e);
                return NotFound();
            }
        }
    }
}