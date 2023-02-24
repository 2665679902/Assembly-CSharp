using System;
using UnityEngine;

// Token: 0x02000589 RID: 1417
[AddComponentMenu("KMonoBehaviour/Workable/CommandModuleWorkable")]
public class CommandModuleWorkable : Workable
{
	// Token: 0x06002289 RID: 8841 RVA: 0x000BB81C File Offset: 0x000B9A1C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsets(CommandModuleWorkable.entryOffsets);
		this.synchronizeAnims = false;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_incubator_kanim") };
		base.SetWorkTime(float.PositiveInfinity);
		this.showProgressBar = false;
	}

	// Token: 0x0600228A RID: 8842 RVA: 0x000BB871 File Offset: 0x000B9A71
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
	}

	// Token: 0x0600228B RID: 8843 RVA: 0x000BB87C File Offset: 0x000B9A7C
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (!(worker != null))
		{
			return base.OnWorkTick(worker, dt);
		}
		if (DlcManager.IsExpansion1Active())
		{
			GameObject gameObject = worker.gameObject;
			base.CompleteWork(worker);
			base.GetComponent<ClustercraftExteriorDoor>().FerryMinion(gameObject);
			return true;
		}
		GameObject gameObject2 = worker.gameObject;
		base.CompleteWork(worker);
		base.GetComponent<MinionStorage>().SerializeMinion(gameObject2);
		return true;
	}

	// Token: 0x0600228C RID: 8844 RVA: 0x000BB8D9 File Offset: 0x000B9AD9
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x000BB8E2 File Offset: 0x000B9AE2
	protected override void OnCompleteWork(Worker worker)
	{
	}

	// Token: 0x040013EC RID: 5100
	private static CellOffset[] entryOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(0, 1),
		new CellOffset(0, 2),
		new CellOffset(0, 3),
		new CellOffset(0, 4)
	};
}
