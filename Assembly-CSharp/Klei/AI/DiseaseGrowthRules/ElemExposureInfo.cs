using System;
using System.Collections.Generic;
using System.IO;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA8 RID: 3496
	public struct ElemExposureInfo
	{
		// Token: 0x06006A6D RID: 27245 RVA: 0x00295201 File Offset: 0x00293401
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.populationHalfLife);
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x00295210 File Offset: 0x00293410
		public static void SetBulk(ElemExposureInfo[] info, Func<Element, bool> test, ElemExposureInfo settings)
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

		// Token: 0x06006A6F RID: 27247 RVA: 0x0029524B File Offset: 0x0029344B
		public float CalculateExposureDiseaseCountDelta(int disease_count, float dt)
		{
			return (Disease.HalfLifeToGrowthRate(this.populationHalfLife, dt) - 1f) * (float)disease_count;
		}

		// Token: 0x04004FF7 RID: 20471
		public float populationHalfLife;
	}
}
