using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000478 RID: 1144
[AddComponentMenu("KMonoBehaviour/scripts/EffectArea")]
public class EffectArea : KMonoBehaviour
{
	// Token: 0x06001998 RID: 6552 RVA: 0x000896B1 File Offset: 0x000878B1
	protected override void OnPrefabInit()
	{
		this.Effect = Db.Get().effects.Get(this.EffectName);
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000896D0 File Offset: 0x000878D0
	private void Update()
	{
		int num = 0;
		int num2 = 0;
		Grid.PosToXY(base.transform.GetPosition(), out num, out num2);
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
		{
			int num3 = 0;
			int num4 = 0;
			Grid.PosToXY(minionIdentity.transform.GetPosition(), out num3, out num4);
			if (Math.Abs(num3 - num) <= this.Area && Math.Abs(num4 - num2) <= this.Area)
			{
				minionIdentity.GetComponent<Effects>().Add(this.Effect, true);
			}
		}
	}

	// Token: 0x04000E53 RID: 3667
	public string EffectName;

	// Token: 0x04000E54 RID: 3668
	public int Area;

	// Token: 0x04000E55 RID: 3669
	private Effect Effect;
}
