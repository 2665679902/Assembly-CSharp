using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000CB0 RID: 3248
	public class Sicknesses : ResourceSet<Sickness>
	{
		// Token: 0x060065E1 RID: 26081 RVA: 0x00270DAC File Offset: 0x0026EFAC
		public Sicknesses(ResourceSet parent)
			: base("Sicknesses", parent)
		{
			this.FoodSickness = base.Add(new FoodSickness());
			this.SlimeSickness = base.Add(new SlimeSickness());
			this.ZombieSickness = base.Add(new ZombieSickness());
			if (DlcManager.FeatureRadiationEnabled())
			{
				this.RadiationSickness = base.Add(new RadiationSickness());
			}
			this.Allergies = base.Add(new Allergies());
			this.ColdBrain = base.Add(new ColdBrain());
			this.HeatRash = base.Add(new HeatRash());
			this.Sunburn = base.Add(new Sunburn());
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x00270E54 File Offset: 0x0026F054
		public static bool IsValidID(string id)
		{
			bool flag = false;
			using (List<Sickness>.Enumerator enumerator = Db.Get().Sicknesses.resources.GetEnumerator())
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

		// Token: 0x04004A14 RID: 18964
		public Sickness FoodSickness;

		// Token: 0x04004A15 RID: 18965
		public Sickness SlimeSickness;

		// Token: 0x04004A16 RID: 18966
		public Sickness ZombieSickness;

		// Token: 0x04004A17 RID: 18967
		public Sickness Allergies;

		// Token: 0x04004A18 RID: 18968
		public Sickness RadiationSickness;

		// Token: 0x04004A19 RID: 18969
		public Sickness ColdBrain;

		// Token: 0x04004A1A RID: 18970
		public Sickness HeatRash;

		// Token: 0x04004A1B RID: 18971
		public Sickness Sunburn;
	}
}
