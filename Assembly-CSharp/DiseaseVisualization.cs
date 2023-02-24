using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008BD RID: 2237
public class DiseaseVisualization : ScriptableObject
{
	// Token: 0x06004058 RID: 16472 RVA: 0x00167DF0 File Offset: 0x00165FF0
	public DiseaseVisualization.Info GetInfo(HashedString id)
	{
		foreach (DiseaseVisualization.Info info in this.info)
		{
			if (id == info.name)
			{
				return info;
			}
		}
		return default(DiseaseVisualization.Info);
	}

	// Token: 0x04002A28 RID: 10792
	public Sprite overlaySprite;

	// Token: 0x04002A29 RID: 10793
	public List<DiseaseVisualization.Info> info = new List<DiseaseVisualization.Info>();

	// Token: 0x0200168D RID: 5773
	[Serializable]
	public struct Info
	{
		// Token: 0x060087F2 RID: 34802 RVA: 0x002F4552 File Offset: 0x002F2752
		public Info(string name)
		{
			this.name = name;
			this.overlayColourName = "germFoodPoisoning";
		}

		// Token: 0x04006A27 RID: 27175
		public string name;

		// Token: 0x04006A28 RID: 27176
		public string overlayColourName;
	}
}
