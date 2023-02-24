using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000228 RID: 552
	[Serializable]
	public class Key : Token
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x000440BF File Offset: 0x000422BF
		public Key()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000440D1 File Offset: 0x000422D1
		public Key(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
