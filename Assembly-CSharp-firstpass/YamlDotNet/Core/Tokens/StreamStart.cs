using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	public class StreamStart : Token
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x0004413E File Offset: 0x0004233E
		public StreamStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00044150 File Offset: 0x00042350
		public StreamStart(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
