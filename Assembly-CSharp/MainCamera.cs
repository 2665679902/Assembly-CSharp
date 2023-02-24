using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class MainCamera : MonoBehaviour
{
	// Token: 0x06001A96 RID: 6806 RVA: 0x0008E33F File Offset: 0x0008C53F
	private void Awake()
	{
		if (Camera.main != null)
		{
			UnityEngine.Object.Destroy(Camera.main.gameObject);
		}
		base.gameObject.tag = "MainCamera";
	}
}
