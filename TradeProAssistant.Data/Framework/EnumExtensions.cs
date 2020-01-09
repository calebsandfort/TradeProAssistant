using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Framework
{
    #region Enum Attributes
    #region StringValueAttribute
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

    }
    #endregion
    #endregion

    #region Enum Extensions
    public static class EnumExtensions
    {
        #region GetStringValue
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
        #endregion

        #region GetQueryOperatorSymbol
        public static String GetQueryOperatorSymbol(this QueryOperators value)
        {
            String queryOperatorSymbol = String.Empty;

            switch (value)
            {
                case QueryOperators.Equals:
                    queryOperatorSymbol = "=";
                    break;
                case QueryOperators.NotEquals:
                    queryOperatorSymbol = "!=";
                    break;
                case QueryOperators.GreaterThan:
                    queryOperatorSymbol = ">";
                    break;
                case QueryOperators.LessThan:
                    queryOperatorSymbol = "<";
                    break;
                case QueryOperators.GreaterThanOrEqual:
                    queryOperatorSymbol = ">=";
                    break;
                case QueryOperators.LessThanOrEqual:
                    queryOperatorSymbol = "<=";
                    break;
                case QueryOperators.Contains:
                    queryOperatorSymbol = "LIKE";
                    break;
            }

            return queryOperatorSymbol;
        }
        #endregion

        #region GetLogicOperatorSymbol
        public static String GetLogicOperatorSymbol(this bool value)
        {
            return value ? "AND" : "OR";
        }
        #endregion
    }
    #endregion
}
