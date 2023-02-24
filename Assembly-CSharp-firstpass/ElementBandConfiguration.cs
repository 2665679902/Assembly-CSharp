using System;
using System.Collections.Generic;

// Token: 0x02000128 RID: 296
[Serializable]
public class ElementBandConfiguration : List<ElementGradient>
{
	// Token: 0x06000A3A RID: 2618 RVA: 0x0002729A File Offset: 0x0002549A
	public ElementBandConfiguration()
	{
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x000272A2 File Offset: 0x000254A2
	public ElementBandConfiguration(int size)
		: base(size)
	{
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x000272AB File Offset: 0x000254AB
	public ElementBandConfiguration(IEnumerable<ElementGradient> collection)
		: base(collection)
	{
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x000272B4 File Offset: 0x000254B4
	public List<float> ConvertBandSizeToMaxSize()
	{
		List<float> list = new List<float>();
		float num = 0f;
		for (int i = 0; i < base.Count; i++)
		{
			ElementGradient elementGradient = base[i];
			num += elementGradient.bandSize;
		}
		float num2 = 0f;
		for (int j = 0; j < base.Count; j++)
		{
			ElementGradient elementGradient2 = base[j];
			elementGradient2.maxValue = num2 + elementGradient2.bandSize / num;
			num2 = elementGradient2.maxValue;
			list.Add(elementGradient2.maxValue);
		}
		return list;
	}
}
