using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D68 RID: 3432
	[DebuggerDisplay("{Attribute.Id}")]
	public class AttributeInstance : ModifierInstance<Attribute>
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060068DA RID: 26842 RVA: 0x0028C2A7 File Offset: 0x0028A4A7
		public string Id
		{
			get
			{
				return this.Attribute.Id;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060068DB RID: 26843 RVA: 0x0028C2B4 File Offset: 0x0028A4B4
		public string Name
		{
			get
			{
				return this.Attribute.Name;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060068DC RID: 26844 RVA: 0x0028C2C1 File Offset: 0x0028A4C1
		public string Description
		{
			get
			{
				return this.Attribute.Description;
			}
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x0028C2CE File Offset: 0x0028A4CE
		public float GetBaseValue()
		{
			return this.Attribute.BaseValue;
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x0028C2DC File Offset: 0x0028A4DC
		public float GetTotalDisplayValue()
		{
			float num = this.Attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != this.Modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = this.Modifiers[num3];
				if (!attributeModifier.IsMultiplier)
				{
					num += attributeModifier.Value;
				}
				else
				{
					num2 += attributeModifier.Value;
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x0028C350 File Offset: 0x0028A550
		public float GetTotalValue()
		{
			float num = this.Attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != this.Modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = this.Modifiers[num3];
				if (!attributeModifier.UIOnly)
				{
					if (!attributeModifier.IsMultiplier)
					{
						num += attributeModifier.Value;
					}
					else
					{
						num2 += attributeModifier.Value;
					}
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x0028C3CC File Offset: 0x0028A5CC
		public static float GetTotalDisplayValue(Attribute attribute, List<AttributeModifier> modifiers)
		{
			float num = attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = modifiers[num3];
				if (!attributeModifier.IsMultiplier)
				{
					num += attributeModifier.Value;
				}
				else
				{
					num2 += attributeModifier.Value;
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x0028C430 File Offset: 0x0028A630
		public static float GetTotalValue(Attribute attribute, List<AttributeModifier> modifiers)
		{
			float num = attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = modifiers[num3];
				if (!attributeModifier.UIOnly)
				{
					if (!attributeModifier.IsMultiplier)
					{
						num += attributeModifier.Value;
					}
					else
					{
						num2 += attributeModifier.Value;
					}
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x0028C49C File Offset: 0x0028A69C
		public float GetModifierContribution(AttributeModifier testModifier)
		{
			if (!testModifier.IsMultiplier)
			{
				return testModifier.Value;
			}
			float num = this.Attribute.BaseValue;
			for (int num2 = 0; num2 != this.Modifiers.Count; num2++)
			{
				AttributeModifier attributeModifier = this.Modifiers[num2];
				if (!attributeModifier.IsMultiplier)
				{
					num += attributeModifier.Value;
				}
			}
			return num * testModifier.Value;
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x0028C500 File Offset: 0x0028A700
		public AttributeInstance(GameObject game_object, Attribute attribute)
			: base(game_object, attribute)
		{
			DebugUtil.Assert(attribute != null);
			this.Attribute = attribute;
		}

		// Token: 0x060068E4 RID: 26852 RVA: 0x0028C51A File Offset: 0x0028A71A
		public void Add(AttributeModifier modifier)
		{
			this.Modifiers.Add(modifier);
			if (this.OnDirty != null)
			{
				this.OnDirty();
			}
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x0028C53C File Offset: 0x0028A73C
		public void Remove(AttributeModifier modifier)
		{
			int i = 0;
			while (i < this.Modifiers.Count)
			{
				if (this.Modifiers[i] == modifier)
				{
					this.Modifiers.RemoveAt(i);
					if (this.OnDirty != null)
					{
						this.OnDirty();
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x0028C58E File Offset: 0x0028A78E
		public void ClearModifiers()
		{
			if (this.Modifiers.Count > 0)
			{
				this.Modifiers.Clear();
				if (this.OnDirty != null)
				{
					this.OnDirty();
				}
			}
		}

		// Token: 0x060068E7 RID: 26855 RVA: 0x0028C5BC File Offset: 0x0028A7BC
		public string GetDescription()
		{
			return string.Format(DUPLICANTS.ATTRIBUTES.VALUE, this.Name, this.GetFormattedValue());
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x0028C5D9 File Offset: 0x0028A7D9
		public string GetFormattedValue()
		{
			return this.Attribute.formatter.GetFormattedAttribute(this);
		}

		// Token: 0x060068E9 RID: 26857 RVA: 0x0028C5EC File Offset: 0x0028A7EC
		public string GetAttributeValueTooltip()
		{
			return this.Attribute.GetTooltip(this);
		}

		// Token: 0x04004EF3 RID: 20211
		public Attribute Attribute;

		// Token: 0x04004EF4 RID: 20212
		public System.Action OnDirty;

		// Token: 0x04004EF5 RID: 20213
		public ArrayRef<AttributeModifier> Modifiers;

		// Token: 0x04004EF6 RID: 20214
		public bool hide;
	}
}
