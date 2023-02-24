using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class GassyMooCometConfig : IEntityConfig
{
	// Token: 0x06000BC9 RID: 3017 RVA: 0x00042888 File Offset: 0x00040A88
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00042890 File Offset: 0x00040A90
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(GassyMooCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.GASSYMOOCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		GassyMooComet gassyMooComet = gameObject.AddOrGet<GassyMooComet>();
		gassyMooComet.massRange = new Vector2(100f, 200f);
		gassyMooComet.EXHAUST_ELEMENT = SimHashes.Methane;
		gassyMooComet.temperatureRange = new Vector2(296.15f, 318.15f);
		gassyMooComet.entityDamage = 0;
		gassyMooComet.explosionOreCount = new Vector2I(0, 0);
		gassyMooComet.totalTileDamage = 0f;
		gassyMooComet.splashRadius = 1;
		gassyMooComet.impactSound = "Meteor_GassyMoo_Impact";
		gassyMooComet.flyingSoundID = 4;
		gassyMooComet.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		gassyMooComet.addTiles = 0;
		gassyMooComet.destroyOnExplode = false;
		gassyMooComet.craterPrefabs = new string[] { "Moo" };
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Creature, true);
		primaryElement.Temperature = (gassyMooComet.temperatureRange.x + gassyMooComet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_gassymoo_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x000429F7 File Offset: 0x00040BF7
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000429F9 File Offset: 0x00040BF9
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006E9 RID: 1769
	public static string ID = "GassyMoo";
}
