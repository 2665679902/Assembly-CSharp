using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class KProfilerBegin : MonoBehaviour
{
	// Token: 0x06000052 RID: 82 RVA: 0x00003F79 File Offset: 0x00002179
	private void Start()
	{
		global::Debug.Log("KProfiler: Start");
		KProfiler.BeginThread("Main", "Game");
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003F94 File Offset: 0x00002194
	private void Update()
	{
		KProfiler.BeginFrame();
	}

	// Token: 0x04000002 RID: 2
	public static int begin_counter;
}
