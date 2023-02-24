using System;

// Token: 0x020008FA RID: 2298
public class RunningWeightedAverage
{
	// Token: 0x06004263 RID: 16995 RVA: 0x00176574 File Offset: 0x00174774
	public RunningWeightedAverage(float minValue = -3.4028235E+38f, float maxValue = 3.4028235E+38f, int sampleCount = 15, bool allowZero = true)
	{
		this.min = minValue;
		this.max = maxValue;
		this.ignoreZero = !allowZero;
		this.samples = new float[sampleCount];
	}

	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x06004264 RID: 16996 RVA: 0x001765A1 File Offset: 0x001747A1
	public float GetWeightedAverage
	{
		get
		{
			return this.WeightedAverage();
		}
	}

	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x06004265 RID: 16997 RVA: 0x001765A9 File Offset: 0x001747A9
	public float GetUnweightedAverage
	{
		get
		{
			return this.WeightedAverage();
		}
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x001765B4 File Offset: 0x001747B4
	public void AddSample(float value)
	{
		if (this.ignoreZero && value == 0f)
		{
			return;
		}
		if (value > this.max)
		{
			value = this.max;
		}
		if (value < this.min)
		{
			value = this.min;
		}
		if (this.validValues < this.samples.Length)
		{
			this.validValues++;
		}
		for (int i = 0; i < this.samples.Length - 1; i++)
		{
			this.samples[i] = this.samples[i + 1];
		}
		this.samples[this.samples.Length - 1] = value;
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x0017664C File Offset: 0x0017484C
	private float WeightedAverage()
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = this.samples.Length - 1; i > this.samples.Length - 1 - this.validValues; i--)
		{
			float num3 = (float)(i + 1) / ((float)this.validValues + 1f);
			num += this.samples[i] * num3;
			num2 += num3;
		}
		num /= num2;
		if (float.IsNaN(num))
		{
			return 0f;
		}
		return num;
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x001766C4 File Offset: 0x001748C4
	private float UnweightedAverage()
	{
		float num = 0f;
		for (int i = this.samples.Length - 1; i > this.samples.Length - 1 - this.validValues; i--)
		{
			num += this.samples[i];
		}
		num /= (float)this.samples.Length;
		if (float.IsNaN(num))
		{
			return 0f;
		}
		return num;
	}

	// Token: 0x04002C5B RID: 11355
	private float[] samples;

	// Token: 0x04002C5C RID: 11356
	private float min;

	// Token: 0x04002C5D RID: 11357
	private float max;

	// Token: 0x04002C5E RID: 11358
	private bool ignoreZero;

	// Token: 0x04002C5F RID: 11359
	private int validValues;
}
