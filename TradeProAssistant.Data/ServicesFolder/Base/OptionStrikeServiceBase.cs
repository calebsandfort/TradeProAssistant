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
	public class OptionStrikeServiceBase
	{
		#region SetIncludes
		private static DbQuery<OptionStrike> SetIncludes(DbQuery<OptionStrike> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static OptionStrike Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static OptionStrike Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = OptionStrike.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static OptionStrike Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<OptionStrike> dbQuery = context.OptionStrikes;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var optionstrikes = dbQuery.Where(query.WhereClause).Take(1);

				return (optionstrikes != null && optionstrikes.Count() > 0) ? optionstrikes.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<OptionStrike> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<OptionStrike> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<OptionStrike> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = OptionStrike.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = OptionStrike.PropertyNames.StrikePrice;
					query.SortDescending = false;
				}

				DbQuery<OptionStrike> dbQuery = context.OptionStrikes;

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
						PropertyName = OptionStrike.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.OptionStrikes.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(OptionStrike optionstrike)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(optionstrike).State = optionstrike.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return optionstrike.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<OptionStrike> dbQuery = context.OptionStrikes;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(OptionStrike optionstrike)
        {
            Delete(optionstrike.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    OptionStrike optionstrike = context.OptionStrikes.Find(identifier);
                    context.Entry(optionstrike).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

