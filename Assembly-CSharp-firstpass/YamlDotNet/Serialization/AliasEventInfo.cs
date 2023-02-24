using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200017B RID: 379
	public class AliasEventInfo : EventInfo
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x0003778A File Offset: 0x0003598A
		public AliasEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00037793 File Offset: 0x00035993
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0003779B File Offset: 0x0003599B
		public string Alias { get; set; }
	}
}
