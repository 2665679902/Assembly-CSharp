using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class BeeConfig : IEntityConfig
{
	// Token: 0x060002C4 RID: 708 RVA: 0x00016A10 File Offset: 0x00014C10
	public static GameObject CreateBee(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseBeeConfig.BaseBee(id, name, desc, anim_file, "BeeBaseTrait", DECOR.BONUS.TIER4, is_baby, null);
		Trait trait = Db.Get().CreateTrait("BeeBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 5f, name, false, false, true));
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00016AAD File Offset: 0x00014CAD
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00016AB4 File Offset: 0x00014CB4
	public GameObject CreatePrefab()
	{
		return BeeConfig.CreateBee("Bee", STRINGS.CREATURES.SPECIES.BEE.NAME, STRINGS.CREATURES.SPECIES.BEE.DESC, "bee_kanim", false);
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00016ADA File Offset: 0x00014CDA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00016ADC File Offset: 0x00014CDC
	public void OnSpawn(GameObject inst)
	{
		BaseBeeConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x040001D9 RID: 473
	public const string ID = "Bee";

	// Token: 0x040001DA RID: 474
	public const string BASE_TRAIT_ID = "BeeBaseTrait";
}
