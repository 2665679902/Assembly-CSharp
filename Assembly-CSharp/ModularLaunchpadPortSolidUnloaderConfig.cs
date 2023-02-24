using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class ModularLaunchpadPortSolidUnloaderConfig : IBuildingConfig
{
	// Token: 0x06000CAB RID: 3243 RVA: 0x00046D0D File Offset: 0x00044F0D
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00046D14 File Offset: 0x00044F14
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortSolidUnloader", "conduit_port_solid_unloader_kanim", ConduitType.Solid, false, 2, 3);
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00046D29 File Offset: 0x00044F29
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Solid, 20f, false);
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00046D39 File Offset: 0x00044F39
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, false);
	}

	// Token: 0x04000758 RID: 1880
	public const string ID = "ModularLaunchpadPortSolidUnloader";
}
