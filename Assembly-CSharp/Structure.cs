using System;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Structure")]
public class Structure : KMonoBehaviour
{
	// Token: 0x06001D57 RID: 7511 RVA: 0x0009CDE4 File Offset: 0x0009AFE4
	public bool IsEntombed()
	{
		return this.isEntombed;
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x0009CDEC File Offset: 0x0009AFEC
	public static bool IsBuildingEntombed(Building building)
	{
		if (!Grid.IsValidCell(Grid.PosToCell(building)))
		{
			return false;
		}
		for (int i = 0; i < building.PlacementCells.Length; i++)
		{
			int num = building.PlacementCells[i];
			if (Grid.Element[num].IsSolid && !Grid.Foundation[num])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x0009CE44 File Offset: 0x0009B044
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Extents extents = this.building.GetExtents();
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Structure.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.OnSolidChanged(null);
		base.Subscribe<Structure>(-887025858, Structure.RocketLandedDelegate);
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x0009CEAD File Offset: 0x0009B0AD
	public void UpdatePosition()
	{
		GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, this.building.GetExtents());
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x0009CECA File Offset: 0x0009B0CA
	private void RocketChanged(object data)
	{
		this.OnSolidChanged(data);
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x0009CED4 File Offset: 0x0009B0D4
	private void OnSolidChanged(object data)
	{
		bool flag = Structure.IsBuildingEntombed(this.building);
		if (flag != this.isEntombed)
		{
			this.isEntombed = flag;
			if (this.isEntombed)
			{
				base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
			}
			else
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.Entombed);
			}
			this.operational.SetFlag(Structure.notEntombedFlag, !this.isEntombed);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.Entombed, this.isEntombed, this);
			base.Trigger(-1089732772, null);
		}
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x0009CF6F File Offset: 0x0009B16F
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x04001091 RID: 4241
	[MyCmpReq]
	private Building building;

	// Token: 0x04001092 RID: 4242
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04001093 RID: 4243
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001094 RID: 4244
	public static readonly Operational.Flag notEntombedFlag = new Operational.Flag("not_entombed", Operational.Flag.Type.Functional);

	// Token: 0x04001095 RID: 4245
	private bool isEntombed;

	// Token: 0x04001096 RID: 4246
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001097 RID: 4247
	private static EventSystem.IntraObjectHandler<Structure> RocketLandedDelegate = new EventSystem.IntraObjectHandler<Structure>(delegate(Structure cmp, object data)
	{
		cmp.RocketChanged(data);
	});
}
