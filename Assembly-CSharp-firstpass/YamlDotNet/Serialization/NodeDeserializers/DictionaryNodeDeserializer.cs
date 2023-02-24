using System;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Helpers;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001C9 RID: 457
	public sealed class DictionaryNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x0003A772 File Offset: 0x00038972
		public DictionaryNodeDeserializer(IObjectFactory objectFactory)
		{
			this._objectFactory = objectFactory;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003A784 File Offset: 0x00038984
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(expectedType, typeof(IDictionary<, >));
			Type type;
			Type type2;
			IDictionary dictionary;
			if (implementedGenericInterface != null)
			{
				Type[] genericArguments = implementedGenericInterface.GetGenericArguments();
				type = genericArguments[0];
				type2 = genericArguments[1];
				value = this._objectFactory.Create(expectedType);
				dictionary = value as IDictionary;
				if (dictionary == null)
				{
					dictionary = new GenericDictionaryToNonGenericAdapter(value, implementedGenericInterface);
				}
			}
			else
			{
				if (!typeof(IDictionary).IsAssignableFrom(expectedType))
				{
					value = null;
					return false;
				}
				type = typeof(object);
				type2 = typeof(object);
				value = this._objectFactory.Create(expectedType);
				dictionary = (IDictionary)value;
			}
			DictionaryNodeDeserializer.DeserializeHelper(type, type2, parser, nestedObjectDeserializer, dictionary);
			return true;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003A834 File Offset: 0x00038A34
		private static void DeserializeHelper(Type tKey, Type tValue, IParser parser, Func<IParser, Type, object> nestedObjectDeserializer, IDictionary result)
		{
			parser.Expect<MappingStart>();
			while (!parser.Accept<MappingEnd>())
			{
				object key = nestedObjectDeserializer(parser, tKey);
				IValuePromise valuePromise = key as IValuePromise;
				object value = nestedObjectDeserializer(parser, tValue);
				IValuePromise valuePromise2 = value as IValuePromise;
				if (valuePromise == null)
				{
					if (valuePromise2 == null)
					{
						result[key] = value;
					}
					else
					{
						valuePromise2.ValueAvailable += delegate(object v)
						{
							result[key] = v;
						};
					}
				}
				else if (valuePromise2 == null)
				{
					valuePromise.ValueAvailable += delegate(object v)
					{
						result[v] = value;
					};
				}
				else
				{
					bool hasFirstPart = false;
					valuePromise.ValueAvailable += delegate(object v)
					{
						if (hasFirstPart)
						{
							result[v] = value;
							return;
						}
						key = v;
						hasFirstPart = true;
					};
					valuePromise2.ValueAvailable += delegate(object v)
					{
						if (hasFirstPart)
						{
							result[key] = v;
							return;
						}
						value = v;
						hasFirstPart = true;
					};
				}
			}
			parser.Expect<MappingEnd>();
		}

		// Token: 0x04000835 RID: 2101
		private readonly IObjectFactory _objectFactory;
	}
}
