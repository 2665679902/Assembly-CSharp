using System;
using UnityEngine;

// Token: 0x02000456 RID: 1110
[AddComponentMenu("KMonoBehaviour/scripts/CameraFollowHelper")]
public class CameraFollowHelper : KMonoBehaviour
{
	// Token: 0x06001868 RID: 6248 RVA: 0x000817C6 File Offset: 0x0007F9C6
	private void LateUpdate()
	{
		if (CameraController.Instance != null)
		{
			CameraController.Instance.UpdateFollowTarget();
		}
	}
}
