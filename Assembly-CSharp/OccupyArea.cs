using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000871 RID: 2161
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/OccupyArea")]
public class OccupyArea : KMonoBehaviour
{
	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x06003E11 RID: 15889 RVA: 0x0015A285 File Offset: 0x00158485
	// (set) Token: 0x06003E12 RID: 15890 RVA: 0x0015A28D File Offset: 0x0015848D
	public bool ApplyToCells
	{
		get
		{
			return this.applyToCells;
		}
		set
		{
			if (value != this.applyToCells)
			{
				if (value)
				{
					this.UpdateOccupiedArea();
				}
				else
				{
					this.ClearOccupiedArea();
				}
				this.applyToCells = value;
			}
		}
	}

	// Token: 0x06003E13 RID: 15891 RVA: 0x0015A2B0 File Offset: 0x001584B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.applyToCells)
		{
			this.UpdateOccupiedArea();
		}
	}

	// Token: 0x06003E14 RID: 15892 RVA: 0x0015A2C6 File Offset: 0x001584C6
	private void ValidatePosition()
	{
		if (!Grid.IsValidCell(Grid.PosToCell(this)))
		{
			global::Debug.LogWarning(base.name + " is outside the grid! DELETING!");
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06003E15 RID: 15893 RVA: 0x0015A2F5 File Offset: 0x001584F5
	[OnSerializing]
	private void OnSerializing()
	{
		this.ValidatePosition();
	}

	// Token: 0x06003E16 RID: 15894 RVA: 0x0015A2FD File Offset: 0x001584FD
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.ValidatePosition();
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x0015A305 File Offset: 0x00158505
	public void SetCellOffsets(CellOffset[] cells)
	{
		this.OccupiedCellsOffsets = cells;
	}

	// Token: 0x06003E18 RID: 15896 RVA: 0x0015A310 File Offset: 0x00158510
	public bool CheckIsOccupying(int checkCell)
	{
		int num = Grid.PosToCell(base.gameObject);
		if (checkCell == num)
		{
			return true;
		}
		foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
		{
			if (Grid.OffsetCell(num, cellOffset) == checkCell)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x0015A359 File Offset: 0x00158559
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.ClearOccupiedArea();
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x0015A368 File Offset: 0x00158568
	private void ClearOccupiedArea()
	{
		if (this.occupiedGridCells == null)
		{
			return;
		}
		foreach (ObjectLayer objectLayer in this.objectLayers)
		{
			if (objectLayer != ObjectLayer.NumLayers)
			{
				foreach (int num in this.occupiedGridCells)
				{
					if (Grid.Objects[num, (int)objectLayer] == base.gameObject)
					{
						Grid.Objects[num, (int)objectLayer] = null;
					}
				}
			}
		}
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x0015A3E4 File Offset: 0x001585E4
	public void UpdateOccupiedArea()
	{
		if (this.objectLayers.Length == 0)
		{
			return;
		}
		if (this.occupiedGridCells == null)
		{
			this.occupiedGridCells = new int[this.OccupiedCellsOffsets.Length];
		}
		this.ClearOccupiedArea();
		int num = Grid.PosToCell(base.gameObject);
		foreach (ObjectLayer objectLayer in this.objectLayers)
		{
			if (objectLayer != ObjectLayer.NumLayers)
			{
				for (int j = 0; j < this.OccupiedCellsOffsets.Length; j++)
				{
					CellOffset cellOffset = this.OccupiedCellsOffsets[j];
					int num2 = Grid.OffsetCell(num, cellOffset);
					Grid.Objects[num2, (int)objectLayer] = base.gameObject;
					this.occupiedGridCells[j] = num2;
				}
			}
		}
	}

	// Token: 0x06003E1C RID: 15900 RVA: 0x0015A494 File Offset: 0x00158694
	public int GetWidthInCells()
	{
		int num = int.MaxValue;
		int num2 = int.MinValue;
		foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
		{
			num = Math.Min(num, cellOffset.x);
			num2 = Math.Max(num2, cellOffset.x);
		}
		return num2 - num + 1;
	}

	// Token: 0x06003E1D RID: 15901 RVA: 0x0015A4EC File Offset: 0x001586EC
	public int GetHeightInCells()
	{
		int num = int.MaxValue;
		int num2 = int.MinValue;
		foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
		{
			num = Math.Min(num, cellOffset.y);
			num2 = Math.Max(num2, cellOffset.y);
		}
		return num2 - num + 1;
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x0015A544 File Offset: 0x00158744
	public Extents GetExtents()
	{
		return new Extents(Grid.PosToCell(base.gameObject), this.OccupiedCellsOffsets);
	}

	// Token: 0x06003E1F RID: 15903 RVA: 0x0015A55C File Offset: 0x0015875C
	public Extents GetExtents(Orientation orientation)
	{
		return new Extents(Grid.PosToCell(base.gameObject), this.OccupiedCellsOffsets, orientation);
	}

	// Token: 0x06003E20 RID: 15904 RVA: 0x0015A578 File Offset: 0x00158778
	private void OnDrawGizmosSelected()
	{
		int num = Grid.PosToCell(base.gameObject);
		if (this.OccupiedCellsOffsets != null)
		{
			foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawWireCube(Grid.CellToPos(Grid.OffsetCell(num, cellOffset)) + Vector3.right / 2f + Vector3.up / 2f, Vector3.one);
			}
		}
		if (this.AboveOccupiedCellOffsets != null)
		{
			foreach (CellOffset cellOffset2 in this.AboveOccupiedCellOffsets)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawWireCube(Grid.CellToPos(Grid.OffsetCell(num, cellOffset2)) + Vector3.right / 2f + Vector3.up / 2f, Vector3.one * 0.9f);
			}
		}
		if (this.BelowOccupiedCellOffsets != null)
		{
			foreach (CellOffset cellOffset3 in this.BelowOccupiedCellOffsets)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireCube(Grid.CellToPos(Grid.OffsetCell(num, cellOffset3)) + Vector3.right / 2f + Vector3.up / 2f, Vector3.one * 0.9f);
			}
		}
	}

	// Token: 0x06003E21 RID: 15905 RVA: 0x0015A6F0 File Offset: 0x001588F0
	public bool CanOccupyArea(int rootCell, ObjectLayer layer)
	{
		for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
		{
			CellOffset cellOffset = this.OccupiedCellsOffsets[i];
			int num = Grid.OffsetCell(rootCell, cellOffset);
			if (Grid.Objects[num, (int)layer] != null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003E22 RID: 15906 RVA: 0x0015A73C File Offset: 0x0015893C
	public bool TestArea(int rootCell, object data, Func<int, object, bool> testDelegate)
	{
		for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
		{
			CellOffset cellOffset = this.OccupiedCellsOffsets[i];
			int num = Grid.OffsetCell(rootCell, cellOffset);
			if (!testDelegate(num, data))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x0015A780 File Offset: 0x00158980
	public bool TestAreaAbove(int rootCell, object data, Func<int, object, bool> testDelegate)
	{
		if (this.AboveOccupiedCellOffsets == null)
		{
			List<CellOffset> list = new List<CellOffset>();
			for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
			{
				CellOffset cellOffset = new CellOffset(this.OccupiedCellsOffsets[i].x, this.OccupiedCellsOffsets[i].y + 1);
				if (Array.IndexOf<CellOffset>(this.OccupiedCellsOffsets, cellOffset) == -1)
				{
					list.Add(cellOffset);
				}
			}
			this.AboveOccupiedCellOffsets = list.ToArray();
		}
		for (int j = 0; j < this.AboveOccupiedCellOffsets.Length; j++)
		{
			int num = Grid.OffsetCell(rootCell, this.AboveOccupiedCellOffsets[j]);
			if (!testDelegate(num, data))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003E24 RID: 15908 RVA: 0x0015A830 File Offset: 0x00158A30
	public bool TestAreaBelow(int rootCell, object data, Func<int, object, bool> testDelegate)
	{
		if (this.BelowOccupiedCellOffsets == null)
		{
			List<CellOffset> list = new List<CellOffset>();
			for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
			{
				CellOffset cellOffset = new CellOffset(this.OccupiedCellsOffsets[i].x, this.OccupiedCellsOffsets[i].y - 1);
				if (Array.IndexOf<CellOffset>(this.OccupiedCellsOffsets, cellOffset) == -1)
				{
					list.Add(cellOffset);
				}
			}
			this.BelowOccupiedCellOffsets = list.ToArray();
		}
		for (int j = 0; j < this.BelowOccupiedCellOffsets.Length; j++)
		{
			int num = Grid.OffsetCell(rootCell, this.BelowOccupiedCellOffsets[j]);
			if (!testDelegate(num, data))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x040028A4 RID: 10404
	public CellOffset[] OccupiedCellsOffsets;

	// Token: 0x040028A5 RID: 10405
	private CellOffset[] AboveOccupiedCellOffsets;

	// Token: 0x040028A6 RID: 10406
	private CellOffset[] BelowOccupiedCellOffsets;

	// Token: 0x040028A7 RID: 10407
	private int[] occupiedGridCells;

	// Token: 0x040028A8 RID: 10408
	public ObjectLayer[] objectLayers = new ObjectLayer[0];

	// Token: 0x040028A9 RID: 10409
	[SerializeField]
	private bool applyToCells = true;
}
