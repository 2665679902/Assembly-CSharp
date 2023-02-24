using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	public class Tag : Token
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0004415A File Offset: 0x0004235A
		public string Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00044162 File Offset: 0x00042362
		public string Suffix
		{
			get
			{
				return this.suffix;
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0004416A File Offset: 0x0004236A
		public Tag(string handle, string suffix)
			: this(handle, suffix, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0004417E File Offset: 0x0004237E
		public Tag(string handle, string suffix, Mark start, Mark end)
			: base(start, end)
		{
			this.handle = handle;
			this.suffix = suffix;
		}

		// Token: 0x04000912 RID: 2322
		private readonly string handle;

		// Token: 0x04000913 RID: 2323
		private readonly string suffix;
	}
}
