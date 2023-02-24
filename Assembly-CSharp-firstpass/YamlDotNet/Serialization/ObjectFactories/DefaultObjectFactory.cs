using System;
using System.Collections.Generic;

namespace YamlDotNet.Serialization.ObjectFactories
{
	// Token: 0x020001C0 RID: 448
	public sealed class DefaultObjectFactory : IObjectFactory
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x0003A324 File Offset: 0x00038524
		public object Create(Type type)
		{
			Type type2;
			if (type.IsInterface() && DefaultObjectFactory.defaultInterfaceImplementations.TryGetValue(type.GetGenericTypeDefinition(), out type2))
			{
				type = type2.MakeGenericType(type.GetGenericArguments());
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Format("Failed to create an instance of type '{0}'.", type), ex);
			}
			return obj;
		}

		// Token: 0x04000831 RID: 2097
		private static readonly Dictionary<Type, Type> defaultInterfaceImplementations = new Dictionary<Type, Type>
		{
			{
				typeof(IEnumerable<>),
				typeof(List<>)
			},
			{
				typeof(ICollection<>),
				typeof(List<>)
			},
			{
				typeof(IList<>),
				typeof(List<>)
			},
			{
				typeof(IDictionary<, >),
				typeof(Dictionary<, >)
			}
		};
	}
}
