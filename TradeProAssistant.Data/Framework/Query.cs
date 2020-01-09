using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Data.Framework
{
    [DataContract]
    public class Query
    {
        public static String ContainsFormatter = "%\'{0}\'%";

        public Query()
        {
            this.QuerySingleFilters = new List<QuerySingleFilter>();
            this.QueryFilterGroups = new List<QueryFilterGroup>();
            this.Includes = new List<string>();
        }

        #region Public Properties

        #region Includes
        [DataMember]
        public List<String> Includes { get; set; }
        #endregion

        #region QuerySingleFilters
        [DataMember]
        public List<QuerySingleFilter> QuerySingleFilters { get; set; }
        #endregion

        #region QueryFilterGroups
        [DataMember]
        public List<QueryFilterGroup> QueryFilterGroups { get; set; }
        #endregion

        #region PropertyName
        private String sortPropertyName = String.Empty;
        [DataMember]
        public String SortPropertyName
        {
            get
            {
                return sortPropertyName;
            }
            set
            {
                sortPropertyName = value;
            }
        }
        #endregion

        [DataMember]
        public bool UsePaging { get; set; }
        [DataMember]
        public int CurrentPage { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public bool SortDescending { get; set; }

        public String SortExpression
        {
            get
            {
                return String.Format("{0} {1}", this.SortPropertyName, this.SortDescending ? "desc" : "asc");
            }
        }

        #endregion

        #region Methods
        public String WhereClause
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