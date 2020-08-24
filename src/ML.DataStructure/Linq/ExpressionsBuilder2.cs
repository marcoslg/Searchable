using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ML.DataStructure.Linq.Entities;
using ML.DataStructure.Linq.Entities.Enums;

namespace ML.DataStructure.Linq
{
    public static class ExpressionsBuilder2
    {
        public static Func<IQueryable<TSource>, IOrderedQueryable<TSource>> GetOrderBy<TSource>(string propertyName, bool ascending = true) where TSource : class
        {
            var source = Expression.Parameter(typeof(IQueryable<TSource>), "source");
            var item = Expression.Parameter(typeof(TSource), "item");
            var member = Expression.Property(item, propertyName);
            var selector = Expression.Quote(Expression.Lambda(member, item));
            var body = Expression.Call(
                typeof(Queryable), ascending ? "OrderBy" : "OrderByDescending",
                new Type[] { item.Type, member.Type },
                source, selector);
            var expr = Expression.Lambda<Func<IQueryable<TSource>, IOrderedQueryable<TSource>>>(body, source);
            var func = expr.Compile();
            return func;
        }

        public static Expression<Func<TSource, bool>> GetFilter<TSource, TChild>(SearchV2 search)
            where TSource : class
            where TChild : class
        {
            Expression resultFinal = null;
            ParameterExpression parameterExpressionInvoice = Expression.Parameter(typeof(TSource), "source");
            ParameterExpression parameterExpressionInvoiceDetail = Expression.Parameter(typeof(TChild), "source2");

            foreach (var child in search.UnionFilterDescriptors)
            {
                var result = GetFilter<TChild>(child.FilterDescriptors, parameterExpressionInvoice, parameterExpressionInvoiceDetail);

                switch (child.UnionOperator)
                {
                    case UnionOperator.And:
                        resultFinal = resultFinal == null ? result : Expression.AndAlso(resultFinal, result);
                        break;
                    case UnionOperator.Or:
                        resultFinal = resultFinal == null ? result : Expression.OrElse(resultFinal, result);
                        break;
                }
            }

            if (resultFinal == null) return null;
            return Expression.Lambda<Func<TSource, bool>>(resultFinal, parameterExpressionInvoice);
        }

        private static Expression GetFilter<TChild>(List<FilterDescriptor> filterDescriptors,
            ParameterExpression parameterExpressionInvoice, ParameterExpression parameterExpressionInvoiceDetail)
            where TChild : class
        {
            Expression comparison = null;
            Expression result = null;

            if (filterDescriptors != null)
            {
                foreach (FilterDescriptor child in filterDescriptors)
                {
                    Expression member;
                    MethodInfo method;
                    switch (child.Operator)
                    {
                        case FilterOperator.IsEqualTo:
                            if (child.Member.Contains("."))
                            {
                                var parentProperty = child.Member.Split('.')[0];
                                var childProperty = child.Member.Split('.')[1];

                                var left = Expression.Property(parameterExpressionInvoiceDetail, childProperty);
                                var right = Expression.Constant(child.Value);
                                var equal = Expression.Equal(left, right);

                                var lambdaPredicate = Expression.Lambda<Func<TChild, bool>>(equal,
                                    parameterExpressionInvoiceDetail);

                                var colProperty = Expression.Property(parameterExpressionInvoice,
                                    parentProperty);
                                comparison = CallAny(colProperty, lambdaPredicate);
                            }
                            else
                            {
                                member = Expression.Property(parameterExpressionInvoice, child.Member);
                                comparison = Expression.Equal(member,
                                    Expression.Convert(Expression.Constant(child.Value), member.Type));
                            }
                            break;
                        case FilterOperator.IsNotEqualTo:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            comparison = Expression.NotEqual(member,
                                Expression.Convert(Expression.Constant(child.Value), member.Type));
                            break;
                        case FilterOperator.IsGreaterThan:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            comparison = Expression.GreaterThan(member,
                                Expression.Convert(Expression.Constant(child.Value), member.Type));
                            break;
                        case FilterOperator.IsGreaterThanOrEqualTo:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            comparison = Expression.GreaterThanOrEqual(member,
                                Expression.Convert(Expression.Constant(child.Value), member.Type));
                            break;
                        case FilterOperator.IsLessThan:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            comparison = Expression.LessThan(member,
                                Expression.Convert(Expression.Constant(child.Value), member.Type));
                            break;
                        case FilterOperator.IsLessThanOrEqualTo:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            comparison = Expression.LessThanOrEqual(member,
                                Expression.Convert(Expression.Constant(child.Value), member.Type));
                            break;
                        case FilterOperator.Contains:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            comparison = Expression.Call(member, method,
                                Expression.Constant(child.Value, typeof(string)));
                            break;
                        case FilterOperator.DoesNotContain:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            comparison =
                                Expression.Not(Expression.Call(member, method,
                                    Expression.Constant(child.Value, typeof(string))));
                            break;
                        case FilterOperator.EndsWith:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                            comparison = Expression.Call(member, method,
                                Expression.Constant(child.Value, typeof(string)));
                            break;
                        case FilterOperator.StartsWith:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                            comparison = Expression.Call(member, method,
                                Expression.Constant(child.Value, typeof(string)));
                            break;
                    }

                    result = result == null ? comparison : Expression.And(result, comparison);
                }
            }

            return result;
        }

        //public static Expression<Func<TSource, bool>> GetFilter<TSource, TChild>(Search search) where TSource : class where TChild : class
        //{
        //    ParameterExpression parameterExpressionInvoice = Expression.Parameter(typeof(TSource), "source");
        //    ParameterExpression parameterExpressionInvoiceDetail = Expression.Parameter(typeof(TChild), "source2");

        //    Expression comparison = null;
        //    Expression result = null;

        //    if (search.FilterDescriptors != null)
        //    {
        //        foreach (FilterDescriptor child in search.FilterDescriptors)
        //        {
        //            Expression member;
        //            MethodInfo method;
        //            switch (child.Operator)
        //            {
        //                case FilterOperator.IsEqualTo:
        //                    if (child.Member.Contains("."))
        //                    {
        //                        var parentProperty = child.Member.Split('.')[0];
        //                        var childProperty = child.Member.Split('.')[1];

        //                        var left = Expression.Property(parameterExpressionInvoiceDetail, childProperty);
        //                        var right = Expression.Constant(child.Value);
        //                        var equal = Expression.Equal(left, right);

        //                        var lambdaPredicate = Expression.Lambda<Func<TChild, Boolean>>(equal,
        //                            parameterExpressionInvoiceDetail);

        //                        var colProperty = Expression.Property(parameterExpressionInvoice,
        //                            parentProperty);
        //                        comparison = CallAny(colProperty, lambdaPredicate);
        //                    }
        //                    else
        //                    {
        //                        member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                        comparison = Expression.Equal(member,
        //                            Expression.Convert(Expression.Constant(child.Value), member.Type));
        //                    }
        //                    break;
        //                case FilterOperator.IsNotEqualTo:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    comparison = Expression.NotEqual(member,
        //                        Expression.Convert(Expression.Constant(child.Value), member.Type));
        //                    break;
        //                case FilterOperator.IsGreaterThan:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    comparison = Expression.GreaterThan(member,
        //                        Expression.Convert(Expression.Constant(child.Value), member.Type));
        //                    break;
        //                case FilterOperator.IsGreaterThanOrEqualTo:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    comparison = Expression.GreaterThanOrEqual(member,
        //                        Expression.Convert(Expression.Constant(child.Value), member.Type));
        //                    break;
        //                case FilterOperator.IsLessThan:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    comparison = Expression.LessThan(member,
        //                        Expression.Convert(Expression.Constant(child.Value), member.Type));
        //                    break;
        //                case FilterOperator.IsLessThanOrEqualTo:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    comparison = Expression.LessThanOrEqual(member,
        //                        Expression.Convert(Expression.Constant(child.Value), member.Type));
        //                    break;
        //                case FilterOperator.Contains:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //                    comparison = Expression.Call(member, method,
        //                        Expression.Constant(child.Value, typeof(string)));
        //                    break;
        //                case FilterOperator.DoesNotContain:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //                    comparison =
        //                        Expression.Not(Expression.Call(member, method,
        //                            Expression.Constant(child.Value, typeof(string))));
        //                    break;
        //                case FilterOperator.EndsWith:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        //                    comparison = Expression.Call(member, method,
        //                        Expression.Constant(child.Value, typeof(string)));
        //                    break;
        //                case FilterOperator.StartsWith:
        //                    member = Expression.Property(parameterExpressionInvoice, child.Member);
        //                    method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        //                    comparison = Expression.Call(member, method,
        //                        Expression.Constant(child.Value, typeof(string)));
        //                    break;
        //            }

        //            result = result == null ? comparison : Expression.And(result, comparison);
        //        }
        //    }

        //    if (result == null) return null;
        //    return Expression.Lambda<Func<TSource, bool>>(result, parameterExpressionInvoice);
        //}

        //Call any method on a child property of a collection using reflection
        static Expression CallAny(Expression collection, Expression predicateExpression)
        {
            Type cType = GetIEnumerableImpl(collection.Type);
            collection = Expression.Convert(collection, cType);

            Type elemType = cType.GetGenericArguments()[0];
            Type predType = typeof(Func<,>).MakeGenericType(elemType, typeof(bool));

            MethodInfo anyMethod = (MethodInfo)
                GetGenericMethod(typeof(Enumerable), "Any", new[] { elemType },
                    new[] { cType, predType }, BindingFlags.Static);

            return Expression.Call(
                anyMethod,
                collection,
                predicateExpression);
        }

        static MethodBase GetGenericMethod(Type type, string name, Type[] typeArgs,
            Type[] argTypes, BindingFlags flags)
        {
            int typeArity = typeArgs.Length;
            var methods = type.GetMethods()
                .Where(m => m.Name == name)
                .Where(m => m.GetGenericArguments().Length == typeArity)
                .Select(m => m.MakeGenericMethod(typeArgs));

            return Type.DefaultBinder.SelectMethod(flags, methods.ToArray(), argTypes, null);
        }

        static bool IsIEnumerable(Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        static Type GetIEnumerableImpl(Type type)
        {
            if (IsIEnumerable(type))
                return type;
            Type[] t = type.FindInterfaces((m, o) => IsIEnumerable(m), null);
            return t[0];
        }
    }
}
