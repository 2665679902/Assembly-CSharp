using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DAA RID: 3498
	public class ElementExposureRule : ExposureRule
	{
		// Token: 0x06006A74 RID: 27252 RVA: 0x002952D5 File Offset: 0x002934D5
		public ElementExposureRule(SimHashes element)
		{
			this.element = element;
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x002952E4 File Offset: 0x002934E4
		public override bool Test(Element e)
		{
			return e.id == this.element;
		}

		// Token: 0x06006A76 RID: 27254 RVA: 0x002952F4 File Offset: 0x002934F4
		public override string Name()
		{
			return ElementLoader.FindElementByHash(this.element).name;
		}

		// Token: 0x04004FF9 RID: 20473
		public SimHashes element;
	}
}
