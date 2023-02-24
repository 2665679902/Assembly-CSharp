using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DAB RID: 3499
	public class CompositeExposureRule
	{
		// Token: 0x06006A77 RID: 27255 RVA: 0x00295306 File Offset: 0x00293506
		public string Name()
		{
			return this.name;
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x0029530E File Offset: 0x0029350E
		public void Overlay(ExposureRule rule)
		{
			if (rule.populationHalfLife != null)
			{
				this.populationHalfLife = rule.populationHalfLife.Value;
			}
			this.name = rule.Name();
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x0029533B File Offset: 0x0029353B
		public float GetHalfLifeForCount(int count)
		{
			return this.populationHalfLife;
		}

		// Token: 0x04004FFA RID: 20474
		public string name;

		// Token: 0x04004FFB RID: 20475
		public float populationHalfLife;
	}
}
