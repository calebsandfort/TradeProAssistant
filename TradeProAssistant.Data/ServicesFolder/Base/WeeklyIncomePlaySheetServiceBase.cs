using System;
using System.Collections.Generic;
using System.Linq;
using Contexts;
using Data.Framework;
using Entities;
using System.Linq.Dynamic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Services
{
	public class WeeklyIncomePlaySheetServiceBase : ServiceBase
	{
		#region SetIncludes
		private static DbQuery<WeeklyIncomePlaySheet> SetIncludes(DbQuery<WeeklyIncomePlaySheet> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static WeeklyIncomePlaySheet Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static WeeklyIncomePlaySheet Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = WeeklyIncomePlaySheet.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static WeeklyIncomePlaySheet Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<WeeklyIncomePlaySheet> dbQuery = context.WeeklyIncomePlaySheets;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var weeklyincomeplaysheets = dbQuery.Where(query.WhereClause).Take(1);

				return (weeklyincomeplaysheets != null && weeklyincomeplaysheets.Count() > 0) ? weeklyincomeplaysheets.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<WeeklyIncomePlaySheet> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<WeeklyIncomePlaySheet> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<WeeklyIncomePlaySheet> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = WeeklyIncomePlaySheet.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = WeeklyIncomePlaySheet.PropertyNames.Identifier;
					query.SortDescending = false;
				}

				DbQuery<WeeklyIncomePlaySheet> dbQuery = context.WeeklyIncomePlaySheets;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				if (query.UsePaging)
				{
					return dbQuery.Where(query.WhereClause).OrderBy(query.SortExpression).Skip(query.CurrentPage * query.PageSize).Take(query.PageSize).ToList();
				}
				else
				{
					return dbQuery.Where(query.WhereClause).OrderBy(query.SortExpression).ToList();
				}
			}
        }
        #endregion

		#region GetCount
        public static int GetCount()
        {
            return GetCount(new Query());
        }

        public static int GetCount(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = WeeklyIncomePlaySheet.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.WeeklyIncomePlaySheets.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(WeeklyIncomePlaySheet weeklyincomeplaysheet)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(weeklyincomeplaysheet).State = weeklyincomeplaysheet.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return weeklyincomeplaysheet.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<WeeklyIncomePlaySheet> dbQuery = context.WeeklyIncomePlaySheets;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(WeeklyIncomePlaySheet weeklyincomeplaysheet)
        {
            Delete(weeklyincomeplaysheet.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    WeeklyIncomePlaySheet weeklyincomeplaysheet = context.WeeklyIncomePlaySheets.Find(identifier);
                    context.Entry(weeklyincomeplaysheet).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

