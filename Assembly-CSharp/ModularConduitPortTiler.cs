using System;
using UnityEngine;

// Token: 0x02000612 RID: 1554
public class ModularConduitPortTiler : KMonoBehaviour
{
	// Token: 0x0600288E RID: 10382 RVA: 0x000D7200 File Offset: 0x000D5400
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<KPrefabID>().AddTag(GameTags.ModularConduitPort, true);
		if (this.tags == null || this.tags.Length == 0)
		{
			this.tags = new Tag[] { GameTags.ModularConduitPort };
		}
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x000D7250 File Offset: 0x000D5450
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		this.leftCapDefault = new KAnimSynchronizedController(component2, (Grid.SceneLayer)(component2.GetLayer() + this.leftCapDefaultSceneLayerAdjust), ModularConduitPortTiler.leftCapDefaultStr);
		if (this.manageLeftCap)
		{
			this.leftCapLaunchpad = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), ModularConduitPortTiler.leftCapLaunchpadStr);
			this.leftCapConduit = new KAnimSynchronizedController(component2, component2.GetLayer() + Grid.SceneLayer.Backwall, ModularConduitPortTiler.leftCapConduitStr);
		}
		this.rightCapDefault = new KAnimSynchronizedController(component2, (Grid.SceneLayer)(component2.GetLayer() + this.rightCapDefaultSceneLayerAdjust), ModularConduitPortTiler.rightCapDefaultStr);
		if (this.manageRightCap)
		{
			this.rightCapLaunchpad = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), ModularConduitPortTiler.rightCapLaunchpadStr);
			this.rightCapConduit = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), ModularConduitPortTiler.rightCapConduitStr);
		}
		Extents extents = new Extents(this.extents.x - 1, this.extents.y, this.extents.width + 2, this.extents.height);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ModularConduitPort.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
		this.UpdateEndCaps();
		this.CorrectAdjacentLaunchPads();
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x000D73A6 File Offset: 0x000D55A6
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x000D73C0 File Offset: 0x000D55C0
	private void UpdateEndCaps()
	{
		int num;
		int num2;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		int cellLeft = this.GetCellLeft();
		int cellRight = this.GetCellRight();
		if (Grid.IsValidCell(cellLeft))
		{
			if (this.HasTileableNeighbour(cellLeft))
			{
				this.leftCapSetting = ModularConduitPortTiler.AnimCapType.Conduit;
			}
			else if (this.HasLaunchpadNeighbour(cellLeft))
			{
				this.leftCapSetting = ModularConduitPortTiler.AnimCapType.Launchpad;
			}
			else
			{
				this.leftCapSetting = ModularConduitPortTiler.AnimCapType.Default;
			}
		}
		if (Grid.IsValidCell(cellRight))
		{
			if (this.HasTileableNeighbour(cellRight))
			{
				this.rightCapSetting = ModularConduitPortTiler.AnimCapType.Conduit;
			}
			else if (this.HasLaunchpadNeighbour(cellRight))
			{
				this.rightCapSetting = ModularConduitPortTiler.AnimCapType.Launchpad;
			}
			else
			{
				this.rightCapSetting = ModularConduitPortTiler.AnimCapType.Default;
			}
		}
		if (this.manageLeftCap)
		{
			this.leftCapDefault.Enable(this.leftCapSetting == ModularConduitPortTiler.AnimCapType.Default);
			this.leftCapConduit.Enable(this.leftCapSetting == ModularConduitPortTiler.AnimCapType.Conduit);
			this.leftCapLaunchpad.Enable(this.leftCapSetting == ModularConduitPortTiler.AnimCapType.Launchpad);
		}
		if (this.manageRightCap)
		{
			this.rightCapDefault.Enable(this.rightCapSetting == ModularConduitPortTiler.AnimCapType.Default);
			this.rightCapConduit.Enable(this.rightCapSetting == ModularConduitPortTiler.AnimCapType.Conduit);
			this.rightCapLaunchpad.Enable(this.rightCapSetting == ModularConduitPortTiler.AnimCapType.Launchpad);
		}
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x000D74D8 File Offset: 0x000D56D8
	private int GetCellLeft()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(this.extents.x - num2 - 1, 0);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x000D7514 File Offset: 0x000D5714
	private int GetCellRight()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(this.extents.x - num2 + this.extents.width, 0);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x000D7558 File Offset: 0x000D5758
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

	// Token: 0x06002895 RID: 10389 RVA: 0x000D75A4 File Offset: 0x000D57A4
	private bool HasLaunchpadNeighbour(int neighbour_cell)
	{
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		return gameObject != null && gameObject.GetComponent<LaunchPad>() != null;
	}

	// Token: 0x06002896 RID: 10390 RVA: 0x000D75DD File Offset: 0x000D57DD
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

	// Token: 0x06002897 RID: 10391 RVA: 0x000D760C File Offset: 0x000D580C
	private void CorrectAdjacentLaunchPads()
	{
		int cellRight = this.GetCellRight();
		if (Grid.IsValidCell(cellRight) && this.HasLaunchpadNeighbour(cellRight))
		{
			Grid.Objects[cellRight, 1].GetComponent<ModularConduitPortTiler>().UpdateEndCaps();
		}
		int cellLeft = this.GetCellLeft();
		if (Grid.IsValidCell(cellLeft) && this.HasLaunchpadNeighbour(cellLeft))
		{
			Grid.Objects[cellLeft, 1].GetComponent<ModularConduitPortTiler>().UpdateEndCaps();
		}
	}

	// Token: 0x040017CB RID: 6091
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040017CC RID: 6092
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x040017CD RID: 6093
	public Tag[] tags;

	// Token: 0x040017CE RID: 6094
	public bool manageLeftCap = true;

	// Token: 0x040017CF RID: 6095
	public bool manageRightCap = true;

	// Token: 0x040017D0 RID: 6096
	public int leftCapDefaultSceneLayerAdjust;

	// Token: 0x040017D1 RID: 6097
	public int rightCapDefaultSceneLayerAdjust;

	// Token: 0x040017D2 RID: 6098
	private Extents extents;

	// Token: 0x040017D3 RID: 6099
	private ModularConduitPortTiler.AnimCapType leftCapSetting;

	// Token: 0x040017D4 RID: 6100
	private ModularConduitPortTiler.AnimCapType rightCapSetting;

	// Token: 0x040017D5 RID: 6101
	private static readonly string leftCapDefaultStr = "#cap_left_default";

	// Token: 0x040017D6 RID: 6102
	private static readonly string leftCapLaunchpadStr = "#cap_left_launchpad";

	// Token: 0x040017D7 RID: 6103
	private static readonly string leftCapConduitStr = "#cap_left_conduit";

	// Token: 0x040017D8 RID: 6104
	private static readonly string rightCapDefaultStr = "#cap_right_default";

	// Token: 0x040017D9 RID: 6105
	private static readonly string rightCapLaunchpadStr = "#cap_right_launchpad";

	// Token: 0x040017DA RID: 6106
	private static readonly string rightCapConduitStr = "#cap_right_conduit";

	// Token: 0x040017DB RID: 6107
	private KAnimSynchronizedController leftCapDefault;

	// Token: 0x040017DC RID: 6108
	private KAnimSynchronizedController leftCapLaunchpad;

	// Token: 0x040017DD RID: 6109
	private KAnimSynchronizedController leftCapConduit;

	// Token: 0x040017DE RID: 6110
	private KAnimSynchronizedController rightCapDefault;

	// Token: 0x040017DF RID: 6111
	private KAnimSynchronizedController rightCapLaunchpad;

	// Token: 0x040017E0 RID: 6112
	private KAnimSynchronizedController rightCapConduit;

	// Token: 0x02001285 RID: 4741
	private enum AnimCapType
	{
		// Token: 0x04005E11 RID: 24081
		Default,
		// Token: 0x04005E12 RID: 24082
		Conduit,
		// Token: 0x04005E13 RID: 24083
		Launchpad
	}
}
