using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.ValueDeserializers
{
	// Token: 0x020001A9 RID: 425
	public sealed class NodeValueDeserializer : IValueDeserializer
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00038DB5 File Offset: 0x00036FB5
		public NodeValueDeserializer(IList<INodeDeserializer> deserializers, IList<INodeTypeResolver> typeResolvers)
		{
			if (deserializers == null)
			{
				throw new ArgumentNullException("deserializers");
			}
			this.deserializers = deserializers;
			if (typeResolvers == null)
			{
				throw new ArgumentNullException("typeResolvers");
			}
			this.typeResolvers = typeResolvers;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00038DE8 File Offset: 0x00036FE8
		public object DeserializeValue(IParser parser, Type expectedType, SerializerState state, IValueDeserializer nestedObjectDeserializer)
		{
			NodeEvent nodeEvent = parser.Peek<NodeEvent>();
			Type typeFromEvent = this.GetTypeFromEvent(nodeEvent, expectedType);
			try
			{
				Func<IParser, Type, object> <>9__0;
				foreach (INodeDeserializer nodeDeserializer in this.deserializers)
				{
					Type type = typeFromEvent;
					Func<IParser, Type, object> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (IParser r, Type t) => nestedObjectDeserializer.DeserializeValue(r, t, state, nestedObjectDeserializer));
					}
					object obj;
					if (nodeDeserializer.Deserialize(parser, type, func, out obj))
					{
						return obj;
					}
				}
			}
			catch (YamlException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new YamlException(nodeEvent.Start, nodeEvent.End, "Exception during deserialization", ex);
			}
			throw new YamlException(nodeEvent.Start, nodeEvent.End, string.Format("No node deserializer was able to deserialize the node into type {0}", expectedType.AssemblyQualifiedName));
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00038EE0 File Offset: 0x000370E0
		private Type GetTypeFromEvent(NodeEvent nodeEvent, Type currentType)
		{
			using (IEnumerator<INodeTypeResolver> enumerator = this.typeResolvers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Resolve(nodeEvent, ref currentType))
					{
						break;
					}
				}
			}
			return currentType;
		}

		// Token: 0x04000816 RID: 2070
		private readonly IList<INodeDeserializer> deserializers;

		// Token: 0x04000817 RID: 2071
		private readonly IList<INodeTypeResolver> typeResolvers;
	}
}
