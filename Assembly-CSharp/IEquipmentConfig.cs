using System;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public interface IEquipmentConfig
{
	// Token: 0x0600338F RID: 13199
	EquipmentDef CreateEquipmentDef();

	// Token: 0x06003390 RID: 13200
	void DoPostConfigure(GameObject go);

	// Token: 0x06003391 RID: 13201
	string[] GetDlcIds();
}
