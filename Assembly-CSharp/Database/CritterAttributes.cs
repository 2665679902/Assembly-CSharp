using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C8D RID: 3213
	public class CritterAttributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x06006573 RID: 25971 RVA: 0x0026900C File Offset: 0x0026720C
		public CritterAttributes(ResourceSet parent)
			: base("CritterAttributes", parent)
		{
			this.Happiness = base.Add(new Klei.AI.Attribute("Happiness", Strings.Get("STRINGS.CREATURES.STATS.HAPPINESS.NAME"), "", Strings.Get("STRINGS.CREATURES.STATS.HAPPINESS.TOOLTIP"), 0f, Klei.AI.Attribute.Display.General, false, "ui_icon_happiness", null, null));
			this.Happiness.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Metabolism = base.Add(new Klei.AI.Attribute("Metabolism", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, null));
			this.Metabolism.SetFormatter(new ToPercentAttributeFormatter(100f, GameUtil.TimeSlice.None));
		}

		// Token: 0x0400485A RID: 18522
		public Klei.AI.Attribute Happiness;

		// Token: 0x0400485B RID: 18523
		public Klei.AI.Attribute Metabolism;
	}
}
