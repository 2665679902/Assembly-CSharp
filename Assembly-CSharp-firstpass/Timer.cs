using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class Timer
{
	// Token: 0x06000926 RID: 2342 RVA: 0x000247C8 File Offset: 0x000229C8
	public void Start()
	{
		if (!this.isStarted)
		{
			this.startTime = Time.time;
			this.isStarted = true;
		}
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x000247E4 File Offset: 0x000229E4
	public void Stop()
	{
		this.isStarted = false;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x000247ED File Offset: 0x000229ED
	public float GetElapsed()
	{
		return Time.time - this.startTime;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x000247FB File Offset: 0x000229FB
	public bool TryStop(float elapsed_time)
	{
		if (this.isStarted && this.GetElapsed() >= elapsed_time)
		{
			this.Stop();
			return true;
		}
		return false;
	}

	// Token: 0x04000682 RID: 1666
	private float startTime;

	// Token: 0x04000683 RID: 1667
	private bool isStarted;
}
