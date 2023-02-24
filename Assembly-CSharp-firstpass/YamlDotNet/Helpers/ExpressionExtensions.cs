using System;
using System.Linq.Expressions;
using System.Reflection;

namespace YamlDotNet.Helpers
{
	// Token: 0x020001EB RID: 491
	public static class ExpressionExtensions
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x0003CEEC File Offset: 0x0003B0EC
		public static PropertyInfo AsProperty(this LambdaExpression propertyAccessor)
		{
			PropertyInfo propertyInfo = ExpressionExtensions.TryGetMemberExpression<PropertyInfo>(propertyAccessor);
			if (propertyInfo == null)
			{
				throw new ArgumentException("Expected a lambda expression in the form: x => x.SomeProperty", "propertyAccessor");
			}
			return propertyInfo;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003CF10 File Offset: 0x0003B110
		private static TMemberInfo TryGetMemberExpression<TMemberInfo>(LambdaExpression lambdaExpression) where TMemberInfo : MemberInfo
		{
			if (lambdaExpression.Parameters.Count != 1)
			{
				return default(TMemberInfo);
			}
			Expression expression = lambdaExpression.Body;
			UnaryExpression unaryExpression = expression as UnaryExpression;
			if (unaryExpression != null)
			{
				if (unaryExpression.NodeType != ExpressionType.Convert)
				{
					return default(TMemberInfo);
				}
				expression = unaryExpression.Operand;
			}
			MemberExpression memberExpression = expression as MemberExpression;
			if (memberExpression == null)
			{
				return default(TMemberInfo);
			}
			if (memberExpression.Expression != lambdaExpression.Parameters[0])
			{
				return default(TMemberInfo);
			}
			return memberExpression.Member as TMemberInfo;
		}
	}
}
