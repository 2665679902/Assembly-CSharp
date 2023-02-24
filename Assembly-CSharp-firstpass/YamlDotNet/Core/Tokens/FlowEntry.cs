using System;

namespace YamlDotNet.Core.Tokens
{
	// Token: 0x02000223 RID: 547
	[Serializable]
	public class FlowEntry : Token
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x00044033 File Offset: 0x00042233
		public FlowEntry()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00044045 File Offset: 0x00042245
		public FlowEntry(Mark start, Mark end)
			: base(start, end)
		{
		}
	}
}
