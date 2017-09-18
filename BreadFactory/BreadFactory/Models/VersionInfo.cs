using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Models
{
    public class VersionInfo
    {
        public FileVersionInfo FileVersion { get; set; }
        public AssemblyCopyrightAttribute CopyrightAttribute { get; set; }
    }
}
