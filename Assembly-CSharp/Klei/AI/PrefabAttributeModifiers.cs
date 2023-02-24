using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D99 RID: 3481
	[AddComponentMenu("KMonoBehaviour/scripts/PrefabAttributeModifiers")]
	public class PrefabAttributeModifiers : KMonoBehaviour
	{
		// Token: 0x06006A04 RID: 27140 RVA: 0x002932BD File Offset: 0x002914BD
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x002932C5 File Offset: 0x002914C5
		public void AddAttributeDescriptor(AttributeModifier modifier)
		{
			this.descriptors.Add(modifier);
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x002932D3 File Offset: 0x002914D3
		public void RemovePrefabAttribute(AttributeModifier modifier)
		{
			this.descriptors.Remove(modifier);
		}

		// Token: 0x04004FAE RID: 20398
		public List<AttributeModifier> descriptors = new List<AttributeModifier>();
	}
}
