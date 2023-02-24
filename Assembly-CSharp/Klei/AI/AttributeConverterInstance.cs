using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D66 RID: 3430
	public class AttributeConverterInstance : ModifierInstance<AttributeConverter>
	{
		// Token: 0x060068D2 RID: 26834 RVA: 0x0028C0CD File Offset: 0x0028A2CD
		public AttributeConverterInstance(GameObject game_object, AttributeConverter converter, AttributeInstance attribute_instance)
			: base(game_object, converter)
		{
			this.converter = converter;
			this.attributeInstance = attribute_instance;
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x0028C0E5 File Offset: 0x0028A2E5
		public float Evaluate()
		{
			return this.converter.multiplier * this.attributeInstance.GetTotalValue() + this.converter.baseValue;
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x0028C10A File Offset: 0x0028A30A
		public string DescriptionFromAttribute(float value, GameObject go)
		{
			return this.converter.DescriptionFromAttribute(this.Evaluate(), go);
		}

		// Token: 0x04004EF0 RID: 20208
		public AttributeConverter converter;

		// Token: 0x04004EF1 RID: 20209
		public AttributeInstance attributeInstance;
	}
}
