using System;
using System.Collections.Generic;

// Token: 0x0200012A RID: 298
public class LevelLayer : List<LayerGradient>, IMerge<LevelLayer>
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x00027356 File Offset: 0x00025556
	public LevelLayer()
	{
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0002735E File Offset: 0x0002555E
	public LevelLayer(int size)
		: base(size)
	{
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00027367 File Offset: 0x00025567
	public LevelLayer(IEnumerable<LayerGradient> collection)
		: base(collection)
	{
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x00027370 File Offset: 0x00025570
	public void ConvertBandSizeToMaxSize()
	{
		base.Sort((LayerGradient a, LayerGradient b) => Math.Sign(a.bandSize - b.bandSize));
		float num = 0f;
		for (int i = 0; i < base.Count; i++)
		{
			LayerGradient layerGradient = base[i];
			num += layerGradient.bandSize;
		}
		float num2 = 0f;
		for (int j = 0; j < base.Count; j++)
		{
			LayerGradient layerGradient2 = base[j];
			layerGradient2.maxValue = num2 + layerGradient2.bandSize / num;
			num2 = layerGradient2.maxValue;
		}
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00027409 File Offset: 0x00025609
	public LevelLayer Merge(LevelLayer other)
	{
		base.AddRange(other);
		return this;
	}
}
