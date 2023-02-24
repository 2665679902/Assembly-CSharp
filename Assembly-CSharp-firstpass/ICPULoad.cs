using System;

// Token: 0x02000083 RID: 131
public interface ICPULoad
{
	// Token: 0x0600052E RID: 1326
	float GetEstimatedFrameTime();

	// Token: 0x0600052F RID: 1327
	bool AdjustLoad(float currentFrameTime, float frameTimeDelta);
}
