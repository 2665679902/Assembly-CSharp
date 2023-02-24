using System;

// Token: 0x020008F9 RID: 2297
public class RunningAverage
{
	// Token: 0x0600425F RID: 16991 RVA: 0x0017646A File Offset: 0x0017466A
	public RunningAverage(float minValue = -3.4028235E+38f, float maxValue = 3.4028235E+38f, int sampleCount = 15, bool allowZero = true)
	{
		this.min = minValue;
		this.max = maxValue;
		this.ignoreZero = !allowZero;
		this.samples = new float[sampleCount];
	}

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x06004260 RID: 16992 RVA: 0x00176497 File Offset: 0x00174697
	public float AverageValue
	{
		get
		{
			return this.GetAverage();
		}
	}

	// Token: 0x06004261 RID: 16993 RVA: 0x001764A0 File Offset: 0x001746A0
	public void AddSample(float value)
	{
		if (value < this.min || value > this.max || (this.ignoreZero && value == 0f))
		{
			return;
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

	// Token: 0x06004262 RID: 16994 RVA: 0x00176528 File Offset: 0x00174728
	private float GetAverage()
	{
		float num = 0f;
		for (int i = this.samples.Length - 1; i > this.samples.Length - 1 - this.validValues; i--)
		{
			num += this.samples[i];
		}
		return num / (float)this.validValues;
	}

	// Token: 0x04002C56 RID: 11350
	private float[] samples;

	// Token: 0x04002C57 RID: 11351
	private float min;

	// Token: 0x04002C58 RID: 11352
	private float max;

	// Token: 0x04002C59 RID: 11353
	private bool ignoreZero;

	// Token: 0x04002C5A RID: 11354
	private int validValues;
}
