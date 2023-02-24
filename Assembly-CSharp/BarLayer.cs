using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AA8 RID: 2728
public class BarLayer : GraphLayer
{
	// Token: 0x17000632 RID: 1586
	// (get) Token: 0x060053A8 RID: 21416 RVA: 0x001E5F13 File Offset: 0x001E4113
	public int bar_count
	{
		get
		{
			return this.bars.Count;
		}
	}

	// Token: 0x060053A9 RID: 21417 RVA: 0x001E5F20 File Offset: 0x001E4120
	public void NewBar(int[] values, float x_position, string ID = "")
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab_bar, this.bar_container, true);
		if (ID == "")
		{
			ID = this.bars.Count.ToString();
		}
		gameObject.name = string.Format("bar_{0}", ID);
		GraphedBar component = gameObject.GetComponent<GraphedBar>();
		component.SetFormat(this.bar_formats[this.bars.Count % this.bar_formats.Length]);
		int[] array = new int[values.Length];
		for (int i = 0; i < values.Length; i++)
		{
			array[i] = (int)(base.graph.rectTransform().rect.height * base.graph.GetRelativeSize(new Vector2(0f, (float)values[i])).y);
		}
		component.SetValues(array, base.graph.GetRelativePosition(new Vector2(x_position, 0f)).x);
		this.bars.Add(component);
	}

	// Token: 0x060053AA RID: 21418 RVA: 0x001E6020 File Offset: 0x001E4220
	public void ClearBars()
	{
		foreach (GraphedBar graphedBar in this.bars)
		{
			if (graphedBar != null && graphedBar.gameObject != null)
			{
				UnityEngine.Object.DestroyImmediate(graphedBar.gameObject);
			}
		}
		this.bars.Clear();
	}

	// Token: 0x040038CA RID: 14538
	public GameObject bar_container;

	// Token: 0x040038CB RID: 14539
	public GameObject prefab_bar;

	// Token: 0x040038CC RID: 14540
	public GraphedBarFormatting[] bar_formats;

	// Token: 0x040038CD RID: 14541
	private List<GraphedBar> bars = new List<GraphedBar>();
}
