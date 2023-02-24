using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA5 RID: 3493
	public class ElementGrowthRule : GrowthRule
	{
		// Token: 0x06006A63 RID: 27235 RVA: 0x00295048 File Offset: 0x00293248
		public ElementGrowthRule(SimHashes element)
		{
			this.element = element;
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x00295057 File Offset: 0x00293257
		public override bool Test(Element e)
		{
			return e.id == this.element;
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x00295067 File Offset: 0x00293267
		public override string Name()
		{
			return ElementLoader.FindElementByHash(this.element).name;
		}

		// Token: 0x04004FEC RID: 20460
		public SimHashes element;
	}
}
