using System;
using UnityEngine;

// Token: 0x02000479 RID: 1145
public struct ElementSplitter
{
	// Token: 0x0600199B RID: 6555 RVA: 0x00089790 File Offset: 0x00087990
	public ElementSplitter(GameObject go)
	{
		this.primaryElement = go.GetComponent<PrimaryElement>();
		this.onTakeCB = null;
		this.canAbsorbCB = null;
	}

	// Token: 0x04000E56 RID: 3670
	public PrimaryElement primaryElement;

	// Token: 0x04000E57 RID: 3671
	public Func<float, Pickupable> onTakeCB;

	// Token: 0x04000E58 RID: 3672
	public Func<Pickupable, bool> canAbsorbCB;
}
