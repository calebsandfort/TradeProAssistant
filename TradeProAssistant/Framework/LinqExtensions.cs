using Data.Framework;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace TradeProAssistant.Framework
{
    public static class LinqExtensions
    {
        #region Kendo
        public static Query ToQuery(this DataSourceRequest request)
        {
            Query query = new Query();

            #region Paging
            query.UsePaging = true;
            query.PageSize = request.PageSize;
            query.CurrentPage = request.Page - 1;
            #endregion

            #region Sort
            query.SortPropertyName = request.Sorts[0].Member;
            query.SortDescending = request.Sorts[0].SortDirection == System.ComponentModel.ListSortDirection.Descending;
            #endregion

            #region Filters
            if (request.Filters.Count > 0)
            {
                QueryFilterGroup qfg = new QueryFilterGroup() { IsAndFilter = true };
                foreach (IFilterDescriptor iFilterDescriptor in request.Filters)
                {
                    if (iFilterDescriptor is FilterDescriptor)
                    {
                        FilterDescriptor filterDescriptor = iFilterDescriptor as FilterDescriptor;
                        QueryOperators queryOperator = QueryOperators.None;

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.IsLessThan:
                                queryOperator = QueryOperators.LessThan;
                                break;
                            case FilterOperator.IsLessThanOrEqualTo:
                                queryOperator = QueryOperators.LessThanOrEqual;
                                break;
                            case FilterOperator.IsEqualTo:
                                queryOperator = QueryOperators.Equals;
                                break;
                            case FilterOperator.IsNotEqualTo:
                                queryOperator = QueryOperators.NotEquals;
                                break;
                            case FilterOperator.IsGreaterThanOrEqualTo:
                                queryOperator = QueryOperators.GreaterThanOrEqual;
                                break;
                            case FilterOperator.IsGreaterThan:
                                queryOperator = QueryOperators.GreaterThan;
                                break;
                            case FilterOperator.StartsWith:
                                break;
                            case FilterOperator.EndsWith:
                                break;
                            case FilterOperator.Contains:
                                break;
                            case FilterOperator.IsContainedIn:
                                break;
                            case FilterOperator.DoesNotContain:
                                break;
                            case FilterOperator.IsNull:
                                break;
                            case FilterOperator.IsNotNull:
                                break;
                            case FilterOperator.IsEmpty:
                                break;
                            case FilterOperator.IsNotEmpty:
                                break;
                            case FilterOperator.IsNullOrEmpty:
                                break;
                            case FilterOperator.IsNotNullOrEmpty:
                                break;
                            default:
                                break;
                        }

                        String parameter = filterDescriptor.Value.ToString();
                        if (filterDescriptor.Value is String)
                        {
                            parameter = String.Format("\'{0}\'", filterDescriptor.Value);
                        }

                        //filterDescriptor.v

                        if (queryOperator != QueryOperators.None)
                        {
                            qfg.QuerySingleFilters.Add(new QuerySingleFilter()
                            {
                                IsAndFilter = true,
                                PropertyName = filterDescriptor.Member,
                                QueryOperator = queryOperator,
                                Parameter = parameter
                            });
                        }
                    }
                }

                query.QueryFilterGroups.Add(qfg);
            }
            #endregion

            return query;
        }

        public static String SortExpression(this SortDescriptor sortDescriptor)
        {
            return String.Format("{0} {1}", sortDescriptor.Member, sortDescriptor.SortDirection == System.ComponentModel.ListSortDirection.Descending ? "desc" : "asc");
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, SortDescriptor sortDescriptor)
        {
            return source.OrderBy(sortDescriptor.Member, sortDescriptor.SortDirection == System.ComponentModel.ListSortDirection.Descending);
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                          bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, IList<IFilterDescriptor> filterDescriptors)
        {
            List<Expression> equalExpressions = new List<Expression>();
            ParameterExpression item = Expression.Parameter(typeof(TEntity), "item");

            IterateIFilterDescriptors(equalExpressions, item, filterDescriptors);

            if (equalExpressions.Count == 1)
            {
                source = source.Where(Expression.Lambda<Func<TEntity, bool>>(equalExpressions.First(), item));
            }
            else if (equalExpressions.Count == 2)
            {
                var and = Expression.And(equalExpressions[0], equalExpressions[1]);
                source = source.Where(Expression.Lambda<Func<TEntity, bool>>(and, item));
            }
            else if (equalExpressions.Count > 2)
            {
                var and = Expression.And(equalExpressions[0], equalExpressions[1]);

                for (int i = 2; i < equalExpressions.Count; i++)
                {
                    and = Expression.And(and, equalExpressions[i]);
                }

                source = source.Where(Expression.Lambda<Func<TEntity, bool>>(and, item));
            }

            return source;
        }

        public static Expression<Func<TEntity, bool>> ToLambda<TEntity>(this PropertyInfo pi, bool compare)
        {
            ParameterExpression item = Expression.Parameter(typeof(TEntity), "item");
            FilterDescriptor fd = new FilterDescriptor(pi.Name, FilterOperator.IsEqualTo, compare);
            Expression expression = fd.GetEqualityExpression(item);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(expression, item);
            return lambda;
        }

        public static void IterateIFilterDescriptors(List<Expression> equalExpressions, ParameterExpression item, IList<IFilterDescriptor> filterDescriptors, FilterCompositionLogicalOperator logicalOperator = FilterCompositionLogicalOperator.And)
        {
            Expression expression = null;

            if (filterDescriptors.Any(x => x is CompositeFilterDescriptor) && filterDescriptors.Count > 1)
            {
                List<Expression> tempEqualExpressions = filterDescriptors.Select(x => (x as FilterDescriptor).GetEqualityExpression(item)).ToList();
                Expression compositeExpression = null;

                if (tempEqualExpressions.Count == 2)
                {
                    if (logicalOperator == FilterCompositionLogicalOperator.And)
                        compositeExpression = Expression.And(tempEqualExpressions[0], tempEqualExpressions[1]);
                    else if (logicalOperator == FilterCompositionLogicalOperator.Or)
                        compositeExpression = Expression.Or(tempEqualExpressions[0], tempEqualExpressions[1]);
                }
                else if (tempEqualExpressions.Count > 2)
                {
                    if (logicalOperator == FilterCompositionLogicalOperator.And)
                        compositeExpression = Expression.And(tempEqualExpressions[0], tempEqualExpressions[1]);
                    else if (logicalOperator == FilterCompositionLogicalOperator.Or)
                        compositeExpression = Expression.Or(tempEqualExpressions[0], tempEqualExpressions[1]);

                    for (int i = 2; i < tempEqualExpressions.Count; i++)
                    {
                        if (logicalOperator == FilterCompositionLogicalOperator.And)
                            compositeExpression = Expression.And(compositeExpression, tempEqualExpressions[i]);
                        else if (logicalOperator == FilterCompositionLogicalOperator.Or)
                            compositeExpression = Expression.Or(compositeExpression, tempEqualExpressions[i]);
                    }
                }

                if (compositeExpression != null)
                {
                    equalExpressions.Add(compositeExpression);
                }
            }

            foreach (IFilterDescriptor iFilterDescriptor in filterDescriptors)
            {
                if (iFilterDescriptor is FilterDescriptor)
                {
                    expression = (iFilterDescriptor as FilterDescriptor).GetEqualityExpression(item);
                    if (expression != null)
                    {
                        equalExpressions.Add(expression);
                    }
                }
                else if (iFilterDescriptor is CompositeFilterDescriptor)
                {
                    IterateIFilterDescriptors(equalExpressions, item, (iFilterDescriptor as CompositeFilterDescriptor).FilterDescriptors, (iFilterDescriptor as CompositeFilterDescriptor).LogicalOperator);
                }
            }
        }

        public static Expression GetEqualityExpression(this FilterDescriptor filterDescriptor, ParameterExpression item)
        {
            var prop = Expression.Property(item, filterDescriptor.Member);
            ConstantExpression val = null;
            Expression expression = null;

            if (filterDescriptor.ConvertedValue is DateTime || filterDescriptor.ConvertedValue is DateTime?)
            {
                val = Expression.Constant((DateTime)filterDescriptor.ConvertedValue);
            }
            //else if (filterDescriptor.Member == "MpaaRating")
            //{
            //    val = Expression.Constant((MpaaRatings)Int32.Parse(filterDescriptor.ConvertedValue.ToString()));
            //}
            //else if (filterDescriptor.Member == "Genre")
            //{
            //    val = Expression.Constant((MovieGenres)Int32.Parse(filterDescriptor.ConvertedValue.ToString()));
            //}
            //else if (filterDescriptor.Member == "Brand")
            //{
            //    val = Expression.Constant((MovieBrands)Int32.Parse(filterDescriptor.ConvertedValue.ToString()));
            //}
            //else if (filterDescriptor.Member == "MicroSeries" || filterDescriptor.Member == "MacroSeries")
            //{
            //    val = Expression.Constant((MovieSeries)Int32.Parse(filterDescriptor.ConvertedValue.ToString()));
            //}
            else if (prop.Type == typeof(Int32) || prop.Type == typeof(int?))
            {
                val = Expression.Constant(Int32.Parse(filterDescriptor.ConvertedValue.ToString()));
            }
            else if (prop.Type == typeof(Double))
            {
                val = Expression.Constant(Double.Parse(filterDescriptor.ConvertedValue.ToString()));
            }
            else if (prop.Type == typeof(Decimal))
            {
                val = Expression.Constant(Decimal.Parse(filterDescriptor.ConvertedValue.ToString()));
            }
            else
            {
                val = Expression.Constant(filterDescriptor.ConvertedValue.ToString());
            }

            if (filterDescriptor.Operator == FilterOperator.IsEqualTo)
            {

                if (prop.Type == typeof(int?)) expression = Expression.Equal(prop, Expression.Convert(val, prop.Type));
                else if (prop.Type == typeof(DateTime?)) expression = Expression.Equal(prop, Expression.Convert(val, prop.Type));
                else if (prop.Type == typeof(Boolean))
                {
                    bool b = Boolean.Parse(val.Value.ToString());
                    ConstantExpression c = Expression.Constant(b, typeof(Boolean));
                    expression = Expression.Equal(prop, Expression.Convert(c, prop.Type));
                }
                else expression = expression = Expression.Equal(prop, val);
            }
            else if (filterDescriptor.Operator == FilterOperator.IsNotEqualTo)
            {
                if (prop.Type == typeof(DateTime?)) expression = Expression.NotEqual(prop, Expression.Convert(val, prop.Type));
                else expression = Expression.NotEqual(prop, val);
            }
            else if (filterDescriptor.Operator == FilterOperator.IsGreaterThan)
            {
                if (prop.Type == typeof(DateTime?)) expression = Expression.GreaterThan(prop, Expression.Convert(val, prop.Type));
                else expression = Expression.GreaterThan(prop, val);
            }
            else if (filterDescriptor.Operator == FilterOperator.IsGreaterThanOrEqualTo)
            {
                if (prop.Type == typeof(DateTime?)) expression = Expression.GreaterThanOrEqual(prop, Expression.Convert(val, prop.Type));
                else expression = Expression.GreaterThanOrEqual(prop, val);
            }
            else if (filterDescriptor.Operator == FilterOperator.IsLessThan)
            {
                if (prop.Type == typeof(DateTime?)) expression = Expression.LessThan(prop, Expression.Convert(val, prop.Type));
                else expression = Expression.LessThan(prop, val);
            }
            else if (filterDescriptor.Operator == FilterOperator.IsLessThanOrEqualTo)
            {
                if (prop.Type == typeof(DateTime?)) expression = Expression.LessThanOrEqual(prop, Expression.Convert(val, prop.Type));
                else expression = Expression.LessThanOrEqual(prop, val);
            }
            else if (filterDescriptor.Operator == FilterOperator.Contains)
            {
                expression = Expression.Call(prop, typeof(String).GetMethod("Contains"), val);
            }

            return expression;
        }

        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, FilterDescriptor filterDescriptor)
        {
            var item = Expression.Parameter(typeof(TEntity), "item");
            var prop = Expression.Property(item, filterDescriptor.Member);
            var val = Expression.Constant(filterDescriptor.ConvertedValue.ToString());
            var equal = Expression.Equal(prop, val);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);

            //string command = "Where";
            //var type = typeof(TEntity);
            //var property = type.GetProperty(filterDescriptor.Member);
            //var parameter = Expression.Parameter(type, "p");
            //var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            //var whereExpression = Expression.Lambda(propertyAccess, parameter);
            //var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
            //                              source.Expression, Expression.Quote(lambda));
            return source.Where(lambda);
            //return source.Provider.CreateQuery<TEntity>(filterDescriptor.CreateFilterExpression());
        }
        #endregion
    }
}