using System;
using TUNING;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class WireRefinedHighWattageConfig : BaseWireConfig
{
	// Token: 0x06001129 RID: 4393 RVA: 0x0005C89C File Offset: 0x0005AA9C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WireRefinedHighWattage";
		string text2 = "utilities_electric_conduct_hiwatt_kanim";
		float num = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, BUILDINGS.DECOR.PENALTY.TIER3, none);
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		buildingDef.BuildLocationRule = BuildLocationRule.NotInTiles;
		return buildingDef;
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0005C8E6 File Offset: 0x0005AAE6
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max50000, go);
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0005C8F0 File Offset: 0x0005AAF0
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.CanPowerTinker.Id;
	}

	// Token: 0x04000943 RID: 2371
	public const string ID = "WireRefinedHighWattage";
}
