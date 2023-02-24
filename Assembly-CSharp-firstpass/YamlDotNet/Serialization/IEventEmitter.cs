using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000183 RID: 387
	public interface IEventEmitter
	{
		// Token: 0x06000CEA RID: 3306
		void Emit(AliasEventInfo eventInfo, IEmitter emitter);

		// Token: 0x06000CEB RID: 3307
		void Emit(ScalarEventInfo eventInfo, IEmitter emitter);

		// Token: 0x06000CEC RID: 3308
		void Emit(MappingStartEventInfo eventInfo, IEmitter emitter);

		// Token: 0x06000CED RID: 3309
		void Emit(MappingEndEventInfo eventInfo, IEmitter emitter);

		// Token: 0x06000CEE RID: 3310
		void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter);

		// Token: 0x06000CEF RID: 3311
		void Emit(SequenceEndEventInfo eventInfo, IEmitter emitter);
	}
}
