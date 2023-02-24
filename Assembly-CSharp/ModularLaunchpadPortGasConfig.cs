using System;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class ModularLaunchpadPortGasConfig : IBuildingConfig
{
	// Token: 0x06000C92 RID: 3218 RVA: 0x00046BDC File Offset: 0x00044DDC
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00046BE3 File Offset: 0x00044DE3
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortGas", "conduit_port_gas_loader_kanim", ConduitType.Gas, true, 2, 2);
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x00046BF8 File Offset: 0x00044DF8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Gas, 1f, true);
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00046C08 File Offset: 0x00044E08
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, true);
	}

	// Token: 0x04000753 RID: 1875
	public const string ID = "ModularLaunchpadPortGas";
}
