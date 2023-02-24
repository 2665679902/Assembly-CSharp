using System;
using UnityEngine;

// Token: 0x020004A5 RID: 1189
[AddComponentMenu("KMonoBehaviour/scripts/AntiCluster")]
public class MoverLayerOccupier : KMonoBehaviour, ISim200ms
{
	// Token: 0x06001AE1 RID: 6881 RVA: 0x0008FCB0 File Offset: 0x0008DEB0
	private void RefreshCellOccupy()
	{
		int num = Grid.PosToCell(this);
		foreach (CellOffset cellOffset in this.cellOffsets)
		{
			int num2 = Grid.OffsetCell(num, cellOffset);
			if (this.previousCell != Grid.InvalidCell)
			{
				int num3 = Grid.OffsetCell(this.previousCell, cellOffset);
				this.UpdateCell(num3, num2);
			}
			else
			{
				this.UpdateCell(this.previousCell, num2);
			}
		}
		this.previousCell = num;
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x0008FD26 File Offset: 0x0008DF26
	public void Sim200ms(float dt)
	{
		this.RefreshCellOccupy();
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x0008FD30 File Offset: 0x0008DF30
	private void UpdateCell(int previous_cell, int current_cell)
	{
		foreach (ObjectLayer objectLayer in this.objectLayers)
		{
			if (previous_cell != Grid.InvalidCell && previous_cell != current_cell && Grid.Objects[previous_cell, (int)objectLayer] == base.gameObject)
			{
				Grid.Objects[previous_cell, (int)objectLayer] = null;
			}
			GameObject gameObject = Grid.Objects[current_cell, (int)objectLayer];
			if (gameObject == null)
			{
				Grid.Objects[current_cell, (int)objectLayer] = base.gameObject;
			}
			else
			{
				KPrefabID component = base.GetComponent<KPrefabID>();
				KPrefabID component2 = gameObject.GetComponent<KPrefabID>();
				if (component.InstanceID > component2.InstanceID)
				{
					Grid.Objects[current_cell, (int)objectLayer] = base.gameObject;
				}
			}
		}
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x0008FDE8 File Offset: 0x0008DFE8
	private void CleanUpOccupiedCells()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		foreach (CellOffset cellOffset in this.cellOffsets)
		{
			int num2 = Grid.OffsetCell(num, cellOffset);
			foreach (ObjectLayer objectLayer in this.objectLayers)
			{
				if (Grid.Objects[num2, (int)objectLayer] == base.gameObject)
				{
					Grid.Objects[num2, (int)objectLayer] = null;
				}
			}
		}
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x0008FE78 File Offset: 0x0008E078
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.RefreshCellOccupy();
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x0008FE86 File Offset: 0x0008E086
	protected override void OnCleanUp()
	{
		this.CleanUpOccupiedCells();
		base.OnCleanUp();
	}

	// Token: 0x04000EEF RID: 3823
	private int previousCell = Grid.InvalidCell;

	// Token: 0x04000EF0 RID: 3824
	public ObjectLayer[] objectLayers;

	// Token: 0x04000EF1 RID: 3825
	public CellOffset[] cellOffsets;
}
