using System;
using System.Collections.Generic;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D6D RID: 3437
	public class Allergies : Sickness
	{
		// Token: 0x06006920 RID: 26912 RVA: 0x0028D170 File Offset: 0x0028B370
		public Allergies()
			: base("Allergies", Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Inhalation }, 60f, null)
		{
			float num = 0.025f;
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_allergies_kanim" }, Db.Get().Expressions.Pollen));
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, num, DUPLICANTS.DISEASES.ALLERGIES.NAME, false, false, true),
				new AttributeModifier(Db.Get().Attributes.Sneezyness.Id, 10f, DUPLICANTS.DISEASES.ALLERGIES.NAME, false, false, true)
			}));
		}

		// Token: 0x04004F07 RID: 20231
		public const string ID = "Allergies";

		// Token: 0x04004F08 RID: 20232
		public const float STRESS_PER_CYCLE = 15f;
	}
}
