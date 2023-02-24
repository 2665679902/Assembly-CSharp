using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000227 RID: 551
	[Serializable]
	public class FlowSequenceStart : Token
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x000440A3 File Offset: 0x000422A3
		public FlowSequenceStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000440B5 File Offset: 0x000422B5
		public FlowSequenceStart(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
