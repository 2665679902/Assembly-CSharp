using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class DustCometConfig : IEntityConfig
{
	// Token: 0x06000BB1 RID: 2993 RVA: 0x000421D2 File Offset: 0x000403D2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x000421DC File Offset: 0x000403DC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(DustCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.DUSTCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(0.2f, 0.5f);
		comet.temperatureRange = new Vector2(223.15f, 253.15f);
		comet.entityDamage = 2;
		comet.totalTileDamage = 0.15f;
		comet.splashRadius = 0;
		comet.impactSound = "Meteor_Small_Impact";
		comet.flyingSoundID = 0;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Regolith, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_sand_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00042328 File Offset: 0x00040528
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0004232A File Offset: 0x0004052A
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006E4 RID: 1764
	public static string ID = "DustComet";
}
