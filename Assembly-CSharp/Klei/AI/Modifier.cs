using System;
using System.Collections.Generic;

namespace Klei.AI
{
	// Token: 0x02000D94 RID: 3476
	public class Modifier : Resource
	{
		// Token: 0x060069DD RID: 27101 RVA: 0x00292BDC File Offset: 0x00290DDC
		public Modifier(string id, string name, string description)
			: base(id, name)
		{
			this.description = description;
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x00292BF8 File Offset: 0x00290DF8
		public void Add(AttributeModifier modifier)
		{
			if (modifier.AttributeId != "")
			{
				this.SelfModifiers.Add(modifier);
			}
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x00292C18 File Offset: 0x00290E18
		public virtual void AddTo(Attributes attributes)
		{
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				attributes.Add(attributeModifier);
			}
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x00292C6C File Offset: 0x00290E6C
		public virtual void RemoveFrom(Attributes attributes)
		{
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				attributes.Remove(attributeModifier);
			}
		}

		// Token: 0x04004FA3 RID: 20387
		public string description;

		// Token: 0x04004FA4 RID: 20388
		public List<AttributeModifier> SelfModifiers = new List<AttributeModifier>();
	}
}
