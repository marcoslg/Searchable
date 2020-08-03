using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ML.DataStructure.Linq.Entities;
using ML.DataStructure.Linq.Entities.Enums;

namespace ML.DataStructure.Linq
{
    public static class ExpressionsBuilder
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

        public static Expression<Func<TSource, bool>> GetFilter<TSource>(Search search) where TSource : class
        {
            ParameterExpression parameterExpressionInvoice = Expression.Parameter(typeof(TSource), "source");

            Expression comparison = null;
            Expression result = null;

            if (search.FilterDescriptors != null)
            {
                foreach (FilterDescriptor child in search.FilterDescriptors)
                {
                    Expression member;
                    MethodInfo method;
                    switch (child.Operator)
                    {
                        case FilterOperator.IsEqualTo:
                            member = Expression.Property(parameterExpressionInvoice, child.Member);
                            comparison = Expression.Equal(member,
                                Expression.Convert(Expression.Constant(child.Value), member.Type));
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

            if (result == null) return null;
            return Expression.Lambda<Func<TSource, bool>>(result, parameterExpressionInvoice);
        }



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
