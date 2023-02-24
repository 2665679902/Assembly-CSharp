using System;
using System.Collections.Generic;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D71 RID: 3441
	public class FoodSickness : Sickness
	{
		// Token: 0x06006937 RID: 26935 RVA: 0x0028E61C File Offset: 0x0028C81C
		public FoodSickness()
			: base("FoodSickness", Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.005f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Digestion }, 1020f, "FoodSicknessRecovery")
		{
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier("BladderDelta", 0.33333334f, DUPLICANTS.DISEASES.FOODSICKNESS.NAME, false, false, true),
				new AttributeModifier("ToiletEfficiency", -0.2f, DUPLICANTS.DISEASES.FOODSICKNESS.NAME, false, false, true),
				new AttributeModifier("StaminaDelta", -0.05f, DUPLICANTS.DISEASES.FOODSICKNESS.NAME, false, false, true)
			}));
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_sick_kanim" }, Db.Get().Expressions.Sick));
			base.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 10f));
		}

		// Token: 0x04004F1F RID: 20255
		public const string ID = "FoodSickness";

		// Token: 0x04004F20 RID: 20256
		public const string RECOVERY_ID = "FoodSicknessRecovery";

		// Token: 0x04004F21 RID: 20257
		private const float VOMIT_FREQUENCY = 200f;
	}
}
