using Entities;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            return nextDayOfWeekOccurrence.Date.AddDays(1);
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

            return previousDayOfWeekOccurrence.Date;
        }
        #endregion

        #region ScrubHtml
        public static string ScrubHtml(this string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "", RegexOptions.Compiled).Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ", RegexOptions.Compiled);
            return step2;
        }
        #endregion

        #region FilterForImportantDates
        public static void FilterForImportantDates(this List<Security> securities)
        {
            DateTime monday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Monday);
            DateTime friday = DateTime.Now.GetNextDayOfWeekOccurrence(DayOfWeek.Friday);
            securities.RemoveAll(x => (x.NextEarningsDate.HasValue && x.NextEarningsDate >= monday && x.NextEarningsDate <= friday));
            securities.RemoveAll(x => (x.ExDividendDate.HasValue && x.ExDividendDate >= monday && x.ExDividendDate <= friday));
        }
        #endregion

        #region FilterForImportantDates
        public static void FilterForImportantDates(this List<SecurityDto> securities)
        {
            DateTime monday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Monday);
            DateTime friday = DateTime.Now.GetNextDayOfWeekOccurrence(DayOfWeek.Friday);
            securities.RemoveAll(x => (x.NextEarningsDate.HasValue && x.NextEarningsDate >= monday && x.NextEarningsDate <= friday));
            securities.RemoveAll(x => (x.ExDividendDate.HasValue && x.ExDividendDate >= monday && x.ExDividendDate <= friday));
        }
        #endregion
    }
}