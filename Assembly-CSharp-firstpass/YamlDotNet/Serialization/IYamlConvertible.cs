using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000193 RID: 403
	public interface IYamlConvertible
	{
		// Token: 0x06000D1A RID: 3354
		void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer);

		// Token: 0x06000D1B RID: 3355
		void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer);
	}
}
