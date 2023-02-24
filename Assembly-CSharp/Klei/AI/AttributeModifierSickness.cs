using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D7F RID: 3455
	public class AttributeModifierSickness : Sickness.SicknessComponent
	{
		// Token: 0x0600696A RID: 26986 RVA: 0x00290850 File Offset: 0x0028EA50
		public AttributeModifierSickness(AttributeModifier[] attribute_modifiers)
		{
			this.attributeModifiers = attribute_modifiers;
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x00290860 File Offset: 0x0028EA60
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			Attributes attributes = go.GetAttributes();
			for (int i = 0; i < this.attributeModifiers.Length; i++)
			{
				AttributeModifier attributeModifier = this.attributeModifiers[i];
				attributes.Add(attributeModifier);
			}
			return null;
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x00290898 File Offset: 0x0028EA98
		public override void OnCure(GameObject go, object instance_data)
		{
			Attributes attributes = go.GetAttributes();
			for (int i = 0; i < this.attributeModifiers.Length; i++)
			{
				AttributeModifier attributeModifier = this.attributeModifiers[i];
				attributes.Remove(attributeModifier);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600696D RID: 26989 RVA: 0x002908CF File Offset: 0x0028EACF
		public AttributeModifier[] Modifers
		{
			get
			{
				return this.attributeModifiers;
			}
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x002908D8 File Offset: 0x0028EAD8
		public override List<Descriptor> GetSymptoms()
		{
			List<Descriptor> list = new List<Descriptor>();
			foreach (AttributeModifier attributeModifier in this.attributeModifiers)
			{
				Attribute attribute = Db.Get().Attributes.Get(attributeModifier.AttributeId);
				list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS, attribute.Name, attributeModifier.GetFormattedString()), string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS_TOOLTIP, attribute.Name, attributeModifier.GetFormattedString()), Descriptor.DescriptorType.Symptom, false));
			}
			return list;
		}

		// Token: 0x04004F48 RID: 20296
		private AttributeModifier[] attributeModifiers;
	}
}
