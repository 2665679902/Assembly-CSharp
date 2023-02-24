using System;
using TUNING;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class WireRefinedConfig : BaseWireConfig
{
	// Token: 0x06001126 RID: 4390 RVA: 0x0005C844 File Offset: 0x0005AA44
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WireRefined";
		string text2 = "utilities_electric_conduct_kanim";
		float num = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, BUILDINGS.DECOR.NONE, none);
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		return buildingDef;
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0005C887 File Offset: 0x0005AA87
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max2000, go);
	}

	// Token: 0x04000942 RID: 2370
	public const string ID = "WireRefined";
}
