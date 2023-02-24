using System;
using Database;
using UnityEngine;

// Token: 0x02000AE6 RID: 2790
public static class KleiPermitVisUtil
{
	// Token: 0x06005563 RID: 21859 RVA: 0x001EDF24 File Offset: 0x001EC124
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, BuildingFacadeResource buildingPermit)
	{
		KAnimFile anim = Assets.GetAnim(buildingPermit.AnimFile);
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { anim });
		buildingKAnim.Play(KleiPermitVisUtil.GetFirstAnimHash(anim), KAnim.PlayMode.Loop, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06005564 RID: 21860 RVA: 0x001EDF8C File Offset: 0x001EC18C
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, BuildingDef buildingDef)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(buildingDef.AnimFiles);
		buildingKAnim.Play(KleiPermitVisUtil.GetFirstAnimHash(buildingDef.AnimFiles[0]), KAnim.PlayMode.Loop, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06005565 RID: 21861 RVA: 0x001EDFE4 File Offset: 0x001EC1E4
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, ArtableStage artablePermit)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { Assets.GetAnim(artablePermit.animFile) });
		buildingKAnim.Play(artablePermit.anim, KAnim.PlayMode.Once, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06005566 RID: 21862 RVA: 0x001EE04C File Offset: 0x001EC24C
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, DbStickerBomb artablePermit)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { artablePermit.animFile });
		bool flag;
		HashedString hashedString;
		KleiPermitVisUtil.GetDefaultStickerAnimHash(artablePermit.animFile).Deconstruct(out flag, out hashedString);
		bool flag2 = flag;
		HashedString hashedString2 = hashedString;
		if (flag2)
		{
			buildingKAnim.Play(hashedString2, KAnim.PlayMode.Once, 1f, 0f);
		}
		else
		{
			global::Debug.Assert(false, "Couldn't find default sticker for sticker " + artablePermit.Id);
			buildingKAnim.Play(KleiPermitVisUtil.GetFirstAnimHash(artablePermit.animFile), KAnim.PlayMode.Once, 1f, 0f);
		}
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06005567 RID: 21863 RVA: 0x001EE0F0 File Offset: 0x001EC2F0
	public static void ConfigureBuildingPosition(RectTransform transform, PrefabDefinedUIPosition anchorPosition, BuildingDef buildingDef, Alignment alignment)
	{
		anchorPosition.SetOn(transform);
		transform.anchoredPosition += new Vector2(176f * (float)buildingDef.WidthInCells * -(alignment.x - 0.5f), 176f * (float)buildingDef.HeightInCells * -alignment.y);
	}

	// Token: 0x06005568 RID: 21864 RVA: 0x001EE14C File Offset: 0x001EC34C
	public static void ConfigureBuildingPosition(RectTransform transform, Vector2 anchorPosition, BuildingDef buildingDef, Alignment alignment)
	{
		transform.anchoredPosition = anchorPosition + new Vector2(176f * (float)buildingDef.WidthInCells * -(alignment.x - 0.5f), 176f * (float)buildingDef.HeightInCells * -alignment.y);
	}

	// Token: 0x06005569 RID: 21865 RVA: 0x001EE19A File Offset: 0x001EC39A
	public static HashedString GetFirstAnimHash(KAnimFile animFile)
	{
		return animFile.GetData().GetAnim(0).hash;
	}

	// Token: 0x0600556A RID: 21866 RVA: 0x001EE1B0 File Offset: 0x001EC3B0
	public static Option<HashedString> GetDefaultStickerAnimHash(KAnimFile stickerAnimFile)
	{
		KAnimFileData data = stickerAnimFile.GetData();
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim = data.GetAnim(i);
			if (anim.name.StartsWith("idle_sticker"))
			{
				return anim.hash;
			}
		}
		return Option.None;
	}

	// Token: 0x0600556B RID: 21867 RVA: 0x001EE208 File Offset: 0x001EC408
	public static Option<BuildLocationRule> GetBuildLocationRule(PermitResource permit)
	{
		Option<BuildingDef> buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
		if (!buildingDef.HasValue)
		{
			return Option.None;
		}
		return buildingDef.Value.BuildLocationRule;
	}

	// Token: 0x0600556C RID: 21868 RVA: 0x001EE244 File Offset: 0x001EC444
	public static Option<BuildingDef> GetBuildingDef(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = permit as BuildingFacadeResource;
		if (buildingFacadeResource != null)
		{
			BuildingComplete component = Assets.GetPrefab(buildingFacadeResource.PrefabID).GetComponent<BuildingComplete>();
			if (component == null || !component)
			{
				return Option.None;
			}
			return component.Def;
		}
		else
		{
			ArtableStage artableStage = permit as ArtableStage;
			if (artableStage == null)
			{
				return Option.None;
			}
			BuildingComplete component2 = Assets.GetPrefab(artableStage.prefabId).GetComponent<BuildingComplete>();
			if (component2 == null || !component2)
			{
				return Option.None;
			}
			return component2.Def;
		}
	}

	// Token: 0x040039FF RID: 14847
	public const float TILE_SIZE_UI = 176f;
}
