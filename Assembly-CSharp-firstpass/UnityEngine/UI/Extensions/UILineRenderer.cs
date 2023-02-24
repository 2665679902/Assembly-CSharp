using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000244 RID: 580
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Extensions/Primitives/UILineRenderer")]
	public class UILineRenderer : UIPrimitiveBase
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000450DD File Offset: 0x000432DD
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x000450E5 File Offset: 0x000432E5
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00045103 File Offset: 0x00043303
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0004510B File Offset: 0x0004330B
		public Vector2[] Points
		{
			get
			{
				return this.m_points;
			}
			set
			{
				if (this.m_points == value)
				{
					return;
				}
				this.m_points = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00045124 File Offset: 0x00043324
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (this.m_points == null)
			{
				return;
			}
			Vector2[] array = this.m_points;
			if (this.BezierMode != UILineRenderer.BezierType.None && this.m_points.Length > 3)
			{
				BezierPath bezierPath = new BezierPath();
				bezierPath.SetControlPoints(array);
				bezierPath.SegmentsPerCurve = this.BezierSegmentsPerCurve;
				UILineRenderer.BezierType bezierMode = this.BezierMode;
				List<Vector2> list;
				if (bezierMode != UILineRenderer.BezierType.Basic)
				{
					if (bezierMode != UILineRenderer.BezierType.Improved)
					{
						list = bezierPath.GetDrawingPoints2();
					}
					else
					{
						list = bezierPath.GetDrawingPoints1();
					}
				}
				else
				{
					list = bezierPath.GetDrawingPoints0();
				}
				array = list.ToArray();
			}
			float num = base.rectTransform.rect.width;
			float num2 = base.rectTransform.rect.height;
			float num3 = -base.rectTransform.pivot.x * base.rectTransform.rect.width;
			float num4 = -base.rectTransform.pivot.y * base.rectTransform.rect.height;
			if (!this.relativeSize)
			{
				num = 1f;
				num2 = 1f;
			}
			if (this.UseMargins)
			{
				num -= this.Margin.x;
				num2 -= this.Margin.y;
				num3 += this.Margin.x / 2f;
				num4 += this.Margin.y / 2f;
			}
			vh.Clear();
			List<UIVertex[]> list2 = new List<UIVertex[]>();
			if (this.LineList)
			{
				for (int i = 1; i < array.Length; i += 2)
				{
					Vector2 vector = array[i - 1];
					Vector2 vector2 = array[i];
					vector = new Vector2(vector.x * num + num3, vector.y * num2 + num4);
					vector2 = new Vector2(vector2.x * num + num3, vector2.y * num2 + num4);
					if (this.LineCaps)
					{
						list2.Add(this.CreateLineCap(vector, vector2, UILineRenderer.SegmentType.Start));
					}
					list2.Add(this.CreateLineSegment(vector, vector2, UILineRenderer.SegmentType.Middle));
					if (this.LineCaps)
					{
						list2.Add(this.CreateLineCap(vector, vector2, UILineRenderer.SegmentType.End));
					}
				}
			}
			else
			{
				for (int j = 1; j < array.Length; j++)
				{
					Vector2 vector3 = array[j - 1];
					Vector2 vector4 = array[j];
					vector3 = new Vector2(vector3.x * num + num3, vector3.y * num2 + num4);
					vector4 = new Vector2(vector4.x * num + num3, vector4.y * num2 + num4);
					if (this.LineCaps && j == 1)
					{
						list2.Add(this.CreateLineCap(vector3, vector4, UILineRenderer.SegmentType.Start));
					}
					list2.Add(this.CreateLineSegment(vector3, vector4, UILineRenderer.SegmentType.Middle));
					if (this.LineCaps && j == array.Length - 1)
					{
						list2.Add(this.CreateLineCap(vector3, vector4, UILineRenderer.SegmentType.End));
					}
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				if (!this.LineList && k < list2.Count - 1)
				{
					Vector3 vector5 = list2[k][1].position - list2[k][2].position;
					Vector3 vector6 = list2[k + 1][2].position - list2[k + 1][1].position;
					float num5 = Vector2.Angle(vector5, vector6) * 0.017453292f;
					float num6 = Mathf.Sign(Vector3.Cross(vector5.normalized, vector6.normalized).z);
					float num7 = this.LineThickness / (2f * Mathf.Tan(num5 / 2f));
					Vector3 vector7 = list2[k][2].position - vector5.normalized * num7 * num6;
					Vector3 vector8 = list2[k][3].position + vector5.normalized * num7 * num6;
					UILineRenderer.JoinType joinType = this.LineJoins;
					if (joinType == UILineRenderer.JoinType.Miter)
					{
						if (num7 < vector5.magnitude / 2f && num7 < vector6.magnitude / 2f && num5 > 0.2617994f)
						{
							list2[k][2].position = vector7;
							list2[k][3].position = vector8;
							list2[k + 1][0].position = vector8;
							list2[k + 1][1].position = vector7;
						}
						else
						{
							joinType = UILineRenderer.JoinType.Bevel;
						}
					}
					if (joinType == UILineRenderer.JoinType.Bevel)
					{
						if (num7 < vector5.magnitude / 2f && num7 < vector6.magnitude / 2f && num5 > 0.5235988f)
						{
							if (num6 < 0f)
							{
								list2[k][2].position = vector7;
								list2[k + 1][1].position = vector7;
							}
							else
							{
								list2[k][3].position = vector8;
								list2[k + 1][0].position = vector8;
							}
						}
						UIVertex[] array2 = new UIVertex[]
						{
							list2[k][2],
							list2[k][3],
							list2[k + 1][0],
							list2[k + 1][1]
						};
						vh.AddUIVertexQuad(array2);
					}
				}
				vh.AddUIVertexQuad(list2[k]);
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00045714 File Offset: 0x00043914
		private UIVertex[] CreateLineCap(Vector2 start, Vector2 end, UILineRenderer.SegmentType type)
		{
			if (type == UILineRenderer.SegmentType.Start)
			{
				Vector2 vector = start - (end - start).normalized * this.LineThickness / 2f;
				return this.CreateLineSegment(vector, start, UILineRenderer.SegmentType.Start);
			}
			if (type == UILineRenderer.SegmentType.End)
			{
				Vector2 vector2 = end + (end - start).normalized * this.LineThickness / 2f;
				return this.CreateLineSegment(end, vector2, UILineRenderer.SegmentType.End);
			}
			Debug.LogError("Bad SegmentType passed in to CreateLineCap. Must be SegmentType.Start or SegmentType.End");
			return null;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000457A0 File Offset: 0x000439A0
		private UIVertex[] CreateLineSegment(Vector2 start, Vector2 end, UILineRenderer.SegmentType type)
		{
			Vector2[] array = UILineRenderer.middleUvs;
			if (type == UILineRenderer.SegmentType.Start)
			{
				array = UILineRenderer.startUvs;
			}
			else if (type == UILineRenderer.SegmentType.End)
			{
				array = UILineRenderer.endUvs;
			}
			Vector2 vector = new Vector2(start.y - end.y, end.x - start.x).normalized * this.LineThickness / 2f;
			Vector2 vector2 = start - vector;
			Vector2 vector3 = start + vector;
			Vector2 vector4 = end + vector;
			Vector2 vector5 = end - vector;
			return base.SetVbo(new Vector2[] { vector2, vector3, vector4, vector5 }, array);
		}

		// Token: 0x04000948 RID: 2376
		private const float MIN_MITER_JOIN = 0.2617994f;

		// Token: 0x04000949 RID: 2377
		private const float MIN_BEVEL_NICE_JOIN = 0.5235988f;

		// Token: 0x0400094A RID: 2378
		private static readonly Vector2 UV_TOP_LEFT = Vector2.zero;

		// Token: 0x0400094B RID: 2379
		private static readonly Vector2 UV_BOTTOM_LEFT = new Vector2(0f, 1f);

		// Token: 0x0400094C RID: 2380
		private static readonly Vector2 UV_TOP_CENTER = new Vector2(0.5f, 0f);

		// Token: 0x0400094D RID: 2381
		private static readonly Vector2 UV_BOTTOM_CENTER = new Vector2(0.5f, 1f);

		// Token: 0x0400094E RID: 2382
		private static readonly Vector2 UV_TOP_RIGHT = new Vector2(1f, 0f);

		// Token: 0x0400094F RID: 2383
		private static readonly Vector2 UV_BOTTOM_RIGHT = new Vector2(1f, 1f);

		// Token: 0x04000950 RID: 2384
		private static readonly Vector2[] startUvs = new Vector2[]
		{
			UILineRenderer.UV_TOP_LEFT,
			UILineRenderer.UV_BOTTOM_LEFT,
			UILineRenderer.UV_BOTTOM_CENTER,
			UILineRenderer.UV_TOP_CENTER
		};

		// Token: 0x04000951 RID: 2385
		private static readonly Vector2[] middleUvs = new Vector2[]
		{
			UILineRenderer.UV_TOP_CENTER,
			UILineRenderer.UV_BOTTOM_CENTER,
			UILineRenderer.UV_BOTTOM_CENTER,
			UILineRenderer.UV_TOP_CENTER
		};

		// Token: 0x04000952 RID: 2386
		private static readonly Vector2[] endUvs = new Vector2[]
		{
			UILineRenderer.UV_TOP_CENTER,
			UILineRenderer.UV_BOTTOM_CENTER,
			UILineRenderer.UV_BOTTOM_RIGHT,
			UILineRenderer.UV_TOP_RIGHT
		};

		// Token: 0x04000953 RID: 2387
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000954 RID: 2388
		[SerializeField]
		private Vector2[] m_points;

		// Token: 0x04000955 RID: 2389
		public float LineThickness = 2f;

		// Token: 0x04000956 RID: 2390
		public bool UseMargins;

		// Token: 0x04000957 RID: 2391
		public Vector2 Margin;

		// Token: 0x04000958 RID: 2392
		public bool relativeSize;

		// Token: 0x04000959 RID: 2393
		public bool LineList;

		// Token: 0x0400095A RID: 2394
		public bool LineCaps;

		// Token: 0x0400095B RID: 2395
		public UILineRenderer.JoinType LineJoins;

		// Token: 0x0400095C RID: 2396
		public UILineRenderer.BezierType BezierMode;

		// Token: 0x0400095D RID: 2397
		public int BezierSegmentsPerCurve = 10;

		// Token: 0x02000A6C RID: 2668
		private enum SegmentType
		{
			// Token: 0x0400237D RID: 9085
			Start,
			// Token: 0x0400237E RID: 9086
			Middle,
			// Token: 0x0400237F RID: 9087
			End
		}

		// Token: 0x02000A6D RID: 2669
		public enum JoinType
		{
			// Token: 0x04002381 RID: 9089
			Bevel,
			// Token: 0x04002382 RID: 9090
			Miter
		}

		// Token: 0x02000A6E RID: 2670
		public enum BezierType
		{
			// Token: 0x04002384 RID: 9092
			None,
			// Token: 0x04002385 RID: 9093
			Quick,
			// Token: 0x04002386 RID: 9094
			Basic,
			// Token: 0x04002387 RID: 9095
			Improved
		}
	}
}
