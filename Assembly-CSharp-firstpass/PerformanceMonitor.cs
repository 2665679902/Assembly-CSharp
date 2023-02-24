using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class PerformanceMonitor : MonoBehaviour
{
	// Token: 0x0600083B RID: 2107 RVA: 0x00021518 File Offset: 0x0001F718
	private void Update()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		if (unscaledDeltaTime <= 0.033333335f)
		{
			this.numFramesAbove30 += 1UL;
		}
		else
		{
			this.numFramesBelow30 += 1UL;
		}
		if (this.frameTimes.Count == PerformanceMonitor.frameRateWindowSize)
		{
			LinkedListNode<float> first = this.frameTimes.First;
			this.frameTimeTotal -= first.Value;
			this.frameTimes.RemoveFirst();
			first.Value = unscaledDeltaTime;
			this.frameTimes.AddLast(first);
		}
		else
		{
			this.frameTimes.AddLast(unscaledDeltaTime);
		}
		this.frameTimeTotal += unscaledDeltaTime;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000215CC File Offset: 0x0001F7CC
	public void Reset()
	{
		this.numFramesAbove30 = 0UL;
		this.numFramesBelow30 = 0UL;
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x0600083D RID: 2109 RVA: 0x000215DE File Offset: 0x0001F7DE
	public ulong NumFramesAbove30
	{
		get
		{
			return this.numFramesAbove30;
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x0600083E RID: 2110 RVA: 0x000215E6 File Offset: 0x0001F7E6
	public ulong NumFramesBelow30
	{
		get
		{
			return this.numFramesBelow30;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x0600083F RID: 2111 RVA: 0x000215EE File Offset: 0x0001F7EE
	public float FPS
	{
		get
		{
			if (this.frameTimeTotal != 0f)
			{
				return (float)this.frameTimes.Count / this.frameTimeTotal;
			}
			return 0f;
		}
	}

	// Token: 0x0400062D RID: 1581
	private ulong numFramesAbove30;

	// Token: 0x0400062E RID: 1582
	private ulong numFramesBelow30;

	// Token: 0x0400062F RID: 1583
	private LinkedList<float> frameTimes = new LinkedList<float>();

	// Token: 0x04000630 RID: 1584
	private float frameTimeTotal;

	// Token: 0x04000631 RID: 1585
	private static readonly int frameRateWindowSize = 150;

	// Token: 0x04000632 RID: 1586
	private const float GOOD_FRAME_TIME = 0.033333335f;
}
