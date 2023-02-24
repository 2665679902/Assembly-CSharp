using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200021A RID: 538
	[Serializable]
	public class Anchor : Token
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00043EE4 File Offset: 0x000420E4
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00043EEC File Offset: 0x000420EC
		public Anchor(string value)
			: this(value, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00043EFF File Offset: 0x000420FF
		public Anchor(string value, Mark start, Mark end)
			: base(start, end)
		{
			this.value = value;
		}

		// Token: 0x0400090C RID: 2316
		private readonly string value;
	}
}
