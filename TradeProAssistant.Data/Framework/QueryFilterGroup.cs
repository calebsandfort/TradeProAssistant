using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Data.Framework
{
    [KnownType(typeof(QueryFilter))]
    [DataContract]
    public class QueryFilterGroup
    {
        public QueryFilterGroup()
        {
            this.QuerySingleFilters = new List<QuerySingleFilter>();
            this.QueryFilterGroups = new List<QueryFilterGroup>();
        }

        #region Public Properties

        #region IsAndFilter
        [DataMember]
        public bool IsAndFilter { get; set; }
        #endregion

        #region QueryOperator
        [DataMember]
        public int QueryOperatorInt { get; set; }
        public QueryOperators QueryOperator
        {
            get
            {
                return (QueryOperators)QueryOperatorInt;
            }
            set
            {
                QueryOperatorInt = (int)value;
            }
        }
        #endregion

        #region QuerySingleFilters
        [DataMember]
        public List<QuerySingleFilter> QuerySingleFilters { get; set; }
        #endregion

        #region QueryFilterGroups
        [DataMember]
        public List<QueryFilterGroup> QueryFilterGroups { get; set; }
        #endregion

        #endregion

        #region Methods
        public string WhereClause
        {
            get
            {
                StringBuilder whereClause = new StringBuilder();

                if ((this.QuerySingleFilters != null && this.QuerySingleFilters.Count > 0) || (this.QueryFilterGroups != null && this.QueryFilterGroups.Count > 0))
                {
                    whereClause.Append("(");
                    bool firstFilter = true;

                    if (this.QuerySingleFilters != null && this.QuerySingleFilters.Count > 0)
                    {
                        foreach (QuerySingleFilter querySingleFilter in this.QuerySingleFilters)
                        {
                            if (!firstFilter)
                            {
                                whereClause.AppendFormat(" {0}", querySingleFilter.IsAndFilter.GetLogicOperatorSymbol());
                            }

                            whereClause.AppendFormat(" {0}", querySingleFilter.WhereClause);

                            firstFilter = false;
                        }
                    }

                    if (this.QueryFilterGroups != null && this.QueryFilterGroups.Count > 0)
                    {
                        foreach (QueryFilterGroup queryFilterGroup in this.QueryFilterGroups)
                        {
                            if (!firstFilter)
                            {
                                whereClause.AppendFormat(" {0}", queryFilterGroup.IsAndFilter.GetLogicOperatorSymbol());
                            }

                            whereClause.AppendFormat(" {0}", queryFilterGroup.WhereClause);

                            firstFilter = false;
                        }
                    }

                    whereClause.Append(")");
                }

                return whereClause.ToString();
            }
        }
        #endregion
    }
}