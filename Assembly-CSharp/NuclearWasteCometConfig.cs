using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class NuclearWasteCometConfig : IEntityConfig
{
	// Token: 0x06000BB7 RID: 2999 RVA: 0x00042340 File Offset: 0x00040540
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x00042348 File Offset: 0x00040548
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(NuclearWasteCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.NUCLEAR_WASTE.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(NuclearWasteCometConfig.MASS, NuclearWasteCometConfig.MASS);
		comet.EXHAUST_ELEMENT = SimHashes.Fallout;
		comet.EXHAUST_RATE = NuclearWasteCometConfig.MASS * 0.2f;
		comet.temperatureRange = new Vector2(473.15f, 573.15f);
		comet.entityDamage = 2;
		comet.totalTileDamage = 0.45f;
		comet.splashRadius = 0;
		comet.impactSound = "Meteor_Nuclear_Impact";
		comet.flyingSoundID = 3;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		comet.addTiles = 1;
		comet.diseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id);
		comet.addDiseaseCount = 1000000;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Corium, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("nuclear_metldown_comet_fx_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x000424F0 File Offset: 0x000406F0
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x000424F2 File Offset: 0x000406F2
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006E5 RID: 1765
	public static string ID = "NuclearWasteComet";

	// Token: 0x040006E6 RID: 1766
	public static float MASS = 1f;
}
