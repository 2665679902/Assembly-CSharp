using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200017A RID: 378
	public abstract class EventInfo
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0003776A File Offset: 0x0003596A
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00037772 File Offset: 0x00035972
		public IObjectDescriptor Source { get; private set; }

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003777B File Offset: 0x0003597B
		protected EventInfo(IObjectDescriptor source)
		{
			this.Source = source;
		}
	}
}
