using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000226 RID: 550
	[Serializable]
	public class FlowSequenceEnd : Token
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x00044087 File Offset: 0x00042287
		public FlowSequenceEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00044099 File Offset: 0x00042299
		public FlowSequenceEnd(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
