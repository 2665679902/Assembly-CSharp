using System;
using UnityEngine;

// Token: 0x020009BE RID: 2494
public abstract class UtilityNetworkLink : KMonoBehaviour
{
	// Token: 0x06004A08 RID: 18952 RVA: 0x0019EF92 File Offset: 0x0019D192
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<UtilityNetworkLink>(774203113, UtilityNetworkLink.OnBuildingBrokenDelegate);
		base.Subscribe<UtilityNetworkLink>(-1735440190, UtilityNetworkLink.OnBuildingFullyRepairedDelegate);
		this.Connect();
	}

	// Token: 0x06004A09 RID: 18953 RVA: 0x0019EFC2 File Offset: 0x0019D1C2
	protected override void OnCleanUp()
	{
		base.Unsubscribe<UtilityNetworkLink>(774203113, UtilityNetworkLink.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<UtilityNetworkLink>(-1735440190, UtilityNetworkLink.OnBuildingFullyRepairedDelegate, false);
		this.Disconnect();
		base.OnCleanUp();
	}

	// Token: 0x06004A0A RID: 18954 RVA: 0x0019EFF4 File Offset: 0x0019D1F4
	protected void Connect()
	{
		if (!this.visualizeOnly && !this.connected)
		{
			this.connected = true;
			int num;
			int num2;
			this.GetCells(out num, out num2);
			this.OnConnect(num, num2);
		}
	}

	// Token: 0x06004A0B RID: 18955 RVA: 0x0019F02A File Offset: 0x0019D22A
	protected virtual void OnConnect(int cell1, int cell2)
	{
	}

	// Token: 0x06004A0C RID: 18956 RVA: 0x0019F02C File Offset: 0x0019D22C
	protected void Disconnect()
	{
		if (!this.visualizeOnly && this.connected)
		{
			this.connected = false;
			int num;
			int num2;
			this.GetCells(out num, out num2);
			this.OnDisconnect(num, num2);
		}
	}

	// Token: 0x06004A0D RID: 18957 RVA: 0x0019F062 File Offset: 0x0019D262
	protected virtual void OnDisconnect(int cell1, int cell2)
	{
	}

	// Token: 0x06004A0E RID: 18958 RVA: 0x0019F064 File Offset: 0x0019D264
	public void GetCells(out int linked_cell1, out int linked_cell2)
	{
		Building component = base.GetComponent<Building>();
		if (component != null)
		{
			Orientation orientation = component.Orientation;
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.GetCells(num, orientation, out linked_cell1, out linked_cell2);
			return;
		}
		linked_cell1 = -1;
		linked_cell2 = -1;
	}

	// Token: 0x06004A0F RID: 18959 RVA: 0x0019F0AC File Offset: 0x0019D2AC
	public void GetCells(int cell, Orientation orientation, out int linked_cell1, out int linked_cell2)
	{
		CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.link1, orientation);
		CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.link2, orientation);
		linked_cell1 = Grid.OffsetCell(cell, rotatedCellOffset);
		linked_cell2 = Grid.OffsetCell(cell, rotatedCellOffset2);
	}

	// Token: 0x06004A10 RID: 18960 RVA: 0x0019F0E8 File Offset: 0x0019D2E8
	public bool AreCellsValid(int cell, Orientation orientation)
	{
		CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.link1, orientation);
		CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.link2, orientation);
		return Grid.IsCellOffsetValid(cell, rotatedCellOffset) && Grid.IsCellOffsetValid(cell, rotatedCellOffset2);
	}

	// Token: 0x06004A11 RID: 18961 RVA: 0x0019F121 File Offset: 0x0019D321
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x06004A12 RID: 18962 RVA: 0x0019F129 File Offset: 0x0019D329
	private void OnBuildingFullyRepaired(object data)
	{
		this.Connect();
	}

	// Token: 0x06004A13 RID: 18963 RVA: 0x0019F134 File Offset: 0x0019D334
	public int GetNetworkCell()
	{
		int num;
		int num2;
		this.GetCells(out num, out num2);
		return num;
	}

	// Token: 0x040030A3 RID: 12451
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x040030A4 RID: 12452
	[SerializeField]
	public CellOffset link1;

	// Token: 0x040030A5 RID: 12453
	[SerializeField]
	public CellOffset link2;

	// Token: 0x040030A6 RID: 12454
	[SerializeField]
	public bool visualizeOnly;

	// Token: 0x040030A7 RID: 12455
	private bool connected;

	// Token: 0x040030A8 RID: 12456
	private static readonly EventSystem.IntraObjectHandler<UtilityNetworkLink> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<UtilityNetworkLink>(delegate(UtilityNetworkLink component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x040030A9 RID: 12457
	private static readonly EventSystem.IntraObjectHandler<UtilityNetworkLink> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<UtilityNetworkLink>(delegate(UtilityNetworkLink component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});
}
