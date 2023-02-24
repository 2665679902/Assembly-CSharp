using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class ModularLaunchpadPortLiquidUnloaderConfig : IBuildingConfig
{
	// Token: 0x06000CA1 RID: 3233 RVA: 0x00046C93 File Offset: 0x00044E93
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00046C9A File Offset: 0x00044E9A
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortLiquidUnloader", "conduit_port_liquid_unloader_kanim", ConduitType.Liquid, false, 2, 3);
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00046CAF File Offset: 0x00044EAF
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Liquid, 10f, false);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00046CBF File Offset: 0x00044EBF
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, false);
	}

	// Token: 0x04000756 RID: 1878
	public const string ID = "ModularLaunchpadPortLiquidUnloader";
}
