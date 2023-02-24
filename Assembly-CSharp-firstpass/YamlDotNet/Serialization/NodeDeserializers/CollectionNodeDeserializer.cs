using System;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Helpers;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001C8 RID: 456
	public sealed class CollectionNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x0003A5AB File Offset: 0x000387AB
		public CollectionNodeDeserializer(IObjectFactory objectFactory)
		{
			this._objectFactory = objectFactory;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003A5BC File Offset: 0x000387BC
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			bool flag = true;
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(expectedType, typeof(ICollection<>));
			Type type;
			IList list;
			if (implementedGenericInterface != null)
			{
				type = implementedGenericInterface.GetGenericArguments()[0];
				value = this._objectFactory.Create(expectedType);
				list = value as IList;
				if (list == null)
				{
					Type implementedGenericInterface2 = ReflectionUtility.GetImplementedGenericInterface(expectedType, typeof(IList<>));
					flag = implementedGenericInterface2 != null;
					list = new GenericCollectionToNonGenericAdapter(value, implementedGenericInterface, implementedGenericInterface2);
				}
			}
			else
			{
				if (!typeof(IList).IsAssignableFrom(expectedType))
				{
					value = null;
					return false;
				}
				type = typeof(object);
				value = this._objectFactory.Create(expectedType);
				list = (IList)value;
			}
			CollectionNodeDeserializer.DeserializeHelper(type, parser, nestedObjectDeserializer, list, flag);
			return true;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003A67C File Offset: 0x0003887C
		internal static void DeserializeHelper(Type tItem, IParser parser, Func<IParser, Type, object> nestedObjectDeserializer, IList result, bool canUpdate)
		{
			parser.Expect<SequenceStart>();
			while (!parser.Accept<SequenceEnd>())
			{
				ParsingEvent parsingEvent = parser.Current;
				object obj = nestedObjectDeserializer(parser, tItem);
				IValuePromise valuePromise = obj as IValuePromise;
				if (valuePromise == null)
				{
					result.Add(TypeConverter.ChangeType(obj, tItem));
				}
				else
				{
					if (!canUpdate)
					{
						throw new ForwardAnchorNotSupportedException(parsingEvent.Start, parsingEvent.End, "Forward alias references are not allowed because this type does not implement IList<>");
					}
					int index = result.Add(tItem.IsValueType() ? Activator.CreateInstance(tItem) : null);
					valuePromise.ValueAvailable += delegate(object v)
					{
						result[index] = TypeConverter.ChangeType(v, tItem);
					};
				}
			}
			parser.Expect<SequenceEnd>();
		}

		// Token: 0x04000834 RID: 2100
		private readonly IObjectFactory _objectFactory;
	}
}
