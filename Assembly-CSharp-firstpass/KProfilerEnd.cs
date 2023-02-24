using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class KProfilerEnd : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x00003FA5 File Offset: 0x000021A5
	private void Start()
	{
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003FA7 File Offset: 0x000021A7
	private void LateUpdate()
	{
		KProfiler.EndFrame();
	}
}
