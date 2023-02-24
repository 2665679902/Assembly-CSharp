using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200021E RID: 542
	[Serializable]
	public class BlockMappingStart : Token
	{
		// Token: 0x060010AD RID: 4269 RVA: 0x00043F74 File Offset: 0x00042174
		public BlockMappingStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00043F86 File Offset: 0x00042186
		public BlockMappingStart(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
