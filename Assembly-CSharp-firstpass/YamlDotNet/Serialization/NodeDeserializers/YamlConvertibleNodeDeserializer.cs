using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001CF RID: 463
	public sealed class YamlConvertibleNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E37 RID: 3639 RVA: 0x0003B038 File Offset: 0x00039238
		public YamlConvertibleNodeDeserializer(IObjectFactory objectFactory)
		{
			this.objectFactory = objectFactory;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003B048 File Offset: 0x00039248
		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			if (typeof(IYamlConvertible).IsAssignableFrom(expectedType))
			{
				IYamlConvertible yamlConvertible = (IYamlConvertible)this.objectFactory.Create(expectedType);
				yamlConvertible.Read(parser, expectedType, (Type type) => nestedObjectDeserializer(parser, type));
				value = yamlConvertible;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0400083D RID: 2109
		private readonly IObjectFactory objectFactory;
	}
}
