using System;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001CA RID: 458
	public sealed class EnumerableNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x0003A93C File Offset: 0x00038B3C
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			Type type;
			if (expectedType == typeof(IEnumerable))
			{
				type = typeof(object);
			}
			else
			{
				Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(expectedType, typeof(IEnumerable<>));
				if (implementedGenericInterface != expectedType)
				{
					value = null;
					return false;
				}
				type = implementedGenericInterface.GetGenericArguments()[0];
			}
			Type type2 = typeof(List<>).MakeGenericType(new Type[] { type });
			value = nestedObjectDeserializer(parser, type2);
			return true;
		}
	}
}
