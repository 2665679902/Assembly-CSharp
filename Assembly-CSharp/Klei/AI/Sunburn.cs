using System;
using System.Collections.Generic;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D7B RID: 3451
	public class Sunburn : Sickness
	{
		// Token: 0x06006963 RID: 26979 RVA: 0x0028FED8 File Offset: 0x0028E0D8
		public Sunburn()
			: base("SunburnSickness", Sickness.SicknessType.Ailment, Sickness.Severity.Minor, 0.005f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Exposure }, 1020f, null)
		{
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0.033333335f, DUPLICANTS.DISEASES.SUNBURNSICKNESS.NAME, false, false, true)
			}));
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_hot_kanim", "anim_loco_run_hot_kanim", "anim_loco_walk_hot_kanim" }, Db.Get().Expressions.SickFierySkin));
			base.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Hot, 5f));
		}

		// Token: 0x04004F41 RID: 20289
		public const string ID = "SunburnSickness";
	}
}
