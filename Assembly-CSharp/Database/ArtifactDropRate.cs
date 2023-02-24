using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000C78 RID: 3192
	public class ArtifactDropRate : Resource
	{
		// Token: 0x0600651E RID: 25886 RVA: 0x0025D76E File Offset: 0x0025B96E
		public void AddItem(ArtifactTier tier, float weight)
		{
			this.rates.Add(new global::Tuple<ArtifactTier, float>(tier, weight));
			this.totalWeight += weight;
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x0025D790 File Offset: 0x0025B990
		public float GetTierWeight(ArtifactTier tier)
		{
			float num = 0f;
			foreach (global::Tuple<ArtifactTier, float> tuple in this.rates)
			{
				if (tuple.first == tier)
				{
					num = tuple.second;
				}
			}
			return num;
		}

		// Token: 0x04004615 RID: 17941
		public List<global::Tuple<ArtifactTier, float>> rates = new List<global::Tuple<ArtifactTier, float>>();

		// Token: 0x04004616 RID: 17942
		public float totalWeight;
	}
}
