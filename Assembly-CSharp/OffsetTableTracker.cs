using System;

// Token: 0x02000875 RID: 2165
public class OffsetTableTracker : OffsetTracker
{
	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x06003E35 RID: 15925 RVA: 0x0015C271 File Offset: 0x0015A471
	private static NavGrid navGrid
	{
		get
		{
			if (OffsetTableTracker.navGridImpl == null)
			{
				OffsetTableTracker.navGridImpl = Pathfinding.Instance.GetNavGrid("MinionNavGrid");
			}
			return OffsetTableTracker.navGridImpl;
		}
	}

	// Token: 0x06003E36 RID: 15926 RVA: 0x0015C293 File Offset: 0x0015A493
	public OffsetTableTracker(CellOffset[][] table, KMonoBehaviour cmp)
	{
		this.table = table;
		this.cmp = cmp;
	}

	// Token: 0x06003E37 RID: 15927 RVA: 0x0015C2AC File Offset: 0x0015A4AC
	protected override void UpdateCell(int previous_cell, int current_cell)
	{
		if (previous_cell == current_cell)
		{
			return;
		}
		base.UpdateCell(previous_cell, current_cell);
		Extents extents = new Extents(current_cell, this.table);
		extents.height += 2;
		extents.y--;
		if (!this.solidPartitionerEntry.IsValid())
		{
			this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("OffsetTableTracker.UpdateCell", this.cmp.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnCellChanged));
			this.validNavCellChangedPartitionerEntry = GameScenePartitioner.Instance.Add("OffsetTableTracker.UpdateCell", this.cmp.gameObject, extents, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnCellChanged));
		}
		else
		{
			GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, extents);
			GameScenePartitioner.Instance.UpdatePosition(this.validNavCellChangedPartitionerEntry, extents);
		}
		this.offsets = null;
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x0015C394 File Offset: 0x0015A594
	private static bool IsValidRow(int current_cell, CellOffset[] row, int rowIdx, int[] debugIdxs)
	{
		for (int i = 1; i < row.Length; i++)
		{
			int num = Grid.OffsetCell(current_cell, row[i]);
			if (!Grid.IsValidCell(num))
			{
				return false;
			}
			if (Grid.Solid[num])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x0015C3D8 File Offset: 0x0015A5D8
	private void UpdateOffsets(int cell, CellOffset[][] table)
	{
		HashSetPool<CellOffset, OffsetTableTracker>.PooledHashSet pooledHashSet = HashSetPool<CellOffset, OffsetTableTracker>.Allocate();
		if (Grid.IsValidCell(cell))
		{
			for (int i = 0; i < table.Length; i++)
			{
				CellOffset[] array = table[i];
				if (!pooledHashSet.Contains(array[0]))
				{
					int num = Grid.OffsetCell(cell, array[0]);
					for (int j = 0; j < OffsetTableTracker.navGrid.ValidNavTypes.Length; j++)
					{
						NavType navType = OffsetTableTracker.navGrid.ValidNavTypes[j];
						if (navType != NavType.Tube && OffsetTableTracker.navGrid.NavTable.IsValid(num, navType) && OffsetTableTracker.IsValidRow(cell, array, i, this.DEBUG_rowValidIdx))
						{
							pooledHashSet.Add(array[0]);
							break;
						}
					}
				}
			}
		}
		if (this.offsets == null || this.offsets.Length != pooledHashSet.Count)
		{
			this.offsets = new CellOffset[pooledHashSet.Count];
		}
		pooledHashSet.CopyTo(this.offsets);
		pooledHashSet.Recycle();
	}

	// Token: 0x06003E3A RID: 15930 RVA: 0x0015C4C9 File Offset: 0x0015A6C9
	protected override void UpdateOffsets(int current_cell)
	{
		base.UpdateOffsets(current_cell);
		this.UpdateOffsets(current_cell, this.table);
	}

	// Token: 0x06003E3B RID: 15931 RVA: 0x0015C4DF File Offset: 0x0015A6DF
	private void OnCellChanged(object data)
	{
		this.offsets = null;
	}

	// Token: 0x06003E3C RID: 15932 RVA: 0x0015C4E8 File Offset: 0x0015A6E8
	public override void Clear()
	{
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.validNavCellChangedPartitionerEntry);
	}

	// Token: 0x06003E3D RID: 15933 RVA: 0x0015C50A File Offset: 0x0015A70A
	public static void OnPathfindingInvalidated()
	{
		OffsetTableTracker.navGridImpl = null;
	}

	// Token: 0x040028BB RID: 10427
	private readonly CellOffset[][] table;

	// Token: 0x040028BC RID: 10428
	public HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x040028BD RID: 10429
	public HandleVector<int>.Handle validNavCellChangedPartitionerEntry;

	// Token: 0x040028BE RID: 10430
	private static NavGrid navGridImpl;

	// Token: 0x040028BF RID: 10431
	private KMonoBehaviour cmp;

	// Token: 0x040028C0 RID: 10432
	private int[] DEBUG_rowValidIdx;
}
