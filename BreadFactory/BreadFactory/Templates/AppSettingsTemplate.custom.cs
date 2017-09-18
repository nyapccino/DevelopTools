using BreadFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Templates
{
    public partial class AppSettingsTemplate
    {
        public VersionInfo Version { get; set; }
        public List<AppSettingsModel> ModelList { get; set; }
    }
}
