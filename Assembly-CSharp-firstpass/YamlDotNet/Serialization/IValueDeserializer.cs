using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000190 RID: 400
	public interface IValueDeserializer
	{
		// Token: 0x06000D16 RID: 3350
		object DeserializeValue(IParser parser, Type expectedType, SerializerState state, IValueDeserializer nestedObjectDeserializer);
	}
}
