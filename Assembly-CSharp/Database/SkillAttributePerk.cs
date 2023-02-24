using System;
using Klei.AI;
using STRINGS;

namespace Database
{
	// Token: 0x02000CFC RID: 3324
	public class SkillAttributePerk : SkillPerk
	{
		// Token: 0x0600671D RID: 26397 RVA: 0x0027A390 File Offset: 0x00278590
		public SkillAttributePerk(string id, string attributeId, float modifierBonus, string modifierDesc)
			: base(id, "", null, null, delegate(MinionResume identity)
			{
			}, false)
		{
			Klei.AI.Attribute attribute = Db.Get().Attributes.Get(attributeId);
			this.modifier = new AttributeModifier(attributeId, modifierBonus, modifierDesc, false, false, true);
			this.Name = string.Format(UI.ROLES_SCREEN.PERKS.ATTRIBUTE_EFFECT_FMT, this.modifier.GetFormattedString(), attribute.Name);
			base.OnApply = delegate(MinionResume identity)
			{
				if (identity.GetAttributes().Get(this.modifier.AttributeId).Modifiers.FindIndex((AttributeModifier mod) => mod == this.modifier) == -1)
				{
					identity.GetAttributes().Add(this.modifier);
				}
			};
			base.OnRemove = delegate(MinionResume identity)
			{
				identity.GetAttributes().Remove(this.modifier);
			};
		}

		// Token: 0x04004B36 RID: 19254
		public AttributeModifier modifier;
	}
}
