using System;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004CC RID: 1228
	public class FeatureConverter
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x00072CD2 File Offset: 0x00070ED2
		// (set) Token: 0x060034A9 RID: 13481 RVA: 0x00072CDA File Offset: 0x00070EDA
		[StringEnumConverter]
		public FeatureConverter.Shape shape { get; private set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x00072CE3 File Offset: 0x00070EE3
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x00072CEB File Offset: 0x00070EEB
		public MinMax size { get; private set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x00072CF4 File Offset: 0x00070EF4
		// (set) Token: 0x060034AD RID: 13485 RVA: 0x00072CFC File Offset: 0x00070EFC
		public MinMax density { get; private set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x00072D05 File Offset: 0x00070F05
		// (set) Token: 0x060034AF RID: 13487 RVA: 0x00072D0D File Offset: 0x00070F0D
		public ChangeFloats mass { get; private set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x00072D16 File Offset: 0x00070F16
		// (set) Token: 0x060034B1 RID: 13489 RVA: 0x00072D1E File Offset: 0x00070F1E
		public ChangeFloats temperature { get; private set; }

		// Token: 0x02000AF9 RID: 2809
		public enum Shape
		{
			// Token: 0x04002593 RID: 9619
			Circle,
			// Token: 0x04002594 RID: 9620
			Oval,
			// Token: 0x04002595 RID: 9621
			Blob,
			// Token: 0x04002596 RID: 9622
			Square,
			// Token: 0x04002597 RID: 9623
			Rectangle,
			// Token: 0x04002598 RID: 9624
			Line
		}
	}
}
