using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C81 RID: 3201
	public class BuildingAttributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x0600653A RID: 25914 RVA: 0x0025F008 File Offset: 0x0025D208
		public BuildingAttributes(ResourceSet parent)
			: base("BuildingAttributes", parent)
		{
			this.Decor = base.Add(new Klei.AI.Attribute("Decor", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.DecorRadius = base.Add(new Klei.AI.Attribute("DecorRadius", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.NoisePollution = base.Add(new Klei.AI.Attribute("NoisePollution", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.NoisePollutionRadius = base.Add(new Klei.AI.Attribute("NoisePollutionRadius", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.Hygiene = base.Add(new Klei.AI.Attribute("Hygiene", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.Comfort = base.Add(new Klei.AI.Attribute("Comfort", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.OverheatTemperature = base.Add(new Klei.AI.Attribute("OverheatTemperature", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.OverheatTemperature.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.ModifyOnly));
			this.FatalTemperature = base.Add(new Klei.AI.Attribute("FatalTemperature", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.FatalTemperature.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.ModifyOnly));
		}

		// Token: 0x0400466F RID: 18031
		public Klei.AI.Attribute Decor;

		// Token: 0x04004670 RID: 18032
		public Klei.AI.Attribute DecorRadius;

		// Token: 0x04004671 RID: 18033
		public Klei.AI.Attribute NoisePollution;

		// Token: 0x04004672 RID: 18034
		public Klei.AI.Attribute NoisePollutionRadius;

		// Token: 0x04004673 RID: 18035
		public Klei.AI.Attribute Hygiene;

		// Token: 0x04004674 RID: 18036
		public Klei.AI.Attribute Comfort;

		// Token: 0x04004675 RID: 18037
		public Klei.AI.Attribute OverheatTemperature;

		// Token: 0x04004676 RID: 18038
		public Klei.AI.Attribute FatalTemperature;
	}
}
