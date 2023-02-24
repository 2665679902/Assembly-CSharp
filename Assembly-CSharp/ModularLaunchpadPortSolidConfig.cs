using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class ModularLaunchpadPortSolidConfig : IBuildingConfig
{
	// Token: 0x06000CA6 RID: 3238 RVA: 0x00046CD0 File Offset: 0x00044ED0
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00046CD7 File Offset: 0x00044ED7
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortSolid", "conduit_port_solid_loader_kanim", ConduitType.Solid, true, 2, 2);
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00046CEC File Offset: 0x00044EEC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Solid, 20f, true);
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00046CFC File Offset: 0x00044EFC
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, true);
	}

	// Token: 0x04000757 RID: 1879
	public const string ID = "ModularLaunchpadPortSolid";
}
