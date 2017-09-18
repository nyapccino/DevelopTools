using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Models
{
    public class ValueModel
    {
        public int No { get; set; }
        public string TypeName { get; set; }
        public List<string> ValueList { get; set; } = new List<string>();
    }
}
