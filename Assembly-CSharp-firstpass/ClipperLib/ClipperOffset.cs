using System;
using System.Collections.Generic;

namespace ClipperLib
{
	// Token: 0x0200016E RID: 366
	public class ClipperOffset
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00035040 File Offset: 0x00033240
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00035048 File Offset: 0x00033248
		public double ArcTolerance { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00035051 File Offset: 0x00033251
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00035059 File Offset: 0x00033259
		public double MiterLimit { get; set; }

		// Token: 0x06000C57 RID: 3159 RVA: 0x00035062 File Offset: 0x00033262
		public ClipperOffset(double miterLimit = 2.0, double arcTolerance = 0.25)
		{
			this.MiterLimit = miterLimit;
			this.ArcTolerance = arcTolerance;
			this.m_lowest.X = -1L;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0003509B File Offset: 0x0003329B
		public void Clear()
		{
			this.m_polyNodes.Childs.Clear();
			this.m_lowest.X = -1L;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000350BA File Offset: 0x000332BA
		internal static long Round(double value)
		{
			if (value >= 0.0)
			{
				return (long)(value + 0.5);
			}
			return (long)(value - 0.5);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000350E4 File Offset: 0x000332E4
		public void AddPath(List<IntPoint> path, JoinType joinType, EndType endType)
		{
			int num = path.Count - 1;
			if (num < 0)
			{
				return;
			}
			PolyNode polyNode = new PolyNode();
			polyNode.m_jointype = joinType;
			polyNode.m_endtype = endType;
			if (endType != EndType.etClosedLine)
			{
				if (endType != EndType.etClosedPolygon)
				{
					goto IL_48;
				}
			}
			while (num > 0 && path[0] == path[num])
			{
				num--;
			}
			IL_48:
			polyNode.m_polygon.Capacity = num + 1;
			polyNode.m_polygon.Add(path[0]);
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i <= num; i++)
			{
				if (polyNode.m_polygon[num2] != path[i])
				{
					num2++;
					polyNode.m_polygon.Add(path[i]);
					if (path[i].Y > polyNode.m_polygon[num3].Y || (path[i].Y == polyNode.m_polygon[num3].Y && path[i].X < polyNode.m_polygon[num3].X))
					{
						num3 = num2;
					}
				}
			}
			if (endType == EndType.etClosedPolygon && num2 < 2)
			{
				return;
			}
			this.m_polyNodes.AddChild(polyNode);
			if (endType != EndType.etClosedPolygon)
			{
				return;
			}
			if (this.m_lowest.X < 0L)
			{
				this.m_lowest = new IntPoint((long)(this.m_polyNodes.ChildCount - 1), (long)num3);
				return;
			}
			IntPoint intPoint = this.m_polyNodes.Childs[(int)this.m_lowest.X].m_polygon[(int)this.m_lowest.Y];
			if (polyNode.m_polygon[num3].Y > intPoint.Y || (polyNode.m_polygon[num3].Y == intPoint.Y && polyNode.m_polygon[num3].X < intPoint.X))
			{
				this.m_lowest = new IntPoint((long)(this.m_polyNodes.ChildCount - 1), (long)num3);
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000352E8 File Offset: 0x000334E8
		public void AddPaths(List<List<IntPoint>> paths, JoinType joinType, EndType endType)
		{
			foreach (List<IntPoint> list in paths)
			{
				this.AddPath(list, joinType, endType);
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00035338 File Offset: 0x00033538
		private void FixOrientations()
		{
			if (this.m_lowest.X >= 0L && !Clipper.Orientation(this.m_polyNodes.Childs[(int)this.m_lowest.X].m_polygon))
			{
				for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
				{
					PolyNode polyNode = this.m_polyNodes.Childs[i];
					if (polyNode.m_endtype == EndType.etClosedPolygon || (polyNode.m_endtype == EndType.etClosedLine && Clipper.Orientation(polyNode.m_polygon)))
					{
						polyNode.m_polygon.Reverse();
					}
				}
				return;
			}
			for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
			{
				PolyNode polyNode2 = this.m_polyNodes.Childs[j];
				if (polyNode2.m_endtype == EndType.etClosedLine && !Clipper.Orientation(polyNode2.m_polygon))
				{
					polyNode2.m_polygon.Reverse();
				}
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00035418 File Offset: 0x00033618
		internal static DoublePoint GetUnitNormal(IntPoint pt1, IntPoint pt2)
		{
			double num = (double)(pt2.X - pt1.X);
			double num2 = (double)(pt2.Y - pt1.Y);
			if (num == 0.0 && num2 == 0.0)
			{
				return default(DoublePoint);
			}
			double num3 = 1.0 / Math.Sqrt(num * num + num2 * num2);
			num *= num3;
			num2 *= num3;
			return new DoublePoint(num2, -num);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0003548C File Offset: 0x0003368C
		private void DoOffset(double delta)
		{
			this.m_destPolys = new List<List<IntPoint>>();
			this.m_delta = delta;
			if (ClipperBase.near_zero(delta))
			{
				this.m_destPolys.Capacity = this.m_polyNodes.ChildCount;
				for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
				{
					PolyNode polyNode = this.m_polyNodes.Childs[i];
					if (polyNode.m_endtype == EndType.etClosedPolygon)
					{
						this.m_destPolys.Add(polyNode.m_polygon);
					}
				}
				return;
			}
			if (this.MiterLimit > 2.0)
			{
				this.m_miterLim = 2.0 / (this.MiterLimit * this.MiterLimit);
			}
			else
			{
				this.m_miterLim = 0.5;
			}
			double num;
			if (this.ArcTolerance <= 0.0)
			{
				num = 0.25;
			}
			else if (this.ArcTolerance > Math.Abs(delta) * 0.25)
			{
				num = Math.Abs(delta) * 0.25;
			}
			else
			{
				num = this.ArcTolerance;
			}
			double num2 = 3.141592653589793 / Math.Acos(1.0 - num / Math.Abs(delta));
			this.m_sin = Math.Sin(6.283185307179586 / num2);
			this.m_cos = Math.Cos(6.283185307179586 / num2);
			this.m_StepsPerRad = num2 / 6.283185307179586;
			if (delta < 0.0)
			{
				this.m_sin = -this.m_sin;
			}
			this.m_destPolys.Capacity = this.m_polyNodes.ChildCount * 2;
			for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
			{
				PolyNode polyNode2 = this.m_polyNodes.Childs[j];
				this.m_srcPoly = polyNode2.m_polygon;
				int count = this.m_srcPoly.Count;
				if (count != 0 && (delta > 0.0 || (count >= 3 && polyNode2.m_endtype == EndType.etClosedPolygon)))
				{
					this.m_destPoly = new List<IntPoint>();
					if (count == 1)
					{
						if (polyNode2.m_jointype == JoinType.jtRound)
						{
							double num3 = 1.0;
							double num4 = 0.0;
							int num5 = 1;
							while ((double)num5 <= num2)
							{
								this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[0].X + num3 * delta), ClipperOffset.Round((double)this.m_srcPoly[0].Y + num4 * delta)));
								double num6 = num3;
								num3 = num3 * this.m_cos - this.m_sin * num4;
								num4 = num6 * this.m_sin + num4 * this.m_cos;
								num5++;
							}
						}
						else
						{
							double num7 = -1.0;
							double num8 = -1.0;
							for (int k = 0; k < 4; k++)
							{
								this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[0].X + num7 * delta), ClipperOffset.Round((double)this.m_srcPoly[0].Y + num8 * delta)));
								if (num7 < 0.0)
								{
									num7 = 1.0;
								}
								else if (num8 < 0.0)
								{
									num8 = 1.0;
								}
								else
								{
									num7 = -1.0;
								}
							}
						}
						this.m_destPolys.Add(this.m_destPoly);
					}
					else
					{
						this.m_normals.Clear();
						this.m_normals.Capacity = count;
						for (int l = 0; l < count - 1; l++)
						{
							this.m_normals.Add(ClipperOffset.GetUnitNormal(this.m_srcPoly[l], this.m_srcPoly[l + 1]));
						}
						if (polyNode2.m_endtype == EndType.etClosedLine || polyNode2.m_endtype == EndType.etClosedPolygon)
						{
							this.m_normals.Add(ClipperOffset.GetUnitNormal(this.m_srcPoly[count - 1], this.m_srcPoly[0]));
						}
						else
						{
							this.m_normals.Add(new DoublePoint(this.m_normals[count - 2]));
						}
						if (polyNode2.m_endtype == EndType.etClosedPolygon)
						{
							int num9 = count - 1;
							for (int m = 0; m < count; m++)
							{
								this.OffsetPoint(m, ref num9, polyNode2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
						else if (polyNode2.m_endtype == EndType.etClosedLine)
						{
							int num10 = count - 1;
							for (int n = 0; n < count; n++)
							{
								this.OffsetPoint(n, ref num10, polyNode2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
							this.m_destPoly = new List<IntPoint>();
							DoublePoint doublePoint = this.m_normals[count - 1];
							for (int num11 = count - 1; num11 > 0; num11--)
							{
								this.m_normals[num11] = new DoublePoint(-this.m_normals[num11 - 1].X, -this.m_normals[num11 - 1].Y);
							}
							this.m_normals[0] = new DoublePoint(-doublePoint.X, -doublePoint.Y);
							num10 = 0;
							for (int num12 = count - 1; num12 >= 0; num12--)
							{
								this.OffsetPoint(num12, ref num10, polyNode2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
						else
						{
							int num13 = 0;
							for (int num14 = 1; num14 < count - 1; num14++)
							{
								this.OffsetPoint(num14, ref num13, polyNode2.m_jointype);
							}
							if (polyNode2.m_endtype == EndType.etOpenButt)
							{
								int num15 = count - 1;
								IntPoint intPoint = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[num15].X + this.m_normals[num15].X * delta), ClipperOffset.Round((double)this.m_srcPoly[num15].Y + this.m_normals[num15].Y * delta));
								this.m_destPoly.Add(intPoint);
								intPoint = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[num15].X - this.m_normals[num15].X * delta), ClipperOffset.Round((double)this.m_srcPoly[num15].Y - this.m_normals[num15].Y * delta));
								this.m_destPoly.Add(intPoint);
							}
							else
							{
								int num16 = count - 1;
								num13 = count - 2;
								this.m_sinA = 0.0;
								this.m_normals[num16] = new DoublePoint(-this.m_normals[num16].X, -this.m_normals[num16].Y);
								if (polyNode2.m_endtype == EndType.etOpenSquare)
								{
									this.DoSquare(num16, num13);
								}
								else
								{
									this.DoRound(num16, num13);
								}
							}
							for (int num17 = count - 1; num17 > 0; num17--)
							{
								this.m_normals[num17] = new DoublePoint(-this.m_normals[num17 - 1].X, -this.m_normals[num17 - 1].Y);
							}
							this.m_normals[0] = new DoublePoint(-this.m_normals[1].X, -this.m_normals[1].Y);
							num13 = count - 1;
							for (int num18 = num13 - 1; num18 > 0; num18--)
							{
								this.OffsetPoint(num18, ref num13, polyNode2.m_jointype);
							}
							if (polyNode2.m_endtype == EndType.etOpenButt)
							{
								IntPoint intPoint = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[0].X - this.m_normals[0].X * delta), ClipperOffset.Round((double)this.m_srcPoly[0].Y - this.m_normals[0].Y * delta));
								this.m_destPoly.Add(intPoint);
								intPoint = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[0].X + this.m_normals[0].X * delta), ClipperOffset.Round((double)this.m_srcPoly[0].Y + this.m_normals[0].Y * delta));
								this.m_destPoly.Add(intPoint);
							}
							else
							{
								num13 = 1;
								this.m_sinA = 0.0;
								if (polyNode2.m_endtype == EndType.etOpenSquare)
								{
									this.DoSquare(0, 1);
								}
								else
								{
									this.DoRound(0, 1);
								}
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
					}
				}
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00035D8C File Offset: 0x00033F8C
		public void Execute(ref List<List<IntPoint>> solution, double delta)
		{
			solution.Clear();
			this.FixOrientations();
			this.DoOffset(delta);
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(this.m_destPolys, PolyType.ptSubject, true);
			if (delta > 0.0)
			{
				clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);
				return;
			}
			IntRect bounds = ClipperBase.GetBounds(this.m_destPolys);
			clipper.AddPath(new List<IntPoint>(4)
			{
				new IntPoint(bounds.left - 10L, bounds.bottom + 10L),
				new IntPoint(bounds.right + 10L, bounds.bottom + 10L),
				new IntPoint(bounds.right + 10L, bounds.top - 10L),
				new IntPoint(bounds.left - 10L, bounds.top - 10L)
			}, PolyType.ptSubject, true);
			clipper.ReverseSolution = true;
			clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftNegative, PolyFillType.pftNegative);
			if (solution.Count > 0)
			{
				solution.RemoveAt(0);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00035E9C File Offset: 0x0003409C
		public void Execute(ref PolyTree solution, double delta)
		{
			solution.Clear();
			this.FixOrientations();
			this.DoOffset(delta);
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(this.m_destPolys, PolyType.ptSubject, true);
			if (delta > 0.0)
			{
				clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);
				return;
			}
			IntRect bounds = ClipperBase.GetBounds(this.m_destPolys);
			clipper.AddPath(new List<IntPoint>(4)
			{
				new IntPoint(bounds.left - 10L, bounds.bottom + 10L),
				new IntPoint(bounds.right + 10L, bounds.bottom + 10L),
				new IntPoint(bounds.right + 10L, bounds.top - 10L),
				new IntPoint(bounds.left - 10L, bounds.top - 10L)
			}, PolyType.ptSubject, true);
			clipper.ReverseSolution = true;
			clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftNegative, PolyFillType.pftNegative);
			if (solution.ChildCount == 1 && solution.Childs[0].ChildCount > 0)
			{
				PolyNode polyNode = solution.Childs[0];
				solution.Childs.Capacity = polyNode.ChildCount;
				solution.Childs[0] = polyNode.Childs[0];
				solution.Childs[0].m_Parent = solution;
				for (int i = 1; i < polyNode.ChildCount; i++)
				{
					solution.AddChild(polyNode.Childs[i]);
				}
				return;
			}
			solution.Clear();
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00036038 File Offset: 0x00034238
		private void OffsetPoint(int j, ref int k, JoinType jointype)
		{
			this.m_sinA = this.m_normals[k].X * this.m_normals[j].Y - this.m_normals[j].X * this.m_normals[k].Y;
			if (Math.Abs(this.m_sinA * this.m_delta) < 1.0)
			{
				if (this.m_normals[k].X * this.m_normals[j].X + this.m_normals[j].Y * this.m_normals[k].Y > 0.0)
				{
					this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta)));
					return;
				}
			}
			else if (this.m_sinA > 1.0)
			{
				this.m_sinA = 1.0;
			}
			else if (this.m_sinA < -1.0)
			{
				this.m_sinA = -1.0;
			}
			if (this.m_sinA * this.m_delta < 0.0)
			{
				this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta)));
				this.m_destPoly.Add(this.m_srcPoly[j]);
				this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta)));
			}
			else
			{
				switch (jointype)
				{
				case JoinType.jtSquare:
					this.DoSquare(j, k);
					break;
				case JoinType.jtRound:
					this.DoRound(j, k);
					break;
				case JoinType.jtMiter:
				{
					double num = 1.0 + (this.m_normals[j].X * this.m_normals[k].X + this.m_normals[j].Y * this.m_normals[k].Y);
					if (num >= this.m_miterLim)
					{
						this.DoMiter(j, k, num);
					}
					else
					{
						this.DoSquare(j, k);
					}
					break;
				}
				}
			}
			k = j;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003637C File Offset: 0x0003457C
		internal void DoSquare(int j, int k)
		{
			double num = Math.Tan(Math.Atan2(this.m_sinA, this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y) / 4.0);
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[k].X - this.m_normals[k].Y * num)), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[k].Y + this.m_normals[k].X * num))));
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[j].X + this.m_normals[j].Y * num)), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[j].Y - this.m_normals[j].X * num))));
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0003651C File Offset: 0x0003471C
		internal void DoMiter(int j, int k, double r)
		{
			double num = this.m_delta / r;
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + (this.m_normals[k].X + this.m_normals[j].X) * num), ClipperOffset.Round((double)this.m_srcPoly[j].Y + (this.m_normals[k].Y + this.m_normals[j].Y) * num)));
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000365BC File Offset: 0x000347BC
		internal void DoRound(int j, int k)
		{
			double num = Math.Atan2(this.m_sinA, this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y);
			int num2 = Math.Max((int)ClipperOffset.Round(this.m_StepsPerRad * Math.Abs(num)), 1);
			double num3 = this.m_normals[k].X;
			double num4 = this.m_normals[k].Y;
			for (int i = 0; i < num2; i++)
			{
				this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + num3 * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + num4 * this.m_delta)));
				double num5 = num3;
				num3 = num3 * this.m_cos - this.m_sin * num4;
				num4 = num5 * this.m_sin + num4 * this.m_cos;
			}
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta)));
		}

		// Token: 0x040007C2 RID: 1986
		private List<List<IntPoint>> m_destPolys;

		// Token: 0x040007C3 RID: 1987
		private List<IntPoint> m_srcPoly;

		// Token: 0x040007C4 RID: 1988
		private List<IntPoint> m_destPoly;

		// Token: 0x040007C5 RID: 1989
		private List<DoublePoint> m_normals = new List<DoublePoint>();

		// Token: 0x040007C6 RID: 1990
		private double m_delta;

		// Token: 0x040007C7 RID: 1991
		private double m_sinA;

		// Token: 0x040007C8 RID: 1992
		private double m_sin;

		// Token: 0x040007C9 RID: 1993
		private double m_cos;

		// Token: 0x040007CA RID: 1994
		private double m_miterLim;

		// Token: 0x040007CB RID: 1995
		private double m_StepsPerRad;

		// Token: 0x040007CC RID: 1996
		private IntPoint m_lowest;

		// Token: 0x040007CD RID: 1997
		private PolyNode m_polyNodes = new PolyNode();

		// Token: 0x040007D0 RID: 2000
		private const double two_pi = 6.283185307179586;

		// Token: 0x040007D1 RID: 2001
		private const double def_arc_tolerance = 0.25;
	}
}
