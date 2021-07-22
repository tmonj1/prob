using System.Diagnostics;

namespace Prob
{
    /// <summary>
    /// Execute a command using bash ("bash -c &lt;command>") and return the result (from stdout).
    /// </summary>
    public class BashExecutor
    {
        private ProcessStartInfo _processStartInfo;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="cmd">command (ex: curl -k https://prob)</param>
        public BashExecutor(string cmd = null)
        {
            _processStartInfo = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                Arguments = wrapCommandText(cmd),
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
        }

        /// <summary>
        /// run a command using bash ("bash -c &lt;command>").
        /// </summary>
        /// <returns>execution results of the command (stdout)</returns>
        public string Run(string cmd = null)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                _processStartInfo.Arguments = wrapCommandText(cmd);
            }
            var proc = new Process()
            {
                StartInfo = _processStartInfo
            };
            proc.Start();
            var result = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return result ?? "";
        }

        /// <summary>
        /// format a raw command text to text in an appropriate form for use in "bash -c &lt;command>".
        /// </summary>
        /// <param name="rawCmd"></param>
        /// <returns></returns>
        private static string wrapCommandText(string rawCmd)
        {
            if (string.IsNullOrEmpty(rawCmd))
            {
                return null;
            }

            return $"-c \"{rawCmd}\"";
        }
    }
}