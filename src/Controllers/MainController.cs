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
        /// returns environment variables for "/envs".
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

        /// <summary>
        /// returns all request headers.
        /// </summary>
        /// <returns>request headers</returns>
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

        /// <summary>
        /// echoes back query string part of the request.
        /// </summary>
        /// <returns>query string</returns>
        [HttpGet("echo")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult Echo() => Ok(HttpContext.Request.QueryString.Value);

        [HttpGet("cookies")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult Cookie() => Ok(
            String.Join(
                Environment.NewLine,
                HttpContext.Request.Cookies.OrderBy(e => e.Key).Select(e => $"{e.Key}:{e.Value}")
            )
        );

        [HttpGet("redirect")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult RedirectTo([FromQuery] string url)
        {
            return Redirect(url);
        }
    }
}
