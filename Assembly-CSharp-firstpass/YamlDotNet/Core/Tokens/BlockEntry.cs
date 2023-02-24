using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	public class BlockEntry : Token
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x00043F58 File Offset: 0x00042158
		public BlockEntry()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00043F6A File Offset: 0x0004216A
		public BlockEntry(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
