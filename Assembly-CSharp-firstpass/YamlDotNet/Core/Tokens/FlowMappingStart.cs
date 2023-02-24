using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000225 RID: 549
	[Serializable]
	public class FlowMappingStart : Token
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x0004406B File Offset: 0x0004226B
		public FlowMappingStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004407D File Offset: 0x0004227D
		public FlowMappingStart(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
