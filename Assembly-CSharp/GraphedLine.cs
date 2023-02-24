using System;
using UnityEngine;
using UnityEngine.UI.Extensions;

// Token: 0x02000AAC RID: 2732
[AddComponentMenu("KMonoBehaviour/scripts/GraphedLine")]
[Serializable]
public class GraphedLine : KMonoBehaviour
{
	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x060053B2 RID: 21426 RVA: 0x001E6231 File Offset: 0x001E4431
	public int PointCount
	{
		get
		{
			return this.points.Length;
		}
	}

	// Token: 0x060053B3 RID: 21427 RVA: 0x001E623B File Offset: 0x001E443B
	public void SetPoints(Vector2[] points)
	{
		this.points = points;
		this.UpdatePoints();
	}

	// Token: 0x060053B4 RID: 21428 RVA: 0x001E624C File Offset: 0x001E444C
	private void UpdatePoints()
	{
		Vector2[] array = new Vector2[this.points.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.layer.graph.GetRelativePosition(this.points[i]);
		}
		this.line_renderer.Points = array;
	}

	// Token: 0x060053B5 RID: 21429 RVA: 0x001E62A4 File Offset: 0x001E44A4
	public Vector2 GetClosestDataToPointOnXAxis(Vector2 toPoint)
	{
		float num = toPoint.x / this.layer.graph.rectTransform().sizeDelta.x;
		float num2 = this.layer.graph.axis_x.min_value + this.layer.graph.axis_x.range * num;
		Vector2 vector = Vector2.zero;
		foreach (Vector2 vector2 in this.points)
		{
			if (Mathf.Abs(vector2.x - num2) < Mathf.Abs(vector.x - num2))
			{
				vector = vector2;
			}
		}
		return vector;
	}

	// Token: 0x060053B6 RID: 21430 RVA: 0x001E634B File Offset: 0x001E454B
	public void HidePointHighlight()
	{
		if (this.highlightPoint != null)
		{
			this.highlightPoint.SetActive(false);
		}
	}

	// Token: 0x060053B7 RID: 21431 RVA: 0x001E6368 File Offset: 0x001E4568
	public void SetPointHighlight(Vector2 point)
	{
		if (this.highlightPoint == null)
		{
			return;
		}
		this.highlightPoint.SetActive(true);
		Vector2 relativePosition = this.layer.graph.GetRelativePosition(point);
		this.highlightPoint.rectTransform().SetLocalPosition(new Vector2(relativePosition.x * this.layer.graph.rectTransform().sizeDelta.x - this.layer.graph.rectTransform().sizeDelta.x / 2f, relativePosition.y * this.layer.graph.rectTransform().sizeDelta.y - this.layer.graph.rectTransform().sizeDelta.y / 2f));
		ToolTip component = this.layer.graph.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		component.tooltipPositionOffset = new Vector2(this.highlightPoint.rectTransform().localPosition.x, this.layer.graph.rectTransform().rect.height / 2f - 12f);
		component.SetSimpleTooltip(string.Concat(new string[]
		{
			this.layer.graph.axis_x.name,
			" ",
			point.x.ToString(),
			", ",
			Mathf.RoundToInt(point.y).ToString(),
			" ",
			this.layer.graph.axis_y.name
		}));
		ToolTipScreen.Instance.SetToolTip(component);
	}

	// Token: 0x040038D5 RID: 14549
	public UILineRenderer line_renderer;

	// Token: 0x040038D6 RID: 14550
	public LineLayer layer;

	// Token: 0x040038D7 RID: 14551
	private Vector2[] points = new Vector2[0];

	// Token: 0x040038D8 RID: 14552
	[SerializeField]
	private GameObject highlightPoint;
}
