using System;
using UnityEngine;

// Token: 0x0200087E RID: 2174
public struct OreSizeVisualizerData
{
	// Token: 0x06003E6A RID: 15978 RVA: 0x0015D294 File Offset: 0x0015B494
	public OreSizeVisualizerData(GameObject go)
	{
		this.primaryElement = go.GetComponent<PrimaryElement>();
		this.onMassChangedCB = null;
	}

	// Token: 0x040028DA RID: 10458
	public PrimaryElement primaryElement;

	// Token: 0x040028DB RID: 10459
	public Action<object> onMassChangedCB;
}
