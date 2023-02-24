using System;
using TUNING;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class WireConfig : BaseWireConfig
{
	// Token: 0x06001116 RID: 4374 RVA: 0x0005C674 File Offset: 0x0005A874
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Wire";
		string text2 = "utilities_electric_kanim";
		float num = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		return base.CreateBuildingDef(text, text2, num, tier, num2, BUILDINGS.DECOR.PENALTY.TIER0, none);
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0005C6AC File Offset: 0x0005A8AC
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max1000, go);
	}

	// Token: 0x0400093E RID: 2366
	public const string ID = "Wire";
}
