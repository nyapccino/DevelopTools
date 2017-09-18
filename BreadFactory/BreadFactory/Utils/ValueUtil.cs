using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Utils
{
    public class ValueUtil
    {
        public static bool IsValid(string value)
        {
            return (!string.IsNullOrEmpty(value) && value != "-");
        }

        public static object GetValue(IEnumerable<string> values)
        {
            if (values.Count() == 1)
            {
                return values.First();
            }
            else
            {
                return values;
            }
        }
    }
}
