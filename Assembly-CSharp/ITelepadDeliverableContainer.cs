using System;
using UnityEngine;

// Token: 0x020007B5 RID: 1973
public interface ITelepadDeliverableContainer
{
	// Token: 0x060037D0 RID: 14288
	void SelectDeliverable();

	// Token: 0x060037D1 RID: 14289
	void DeselectDeliverable();

	// Token: 0x060037D2 RID: 14290
	GameObject GetGameObject();
}
