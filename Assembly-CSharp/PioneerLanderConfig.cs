using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class PioneerLanderConfig : IEntityConfig
{
	// Token: 0x06000D63 RID: 3427 RVA: 0x0004A607 File Offset: 0x00048807
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0004A610 File Offset: 0x00048810
	public GameObject CreatePrefab()
	{
		string text = "PioneerLander";
		string text2 = STRINGS.BUILDINGS.PREFABS.PIONEERLANDER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PIONEERLANDER.DESC;
		float num = 400f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("rocket_pioneer_cargo_lander_kanim"), "grounded", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGetDef<CargoLander.Def>().previewTag = "PioneerLander_Preview".ToTag();
		CargoDropperMinion.Def def = gameObject.AddOrGetDef<CargoDropperMinion.Def>();
		def.kAnimName = "anim_interacts_pioneer_cargo_lander_kanim";
		def.animName = "enter";
		gameObject.AddOrGet<MinionStorage>();
		gameObject.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(gameObject);
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Deconstructable>().audioSize = "large";
		gameObject.AddOrGet<Storable>();
		Placeable placeable = gameObject.AddOrGet<Placeable>();
		placeable.kAnimName = "rocket_pioneer_cargo_lander_kanim";
		placeable.animName = "place";
		placeable.placementRules = new List<Placeable.PlacementRules>
		{
			Placeable.PlacementRules.OnFoundation,
			Placeable.PlacementRules.VisibleToSpace,
			Placeable.PlacementRules.RestrictToWorld
		};
		EntityTemplates.CreateAndRegisterPreview("PioneerLander_Preview", Assets.GetAnim("rocket_pioneer_cargo_lander_kanim"), "place", ObjectLayer.Building, 3, 3);
		return gameObject;
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0004A730 File Offset: 0x00048930
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.ApplyToCells = false;
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0004A74E File Offset: 0x0004894E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040007CA RID: 1994
	public const string ID = "PioneerLander";

	// Token: 0x040007CB RID: 1995
	public const string PREVIEW_ID = "PioneerLander_Preview";

	// Token: 0x040007CC RID: 1996
	public const float MASS = 400f;
}
