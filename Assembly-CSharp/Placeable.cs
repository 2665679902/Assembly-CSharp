using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020004B8 RID: 1208
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Placeable")]
public class Placeable : KMonoBehaviour
{
	// Token: 0x06001BD6 RID: 7126 RVA: 0x00093A58 File Offset: 0x00091C58
	public bool IsValidPlaceLocation(int cell, out string reason)
	{
		if (this.placementRules.Contains(Placeable.PlacementRules.RestrictToWorld) && (int)Grid.WorldIdx[cell] != this.restrictWorldId)
		{
			reason = UI.TOOLS.PLACE.REASONS.RESTRICT_TO_WORLD;
			return false;
		}
		if (!this.occupyArea.CanOccupyArea(cell, this.occupyArea.objectLayers[0]))
		{
			reason = UI.TOOLS.PLACE.REASONS.CAN_OCCUPY_AREA;
			return false;
		}
		if (this.placementRules.Contains(Placeable.PlacementRules.OnFoundation) && !this.occupyArea.TestAreaBelow(cell, null, new Func<int, object, bool>(this.FoundationTest)))
		{
			reason = UI.TOOLS.PLACE.REASONS.ON_FOUNDATION;
			return false;
		}
		if (this.placementRules.Contains(Placeable.PlacementRules.VisibleToSpace) && !this.occupyArea.TestArea(cell, null, new Func<int, object, bool>(this.SunnySpaceTest)))
		{
			reason = UI.TOOLS.PLACE.REASONS.VISIBLE_TO_SPACE;
			return false;
		}
		reason = "ok!";
		return true;
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x00093B34 File Offset: 0x00091D34
	private bool SunnySpaceTest(int cell, object data)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = (int)Grid.WorldIdx[cell];
		WorldContainer world = ClusterManager.Instance.GetWorld(num3);
		int num4 = world.WorldOffset.y + world.WorldSize.y;
		return !Grid.Solid[cell] && !Grid.Foundation[cell] && (Grid.ExposedToSunlight[cell] >= 253 || this.ClearPathToSky(num, num2, num4));
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x00093BBC File Offset: 0x00091DBC
	private bool ClearPathToSky(int x, int startY, int top)
	{
		for (int i = startY; i < top; i++)
		{
			int num = Grid.XYToCell(x, i);
			if (Grid.Solid[num] || Grid.Foundation[num])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x00093BFA File Offset: 0x00091DFA
	private bool FoundationTest(int cell, object data)
	{
		return Grid.IsValidBuildingCell(cell) && (Grid.Solid[cell] || Grid.Foundation[cell]);
	}

	// Token: 0x04000F87 RID: 3975
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x04000F88 RID: 3976
	public string kAnimName;

	// Token: 0x04000F89 RID: 3977
	public string animName;

	// Token: 0x04000F8A RID: 3978
	public List<Placeable.PlacementRules> placementRules = new List<Placeable.PlacementRules>();

	// Token: 0x04000F8B RID: 3979
	[NonSerialized]
	public int restrictWorldId;

	// Token: 0x020010F5 RID: 4341
	public enum PlacementRules
	{
		// Token: 0x04005931 RID: 22833
		OnFoundation,
		// Token: 0x04005932 RID: 22834
		VisibleToSpace,
		// Token: 0x04005933 RID: 22835
		RestrictToWorld
	}
}
