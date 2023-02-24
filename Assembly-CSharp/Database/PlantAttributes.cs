using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000CA7 RID: 3239
	public class PlantAttributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x060065CC RID: 26060 RVA: 0x0026EA44 File Offset: 0x0026CC44
		public PlantAttributes(ResourceSet parent)
			: base("PlantAttributes", parent)
		{
			this.WiltTempRangeMod = base.Add(new Klei.AI.Attribute("WiltTempRangeMod", false, Klei.AI.Attribute.Display.Normal, false, 1f, null, null, null, null));
			this.WiltTempRangeMod.SetFormatter(new PercentAttributeFormatter());
			this.YieldAmount = base.Add(new Klei.AI.Attribute("YieldAmount", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.YieldAmount.SetFormatter(new PercentAttributeFormatter());
			this.HarvestTime = base.Add(new Klei.AI.Attribute("HarvestTime", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.HarvestTime.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Time, GameUtil.TimeSlice.None));
			this.DecorBonus = base.Add(new Klei.AI.Attribute("DecorBonus", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.DecorBonus.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.MinLightLux = base.Add(new Klei.AI.Attribute("MinLightLux", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.MinLightLux.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Lux, GameUtil.TimeSlice.None));
			this.FertilizerUsageMod = base.Add(new Klei.AI.Attribute("FertilizerUsageMod", false, Klei.AI.Attribute.Display.Normal, false, 1f, null, null, null, null));
			this.FertilizerUsageMod.SetFormatter(new PercentAttributeFormatter());
			this.MinRadiationThreshold = base.Add(new Klei.AI.Attribute("MinRadiationThreshold", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.MinRadiationThreshold.SetFormatter(new RadsPerCycleAttributeFormatter());
			this.MaxRadiationThreshold = base.Add(new Klei.AI.Attribute("MaxRadiationThreshold", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.MaxRadiationThreshold.SetFormatter(new RadsPerCycleAttributeFormatter());
		}

		// Token: 0x040049C8 RID: 18888
		public Klei.AI.Attribute WiltTempRangeMod;

		// Token: 0x040049C9 RID: 18889
		public Klei.AI.Attribute YieldAmount;

		// Token: 0x040049CA RID: 18890
		public Klei.AI.Attribute HarvestTime;

		// Token: 0x040049CB RID: 18891
		public Klei.AI.Attribute DecorBonus;

		// Token: 0x040049CC RID: 18892
		public Klei.AI.Attribute MinLightLux;

		// Token: 0x040049CD RID: 18893
		public Klei.AI.Attribute FertilizerUsageMod;

		// Token: 0x040049CE RID: 18894
		public Klei.AI.Attribute MinRadiationThreshold;

		// Token: 0x040049CF RID: 18895
		public Klei.AI.Attribute MaxRadiationThreshold;
	}
}
