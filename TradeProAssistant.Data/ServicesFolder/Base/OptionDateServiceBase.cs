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
	public class OptionDateServiceBase
	{
		#region SetIncludes
		private static DbQuery<OptionDate> SetIncludes(DbQuery<OptionDate> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static OptionDate Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static OptionDate Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = OptionDate.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static OptionDate Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<OptionDate> dbQuery = context.OptionDates;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var optiondates = dbQuery.Where(query.WhereClause).Take(1);

				return (optiondates != null && optiondates.Count() > 0) ? optiondates.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<OptionDate> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<OptionDate> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<OptionDate> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = OptionDate.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = OptionDate.PropertyNames.ExpiryDate;
					query.SortDescending = false;
				}

				DbQuery<OptionDate> dbQuery = context.OptionDates;

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
						PropertyName = OptionDate.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.OptionDates.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(OptionDate optiondate)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(optiondate).State = optiondate.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return optiondate.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<OptionDate> dbQuery = context.OptionDates;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(OptionDate optiondate)
        {
            Delete(optiondate.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    OptionDate optiondate = context.OptionDates.Find(identifier);
                    context.Entry(optiondate).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

