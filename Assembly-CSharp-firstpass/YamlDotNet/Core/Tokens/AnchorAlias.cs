using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	public class AnchorAlias : Token
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00043F10 File Offset: 0x00042110
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00043F18 File Offset: 0x00042118
		public AnchorAlias(string value)
			: this(value, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00043F2B File Offset: 0x0004212B
		public AnchorAlias(string value, Mark start, Mark end)
			: base(start, end)
		{
			this.value = value;
		}

		// Token: 0x0400090D RID: 2317
		private readonly string value;
	}
}
