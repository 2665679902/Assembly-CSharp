using System;
using System.Diagnostics;

namespace Klei.AI
{
	// Token: 0x02000D6B RID: 3435
	[DebuggerDisplay("{AttributeId}")]
	public class AttributeModifier
	{
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06006902 RID: 26882 RVA: 0x0028CC5B File Offset: 0x0028AE5B
		// (set) Token: 0x06006903 RID: 26883 RVA: 0x0028CC63 File Offset: 0x0028AE63
		public string AttributeId { get; private set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06006904 RID: 26884 RVA: 0x0028CC6C File Offset: 0x0028AE6C
		// (set) Token: 0x06006905 RID: 26885 RVA: 0x0028CC74 File Offset: 0x0028AE74
		public float Value { get; private set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06006906 RID: 26886 RVA: 0x0028CC7D File Offset: 0x0028AE7D
		// (set) Token: 0x06006907 RID: 26887 RVA: 0x0028CC85 File Offset: 0x0028AE85
		public bool IsMultiplier { get; private set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06006908 RID: 26888 RVA: 0x0028CC8E File Offset: 0x0028AE8E
		// (set) Token: 0x06006909 RID: 26889 RVA: 0x0028CC96 File Offset: 0x0028AE96
		public bool UIOnly { get; private set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600690A RID: 26890 RVA: 0x0028CC9F File Offset: 0x0028AE9F
		// (set) Token: 0x0600690B RID: 26891 RVA: 0x0028CCA7 File Offset: 0x0028AEA7
		public bool IsReadonly { get; private set; }

		// Token: 0x0600690C RID: 26892 RVA: 0x0028CCB0 File Offset: 0x0028AEB0
		public AttributeModifier(string attribute_id, float value, string description = null, bool is_multiplier = false, bool uiOnly = false, bool is_readonly = true)
		{
			this.AttributeId = attribute_id;
			this.Value = value;
			this.Description = ((description == null) ? attribute_id : description);
			this.DescriptionCB = null;
			this.IsMultiplier = is_multiplier;
			this.UIOnly = uiOnly;
			this.IsReadonly = is_readonly;
		}

		// Token: 0x0600690D RID: 26893 RVA: 0x0028CD00 File Offset: 0x0028AF00
		public AttributeModifier(string attribute_id, float value, Func<string> description_cb, bool is_multiplier = false, bool uiOnly = false)
		{
			this.AttributeId = attribute_id;
			this.Value = value;
			this.DescriptionCB = description_cb;
			this.Description = null;
			this.IsMultiplier = is_multiplier;
			this.UIOnly = uiOnly;
			if (description_cb == null)
			{
				global::Debug.LogWarning("AttributeModifier being constructed without a description callback: " + attribute_id);
			}
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x0028CD52 File Offset: 0x0028AF52
		public void SetValue(float value)
		{
			this.Value = value;
		}

		// Token: 0x0600690F RID: 26895 RVA: 0x0028CD5C File Offset: 0x0028AF5C
		public string GetName()
		{
			Attribute attribute = Db.Get().Attributes.TryGet(this.AttributeId);
			if (attribute != null && attribute.ShowInUI != Attribute.Display.Never)
			{
				return attribute.Name;
			}
			return "";
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x0028CD97 File Offset: 0x0028AF97
		public string GetDescription()
		{
			if (this.DescriptionCB == null)
			{
				return this.Description;
			}
			return this.DescriptionCB();
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0028CDB4 File Offset: 0x0028AFB4
		public string GetFormattedString()
		{
			IAttributeFormatter attributeFormatter = null;
			Attribute attribute = Db.Get().Attributes.TryGet(this.AttributeId);
			if (!this.IsMultiplier)
			{
				if (attribute != null)
				{
					attributeFormatter = attribute.formatter;
				}
				else
				{
					attribute = Db.Get().BuildingAttributes.TryGet(this.AttributeId);
					if (attribute != null)
					{
						attributeFormatter = attribute.formatter;
					}
					else
					{
						attribute = Db.Get().PlantAttributes.TryGet(this.AttributeId);
						if (attribute != null)
						{
							attributeFormatter = attribute.formatter;
						}
					}
				}
			}
			string text = "";
			if (attributeFormatter != null)
			{
				text = attributeFormatter.GetFormattedModifier(this);
			}
			else if (this.IsMultiplier)
			{
				text += GameUtil.GetFormattedPercent(this.Value * 100f, GameUtil.TimeSlice.None);
			}
			else
			{
				text += GameUtil.GetFormattedSimple(this.Value, GameUtil.TimeSlice.None, null);
			}
			if (text != null && text.Length > 0 && text[0] != '-')
			{
				text = GameUtil.AddPositiveSign(text, this.Value > 0f);
			}
			return text;
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0028CEA5 File Offset: 0x0028B0A5
		public AttributeModifier Clone()
		{
			return new AttributeModifier(this.AttributeId, this.Value, this.Description, false, false, true);
		}

		// Token: 0x04004F03 RID: 20227
		public string Description;

		// Token: 0x04004F04 RID: 20228
		public Func<string> DescriptionCB;
	}
}
