using System.Diagnostics;
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
using Prob;

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
        /// Returns environment variables.
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
        /// Returns all request headers.
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
        /// Echoes back query string part of the request.
        /// </summary>
        /// <returns>query string</returns>
        [HttpGet("echo")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult Echo() => Ok(HttpContext.Request.QueryString.Value);

        /// <summary>
        /// show all cookies in the HTTP request.
        /// </summary>
        /// <returns>all cookies in the HTTP request.</returns>
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

        /// <summary>
        /// Redirect to the url specified by "url" in query parameters.
        /// </summary>
        /// <param name="url">url to redirect to.</param>
        /// <returns>(redirected page)</returns>
        [HttpGet("redirect")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult RedirectTo([FromQuery] string url)
        {
            return Redirect(url);
        }

        /// <summary>
        /// Execute ping for the specified host.
        /// </summary>
        /// <param name="host">hostname</param>
        /// <returns>the results of ping command</returns>
        [HttpGet("ping")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult Ping([FromQuery] string host) => Ok(new BashExecutor().Run($"ping -c 1 {host}"));

        /// <summary>
        /// Execute dig command for the specified host.
        /// </summary>
        /// <param name="host">target hostname</param>
        /// <returns>the results of dig</returns>
        [HttpGet("dns")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult Dns([FromQuery] string host) => Ok(new BashExecutor().Run($"dig {host}"));

        /// <summary>
        /// Execute a command in /bin/bash ("bash -c &lt;command>).
        /// </summary>
        /// <param name="cmd">command text ("echo foo", for example).</param>
        /// <returns>the results of the command</returns>
        [HttpGet("bash")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ApiExplorerSettings(GroupName = "v0.2.0")]
        public ActionResult ExecuteBash([FromQuery] string cmd) => Ok(new BashExecutor().Run(cmd));
    }
}
