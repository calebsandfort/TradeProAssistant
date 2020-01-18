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
	public class WeeklyIncomeComboCountServiceBase : ServiceBase
	{
		#region SetIncludes
		private static DbQuery<WeeklyIncomeComboCount> SetIncludes(DbQuery<WeeklyIncomeComboCount> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static WeeklyIncomeComboCount Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static WeeklyIncomeComboCount Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = WeeklyIncomeComboCount.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static WeeklyIncomeComboCount Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<WeeklyIncomeComboCount> dbQuery = context.WeeklyIncomeComboCounts;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var weeklyincomecombocounts = dbQuery.Where(query.WhereClause).Take(1);

				return (weeklyincomecombocounts != null && weeklyincomecombocounts.Count() > 0) ? weeklyincomecombocounts.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<WeeklyIncomeComboCount> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<WeeklyIncomeComboCount> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<WeeklyIncomeComboCount> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = WeeklyIncomeComboCount.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = WeeklyIncomeComboCount.PropertyNames.Identifier;
					query.SortDescending = false;
				}

				DbQuery<WeeklyIncomeComboCount> dbQuery = context.WeeklyIncomeComboCounts;

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
						PropertyName = WeeklyIncomeComboCount.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.WeeklyIncomeComboCounts.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(WeeklyIncomeComboCount weeklyincomecombocount)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(weeklyincomecombocount).State = weeklyincomecombocount.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return weeklyincomecombocount.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<WeeklyIncomeComboCount> dbQuery = context.WeeklyIncomeComboCounts;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(WeeklyIncomeComboCount weeklyincomecombocount)
        {
            Delete(weeklyincomecombocount.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    WeeklyIncomeComboCount weeklyincomecombocount = context.WeeklyIncomeComboCounts.Find(identifier);
                    context.Entry(weeklyincomecombocount).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

