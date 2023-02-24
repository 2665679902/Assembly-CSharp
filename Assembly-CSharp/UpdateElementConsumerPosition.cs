using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
[AddComponentMenu("KMonoBehaviour/scripts/UpdateElementConsumerPosition")]
public class UpdateElementConsumerPosition : KMonoBehaviour, ISim200ms
{
	// Token: 0x06000527 RID: 1319 RVA: 0x00022FB5 File Offset: 0x000211B5
	public void Sim200ms(float dt)
	{
		base.GetComponent<ElementConsumer>().RefreshConsumptionRate();
	}
}
