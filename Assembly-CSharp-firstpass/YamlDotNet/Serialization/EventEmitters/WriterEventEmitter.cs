using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.EventEmitters
{
	// Token: 0x020001DA RID: 474
	public sealed class WriterEventEmitter : IEventEmitter
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x0003B66B File Offset: 0x0003986B
		void IEventEmitter.Emit(AliasEventInfo eventInfo, IEmitter emitter)
		{
			emitter.Emit(new AnchorAlias(eventInfo.Alias));
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003B67E File Offset: 0x0003987E
		void IEventEmitter.Emit(ScalarEventInfo eventInfo, IEmitter emitter)
		{
			emitter.Emit(new Scalar(eventInfo.Anchor, eventInfo.Tag, eventInfo.RenderedValue, eventInfo.Style, eventInfo.IsPlainImplicit, eventInfo.IsQuotedImplicit));
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003B6AF File Offset: 0x000398AF
		void IEventEmitter.Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			emitter.Emit(new MappingStart(eventInfo.Anchor, eventInfo.Tag, eventInfo.IsImplicit, eventInfo.Style));
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003B6D4 File Offset: 0x000398D4
		void IEventEmitter.Emit(MappingEndEventInfo eventInfo, IEmitter emitter)
		{
			emitter.Emit(new MappingEnd());
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003B6E1 File Offset: 0x000398E1
		void IEventEmitter.Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
		{
			emitter.Emit(new SequenceStart(eventInfo.Anchor, eventInfo.Tag, eventInfo.IsImplicit, eventInfo.Style));
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003B706 File Offset: 0x00039906
		void IEventEmitter.Emit(SequenceEndEventInfo eventInfo, IEmitter emitter)
		{
			emitter.Emit(new SequenceEnd());
		}
	}
}
