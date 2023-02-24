using System;

namespace ProcGen.Noise
{
	// Token: 0x020004F3 RID: 1267
	public class Link
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x00077ECA File Offset: 0x000760CA
		// (set) Token: 0x060036B3 RID: 14003 RVA: 0x00077ED2 File Offset: 0x000760D2
		public Link.Type type { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x00077EDB File Offset: 0x000760DB
		// (set) Token: 0x060036B5 RID: 14005 RVA: 0x00077EE3 File Offset: 0x000760E3
		public string name { get; set; }

		// Token: 0x060036B6 RID: 14006 RVA: 0x00077EEC File Offset: 0x000760EC
		public Link()
		{
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00077EF4 File Offset: 0x000760F4
		public Link(Link.Type type, string name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x02000B17 RID: 2839
		public enum Type
		{
			// Token: 0x0400261B RID: 9755
			None,
			// Token: 0x0400261C RID: 9756
			Primitive,
			// Token: 0x0400261D RID: 9757
			Filter,
			// Token: 0x0400261E RID: 9758
			Transformer,
			// Token: 0x0400261F RID: 9759
			Selector,
			// Token: 0x04002620 RID: 9760
			Modifier,
			// Token: 0x04002621 RID: 9761
			Combiner,
			// Token: 0x04002622 RID: 9762
			FloatPoints,
			// Token: 0x04002623 RID: 9763
			ControlPoints,
			// Token: 0x04002624 RID: 9764
			Terminator
		}
	}
}
