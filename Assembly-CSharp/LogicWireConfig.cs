using System;
using TUNING;
using UnityEngine;

// Token: 0x02000223 RID: 547
public class LogicWireConfig : BaseLogicWireConfig
{
	// Token: 0x06000AD3 RID: 2771 RVA: 0x0003CFE4 File Offset: 0x0003B1E4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LogicWire";
		string text2 = "logic_wires_kanim";
		float num = 3f;
		float[] tier_TINY = BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
		EffectorValues none = NOISE_POLLUTION.NONE;
		return base.CreateBuildingDef(text, text2, num, tier_TINY, BUILDINGS.DECOR.PENALTY.TIER0, none);
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0003D017 File Offset: 0x0003B217
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(LogicWire.BitDepth.OneBit, go);
	}

	// Token: 0x04000648 RID: 1608
	public const string ID = "LogicWire";
}
