using System;
using UnityEngine;

// Token: 0x0200077B RID: 1915
[AddComponentMenu("KMonoBehaviour/scripts/FloorSwitchActivator")]
public class FloorSwitchActivator : KMonoBehaviour
{
	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x060034B4 RID: 13492 RVA: 0x0011C5AB File Offset: 0x0011A7AB
	public PrimaryElement PrimaryElement
	{
		get
		{
			return this.primaryElement;
		}
	}

	// Token: 0x060034B5 RID: 13493 RVA: 0x0011C5B3 File Offset: 0x0011A7B3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
		this.OnCellChange();
	}

	// Token: 0x060034B6 RID: 13494 RVA: 0x0011C5C7 File Offset: 0x0011A7C7
	protected override void OnCleanUp()
	{
		this.Unregister();
		base.OnCleanUp();
	}

	// Token: 0x060034B7 RID: 13495 RVA: 0x0011C5D8 File Offset: 0x0011A7D8
	private void OnCellChange()
	{
		int num = Grid.PosToCell(this);
		GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, num);
		if (Grid.IsValidCell(this.last_cell_occupied) && num != this.last_cell_occupied)
		{
			this.NotifyChanged(this.last_cell_occupied);
		}
		this.NotifyChanged(num);
		this.last_cell_occupied = num;
	}

	// Token: 0x060034B8 RID: 13496 RVA: 0x0011C62D File Offset: 0x0011A82D
	private void NotifyChanged(int cell)
	{
		GameScenePartitioner.Instance.TriggerEvent(cell, GameScenePartitioner.Instance.floorSwitchActivatorChangedLayer, this);
	}

	// Token: 0x060034B9 RID: 13497 RVA: 0x0011C645 File Offset: 0x0011A845
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.Register();
	}

	// Token: 0x060034BA RID: 13498 RVA: 0x0011C653 File Offset: 0x0011A853
	protected override void OnCmpDisable()
	{
		this.Unregister();
		base.OnCmpDisable();
	}

	// Token: 0x060034BB RID: 13499 RVA: 0x0011C664 File Offset: 0x0011A864
	private void Register()
	{
		if (this.registered)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("FloorSwitchActivator.Register", this, num, GameScenePartitioner.Instance.floorSwitchActivatorLayer, null);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "FloorSwitchActivator.Register");
		this.registered = true;
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x0011C6CC File Offset: 0x0011A8CC
	private void Unregister()
	{
		if (!this.registered)
		{
			return;
		}
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		if (this.last_cell_occupied > -1)
		{
			this.NotifyChanged(this.last_cell_occupied);
		}
		this.registered = false;
	}

	// Token: 0x040020A3 RID: 8355
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x040020A4 RID: 8356
	private bool registered;

	// Token: 0x040020A5 RID: 8357
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040020A6 RID: 8358
	private int last_cell_occupied = -1;
}
