using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005D6 RID: 1494
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Ladder")]
public class Ladder : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x0600254E RID: 9550 RVA: 0x000C99D8 File Offset: 0x000C7BD8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Rotatable component = base.GetComponent<Rotatable>();
		foreach (CellOffset cellOffset in this.offsets)
		{
			CellOffset cellOffset2 = cellOffset;
			if (component != null)
			{
				cellOffset2 = component.GetRotatedCellOffset(cellOffset);
			}
			int num = Grid.OffsetCell(Grid.PosToCell(this), cellOffset2);
			Grid.HasPole[num] = this.isPole;
			Grid.HasLadder[num] = !this.isPole;
		}
		base.GetComponent<KPrefabID>().AddTag(GameTags.Ladders, false);
		Components.Ladders.Add(this);
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x000C9A76 File Offset: 0x000C7C76
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x000C9AAC File Offset: 0x000C7CAC
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Rotatable component = base.GetComponent<Rotatable>();
		foreach (CellOffset cellOffset in this.offsets)
		{
			CellOffset cellOffset2 = cellOffset;
			if (component != null)
			{
				cellOffset2 = component.GetRotatedCellOffset(cellOffset);
			}
			int num = Grid.OffsetCell(Grid.PosToCell(this), cellOffset2);
			if (Grid.Objects[num, 24] == null)
			{
				Grid.HasPole[num] = false;
				Grid.HasLadder[num] = false;
			}
		}
		Components.Ladders.Remove(this);
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x000C9B44 File Offset: 0x000C7D44
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = null;
		if (this.upwardsMovementSpeedMultiplier != 1f)
		{
			list = new List<Descriptor>();
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.DUPLICANTMOVEMENTBOOST, GameUtil.GetFormattedPercent(this.upwardsMovementSpeedMultiplier * 100f - 100f, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DUPLICANTMOVEMENTBOOST, GameUtil.GetFormattedPercent(this.upwardsMovementSpeedMultiplier * 100f - 100f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x040015AC RID: 5548
	public float upwardsMovementSpeedMultiplier = 1f;

	// Token: 0x040015AD RID: 5549
	public float downwardsMovementSpeedMultiplier = 1f;

	// Token: 0x040015AE RID: 5550
	public bool isPole;

	// Token: 0x040015AF RID: 5551
	public CellOffset[] offsets = new CellOffset[] { CellOffset.none };
}
