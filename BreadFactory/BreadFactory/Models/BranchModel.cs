using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Models
{
    public class BranchModel
    {
        public int Level { get; set; }
        public int No { get; set; }
        public string LogicalName { get; set; }
        public string PhysicalName { get; set; }
        public List<BranchModel> ChildList { get; set; } = new List<BranchModel>();
    }
}
