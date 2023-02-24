using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000501 RID: 1281
[AddComponentMenu("KMonoBehaviour/Workable/Unsealable")]
public class Unsealable : Workable
{
	// Token: 0x06001E3E RID: 7742 RVA: 0x000A2084 File Offset: 0x000A0284
	private Unsealable()
	{
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x000A208C File Offset: 0x000A028C
	public override CellOffset[] GetOffsets(int cell)
	{
		if (this.facingRight)
		{
			return OffsetGroups.RightOnly;
		}
		return OffsetGroups.LeftOnly;
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x000A20A1 File Offset: 0x000A02A1
	protected override void OnPrefabInit()
	{
		this.faceTargetWhenWorking = true;
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_door_poi_kanim") };
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x000A20D0 File Offset: 0x000A02D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(3f);
		if (this.unsealed)
		{
			Deconstructable component = base.GetComponent<Deconstructable>();
			if (component != null)
			{
				component.allowDeconstruction = true;
			}
		}
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x000A210D File Offset: 0x000A030D
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x000A2118 File Offset: 0x000A0318
	protected override void OnCompleteWork(Worker worker)
	{
		this.unsealed = true;
		base.OnCompleteWork(worker);
		Deconstructable component = base.GetComponent<Deconstructable>();
		if (component != null)
		{
			component.allowDeconstruction = true;
			Game.Instance.Trigger(1980521255, base.gameObject);
		}
	}

	// Token: 0x040010ED RID: 4333
	[Serialize]
	public bool facingRight;

	// Token: 0x040010EE RID: 4334
	[Serialize]
	public bool unsealed;
}
