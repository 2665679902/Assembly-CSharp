using System;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004CB RID: 1227
	public struct ChangeFloats
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060034A4 RID: 13476 RVA: 0x00072CB0 File Offset: 0x00070EB0
		// (set) Token: 0x060034A5 RID: 13477 RVA: 0x00072CB8 File Offset: 0x00070EB8
		[StringEnumConverter]
		public ChangeFloats.ChangeType change { readonly get; private set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x00072CC1 File Offset: 0x00070EC1
		// (set) Token: 0x060034A7 RID: 13479 RVA: 0x00072CC9 File Offset: 0x00070EC9
		public MinMax value { readonly get; private set; }

		// Token: 0x02000AF8 RID: 2808
		public enum ChangeType
		{
			// Token: 0x0400258E RID: 9614
			NoChange,
			// Token: 0x0400258F RID: 9615
			OverrideRange,
			// Token: 0x04002590 RID: 9616
			OverrideSet,
			// Token: 0x04002591 RID: 9617
			TakeNoiseVal
		}
	}
}
