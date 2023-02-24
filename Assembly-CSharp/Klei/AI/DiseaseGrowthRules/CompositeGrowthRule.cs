using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA7 RID: 3495
	public class CompositeGrowthRule
	{
		// Token: 0x06006A69 RID: 27241 RVA: 0x002950A3 File Offset: 0x002932A3
		public string Name()
		{
			return this.name;
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x002950AC File Offset: 0x002932AC
		public void Overlay(GrowthRule rule)
		{
			if (rule.underPopulationDeathRate != null)
			{
				this.underPopulationDeathRate = rule.underPopulationDeathRate.Value;
			}
			if (rule.populationHalfLife != null)
			{
				this.populationHalfLife = rule.populationHalfLife.Value;
			}
			if (rule.overPopulationHalfLife != null)
			{
				this.overPopulationHalfLife = rule.overPopulationHalfLife.Value;
			}
			if (rule.diffusionScale != null)
			{
				this.diffusionScale = rule.diffusionScale.Value;
			}
			if (rule.minCountPerKG != null)
			{
				this.minCountPerKG = rule.minCountPerKG.Value;
			}
			if (rule.maxCountPerKG != null)
			{
				this.maxCountPerKG = rule.maxCountPerKG.Value;
			}
			if (rule.minDiffusionCount != null)
			{
				this.minDiffusionCount = rule.minDiffusionCount.Value;
			}
			if (rule.minDiffusionInfestationTickCount != null)
			{
				this.minDiffusionInfestationTickCount = rule.minDiffusionInfestationTickCount.Value;
			}
			this.name = rule.Name();
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x002951BC File Offset: 0x002933BC
		public float GetHalfLifeForCount(int count, float kg)
		{
			int num = (int)(this.minCountPerKG * kg);
			int num2 = (int)(this.maxCountPerKG * kg);
			if (count < num)
			{
				return this.populationHalfLife;
			}
			if (count < num2)
			{
				return this.populationHalfLife;
			}
			return this.overPopulationHalfLife;
		}

		// Token: 0x04004FEE RID: 20462
		public string name;

		// Token: 0x04004FEF RID: 20463
		public float underPopulationDeathRate;

		// Token: 0x04004FF0 RID: 20464
		public float populationHalfLife;

		// Token: 0x04004FF1 RID: 20465
		public float overPopulationHalfLife;

		// Token: 0x04004FF2 RID: 20466
		public float diffusionScale;

		// Token: 0x04004FF3 RID: 20467
		public float minCountPerKG;

		// Token: 0x04004FF4 RID: 20468
		public float maxCountPerKG;

		// Token: 0x04004FF5 RID: 20469
		public int minDiffusionCount;

		// Token: 0x04004FF6 RID: 20470
		public byte minDiffusionInfestationTickCount;
	}
}
