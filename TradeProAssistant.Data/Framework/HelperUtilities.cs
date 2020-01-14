using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Data.Framework
{
    public static class HelperUtilities
    {
        #region GetNextDayOfWeekOccurrence
        public static DateTime GetNextDayOfWeekOccurrence(this DateTime date, DayOfWeek dayOfWeek)
        {
            DateTime nextDayOfWeekOccurrence = date;

            while (nextDayOfWeekOccurrence.DayOfWeek != dayOfWeek)
            {
                nextDayOfWeekOccurrence = nextDayOfWeekOccurrence.AddDays(1);
            }

            return nextDayOfWeekOccurrence;
        }
        #endregion

        #region GetPreviousDayOfWeekOccurrence
        public static DateTime GetPreviousDayOfWeekOccurrence(this DateTime date, DayOfWeek dayOfWeek)
        {
            DateTime previousDayOfWeekOccurrence = date;

            while (previousDayOfWeekOccurrence.DayOfWeek != dayOfWeek)
            {
                previousDayOfWeekOccurrence = previousDayOfWeekOccurrence.AddDays(-1);
            }

            return previousDayOfWeekOccurrence;
        } 
        #endregion
    }
}