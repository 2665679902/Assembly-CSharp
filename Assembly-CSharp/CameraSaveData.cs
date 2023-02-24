using System;
using UnityEngine;

// Token: 0x02000679 RID: 1657
public static class CameraSaveData
{
	// Token: 0x06002CB3 RID: 11443 RVA: 0x000EA638 File Offset: 0x000E8838
	public static void Load(FastReader reader)
	{
		CameraSaveData.position = reader.ReadVector3();
		CameraSaveData.localScale = reader.ReadVector3();
		CameraSaveData.rotation = reader.ReadQuaternion();
		CameraSaveData.orthographicsSize = reader.ReadSingle();
		CameraSaveData.valid = true;
	}

	// Token: 0x04001A9C RID: 6812
	public static bool valid;

	// Token: 0x04001A9D RID: 6813
	public static Vector3 position;

	// Token: 0x04001A9E RID: 6814
	public static Vector3 localScale;

	// Token: 0x04001A9F RID: 6815
	public static Quaternion rotation;

	// Token: 0x04001AA0 RID: 6816
	public static float orthographicsSize;
}
