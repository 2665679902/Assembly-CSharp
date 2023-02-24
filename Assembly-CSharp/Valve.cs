using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000668 RID: 1640
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Valve")]
public class Valve : Workable, ISaveLoadable
{
	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000E809D File Offset: 0x000E629D
	public float QueuedMaxFlow
	{
		get
		{
			if (this.chore == null)
			{
				return -1f;
			}
			return this.desiredFlow;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000E80B3 File Offset: 0x000E62B3
	public float DesiredFlow
	{
		get
		{
			return this.desiredFlow;
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000E80BB File Offset: 0x000E62BB
	public float MaxFlow
	{
		get
		{
			return this.valveBase.MaxFlow;
		}
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x000E80C8 File Offset: 0x000E62C8
	private void OnCopySettings(object data)
	{
		Valve component = ((GameObject)data).GetComponent<Valve>();
		if (component != null)
		{
			this.ChangeFlow(component.desiredFlow);
		}
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x000E80F8 File Offset: 0x000E62F8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.synchronizeAnims = false;
		this.valveBase.CurrentFlow = this.valveBase.MaxFlow;
		this.desiredFlow = this.valveBase.MaxFlow;
		base.Subscribe<Valve>(-905833192, Valve.OnCopySettingsDelegate);
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x000E8155 File Offset: 0x000E6355
	protected override void OnSpawn()
	{
		this.ChangeFlow(this.desiredFlow);
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x000E8174 File Offset: 0x000E6374
	protected override void OnCleanUp()
	{
		Prioritizable.RemoveRef(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x000E8188 File Offset: 0x000E6388
	public void ChangeFlow(float amount)
	{
		this.desiredFlow = Mathf.Clamp(amount, 0f, this.valveBase.MaxFlow);
		KSelectable component = base.GetComponent<KSelectable>();
		component.ToggleStatusItem(Db.Get().BuildingStatusItems.PumpingLiquidOrGas, this.desiredFlow >= 0f, this.valveBase.AccumulatorHandle);
		if (DebugHandler.InstantBuildMode)
		{
			this.UpdateFlow();
			return;
		}
		if (this.desiredFlow == this.valveBase.CurrentFlow)
		{
			if (this.chore != null)
			{
				this.chore.Cancel("desiredFlow == currentFlow");
				this.chore = null;
			}
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.ValveRequest, false);
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.PendingWork, false);
			return;
		}
		if (this.chore == null)
		{
			component.AddStatusItem(Db.Get().BuildingStatusItems.ValveRequest, this);
			component.AddStatusItem(Db.Get().BuildingStatusItems.PendingWork, this);
			this.chore = new WorkChore<Valve>(Db.Get().ChoreTypes.Toggle, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			return;
		}
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x000E82C3 File Offset: 0x000E64C3
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.UpdateFlow();
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x000E82D4 File Offset: 0x000E64D4
	public void UpdateFlow()
	{
		this.valveBase.CurrentFlow = this.desiredFlow;
		this.valveBase.UpdateAnim();
		if (this.chore != null)
		{
			this.chore.Cancel("forced complete");
		}
		this.chore = null;
		KSelectable component = base.GetComponent<KSelectable>();
		component.RemoveStatusItem(Db.Get().BuildingStatusItems.ValveRequest, false);
		component.RemoveStatusItem(Db.Get().BuildingStatusItems.PendingWork, false);
	}

	// Token: 0x04001A42 RID: 6722
	[MyCmpReq]
	private ValveBase valveBase;

	// Token: 0x04001A43 RID: 6723
	[Serialize]
	private float desiredFlow = 0.5f;

	// Token: 0x04001A44 RID: 6724
	private Chore chore;

	// Token: 0x04001A45 RID: 6725
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001A46 RID: 6726
	private static readonly EventSystem.IntraObjectHandler<Valve> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Valve>(delegate(Valve component, object data)
	{
		component.OnCopySettings(data);
	});
}
