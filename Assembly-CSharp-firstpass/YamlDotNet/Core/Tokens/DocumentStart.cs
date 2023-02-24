using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	public class DocumentStart : Token
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x00044017 File Offset: 0x00042217
		public DocumentStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00044029 File Offset: 0x00042229
		public DocumentStart(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
