using System;
using TUNING;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class WireHighWattageConfig : BaseWireConfig
{
	// Token: 0x06001119 RID: 4377 RVA: 0x0005C6C0 File Offset: 0x0005A8C0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HighWattageWire";
		string text2 = "utilities_electric_insulated_kanim";
		float num = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, BUILDINGS.DECOR.PENALTY.TIER5, none);
		buildingDef.BuildLocationRule = BuildLocationRule.NotInTiles;
		return buildingDef;
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0005C6FF File Offset: 0x0005A8FF
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max20000, go);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0005C709 File Offset: 0x0005A909
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
	}

	// Token: 0x0400093F RID: 2367
	public const string ID = "HighWattageWire";
}
