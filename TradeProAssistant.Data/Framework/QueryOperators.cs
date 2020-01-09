using System;

namespace Data.Framework
{
    public enum QueryOperators
    {
        [StringValue("None")]
        None = -1,
        [StringValue("Equals")]
        Equals = 0,
        [StringValue("Not Equals")]
        NotEquals = 1,
        [StringValue("Greater Than")]
        GreaterThan = 2,
        [StringValue("Less Than")]
        LessThan = 3,
        [StringValue("All")]
        All = 4,
        [StringValue("And")]
        And = 5,
        [StringValue("Greater Than or Equal")]
        GreaterThanOrEqual = 6,
        [StringValue("Less Than or Equal")]
        LessThanOrEqual = 7,
        [StringValue("Contains")]
        Contains = 8,
    }
}
