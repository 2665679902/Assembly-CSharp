using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	public class Comment : Token
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00043FAC File Offset: 0x000421AC
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00043FB4 File Offset: 0x000421B4
		public string Value { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00043FBD File Offset: 0x000421BD
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00043FC5 File Offset: 0x000421C5
		public bool IsInline { get; private set; }

		// Token: 0x060010B5 RID: 4277 RVA: 0x00043FCE File Offset: 0x000421CE
		public Comment(string value, bool isInline)
			: this(value, isInline, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00043FE2 File Offset: 0x000421E2
		public Comment(string value, bool isInline, Mark start, Mark end)
			: base(start, end)
		{
			this.IsInline = isInline;
			this.Value = value;
		}
	}
}
