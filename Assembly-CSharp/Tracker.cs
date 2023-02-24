﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F4 RID: 1268
public abstract class Tracker
{
	// Token: 0x06001DDA RID: 7642 RVA: 0x0009EF38 File Offset: 0x0009D138
	public global::Tuple<float, float>[] ChartableData(float periodLength)
	{
		float time = GameClock.Instance.GetTime();
		List<global::Tuple<float, float>> list = new List<global::Tuple<float, float>>();
		int num = this.dataPoints.Count - 1;
		while (num >= 0 && this.dataPoints[num].periodStart >= time - periodLength)
		{
			list.Add(new global::Tuple<float, float>(this.dataPoints[num].periodStart, this.dataPoints[num].periodValue));
			num--;
		}
		if (list.Count == 0)
		{
			if (this.dataPoints.Count > 0)
			{
				list.Add(new global::Tuple<float, float>(this.dataPoints[this.dataPoints.Count - 1].periodStart, this.dataPoints[this.dataPoints.Count - 1].periodValue));
			}
			else
			{
				list.Add(new global::Tuple<float, float>(0f, 0f));
			}
		}
		list.Reverse();
		return list.ToArray();
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x0009F030 File Offset: 0x0009D230
	public float GetDataTimeLength()
	{
		float num = 0f;
		for (int i = this.dataPoints.Count - 1; i >= 0; i--)
		{
			num += this.dataPoints[i].periodEnd - this.dataPoints[i].periodStart;
		}
		return num;
	}

	// Token: 0x06001DDC RID: 7644
	public abstract void UpdateData();

	// Token: 0x06001DDD RID: 7645
	public abstract string FormatValueString(float value);

	// Token: 0x06001DDE RID: 7646 RVA: 0x0009F082 File Offset: 0x0009D282
	public float GetCurrentValue()
	{
		if (this.dataPoints.Count == 0)
		{
			return 0f;
		}
		return this.dataPoints[this.dataPoints.Count - 1].periodValue;
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x0009F0B4 File Offset: 0x0009D2B4
	public float GetMinValue(float sampleHistoryLengthSeconds)
	{
		float time = GameClock.Instance.GetTime();
		global::Tuple<float, float>[] array = this.ChartableData(sampleHistoryLengthSeconds);
		if (array.Length == 0)
		{
			return 0f;
		}
		if (array.Length == 1)
		{
			return array[0].second;
		}
		float num = array[array.Length - 1].second;
		int num2 = array.Length - 1;
		while (num2 >= 0 && time - array[num2].first <= sampleHistoryLengthSeconds)
		{
			num = Mathf.Min(num, array[num2].second);
			num2--;
		}
		return num;
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x0009F128 File Offset: 0x0009D328
	public float GetMaxValue(int sampleHistoryLengthSeconds)
	{
		float time = GameClock.Instance.GetTime();
		global::Tuple<float, float>[] array = this.ChartableData((float)sampleHistoryLengthSeconds);
		if (array.Length == 0)
		{
			return 0f;
		}
		if (array.Length == 1)
		{
			return array[0].second;
		}
		float num = array[array.Length - 1].second;
		int num2 = array.Length - 1;
		while (num2 >= 0 && time - array[num2].first <= (float)sampleHistoryLengthSeconds)
		{
			num = Mathf.Max(num, array[num2].second);
			num2--;
		}
		return num;
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x0009F1A0 File Offset: 0x0009D3A0
	public float GetAverageValue(float sampleHistoryLengthSeconds)
	{
		float time = GameClock.Instance.GetTime();
		global::Tuple<float, float>[] array = this.ChartableData(sampleHistoryLengthSeconds);
		float num = 0f;
		float num2 = 0f;
		for (int i = array.Length - 1; i >= 0; i--)
		{
			if (array[i].first >= time - sampleHistoryLengthSeconds)
			{
				float num3 = ((i == array.Length - 1) ? (time - array[i].first) : (array[i + 1].first - array[i].first));
				num2 += num3;
				if (!float.IsNaN(array[i].second))
				{
					num += num3 * array[i].second;
				}
			}
		}
		float num4;
		if (num2 == 0f)
		{
			if (array.Length == 0)
			{
				num4 = 0f;
			}
			else
			{
				num4 = array[array.Length - 1].second;
			}
		}
		else
		{
			num4 = num / num2;
		}
		return num4;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x0009F274 File Offset: 0x0009D474
	public float GetDelta(float secondsAgo)
	{
		float time = GameClock.Instance.GetTime();
		global::Tuple<float, float>[] array = this.ChartableData(secondsAgo);
		if (array.Length < 2)
		{
			return 0f;
		}
		float num = -1f;
		float second = array[array.Length - 1].second;
		for (int i = array.Length - 1; i >= 0; i--)
		{
			if (time - array[i].first >= secondsAgo)
			{
				num = array[i].second;
			}
		}
		return second - num;
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x0009F2E4 File Offset: 0x0009D4E4
	protected void AddPoint(float value)
	{
		if (float.IsNaN(value))
		{
			value = 0f;
		}
		this.dataPoints.Add(new DataPoint((this.dataPoints.Count == 0) ? GameClock.Instance.GetTime() : this.dataPoints[this.dataPoints.Count - 1].periodEnd, GameClock.Instance.GetTime(), value));
		int num = Math.Max(0, this.dataPoints.Count - this.maxPoints);
		this.dataPoints.RemoveRange(0, num);
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x0009F378 File Offset: 0x0009D578
	public List<DataPoint> GetCompressedData()
	{
		int num = 10;
		List<DataPoint> list = new List<DataPoint>();
		float num2 = (this.dataPoints[this.dataPoints.Count - 1].periodEnd - this.dataPoints[0].periodStart) / (float)num;
		for (int i = 0; i < num; i++)
		{
			float num3 = num2 * (float)i;
			float num4 = num3 + num2;
			float num5 = 0f;
			for (int j = 0; j < this.dataPoints.Count; j++)
			{
				DataPoint dataPoint = this.dataPoints[j];
				num5 += dataPoint.periodValue * Mathf.Max(0f, Mathf.Min(num4, dataPoint.periodEnd) - Mathf.Max(dataPoint.periodStart, num3));
			}
			list.Add(new DataPoint(num3, num4, num5 / (num4 - num3)));
		}
		return list;
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x0009F45B File Offset: 0x0009D65B
	public void OverwriteData(List<DataPoint> newData)
	{
		this.dataPoints = newData;
	}

	// Token: 0x040010B9 RID: 4281
	private const int standardSampleRate = 4;

	// Token: 0x040010BA RID: 4282
	private const int defaultCyclesTracked = 5;

	// Token: 0x040010BB RID: 4283
	protected List<DataPoint> dataPoints = new List<DataPoint>();

	// Token: 0x040010BC RID: 4284
	private int maxPoints = Mathf.CeilToInt(750f);
}
