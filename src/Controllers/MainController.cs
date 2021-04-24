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
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly IWebHostEnvironment _env;

        public MainController(ILogger<MainController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// returns "Hello, world".
        /// </summary>
        /// <returns>"Hello, world"</returns>
        [HttpGet("/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("text/plain")]
        public async Task<string> GetAsync() =>
            await Task.Run(() => "Hello, world.");

        /// <summary>
        /// return environment variables for "/envs".
        /// </summary>
        /// <returns></returns>
        [HttpGet("/envs")]
        public async Task<ActionResult> GetEnvsAsync()
        {
            var envDic = Environment.GetEnvironmentVariables();
            var sortedEnvList = new SortedList(envDic);
            var envs = string.Empty;
            foreach (DictionaryEntry e in sortedEnvList)
            {
                envs += $"{e.Key}:{e.Value}{Environment.NewLine}";
            }
            return await Task.Run(() => Ok(sortedEnvList));
            // return await Task.Run(() => Ok(envs));
            // return await Task.Run(() => Json(sortedEnvList));
        }
    }
}
