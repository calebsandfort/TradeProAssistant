using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Data.Framework
{
    [KnownType(typeof(QueryFilter))]
    [DataContract]
    public class QuerySingleFilter
    {
        #region Public Properties

        #region IsAndFilter
        [DataMember]
        public bool IsAndFilter { get; set; }
        #endregion

        #region PropertyName
        [DataMember]
        public String PropertyName { get; set; }
        #endregion

        #region Parameter
        [DataMember]
        public String Parameter { get; set; }
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

        #endregion

        #region Methods
        public string WhereClause
        {
            get { return String.Format("{0} {1} {2}", this.PropertyName, this.QueryOperator.GetQueryOperatorSymbol(), this.Parameter); }
        }
        #endregion
    }
}