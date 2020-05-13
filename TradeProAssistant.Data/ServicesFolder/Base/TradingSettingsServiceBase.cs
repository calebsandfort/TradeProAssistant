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
	public class TradingSettingsServiceBase : ServiceBase
	{
		#region SetIncludes
		private static DbQuery<TradingSettings> SetIncludes(DbQuery<TradingSettings> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static TradingSettings Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static TradingSettings Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = TradingSettings.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static TradingSettings Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<TradingSettings> dbQuery = context.TradingSettings;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var tradingsettings = dbQuery.Where(query.WhereClause).Take(1);

				return (tradingsettings != null && tradingsettings.Count() > 0) ? tradingsettings.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<TradingSettings> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<TradingSettings> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<TradingSettings> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = TradingSettings.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = TradingSettings.PropertyNames.Identifier;
					query.SortDescending = false;
				}

				DbQuery<TradingSettings> dbQuery = context.TradingSettings;

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
						PropertyName = TradingSettings.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.TradingSettings.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(TradingSettings tradingsettings)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(tradingsettings).State = tradingsettings.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return tradingsettings.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<TradingSettings> dbQuery = context.TradingSettings;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(TradingSettings tradingsettings)
        {
            Delete(tradingsettings.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    TradingSettings tradingsettings = context.TradingSettings.Find(identifier);
                    context.Entry(tradingsettings).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

