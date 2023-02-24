using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D67 RID: 3431
	[AddComponentMenu("KMonoBehaviour/scripts/AttributeConverters")]
	public class AttributeConverters : KMonoBehaviour
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060068D5 RID: 26837 RVA: 0x0028C11E File Offset: 0x0028A31E
		public int Count
		{
			get
			{
				return this.converters.Count;
			}
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x0028C12C File Offset: 0x0028A32C
		protected override void OnPrefabInit()
		{
			foreach (AttributeInstance attributeInstance in this.GetAttributes())
			{
				foreach (AttributeConverter attributeConverter in attributeInstance.Attribute.converters)
				{
					AttributeConverterInstance attributeConverterInstance = new AttributeConverterInstance(base.gameObject, attributeConverter, attributeInstance);
					this.converters.Add(attributeConverterInstance);
				}
			}
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x0028C1D0 File Offset: 0x0028A3D0
		public AttributeConverterInstance Get(AttributeConverter converter)
		{
			foreach (AttributeConverterInstance attributeConverterInstance in this.converters)
			{
				if (attributeConverterInstance.converter == converter)
				{
					return attributeConverterInstance;
				}
			}
			return null;
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x0028C22C File Offset: 0x0028A42C
		public AttributeConverterInstance GetConverter(string id)
		{
			foreach (AttributeConverterInstance attributeConverterInstance in this.converters)
			{
				if (attributeConverterInstance.converter.Id == id)
				{
					return attributeConverterInstance;
				}
			}
			return null;
		}

		// Token: 0x04004EF2 RID: 20210
		public List<AttributeConverterInstance> converters = new List<AttributeConverterInstance>();
	}
}
