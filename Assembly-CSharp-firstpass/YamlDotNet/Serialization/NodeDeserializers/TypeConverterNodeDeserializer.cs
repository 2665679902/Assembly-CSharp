using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001CE RID: 462
	public sealed class TypeConverterNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x0003AFCC File Offset: 0x000391CC
		public TypeConverterNodeDeserializer(IEnumerable<IYamlTypeConverter> converters)
		{
			if (converters == null)
			{
				throw new ArgumentNullException("converters");
			}
			this.converters = converters;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003AFEC File Offset: 0x000391EC
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			IYamlTypeConverter yamlTypeConverter = this.converters.FirstOrDefault((IYamlTypeConverter c) => c.Accepts(expectedType));
			if (yamlTypeConverter == null)
			{
				value = null;
				return false;
			}
			value = yamlTypeConverter.ReadYaml(parser, expectedType);
			return true;
		}

		// Token: 0x0400083C RID: 2108
		private readonly IEnumerable<IYamlTypeConverter> converters;
	}
}
