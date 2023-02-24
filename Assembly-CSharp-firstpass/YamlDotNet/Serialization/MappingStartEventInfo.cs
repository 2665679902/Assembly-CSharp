using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200017E RID: 382
	public sealed class MappingStartEventInfo : ObjectEventInfo
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x00037828 File Offset: 0x00035A28
		public MappingStartEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00037831 File Offset: 0x00035A31
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00037839 File Offset: 0x00035A39
		public bool IsImplicit { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00037842 File Offset: 0x00035A42
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0003784A File Offset: 0x00035A4A
		public MappingStyle Style { get; set; }
	}
}
