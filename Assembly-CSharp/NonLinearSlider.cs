using System;
using UnityEngine;

// Token: 0x02000B3F RID: 2879
public class NonLinearSlider : KSlider
{
	// Token: 0x06005920 RID: 22816 RVA: 0x002044AD File Offset: 0x002026AD
	public static NonLinearSlider.Range[] GetDefaultRange(float maxValue)
	{
		return new NonLinearSlider.Range[]
		{
			new NonLinearSlider.Range(100f, maxValue)
		};
	}

	// Token: 0x06005921 RID: 22817 RVA: 0x002044C7 File Offset: 0x002026C7
	protected override void Start()
	{
		base.Start();
		base.minValue = 0f;
		base.maxValue = 100f;
	}

	// Token: 0x06005922 RID: 22818 RVA: 0x002044E5 File Offset: 0x002026E5
	public void SetRanges(NonLinearSlider.Range[] ranges)
	{
		this.ranges = ranges;
	}

	// Token: 0x06005923 RID: 22819 RVA: 0x002044F0 File Offset: 0x002026F0
	public float GetPercentageFromValue(float value)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < this.ranges.Length; i++)
		{
			if (value >= num2 && value <= this.ranges[i].peakValue)
			{
				float num3 = (value - num2) / (this.ranges[i].peakValue - num2);
				return Mathf.Lerp(num, num + this.ranges[i].width, num3);
			}
			num += this.ranges[i].width;
			num2 = this.ranges[i].peakValue;
		}
		return 100f;
	}

	// Token: 0x06005924 RID: 22820 RVA: 0x00204594 File Offset: 0x00202794
	public float GetValueForPercentage(float percentage)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < this.ranges.Length; i++)
		{
			if (percentage >= num && num + this.ranges[i].width >= percentage)
			{
				float num3 = (percentage - num) / this.ranges[i].width;
				return Mathf.Lerp(num2, this.ranges[i].peakValue, num3);
			}
			num += this.ranges[i].width;
			num2 = this.ranges[i].peakValue;
		}
		return num2;
	}

	// Token: 0x06005925 RID: 22821 RVA: 0x00204630 File Offset: 0x00202830
	protected override void Set(float input, bool sendCallback)
	{
		base.Set(input, sendCallback);
	}

	// Token: 0x04003C32 RID: 15410
	public NonLinearSlider.Range[] ranges;

	// Token: 0x020019D8 RID: 6616
	[Serializable]
	public struct Range
	{
		// Token: 0x0600917B RID: 37243 RVA: 0x00314A03 File Offset: 0x00312C03
		public Range(float width, float peakValue)
		{
			this.width = width;
			this.peakValue = peakValue;
		}

		// Token: 0x04007587 RID: 30087
		public float width;

		// Token: 0x04007588 RID: 30088
		public float peakValue;
	}
}
