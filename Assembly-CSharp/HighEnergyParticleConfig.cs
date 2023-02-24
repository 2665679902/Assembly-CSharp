using System;
using STRINGS;
using UnityEngine;

// Token: 0x020007AA RID: 1962
public class HighEnergyParticleConfig : IEntityConfig
{
	// Token: 0x06003780 RID: 14208 RVA: 0x00134C20 File Offset: 0x00132E20
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06003781 RID: 14209 RVA: 0x00134C28 File Offset: 0x00132E28
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity("HighEnergyParticle", ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, ITEMS.RADIATION.HIGHENERGYPARITCLE.DESC, 1f, false, Assets.GetAnim("spark_radial_high_energy_particles_kanim"), "travel_pre", Grid.SceneLayer.FXFront2, SimHashes.Creature, null, 293f);
		EntityTemplates.AddCollision(gameObject, EntityTemplates.CollisionShape.CIRCLE, 0.2f, 0.2f);
		gameObject.AddOrGet<LoopingSounds>();
		RadiationEmitter radiationEmitter = gameObject.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emitRadiusX = 3;
		radiationEmitter.emitRadiusY = 3;
		radiationEmitter.emitRads = 0.4f * ((float)radiationEmitter.emitRadiusX / 6f);
		gameObject.AddComponent<HighEnergyParticle>().speed = 8f;
		return gameObject;
	}

	// Token: 0x06003782 RID: 14210 RVA: 0x00134CDF File Offset: 0x00132EDF
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06003783 RID: 14211 RVA: 0x00134CE1 File Offset: 0x00132EE1
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04002529 RID: 9513
	public const int PARTICLE_SPEED = 8;

	// Token: 0x0400252A RID: 9514
	public const float PARTICLE_COLLISION_SIZE = 0.2f;

	// Token: 0x0400252B RID: 9515
	public const float PER_CELL_FALLOFF = 0.1f;

	// Token: 0x0400252C RID: 9516
	public const float FALLOUT_RATIO = 0.5f;

	// Token: 0x0400252D RID: 9517
	public const int MAX_PAYLOAD = 500;

	// Token: 0x0400252E RID: 9518
	public const int EXPLOSION_FALLOUT_TEMPERATURE = 5000;

	// Token: 0x0400252F RID: 9519
	public const float EXPLOSION_FALLOUT_MASS_PER_PARTICLE = 0.001f;

	// Token: 0x04002530 RID: 9520
	public const float EXPLOSION_EMIT_DURRATION = 1f;

	// Token: 0x04002531 RID: 9521
	public const short EXPLOSION_EMIT_RADIUS = 6;

	// Token: 0x04002532 RID: 9522
	public const string ID = "HighEnergyParticle";
}
