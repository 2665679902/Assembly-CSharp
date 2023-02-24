using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000358 RID: 856
public static class SequenceUtil
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06001135 RID: 4405 RVA: 0x0005CC88 File Offset: 0x0005AE88
	public static YieldInstruction WaitForNextFrame
	{
		get
		{
			return null;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06001136 RID: 4406 RVA: 0x0005CC8B File Offset: 0x0005AE8B
	public static YieldInstruction WaitForEndOfFrame
	{
		get
		{
			if (SequenceUtil.waitForEndOfFrame == null)
			{
				SequenceUtil.waitForEndOfFrame = new WaitForEndOfFrame();
			}
			return SequenceUtil.waitForEndOfFrame;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06001137 RID: 4407 RVA: 0x0005CCA3 File Offset: 0x0005AEA3
	public static YieldInstruction WaitForFixedUpdate
	{
		get
		{
			if (SequenceUtil.waitForFixedUpdate == null)
			{
				SequenceUtil.waitForFixedUpdate = new WaitForFixedUpdate();
			}
			return SequenceUtil.waitForFixedUpdate;
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0005CCBC File Offset: 0x0005AEBC
	public static YieldInstruction WaitForSeconds(float duration)
	{
		WaitForSeconds waitForSeconds;
		if (!SequenceUtil.scaledTimeCache.TryGetValue(duration, out waitForSeconds))
		{
			waitForSeconds = (SequenceUtil.scaledTimeCache[duration] = new WaitForSeconds(duration));
		}
		return waitForSeconds;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0005CCF0 File Offset: 0x0005AEF0
	public static WaitForSecondsRealtime WaitForSecondsRealtime(float duration)
	{
		WaitForSecondsRealtime waitForSecondsRealtime;
		if (!SequenceUtil.reailTimeWaitCache.TryGetValue(duration, out waitForSecondsRealtime))
		{
			waitForSecondsRealtime = (SequenceUtil.reailTimeWaitCache[duration] = new WaitForSecondsRealtime(duration));
		}
		return waitForSecondsRealtime;
	}

	// Token: 0x04000951 RID: 2385
	private static WaitForEndOfFrame waitForEndOfFrame = null;

	// Token: 0x04000952 RID: 2386
	private static WaitForFixedUpdate waitForFixedUpdate = null;

	// Token: 0x04000953 RID: 2387
	private static Dictionary<float, WaitForSeconds> scaledTimeCache = new Dictionary<float, WaitForSeconds>();

	// Token: 0x04000954 RID: 2388
	private static Dictionary<float, WaitForSecondsRealtime> reailTimeWaitCache = new Dictionary<float, WaitForSecondsRealtime>();
}
