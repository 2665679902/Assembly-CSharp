using System;
using UnityEngine;

// Token: 0x0200065C RID: 1628
public interface IUsable
{
	// Token: 0x06002BB9 RID: 11193
	bool IsUsable();

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06002BBA RID: 11194
	Transform transform { get; }
}
