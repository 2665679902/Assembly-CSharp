using System;
using System.Collections.Generic;
using System.IO;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA2 RID: 3490
	public struct ElemGrowthInfo
	{
		// Token: 0x06006A59 RID: 27225 RVA: 0x00294D9C File Offset: 0x00292F9C
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.underPopulationDeathRate);
			writer.Write(this.populationHalfLife);
			writer.Write(this.overPopulationHalfLife);
			writer.Write(this.diffusionScale);
			writer.Write(this.minCountPerKG);
			writer.Write(this.maxCountPerKG);
			writer.Write(this.minDiffusionCount);
			writer.Write(this.minDiffusionInfestationTickCount);
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x00294E0C File Offset: 0x0029300C
		public static void SetBulk(ElemGrowthInfo[] info, Func<Element, bool> test, ElemGrowthInfo settings)
		{
			List<Element> elements = ElementLoader.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				if (test(elements[i]))
				{
					info[i] = settings;
				}
			}
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x00294E48 File Offset: 0x00293048
		public float CalculateDiseaseCountDelta(int disease_count, float kg, float dt)
		{
			float num = this.minCountPerKG * kg;
			float num2 = this.maxCountPerKG * kg;
			float num3;
			if (num <= (float)disease_count && (float)disease_count <= num2)
			{
				num3 = (Disease.HalfLifeToGrowthRate(this.populationHalfLife, dt) - 1f) * (float)disease_count;
			}
			else if ((float)disease_count < num)
			{
				num3 = -this.underPopulationDeathRate * dt;
			}
			else
			{
				num3 = (Disease.HalfLifeToGrowthRate(this.overPopulationHalfLife, dt) - 1f) * (float)disease_count;
			}
			return num3;
		}

		// Token: 0x04004FDB RID: 20443
		public float underPopulationDeathRate;

		// Token: 0x04004FDC RID: 20444
		public float populationHalfLife;

		// Token: 0x04004FDD RID: 20445
		public float overPopulationHalfLife;

		// Token: 0x04004FDE RID: 20446
		public float diffusionScale;

		// Token: 0x04004FDF RID: 20447
		public float minCountPerKG;

		// Token: 0x04004FE0 RID: 20448
		public float maxCountPerKG;

		// Token: 0x04004FE1 RID: 20449
		public int minDiffusionCount;

		// Token: 0x04004FE2 RID: 20450
		public byte minDiffusionInfestationTickCount;
	}
}
