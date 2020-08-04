using System;
using System.Linq.Expressions;

namespace Authorize.Application.Extensions
{
    internal static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.And(left, right), parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.Or(left, right), parameter);
        }
        public static Expression<Func<T, bool>> CombinedAnd<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {

            if (left == null && right == null)
            {
                return null;
            }
            if (left == null)
            {
                return right;
            }

            if (right == null)
            {
                return left;
            }

            return left.And(right);

        }

        public static Expression<Func<T, bool>> CombinedOr<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {

            if (left == null && right == null)
            {
                return null;
            }
            if (left == null)
            {
                return right;
            }

            if (right == null)
            {
                return left;
            }

            return left.Or(right);

        }

        public static Func<T, bool> ConvertToFunc<T>(this Expression<Func<T, bool>> expression)
        {
            return expression.Compile();
        }

    }

    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
