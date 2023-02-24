using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class MoleConfig : IEntityConfig
{
	// Token: 0x060004D6 RID: 1238 RVA: 0x00021D0C File Offset: 0x0001FF0C
	public static GameObject CreateMole(string id, string name, string desc, string anim_file, bool is_baby = false)
	{
		GameObject gameObject = BaseMoleConfig.BaseMole(id, name, STRINGS.CREATURES.SPECIES.MOLE.DESC, "MoleBaseTrait", anim_file, is_baby, null, 10);
		gameObject.AddTag(GameTags.Creatures.Digger);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, MoleTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("MoleBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, MoleTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -MoleTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet diet = new Diet(BaseMoleConfig.SimpleOreDiet(new List<Tag>
		{
			SimHashes.Regolith.CreateTag(),
			SimHashes.Dirt.CreateTag(),
			SimHashes.IronOre.CreateTag()
		}, MoleConfig.CALORIES_PER_KG_OF_DIRT, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL).ToArray());
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = MoleConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
		gameObject.AddOrGet<LoopingSounds>();
		foreach (HashedString hashedString in MoleTuning.GINGER_SYMBOL_NAMES)
		{
			gameObject.GetComponent<KAnimControllerBase>().SetSymbolVisiblity(hashedString, false);
		}
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00021EDE File Offset: 0x000200DE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00021EE8 File Offset: 0x000200E8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MoleConfig.CreateMole("Mole", STRINGS.CREATURES.SPECIES.MOLE.NAME, STRINGS.CREATURES.SPECIES.MOLE.DESC, "driller_kanim", false);
		string text = "MoleEgg";
		string text2 = STRINGS.CREATURES.SPECIES.MOLE.EGG_NAME;
		string text3 = STRINGS.CREATURES.SPECIES.MOLE.DESC;
		string text4 = "egg_driller_kanim";
		float egg_MASS = MoleTuning.EGG_MASS;
		string text5 = "MoleBaby";
		float num = 60.000004f;
		float num2 = 20f;
		int egg_SORT_ORDER = MoleConfig.EGG_SORT_ORDER;
		return EntityTemplates.ExtendEntityToFertileCreature(gameObject, text, text2, text3, text4, egg_MASS, text5, num, num2, MoleTuning.EGG_CHANCES_BASE, egg_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00021F65 File Offset: 0x00020165
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00021F67 File Offset: 0x00020167
	public void OnSpawn(GameObject inst)
	{
		MoleConfig.SetSpawnNavType(inst);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00021F70 File Offset: 0x00020170
	public static void SetSpawnNavType(GameObject inst)
	{
		int num = Grid.PosToCell(inst);
		Navigator component = inst.GetComponent<Navigator>();
		if (component != null)
		{
			if (Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Solid);
				inst.transform.SetPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.FXFront));
				inst.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.FXFront);
				return;
			}
			inst.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Creatures);
		}
	}

	// Token: 0x04000332 RID: 818
	public const string ID = "Mole";

	// Token: 0x04000333 RID: 819
	public const string BASE_TRAIT_ID = "MoleBaseTrait";

	// Token: 0x04000334 RID: 820
	public const string EGG_ID = "MoleEgg";

	// Token: 0x04000335 RID: 821
	private static float MIN_POOP_SIZE_IN_CALORIES = 2400000f;

	// Token: 0x04000336 RID: 822
	private static float CALORIES_PER_KG_OF_DIRT = 1000f;

	// Token: 0x04000337 RID: 823
	public static int EGG_SORT_ORDER = 800;
}
