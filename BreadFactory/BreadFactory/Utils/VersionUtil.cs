using BreadFactory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Utils
{
    public class VersionUtil
    {
        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        /// <returns></returns>
        public static VersionInfo GetVersionInfo()
        {
            return new VersionInfo
            {
                FileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location),
                CopyrightAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute))
            };
        }
    }
}
