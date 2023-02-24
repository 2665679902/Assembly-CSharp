using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class ModularLaunchpadPortLiquidConfig : IBuildingConfig
{
	// Token: 0x06000C9C RID: 3228 RVA: 0x00046C56 File Offset: 0x00044E56
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00046C5D File Offset: 0x00044E5D
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortLiquid", "conduit_port_liquid_loader_kanim", ConduitType.Liquid, true, 2, 2);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00046C72 File Offset: 0x00044E72
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Liquid, 10f, true);
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00046C82 File Offset: 0x00044E82
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, true);
	}

	// Token: 0x04000755 RID: 1877
	public const string ID = "ModularLaunchpadPortLiquid";
}
