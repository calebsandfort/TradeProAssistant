using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Framework
{
    public abstract class QueryFilter
    {
        public bool IsAndFilter = false;

        public abstract String WhereClause
        {
            get;
        }
    }
}