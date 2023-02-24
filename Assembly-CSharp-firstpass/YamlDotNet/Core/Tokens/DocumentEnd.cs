using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class DocumentEnd : Token
	{
		// Token: 0x060010B7 RID: 4279 RVA: 0x00043FFB File Offset: 0x000421FB
		public DocumentEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004400D File Offset: 0x0004220D
		public DocumentEnd(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
