using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001AE RID: 430
public class GraveConfig : IBuildingConfig
{
	// Token: 0x0600085C RID: 2140 RVA: 0x00031B3C File Offset: 0x0002FD3C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Grave";
		int num = 1;
		int num2 = 2;
		string text2 = "gravestone_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		return buildingDef;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00031BA8 File Offset: 0x0002FDA8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GraveConfig.STORAGE_OVERRIDE_ANIM_FILES = new KAnimFile[] { Assets.GetAnim("anim_bury_dupe_kanim") };
		Storage storage = go.AddOrGet<Storage>();
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(GraveConfig.StorageModifiers);
		storage.overrideAnims = GraveConfig.STORAGE_OVERRIDE_ANIM_FILES;
		storage.workAnims = GraveConfig.STORAGE_WORK_ANIMS;
		storage.workingPstComplete = new HashedString[] { GraveConfig.STORAGE_PST_ANIM };
		storage.synchronizeAnims = false;
		storage.useGunForDelivery = false;
		storage.workAnimPlayMode = KAnim.PlayMode.Once;
		go.AddOrGet<Grave>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00031C3B File Offset: 0x0002FE3B
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000546 RID: 1350
	public const string ID = "Grave";

	// Token: 0x04000547 RID: 1351
	private static KAnimFile[] STORAGE_OVERRIDE_ANIM_FILES;

	// Token: 0x04000548 RID: 1352
	private static readonly HashedString[] STORAGE_WORK_ANIMS = new HashedString[] { "working_pre" };

	// Token: 0x04000549 RID: 1353
	private static readonly HashedString STORAGE_PST_ANIM = HashedString.Invalid;

	// Token: 0x0400054A RID: 1354
	private static readonly List<Storage.StoredItemModifier> StorageModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Preserve
	};
}
