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
	public class PullbackTradeTicketServiceBase : ServiceBase
	{
		#region SetIncludes
		private static DbQuery<PullbackTradeTicket> SetIncludes(DbQuery<PullbackTradeTicket> dbQuery, List<String> includes)
        {
            foreach (String include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

			return dbQuery;
        }
		#endregion

		#region Get
        public static PullbackTradeTicket Get(int identifier)
        {
            return Get(identifier, new List<String>());
        }

		public static PullbackTradeTicket Get(int identifier, List<String> includes)
        {
            Query query = new Query();
			query.Includes = includes;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                IsAndFilter = false,
                Parameter = identifier.ToString(),
                PropertyName = PullbackTradeTicket.PropertyNames.Identifier,
                QueryOperator = QueryOperators.Equals
            });

            return Get(query);
        }

        public static PullbackTradeTicket Get(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				DbQuery<PullbackTradeTicket> dbQuery = context.PullbackTradeTickets;

				if(query.Includes.Count > 0)
				{
					dbQuery = SetIncludes(dbQuery, query.Includes);
				}

				var pullbacktradetickets = dbQuery.Where(query.WhereClause).Take(1);

				return (pullbacktradetickets != null && pullbacktradetickets.Count() > 0) ? pullbacktradetickets.First() : null;
			}
        }
        #endregion

		#region GetCollection
        public static List<PullbackTradeTicket> GetCollection()
        {
            return GetCollection(new List<String>());
        }

		public static List<PullbackTradeTicket> GetCollection(List<String> includes)
        {
            return GetCollection(new Query() { Includes = includes });
        }

        public static List<PullbackTradeTicket> GetCollection(Query query)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				if (String.IsNullOrEmpty(query.WhereClause))
				{
					query.QuerySingleFilters.Add(new QuerySingleFilter()
					{
						IsAndFilter = false,
						Parameter = "0",
						PropertyName = PullbackTradeTicket.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				if (String.IsNullOrEmpty(query.SortPropertyName))
				{
					query.SortPropertyName = PullbackTradeTicket.PropertyNames.Timestamp;
					query.SortDescending = false;
				}

				DbQuery<PullbackTradeTicket> dbQuery = context.PullbackTradeTickets;

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
						PropertyName = PullbackTradeTicket.PropertyNames.Identifier,
						QueryOperator = QueryOperators.NotEquals
					});
				}

				return context.PullbackTradeTickets.Count(query.WhereClause);
			}
        }
        #endregion

		#region Save
		public static int Save(PullbackTradeTicket pullbacktradeticket)
		{
			using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
				context.Entry(pullbacktradeticket).State = pullbacktradeticket.IsNew ?
										   EntityState.Added :
										   EntityState.Modified;
 
				context.SaveChanges();

				return pullbacktradeticket.Identifier;
			}
		}
		#endregion

		#region Delete
		public static void Delete(Query query)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DbQuery<PullbackTradeTicket> dbQuery = context.PullbackTradeTickets;
                List<int> identifiers = dbQuery.Where(query.WhereClause).Select(i => i.Identifier).ToList();

                foreach (int identifier in identifiers)
                {
                    Delete(identifier);
                }
            }
        }

        public static void Delete(PullbackTradeTicket pullbacktradeticket)
        {
            Delete(pullbacktradeticket.Identifier);
        }

        public static void Delete(int identifier)
        {
            using(TradeProAssistantContext context = new TradeProAssistantContext()) 
			{
                try
                {
                    PullbackTradeTicket pullbacktradeticket = context.PullbackTradeTickets.Find(identifier);
                    context.Entry(pullbacktradeticket).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch { }
            }
        }
        #endregion
	}
}

