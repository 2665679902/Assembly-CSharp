using System;
using System.Collections.Generic;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA9 RID: 3497
	public class ExposureRule
	{
		// Token: 0x06006A70 RID: 27248 RVA: 0x00295264 File Offset: 0x00293464
		public void Apply(ElemExposureInfo[] infoList)
		{
			List<Element> elements = ElementLoader.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				if (this.Test(elements[i]))
				{
					ElemExposureInfo elemExposureInfo = infoList[i];
					if (this.populationHalfLife != null)
					{
						elemExposureInfo.populationHalfLife = this.populationHalfLife.Value;
					}
					infoList[i] = elemExposureInfo;
				}
			}
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x002952C7 File Offset: 0x002934C7
		public virtual bool Test(Element e)
		{
			return true;
		}

		// Token: 0x06006A72 RID: 27250 RVA: 0x002952CA File Offset: 0x002934CA
		public virtual string Name()
		{
			return null;
		}

		// Token: 0x04004FF8 RID: 20472
		public float? populationHalfLife;
	}
}
