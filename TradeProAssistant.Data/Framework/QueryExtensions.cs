using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Framework
{
    public static class QueryExtensions
    {
        public static String Parameterize(this String parameter)
        {
            return String.Format("\"{0}\"", parameter);
        }

        public static String Parameterize(this DateTime parameter)
        {
            return String.Format("'{0}'", parameter);
        }
    }
}
