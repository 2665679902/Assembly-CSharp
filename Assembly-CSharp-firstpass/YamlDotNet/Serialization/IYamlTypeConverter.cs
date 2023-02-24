using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000197 RID: 407
	public interface IYamlTypeConverter
	{
		// Token: 0x06000D26 RID: 3366
		bool Accepts(Type type);

		// Token: 0x06000D27 RID: 3367
		object ReadYaml(IParser parser, Type type);

		// Token: 0x06000D28 RID: 3368
		void WriteYaml(IEmitter emitter, object value, Type type);
	}
}
