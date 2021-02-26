using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFontsMetrics.Managers
{
    public class FilesManager
    {
        internal static string GetFullPath(string relativefilepath)
        {
            var basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            var relative = relativefilepath.Trim('/');
            return $"{basepath}\\{relative}";
        }

        internal static string ReadString(string relativefilepath)
        {
            return File.ReadAllText(GetFullPath(relativefilepath));
        }

        internal static bool Exists(string relativefilepath)
        {
            return File.Exists(GetFullPath(relativefilepath));
        }

        /// <summary>
        /// This is only called once when the settings does not exist.
        /// </summary>
        /// <param name="relativefilepath"></param>
        /// <param name="rawsettings"></param>
        internal static void WriteString(string relativefilepath, string rawsettings)
        {
            CheckDirectoryFilePath(relativefilepath);

            using (var stream = File.CreateText(GetFullPath(relativefilepath)))
            {
                stream.Write(rawsettings);
                stream.Flush();
                stream.Close();
            }
        }

        internal static void CheckDirectoryFilePath(string relativefilepath)
        {
            var fullpath = GetFullPath(relativefilepath);
            var namepath = fullpath.LastIndexOf('\\');
            var directorypath = fullpath.Substring(0, namepath);

            if (!Directory.Exists(directorypath))
            {
                Directory.CreateDirectory(directorypath);
            }
        }
    }
}
