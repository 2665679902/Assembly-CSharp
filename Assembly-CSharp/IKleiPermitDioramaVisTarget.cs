using System;
using Database;
using UnityEngine;

// Token: 0x02000AD6 RID: 2774
public interface IKleiPermitDioramaVisTarget
{
	// Token: 0x06005513 RID: 21779
	GameObject GetGameObject();

	// Token: 0x06005514 RID: 21780
	void ConfigureSetup();

	// Token: 0x06005515 RID: 21781
	void ConfigureWith(PermitResource permit);
}
