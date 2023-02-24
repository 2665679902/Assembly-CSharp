using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000185 RID: 389
	public interface INodeDeserializer
	{
		// Token: 0x06000CF1 RID: 3313
		bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value);
	}
}
