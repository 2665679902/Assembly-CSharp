using System;
using UnityEngine;

// Token: 0x020004C4 RID: 1220
public interface IRottable
{
	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06001C42 RID: 7234
	GameObject gameObject { get; }

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06001C43 RID: 7235
	float RotTemperature { get; }

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06001C44 RID: 7236
	float PreserveTemperature { get; }
}
