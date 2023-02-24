using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D9E RID: 3486
	public class TraitUtil
	{
		// Token: 0x06006A23 RID: 27171 RVA: 0x00293A2A File Offset: 0x00291C2A
		public static System.Action CreateDisabledTaskTrait(string id, string name, string desc, string disabled_chore_group, bool is_valid_starter_trait)
		{
			return delegate
			{
				ChoreGroup[] array = new ChoreGroup[] { Db.Get().ChoreGroups.Get(disabled_chore_group) };
				Db.Get().CreateTrait(id, name, desc, null, true, array, false, is_valid_starter_trait);
			};
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x00293A60 File Offset: 0x00291C60
		public static System.Action CreateTrait(string id, string name, string desc, string attributeId, float delta, string[] chore_groups, bool positiveTrait = false)
		{
			return delegate
			{
				List<ChoreGroup> list = new List<ChoreGroup>();
				foreach (string text in chore_groups)
				{
					list.Add(Db.Get().ChoreGroups.Get(text));
				}
				Db.Get().CreateTrait(id, name, desc, null, true, list.ToArray(), positiveTrait, true).Add(new AttributeModifier(attributeId, delta, name, false, false, true));
			};
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x00293AB4 File Offset: 0x00291CB4
		public static System.Action CreateAttributeEffectTrait(string id, string name, string desc, string attributeId, float delta, string attributeId2, float delta2, bool positiveTrait = false)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
				trait.Add(new AttributeModifier(attributeId, delta, name, false, false, true));
				trait.Add(new AttributeModifier(attributeId2, delta2, name, false, false, true));
			};
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x00293B10 File Offset: 0x00291D10
		public static System.Action CreateAttributeEffectTrait(string id, string name, string desc, string attributeId, float delta, bool positiveTrait = false, Action<GameObject> on_add = null, bool is_valid_starter_trait = true)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, is_valid_starter_trait);
				trait.Add(new AttributeModifier(attributeId, delta, name, false, false, true));
				trait.OnAddTrait = on_add;
			};
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x00293B69 File Offset: 0x00291D69
		public static System.Action CreateEffectModifierTrait(string id, string name, string desc, string[] ignoredEffects, bool positiveTrait = false)
		{
			return delegate
			{
				Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true).AddIgnoredEffects(ignoredEffects);
			};
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x00293B9F File Offset: 0x00291D9F
		public static System.Action CreateNamedTrait(string id, string name, string desc, bool positiveTrait = false)
		{
			return delegate
			{
				Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
			};
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x00293BD0 File Offset: 0x00291DD0
		public static System.Action CreateTrait(string id, string name, string desc, Action<GameObject> on_add, ChoreGroup[] disabled_chore_groups = null, bool positiveTrait = false, Func<string> extendedDescFn = null)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, disabled_chore_groups, positiveTrait, true);
				trait.OnAddTrait = on_add;
				if (extendedDescFn != null)
				{
					Trait trait2 = trait;
					trait2.ExtendedTooltip = (Func<string>)Delegate.Combine(trait2.ExtendedTooltip, extendedDescFn);
				}
			};
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x00293C21 File Offset: 0x00291E21
		public static System.Action CreateComponentTrait<T>(string id, string name, string desc, bool positiveTrait = false, Func<string> extendedDescFn = null) where T : KMonoBehaviour
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, positiveTrait, true);
				trait.OnAddTrait = delegate(GameObject go)
				{
					go.FindOrAddUnityComponent<T>();
				};
				if (extendedDescFn != null)
				{
					Trait trait2 = trait;
					trait2.ExtendedTooltip = (Func<string>)Delegate.Combine(trait2.ExtendedTooltip, extendedDescFn);
				}
			};
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x00293C57 File Offset: 0x00291E57
		public static System.Action CreateSkillGrantingTrait(string id, string name, string desc, string skillId)
		{
			return delegate
			{
				Trait trait = Db.Get().CreateTrait(id, name, desc, null, true, null, true, true);
				trait.TooltipCB = () => string.Format(DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_DESC, desc, SkillWidget.SkillPerksString(Db.Get().Skills.Get(skillId)));
				trait.OnAddTrait = delegate(GameObject go)
				{
					MinionResume component = go.GetComponent<MinionResume>();
					if (component != null)
					{
						component.GrantSkill(skillId);
					}
				};
			};
		}

		// Token: 0x06006A2C RID: 27180 RVA: 0x00293C88 File Offset: 0x00291E88
		public static string GetSkillGrantingTraitNameById(string id)
		{
			string text = "";
			StringEntry stringEntry;
			if (Strings.TryGet("STRINGS.DUPLICANTS.TRAITS.GRANTSKILL_" + id.ToUpper() + ".NAME", out stringEntry))
			{
				text = stringEntry.String;
			}
			return text;
		}
	}
}
