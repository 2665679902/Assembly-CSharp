using System;
using System.Collections.Generic;

// Token: 0x0200053F RID: 1343
public class Accumulators
{
	// Token: 0x06002015 RID: 8213 RVA: 0x000AF701 File Offset: 0x000AD901
	public Accumulators()
	{
		this.elapsedTime = 0f;
		this.accumulated = new KCompactedVector<float>(0);
		this.average = new KCompactedVector<float>(0);
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x000AF72C File Offset: 0x000AD92C
	public HandleVector<int>.Handle Add(string name, KMonoBehaviour owner)
	{
		HandleVector<int>.Handle handle = this.accumulated.Allocate(0f);
		this.average.Allocate(0f);
		return handle;
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x000AF74F File Offset: 0x000AD94F
	public HandleVector<int>.Handle Remove(HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return HandleVector<int>.InvalidHandle;
		}
		this.accumulated.Free(handle);
		this.average.Free(handle);
		return HandleVector<int>.InvalidHandle;
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x000AF780 File Offset: 0x000AD980
	public void Sim200ms(float dt)
	{
		this.elapsedTime += dt;
		if (this.elapsedTime < 3f)
		{
			return;
		}
		this.elapsedTime -= 3f;
		List<float> dataList = this.accumulated.GetDataList();
		List<float> dataList2 = this.average.GetDataList();
		int count = dataList.Count;
		float num = 0.33333334f;
		for (int i = 0; i < count; i++)
		{
			dataList2[i] = dataList[i] * num;
			dataList[i] = 0f;
		}
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x000AF80F File Offset: 0x000ADA0F
	public float GetAverageRate(HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return 0f;
		}
		return this.average.GetData(handle);
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x000AF82C File Offset: 0x000ADA2C
	public void Accumulate(HandleVector<int>.Handle handle, float amount)
	{
		float data = this.accumulated.GetData(handle);
		this.accumulated.SetData(handle, data + amount);
	}

	// Token: 0x04001261 RID: 4705
	private const float TIME_WINDOW = 3f;

	// Token: 0x04001262 RID: 4706
	private float elapsedTime;

	// Token: 0x04001263 RID: 4707
	private KCompactedVector<float> accumulated;

	// Token: 0x04001264 RID: 4708
	private KCompactedVector<float> average;
}
