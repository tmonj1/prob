using System.Net;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace src.Controllers
{
    [ApiController]
    [Route("/")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// returns "Hello, world".
        /// </summary>
        /// <returns>"Hello, world"</returns>
        [HttpGet("/")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get() => "Hello, world.";

        /// <summary>
        /// return environment variables for "/envs".
        /// </summary>
        /// <returns></returns>
        [HttpGet("envs")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Envs()
        {
            var envDic = Environment.GetEnvironmentVariables();
            var sortedEnvList = new SortedList(envDic);
            var envs = string.Empty;
            foreach (DictionaryEntry e in sortedEnvList)
            {
                envs += $"{e.Key}:{e.Value}{Environment.NewLine}";
            }
            return Ok(envs);
        }

        [HttpGet("headers")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Headers()
        {
            var sortedHeaders = HttpContext.Request.Headers
                .OrderBy(e => e.Key)
                .Select(e => $"{e.Key}:{e.Value}");
            return Ok(String.Join(Environment.NewLine, sortedHeaders));
        }

        [HttpGet("echo")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Echo() => Ok(HttpContext.Request.QueryString.Value);
    }
}
