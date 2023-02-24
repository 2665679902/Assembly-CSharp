using System;
using System.Collections.Generic;
using System.Linq;
using ProcGen;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

// Token: 0x02000A53 RID: 2643
public class ClusterMapPath : MonoBehaviour
{
	// Token: 0x06005045 RID: 20549 RVA: 0x001CBC54 File Offset: 0x001C9E54
	public void Init()
	{
		this.lineRenderer = base.gameObject.GetComponentInChildren<UILineRenderer>();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005046 RID: 20550 RVA: 0x001CBC73 File Offset: 0x001C9E73
	public void Init(List<Vector2> nodes, Color color)
	{
		this.m_nodes = nodes;
		this.m_color = color;
		this.lineRenderer = base.gameObject.GetComponentInChildren<UILineRenderer>();
		this.UpdateColor();
		this.UpdateRenderer();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005047 RID: 20551 RVA: 0x001CBCAC File Offset: 0x001C9EAC
	public void SetColor(Color color)
	{
		this.m_color = color;
		this.UpdateColor();
	}

	// Token: 0x06005048 RID: 20552 RVA: 0x001CBCBB File Offset: 0x001C9EBB
	private void UpdateColor()
	{
		this.lineRenderer.color = this.m_color;
		this.pathStart.color = this.m_color;
		this.pathEnd.color = this.m_color;
	}

	// Token: 0x06005049 RID: 20553 RVA: 0x001CBCF0 File Offset: 0x001C9EF0
	public void SetPoints(List<Vector2> points)
	{
		this.m_nodes = points;
		this.UpdateRenderer();
	}

	// Token: 0x0600504A RID: 20554 RVA: 0x001CBD00 File Offset: 0x001C9F00
	private void UpdateRenderer()
	{
		HashSet<Vector2> pointsOnCatmullRomSpline = ProcGen.Util.GetPointsOnCatmullRomSpline(this.m_nodes, 10);
		this.lineRenderer.Points = pointsOnCatmullRomSpline.ToArray<Vector2>();
		if (this.lineRenderer.Points.Length > 1)
		{
			this.pathStart.transform.localPosition = this.lineRenderer.Points[0];
			this.pathStart.gameObject.SetActive(true);
			Vector2 vector = this.lineRenderer.Points[this.lineRenderer.Points.Length - 1];
			Vector2 vector2 = this.lineRenderer.Points[this.lineRenderer.Points.Length - 2];
			this.pathEnd.transform.localPosition = vector;
			Vector2 vector3 = vector - vector2;
			this.pathEnd.transform.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
			this.pathEnd.gameObject.SetActive(true);
			return;
		}
		this.pathStart.gameObject.SetActive(false);
		this.pathEnd.gameObject.SetActive(false);
	}

	// Token: 0x0600504B RID: 20555 RVA: 0x001CBE28 File Offset: 0x001CA028
	public float GetRotationForNextSegment()
	{
		if (this.m_nodes.Count > 1)
		{
			Vector2 vector = this.m_nodes[0];
			Vector2 vector2 = this.m_nodes[1] - vector;
			return Vector2.SignedAngle(Vector2.up, vector2);
		}
		return 0f;
	}

	// Token: 0x040035EF RID: 13807
	private List<Vector2> m_nodes;

	// Token: 0x040035F0 RID: 13808
	private Color m_color;

	// Token: 0x040035F1 RID: 13809
	public UILineRenderer lineRenderer;

	// Token: 0x040035F2 RID: 13810
	public Image pathStart;

	// Token: 0x040035F3 RID: 13811
	public Image pathEnd;
}
