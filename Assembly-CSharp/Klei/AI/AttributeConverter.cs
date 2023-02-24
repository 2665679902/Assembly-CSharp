using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D65 RID: 3429
	public class AttributeConverter : Resource
	{
		// Token: 0x060068CE RID: 26830 RVA: 0x0028BFDD File Offset: 0x0028A1DD
		public AttributeConverter(string id, string name, string description, float multiplier, float base_value, Attribute attribute, IAttributeFormatter formatter = null)
			: base(id, name)
		{
			this.description = description;
			this.multiplier = multiplier;
			this.baseValue = base_value;
			this.attribute = attribute;
			this.formatter = formatter;
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x0028C00E File Offset: 0x0028A20E
		public AttributeConverterInstance Lookup(Component cmp)
		{
			return this.Lookup(cmp.gameObject);
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x0028C01C File Offset: 0x0028A21C
		public AttributeConverterInstance Lookup(GameObject go)
		{
			AttributeConverters component = go.GetComponent<AttributeConverters>();
			if (component != null)
			{
				return component.Get(this);
			}
			return null;
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x0028C044 File Offset: 0x0028A244
		public string DescriptionFromAttribute(float value, GameObject go)
		{
			string text;
			if (this.formatter != null)
			{
				text = this.formatter.GetFormattedValue(value, this.formatter.DeltaTimeSlice);
			}
			else if (this.attribute.formatter != null)
			{
				text = this.attribute.formatter.GetFormattedValue(value, this.attribute.formatter.DeltaTimeSlice);
			}
			else
			{
				text = GameUtil.GetFormattedSimple(value, GameUtil.TimeSlice.None, null);
			}
			if (text != null)
			{
				text = GameUtil.AddPositiveSign(text, value > 0f);
				return string.Format(this.description, text);
			}
			return null;
		}

		// Token: 0x04004EEB RID: 20203
		public string description;

		// Token: 0x04004EEC RID: 20204
		public float multiplier;

		// Token: 0x04004EED RID: 20205
		public float baseValue;

		// Token: 0x04004EEE RID: 20206
		public Attribute attribute;

		// Token: 0x04004EEF RID: 20207
		public IAttributeFormatter formatter;
	}
}
