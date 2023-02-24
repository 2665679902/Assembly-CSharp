using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D98 RID: 3480
	public static class ModifiersExtensions
	{
		// Token: 0x060069FE RID: 27134 RVA: 0x0029320E File Offset: 0x0029140E
		public static Attributes GetAttributes(this KMonoBehaviour cmp)
		{
			return cmp.gameObject.GetAttributes();
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x0029321C File Offset: 0x0029141C
		public static Attributes GetAttributes(this GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			if (component != null)
			{
				return component.attributes;
			}
			return null;
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x00293241 File Offset: 0x00291441
		public static Amounts GetAmounts(this KMonoBehaviour cmp)
		{
			if (cmp is Modifiers)
			{
				return ((Modifiers)cmp).amounts;
			}
			return cmp.gameObject.GetAmounts();
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x00293264 File Offset: 0x00291464
		public static Amounts GetAmounts(this GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			if (component != null)
			{
				return component.amounts;
			}
			return null;
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x00293289 File Offset: 0x00291489
		public static Sicknesses GetSicknesses(this KMonoBehaviour cmp)
		{
			return cmp.gameObject.GetSicknesses();
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x00293298 File Offset: 0x00291498
		public static Sicknesses GetSicknesses(this GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			if (component != null)
			{
				return component.sicknesses;
			}
			return null;
		}
	}
}
