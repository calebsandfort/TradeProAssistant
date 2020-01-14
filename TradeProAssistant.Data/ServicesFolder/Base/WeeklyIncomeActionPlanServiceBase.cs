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
	public class WeeklyIncomeActionPlanServiceBase : ServiceBase
	{
		#region SetIncludes
		private static DbQuery<WeeklyIncomeActionPlan> SetIncludes(DbQuery<WeeklyIncomeActionPlan> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static WeeklyIncomeActionPlan Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static WeeklyIncomeActionPlan Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = WeeklyIncomeActionPlan.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static WeeklyIncomeActionPlan Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<WeeklyIncomeActionPlan> dbQuery = context.WeeklyIncomeActionPlans;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var weeklyincomeactionplans = dbQuery.Where(query.WhereClause).Take(1);

				return (weeklyincomeactionplans != null && weeklyincomeactionplans.Count() > 0) ? weeklyincomeactionplans.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<WeeklyIncomeActionPlan> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<WeeklyIncomeActionPlan> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<WeeklyIncomeActionPlan> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = WeeklyIncomeActionPlan.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = WeeklyIncomeActionPlan.PropertyNames.Identifier;
					query.SortDescending = false;
				}

				DbQuery<WeeklyIncomeActionPlan> dbQuery = context.WeeklyIncomeActionPlans;

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
						PropertyName = WeeklyIncomeActionPlan.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.WeeklyIncomeActionPlans.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(WeeklyIncomeActionPlan weeklyincomeactionplan)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(weeklyincomeactionplan).State = weeklyincomeactionplan.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return weeklyincomeactionplan.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<WeeklyIncomeActionPlan> dbQuery = context.WeeklyIncomeActionPlans;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(WeeklyIncomeActionPlan weeklyincomeactionplan)
        {
            Delete(weeklyincomeactionplan.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    WeeklyIncomeActionPlan weeklyincomeactionplan = context.WeeklyIncomeActionPlans.Find(identifier);
                    context.Entry(weeklyincomeactionplan).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

