using System.Reflection;

namespace Prob
{
    /// <summary>
    /// Provide ApplicationVersionInfo extension method for Assembly class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Return ApplicationVersionInfo object.
        /// </summary>
        /// <param name="assembly">Assembly object</param>
        /// <returns>ApplicationVersionInfo object</returns>
        public static ApplicationVersionInfo GetApplicationVersionInfo(this Assembly assembly) =>
            new(assembly);
    };

    /// <summary>
    /// Provides properties of the assembly.
    /// </summary>
    public class ApplicationVersionInfo
    {
        /// <summary>
        /// Assembly Full Name
        /// </summary>
        private string _name = string.Empty;

        /// <summary>
        /// Informational Version (= Semantic Version)
        /// </summary>
        private string _informationalVersion = string.Empty;

        /// <summary>
        /// Assembly Version
        /// </summary>
        private string _version = string.Empty;

        /// <summary>
        /// Assembly File Version
        /// </summary>
        private string _fileVersion = string.Empty;

        public ApplicationVersionInfo(Assembly assembly)
        {
            CacheAssemblyInfoOnce(assembly);
        }

        /// <summary>
        /// Gets Semantic Name.
        /// </summary>
        /// <value>
        /// Property <c>Name</c> represents the full qualified assembly name.
        /// </value>
        public string Name => _name;

        /// <summary>
        /// Gets semver (or assembly version if unavailable).
        /// </summary>
        /// <value>
        /// Semantic Version (semer). This is equal to the assembly informational version.
        /// </value>
        public string SemanticVersion => _informationalVersion;

        /// <summary>
        /// Gets assembly version.
        /// </summary>
        /// <value>Assembly version.</value>
        public string Version => _version;

        /// <summary>
        /// Gets file version.
        /// </summary>
        /// <value>Assembly File Version.</value>
        public string FileVersion => _fileVersion;

        /// <summary>
        /// Gets assembly informational version.
        /// </summary>
        /// <value>Assembly Informational Version.</value>
        public string InformationalVersion => _informationalVersion;

        /// <summary>
        /// Caches assembly info if not cached yet.
        /// </summary>
        private void CacheAssemblyInfoOnce(Assembly assembly)
        {
            // assembly name
            var nameAndAttributes = assembly.FullName.Split(",");
            _name = nameAndAttributes[0];

            // assembly version
            _version = assembly.GetName().Version.ToString();

            // assembly file version
            var fileVer = (assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))
                as AssemblyFileVersionAttribute);
            if (fileVer != null)
            {
                _fileVersion = fileVer.Version;
            }
            else
            {
                _fileVersion = "not available";
            }

            // assembly informatinal version (= semantic version)
            if (assembly.GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute))
                is AssemblyInformationalVersionAttribute infoVer)
            {
                _informationalVersion = infoVer.InformationalVersion;
            }
            else if (nameAndAttributes.Length > 1)
            {
                // use 2nd part of assembly full name
                _informationalVersion = nameAndAttributes[1].Replace("Version=", "").Replace(" ", "");
            }
            else
            {
                // should not come here
                _informationalVersion = "not available";
            }
        }
    }
}