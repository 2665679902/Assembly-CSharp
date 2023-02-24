using System;
using UnityEngine;

// Token: 0x0200057F RID: 1407
public abstract class IBuildingConfig
{
	// Token: 0x06002234 RID: 8756
	public abstract BuildingDef CreateBuildingDef();

	// Token: 0x06002235 RID: 8757 RVA: 0x000B9AD9 File Offset: 0x000B7CD9
	public virtual void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
	}

	// Token: 0x06002236 RID: 8758
	public abstract void DoPostConfigureComplete(GameObject go);

	// Token: 0x06002237 RID: 8759 RVA: 0x000B9ADB File Offset: 0x000B7CDB
	public virtual void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06002238 RID: 8760 RVA: 0x000B9ADD File Offset: 0x000B7CDD
	public virtual void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06002239 RID: 8761 RVA: 0x000B9ADF File Offset: 0x000B7CDF
	public virtual void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x000B9AE1 File Offset: 0x000B7CE1
	public virtual string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x000B9AE8 File Offset: 0x000B7CE8
	public virtual bool ForbidFromLoading()
	{
		return false;
	}
}
