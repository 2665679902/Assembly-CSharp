using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000196 RID: 406
	[Obsolete("Please use IYamlConvertible instead")]
	public interface IYamlSerializable
	{
		// Token: 0x06000D24 RID: 3364
		void ReadYaml(IParser parser);

		// Token: 0x06000D25 RID: 3365
		void WriteYaml(IEmitter emitter);
	}
}
