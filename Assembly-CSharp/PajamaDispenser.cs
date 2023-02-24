using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class PajamaDispenser : Workable, IDispenser
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000865 RID: 2149 RVA: 0x00031D60 File Offset: 0x0002FF60
	// (remove) Token: 0x06000866 RID: 2150 RVA: 0x00031D98 File Offset: 0x0002FF98
	public event System.Action OnStopWorkEvent;

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000867 RID: 2151 RVA: 0x00031DCD File Offset: 0x0002FFCD
	// (set) Token: 0x06000868 RID: 2152 RVA: 0x00031DD8 File Offset: 0x0002FFD8
	private WorkChore<PajamaDispenser> Chore
	{
		get
		{
			return this.chore;
		}
		set
		{
			this.chore = value;
			if (this.chore != null)
			{
				base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.DispenseRequested, null);
				return;
			}
			base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.DispenseRequested, true);
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00031E37 File Offset: 0x00030037
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (PajamaDispenser.pajamaPrefab != null)
		{
			return;
		}
		PajamaDispenser.pajamaPrefab = Assets.GetPrefab(new Tag("SleepClinicPajamas"));
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00031E64 File Offset: 0x00030064
	protected override void OnCompleteWork(Worker worker)
	{
		Vector3 targetPoint = this.GetTargetPoint();
		targetPoint.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingFront);
		Util.KInstantiate(PajamaDispenser.pajamaPrefab, targetPoint, Quaternion.identity, null, null, true, 0).SetActive(true);
		this.didCompleteChore = true;
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00031EA8 File Offset: 0x000300A8
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		if (this.Chore != null && this.Chore.smi.IsRunning())
		{
			this.Chore.Cancel("work interrupted");
		}
		this.Chore = null;
		if (!this.didCompleteChore)
		{
			this.FetchPajamas();
		}
		this.didCompleteChore = false;
		if (this.OnStopWorkEvent != null)
		{
			this.OnStopWorkEvent();
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00031F18 File Offset: 0x00030118
	[ContextMenu("fetch")]
	public void FetchPajamas()
	{
		if (this.Chore != null)
		{
			return;
		}
		this.didCompleteChore = false;
		this.Chore = new WorkChore<PajamaDispenser>(Db.Get().ChoreTypes.EquipmentFetch, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, false);
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00031F64 File Offset: 0x00030164
	public void CancelFetch()
	{
		if (this.Chore == null)
		{
			return;
		}
		this.Chore.Cancel("User Cancelled");
		this.Chore = null;
		this.didCompleteChore = false;
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.DispenseRequested, false);
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00031FB9 File Offset: 0x000301B9
	public List<Tag> DispensedItems()
	{
		return PajamaDispenser.PajamaList;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00031FC0 File Offset: 0x000301C0
	public Tag SelectedItem()
	{
		return PajamaDispenser.PajamaList[0];
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00031FCD File Offset: 0x000301CD
	public void SelectItem(Tag tag)
	{
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00031FCF File Offset: 0x000301CF
	public void OnOrderDispense()
	{
		this.FetchPajamas();
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00031FD7 File Offset: 0x000301D7
	public void OnCancelDispense()
	{
		this.CancelFetch();
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00031FDF File Offset: 0x000301DF
	public bool HasOpenChore()
	{
		return this.Chore != null;
	}

	// Token: 0x0400054D RID: 1357
	private static GameObject pajamaPrefab = null;

	// Token: 0x0400054F RID: 1359
	public bool didCompleteChore;

	// Token: 0x04000550 RID: 1360
	private WorkChore<PajamaDispenser> chore;

	// Token: 0x04000551 RID: 1361
	private static List<Tag> PajamaList = new List<Tag> { "SleepClinicPajamas" };
}
