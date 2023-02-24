using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x020005E6 RID: 1510
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicDuplicantSensor : Switch, ISim1000ms, ISim200ms
{
	// Token: 0x06002622 RID: 9762 RVA: 0x000CD774 File Offset: 0x000CB974
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.simRenderLoadBalance = true;
	}

	// Token: 0x06002623 RID: 9763 RVA: 0x000CD784 File Offset: 0x000CB984
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.RefreshReachableCells();
		this.wasOn = this.switchedOn;
		Vector2I vector2I = Grid.CellToXY(this.NaturalBuildingCell());
		int num = Grid.XYToCell(vector2I.x, vector2I.y + this.pickupRange / 2);
		CellOffset rotatedCellOffset = new CellOffset(0, this.pickupRange / 2);
		if (this.rotatable)
		{
			rotatedCellOffset = this.rotatable.GetRotatedCellOffset(rotatedCellOffset);
			if (Grid.IsCellOffsetValid(this.NaturalBuildingCell(), rotatedCellOffset))
			{
				num = Grid.OffsetCell(this.NaturalBuildingCell(), rotatedCellOffset);
			}
		}
		this.pickupableExtents = new Extents(num, this.pickupRange / 2);
		this.pickupablesChangedEntry = GameScenePartitioner.Instance.Add("DuplicantSensor.PickupablesChanged", base.gameObject, this.pickupableExtents, GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.OnPickupablesChanged));
		this.pickupablesDirty = true;
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x000CD886 File Offset: 0x000CBA86
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.pickupablesChangedEntry);
		MinionGroupProber.Get().ReleaseProber(this);
		base.OnCleanUp();
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x000CD8AA File Offset: 0x000CBAAA
	public void Sim1000ms(float dt)
	{
		this.RefreshReachableCells();
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x000CD8B2 File Offset: 0x000CBAB2
	public void Sim200ms(float dt)
	{
		this.RefreshPickupables();
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x000CD8BC File Offset: 0x000CBABC
	private void RefreshReachableCells()
	{
		ListPool<int, LogicDuplicantSensor>.PooledList pooledList = ListPool<int, LogicDuplicantSensor>.Allocate(this.reachableCells);
		this.reachableCells.Clear();
		int num;
		int num2;
		Grid.CellToXY(this.NaturalBuildingCell(), out num, out num2);
		int num3 = num - this.pickupRange / 2;
		for (int i = num2; i < num2 + this.pickupRange + 1; i++)
		{
			for (int j = num3; j < num3 + this.pickupRange + 1; j++)
			{
				int num4 = Grid.XYToCell(j, i);
				CellOffset rotatedCellOffset = new CellOffset(j - num, i - num2);
				if (this.rotatable)
				{
					rotatedCellOffset = this.rotatable.GetRotatedCellOffset(rotatedCellOffset);
					if (Grid.IsCellOffsetValid(this.NaturalBuildingCell(), rotatedCellOffset))
					{
						num4 = Grid.OffsetCell(this.NaturalBuildingCell(), rotatedCellOffset);
						Vector2I vector2I = Grid.CellToXY(num4);
						if (Grid.IsValidCell(num4) && Grid.IsPhysicallyAccessible(num, num2, vector2I.x, vector2I.y, true))
						{
							this.reachableCells.Add(num4);
						}
					}
				}
				else if (Grid.IsValidCell(num4) && Grid.IsPhysicallyAccessible(num, num2, j, i, true))
				{
					this.reachableCells.Add(num4);
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x000CD9EF File Offset: 0x000CBBEF
	public bool IsCellReachable(int cell)
	{
		return this.reachableCells.Contains(cell);
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x000CDA00 File Offset: 0x000CBC00
	private void RefreshPickupables()
	{
		if (!this.pickupablesDirty)
		{
			return;
		}
		this.duplicants.Clear();
		ListPool<ScenePartitionerEntry, LogicDuplicantSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicDuplicantSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(this.pickupableExtents.x, this.pickupableExtents.y, this.pickupableExtents.width, this.pickupableExtents.height, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		int num = Grid.PosToCell(this);
		for (int i = 0; i < pooledList.Count; i++)
		{
			Pickupable pickupable = pooledList[i].obj as Pickupable;
			int pickupableCell = this.GetPickupableCell(pickupable);
			int cellRange = Grid.GetCellRange(num, pickupableCell);
			if (this.IsPickupableRelevantToMyInterestsAndReachable(pickupable) && cellRange <= this.pickupRange)
			{
				this.duplicants.Add(pickupable);
			}
		}
		this.SetState(this.duplicants.Count > 0);
		this.pickupablesDirty = false;
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x000CDAE0 File Offset: 0x000CBCE0
	private void OnPickupablesChanged(object data)
	{
		Pickupable pickupable = data as Pickupable;
		if (pickupable && this.IsPickupableRelevantToMyInterests(pickupable))
		{
			this.pickupablesDirty = true;
		}
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x000CDB0C File Offset: 0x000CBD0C
	private bool IsPickupableRelevantToMyInterests(Pickupable pickupable)
	{
		return pickupable.KPrefabID.HasTag(GameTags.DupeBrain);
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x000CDB24 File Offset: 0x000CBD24
	private bool IsPickupableRelevantToMyInterestsAndReachable(Pickupable pickupable)
	{
		if (!this.IsPickupableRelevantToMyInterests(pickupable))
		{
			return false;
		}
		int pickupableCell = this.GetPickupableCell(pickupable);
		return this.IsCellReachable(pickupableCell);
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x000CDB50 File Offset: 0x000CBD50
	private int GetPickupableCell(Pickupable pickupable)
	{
		return pickupable.cachedCell;
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x000CDB58 File Offset: 0x000CBD58
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x000CDB67 File Offset: 0x000CBD67
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x000CDB88 File Offset: 0x000CBD88
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x000CDC10 File Offset: 0x000CBE10
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001645 RID: 5701
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001646 RID: 5702
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001647 RID: 5703
	public int pickupRange = 4;

	// Token: 0x04001648 RID: 5704
	private bool wasOn;

	// Token: 0x04001649 RID: 5705
	private List<Pickupable> duplicants = new List<Pickupable>();

	// Token: 0x0400164A RID: 5706
	private HandleVector<int>.Handle pickupablesChangedEntry;

	// Token: 0x0400164B RID: 5707
	private bool pickupablesDirty;

	// Token: 0x0400164C RID: 5708
	private Extents pickupableExtents;

	// Token: 0x0400164D RID: 5709
	private List<int> reachableCells = new List<int>(100);
}
