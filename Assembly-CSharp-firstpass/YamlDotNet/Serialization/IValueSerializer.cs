using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000192 RID: 402
	public interface IValueSerializer
	{
		// Token: 0x06000D19 RID: 3353
		void SerializeValue(IEmitter emitter, object value, Type type);
	}
}
