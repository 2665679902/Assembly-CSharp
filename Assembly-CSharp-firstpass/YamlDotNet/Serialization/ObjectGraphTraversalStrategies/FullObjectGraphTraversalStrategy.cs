using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Helpers;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.ObjectGraphTraversalStrategies
{
	// Token: 0x020001BE RID: 446
	public class FullObjectGraphTraversalStrategy : IObjectGraphTraversalStrategy
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x00039D84 File Offset: 0x00037F84
		public FullObjectGraphTraversalStrategy(ITypeInspector typeDescriptor, ITypeResolver typeResolver, int maxRecursion, INamingConvention namingConvention)
		{
			if (maxRecursion <= 0)
			{
				throw new ArgumentOutOfRangeException("maxRecursion", maxRecursion, "maxRecursion must be greater than 1");
			}
			if (typeDescriptor == null)
			{
				throw new ArgumentNullException("typeDescriptor");
			}
			this.typeDescriptor = typeDescriptor;
			if (typeResolver == null)
			{
				throw new ArgumentNullException("typeResolver");
			}
			this.typeResolver = typeResolver;
			this.maxRecursion = maxRecursion;
			this.namingConvention = namingConvention;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00039DEA File Offset: 0x00037FEA
		void IObjectGraphTraversalStrategy.Traverse<TContext>(IObjectDescriptor graph, IObjectGraphVisitor<TContext> visitor, TContext context)
		{
			this.Traverse<TContext>(graph, visitor, 0, context);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00039DF8 File Offset: 0x00037FF8
		protected virtual void Traverse<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, int currentDepth, TContext context)
		{
			if (++currentDepth > this.maxRecursion)
			{
				throw new InvalidOperationException("Too much recursion when traversing the object graph");
			}
			if (!visitor.Enter(value, context))
			{
				return;
			}
			TypeCode typeCode = value.Type.GetTypeCode();
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "TypeCode.{0} is not supported.", typeCode));
			case TypeCode.DBNull:
				visitor.VisitScalar(new ObjectDescriptor(null, typeof(object), typeof(object)), context);
				return;
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				visitor.VisitScalar(value, context);
				return;
			}
			if (value.Value == null || value.Type == typeof(TimeSpan))
			{
				visitor.VisitScalar(value, context);
				return;
			}
			Type underlyingType = Nullable.GetUnderlyingType(value.Type);
			if (underlyingType != null)
			{
				this.Traverse<TContext>(new ObjectDescriptor(value.Value, underlyingType, value.Type, value.ScalarStyle), visitor, currentDepth, context);
				return;
			}
			this.TraverseObject<TContext>(value, visitor, currentDepth, context);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00039F40 File Offset: 0x00038140
		protected virtual void TraverseObject<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, int currentDepth, TContext context)
		{
			if (typeof(IDictionary).IsAssignableFrom(value.Type))
			{
				this.TraverseDictionary<TContext>(value, visitor, currentDepth, typeof(object), typeof(object), context);
				return;
			}
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(value.Type, typeof(IDictionary<, >));
			if (implementedGenericInterface != null)
			{
				GenericDictionaryToNonGenericAdapter genericDictionaryToNonGenericAdapter = new GenericDictionaryToNonGenericAdapter(value.Value, implementedGenericInterface);
				Type[] genericArguments = implementedGenericInterface.GetGenericArguments();
				this.TraverseDictionary<TContext>(new ObjectDescriptor(genericDictionaryToNonGenericAdapter, value.Type, value.StaticType, value.ScalarStyle), visitor, currentDepth, genericArguments[0], genericArguments[1], context);
				return;
			}
			if (typeof(IEnumerable).IsAssignableFrom(value.Type))
			{
				this.TraverseList<TContext>(value, visitor, currentDepth, context);
				return;
			}
			this.TraverseProperties<TContext>(value, visitor, currentDepth, context);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003A010 File Offset: 0x00038210
		protected virtual void TraverseDictionary<TContext>(IObjectDescriptor dictionary, IObjectGraphVisitor<TContext> visitor, int currentDepth, Type keyType, Type valueType, TContext context)
		{
			visitor.VisitMappingStart(dictionary, keyType, valueType, context);
			bool flag = dictionary.Type.FullName.Equals("System.Dynamic.ExpandoObject");
			foreach (object obj in ((IDictionary)dictionary.Value))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (flag ? this.namingConvention.Apply(dictionaryEntry.Key.ToString()) : dictionaryEntry.Key.ToString());
				IObjectDescriptor objectDescriptor = this.GetObjectDescriptor(text, keyType);
				IObjectDescriptor objectDescriptor2 = this.GetObjectDescriptor(dictionaryEntry.Value, valueType);
				if (visitor.EnterMapping(objectDescriptor, objectDescriptor2, context))
				{
					this.Traverse<TContext>(objectDescriptor, visitor, currentDepth, context);
					this.Traverse<TContext>(objectDescriptor2, visitor, currentDepth, context);
				}
			}
			visitor.VisitMappingEnd(dictionary, context);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003A104 File Offset: 0x00038304
		private void TraverseList<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, int currentDepth, TContext context)
		{
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(value.Type, typeof(IEnumerable<>));
			Type type = ((implementedGenericInterface != null) ? implementedGenericInterface.GetGenericArguments()[0] : typeof(object));
			visitor.VisitSequenceStart(value, type, context);
			foreach (object obj in ((IEnumerable)value.Value))
			{
				this.Traverse<TContext>(this.GetObjectDescriptor(obj, type), visitor, currentDepth, context);
			}
			visitor.VisitSequenceEnd(value, context);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003A1B4 File Offset: 0x000383B4
		protected virtual void TraverseProperties<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, int currentDepth, TContext context)
		{
			visitor.VisitMappingStart(value, typeof(string), typeof(object), context);
			foreach (IPropertyDescriptor propertyDescriptor in this.typeDescriptor.GetProperties(value.Type, value.Value))
			{
				IObjectDescriptor objectDescriptor = propertyDescriptor.Read(value.Value);
				if (visitor.EnterMapping(propertyDescriptor, objectDescriptor, context))
				{
					this.Traverse<TContext>(new ObjectDescriptor(propertyDescriptor.Name, typeof(string), typeof(string)), visitor, currentDepth, context);
					this.Traverse<TContext>(objectDescriptor, visitor, currentDepth, context);
				}
			}
			visitor.VisitMappingEnd(value, context);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003A280 File Offset: 0x00038480
		private IObjectDescriptor GetObjectDescriptor(object value, Type staticType)
		{
			return new ObjectDescriptor(value, this.typeResolver.Resolve(staticType, value), staticType);
		}

		// Token: 0x0400082C RID: 2092
		private readonly int maxRecursion;

		// Token: 0x0400082D RID: 2093
		private readonly ITypeInspector typeDescriptor;

		// Token: 0x0400082E RID: 2094
		private readonly ITypeResolver typeResolver;

		// Token: 0x0400082F RID: 2095
		private INamingConvention namingConvention;
	}
}
