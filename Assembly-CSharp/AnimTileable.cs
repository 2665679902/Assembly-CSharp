using System;
using UnityEngine;

// Token: 0x02000547 RID: 1351
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/AnimTileable")]
public class AnimTileable : KMonoBehaviour
{
	// Token: 0x06002031 RID: 8241 RVA: 0x000AFBD4 File Offset: 0x000ADDD4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.tags == null || this.tags.Length == 0)
		{
			this.tags = new Tag[] { base.GetComponent<KPrefabID>().PrefabTag };
		}
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x000AFC0C File Offset: 0x000ADE0C
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		else
		{
			Building component2 = base.GetComponent<Building>();
			this.extents = component2.GetExtents();
		}
		Extents extents = new Extents(this.extents.x - 1, this.extents.y - 1, this.extents.width + 2, this.extents.height + 2);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("AnimTileable.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
		this.UpdateEndCaps();
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x000AFCC4 File Offset: 0x000ADEC4
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x000AFCDC File Offset: 0x000ADEDC
	private void UpdateEndCaps()
	{
		int num = Grid.PosToCell(this);
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		bool flag4 = true;
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset rotatedCellOffset = new CellOffset(this.extents.x - num2 - 1, 0);
		CellOffset rotatedCellOffset2 = new CellOffset(this.extents.x - num2 + this.extents.width, 0);
		CellOffset rotatedCellOffset3 = new CellOffset(0, this.extents.y - num3 + this.extents.height);
		CellOffset rotatedCellOffset4 = new CellOffset(0, this.extents.y - num3 - 1);
		Rotatable component = base.GetComponent<Rotatable>();
		if (component)
		{
			rotatedCellOffset = component.GetRotatedCellOffset(rotatedCellOffset);
			rotatedCellOffset2 = component.GetRotatedCellOffset(rotatedCellOffset2);
			rotatedCellOffset3 = component.GetRotatedCellOffset(rotatedCellOffset3);
			rotatedCellOffset4 = component.GetRotatedCellOffset(rotatedCellOffset4);
		}
		int num4 = Grid.OffsetCell(num, rotatedCellOffset);
		int num5 = Grid.OffsetCell(num, rotatedCellOffset2);
		int num6 = Grid.OffsetCell(num, rotatedCellOffset3);
		int num7 = Grid.OffsetCell(num, rotatedCellOffset4);
		if (Grid.IsValidCell(num4))
		{
			flag = !this.HasTileableNeighbour(num4);
		}
		if (Grid.IsValidCell(num5))
		{
			flag2 = !this.HasTileableNeighbour(num5);
		}
		if (Grid.IsValidCell(num6))
		{
			flag3 = !this.HasTileableNeighbour(num6);
		}
		if (Grid.IsValidCell(num7))
		{
			flag4 = !this.HasTileableNeighbour(num7);
		}
		foreach (KBatchedAnimController kbatchedAnimController in base.GetComponentsInChildren<KBatchedAnimController>())
		{
			foreach (KAnimHashedString kanimHashedString in AnimTileable.leftSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString, flag);
			}
			foreach (KAnimHashedString kanimHashedString2 in AnimTileable.rightSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString2, flag2);
			}
			foreach (KAnimHashedString kanimHashedString3 in AnimTileable.topSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString3, flag3);
			}
			foreach (KAnimHashedString kanimHashedString4 in AnimTileable.bottomSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString4, flag4);
			}
		}
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x000AFF14 File Offset: 0x000AE114
	private bool HasTileableNeighbour(int neighbour_cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && component.HasAnyTags(this.tags))
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x000AFF5F File Offset: 0x000AE15F
	private void OnNeighbourCellsUpdated(object data)
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		if (this.partitionerEntry.IsValid())
		{
			this.UpdateEndCaps();
		}
	}

	// Token: 0x04001279 RID: 4729
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400127A RID: 4730
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x0400127B RID: 4731
	public Tag[] tags;

	// Token: 0x0400127C RID: 4732
	private Extents extents;

	// Token: 0x0400127D RID: 4733
	private static readonly KAnimHashedString[] leftSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_left"),
		new KAnimHashedString("cap_left_fg"),
		new KAnimHashedString("cap_left_place")
	};

	// Token: 0x0400127E RID: 4734
	private static readonly KAnimHashedString[] rightSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_right"),
		new KAnimHashedString("cap_right_fg"),
		new KAnimHashedString("cap_right_place")
	};

	// Token: 0x0400127F RID: 4735
	private static readonly KAnimHashedString[] topSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_top"),
		new KAnimHashedString("cap_top_fg"),
		new KAnimHashedString("cap_top_place")
	};

	// Token: 0x04001280 RID: 4736
	private static readonly KAnimHashedString[] bottomSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_bottom"),
		new KAnimHashedString("cap_bottom_fg"),
		new KAnimHashedString("cap_bottom_place")
	};
}
