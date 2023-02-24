using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200022E RID: 558
	[Serializable]
	public abstract class Token
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x000442B5 File Offset: 0x000424B5
		public Mark Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000442BD File Offset: 0x000424BD
		public Mark End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000442C5 File Offset: 0x000424C5
		protected Token(Mark start, Mark end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x04000917 RID: 2327
		private readonly Mark start;

		// Token: 0x04000918 RID: 2328
		private readonly Mark end;
	}
}
