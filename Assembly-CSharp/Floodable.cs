using System;
using UnityEngine;

// Token: 0x02000481 RID: 1153
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Floodable")]
public class Floodable : KMonoBehaviour
{
	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060019BB RID: 6587 RVA: 0x0008A629 File Offset: 0x00088829
	public bool IsFlooded
	{
		get
		{
			return this.isFlooded;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060019BC RID: 6588 RVA: 0x0008A631 File Offset: 0x00088831
	public BuildingDef Def
	{
		get
		{
			return this.building.Def;
		}
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x0008A640 File Offset: 0x00088840
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Floodable.OnSpawn", base.gameObject, this.building.GetExtents(), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnElementChanged));
		this.OnElementChanged(null);
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x0008A698 File Offset: 0x00088898
	private void OnElementChanged(object data)
	{
		bool flag = false;
		for (int i = 0; i < this.building.PlacementCells.Length; i++)
		{
			if (Grid.IsSubstantialLiquid(this.building.PlacementCells[i], 0.35f))
			{
				flag = true;
				break;
			}
		}
		if (flag != this.isFlooded)
		{
			this.isFlooded = flag;
			this.operational.SetFlag(Floodable.notFloodedFlag, !this.isFlooded);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.Flooded, this.isFlooded, this);
		}
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x0008A727 File Offset: 0x00088927
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x04000E70 RID: 3696
	[MyCmpReq]
	private Building building;

	// Token: 0x04000E71 RID: 3697
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04000E72 RID: 3698
	[MyCmpGet]
	private SimCellOccupier simCellOccupier;

	// Token: 0x04000E73 RID: 3699
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04000E74 RID: 3700
	public static Operational.Flag notFloodedFlag = new Operational.Flag("not_flooded", Operational.Flag.Type.Functional);

	// Token: 0x04000E75 RID: 3701
	private bool isFlooded;

	// Token: 0x04000E76 RID: 3702
	private HandleVector<int>.Handle partitionerEntry;
}
