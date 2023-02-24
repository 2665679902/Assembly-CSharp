using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.EventEmitters
{
	// Token: 0x020001D6 RID: 470
	public abstract class ChainedEventEmitter : IEventEmitter
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x0003B158 File Offset: 0x00039358
		protected ChainedEventEmitter(IEventEmitter nextEmitter)
		{
			if (nextEmitter == null)
			{
				throw new ArgumentNullException("nextEmitter");
			}
			this.nextEmitter = nextEmitter;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003B175 File Offset: 0x00039375
		public virtual void Emit(AliasEventInfo eventInfo, IEmitter emitter)
		{
			this.nextEmitter.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003B184 File Offset: 0x00039384
		public virtual void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
		{
			this.nextEmitter.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003B193 File Offset: 0x00039393
		public virtual void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			this.nextEmitter.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003B1A2 File Offset: 0x000393A2
		public virtual void Emit(MappingEndEventInfo eventInfo, IEmitter emitter)
		{
			this.nextEmitter.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003B1B1 File Offset: 0x000393B1
		public virtual void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
		{
			this.nextEmitter.Emit(eventInfo, emitter);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003B1C0 File Offset: 0x000393C0
		public virtual void Emit(SequenceEndEventInfo eventInfo, IEmitter emitter)
		{
			this.nextEmitter.Emit(eventInfo, emitter);
		}

		// Token: 0x0400083F RID: 2111
		protected readonly IEventEmitter nextEmitter;
	}
}
