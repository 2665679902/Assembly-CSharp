﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ClipperLib;
using KSerialization;
using UnityEngine;

namespace Delaunay.Geo
{
	// Token: 0x02000153 RID: 339
	[SerializationConfig(MemberSerialization.OptIn)]
	public sealed class Polygon
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002CD4D File Offset: 0x0002AF4D
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0002CD55 File Offset: 0x0002AF55
		public Rect bounds { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002CD5E File Offset: 0x0002AF5E
		public List<Vector2> Vertices
		{
			get
			{
				return this.vertices;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002CD66 File Offset: 0x0002AF66
		public Polygon()
		{
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002CD6E File Offset: 0x0002AF6E
		[OnDeserializing]
		internal void OnDeserializingMethod()
		{
			this.vertices = new List<Vector2>();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002CD7B File Offset: 0x0002AF7B
		[OnDeserialized]
		internal void OnDeserializedMethod()
		{
			this.Initialize();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002CD83 File Offset: 0x0002AF83
		public Polygon(List<Vector2> verts)
		{
			this.vertices = verts;
			this.Initialize();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002CD98 File Offset: 0x0002AF98
		public Polygon(Rect bounds)
		{
			this.vertices = new List<Vector2>();
			this.vertices.Add(new Vector2(bounds.x, bounds.y));
			this.vertices.Add(new Vector2(bounds.x + bounds.width, bounds.y));
			this.vertices.Add(new Vector2(bounds.x + bounds.width, bounds.y + bounds.height));
			this.vertices.Add(new Vector2(bounds.x, bounds.y + bounds.height));
			this.Initialize();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002CE54 File Offset: 0x0002B054
		public void Add(Vector2 newVert)
		{
			if (this.vertices == null)
			{
				this.vertices = new List<Vector2>();
			}
			this.vertices.Add(newVert);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002CE75 File Offset: 0x0002B075
		public void Initialize()
		{
			this.RefreshBounds();
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CE80 File Offset: 0x0002B080
		public void RefreshBounds()
		{
			global::Debug.Assert(this.vertices != null, "No verts added");
			Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
			Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
			for (int i = 0; i < this.vertices.Count; i++)
			{
				if (this.vertices[i].y < vector.y)
				{
					vector.y = this.vertices[i].y;
				}
				if (this.vertices[i].x < vector.x)
				{
					vector.x = this.vertices[i].x;
				}
				if (this.vertices[i].y > vector2.y)
				{
					vector2.y = this.vertices[i].y;
				}
				if (this.vertices[i].x > vector2.x)
				{
					vector2.x = this.vertices[i].x;
				}
			}
			this.bounds = Rect.MinMaxRect(vector.x, vector.y, vector2.x, vector2.y);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002CFC5 File Offset: 0x0002B1C5
		public float MinX
		{
			get
			{
				return this.vertices.Min((Vector2 point) => point.x);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002CFF1 File Offset: 0x0002B1F1
		public float MinY
		{
			get
			{
				return this.vertices.Min((Vector2 point) => point.y);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002D01D File Offset: 0x0002B21D
		public float MaxX
		{
			get
			{
				return this.vertices.Max((Vector2 point) => point.x);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002D049 File Offset: 0x0002B249
		public float MaxY
		{
			get
			{
				return this.vertices.Max((Vector2 point) => point.y);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D075 File Offset: 0x0002B275
		public float Area()
		{
			return Mathf.Abs(this.SignedDoubleArea() * 0.5f);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D088 File Offset: 0x0002B288
		public Winding Winding()
		{
			float num = this.SignedDoubleArea();
			if (num < 0f)
			{
				return Delaunay.Geo.Winding.CLOCKWISE;
			}
			if (num > 0f)
			{
				return Delaunay.Geo.Winding.COUNTERCLOCKWISE;
			}
			return Delaunay.Geo.Winding.NONE;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002D0B1 File Offset: 0x0002B2B1
		public void ForceWinding(Winding wind)
		{
			if (this.Winding() != wind)
			{
				this.vertices.Reverse();
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002D0C8 File Offset: 0x0002B2C8
		private float SignedDoubleArea()
		{
			int count = this.vertices.Count;
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				int num2 = (i + 1) % count;
				Vector2 vector = this.vertices[i];
				Vector2 vector2 = this.vertices[num2];
				num += vector.x * vector2.y - vector2.x * vector.y;
			}
			return num;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D13C File Offset: 0x0002B33C
		public Vector2 Centroid()
		{
			if (this.centroid == null)
			{
				this.centroid = new Vector2?(Vector2.zero);
				if (this.vertices.Count > 1)
				{
					float num = this.Area();
					int num2 = 1;
					for (int i = 0; i < this.vertices.Count; i++)
					{
						float num3 = this.vertices[i].x * this.vertices[num2].y - this.vertices[num2].x * this.vertices[i].y;
						Vector2? vector = this.centroid;
						Vector2 vector2 = new Vector2((this.vertices[i].x + this.vertices[num2].x) * num3, (this.vertices[i].y + this.vertices[num2].y) * num3);
						this.centroid = vector + vector2;
						num2 = (num2 + 1) % this.vertices.Count;
					}
					this.centroid /= 6f * num;
				}
			}
			return this.centroid.Value;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		public bool PointInPolygon(Vector2I point)
		{
			return this.PointInPolygon(new Vector2((float)point.x, (float)point.y));
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002D2E3 File Offset: 0x0002B4E3
		public bool Contains(Vector2 point)
		{
			return this.PointInPolygon(point);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D2EC File Offset: 0x0002B4EC
		public bool PointInPolygon(Vector2 point)
		{
			if (!this.bounds.Contains(point))
			{
				return false;
			}
			int num = this.vertices.Count - 1;
			bool flag = false;
			int i = 0;
			while (i < this.vertices.Count)
			{
				if (((this.vertices[i].y <= point.y && point.y < this.vertices[num].y) || (this.vertices[num].y <= point.y && point.y < this.vertices[i].y)) && point.x < (this.vertices[num].x - this.vertices[i].x) * (point.y - this.vertices[i].y) / (this.vertices[num].y - this.vertices[i].y) + this.vertices[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D421 File Offset: 0x0002B621
		public LineSegment GetEdge(int edgeIndex)
		{
			return new LineSegment(new Vector2?(this.vertices[edgeIndex]), new Vector2?(this.vertices[(edgeIndex + 1) % this.vertices.Count]));
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D458 File Offset: 0x0002B658
		public Polygon.Commonality SharesEdgeClosest(Polygon other)
		{
			Polygon.Commonality commonality = Polygon.Commonality.None;
			float num = 0f;
			MathUtil.Pair<Vector2, Vector2> closestEdge = this.GetClosestEdge(other.Centroid(), ref num);
			MathUtil.Pair<Vector2, Vector2> closestEdge2 = other.GetClosestEdge(this.Centroid(), ref num);
			if (Vector2.Distance(closestEdge.First, closestEdge2.First) >= 1E-05f && Vector2.Distance(closestEdge.First, closestEdge2.Second) >= 1E-05f)
			{
				return commonality;
			}
			if (Vector2.Distance(closestEdge.Second, closestEdge2.First) < 1E-05f || Vector2.Distance(closestEdge.Second, closestEdge2.Second) < 1E-05f)
			{
				return Polygon.Commonality.Edge;
			}
			return Polygon.Commonality.Point;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		public static void DebugLog(string message)
		{
			if (Polygon.DoDebugSpew)
			{
				global::Debug.Log(message);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D500 File Offset: 0x0002B700
		public Polygon.Commonality SharesEdge(Polygon other, ref int edgeIdx, out LineSegment overlapSegment)
		{
			Polygon.Commonality commonality = Polygon.Commonality.None;
			int num = this.vertices.Count - 1;
			int i = 0;
			while (i < this.vertices.Count)
			{
				Vector2 vector = this.vertices[num];
				Vector2 vector2 = this.vertices[i];
				Bounds bounds = new Bounds(vector, Vector3.zero);
				bounds.Encapsulate(vector2);
				int num2 = other.vertices.Count - 1;
				int j = 0;
				while (j < other.vertices.Count)
				{
					Vector2 vector3 = other.vertices[num2];
					Vector2 vector4 = other.vertices[j];
					if (0 + ((Vector2.Distance(vector4, vector2) < 0.001f) ? 1 : 0) + ((Vector2.Distance(vector4, vector) < 0.001f) ? 1 : 0) + ((Vector2.Distance(vector3, vector2) < 0.001f) ? 1 : 0) + ((Vector2.Distance(vector3, vector) < 0.001f) ? 1 : 0) == 1)
					{
						commonality = Polygon.Commonality.Point;
					}
					Bounds bounds2 = new Bounds(vector3, Vector3.zero);
					bounds2.Encapsulate(vector4);
					if (bounds.Intersects(bounds2))
					{
						float num3 = (vector2.x - vector.x) * (vector3.y - vector.y) - (vector3.x - vector.x) * (vector2.y - vector.y);
						float num4 = (vector4.x - vector3.x) * (vector.y - vector3.y) - (vector.x - vector3.x) * (vector4.y - vector3.y);
						if (Mathf.Abs(num3) < 0.001f && Mathf.Abs(num4) < 0.001f)
						{
							bool flag = vector.x < vector2.x || (vector.x == vector2.x && vector.y < vector2.y);
							Vector2 vector5 = (flag ? vector : vector2);
							Vector2 vector6 = (flag ? vector2 : vector);
							bool flag2 = vector3.x < vector4.x || (vector3.x == vector4.x && vector3.y < vector4.y);
							Vector2 vector7 = (flag2 ? vector3 : vector4);
							Vector2 vector8 = (flag2 ? vector4 : vector3);
							if (vector5.x >= vector7.x && (vector5.x != vector7.x || vector5.y >= vector7.y))
							{
								Vector2 vector9 = vector5;
								Vector2 vector10 = vector6;
								vector6 = vector8;
								vector7 = vector9;
								vector8 = vector10;
							}
							if (Vector2.Distance(vector6, vector7) < 0.001f)
							{
								commonality = Polygon.Commonality.Point;
							}
							else if (vector6.x - vector7.x > 0f || (vector6.x - vector7.x == 0f && vector6.y - vector7.y > 0f))
							{
								edgeIdx = num;
								Vector2 vector11;
								Vector2 vector12;
								if (vector6.x - vector8.x > 0f || (vector6.x - vector8.x == 0f && vector6.y - vector7.y > 0f))
								{
									vector11 = vector7;
									vector12 = vector6;
								}
								else
								{
									vector11 = vector7;
									vector12 = vector8;
								}
								overlapSegment = new LineSegment(new Vector2?(vector11), new Vector2?(vector12));
								return Polygon.Commonality.Edge;
							}
						}
					}
					num2 = j++;
				}
				num = i++;
			}
			overlapSegment = null;
			return commonality;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D894 File Offset: 0x0002BA94
		public float DistanceToClosestEdge(Vector2? point = null)
		{
			if (point == null)
			{
				point = new Vector2?(this.Centroid());
			}
			float num = 0f;
			MathUtil.Pair<Vector2, Vector2> closestEdge = this.GetClosestEdge(point.Value, ref num);
			Vector2 vector = closestEdge.Second - closestEdge.First;
			return Vector2.Distance(closestEdge.First + vector * num, point.Value);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D900 File Offset: 0x0002BB00
		public MathUtil.Pair<Vector2, Vector2> GetClosestEdge(Vector2 point, ref float timeOnEdge)
		{
			MathUtil.Pair<Vector2, Vector2> pair = null;
			float num = 0f;
			timeOnEdge = 0f;
			float num2 = float.MaxValue;
			int num3 = this.vertices.Count - 1;
			int i = 0;
			while (i < this.vertices.Count)
			{
				MathUtil.Pair<Vector2, Vector2> pair2 = new MathUtil.Pair<Vector2, Vector2>(this.vertices[num3], this.vertices[i]);
				float num4 = Mathf.Abs(MathUtil.GetClosestPointBetweenPointAndLineSegment(pair2, point, ref num));
				if (num4 < num2)
				{
					num2 = num4;
					pair = pair2;
					timeOnEdge = num;
				}
				num3 = i++;
			}
			return pair;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002D98C File Offset: 0x0002BB8C
		public List<KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>>> GetEdgesWithinDistance(Vector2 point, float distance = 3.4028235E+38f)
		{
			List<KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>>> list = new List<KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>>>();
			float num = 0f;
			int num2 = this.vertices.Count - 1;
			int i = 0;
			while (i < this.vertices.Count)
			{
				MathUtil.Pair<Vector2, Vector2> pair = new MathUtil.Pair<Vector2, Vector2>(this.vertices[num2], this.vertices[i]);
				MathUtil.Pair<float, float> pair2 = new MathUtil.Pair<float, float>();
				float num3 = Mathf.Abs(MathUtil.GetClosestPointBetweenPointAndLineSegment(pair, point, ref num));
				if (num3 < distance)
				{
					pair2.First = num3;
					pair2.Second = num;
					list.Add(new KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>>(pair2, pair));
				}
				num2 = i++;
			}
			list.Sort((KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>> a, KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>> b) => a.Key.First.CompareTo(b.Key.First));
			return list;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002DA50 File Offset: 0x0002BC50
		public bool IsConvex()
		{
			if (this.vertices.Count < 4)
			{
				return true;
			}
			bool flag = false;
			int count = this.vertices.Count;
			for (int i = 0; i < count; i++)
			{
				double num = (double)(this.vertices[(i + 2) % count].x - this.vertices[(i + 1) % count].x);
				double num2 = (double)(this.vertices[(i + 2) % count].y - this.vertices[(i + 1) % count].y);
				double num3 = (double)(this.vertices[i].x - this.vertices[(i + 1) % count].x);
				double num4 = (double)(this.vertices[i].y - this.vertices[(i + 1) % count].y);
				double num5 = num * num4 - num2 * num3;
				if (i == 0)
				{
					flag = num5 > 0.0;
				}
				else if (flag != num5 > 0.0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		private List<IntPoint> GetPath()
		{
			List<IntPoint> list = new List<IntPoint>();
			for (int i = 0; i < this.vertices.Count; i++)
			{
				list.Add(new IntPoint((double)(this.vertices[i].x * 10000f), (double)(this.vertices[i].y * 10000f)));
			}
			return list;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		public Polygon Clip(Polygon clippingPoly, ClipType type = ClipType.ctIntersection)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Add(this.GetPath());
			List<List<IntPoint>> list2 = new List<List<IntPoint>>();
			list2.Add(clippingPoly.GetPath());
			Clipper clipper = new Clipper(0);
			PolyTree polyTree = new PolyTree();
			clipper.AddPaths(list, PolyType.ptSubject, true);
			clipper.AddPaths(list2, PolyType.ptClip, true);
			clipper.Execute(type, polyTree, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);
			List<List<IntPoint>> list3 = Clipper.PolyTreeToPaths(polyTree);
			if (list3.Count > 0)
			{
				List<Vector2> list4 = new List<Vector2>();
				for (int i = 0; i < list3[0].Count; i++)
				{
					list4.Add(new Vector2((float)list3[0][i].X * 0.0001f, (float)list3[0][i].Y * 0.0001f));
				}
				return new Polygon(list4);
			}
			return null;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002DCAC File Offset: 0x0002BEAC
		private int CrossingNumber(Vector2 point)
		{
			int num = 0;
			for (int i = 0; i < this.vertices.Count; i++)
			{
				int num2 = i;
				int num3 = ((i < this.vertices.Count - 1) ? (i + 1) : 0);
				if ((this.vertices[num2].y <= point.y && this.vertices[num3].y > point.y) || (this.vertices[num2].y > point.y && this.vertices[num3].y <= point.y))
				{
					float num4 = (point.y - this.vertices[num2].y) / (this.vertices[num3].y - this.vertices[num2].y);
					if (point.x < this.vertices[num2].x + num4 * (this.vertices[num3].x - this.vertices[num2].x))
					{
						num++;
					}
				}
			}
			return num & 1;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002DDE0 File Offset: 0x0002BFE0
		private float perp(Vector2 u, Vector2 v)
		{
			return u.x * v.y - u.y * v.x;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002DE00 File Offset: 0x0002C000
		public bool ClipSegment(LineSegment segment, ref LineSegment intersectingSegment)
		{
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			return this.ClipSegment(segment, ref intersectingSegment, ref zero, ref zero2);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002DE28 File Offset: 0x0002C028
		public bool ClipSegment(LineSegment segment, ref LineSegment intersectingSegment, ref Vector2 normNear, ref Vector2 normFar)
		{
			normNear = Vector2.zero;
			normFar = Vector2.zero;
			if (segment.p0 == segment.p1)
			{
				intersectingSegment = segment;
				return this.CrossingNumber(segment.p0.Value) == 1;
			}
			float num = 0f;
			float num2 = 1f;
			Vector2 vector = segment.Direction();
			for (int i = 0; i < this.vertices.Count; i++)
			{
				int num3 = i;
				int num4 = ((i < this.vertices.Count - 1) ? (i + 1) : 0);
				Vector2 vector2 = this.vertices[num4] - this.vertices[num3];
				Vector2 vector3 = new Vector2(vector2.y, -vector2.x);
				float num5 = this.perp(vector2, segment.p0.Value - this.vertices[num3]);
				float num6 = -this.perp(vector2, vector);
				if (Mathf.Abs(num6) < Mathf.Epsilon)
				{
					if (num5 < 0f)
					{
						return false;
					}
				}
				else
				{
					float num7 = num5 / num6;
					if (num6 < 0f)
					{
						if (num7 > num)
						{
							num = num7;
							normNear = vector3;
							if (num > num2)
							{
								return false;
							}
						}
					}
					else if (num7 < num2)
					{
						num2 = num7;
						normFar = vector3;
						if (num2 < num)
						{
							return false;
						}
					}
				}
			}
			intersectingSegment.p0 = segment.p0 + num * vector;
			intersectingSegment.p1 = segment.p0 + num2 * vector;
			normFar.Normalize();
			normNear.Normalize();
			return true;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002E044 File Offset: 0x0002C244
		public bool ClipSegmentSAT(LineSegment segment, ref LineSegment intersectingSegment, ref Vector2 normNear, ref Vector2 normFar)
		{
			normNear = Vector2.zero;
			normFar = Vector2.zero;
			float num = 0f;
			float num2 = 1f;
			Vector2 vector = segment.Direction();
			for (int i = 0; i < this.vertices.Count; i++)
			{
				Vector2 vector2 = this.vertices[i];
				Vector2 vector3 = this.vertices[(i < this.vertices.Count - 1) ? (i + 1) : 0] - vector2;
				Vector2 vector4 = new Vector2(vector3.y, -vector3.x);
				Vector2 vector5 = vector2 - segment.p0.Value;
				float num3 = this.perp(vector5, vector4);
				float num4 = this.perp(vector, vector4);
				if (Mathf.Abs(num4) < Mathf.Epsilon)
				{
					if (num3 < 0f)
					{
						return false;
					}
				}
				else
				{
					float num5 = num3 / num4;
					if (num4 < 0f)
					{
						if (num5 > num2)
						{
							return false;
						}
						if (num5 > num)
						{
							num = num5;
							normNear = vector4;
						}
					}
					else
					{
						if (num5 < num)
						{
							return false;
						}
						if (num5 < num2)
						{
							num2 = num5;
							normFar = vector4;
						}
					}
				}
			}
			intersectingSegment.p0 = segment.p0 + num * vector;
			intersectingSegment.p1 = segment.p0 + num2 * vector;
			normFar.Normalize();
			normNear.Normalize();
			return true;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
		public void DebugDraw(Color colour, Vector2 offset, bool drawCentroid = false, float duration = 1f, float inset = 0f)
		{
			Vector2 vector = this.Centroid();
			for (int i = 0; i < this.vertices.Count; i++)
			{
				Vector2 vector2 = this.vertices[i];
				Vector2 vector3 = this.vertices[(i < this.vertices.Count - 1) ? (i + 1) : 0];
				if (inset != 0f)
				{
					(vector2 - vector).normalized * -inset;
					(vector3 - vector).normalized * -inset;
				}
			}
		}

		// Token: 0x04000740 RID: 1856
		[Serialize]
		private List<Vector2> vertices;

		// Token: 0x04000741 RID: 1857
		private Vector2? centroid;

		// Token: 0x04000742 RID: 1858
		public static bool DoDebugSpew;

		// Token: 0x04000743 RID: 1859
		private const int CLIPPER_INTEGER_SCALE = 10000;

		// Token: 0x04000744 RID: 1860
		private const float CLIPPER_INVERSE_SCALE = 0.0001f;

		// Token: 0x02000A16 RID: 2582
		public enum Commonality
		{
			// Token: 0x0400228D RID: 8845
			None,
			// Token: 0x0400228E RID: 8846
			Point,
			// Token: 0x0400228F RID: 8847
			Edge
		}
	}
}
