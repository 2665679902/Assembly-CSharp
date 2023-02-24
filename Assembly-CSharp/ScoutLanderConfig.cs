using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class ScoutLanderConfig : IEntityConfig
{
	// Token: 0x06000F46 RID: 3910 RVA: 0x00053351 File Offset: 0x00051551
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00053358 File Offset: 0x00051558
	public GameObject CreatePrefab()
	{
		string text = "ScoutLander";
		string text2 = STRINGS.BUILDINGS.PREFABS.SCOUTLANDER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.SCOUTLANDER.DESC;
		float num = 400f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("rocket_scout_cargo_lander_kanim"), "grounded", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGetDef<CargoLander.Def>().previewTag = "ScoutLander_Preview".ToTag();
		gameObject.AddOrGetDef<CargoDropperStorage.Def>();
		gameObject.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(gameObject);
		gameObject.AddOrGet<Operational>();
		Storage storage = gameObject.AddComponent<Storage>();
		storage.showInUI = true;
		storage.allowItemRemoval = false;
		storage.capacityKg = 2000f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		gameObject.AddOrGet<Deconstructable>().audioSize = "large";
		gameObject.AddOrGet<Storable>();
		Placeable placeable = gameObject.AddOrGet<Placeable>();
		placeable.kAnimName = "rocket_scout_cargo_lander_kanim";
		placeable.animName = "place";
		placeable.placementRules = new List<Placeable.PlacementRules>
		{
			Placeable.PlacementRules.OnFoundation,
			Placeable.PlacementRules.VisibleToSpace,
			Placeable.PlacementRules.RestrictToWorld
		};
		EntityTemplates.CreateAndRegisterPreview("ScoutLander_Preview", Assets.GetAnim("rocket_scout_cargo_lander_kanim"), "place", ObjectLayer.Building, 3, 3);
		return gameObject;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00053486 File Offset: 0x00051686
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.ApplyToCells = false;
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000534A4 File Offset: 0x000516A4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000878 RID: 2168
	public const string ID = "ScoutLander";

	// Token: 0x04000879 RID: 2169
	public const string PREVIEW_ID = "ScoutLander_Preview";

	// Token: 0x0400087A RID: 2170
	public const float MASS = 400f;
}
