using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D9D RID: 3485
	public class Trait : Modifier
	{
		// Token: 0x06006A1A RID: 27162 RVA: 0x002936B6 File Offset: 0x002918B6
		public Trait(string id, string name, string description, float rating, bool should_save, ChoreGroup[] disallowed_chore_groups, bool positive_trait, bool is_valid_starter_trait)
			: base(id, name, description)
		{
			this.Rating = rating;
			this.ShouldSave = should_save;
			this.disabledChoreGroups = disallowed_chore_groups;
			this.PositiveTrait = positive_trait;
			this.ValidStarterTrait = is_valid_starter_trait;
			this.ignoredEffects = new string[0];
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x002936F8 File Offset: 0x002918F8
		public void AddIgnoredEffects(string[] effects)
		{
			List<string> list = new List<string>(this.ignoredEffects);
			list.AddRange(effects);
			this.ignoredEffects = list.ToArray();
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x00293724 File Offset: 0x00291924
		public string GetTooltip()
		{
			string text;
			if (this.TooltipCB != null)
			{
				text = this.TooltipCB();
			}
			else
			{
				text = this.description;
				text += this.GetAttributeModifiersString(true);
				text += this.GetDisabledChoresString(true);
				text += this.GetIgnoredEffectsString(true);
				text += this.GetExtendedTooltipStr();
			}
			return text;
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x00293788 File Offset: 0x00291988
		public string GetAttributeModifiersString(bool list_entry)
		{
			string text = "";
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				Attribute attribute = Db.Get().Attributes.Get(attributeModifier.AttributeId);
				if (list_entry)
				{
					text += DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY;
				}
				text += string.Format(DUPLICANTS.TRAITS.ATTRIBUTE_MODIFIERS, attribute.Name, attributeModifier.GetFormattedString());
			}
			return text;
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x00293828 File Offset: 0x00291A28
		public string GetDisabledChoresString(bool list_entry)
		{
			string text = "";
			if (this.disabledChoreGroups != null)
			{
				string text2 = DUPLICANTS.TRAITS.CANNOT_DO_TASK;
				if (this.isTaskBeingRefused)
				{
					text2 = DUPLICANTS.TRAITS.REFUSES_TO_DO_TASK;
				}
				foreach (ChoreGroup choreGroup in this.disabledChoreGroups)
				{
					if (list_entry)
					{
						text += DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY;
					}
					text += string.Format(text2, choreGroup.Name);
				}
			}
			return text;
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x002938A4 File Offset: 0x00291AA4
		public string GetIgnoredEffectsString(bool list_entry)
		{
			string text = "";
			if (this.ignoredEffects != null && this.ignoredEffects.Length != 0)
			{
				foreach (string text2 in this.ignoredEffects)
				{
					if (list_entry)
					{
						text += DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY;
					}
					string text3 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text2.ToUpper() + ".NAME");
					text += string.Format(DUPLICANTS.TRAITS.IGNORED_EFFECTS, text3);
				}
			}
			return text;
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x00293930 File Offset: 0x00291B30
		public string GetExtendedTooltipStr()
		{
			string text = "";
			if (this.ExtendedTooltip != null)
			{
				foreach (Func<string> func in this.ExtendedTooltip.GetInvocationList())
				{
					text = text + "\n" + func();
				}
			}
			return text;
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x00293984 File Offset: 0x00291B84
		public override void AddTo(Attributes attributes)
		{
			base.AddTo(attributes);
			ChoreConsumer component = attributes.gameObject.GetComponent<ChoreConsumer>();
			if (component != null && this.disabledChoreGroups != null)
			{
				foreach (ChoreGroup choreGroup in this.disabledChoreGroups)
				{
					component.SetPermittedByTraits(choreGroup, false);
				}
			}
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x002939D8 File Offset: 0x00291BD8
		public override void RemoveFrom(Attributes attributes)
		{
			base.RemoveFrom(attributes);
			ChoreConsumer component = attributes.gameObject.GetComponent<ChoreConsumer>();
			if (component != null && this.disabledChoreGroups != null)
			{
				foreach (ChoreGroup choreGroup in this.disabledChoreGroups)
				{
					component.SetPermittedByTraits(choreGroup, true);
				}
			}
		}

		// Token: 0x04004FB6 RID: 20406
		public float Rating;

		// Token: 0x04004FB7 RID: 20407
		public bool ShouldSave;

		// Token: 0x04004FB8 RID: 20408
		public bool PositiveTrait;

		// Token: 0x04004FB9 RID: 20409
		public bool ValidStarterTrait;

		// Token: 0x04004FBA RID: 20410
		public Action<GameObject> OnAddTrait;

		// Token: 0x04004FBB RID: 20411
		public Func<string> TooltipCB;

		// Token: 0x04004FBC RID: 20412
		public Func<string> ExtendedTooltip;

		// Token: 0x04004FBD RID: 20413
		public ChoreGroup[] disabledChoreGroups;

		// Token: 0x04004FBE RID: 20414
		public bool isTaskBeingRefused;

		// Token: 0x04004FBF RID: 20415
		public string[] ignoredEffects;
	}
}
