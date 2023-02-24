using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000251 RID: 593
public class SatelliteCometConfig : IEntityConfig
{
	// Token: 0x06000BBD RID: 3005 RVA: 0x00042512 File Offset: 0x00040712
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0004251C File Offset: 0x0004071C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(SatelliteCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.SATELLITE.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(100f, 200f);
		comet.EXHAUST_ELEMENT = SimHashes.AluminumGas;
		comet.temperatureRange = new Vector2(473.15f, 573.15f);
		comet.entityDamage = 2;
		comet.explosionOreCount = new Vector2I(8, 8);
		comet.totalTileDamage = 2f;
		comet.splashRadius = 1;
		comet.impactSound = "Meteor_Large_Impact";
		comet.flyingSoundID = 1;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		comet.addTiles = 0;
		comet.craterPrefabs = new string[]
		{
			"PropSurfaceSatellite1",
			PropSurfaceSatellite2Config.ID,
			PropSurfaceSatellite3Config.ID
		};
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Aluminum, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_rock_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x000426AB File Offset: 0x000408AB
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x000426AD File Offset: 0x000408AD
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006E7 RID: 1767
	public static string ID = "SatelliteCometComet";
}
