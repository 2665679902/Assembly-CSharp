using System;
using System.Collections.Generic;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.EventEmitters
{
	// Token: 0x020001D7 RID: 471
	internal class CustomTagEventEmitter : ChainedEventEmitter
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x0003B1CF File Offset: 0x000393CF
		public CustomTagEventEmitter(IEventEmitter inner, IDictionary<Type, string> tagMappings)
			: base(inner)
		{
			this.tagMappings = tagMappings;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003B1DF File Offset: 0x000393DF
		public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			if (this.tagMappings.ContainsKey(eventInfo.Source.Type))
			{
				eventInfo.Tag = this.tagMappings[eventInfo.Source.Type];
			}
			base.Emit(eventInfo, emitter);
		}

		// Token: 0x04000840 RID: 2112
		private IDictionary<Type, string> tagMappings;
	}
}
