using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200022A RID: 554
	[Serializable]
	public class StreamEnd : Token
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x00044122 File Offset: 0x00042322
		public StreamEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00044134 File Offset: 0x00042334
		public StreamEnd(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
