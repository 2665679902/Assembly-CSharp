using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D63 RID: 3427
	public class Amounts : Modifications<Amount, AmountInstance>
	{
		// Token: 0x060068C0 RID: 26816 RVA: 0x0028BD9D File Offset: 0x00289F9D
		public Amounts(GameObject go)
			: base(go, null)
		{
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0028BDA7 File Offset: 0x00289FA7
		public float GetValue(string amount_id)
		{
			return base.Get(amount_id).value;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x0028BDB5 File Offset: 0x00289FB5
		public void SetValue(string amount_id, float value)
		{
			base.Get(amount_id).value = value;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x0028BDC4 File Offset: 0x00289FC4
		public override AmountInstance Add(AmountInstance instance)
		{
			instance.Activate();
			return base.Add(instance);
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x0028BDD3 File Offset: 0x00289FD3
		public override void Remove(AmountInstance instance)
		{
			instance.Deactivate();
			base.Remove(instance);
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x0028BDE4 File Offset: 0x00289FE4
		public void Cleanup()
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Deactivate();
			}
		}
	}
}
