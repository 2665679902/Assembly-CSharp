using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000180 RID: 384
	public sealed class SequenceStartEventInfo : ObjectEventInfo
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003785C File Offset: 0x00035A5C
		public SequenceStartEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00037865 File Offset: 0x00035A65
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0003786D File Offset: 0x00035A6D
		public bool IsImplicit { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00037876 File Offset: 0x00035A76
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0003787E File Offset: 0x00035A7E
		public SequenceStyle Style { get; set; }
	}
}
