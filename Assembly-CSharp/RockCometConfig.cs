using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200024A RID: 586
public class RockCometConfig : IEntityConfig
{
	// Token: 0x06000B93 RID: 2963 RVA: 0x00041A64 File Offset: 0x0003FC64
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00041A6C File Offset: 0x0003FC6C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(RockCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.ROCKCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		float mass = ElementLoader.FindElementByHash(SimHashes.Regolith).defaultValues.mass;
		comet.massRange = new Vector2(mass * 0.8f * 6f, mass * 1.2f * 6f);
		comet.temperatureRange = new Vector2(323.15f, 423.15f);
		comet.addTiles = 6;
		comet.addTilesMinHeight = 2;
		comet.addTilesMaxHeight = 8;
		comet.entityDamage = 20;
		comet.totalTileDamage = 0f;
		comet.splashRadius = 1;
		comet.impactSound = "Meteor_Large_Impact";
		comet.flyingSoundID = 2;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactDirt;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Regolith, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_rock_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00041BCD File Offset: 0x0003FDCD
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00041BCF File Offset: 0x0003FDCF
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006DD RID: 1757
	public static readonly string ID = "RockComet";

	// Token: 0x040006DE RID: 1758
	private const SimHashes element = SimHashes.Regolith;

	// Token: 0x040006DF RID: 1759
	private const int ADDED_CELLS = 6;
}
