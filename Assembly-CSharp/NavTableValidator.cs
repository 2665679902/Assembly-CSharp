using System;

// Token: 0x020003C3 RID: 963
public class NavTableValidator
{
	// Token: 0x060013F5 RID: 5109 RVA: 0x00069BBC File Offset: 0x00067DBC
	protected bool IsClear(int cell, CellOffset[] bounding_offsets, bool is_dupe)
	{
		foreach (CellOffset cellOffset in bounding_offsets)
		{
			int num = Grid.OffsetCell(cell, cellOffset);
			if (!Grid.IsWorldValidCell(num) || !NavTableValidator.IsCellPassable(num, is_dupe))
			{
				return false;
			}
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && Grid.Element[num2].IsUnstable)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x00069C1C File Offset: 0x00067E1C
	protected static bool IsCellPassable(int cell, bool is_dupe)
	{
		Grid.BuildFlags buildFlags = Grid.BuildMasks[cell] & ~(Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.FakeFloor);
		if (buildFlags == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
		{
			return true;
		}
		if (is_dupe)
		{
			return (buildFlags & Grid.BuildFlags.DupeImpassable) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor) && ((buildFlags & Grid.BuildFlags.Solid) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor) || (buildFlags & Grid.BuildFlags.DupePassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor));
		}
		return (buildFlags & (Grid.BuildFlags.Solid | Grid.BuildFlags.CritterImpassable)) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x00069C59 File Offset: 0x00067E59
	public virtual void UpdateCell(int cell, NavTable nav_table, CellOffset[] bounding_offsets)
	{
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x00069C5B File Offset: 0x00067E5B
	public virtual void Clear()
	{
	}

	// Token: 0x04000AF8 RID: 2808
	public Action<int> onDirty;
}
