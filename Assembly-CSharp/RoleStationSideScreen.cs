using System;
using UnityEngine;

// Token: 0x02000BD9 RID: 3033
public class RoleStationSideScreen : SideScreenContent
{
	// Token: 0x06005F83 RID: 24451 RVA: 0x0022F035 File Offset: 0x0022D235
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005F84 RID: 24452 RVA: 0x0022F03D File Offset: 0x0022D23D
	public override bool IsValidForTarget(GameObject target)
	{
		return false;
	}

	// Token: 0x0400416B RID: 16747
	public GameObject content;

	// Token: 0x0400416C RID: 16748
	private GameObject target;

	// Token: 0x0400416D RID: 16749
	public LocText DescriptionText;
}
