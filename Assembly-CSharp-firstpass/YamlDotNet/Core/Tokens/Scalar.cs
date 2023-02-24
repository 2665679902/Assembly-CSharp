using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000229 RID: 553
	[Serializable]
	public class Scalar : Token
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x000440DB File Offset: 0x000422DB
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000440E3 File Offset: 0x000422E3
		public ScalarStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000440EB File Offset: 0x000422EB
		public Scalar(string value)
			: this(value, ScalarStyle.Any)
		{
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000440F5 File Offset: 0x000422F5
		public Scalar(string value, ScalarStyle style)
			: this(value, style, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00044109 File Offset: 0x00042309
		public Scalar(string value, ScalarStyle style, Mark start, Mark end)
			: base(start, end)
		{
			this.value = value;
			this.style = style;
		}

		// Token: 0x04000910 RID: 2320
		private readonly string value;

		// Token: 0x04000911 RID: 2321
		private readonly ScalarStyle style;
	}
}
