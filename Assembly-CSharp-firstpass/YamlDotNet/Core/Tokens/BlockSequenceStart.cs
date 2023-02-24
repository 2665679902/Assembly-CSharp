using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200021F RID: 543
	[Serializable]
	public class BlockSequenceStart : Token
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x00043F90 File Offset: 0x00042190
		public BlockSequenceStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00043FA2 File Offset: 0x000421A2
		public BlockSequenceStart(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
