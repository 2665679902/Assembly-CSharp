using System;
using System.Collections.Generic;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D72 RID: 3442
	public class HeatRash : Sickness
	{
		// Token: 0x06006938 RID: 26936 RVA: 0x0028E720 File Offset: 0x0028C920
		public HeatRash()
			: base("HeatSickness", Sickness.SicknessType.Ailment, Sickness.Severity.Minor, 0.005f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Inhalation }, 180f, null)
		{
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier("Learning", -5f, DUPLICANTS.DISEASES.HEATSICKNESS.NAME, false, false, true),
				new AttributeModifier("Machinery", -5f, DUPLICANTS.DISEASES.HEATSICKNESS.NAME, false, false, true),
				new AttributeModifier("Construction", -5f, DUPLICANTS.DISEASES.HEATSICKNESS.NAME, false, false, true),
				new AttributeModifier("Cooking", -5f, DUPLICANTS.DISEASES.HEATSICKNESS.NAME, false, false, true)
			}));
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_hot_kanim", "anim_loco_run_hot_kanim", "anim_loco_walk_hot_kanim" }, Db.Get().Expressions.SickFierySkin));
			base.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Hot, 15f));
		}

		// Token: 0x04004F22 RID: 20258
		public const string ID = "HeatSickness";
	}
}
