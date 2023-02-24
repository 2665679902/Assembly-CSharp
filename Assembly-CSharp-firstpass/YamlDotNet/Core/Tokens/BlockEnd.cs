using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	public class BlockEnd : Token
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x00043F3C File Offset: 0x0004213C
		public BlockEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00043F4E File Offset: 0x0004214E
		public BlockEnd(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
