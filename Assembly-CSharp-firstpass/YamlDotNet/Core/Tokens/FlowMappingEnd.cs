using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	public class FlowMappingEnd : Token
	{
		// Token: 0x060010BD RID: 4285 RVA: 0x0004404F File Offset: 0x0004224F
		public FlowMappingEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00044061 File Offset: 0x00042261
		public FlowMappingEnd(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
