using System;
using TUNING;
using UnityEngine;

// Token: 0x0200021A RID: 538
public class LogicRibbonConfig : BaseLogicWireConfig
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x0003C440 File Offset: 0x0003A640
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LogicRibbon";
		string text2 = "logic_ribbon_kanim";
		float num = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		EffectorValues none = NOISE_POLLUTION.NONE;
		return base.CreateBuildingDef(text, text2, num, tier, BUILDINGS.DECOR.PENALTY.TIER0, none);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0003C473 File Offset: 0x0003A673
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(LogicWire.BitDepth.FourBit, go);
	}

	// Token: 0x0400063D RID: 1597
	public const string ID = "LogicRibbon";
}
