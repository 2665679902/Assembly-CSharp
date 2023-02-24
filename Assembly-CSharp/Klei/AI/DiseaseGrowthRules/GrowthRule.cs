using System;
using System.Collections.Generic;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA3 RID: 3491
	public class GrowthRule
	{
		// Token: 0x06006A5C RID: 27228 RVA: 0x00294EB4 File Offset: 0x002930B4
		public void Apply(ElemGrowthInfo[] infoList)
		{
			List<Element> elements = ElementLoader.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				Element element = elements[i];
				if (element.id != SimHashes.Vacuum && this.Test(element))
				{
					ElemGrowthInfo elemGrowthInfo = infoList[i];
					if (this.underPopulationDeathRate != null)
					{
						elemGrowthInfo.underPopulationDeathRate = this.underPopulationDeathRate.Value;
					}
					if (this.populationHalfLife != null)
					{
						elemGrowthInfo.populationHalfLife = this.populationHalfLife.Value;
					}
					if (this.overPopulationHalfLife != null)
					{
						elemGrowthInfo.overPopulationHalfLife = this.overPopulationHalfLife.Value;
					}
					if (this.diffusionScale != null)
					{
						elemGrowthInfo.diffusionScale = this.diffusionScale.Value;
					}
					if (this.minCountPerKG != null)
					{
						elemGrowthInfo.minCountPerKG = this.minCountPerKG.Value;
					}
					if (this.maxCountPerKG != null)
					{
						elemGrowthInfo.maxCountPerKG = this.maxCountPerKG.Value;
					}
					if (this.minDiffusionCount != null)
					{
						elemGrowthInfo.minDiffusionCount = this.minDiffusionCount.Value;
					}
					if (this.minDiffusionInfestationTickCount != null)
					{
						elemGrowthInfo.minDiffusionInfestationTickCount = this.minDiffusionInfestationTickCount.Value;
					}
					infoList[i] = elemGrowthInfo;
				}
			}
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x00295010 File Offset: 0x00293210
		public virtual bool Test(Element e)
		{
			return true;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x00295013 File Offset: 0x00293213
		public virtual string Name()
		{
			return null;
		}

		// Token: 0x04004FE3 RID: 20451
		public float? underPopulationDeathRate;

		// Token: 0x04004FE4 RID: 20452
		public float? populationHalfLife;

		// Token: 0x04004FE5 RID: 20453
		public float? overPopulationHalfLife;

		// Token: 0x04004FE6 RID: 20454
		public float? diffusionScale;

		// Token: 0x04004FE7 RID: 20455
		public float? minCountPerKG;

		// Token: 0x04004FE8 RID: 20456
		public float? maxCountPerKG;

		// Token: 0x04004FE9 RID: 20457
		public int? minDiffusionCount;

		// Token: 0x04004FEA RID: 20458
		public byte? minDiffusionInfestationTickCount;
	}
}
