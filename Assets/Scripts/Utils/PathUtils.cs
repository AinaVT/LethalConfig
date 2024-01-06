using System.IO;
using System.Reflection;

namespace LethalConfig.Utils
{
    internal static class PathUtils
    {
        public static string PathForResourceInAssembly(string resourceName, Assembly assembly = null)
        {
            var targetAssembly = assembly ?? Assembly.GetCallingAssembly();
            return Path.Combine(Path.GetDirectoryName(targetAssembly.Location), resourceName);
        }
    }  
}
