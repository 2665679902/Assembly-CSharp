using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001D0 RID: 464
	public sealed class YamlSerializableNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x0003B0B1 File Offset: 0x000392B1
		public YamlSerializableNodeDeserializer(IObjectFactory objectFactory)
		{
			this.objectFactory = objectFactory;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003B0C0 File Offset: 0x000392C0
		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			if (typeof(IYamlSerializable).IsAssignableFrom(expectedType))
			{
				IYamlSerializable yamlSerializable = (IYamlSerializable)this.objectFactory.Create(expectedType);
				yamlSerializable.ReadYaml(parser);
				value = yamlSerializable;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0400083E RID: 2110
		private readonly IObjectFactory objectFactory;
	}
}
