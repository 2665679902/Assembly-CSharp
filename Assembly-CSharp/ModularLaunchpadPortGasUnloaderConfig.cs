using System;
using UnityEngine;

// Token: 0x02000277 RID: 631
public class ModularLaunchpadPortGasUnloaderConfig : IBuildingConfig
{
	// Token: 0x06000C97 RID: 3223 RVA: 0x00046C19 File Offset: 0x00044E19
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00046C20 File Offset: 0x00044E20
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortGasUnloader", "conduit_port_gas_unloader_kanim", ConduitType.Gas, false, 2, 3);
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x00046C35 File Offset: 0x00044E35
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Gas, 1f, false);
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x00046C45 File Offset: 0x00044E45
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, false);
	}

	// Token: 0x04000754 RID: 1876
	public const string ID = "ModularLaunchpadPortGasUnloader";
}
