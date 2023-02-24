using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D64 RID: 3428
	public class Attribute : Resource
	{
		// Token: 0x060068C6 RID: 26822 RVA: 0x0028BE10 File Offset: 0x0028A010
		public Attribute(string id, bool is_trainable, Attribute.Display show_in_ui, bool is_profession, float base_value = 0f, string uiSprite = null, string thoughtSprite = null, string uiFullColourSprite = null, string[] overrideDLCIDs = null)
			: base(id, null, null)
		{
			string text = "STRINGS.DUPLICANTS.ATTRIBUTES." + id.ToUpper();
			this.Name = Strings.Get(new StringKey(text + ".NAME"));
			this.ProfessionName = Strings.Get(new StringKey(text + ".NAME"));
			this.Description = Strings.Get(new StringKey(text + ".DESC"));
			this.IsTrainable = is_trainable;
			this.IsProfession = is_profession;
			this.ShowInUI = show_in_ui;
			this.BaseValue = base_value;
			this.formatter = Attribute.defaultFormatter;
			this.uiSprite = uiSprite;
			this.thoughtSprite = thoughtSprite;
			this.uiFullColourSprite = uiFullColourSprite;
			if (overrideDLCIDs != null)
			{
				this.DLCIds = overrideDLCIDs;
			}
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x0028BEFC File Offset: 0x0028A0FC
		public Attribute(string id, string name, string profession_name, string attribute_description, float base_value, Attribute.Display show_in_ui, bool is_trainable, string uiSprite = null, string thoughtSprite = null, string uiFullColourSprite = null)
			: base(id, name)
		{
			this.Description = attribute_description;
			this.ProfessionName = profession_name;
			this.BaseValue = base_value;
			this.ShowInUI = show_in_ui;
			this.IsTrainable = is_trainable;
			this.uiSprite = uiSprite;
			this.thoughtSprite = thoughtSprite;
			this.uiFullColourSprite = uiFullColourSprite;
			if (this.ProfessionName == "")
			{
				this.ProfessionName = null;
			}
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x0028BF7F File Offset: 0x0028A17F
		public void SetFormatter(IAttributeFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x0028BF88 File Offset: 0x0028A188
		public AttributeInstance Lookup(Component cmp)
		{
			return this.Lookup(cmp.gameObject);
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x0028BF98 File Offset: 0x0028A198
		public AttributeInstance Lookup(GameObject go)
		{
			Attributes attributes = go.GetAttributes();
			if (attributes != null)
			{
				return attributes.Get(this);
			}
			return null;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x0028BFB8 File Offset: 0x0028A1B8
		public string GetDescription(AttributeInstance instance)
		{
			return instance.GetDescription();
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x0028BFC0 File Offset: 0x0028A1C0
		public string GetTooltip(AttributeInstance instance)
		{
			return this.formatter.GetTooltip(this, instance);
		}

		// Token: 0x04004EDE RID: 20190
		private static readonly StandardAttributeFormatter defaultFormatter = new StandardAttributeFormatter(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.None);

		// Token: 0x04004EDF RID: 20191
		public string Description;

		// Token: 0x04004EE0 RID: 20192
		public float BaseValue;

		// Token: 0x04004EE1 RID: 20193
		public Attribute.Display ShowInUI;

		// Token: 0x04004EE2 RID: 20194
		public bool IsTrainable;

		// Token: 0x04004EE3 RID: 20195
		public bool IsProfession;

		// Token: 0x04004EE4 RID: 20196
		public string ProfessionName;

		// Token: 0x04004EE5 RID: 20197
		public List<AttributeConverter> converters = new List<AttributeConverter>();

		// Token: 0x04004EE6 RID: 20198
		public string uiSprite;

		// Token: 0x04004EE7 RID: 20199
		public string thoughtSprite;

		// Token: 0x04004EE8 RID: 20200
		public string uiFullColourSprite;

		// Token: 0x04004EE9 RID: 20201
		public string[] DLCIds = DlcManager.AVAILABLE_ALL_VERSIONS;

		// Token: 0x04004EEA RID: 20202
		public IAttributeFormatter formatter;

		// Token: 0x02001E43 RID: 7747
		public enum Display
		{
			// Token: 0x0400882E RID: 34862
			Normal,
			// Token: 0x0400882F RID: 34863
			Skill,
			// Token: 0x04008830 RID: 34864
			Expectation,
			// Token: 0x04008831 RID: 34865
			General,
			// Token: 0x04008832 RID: 34866
			Details,
			// Token: 0x04008833 RID: 34867
			Never
		}
	}
}
