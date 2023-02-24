using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class KComponentCleanUp : MonoBehaviour
{
	// Token: 0x0600068B RID: 1675 RVA: 0x0001D0AB File Offset: 0x0001B2AB
	public static void SetInCleanUpPhase(bool in_cleanup_phase)
	{
		KComponentCleanUp.inCleanUpPhase = in_cleanup_phase;
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
	public static bool InCleanUpPhase
	{
		get
		{
			return KComponentCleanUp.inCleanUpPhase;
		}
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0001D0BA File Offset: 0x0001B2BA
	private void Awake()
	{
		KComponentCleanUp.instance = this;
		this.comps = base.GetComponent<KComponentSpawn>().comps;
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0001D0D3 File Offset: 0x0001B2D3
	private void FixedUpdate()
	{
		KComponentCleanUp.SetInCleanUpPhase(true);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0001D0DB File Offset: 0x0001B2DB
	private void Update()
	{
		KComponentCleanUp.SetInCleanUpPhase(true);
		this.comps.CleanUp();
	}

	// Token: 0x040005BA RID: 1466
	public static KComponentCleanUp instance;

	// Token: 0x040005BB RID: 1467
	private static bool inCleanUpPhase;

	// Token: 0x040005BC RID: 1468
	private KComponents comps;
}
