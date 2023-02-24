using System;
using UnityEngine;

// Token: 0x02000BDE RID: 3038
public class SideScreen : KScreen
{
	// Token: 0x06005FC2 RID: 24514 RVA: 0x002312C6 File Offset: 0x0022F4C6
	public void SetContent(SideScreenContent sideScreenContent, GameObject target)
	{
		if (sideScreenContent.transform.parent != this.contentBody.transform)
		{
			sideScreenContent.transform.SetParent(this.contentBody.transform);
		}
		sideScreenContent.SetTarget(target);
	}

	// Token: 0x0400419E RID: 16798
	[SerializeField]
	private GameObject contentBody;
}
