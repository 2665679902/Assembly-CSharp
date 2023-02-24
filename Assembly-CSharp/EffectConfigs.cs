using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class EffectConfigs : IMultiEntityConfig
{
	// Token: 0x060001D3 RID: 467 RVA: 0x0000CF40 File Offset: 0x0000B140
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		var array = new <>f__AnonymousType0<string, string[], string, KAnim.PlayMode, bool>[]
		{
			new
			{
				id = EffectConfigs.EffectTemplateId,
				animFiles = new string[0],
				initialAnim = "",
				initialMode = KAnim.PlayMode.Once,
				destroyOnAnimComplete = false
			},
			new
			{
				id = EffectConfigs.EffectTemplateOverrideId,
				animFiles = new string[0],
				initialAnim = "",
				initialMode = KAnim.PlayMode.Once,
				destroyOnAnimComplete = false
			},
			new
			{
				id = EffectConfigs.AttackSplashId,
				animFiles = new string[] { "attack_beam_contact_fx_kanim" },
				initialAnim = "loop",
				initialMode = KAnim.PlayMode.Loop,
				destroyOnAnimComplete = false
			},
			new
			{
				id = EffectConfigs.OreAbsorbId,
				animFiles = new string[] { "ore_collision_kanim" },
				initialAnim = "idle",
				initialMode = KAnim.PlayMode.Once,
				destroyOnAnimComplete = true
			},
			new
			{
				id = EffectConfigs.PlantDeathId,
				animFiles = new string[] { "plant_death_fx_kanim" },
				initialAnim = "plant_death",
				initialMode = KAnim.PlayMode.Once,
				destroyOnAnimComplete = true
			},
			new
			{
				id = EffectConfigs.BuildSplashId,
				animFiles = new string[] { "sparks_radial_build_kanim" },
				initialAnim = "loop",
				initialMode = KAnim.PlayMode.Loop,
				destroyOnAnimComplete = false
			},
			new
			{
				id = EffectConfigs.DemolishSplashId,
				animFiles = new string[] { "poi_demolish_impact_kanim" },
				initialAnim = "POI_demolish_impact",
				initialMode = KAnim.PlayMode.Loop,
				destroyOnAnimComplete = false
			}
		};
		for (int i = 0; i < array.Length; i++)
		{
			var <>f__AnonymousType = array[i];
			GameObject gameObject = EntityTemplates.CreateEntity(<>f__AnonymousType.id, <>f__AnonymousType.id, false);
			KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
			kbatchedAnimController.materialType = KAnimBatchGroup.MaterialType.Simple;
			kbatchedAnimController.initialAnim = <>f__AnonymousType.initialAnim;
			kbatchedAnimController.initialMode = <>f__AnonymousType.initialMode;
			kbatchedAnimController.isMovable = true;
			kbatchedAnimController.destroyOnAnimComplete = <>f__AnonymousType.destroyOnAnimComplete;
			if (<>f__AnonymousType.id == EffectConfigs.EffectTemplateOverrideId)
			{
				SymbolOverrideControllerUtil.AddToPrefab(gameObject);
			}
			if (<>f__AnonymousType.animFiles.Length != 0)
			{
				KAnimFile[] array2 = new KAnimFile[<>f__AnonymousType.animFiles.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = Assets.GetAnim(<>f__AnonymousType.animFiles[j]);
				}
				kbatchedAnimController.AnimFiles = array2;
			}
			gameObject.AddOrGet<LoopingSounds>();
			list.Add(gameObject);
		}
		return list;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000D11B File Offset: 0x0000B31B
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000D11D File Offset: 0x0000B31D
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000111 RID: 273
	public static string EffectTemplateId = "EffectTemplateFx";

	// Token: 0x04000112 RID: 274
	public static string EffectTemplateOverrideId = "EffectTemplateOverrideFx";

	// Token: 0x04000113 RID: 275
	public static string AttackSplashId = "AttackSplashFx";

	// Token: 0x04000114 RID: 276
	public static string OreAbsorbId = "OreAbsorbFx";

	// Token: 0x04000115 RID: 277
	public static string PlantDeathId = "PlantDeathFx";

	// Token: 0x04000116 RID: 278
	public static string BuildSplashId = "BuildSplashFx";

	// Token: 0x04000117 RID: 279
	public static string DemolishSplashId = "DemolishSplashFx";
}
