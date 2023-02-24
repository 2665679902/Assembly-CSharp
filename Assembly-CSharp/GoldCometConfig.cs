using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class GoldCometConfig : IEntityConfig
{
	// Token: 0x06000BA5 RID: 2981 RVA: 0x00041EDA File Offset: 0x000400DA
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00041EE4 File Offset: 0x000400E4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(GoldCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.GOLDCOMET.NAME, true);
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
		primaryElement.SetElement(SimHashes.GoldAmalgam, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_gold_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x0004203E File Offset: 0x0004023E
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00042040 File Offset: 0x00040240
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006E2 RID: 1762
	public static string ID = "GoldComet";
}
