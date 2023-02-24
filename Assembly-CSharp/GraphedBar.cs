using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AA9 RID: 2729
[AddComponentMenu("KMonoBehaviour/scripts/GraphedBar")]
[Serializable]
public class GraphedBar : KMonoBehaviour
{
	// Token: 0x060053AC RID: 21420 RVA: 0x001E60AF File Offset: 0x001E42AF
	public void SetFormat(GraphedBarFormatting format)
	{
		this.format = format;
	}

	// Token: 0x060053AD RID: 21421 RVA: 0x001E60B8 File Offset: 0x001E42B8
	public void SetValues(int[] values, float x_position)
	{
		this.ClearValues();
		base.gameObject.rectTransform().anchorMin = new Vector2(x_position, 0f);
		base.gameObject.rectTransform().anchorMax = new Vector2(x_position, 1f);
		base.gameObject.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)this.format.width);
		for (int i = 0; i < values.Length; i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.prefab_segment, this.segments_container, true);
			LayoutElement component = gameObject.GetComponent<LayoutElement>();
			component.preferredHeight = (float)values[i];
			component.minWidth = (float)this.format.width;
			gameObject.GetComponent<Image>().color = this.format.colors[i % this.format.colors.Length];
			this.segments.Add(gameObject);
		}
	}

	// Token: 0x060053AE RID: 21422 RVA: 0x001E6198 File Offset: 0x001E4398
	public void ClearValues()
	{
		foreach (GameObject gameObject in this.segments)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		this.segments.Clear();
	}

	// Token: 0x040038CE RID: 14542
	public GameObject segments_container;

	// Token: 0x040038CF RID: 14543
	public GameObject prefab_segment;

	// Token: 0x040038D0 RID: 14544
	private List<GameObject> segments = new List<GameObject>();

	// Token: 0x040038D1 RID: 14545
	private GraphedBarFormatting format;
}
