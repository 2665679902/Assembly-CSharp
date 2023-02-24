using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class CopperCometConfig : IEntityConfig
{
	// Token: 0x06000B9F RID: 2975 RVA: 0x00041D5E File Offset: 0x0003FF5E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00041D68 File Offset: 0x0003FF68
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(CopperCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.COPPERCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(3f, 20f);
		comet.temperatureRange = new Vector2(323.15f, 423.15f);
		comet.explosionOreCount = new Vector2I(2, 4);
		comet.entityDamage = 15;
		comet.totalTileDamage = 0.5f;
		comet.splashRadius = 1;
		comet.impactSound = "Meteor_Medium_Impact";
		comet.flyingSoundID = 1;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactMetal;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Cuprite, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_copper_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00041EC2 File Offset: 0x000400C2
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00041EC4 File Offset: 0x000400C4
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006E1 RID: 1761
	public static string ID = "CopperCometConfig";
}
