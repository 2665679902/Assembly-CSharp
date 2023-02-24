using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C8F RID: 3215
	public class Diseases : ResourceSet<Disease>
	{
		// Token: 0x06006575 RID: 25973 RVA: 0x00269308 File Offset: 0x00267508
		public Diseases(ResourceSet parent, bool statsOnly = false)
			: base("Diseases", parent)
		{
			this.FoodGerms = base.Add(new FoodGerms(statsOnly));
			this.SlimeGerms = base.Add(new SlimeGerms(statsOnly));
			this.PollenGerms = base.Add(new PollenGerms(statsOnly));
			this.ZombieSpores = base.Add(new ZombieSpores(statsOnly));
			if (DlcManager.FeatureRadiationEnabled())
			{
				this.RadiationPoisoning = base.Add(new RadiationPoisoning(statsOnly));
			}
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x00269384 File Offset: 0x00267584
		public bool IsValidID(string id)
		{
			bool flag = false;
			using (List<Disease>.Enumerator enumerator = this.resources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == id)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x002693E4 File Offset: 0x002675E4
		public byte GetIndex(int hash)
		{
			byte b = 0;
			while ((int)b < this.resources.Count)
			{
				Disease disease = this.resources[(int)b];
				if (hash == disease.id.GetHashCode())
				{
					return b;
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x00269430 File Offset: 0x00267630
		public byte GetIndex(HashedString id)
		{
			return this.GetIndex(id.GetHashCode());
		}

		// Token: 0x04004868 RID: 18536
		public Disease FoodGerms;

		// Token: 0x04004869 RID: 18537
		public Disease SlimeGerms;

		// Token: 0x0400486A RID: 18538
		public Disease PollenGerms;

		// Token: 0x0400486B RID: 18539
		public Disease ZombieSpores;

		// Token: 0x0400486C RID: 18540
		public Disease RadiationPoisoning;
	}
}
