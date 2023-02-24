using System;
using System.Collections.Generic;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D6E RID: 3438
	public class ColdBrain : Sickness
	{
		// Token: 0x06006921 RID: 26913 RVA: 0x0028D254 File Offset: 0x0028B454
		public ColdBrain()
			: base("ColdSickness", Sickness.SicknessType.Ailment, Sickness.Severity.Minor, 0.005f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Inhalation }, 180f, null)
		{
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier("Learning", -5f, DUPLICANTS.DISEASES.COLDSICKNESS.NAME, false, false, true),
				new AttributeModifier("Machinery", -5f, DUPLICANTS.DISEASES.COLDSICKNESS.NAME, false, false, true),
				new AttributeModifier("Construction", -5f, DUPLICANTS.DISEASES.COLDSICKNESS.NAME, false, false, true),
				new AttributeModifier("Cooking", -5f, DUPLICANTS.DISEASES.COLDSICKNESS.NAME, false, false, true),
				new AttributeModifier("Sneezyness", 1f, DUPLICANTS.DISEASES.COLDSICKNESS.NAME, false, false, true)
			}));
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_cold_kanim", "anim_loco_run_cold_kanim", "anim_loco_walk_cold_kanim" }, Db.Get().Expressions.SickCold));
			base.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Cold, 15f));
		}

		// Token: 0x04004F09 RID: 20233
		public const string ID = "ColdSickness";
	}
}
