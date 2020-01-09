using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class PropertyNamesBase
    {
        String _parent = String.Empty;

        public PropertyNamesBase(String parent)
        {
            _parent = parent;
        }

        protected String ResolvePropertyName(String propertyName)
        {
            if (!String.IsNullOrEmpty(_parent))
            {
                propertyName = String.Format("{0}.{1}", _parent, propertyName);
            }

            return propertyName;
        }
    }
}