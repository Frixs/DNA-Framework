using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ixs.DNA.Extensions
{
    /// <summary>
    ///     A helper for expressions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        ///     Compiles an expression and gets the functions return value.
        /// </summary>
        /// <typeparam name="T">The type of return value.</typeparam>
        /// <param name="lambda">The expression to compile.</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        ///     Compiles an expression and gets the functions return value
        /// </summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <typeparam name="TIn">The input to the expression</typeparam>
        /// <param name="lambda">The expression to compile</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T GetPropertyValue<TIn, T>(this Expression<Func<TIn, T>> lambda, TIn input)
        {
            return lambda.Compile().Invoke(input);
        }

        /// <summary>
        ///     Sets the underlying properties value to the given value from an expresion that contains the property.
        /// </summary>
        /// <typeparam name="T">The type of value to set.</typeparam>
        /// <param name="lambda">The expression.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            // Converts a lambda () => some.Property, to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // Get the property information so we can set it.
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            // Set the property value.
            propertyInfo.SetValue(target, value);
        }

        /// <summary>
        ///     Sets the underlying properties value to the given value
        ///     from an expression that contains the property
        /// </summary>
        /// <typeparam name="T">The type of value to set</typeparam>
        /// <typeparam name="TIn">The input to the expression</typeparam>
        /// <param name="lambda">The expression</param>
        /// <param name="value">The value to set the property to</param>
        /// <param name="input"></param>
        public static void SetPropertyValue<TIn, T>(this Expression<Func<TIn, T>> lambda, T value, TIn input)
        {
            // Converts a lambda () => some.Property, to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // Get the property information so we can set it
            var propertyInfo = (PropertyInfo)expression.Member;

            // Set the property value
            propertyInfo.SetValue(input, value);
        }
    }
}
