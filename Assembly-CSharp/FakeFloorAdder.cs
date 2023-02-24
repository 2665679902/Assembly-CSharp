using System;

// Token: 0x0200076F RID: 1903
public class FakeFloorAdder : KMonoBehaviour
{
	// Token: 0x06003427 RID: 13351 RVA: 0x001187E8 File Offset: 0x001169E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.initiallyActive)
		{
			this.SetFloor(true);
		}
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x00118800 File Offset: 0x00116A00
	public void SetFloor(bool active)
	{
		if (this.isActive == active)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		Building component = base.GetComponent<Building>();
		foreach (CellOffset cellOffset in this.floorOffsets)
		{
			CellOffset rotatedOffset = component.GetRotatedOffset(cellOffset);
			int num2 = Grid.OffsetCell(num, rotatedOffset);
			if (active)
			{
				Grid.FakeFloor.Add(num2);
			}
			else
			{
				Grid.FakeFloor.Remove(num2);
			}
			Pathfinding.Instance.AddDirtyNavGridCell(num2);
		}
		this.isActive = active;
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x00118887 File Offset: 0x00116A87
	protected override void OnCleanUp()
	{
		this.SetFloor(false);
		base.OnCleanUp();
	}

	// Token: 0x0400203E RID: 8254
	public CellOffset[] floorOffsets;

	// Token: 0x0400203F RID: 8255
	public bool initiallyActive = true;

	// Token: 0x04002040 RID: 8256
	private bool isActive;
}
