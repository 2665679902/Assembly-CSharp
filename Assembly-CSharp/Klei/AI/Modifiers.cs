using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D97 RID: 3479
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/Modifiers")]
	public class Modifiers : KMonoBehaviour, ISaveLoadableDetails
	{
		// Token: 0x060069F2 RID: 27122 RVA: 0x00292DC0 File Offset: 0x00290FC0
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.amounts = new Amounts(base.gameObject);
			this.sicknesses = new Sicknesses(base.gameObject);
			this.attributes = new Attributes(base.gameObject);
			foreach (string text in this.initialAmounts)
			{
				this.amounts.Add(new AmountInstance(Db.Get().Amounts.Get(text), base.gameObject));
			}
			foreach (string text2 in this.initialAttributes)
			{
				Attribute attribute = Db.Get().CritterAttributes.TryGet(text2);
				if (attribute == null)
				{
					attribute = Db.Get().PlantAttributes.TryGet(text2);
				}
				if (attribute == null)
				{
					attribute = Db.Get().Attributes.TryGet(text2);
				}
				DebugUtil.Assert(attribute != null, "Couldn't find an attribute for id", text2);
				this.attributes.Add(attribute);
			}
			Traits component = base.GetComponent<Traits>();
			if (this.initialTraits != null)
			{
				foreach (string text3 in this.initialTraits)
				{
					Trait trait = Db.Get().traits.Get(text3);
					component.Add(trait);
				}
			}
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x00292F6C File Offset: 0x0029116C
		public float GetPreModifiedAttributeValue(Attribute attribute)
		{
			return AttributeInstance.GetTotalValue(attribute, this.GetPreModifiers(attribute));
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x00292F7C File Offset: 0x0029117C
		public string GetPreModifiedAttributeFormattedValue(Attribute attribute)
		{
			float totalValue = AttributeInstance.GetTotalValue(attribute, this.GetPreModifiers(attribute));
			return attribute.formatter.GetFormattedValue(totalValue, attribute.formatter.DeltaTimeSlice);
		}

		// Token: 0x060069F5 RID: 27125 RVA: 0x00292FB0 File Offset: 0x002911B0
		public string GetPreModifiedAttributeDescription(Attribute attribute)
		{
			float totalValue = AttributeInstance.GetTotalValue(attribute, this.GetPreModifiers(attribute));
			return string.Format(DUPLICANTS.ATTRIBUTES.VALUE, attribute.Name, attribute.formatter.GetFormattedValue(totalValue, GameUtil.TimeSlice.None));
		}

		// Token: 0x060069F6 RID: 27126 RVA: 0x00292FED File Offset: 0x002911ED
		public string GetPreModifiedAttributeToolTip(Attribute attribute)
		{
			return attribute.formatter.GetTooltip(attribute, this.GetPreModifiers(attribute), null);
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x00293004 File Offset: 0x00291204
		private List<AttributeModifier> GetPreModifiers(Attribute attribute)
		{
			List<AttributeModifier> list = new List<AttributeModifier>();
			foreach (string text in this.initialTraits)
			{
				foreach (AttributeModifier attributeModifier in Db.Get().traits.Get(text).SelfModifiers)
				{
					if (attributeModifier.AttributeId == attribute.Id)
					{
						list.Add(attributeModifier);
					}
				}
			}
			MutantPlant component = base.GetComponent<MutantPlant>();
			if (component != null && component.MutationIDs != null)
			{
				foreach (string text2 in component.MutationIDs)
				{
					foreach (AttributeModifier attributeModifier2 in Db.Get().PlantMutations.Get(text2).SelfModifiers)
					{
						if (attributeModifier2.AttributeId == attribute.Id)
						{
							list.Add(attributeModifier2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x00293184 File Offset: 0x00291384
		public void Serialize(BinaryWriter writer)
		{
			this.OnSerialize(writer);
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x0029318D File Offset: 0x0029138D
		public void Deserialize(IReader reader)
		{
			this.OnDeserialize(reader);
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x00293196 File Offset: 0x00291396
		public virtual void OnSerialize(BinaryWriter writer)
		{
			this.amounts.Serialize(writer);
			this.sicknesses.Serialize(writer);
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x002931B0 File Offset: 0x002913B0
		public virtual void OnDeserialize(IReader reader)
		{
			this.amounts.Deserialize(reader);
			this.sicknesses.Deserialize(reader);
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x002931CA File Offset: 0x002913CA
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			if (this.amounts != null)
			{
				this.amounts.Cleanup();
			}
		}

		// Token: 0x04004FA8 RID: 20392
		public Amounts amounts;

		// Token: 0x04004FA9 RID: 20393
		public Attributes attributes;

		// Token: 0x04004FAA RID: 20394
		public Sicknesses sicknesses;

		// Token: 0x04004FAB RID: 20395
		public List<string> initialTraits = new List<string>();

		// Token: 0x04004FAC RID: 20396
		public List<string> initialAmounts = new List<string>();

		// Token: 0x04004FAD RID: 20397
		public List<string> initialAttributes = new List<string>();
	}
}
