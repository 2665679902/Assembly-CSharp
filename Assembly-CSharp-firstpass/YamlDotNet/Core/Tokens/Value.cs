using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200022F RID: 559
	[Serializable]
	public class Value : Token
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x000442DB File Offset: 0x000424DB
		public Value()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000442ED File Offset: 0x000424ED
		public Value(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
