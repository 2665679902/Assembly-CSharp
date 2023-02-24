using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProcGen;
using UnityEngine;

// Token: 0x0200079D RID: 1949
public class Grid
{
	// Token: 0x06003696 RID: 13974 RVA: 0x00130E12 File Offset: 0x0012F012
	private static void UpdateBuildMask(int i, Grid.BuildFlags flag, bool state)
	{
		if (state)
		{
			Grid.BuildMasks[i] |= flag;
			return;
		}
		Grid.BuildMasks[i] &= ~flag;
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x00130E3A File Offset: 0x0012F03A
	public static void SetSolid(int cell, bool solid, CellSolidEvent ev)
	{
		Grid.UpdateBuildMask(cell, Grid.BuildFlags.Solid, solid);
	}

	// Token: 0x06003698 RID: 13976 RVA: 0x00130E46 File Offset: 0x0012F046
	private static void UpdateVisMask(int i, Grid.VisFlags flag, bool state)
	{
		if (state)
		{
			Grid.VisMasks[i] |= flag;
			return;
		}
		Grid.VisMasks[i] &= ~flag;
	}

	// Token: 0x06003699 RID: 13977 RVA: 0x00130E6E File Offset: 0x0012F06E
	private static void UpdateNavValidatorMask(int i, Grid.NavValidatorFlags flag, bool state)
	{
		if (state)
		{
			Grid.NavValidatorMasks[i] |= flag;
			return;
		}
		Grid.NavValidatorMasks[i] &= ~flag;
	}

	// Token: 0x0600369A RID: 13978 RVA: 0x00130E96 File Offset: 0x0012F096
	private static void UpdateNavMask(int i, Grid.NavFlags flag, bool state)
	{
		if (state)
		{
			Grid.NavMasks[i] |= flag;
			return;
		}
		Grid.NavMasks[i] &= ~flag;
	}

	// Token: 0x0600369B RID: 13979 RVA: 0x00130EBE File Offset: 0x0012F0BE
	public static void ResetNavMasksAndDetails()
	{
		Grid.NavMasks = null;
		Grid.tubeEntrances.Clear();
		Grid.restrictions.Clear();
		Grid.suitMarkers.Clear();
	}

	// Token: 0x0600369C RID: 13980 RVA: 0x00130EE4 File Offset: 0x0012F0E4
	public static bool DEBUG_GetRestrictions(int cell, out Grid.Restriction restriction)
	{
		return Grid.restrictions.TryGetValue(cell, out restriction);
	}

	// Token: 0x0600369D RID: 13981 RVA: 0x00130EF4 File Offset: 0x0012F0F4
	public static void RegisterRestriction(int cell, Grid.Restriction.Orientation orientation)
	{
		Grid.HasAccessDoor[cell] = true;
		Grid.restrictions[cell] = new Grid.Restriction
		{
			DirectionMasksForMinionInstanceID = new Dictionary<int, Grid.Restriction.Directions>(),
			orientation = orientation
		};
	}

	// Token: 0x0600369E RID: 13982 RVA: 0x00130F35 File Offset: 0x0012F135
	public static void UnregisterRestriction(int cell)
	{
		Grid.restrictions.Remove(cell);
		Grid.HasAccessDoor[cell] = false;
	}

	// Token: 0x0600369F RID: 13983 RVA: 0x00130F4F File Offset: 0x0012F14F
	public static void SetRestriction(int cell, int minionInstanceID, Grid.Restriction.Directions directions)
	{
		Grid.restrictions[cell].DirectionMasksForMinionInstanceID[minionInstanceID] = directions;
	}

	// Token: 0x060036A0 RID: 13984 RVA: 0x00130F68 File Offset: 0x0012F168
	public static void ClearRestriction(int cell, int minionInstanceID)
	{
		Grid.restrictions[cell].DirectionMasksForMinionInstanceID.Remove(minionInstanceID);
	}

	// Token: 0x060036A1 RID: 13985 RVA: 0x00130F84 File Offset: 0x0012F184
	public static bool HasPermission(int cell, int minionInstanceID, int fromCell, NavType fromNavType)
	{
		if (!Grid.HasAccessDoor[cell])
		{
			return true;
		}
		Grid.Restriction restriction = Grid.restrictions[cell];
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = Grid.CellToXY(fromCell);
		Grid.Restriction.Directions directions = (Grid.Restriction.Directions)0;
		int num = vector2I.x - vector2I2.x;
		int num2 = vector2I.y - vector2I2.y;
		switch (restriction.orientation)
		{
		case Grid.Restriction.Orientation.Vertical:
			if (num < 0)
			{
				directions |= Grid.Restriction.Directions.Left;
			}
			if (num > 0)
			{
				directions |= Grid.Restriction.Directions.Right;
			}
			break;
		case Grid.Restriction.Orientation.Horizontal:
			if (num2 > 0)
			{
				directions |= Grid.Restriction.Directions.Left;
			}
			if (num2 < 0)
			{
				directions |= Grid.Restriction.Directions.Right;
			}
			break;
		case Grid.Restriction.Orientation.SingleCell:
			if (Math.Abs(num) != 1 && Math.Abs(num2) != 1 && fromNavType != NavType.Teleport)
			{
				directions |= Grid.Restriction.Directions.Teleport;
			}
			break;
		}
		Grid.Restriction.Directions directions2 = (Grid.Restriction.Directions)0;
		return (!restriction.DirectionMasksForMinionInstanceID.TryGetValue(minionInstanceID, out directions2) && !restriction.DirectionMasksForMinionInstanceID.TryGetValue(-1, out directions2)) || (directions2 & directions) == (Grid.Restriction.Directions)0;
	}

	// Token: 0x060036A2 RID: 13986 RVA: 0x00131064 File Offset: 0x0012F264
	public static void RegisterTubeEntrance(int cell, int reservationCapacity)
	{
		DebugUtil.Assert(!Grid.tubeEntrances.ContainsKey(cell));
		Grid.HasTubeEntrance[cell] = true;
		Grid.tubeEntrances[cell] = new Grid.TubeEntrance
		{
			reservationCapacity = reservationCapacity,
			reservedInstanceIDs = new HashSet<int>()
		};
	}

	// Token: 0x060036A3 RID: 13987 RVA: 0x001310B8 File Offset: 0x0012F2B8
	public static void UnregisterTubeEntrance(int cell)
	{
		DebugUtil.Assert(Grid.tubeEntrances.ContainsKey(cell));
		Grid.HasTubeEntrance[cell] = false;
		Grid.tubeEntrances.Remove(cell);
	}

	// Token: 0x060036A4 RID: 13988 RVA: 0x001310E4 File Offset: 0x0012F2E4
	public static bool ReserveTubeEntrance(int cell, int minionInstanceID, bool reserve)
	{
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		HashSet<int> reservedInstanceIDs = tubeEntrance.reservedInstanceIDs;
		if (!reserve)
		{
			return reservedInstanceIDs.Remove(minionInstanceID);
		}
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		if (reservedInstanceIDs.Count == tubeEntrance.reservationCapacity)
		{
			return false;
		}
		DebugUtil.Assert(reservedInstanceIDs.Add(minionInstanceID));
		return true;
	}

	// Token: 0x060036A5 RID: 13989 RVA: 0x0013113C File Offset: 0x0012F33C
	public static void SetTubeEntranceReservationCapacity(int cell, int newReservationCapacity)
	{
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		tubeEntrance.reservationCapacity = newReservationCapacity;
		Grid.tubeEntrances[cell] = tubeEntrance;
	}

	// Token: 0x060036A6 RID: 13990 RVA: 0x0013117C File Offset: 0x0012F37C
	public static bool HasUsableTubeEntrance(int cell, int minionInstanceID)
	{
		if (!Grid.HasTubeEntrance[cell])
		{
			return false;
		}
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		if (!tubeEntrance.operational)
		{
			return false;
		}
		HashSet<int> reservedInstanceIDs = tubeEntrance.reservedInstanceIDs;
		return reservedInstanceIDs.Count < tubeEntrance.reservationCapacity || reservedInstanceIDs.Contains(minionInstanceID);
	}

	// Token: 0x060036A7 RID: 13991 RVA: 0x001311CC File Offset: 0x0012F3CC
	public static bool HasReservedTubeEntrance(int cell, int minionInstanceID)
	{
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		return Grid.tubeEntrances[cell].reservedInstanceIDs.Contains(minionInstanceID);
	}

	// Token: 0x060036A8 RID: 13992 RVA: 0x001311F4 File Offset: 0x0012F3F4
	public static void SetTubeEntranceOperational(int cell, bool operational)
	{
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		tubeEntrance.operational = operational;
		Grid.tubeEntrances[cell] = tubeEntrance;
	}

	// Token: 0x060036A9 RID: 13993 RVA: 0x00131234 File Offset: 0x0012F434
	public static void RegisterSuitMarker(int cell)
	{
		DebugUtil.Assert(!Grid.HasSuitMarker[cell]);
		Grid.HasSuitMarker[cell] = true;
		Grid.suitMarkers[cell] = new Grid.SuitMarker
		{
			suitCount = 0,
			lockerCount = 0,
			flags = Grid.SuitMarker.Flags.Operational,
			minionIDsWithSuitReservations = new HashSet<int>(),
			minionIDsWithEmptyLockerReservations = new HashSet<int>()
		};
	}

	// Token: 0x060036AA RID: 13994 RVA: 0x001312A4 File Offset: 0x0012F4A4
	public static void UnregisterSuitMarker(int cell)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.HasSuitMarker[cell] = false;
		Grid.suitMarkers.Remove(cell);
	}

	// Token: 0x060036AB RID: 13995 RVA: 0x001312D0 File Offset: 0x0012F4D0
	public static bool ReserveSuit(int cell, int minionInstanceID, bool reserve)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithSuitReservations = suitMarker.minionIDsWithSuitReservations;
		if (!reserve)
		{
			return minionIDsWithSuitReservations.Remove(minionInstanceID);
		}
		if (minionIDsWithSuitReservations.Count == suitMarker.suitCount)
		{
			return false;
		}
		DebugUtil.Assert(minionIDsWithSuitReservations.Add(minionInstanceID));
		return true;
	}

	// Token: 0x060036AC RID: 13996 RVA: 0x00131328 File Offset: 0x0012F528
	public static bool ReserveEmptyLocker(int cell, int minionInstanceID, bool reserve)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithEmptyLockerReservations = suitMarker.minionIDsWithEmptyLockerReservations;
		if (!reserve)
		{
			return minionIDsWithEmptyLockerReservations.Remove(minionInstanceID);
		}
		if (minionIDsWithEmptyLockerReservations.Count == suitMarker.emptyLockerCount)
		{
			return false;
		}
		DebugUtil.Assert(minionIDsWithEmptyLockerReservations.Add(minionInstanceID));
		return true;
	}

	// Token: 0x060036AD RID: 13997 RVA: 0x00131384 File Offset: 0x0012F584
	public static void UpdateSuitMarker(int cell, int fullLockerCount, int emptyLockerCount, Grid.SuitMarker.Flags flags, PathFinder.PotentialPath.Flags pathFlags)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		suitMarker.suitCount = fullLockerCount;
		suitMarker.lockerCount = fullLockerCount + emptyLockerCount;
		suitMarker.flags = flags;
		suitMarker.pathFlags = pathFlags;
		Grid.suitMarkers[cell] = suitMarker;
	}

	// Token: 0x060036AE RID: 13998 RVA: 0x001313DC File Offset: 0x0012F5DC
	public static bool TryGetSuitMarkerFlags(int cell, out Grid.SuitMarker.Flags flags, out PathFinder.PotentialPath.Flags pathFlags)
	{
		if (Grid.HasSuitMarker[cell])
		{
			flags = Grid.suitMarkers[cell].flags;
			pathFlags = Grid.suitMarkers[cell].pathFlags;
			return true;
		}
		flags = (Grid.SuitMarker.Flags)0;
		pathFlags = PathFinder.PotentialPath.Flags.None;
		return false;
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x00131418 File Offset: 0x0012F618
	public static bool HasSuit(int cell, int minionInstanceID)
	{
		if (!Grid.HasSuitMarker[cell])
		{
			return false;
		}
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithSuitReservations = suitMarker.minionIDsWithSuitReservations;
		return minionIDsWithSuitReservations.Count < suitMarker.suitCount || minionIDsWithSuitReservations.Contains(minionInstanceID);
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x00131460 File Offset: 0x0012F660
	public static bool HasEmptyLocker(int cell, int minionInstanceID)
	{
		if (!Grid.HasSuitMarker[cell])
		{
			return false;
		}
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithEmptyLockerReservations = suitMarker.minionIDsWithEmptyLockerReservations;
		return minionIDsWithEmptyLockerReservations.Count < suitMarker.emptyLockerCount || minionIDsWithEmptyLockerReservations.Contains(minionInstanceID);
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x001314A8 File Offset: 0x0012F6A8
	public unsafe static void InitializeCells()
	{
		for (int num = 0; num != Grid.WidthInCells * Grid.HeightInCells; num++)
		{
			ushort num2 = Grid.elementIdx[num];
			Element element = ElementLoader.elements[(int)num2];
			Grid.Element[num] = element;
			if (element.IsSolid)
			{
				Grid.BuildMasks[num] |= Grid.BuildFlags.Solid;
			}
			else
			{
				Grid.BuildMasks[num] &= ~Grid.BuildFlags.Solid;
			}
			Grid.RenderedByWorld[num] = element.substance != null && element.substance.renderedByWorld && Grid.Objects[num, 9] == null;
		}
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x00131555 File Offset: 0x0012F755
	public static bool IsInitialized()
	{
		return Grid.mass != null;
	}

	// Token: 0x060036B3 RID: 14003 RVA: 0x00131564 File Offset: 0x0012F764
	public static int GetCellInDirection(int cell, Direction d)
	{
		switch (d)
		{
		case Direction.Up:
			return Grid.CellAbove(cell);
		case Direction.Right:
			return Grid.CellRight(cell);
		case Direction.Down:
			return Grid.CellBelow(cell);
		case Direction.Left:
			return Grid.CellLeft(cell);
		case Direction.None:
			return cell;
		}
		return -1;
	}

	// Token: 0x060036B4 RID: 14004 RVA: 0x001315B0 File Offset: 0x0012F7B0
	public static bool Raycast(int cell, Vector2I direction, out int hitDistance, int maxDistance = 100, Grid.BuildFlags layerMask = Grid.BuildFlags.Any)
	{
		bool flag = false;
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = vector2I + direction * maxDistance;
		int num = cell;
		int num2 = Grid.XYToCell(vector2I2.x, vector2I2.y);
		int num3 = 0;
		int num4 = 0;
		float num5 = (float)maxDistance * 0.5f;
		while ((float)num3 < num5)
		{
			if (!Grid.IsValidCell(num) || (Grid.BuildMasks[num] & layerMask) != ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
			{
				flag = true;
				break;
			}
			if (!Grid.IsValidCell(num2) || (Grid.BuildMasks[num2] & layerMask) != ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
			{
				num4 = maxDistance - num3;
			}
			vector2I += direction;
			vector2I2 -= direction;
			num = Grid.XYToCell(vector2I.x, vector2I.y);
			num2 = Grid.XYToCell(vector2I2.x, vector2I2.y);
			num3++;
		}
		if (!flag && maxDistance % 2 == 0)
		{
			flag = !Grid.IsValidCell(num2) || (Grid.BuildMasks[num2] & layerMask) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
		}
		hitDistance = (flag ? num3 : ((num4 > 0) ? num4 : maxDistance));
		return flag | (hitDistance == num4);
	}

	// Token: 0x060036B5 RID: 14005 RVA: 0x001316B1 File Offset: 0x0012F8B1
	public static int CellAbove(int cell)
	{
		return cell + Grid.WidthInCells;
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x001316BA File Offset: 0x0012F8BA
	public static int CellBelow(int cell)
	{
		return cell - Grid.WidthInCells;
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x001316C3 File Offset: 0x0012F8C3
	public static int CellLeft(int cell)
	{
		if (cell % Grid.WidthInCells <= 0)
		{
			return -1;
		}
		return cell - 1;
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x001316D4 File Offset: 0x0012F8D4
	public static int CellRight(int cell)
	{
		if (cell % Grid.WidthInCells >= Grid.WidthInCells - 1)
		{
			return -1;
		}
		return cell + 1;
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x001316EC File Offset: 0x0012F8EC
	public static CellOffset GetOffset(int cell)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		return new CellOffset(num, num2);
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x00131710 File Offset: 0x0012F910
	public static int CellUpLeft(int cell)
	{
		int num = -1;
		if (cell < (Grid.HeightInCells - 1) * Grid.WidthInCells && cell % Grid.WidthInCells > 0)
		{
			num = cell - 1 + Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x00131744 File Offset: 0x0012F944
	public static int CellUpRight(int cell)
	{
		int num = -1;
		if (cell < (Grid.HeightInCells - 1) * Grid.WidthInCells && cell % Grid.WidthInCells < Grid.WidthInCells - 1)
		{
			num = cell + 1 + Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060036BC RID: 14012 RVA: 0x00131780 File Offset: 0x0012F980
	public static int CellDownLeft(int cell)
	{
		int num = -1;
		if (cell > Grid.WidthInCells && cell % Grid.WidthInCells > 0)
		{
			num = cell - 1 - Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x001317AC File Offset: 0x0012F9AC
	public static int CellDownRight(int cell)
	{
		int num = -1;
		if (cell >= Grid.WidthInCells && cell % Grid.WidthInCells < Grid.WidthInCells - 1)
		{
			num = cell + 1 - Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060036BE RID: 14014 RVA: 0x001317DE File Offset: 0x0012F9DE
	public static bool IsCellLeftOf(int cell, int other_cell)
	{
		return Grid.CellColumn(cell) < Grid.CellColumn(other_cell);
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x001317F0 File Offset: 0x0012F9F0
	public static bool IsCellOffsetOf(int cell, int target_cell, CellOffset[] target_offsets)
	{
		int num = target_offsets.Length;
		for (int i = 0; i < num; i++)
		{
			if (cell == Grid.OffsetCell(target_cell, target_offsets[i]))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x00131820 File Offset: 0x0012FA20
	public static bool IsCellOffsetOf(int cell, GameObject target, CellOffset[] target_offsets)
	{
		int num = Grid.PosToCell(target);
		return Grid.IsCellOffsetOf(cell, num, target_offsets);
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x0013183C File Offset: 0x0012FA3C
	public static int GetCellDistance(int cell_a, int cell_b)
	{
		CellOffset offset = Grid.GetOffset(cell_a, cell_b);
		return Math.Abs(offset.x) + Math.Abs(offset.y);
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x00131868 File Offset: 0x0012FA68
	public static int GetCellRange(int cell_a, int cell_b)
	{
		CellOffset offset = Grid.GetOffset(cell_a, cell_b);
		return Math.Max(Math.Abs(offset.x), Math.Abs(offset.y));
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x00131898 File Offset: 0x0012FA98
	public static CellOffset GetOffset(int base_cell, int offset_cell)
	{
		int num;
		int num2;
		Grid.CellToXY(base_cell, out num, out num2);
		int num3;
		int num4;
		Grid.CellToXY(offset_cell, out num3, out num4);
		return new CellOffset(num3 - num, num4 - num2);
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x001318C4 File Offset: 0x0012FAC4
	public static int OffsetCell(int cell, CellOffset offset)
	{
		return cell + offset.x + offset.y * Grid.WidthInCells;
	}

	// Token: 0x060036C5 RID: 14021 RVA: 0x001318DB File Offset: 0x0012FADB
	public static int OffsetCell(int cell, int x, int y)
	{
		return cell + x + y * Grid.WidthInCells;
	}

	// Token: 0x060036C6 RID: 14022 RVA: 0x001318E8 File Offset: 0x0012FAE8
	public static bool IsCellOffsetValid(int cell, int x, int y)
	{
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		return num + x >= 0 && num + x < Grid.WidthInCells && num2 + y >= 0 && num2 + y < Grid.HeightInCells;
	}

	// Token: 0x060036C7 RID: 14023 RVA: 0x00131923 File Offset: 0x0012FB23
	public static bool IsCellOffsetValid(int cell, CellOffset offset)
	{
		return Grid.IsCellOffsetValid(cell, offset.x, offset.y);
	}

	// Token: 0x060036C8 RID: 14024 RVA: 0x00131937 File Offset: 0x0012FB37
	public static int PosToCell(StateMachine.Instance smi)
	{
		return Grid.PosToCell(smi.transform.GetPosition());
	}

	// Token: 0x060036C9 RID: 14025 RVA: 0x00131949 File Offset: 0x0012FB49
	public static int PosToCell(GameObject go)
	{
		return Grid.PosToCell(go.transform.GetPosition());
	}

	// Token: 0x060036CA RID: 14026 RVA: 0x0013195B File Offset: 0x0012FB5B
	public static int PosToCell(KMonoBehaviour cmp)
	{
		return Grid.PosToCell(cmp.transform.GetPosition());
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x00131970 File Offset: 0x0012FB70
	public static bool IsValidBuildingCell(int cell)
	{
		if (!Grid.IsWorldValidCell(cell))
		{
			return false;
		}
		WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[cell]);
		if (world == null)
		{
			return false;
		}
		Vector2I vector2I = Grid.CellToXY(cell);
		return (float)vector2I.x >= world.minimumBounds.x && (float)vector2I.x <= world.maximumBounds.x && (float)vector2I.y >= world.minimumBounds.y && (float)vector2I.y <= world.maximumBounds.y - (float)Grid.TopBorderHeight;
	}

	// Token: 0x060036CC RID: 14028 RVA: 0x00131A07 File Offset: 0x0012FC07
	public static bool IsWorldValidCell(int cell)
	{
		return Grid.IsValidCell(cell) && Grid.WorldIdx[cell] != ClusterManager.INVALID_WORLD_IDX;
	}

	// Token: 0x060036CD RID: 14029 RVA: 0x00131A24 File Offset: 0x0012FC24
	public static bool IsValidCell(int cell)
	{
		return cell >= 0 && cell < Grid.CellCount;
	}

	// Token: 0x060036CE RID: 14030 RVA: 0x00131A34 File Offset: 0x0012FC34
	public static bool IsValidCellInWorld(int cell, int world)
	{
		return cell >= 0 && cell < Grid.CellCount && (int)Grid.WorldIdx[cell] == world;
	}

	// Token: 0x060036CF RID: 14031 RVA: 0x00131A4E File Offset: 0x0012FC4E
	public static bool IsActiveWorld(int cell)
	{
		return ClusterManager.Instance != null && ClusterManager.Instance.activeWorldId == (int)Grid.WorldIdx[cell];
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x00131A72 File Offset: 0x0012FC72
	public static bool AreCellsInSameWorld(int cell, int world_cell)
	{
		return Grid.IsValidCell(cell) && Grid.IsValidCell(world_cell) && Grid.WorldIdx[cell] == Grid.WorldIdx[world_cell];
	}

	// Token: 0x060036D1 RID: 14033 RVA: 0x00131A96 File Offset: 0x0012FC96
	public static bool IsCellOpenToSpace(int cell)
	{
		return !Grid.IsSolidCell(cell) && !(Grid.Objects[cell, 2] != null) && global::World.Instance.zoneRenderData.GetSubWorldZoneType(cell) == SubWorld.ZoneType.Space;
	}

	// Token: 0x060036D2 RID: 14034 RVA: 0x00131ACC File Offset: 0x0012FCCC
	public static int PosToCell(Vector2 pos)
	{
		float x = pos.x;
		int num = (int)(pos.y + 0.05f);
		int num2 = (int)x;
		return num * Grid.WidthInCells + num2;
	}

	// Token: 0x060036D3 RID: 14035 RVA: 0x00131AF8 File Offset: 0x0012FCF8
	public static int PosToCell(Vector3 pos)
	{
		float x = pos.x;
		int num = (int)(pos.y + 0.05f);
		int num2 = (int)x;
		return num * Grid.WidthInCells + num2;
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x00131B24 File Offset: 0x0012FD24
	public static void PosToXY(Vector3 pos, out int x, out int y)
	{
		Grid.CellToXY(Grid.PosToCell(pos), out x, out y);
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x00131B33 File Offset: 0x0012FD33
	public static void PosToXY(Vector3 pos, out Vector2I xy)
	{
		Grid.CellToXY(Grid.PosToCell(pos), out xy.x, out xy.y);
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x00131B4C File Offset: 0x0012FD4C
	public static Vector2I PosToXY(Vector3 pos)
	{
		Vector2I vector2I;
		Grid.CellToXY(Grid.PosToCell(pos), out vector2I.x, out vector2I.y);
		return vector2I;
	}

	// Token: 0x060036D7 RID: 14039 RVA: 0x00131B73 File Offset: 0x0012FD73
	public static int XYToCell(int x, int y)
	{
		return x + y * Grid.WidthInCells;
	}

	// Token: 0x060036D8 RID: 14040 RVA: 0x00131B7E File Offset: 0x0012FD7E
	public static void CellToXY(int cell, out int x, out int y)
	{
		x = Grid.CellColumn(cell);
		y = Grid.CellRow(cell);
	}

	// Token: 0x060036D9 RID: 14041 RVA: 0x00131B90 File Offset: 0x0012FD90
	public static Vector2I CellToXY(int cell)
	{
		return new Vector2I(Grid.CellColumn(cell), Grid.CellRow(cell));
	}

	// Token: 0x060036DA RID: 14042 RVA: 0x00131BA4 File Offset: 0x0012FDA4
	public static Vector3 CellToPos(int cell, float x_offset, float y_offset, float z_offset)
	{
		int widthInCells = Grid.WidthInCells;
		float num = Grid.CellSizeInMeters * (float)(cell % widthInCells);
		float num2 = Grid.CellSizeInMeters * (float)(cell / widthInCells);
		return new Vector3(num + x_offset, num2 + y_offset, z_offset);
	}

	// Token: 0x060036DB RID: 14043 RVA: 0x00131BD8 File Offset: 0x0012FDD8
	public static Vector3 CellToPos(int cell)
	{
		int widthInCells = Grid.WidthInCells;
		float num = Grid.CellSizeInMeters * (float)(cell % widthInCells);
		float num2 = Grid.CellSizeInMeters * (float)(cell / widthInCells);
		return new Vector3(num, num2, 0f);
	}

	// Token: 0x060036DC RID: 14044 RVA: 0x00131C0C File Offset: 0x0012FE0C
	public static Vector3 CellToPos2D(int cell)
	{
		int widthInCells = Grid.WidthInCells;
		float num = Grid.CellSizeInMeters * (float)(cell % widthInCells);
		float num2 = Grid.CellSizeInMeters * (float)(cell / widthInCells);
		return new Vector2(num, num2);
	}

	// Token: 0x060036DD RID: 14045 RVA: 0x00131C3F File Offset: 0x0012FE3F
	public static int CellRow(int cell)
	{
		return cell / Grid.WidthInCells;
	}

	// Token: 0x060036DE RID: 14046 RVA: 0x00131C48 File Offset: 0x0012FE48
	public static int CellColumn(int cell)
	{
		return cell % Grid.WidthInCells;
	}

	// Token: 0x060036DF RID: 14047 RVA: 0x00131C51 File Offset: 0x0012FE51
	public static int ClampX(int x)
	{
		return Math.Min(Math.Max(x, 0), Grid.WidthInCells - 1);
	}

	// Token: 0x060036E0 RID: 14048 RVA: 0x00131C66 File Offset: 0x0012FE66
	public static int ClampY(int y)
	{
		return Math.Min(Math.Max(y, 0), Grid.HeightInCells - 1);
	}

	// Token: 0x060036E1 RID: 14049 RVA: 0x00131C7C File Offset: 0x0012FE7C
	public static Vector2I Constrain(Vector2I val)
	{
		val.x = Mathf.Max(0, Mathf.Min(val.x, Grid.WidthInCells - 1));
		val.y = Mathf.Max(0, Mathf.Min(val.y, Grid.HeightInCells - 1));
		return val;
	}

	// Token: 0x060036E2 RID: 14050 RVA: 0x00131CC8 File Offset: 0x0012FEC8
	public static void Reveal(int cell, byte visibility = 255, bool forceReveal = false)
	{
		bool flag = Grid.Spawnable[cell] == 0 && visibility > 0;
		Grid.Spawnable[cell] = Math.Max(visibility, Grid.Visible[cell]);
		if (forceReveal || !Grid.PreventFogOfWarReveal[cell])
		{
			Grid.Visible[cell] = Math.Max(visibility, Grid.Visible[cell]);
		}
		if (flag && Grid.OnReveal != null)
		{
			Grid.OnReveal(cell);
		}
	}

	// Token: 0x060036E3 RID: 14051 RVA: 0x00131D31 File Offset: 0x0012FF31
	public static ObjectLayer GetObjectLayerForConduitType(ConduitType conduit_type)
	{
		switch (conduit_type)
		{
		case ConduitType.Gas:
			return ObjectLayer.GasConduitConnection;
		case ConduitType.Liquid:
			return ObjectLayer.LiquidConduitConnection;
		case ConduitType.Solid:
			return ObjectLayer.SolidConduitConnection;
		default:
			throw new ArgumentException("Invalid value.", "conduit_type");
		}
	}

	// Token: 0x060036E4 RID: 14052 RVA: 0x00131D64 File Offset: 0x0012FF64
	public static Vector3 CellToPos(int cell, CellAlignment alignment, Grid.SceneLayer layer)
	{
		switch (alignment)
		{
		case CellAlignment.Bottom:
			return Grid.CellToPosCBC(cell, layer);
		case CellAlignment.Top:
			return Grid.CellToPosCTC(cell, layer);
		case CellAlignment.Left:
			return Grid.CellToPosLCC(cell, layer);
		case CellAlignment.Right:
			return Grid.CellToPosRCC(cell, layer);
		case CellAlignment.RandomInternal:
		{
			Vector3 vector = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0f, 0f);
			return Grid.CellToPosCCC(cell, layer) + vector;
		}
		}
		return Grid.CellToPosCCC(cell, layer);
	}

	// Token: 0x060036E5 RID: 14053 RVA: 0x00131DE6 File Offset: 0x0012FFE6
	public static float GetLayerZ(Grid.SceneLayer layer)
	{
		return -Grid.HalfCellSizeInMeters - Grid.CellSizeInMeters * (float)layer * Grid.LayerMultiplier;
	}

	// Token: 0x060036E6 RID: 14054 RVA: 0x00131DFD File Offset: 0x0012FFFD
	public static Vector3 CellToPosCCC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, Grid.HalfCellSizeInMeters, Grid.GetLayerZ(layer));
	}

	// Token: 0x060036E7 RID: 14055 RVA: 0x00131E15 File Offset: 0x00130015
	public static Vector3 CellToPosCBC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, 0.01f, Grid.GetLayerZ(layer));
	}

	// Token: 0x060036E8 RID: 14056 RVA: 0x00131E2D File Offset: 0x0013002D
	public static Vector3 CellToPosCCF(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, Grid.HalfCellSizeInMeters, -Grid.CellSizeInMeters * (float)layer * Grid.LayerMultiplier);
	}

	// Token: 0x060036E9 RID: 14057 RVA: 0x00131E4E File Offset: 0x0013004E
	public static Vector3 CellToPosLCC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, 0.01f, Grid.HalfCellSizeInMeters, Grid.GetLayerZ(layer));
	}

	// Token: 0x060036EA RID: 14058 RVA: 0x00131E66 File Offset: 0x00130066
	public static Vector3 CellToPosRCC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.CellSizeInMeters - 0.01f, Grid.HalfCellSizeInMeters, Grid.GetLayerZ(layer));
	}

	// Token: 0x060036EB RID: 14059 RVA: 0x00131E84 File Offset: 0x00130084
	public static Vector3 CellToPosCTC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, Grid.CellSizeInMeters - 0.01f, Grid.GetLayerZ(layer));
	}

	// Token: 0x060036EC RID: 14060 RVA: 0x00131EA2 File Offset: 0x001300A2
	public static bool IsSolidCell(int cell)
	{
		return Grid.IsValidCell(cell) && Grid.Solid[cell];
	}

	// Token: 0x060036ED RID: 14061 RVA: 0x00131EBC File Offset: 0x001300BC
	public unsafe static bool IsSubstantialLiquid(int cell, float threshold = 0.35f)
	{
		if (Grid.IsValidCell(cell))
		{
			ushort num = Grid.elementIdx[cell];
			if ((int)num < ElementLoader.elements.Count)
			{
				Element element = ElementLoader.elements[(int)num];
				if (element.IsLiquid && Grid.mass[cell] >= element.defaultValues.mass * threshold)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060036EE RID: 14062 RVA: 0x00131F1C File Offset: 0x0013011C
	public static bool IsVisiblyInLiquid(Vector2 pos)
	{
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && Grid.IsLiquid(num))
		{
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && Grid.IsLiquid(num2))
			{
				return true;
			}
			float num3 = Grid.Mass[num];
			float num4 = (float)((int)pos.y) - pos.y;
			if (num3 / 1000f <= num4)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060036EF RID: 14063 RVA: 0x00131F80 File Offset: 0x00130180
	public static bool IsLiquid(int cell)
	{
		return ElementLoader.elements[(int)Grid.ElementIdx[cell]].IsLiquid;
	}

	// Token: 0x060036F0 RID: 14064 RVA: 0x00131FA1 File Offset: 0x001301A1
	public static bool IsGas(int cell)
	{
		return ElementLoader.elements[(int)Grid.ElementIdx[cell]].IsGas;
	}

	// Token: 0x060036F1 RID: 14065 RVA: 0x00131FC4 File Offset: 0x001301C4
	public static void GetVisibleExtents(out int min_x, out int min_y, out int max_x, out int max_y)
	{
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		min_y = (int)vector2.y;
		max_y = (int)(vector.y + 0.5f);
		min_x = (int)vector2.x;
		max_x = (int)(vector.x + 0.5f);
	}

	// Token: 0x060036F2 RID: 14066 RVA: 0x0013205D File Offset: 0x0013025D
	public static void GetVisibleExtents(out Vector2I min, out Vector2I max)
	{
		Grid.GetVisibleExtents(out min.x, out min.y, out max.x, out max.y);
	}

	// Token: 0x060036F3 RID: 14067 RVA: 0x0013207C File Offset: 0x0013027C
	public static bool IsVisible(int cell)
	{
		return Grid.Visible[cell] > 0 || !PropertyTextures.IsFogOfWarEnabled;
	}

	// Token: 0x060036F4 RID: 14068 RVA: 0x00132092 File Offset: 0x00130292
	public static bool VisibleBlockingCB(int cell)
	{
		return !Grid.Transparent[cell] && Grid.IsSolidCell(cell);
	}

	// Token: 0x060036F5 RID: 14069 RVA: 0x001320A9 File Offset: 0x001302A9
	public static bool VisibilityTest(int x, int y, int x2, int y2, bool blocking_tile_visible = false)
	{
		return Grid.TestLineOfSight(x, y, x2, y2, Grid.VisibleBlockingDelegate, blocking_tile_visible);
	}

	// Token: 0x060036F6 RID: 14070 RVA: 0x001320BC File Offset: 0x001302BC
	public static bool VisibilityTest(int cell, int target_cell, bool blocking_tile_visible = false)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = 0;
		int num4 = 0;
		Grid.CellToXY(target_cell, out num3, out num4);
		return Grid.VisibilityTest(num, num2, num3, num4, blocking_tile_visible);
	}

	// Token: 0x060036F7 RID: 14071 RVA: 0x001320EF File Offset: 0x001302EF
	public static bool PhysicalBlockingCB(int cell)
	{
		return Grid.Solid[cell];
	}

	// Token: 0x060036F8 RID: 14072 RVA: 0x001320FC File Offset: 0x001302FC
	public static bool IsPhysicallyAccessible(int x, int y, int x2, int y2, bool blocking_tile_visible = false)
	{
		return Grid.TestLineOfSight(x, y, x2, y2, Grid.PhysicalBlockingDelegate, blocking_tile_visible);
	}

	// Token: 0x060036F9 RID: 14073 RVA: 0x00132110 File Offset: 0x00130310
	public static void CollectCellsInLine(int startCell, int endCell, HashSet<int> outputCells)
	{
		int num = 2;
		int cellDistance = Grid.GetCellDistance(startCell, endCell);
		Vector2 vector = (Grid.CellToPos(endCell) - Grid.CellToPos(startCell)).normalized;
		for (float num2 = 0f; num2 < (float)cellDistance; num2 = Mathf.Min(num2 + 1f / (float)num, (float)cellDistance))
		{
			int num3 = Grid.PosToCell(Grid.CellToPos(startCell) + vector * num2);
			if (Grid.GetCellDistance(startCell, num3) <= cellDistance)
			{
				outputCells.Add(num3);
			}
		}
	}

	// Token: 0x060036FA RID: 14074 RVA: 0x0013219C File Offset: 0x0013039C
	public static bool IsRangeExposedToSunlight(int cell, int scanRadius, CellOffset scanShape, out int cellsClear, int clearThreshold = 1)
	{
		cellsClear = 0;
		if (Grid.IsValidCell(cell) && (int)Grid.ExposedToSunlight[cell] >= clearThreshold)
		{
			cellsClear++;
		}
		bool flag = true;
		bool flag2 = true;
		int num = 1;
		while (num <= scanRadius && (flag || flag2))
		{
			int num2 = Grid.OffsetCell(cell, scanShape.x * num, scanShape.y * num);
			int num3 = Grid.OffsetCell(cell, -scanShape.x * num, scanShape.y * num);
			if (Grid.IsValidCell(num2) && (int)Grid.ExposedToSunlight[num2] >= clearThreshold)
			{
				cellsClear++;
			}
			if (Grid.IsValidCell(num3) && (int)Grid.ExposedToSunlight[num3] >= clearThreshold)
			{
				cellsClear++;
			}
			num++;
		}
		return cellsClear > 0;
	}

	// Token: 0x060036FB RID: 14075 RVA: 0x00132250 File Offset: 0x00130450
	public static bool TestLineOfSight(int x, int y, int x2, int y2, Func<int, bool> blocking_cb, bool blocking_tile_visible = false)
	{
		int num = x;
		int num2 = y;
		int num3 = x2 - x;
		int num4 = y2 - y;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (num3 < 0)
		{
			num5 = -1;
		}
		else if (num3 > 0)
		{
			num5 = 1;
		}
		if (num4 < 0)
		{
			num6 = -1;
		}
		else if (num4 > 0)
		{
			num6 = 1;
		}
		if (num3 < 0)
		{
			num7 = -1;
		}
		else if (num3 > 0)
		{
			num7 = 1;
		}
		int num9 = Math.Abs(num3);
		int num10 = Math.Abs(num4);
		if (num9 <= num10)
		{
			num9 = Math.Abs(num4);
			num10 = Math.Abs(num3);
			if (num4 < 0)
			{
				num8 = -1;
			}
			else if (num4 > 0)
			{
				num8 = 1;
			}
			num7 = 0;
		}
		int num11 = num9 >> 1;
		for (int i = 0; i <= num9; i++)
		{
			int num12 = Grid.XYToCell(x, y);
			if (!Grid.IsValidCell(num12))
			{
				return false;
			}
			bool flag = blocking_cb(num12);
			if ((x != num || y != num2) && flag)
			{
				return blocking_tile_visible && x == x2 && y == y2;
			}
			num11 += num10;
			if (num11 >= num9)
			{
				num11 -= num9;
				x += num5;
				y += num6;
			}
			else
			{
				x += num7;
				y += num8;
			}
		}
		return true;
	}

	// Token: 0x060036FC RID: 14076 RVA: 0x00132368 File Offset: 0x00130568
	public static bool GetFreeGridSpace(Vector2I size, out Vector2I offset)
	{
		Vector2I gridOffset = BestFit.GetGridOffset(ClusterManager.Instance.WorldContainers, size, out offset);
		if (gridOffset.X <= Grid.WidthInCells && gridOffset.Y <= Grid.HeightInCells)
		{
			SimMessages.SimDataResizeGridAndInitializeVacuumCells(gridOffset, size.x, size.y, offset.x, offset.y);
			Game.Instance.roomProber.Refresh();
			return true;
		}
		return false;
	}

	// Token: 0x060036FD RID: 14077 RVA: 0x001323D4 File Offset: 0x001305D4
	public static void FreeGridSpace(Vector2I size, Vector2I offset)
	{
		SimMessages.SimDataFreeCells(size.x, size.y, offset.x, offset.y);
		for (int i = offset.y; i < size.y + offset.y + 1; i++)
		{
			for (int j = offset.x - 1; j < size.x + offset.x + 1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (Grid.IsValidCell(num))
				{
					Grid.Element[num] = ElementLoader.FindElementByHash(SimHashes.Vacuum);
				}
			}
		}
		Game.Instance.roomProber.Refresh();
	}

	// Token: 0x060036FE RID: 14078 RVA: 0x0013246E File Offset: 0x0013066E
	[Conditional("UNITY_EDITOR")]
	public static void DrawBoxOnCell(int cell, Color color, float offset = 0f)
	{
		Grid.CellToPos(cell) + new Vector3(0.5f, 0.5f, 0f);
	}

	// Token: 0x040024A0 RID: 9376
	public static readonly CellOffset[] DefaultOffset = new CellOffset[1];

	// Token: 0x040024A1 RID: 9377
	public static float WidthInMeters;

	// Token: 0x040024A2 RID: 9378
	public static float HeightInMeters;

	// Token: 0x040024A3 RID: 9379
	public static int WidthInCells;

	// Token: 0x040024A4 RID: 9380
	public static int HeightInCells;

	// Token: 0x040024A5 RID: 9381
	public static float CellSizeInMeters;

	// Token: 0x040024A6 RID: 9382
	public static float InverseCellSizeInMeters;

	// Token: 0x040024A7 RID: 9383
	public static float HalfCellSizeInMeters;

	// Token: 0x040024A8 RID: 9384
	public static int CellCount;

	// Token: 0x040024A9 RID: 9385
	public static int InvalidCell = -1;

	// Token: 0x040024AA RID: 9386
	public static int TopBorderHeight = 2;

	// Token: 0x040024AB RID: 9387
	public static Dictionary<int, GameObject>[] ObjectLayers;

	// Token: 0x040024AC RID: 9388
	public static Action<int> OnReveal;

	// Token: 0x040024AD RID: 9389
	public static Grid.BuildFlags[] BuildMasks;

	// Token: 0x040024AE RID: 9390
	public static Grid.BuildFlagsFoundationIndexer Foundation;

	// Token: 0x040024AF RID: 9391
	public static Grid.BuildFlagsSolidIndexer Solid;

	// Token: 0x040024B0 RID: 9392
	public static Grid.BuildFlagsDupeImpassableIndexer DupeImpassable;

	// Token: 0x040024B1 RID: 9393
	public static Grid.BuildFlagsFakeFloorIndexer FakeFloor;

	// Token: 0x040024B2 RID: 9394
	public static Grid.BuildFlagsDupePassableIndexer DupePassable;

	// Token: 0x040024B3 RID: 9395
	public static Grid.BuildFlagsImpassableIndexer CritterImpassable;

	// Token: 0x040024B4 RID: 9396
	public static Grid.BuildFlagsDoorIndexer HasDoor;

	// Token: 0x040024B5 RID: 9397
	public static Grid.VisFlags[] VisMasks;

	// Token: 0x040024B6 RID: 9398
	public static Grid.VisFlagsRevealedIndexer Revealed;

	// Token: 0x040024B7 RID: 9399
	public static Grid.VisFlagsPreventFogOfWarRevealIndexer PreventFogOfWarReveal;

	// Token: 0x040024B8 RID: 9400
	public static Grid.VisFlagsRenderedByWorldIndexer RenderedByWorld;

	// Token: 0x040024B9 RID: 9401
	public static Grid.VisFlagsAllowPathfindingIndexer AllowPathfinding;

	// Token: 0x040024BA RID: 9402
	public static Grid.NavValidatorFlags[] NavValidatorMasks;

	// Token: 0x040024BB RID: 9403
	public static Grid.NavValidatorFlagsLadderIndexer HasLadder;

	// Token: 0x040024BC RID: 9404
	public static Grid.NavValidatorFlagsPoleIndexer HasPole;

	// Token: 0x040024BD RID: 9405
	public static Grid.NavValidatorFlagsTubeIndexer HasTube;

	// Token: 0x040024BE RID: 9406
	public static Grid.NavValidatorFlagsNavTeleporterIndexer HasNavTeleporter;

	// Token: 0x040024BF RID: 9407
	public static Grid.NavValidatorFlagsUnderConstructionIndexer IsTileUnderConstruction;

	// Token: 0x040024C0 RID: 9408
	public static Grid.NavFlags[] NavMasks;

	// Token: 0x040024C1 RID: 9409
	private static Grid.NavFlagsAccessDoorIndexer HasAccessDoor;

	// Token: 0x040024C2 RID: 9410
	public static Grid.NavFlagsTubeEntranceIndexer HasTubeEntrance;

	// Token: 0x040024C3 RID: 9411
	public static Grid.NavFlagsPreventIdleTraversalIndexer PreventIdleTraversal;

	// Token: 0x040024C4 RID: 9412
	public static Grid.NavFlagsReservedIndexer Reserved;

	// Token: 0x040024C5 RID: 9413
	public static Grid.NavFlagsSuitMarkerIndexer HasSuitMarker;

	// Token: 0x040024C6 RID: 9414
	private static Dictionary<int, Grid.Restriction> restrictions = new Dictionary<int, Grid.Restriction>();

	// Token: 0x040024C7 RID: 9415
	private static Dictionary<int, Grid.TubeEntrance> tubeEntrances = new Dictionary<int, Grid.TubeEntrance>();

	// Token: 0x040024C8 RID: 9416
	private static Dictionary<int, Grid.SuitMarker> suitMarkers = new Dictionary<int, Grid.SuitMarker>();

	// Token: 0x040024C9 RID: 9417
	public unsafe static ushort* elementIdx;

	// Token: 0x040024CA RID: 9418
	public unsafe static float* temperature;

	// Token: 0x040024CB RID: 9419
	public unsafe static float* radiation;

	// Token: 0x040024CC RID: 9420
	public unsafe static float* mass;

	// Token: 0x040024CD RID: 9421
	public unsafe static byte* properties;

	// Token: 0x040024CE RID: 9422
	public unsafe static byte* strengthInfo;

	// Token: 0x040024CF RID: 9423
	public unsafe static byte* insulation;

	// Token: 0x040024D0 RID: 9424
	public unsafe static byte* diseaseIdx;

	// Token: 0x040024D1 RID: 9425
	public unsafe static int* diseaseCount;

	// Token: 0x040024D2 RID: 9426
	public unsafe static byte* exposedToSunlight;

	// Token: 0x040024D3 RID: 9427
	public unsafe static float* AccumulatedFlowValues = null;

	// Token: 0x040024D4 RID: 9428
	public static byte[] Visible;

	// Token: 0x040024D5 RID: 9429
	public static byte[] Spawnable;

	// Token: 0x040024D6 RID: 9430
	public static float[] Damage;

	// Token: 0x040024D7 RID: 9431
	public static float[] Decor;

	// Token: 0x040024D8 RID: 9432
	public static bool[] GravitasFacility;

	// Token: 0x040024D9 RID: 9433
	public static byte[] WorldIdx;

	// Token: 0x040024DA RID: 9434
	public static float[] Loudness;

	// Token: 0x040024DB RID: 9435
	public static Element[] Element;

	// Token: 0x040024DC RID: 9436
	public static int[] LightCount;

	// Token: 0x040024DD RID: 9437
	public static Grid.PressureIndexer Pressure;

	// Token: 0x040024DE RID: 9438
	public static Grid.TransparentIndexer Transparent;

	// Token: 0x040024DF RID: 9439
	public static Grid.ElementIdxIndexer ElementIdx;

	// Token: 0x040024E0 RID: 9440
	public static Grid.TemperatureIndexer Temperature;

	// Token: 0x040024E1 RID: 9441
	public static Grid.RadiationIndexer Radiation;

	// Token: 0x040024E2 RID: 9442
	public static Grid.MassIndexer Mass;

	// Token: 0x040024E3 RID: 9443
	public static Grid.PropertiesIndexer Properties;

	// Token: 0x040024E4 RID: 9444
	public static Grid.ExposedToSunlightIndexer ExposedToSunlight;

	// Token: 0x040024E5 RID: 9445
	public static Grid.StrengthInfoIndexer StrengthInfo;

	// Token: 0x040024E6 RID: 9446
	public static Grid.Insulationndexer Insulation;

	// Token: 0x040024E7 RID: 9447
	public static Grid.DiseaseIdxIndexer DiseaseIdx;

	// Token: 0x040024E8 RID: 9448
	public static Grid.DiseaseCountIndexer DiseaseCount;

	// Token: 0x040024E9 RID: 9449
	public static Grid.LightIntensityIndexer LightIntensity;

	// Token: 0x040024EA RID: 9450
	public static Grid.AccumulatedFlowIndexer AccumulatedFlow;

	// Token: 0x040024EB RID: 9451
	public static Grid.ObjectLayerIndexer Objects;

	// Token: 0x040024EC RID: 9452
	public static float LayerMultiplier = 1f;

	// Token: 0x040024ED RID: 9453
	private static readonly Func<int, bool> VisibleBlockingDelegate = (int cell) => Grid.VisibleBlockingCB(cell);

	// Token: 0x040024EE RID: 9454
	private static readonly Func<int, bool> PhysicalBlockingDelegate = (int cell) => Grid.PhysicalBlockingCB(cell);

	// Token: 0x020014D6 RID: 5334
	[Flags]
	public enum BuildFlags : byte
	{
		// Token: 0x04006504 RID: 25860
		Solid = 1,
		// Token: 0x04006505 RID: 25861
		Foundation = 2,
		// Token: 0x04006506 RID: 25862
		Door = 4,
		// Token: 0x04006507 RID: 25863
		DupePassable = 8,
		// Token: 0x04006508 RID: 25864
		DupeImpassable = 16,
		// Token: 0x04006509 RID: 25865
		CritterImpassable = 32,
		// Token: 0x0400650A RID: 25866
		FakeFloor = 192,
		// Token: 0x0400650B RID: 25867
		Any = 255
	}

	// Token: 0x020014D7 RID: 5335
	public struct BuildFlagsFoundationIndexer
	{
		// Token: 0x17000896 RID: 2198
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.Foundation) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.Foundation, value);
			}
		}
	}

	// Token: 0x020014D8 RID: 5336
	public struct BuildFlagsSolidIndexer
	{
		// Token: 0x17000897 RID: 2199
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.Solid) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
		}
	}

	// Token: 0x020014D9 RID: 5337
	public struct BuildFlagsDupeImpassableIndexer
	{
		// Token: 0x17000898 RID: 2200
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.DupeImpassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.DupeImpassable, value);
			}
		}
	}

	// Token: 0x020014DA RID: 5338
	public struct BuildFlagsFakeFloorIndexer
	{
		// Token: 0x17000899 RID: 2201
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.FakeFloor) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x002E44C4 File Offset: 0x002E26C4
		public void Add(int i)
		{
			Grid.BuildFlags buildFlags = Grid.BuildMasks[i];
			int num = (int)(((buildFlags & Grid.BuildFlags.FakeFloor) >> 6) + 1);
			num = Math.Min(num, 3);
			Grid.BuildMasks[i] = (buildFlags & ~Grid.BuildFlags.FakeFloor) | ((Grid.BuildFlags)(num << 6) & Grid.BuildFlags.FakeFloor);
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x002E4504 File Offset: 0x002E2704
		public void Remove(int i)
		{
			Grid.BuildFlags buildFlags = Grid.BuildMasks[i];
			int num = (int)(((buildFlags & Grid.BuildFlags.FakeFloor) >> 6) - Grid.BuildFlags.Solid);
			num = Math.Max(num, 0);
			Grid.BuildMasks[i] = (buildFlags & ~Grid.BuildFlags.FakeFloor) | ((Grid.BuildFlags)(num << 6) & Grid.BuildFlags.FakeFloor);
		}
	}

	// Token: 0x020014DB RID: 5339
	public struct BuildFlagsDupePassableIndexer
	{
		// Token: 0x1700089A RID: 2202
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.DupePassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.DupePassable, value);
			}
		}
	}

	// Token: 0x020014DC RID: 5340
	public struct BuildFlagsImpassableIndexer
	{
		// Token: 0x1700089B RID: 2203
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.CritterImpassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.CritterImpassable, value);
			}
		}
	}

	// Token: 0x020014DD RID: 5341
	public struct BuildFlagsDoorIndexer
	{
		// Token: 0x1700089C RID: 2204
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.Door) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.Door, value);
			}
		}
	}

	// Token: 0x020014DE RID: 5342
	[Flags]
	public enum VisFlags : byte
	{
		// Token: 0x0400650D RID: 25869
		Revealed = 1,
		// Token: 0x0400650E RID: 25870
		PreventFogOfWarReveal = 2,
		// Token: 0x0400650F RID: 25871
		RenderedByWorld = 4,
		// Token: 0x04006510 RID: 25872
		AllowPathfinding = 8
	}

	// Token: 0x020014DF RID: 5343
	public struct VisFlagsRevealedIndexer
	{
		// Token: 0x1700089D RID: 2205
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.Revealed) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.Revealed, value);
			}
		}
	}

	// Token: 0x020014E0 RID: 5344
	public struct VisFlagsPreventFogOfWarRevealIndexer
	{
		// Token: 0x1700089E RID: 2206
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.PreventFogOfWarReveal) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.PreventFogOfWarReveal, value);
			}
		}
	}

	// Token: 0x020014E1 RID: 5345
	public struct VisFlagsRenderedByWorldIndexer
	{
		// Token: 0x1700089F RID: 2207
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.RenderedByWorld) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.RenderedByWorld, value);
			}
		}
	}

	// Token: 0x020014E2 RID: 5346
	public struct VisFlagsAllowPathfindingIndexer
	{
		// Token: 0x170008A0 RID: 2208
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.AllowPathfinding) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.AllowPathfinding, value);
			}
		}
	}

	// Token: 0x020014E3 RID: 5347
	[Flags]
	public enum NavValidatorFlags : byte
	{
		// Token: 0x04006512 RID: 25874
		Ladder = 1,
		// Token: 0x04006513 RID: 25875
		Pole = 2,
		// Token: 0x04006514 RID: 25876
		Tube = 4,
		// Token: 0x04006515 RID: 25877
		NavTeleporter = 8,
		// Token: 0x04006516 RID: 25878
		UnderConstruction = 16
	}

	// Token: 0x020014E4 RID: 5348
	public struct NavValidatorFlagsLadderIndexer
	{
		// Token: 0x170008A1 RID: 2209
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.Ladder) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.Ladder, value);
			}
		}
	}

	// Token: 0x020014E5 RID: 5349
	public struct NavValidatorFlagsPoleIndexer
	{
		// Token: 0x170008A2 RID: 2210
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.Pole) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.Pole, value);
			}
		}
	}

	// Token: 0x020014E6 RID: 5350
	public struct NavValidatorFlagsTubeIndexer
	{
		// Token: 0x170008A3 RID: 2211
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.Tube) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.Tube, value);
			}
		}
	}

	// Token: 0x020014E7 RID: 5351
	public struct NavValidatorFlagsNavTeleporterIndexer
	{
		// Token: 0x170008A4 RID: 2212
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.NavTeleporter) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.NavTeleporter, value);
			}
		}
	}

	// Token: 0x020014E8 RID: 5352
	public struct NavValidatorFlagsUnderConstructionIndexer
	{
		// Token: 0x170008A5 RID: 2213
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.UnderConstruction) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.UnderConstruction, value);
			}
		}
	}

	// Token: 0x020014E9 RID: 5353
	[Flags]
	public enum NavFlags : byte
	{
		// Token: 0x04006518 RID: 25880
		AccessDoor = 1,
		// Token: 0x04006519 RID: 25881
		TubeEntrance = 2,
		// Token: 0x0400651A RID: 25882
		PreventIdleTraversal = 4,
		// Token: 0x0400651B RID: 25883
		Reserved = 8,
		// Token: 0x0400651C RID: 25884
		SuitMarker = 16
	}

	// Token: 0x020014EA RID: 5354
	public struct NavFlagsAccessDoorIndexer
	{
		// Token: 0x170008A6 RID: 2214
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.AccessDoor) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.AccessDoor, value);
			}
		}
	}

	// Token: 0x020014EB RID: 5355
	public struct NavFlagsTubeEntranceIndexer
	{
		// Token: 0x170008A7 RID: 2215
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.TubeEntrance) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.TubeEntrance, value);
			}
		}
	}

	// Token: 0x020014EC RID: 5356
	public struct NavFlagsPreventIdleTraversalIndexer
	{
		// Token: 0x170008A8 RID: 2216
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.PreventIdleTraversal) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.PreventIdleTraversal, value);
			}
		}
	}

	// Token: 0x020014ED RID: 5357
	public struct NavFlagsReservedIndexer
	{
		// Token: 0x170008A9 RID: 2217
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.Reserved) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.Reserved, value);
			}
		}
	}

	// Token: 0x020014EE RID: 5358
	public struct NavFlagsSuitMarkerIndexer
	{
		// Token: 0x170008AA RID: 2218
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.SuitMarker) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.SuitMarker, value);
			}
		}
	}

	// Token: 0x020014EF RID: 5359
	public struct Restriction
	{
		// Token: 0x0400651D RID: 25885
		public const int DefaultID = -1;

		// Token: 0x0400651E RID: 25886
		public Dictionary<int, Grid.Restriction.Directions> DirectionMasksForMinionInstanceID;

		// Token: 0x0400651F RID: 25887
		public Grid.Restriction.Orientation orientation;

		// Token: 0x02002062 RID: 8290
		[Flags]
		public enum Directions : byte
		{
			// Token: 0x0400904C RID: 36940
			Left = 1,
			// Token: 0x0400904D RID: 36941
			Right = 2,
			// Token: 0x0400904E RID: 36942
			Teleport = 4
		}

		// Token: 0x02002063 RID: 8291
		public enum Orientation : byte
		{
			// Token: 0x04009050 RID: 36944
			Vertical,
			// Token: 0x04009051 RID: 36945
			Horizontal,
			// Token: 0x04009052 RID: 36946
			SingleCell
		}
	}

	// Token: 0x020014F0 RID: 5360
	private struct TubeEntrance
	{
		// Token: 0x04006520 RID: 25888
		public bool operational;

		// Token: 0x04006521 RID: 25889
		public int reservationCapacity;

		// Token: 0x04006522 RID: 25890
		public HashSet<int> reservedInstanceIDs;
	}

	// Token: 0x020014F1 RID: 5361
	public struct SuitMarker
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06008246 RID: 33350 RVA: 0x002E46E1 File Offset: 0x002E28E1
		public int emptyLockerCount
		{
			get
			{
				return this.lockerCount - this.suitCount;
			}
		}

		// Token: 0x04006523 RID: 25891
		public int suitCount;

		// Token: 0x04006524 RID: 25892
		public int lockerCount;

		// Token: 0x04006525 RID: 25893
		public Grid.SuitMarker.Flags flags;

		// Token: 0x04006526 RID: 25894
		public PathFinder.PotentialPath.Flags pathFlags;

		// Token: 0x04006527 RID: 25895
		public HashSet<int> minionIDsWithSuitReservations;

		// Token: 0x04006528 RID: 25896
		public HashSet<int> minionIDsWithEmptyLockerReservations;

		// Token: 0x02002064 RID: 8292
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x04009054 RID: 36948
			OnlyTraverseIfUnequipAvailable = 1,
			// Token: 0x04009055 RID: 36949
			Operational = 2,
			// Token: 0x04009056 RID: 36950
			Rotated = 4
		}
	}

	// Token: 0x020014F2 RID: 5362
	public struct ObjectLayerIndexer
	{
		// Token: 0x170008AC RID: 2220
		public GameObject this[int cell, int layer]
		{
			get
			{
				GameObject gameObject = null;
				Grid.ObjectLayers[layer].TryGetValue(cell, out gameObject);
				return gameObject;
			}
			set
			{
				if (value == null)
				{
					Grid.ObjectLayers[layer].Remove(cell);
				}
				else
				{
					Grid.ObjectLayers[layer][cell] = value;
				}
				GameScenePartitioner.Instance.TriggerEvent(cell, GameScenePartitioner.Instance.objectLayers[layer], value);
			}
		}
	}

	// Token: 0x020014F3 RID: 5363
	public struct PressureIndexer
	{
		// Token: 0x170008AD RID: 2221
		public unsafe float this[int i]
		{
			get
			{
				return Grid.mass[i] * 101.3f;
			}
		}
	}

	// Token: 0x020014F4 RID: 5364
	public struct TransparentIndexer
	{
		// Token: 0x170008AE RID: 2222
		public unsafe bool this[int i]
		{
			get
			{
				return (Grid.properties[i] & 16) > 0;
			}
		}
	}

	// Token: 0x020014F5 RID: 5365
	public struct ElementIdxIndexer
	{
		// Token: 0x170008AF RID: 2223
		public unsafe ushort this[int i]
		{
			get
			{
				return Grid.elementIdx[i];
			}
		}
	}

	// Token: 0x020014F6 RID: 5366
	public struct TemperatureIndexer
	{
		// Token: 0x170008B0 RID: 2224
		public unsafe float this[int i]
		{
			get
			{
				return Grid.temperature[i];
			}
		}
	}

	// Token: 0x020014F7 RID: 5367
	public struct RadiationIndexer
	{
		// Token: 0x170008B1 RID: 2225
		public unsafe float this[int i]
		{
			get
			{
				return Grid.radiation[i];
			}
		}
	}

	// Token: 0x020014F8 RID: 5368
	public struct MassIndexer
	{
		// Token: 0x170008B2 RID: 2226
		public unsafe float this[int i]
		{
			get
			{
				return Grid.mass[i];
			}
		}
	}

	// Token: 0x020014F9 RID: 5369
	public struct PropertiesIndexer
	{
		// Token: 0x170008B3 RID: 2227
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.properties[i];
			}
		}
	}

	// Token: 0x020014FA RID: 5370
	public struct ExposedToSunlightIndexer
	{
		// Token: 0x170008B4 RID: 2228
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.exposedToSunlight[i];
			}
		}
	}

	// Token: 0x020014FB RID: 5371
	public struct StrengthInfoIndexer
	{
		// Token: 0x170008B5 RID: 2229
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.strengthInfo[i];
			}
		}
	}

	// Token: 0x020014FC RID: 5372
	public struct Insulationndexer
	{
		// Token: 0x170008B6 RID: 2230
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.insulation[i];
			}
		}
	}

	// Token: 0x020014FD RID: 5373
	public struct DiseaseIdxIndexer
	{
		// Token: 0x170008B7 RID: 2231
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.diseaseIdx[i];
			}
		}
	}

	// Token: 0x020014FE RID: 5374
	public struct DiseaseCountIndexer
	{
		// Token: 0x170008B8 RID: 2232
		public unsafe int this[int i]
		{
			get
			{
				return Grid.diseaseCount[i];
			}
		}
	}

	// Token: 0x020014FF RID: 5375
	public struct AccumulatedFlowIndexer
	{
		// Token: 0x170008B9 RID: 2233
		public unsafe float this[int i]
		{
			get
			{
				return Grid.AccumulatedFlowValues[i];
			}
		}
	}

	// Token: 0x02001500 RID: 5376
	public struct LightIntensityIndexer
	{
		// Token: 0x170008BA RID: 2234
		public unsafe int this[int i]
		{
			get
			{
				float num = Game.Instance.currentFallbackSunlightIntensity;
				WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[i]);
				if (world != null)
				{
					num = world.currentSunlightIntensity;
				}
				int num2 = (int)((float)Grid.exposedToSunlight[i] / 255f * num);
				int num3 = Grid.LightCount[i];
				return num2 + num3;
			}
		}
	}

	// Token: 0x02001501 RID: 5377
	public enum SceneLayer
	{
		// Token: 0x0400652A RID: 25898
		WorldSelection = -3,
		// Token: 0x0400652B RID: 25899
		NoLayer,
		// Token: 0x0400652C RID: 25900
		Background,
		// Token: 0x0400652D RID: 25901
		Backwall = 1,
		// Token: 0x0400652E RID: 25902
		Gas,
		// Token: 0x0400652F RID: 25903
		GasConduits,
		// Token: 0x04006530 RID: 25904
		GasConduitBridges,
		// Token: 0x04006531 RID: 25905
		LiquidConduits,
		// Token: 0x04006532 RID: 25906
		LiquidConduitBridges,
		// Token: 0x04006533 RID: 25907
		SolidConduits,
		// Token: 0x04006534 RID: 25908
		SolidConduitContents,
		// Token: 0x04006535 RID: 25909
		SolidConduitBridges,
		// Token: 0x04006536 RID: 25910
		Wires,
		// Token: 0x04006537 RID: 25911
		WireBridges,
		// Token: 0x04006538 RID: 25912
		WireBridgesFront,
		// Token: 0x04006539 RID: 25913
		LogicWires,
		// Token: 0x0400653A RID: 25914
		LogicGates,
		// Token: 0x0400653B RID: 25915
		LogicGatesFront,
		// Token: 0x0400653C RID: 25916
		InteriorWall,
		// Token: 0x0400653D RID: 25917
		GasFront,
		// Token: 0x0400653E RID: 25918
		BuildingBack,
		// Token: 0x0400653F RID: 25919
		Building,
		// Token: 0x04006540 RID: 25920
		BuildingUse,
		// Token: 0x04006541 RID: 25921
		BuildingFront,
		// Token: 0x04006542 RID: 25922
		TransferArm,
		// Token: 0x04006543 RID: 25923
		Ore,
		// Token: 0x04006544 RID: 25924
		Creatures,
		// Token: 0x04006545 RID: 25925
		Move,
		// Token: 0x04006546 RID: 25926
		Front,
		// Token: 0x04006547 RID: 25927
		GlassTile,
		// Token: 0x04006548 RID: 25928
		Liquid,
		// Token: 0x04006549 RID: 25929
		Ground,
		// Token: 0x0400654A RID: 25930
		TileMain,
		// Token: 0x0400654B RID: 25931
		TileFront,
		// Token: 0x0400654C RID: 25932
		FXFront,
		// Token: 0x0400654D RID: 25933
		FXFront2,
		// Token: 0x0400654E RID: 25934
		SceneMAX
	}
}
