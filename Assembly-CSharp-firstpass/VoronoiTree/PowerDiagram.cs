using System;
using System.Collections.Generic;
using System.Linq;
using ClipperLib;
using Delaunay.Geo;
using MIConvexHull;
using UnityEngine;

namespace VoronoiTree
{
	// Token: 0x020004B2 RID: 1202
	public class PowerDiagram
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x0006CF7A File Offset: 0x0006B17A
		// (set) Token: 0x06003350 RID: 13136 RVA: 0x0006CF82 File Offset: 0x0006B182
		public VoronoiMesh<PowerDiagram.DualSite2d, PowerDiagramSite, VoronoiEdge<PowerDiagram.DualSite2d, PowerDiagramSite>> voronoiMesh { get; private set; }

		// Token: 0x06003351 RID: 13137 RVA: 0x0006CF8B File Offset: 0x0006B18B
		public List<PowerDiagramSite> GetSites()
		{
			return this.sites;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x0006CF93 File Offset: 0x0006B193
		// (set) Token: 0x06003353 RID: 13139 RVA: 0x0006CF9B File Offset: 0x0006B19B
		public int completedIterations { get; set; }

		// Token: 0x06003354 RID: 13140 RVA: 0x0006CFA4 File Offset: 0x0006B1A4
		public PowerDiagram(Polygon polyBounds, IEnumerable<PowerDiagramSite> newSites)
		{
			this.bounds = polyBounds;
			this.bounds.ForceWinding(Winding.COUNTERCLOCKWISE);
			this.weightSum = 0f;
			this.sites.Clear();
			IEnumerator<PowerDiagramSite> enumerator = newSites.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				if (!this.bounds.Contains(enumerator.Current.position))
				{
					global::Debug.LogErrorFormat("Cant feed points [{0}] to powerdiagram that are outside its area [{1}] ", new object[]
					{
						enumerator.Current.id,
						enumerator.Current.position
					});
				}
				if (this.bounds.Contains(enumerator.Current.position))
				{
					this.AddSite(enumerator.Current);
				}
				num++;
			}
			Vector2 vector = this.bounds.Centroid();
			for (int i = 0; i < this.bounds.Vertices.Count; i++)
			{
				Vector2 vector2 = this.bounds.Vertices[i];
				Vector2 vector3 = this.bounds.Vertices[(i < this.bounds.Vertices.Count - 1) ? (i + 1) : 0];
				Vector2 vector4 = (vector2 - vector).normalized * 1000f;
				PowerDiagramSite powerDiagramSite = new PowerDiagramSite(vector2 + vector4);
				powerDiagramSite.dummy = true;
				this.externalEdgePoints.Add(powerDiagramSite);
				powerDiagramSite.weight = Mathf.Epsilon;
				powerDiagramSite.currentWeight = Mathf.Epsilon;
				this.dualSites.Add(new PowerDiagram.DualSite2d(powerDiagramSite));
				Vector2 vector5 = ((vector3 - vector2) * 0.5f + vector3 - vector).normalized * 1000f;
				PowerDiagramSite powerDiagramSite2 = new PowerDiagramSite(vector3 + vector5);
				powerDiagramSite2.dummy = true;
				powerDiagramSite2.weight = Mathf.Epsilon;
				powerDiagramSite2.currentWeight = Mathf.Epsilon;
				this.externalEdgePoints.Add(powerDiagramSite2);
				this.dualSites.Add(new PowerDiagram.DualSite2d(powerDiagramSite2));
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x0006D1E8 File Offset: 0x0006B3E8
		public void ComputePowerDiagram(int maxIterations, float threashold = 1f)
		{
			this.completedIterations = 0;
			float num = 0f;
			foreach (PowerDiagramSite powerDiagramSite in this.sites)
			{
				if (powerDiagramSite.poly == null)
				{
					string text = "site poly is null for [";
					string text2 = powerDiagramSite.id.ToString();
					string text3 = "]";
					Vector2 position = powerDiagramSite.position;
					throw new Exception(text + text2 + text3 + position.ToString());
				}
				powerDiagramSite.position = powerDiagramSite.poly.Centroid();
			}
			for (int i = 0; i <= maxIterations; i++)
			{
				try
				{
					this.UpdateWeights(this.sites);
					this.ComputePD();
				}
				catch (Exception ex)
				{
					global::Debug.LogError(string.Concat(new string[]
					{
						"Error [",
						num.ToString(),
						"] iters ",
						this.completedIterations.ToString(),
						"/",
						maxIterations.ToString(),
						" Exception:",
						ex.Message,
						"\n",
						ex.StackTrace
					}));
					break;
				}
				num = 0f;
				foreach (PowerDiagramSite powerDiagramSite2 in this.sites)
				{
					float num2 = ((powerDiagramSite2.poly == null) ? 0.1f : powerDiagramSite2.poly.Area());
					float num3 = powerDiagramSite2.weight / this.weightSum * this.bounds.Area();
					num = Mathf.Max(Mathf.Abs(num2 - num3) / num3, num);
				}
				if (num < threashold)
				{
					this.completedIterations = i;
					return;
				}
				int completedIterations = this.completedIterations;
				this.completedIterations = completedIterations + 1;
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x0006D3F0 File Offset: 0x0006B5F0
		public void ComputeVD()
		{
			this.voronoiMesh = VoronoiMesh.Create<PowerDiagram.DualSite2d, PowerDiagramSite>(this.dualSites);
			foreach (PowerDiagramSite powerDiagramSite in this.voronoiMesh.Vertices)
			{
				Vector2 circumcenter = powerDiagramSite.Circumcenter;
				foreach (PowerDiagram.DualSite2d dualSite2d in powerDiagramSite.Vertices)
				{
					if (!dualSite2d.visited)
					{
						dualSite2d.visited = true;
						if (!dualSite2d.site.dummy)
						{
							List<Vector2> list = new List<Vector2>();
							dualSite2d.site.neighbours = this.TouchingFaces(dualSite2d, powerDiagramSite);
							foreach (PowerDiagramSite powerDiagramSite2 in dualSite2d.site.neighbours)
							{
								Vector2 circumcenter2 = powerDiagramSite2.Circumcenter;
								Color.red.a = 0.3f;
								list.Add(circumcenter2);
							}
							if (list.Count > 0)
							{
								Polygon polygon = PowerDiagram.PolyForRandomPoints(list);
								dualSite2d.site.poly = polygon.Clip(this.bounds, ClipType.ctIntersection);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x0006D564 File Offset: 0x0006B764
		public void ComputeVD3d()
		{
			List<PowerDiagram.DualSite3d> list = new List<PowerDiagram.DualSite3d>();
			foreach (PowerDiagramSite powerDiagramSite in this.sites)
			{
				list.Add(powerDiagramSite.ToDualSite());
			}
			for (int i = 0; i < this.externalEdgePoints.Count; i++)
			{
				list.Add(this.externalEdgePoints[i].ToDualSite());
			}
			foreach (ConvexFace<PowerDiagram.DualSite3d, PowerDiagram.TriangulationCellExt<PowerDiagram.DualSite3d>> convexFace in VoronoiMesh.Create<PowerDiagram.DualSite3d, PowerDiagram.TriangulationCellExt<PowerDiagram.DualSite3d>>(list).Vertices)
			{
				Vector3 vector = Vector3.zero;
				foreach (PowerDiagram.DualSite3d dualSite3d in convexFace.Vertices)
				{
					vector += dualSite3d.coord;
				}
				vector *= 0.33333334f;
				DebugExtension.DebugPoint(vector, Color.red, 1f, 0f, true);
			}
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x0006D68C File Offset: 0x0006B88C
		private bool ContainsVert(PowerDiagramSite face, PowerDiagram.DualSite2d target)
		{
			if (face == null || face.Vertices == null)
			{
				return false;
			}
			for (int i = 0; i < face.Vertices.Length; i++)
			{
				if (face.Vertices[i] == target)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x0006D6C7 File Offset: 0x0006B8C7
		private void AddSite(PowerDiagramSite site)
		{
			this.weightSum += site.weight;
			site.currentWeight = site.weight;
			this.sites.Add(site);
			this.dualSites.Add(new PowerDiagram.DualSite2d(site));
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0006D708 File Offset: 0x0006B908
		private List<PowerDiagramSite> TouchingFaces(PowerDiagram.DualSite2d site, PowerDiagramSite startingFace)
		{
			List<PowerDiagramSite> list = new List<PowerDiagramSite>();
			Stack<PowerDiagramSite> stack = new Stack<PowerDiagramSite>();
			stack.Push(startingFace);
			while (stack.Count > 0)
			{
				PowerDiagramSite powerDiagramSite = stack.Pop();
				if (this.ContainsVert(powerDiagramSite, site) && !list.Contains(powerDiagramSite))
				{
					list.Add(powerDiagramSite);
					for (int i = 0; i < powerDiagramSite.Adjacency.Length; i++)
					{
						if (this.ContainsVert(powerDiagramSite.Adjacency[i], site))
						{
							stack.Push(powerDiagramSite.Adjacency[i]);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x0006D788 File Offset: 0x0006B988
		private PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> GetNeigborFaceForEdge(PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> currentFace, PowerDiagram.DualSite3d sharedVert0, PowerDiagram.DualSite3d sharedVert1)
		{
			for (int i = 0; i < currentFace.Adjacency.Length; i++)
			{
				PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> convexFaceExt = currentFace.Adjacency[i];
				if (convexFaceExt != null)
				{
					int num = 0;
					for (int j = 0; j < convexFaceExt.Vertices.Length; j++)
					{
						if (sharedVert0 == convexFaceExt.Vertices[j])
						{
							num++;
						}
						else if (sharedVert1 == convexFaceExt.Vertices[j])
						{
							num++;
						}
						if (num == 2)
						{
							return convexFaceExt;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x0006D7F0 File Offset: 0x0006B9F0
		private PowerDiagram.Edge GetEdge(PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> face0, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> face1)
		{
			PowerDiagram.Edge edge = null;
			for (int i = 0; i < face0.Vertices.Length; i++)
			{
				for (int j = 0; j < face1.Vertices.Length; j++)
				{
					if (face0.Vertices[i] == face1.Vertices[j])
					{
						if (edge == null)
						{
							edge = new PowerDiagram.Edge(face0.Vertices[i], null);
						}
						else
						{
							edge.Second = face0.Vertices[i];
						}
					}
				}
			}
			return edge;
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x0006D85C File Offset: 0x0006BA5C
		private bool ContainsVert(PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> face, PowerDiagram.DualSite3d target)
		{
			for (int i = 0; i < face.Vertices.Length; i++)
			{
				if (face.Vertices[i] == target)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x0006D88C File Offset: 0x0006BA8C
		private List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> TouchingFaces(PowerDiagram.DualSite3d site, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> startingFace)
		{
			List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> list = new List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>>();
			Stack<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> stack = new Stack<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>>();
			stack.Push(startingFace);
			while (stack.Count > 0)
			{
				PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> convexFaceExt = stack.Pop();
				if (this.ContainsVert(convexFaceExt, site) && !list.Contains(convexFaceExt))
				{
					list.Add(convexFaceExt);
					for (int i = 0; i < convexFaceExt.Adjacency.Length; i++)
					{
						if (this.ContainsVert(convexFaceExt.Adjacency[i], site) && !list.Contains(convexFaceExt.Adjacency[i]))
						{
							stack.Push(convexFaceExt.Adjacency[i]);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x0006D91C File Offset: 0x0006BB1C
		private List<PowerDiagramSite> GenerateNeighbors(PowerDiagram.DualSite3d dualSite, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> startingFace)
		{
			List<PowerDiagramSite> list = new List<PowerDiagramSite>();
			List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> list2 = new List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>>();
			Stack<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> stack = new Stack<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>>();
			stack.Push(startingFace);
			while (stack.Count > 0)
			{
				PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> convexFaceExt = stack.Pop();
				list2.Add(convexFaceExt);
				for (int i = 0; i < convexFaceExt.Adjacency.Length; i++)
				{
					if (this.ContainsVert(convexFaceExt.Adjacency[i], dualSite) && !list2.Contains(convexFaceExt.Adjacency[i]))
					{
						PowerDiagram.Edge edge = this.GetEdge(convexFaceExt, convexFaceExt.Adjacency[i]);
						PowerDiagram.DualSite3d dualSite3d = ((edge.First == dualSite) ? edge.Second : edge.First);
						global::Debug.Assert(dualSite3d != dualSite, "We're our own neighbour??");
						global::Debug.Assert(dualSite3d.site.id == -1 || !list.Contains(dualSite3d.site), "Tried adding a site twice!");
						list.Add(dualSite3d.site);
						stack.Push(convexFaceExt.Adjacency[i]);
					}
				}
			}
			return list;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0006DA30 File Offset: 0x0006BC30
		private void ComputePD()
		{
			List<PowerDiagram.DualSite3d> list = new List<PowerDiagram.DualSite3d>();
			foreach (PowerDiagramSite powerDiagramSite in this.sites)
			{
				list.Add(powerDiagramSite.ToDualSite());
			}
			for (int i = 0; i < this.externalEdgePoints.Count; i++)
			{
				list.Add(this.externalEdgePoints[i].ToDualSite());
			}
			this.CheckPositions(list);
			ConvexHull<PowerDiagram.DualSite3d, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> convexHull = PowerDiagram.CreateHull(list, 1E-10);
			foreach (PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> convexFaceExt in convexHull.Faces)
			{
				if (convexFaceExt.Normal[2] < (double)(-(double)Mathf.Epsilon))
				{
					foreach (PowerDiagram.DualSite3d dualSite3d in convexFaceExt.Vertices)
					{
						if (!dualSite3d.site.dummy && !dualSite3d.visited)
						{
							dualSite3d.visited = true;
							List<Vector2> list2 = new List<Vector2>();
							List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> list3 = this.TouchingFaces(dualSite3d, convexFaceExt);
							dualSite3d.site.neighbours = this.GenerateNeighbors(dualSite3d, convexFaceExt);
							foreach (PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> convexFaceExt2 in list3)
							{
								Vector2 dualPoint = convexFaceExt2.GetDualPoint();
								list2.Add(dualPoint);
							}
							Polygon polygon = PowerDiagram.PolyForRandomPoints(list2).Clip(this.bounds, ClipType.ctIntersection);
							if (polygon == null)
							{
								DebugExtension.DebugCircle2d(dualSite3d.site.position, Color.magenta, 5f, 0f, true, 20f);
							}
							else
							{
								dualSite3d.site.poly = polygon;
							}
						}
					}
				}
			}
			this.debug_LastHull = convexHull;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x0006DC64 File Offset: 0x0006BE64
		private void UpdateWeights(List<PowerDiagramSite> sites)
		{
			foreach (PowerDiagramSite powerDiagramSite in sites)
			{
				if (powerDiagramSite.poly == null)
				{
					string text = "site poly is null for [";
					string text2 = powerDiagramSite.id.ToString();
					string text3 = "]";
					Vector2 position = powerDiagramSite.position;
					throw new Exception(text + text2 + text3 + position.ToString());
				}
				powerDiagramSite.position = powerDiagramSite.poly.Centroid();
				powerDiagramSite.currentWeight = Mathf.Max(powerDiagramSite.currentWeight, 1f);
			}
			float num = 0f;
			foreach (PowerDiagramSite powerDiagramSite2 in sites)
			{
				float num2 = ((powerDiagramSite2.poly == null) ? 0.1f : powerDiagramSite2.poly.Area());
				float num3 = powerDiagramSite2.weight / this.weightSum * this.bounds.Area();
				float num4 = Mathf.Sqrt(num2 / 3.1415927f);
				float num5 = Mathf.Sqrt(num3 / 3.1415927f);
				float num6 = num4 - num5;
				float num7 = num3 / num2;
				if (((double)num7 > 1.1 && (double)powerDiagramSite2.previousWeightAdaption < 0.9) || ((double)num7 < 0.9 && (double)powerDiagramSite2.previousWeightAdaption > 1.1))
				{
					num7 = Mathf.Sqrt(num7);
				}
				if ((double)num7 < 1.1 && (double)num7 > 0.9 && powerDiagramSite2.currentWeight != 1f)
				{
					num7 = Mathf.Sqrt(num7);
				}
				if (powerDiagramSite2.currentWeight < 10f)
				{
					num7 *= num7;
				}
				if (powerDiagramSite2.currentWeight > 10f)
				{
					num7 = Mathf.Sqrt(num7);
				}
				powerDiagramSite2.previousWeightAdaption = num7;
				powerDiagramSite2.currentWeight *= num7;
				if (powerDiagramSite2.currentWeight < 1f)
				{
					float num8 = Mathf.Sqrt(powerDiagramSite2.currentWeight) - num6;
					if (num8 < 0f)
					{
						powerDiagramSite2.currentWeight = -(num8 * num8);
						if (powerDiagramSite2.currentWeight < num)
						{
							num = powerDiagramSite2.currentWeight;
						}
					}
				}
			}
			if (num < 0f)
			{
				num = -num;
				foreach (PowerDiagramSite powerDiagramSite3 in sites)
				{
					powerDiagramSite3.currentWeight += num + 1f;
				}
			}
			float num9 = 1f;
			foreach (PowerDiagramSite powerDiagramSite4 in sites)
			{
				foreach (PowerDiagramSite powerDiagramSite5 in powerDiagramSite4.neighbours)
				{
					float num10 = (powerDiagramSite4.position - powerDiagramSite5.position).sqrMagnitude / (Mathf.Abs(powerDiagramSite4.currentWeight - powerDiagramSite5.currentWeight) + 1f);
					if (num10 < num9)
					{
						num9 = num10;
					}
				}
			}
			foreach (PowerDiagramSite powerDiagramSite6 in sites)
			{
				powerDiagramSite6.currentWeight *= num9;
			}
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x0006E05C File Offset: 0x0006C25C
		private List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> GetNeigborFaces(PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> currentFace)
		{
			List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> list = new List<PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>>();
			for (int i = 0; i < currentFace.Adjacency.Length; i++)
			{
				PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d> convexFaceExt = currentFace.Adjacency[i];
				if (convexFaceExt != null)
				{
					list.Add(convexFaceExt);
				}
			}
			return list;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0006E098 File Offset: 0x0006C298
		private void CheckPositions(List<PowerDiagram.DualSite3d> dual3dSites)
		{
			for (int i = 0; i < dual3dSites.Count; i++)
			{
				if (!dual3dSites[i].site.dummy)
				{
					global::Debug.Assert(dual3dSites[i].site.currentWeight != 0f);
					for (int j = i + 1; j < dual3dSites.Count; j++)
					{
						if (!dual3dSites[j].site.dummy && dual3dSites[i].coord == dual3dSites[j].coord)
						{
							dual3dSites[j].coord += new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0006E164 File Offset: 0x0006C364
		public static Polygon PolyForRandomPoints(List<Vector2> verts)
		{
			double[][] array = new double[verts.Count][];
			for (int i = 0; i < verts.Count; i++)
			{
				array[i] = new double[]
				{
					(double)verts[i].x,
					(double)verts[i].y
				};
			}
			double[][] array2 = ConvexHull.Create(array, 1E-10).Points.Select((DefaultVertex p) => p.Position).ToArray<double[]>();
			Polygon polygon = new Polygon();
			for (int j = 0; j < array2.Length; j++)
			{
				polygon.Add(new Vector2((float)array2[j][0], (float)array2[j][1]));
			}
			polygon.Initialize();
			polygon.ForceWinding(Winding.COUNTERCLOCKWISE);
			return polygon;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x0006E234 File Offset: 0x0006C434
		public static ConvexHull<PowerDiagram.DualSite3d, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> CreateHull(IList<PowerDiagram.DualSite3d> data, double PlaneDistanceTolerance = 1E-10)
		{
			return ConvexHull<PowerDiagram.DualSite3d, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>>.Create(data, PlaneDistanceTolerance);
		}

		// Token: 0x040011FF RID: 4607
		public const Winding ForcedWinding = Winding.COUNTERCLOCKWISE;

		// Token: 0x04001201 RID: 4609
		private Polygon bounds;

		// Token: 0x04001202 RID: 4610
		private List<PowerDiagramSite> externalEdgePoints = new List<PowerDiagramSite>();

		// Token: 0x04001203 RID: 4611
		private float weightSum;

		// Token: 0x04001204 RID: 4612
		private List<PowerDiagramSite> sites = new List<PowerDiagramSite>();

		// Token: 0x04001205 RID: 4613
		private List<PowerDiagram.DualSite2d> dualSites = new List<PowerDiagram.DualSite2d>();

		// Token: 0x04001207 RID: 4615
		private ConvexHull<PowerDiagram.DualSite3d, PowerDiagram.ConvexFaceExt<PowerDiagram.DualSite3d>> debug_LastHull;

		// Token: 0x02000AD8 RID: 2776
		private class Edge : MathUtil.Pair<PowerDiagram.DualSite3d, PowerDiagram.DualSite3d>
		{
			// Token: 0x06005791 RID: 22417 RVA: 0x000A3656 File Offset: 0x000A1856
			public Edge(PowerDiagram.DualSite3d first, PowerDiagram.DualSite3d second)
			{
				base.First = first;
				base.Second = second;
			}
		}

		// Token: 0x02000AD9 RID: 2777
		public class ConvexFaceExt<TVertex> : ConvexFace<TVertex, PowerDiagram.ConvexFaceExt<TVertex>> where TVertex : IVertex
		{
			// Token: 0x17000EBF RID: 3775
			// (get) Token: 0x06005793 RID: 22419 RVA: 0x000A3674 File Offset: 0x000A1874
			public TVertex vertex0
			{
				get
				{
					return base.Vertices[0];
				}
			}

			// Token: 0x17000EC0 RID: 3776
			// (get) Token: 0x06005794 RID: 22420 RVA: 0x000A3682 File Offset: 0x000A1882
			public TVertex vertex1
			{
				get
				{
					return base.Vertices[1];
				}
			}

			// Token: 0x17000EC1 RID: 3777
			// (get) Token: 0x06005795 RID: 22421 RVA: 0x000A3690 File Offset: 0x000A1890
			public TVertex vertex2
			{
				get
				{
					return base.Vertices[2];
				}
			}

			// Token: 0x17000EC2 RID: 3778
			// (get) Token: 0x06005796 RID: 22422 RVA: 0x000A369E File Offset: 0x000A189E
			public PowerDiagram.ConvexFaceExt<TVertex> edge0
			{
				get
				{
					return base.Adjacency[0];
				}
			}

			// Token: 0x17000EC3 RID: 3779
			// (get) Token: 0x06005797 RID: 22423 RVA: 0x000A36A8 File Offset: 0x000A18A8
			public PowerDiagram.ConvexFaceExt<TVertex> edge1
			{
				get
				{
					return base.Adjacency[1];
				}
			}

			// Token: 0x17000EC4 RID: 3780
			// (get) Token: 0x06005798 RID: 22424 RVA: 0x000A36B2 File Offset: 0x000A18B2
			public PowerDiagram.ConvexFaceExt<TVertex> edge2
			{
				get
				{
					return base.Adjacency[2];
				}
			}

			// Token: 0x06005799 RID: 22425 RVA: 0x000A36BC File Offset: 0x000A18BC
			public Vector2 GetDualPoint()
			{
				if (this.dualPoint.x == 0f && this.dualPoint.y == 0f)
				{
					TVertex tvertex = this.vertex0;
					float num = (float)tvertex.Position[0];
					tvertex = this.vertex0;
					float num2 = (float)tvertex.Position[1];
					tvertex = this.vertex0;
					Vector3 vector = new Vector3(num, num2, (float)tvertex.Position[2]);
					tvertex = this.vertex1;
					float num3 = (float)tvertex.Position[0];
					tvertex = this.vertex1;
					float num4 = (float)tvertex.Position[1];
					tvertex = this.vertex1;
					Vector3 vector2 = new Vector3(num3, num4, (float)tvertex.Position[2]);
					tvertex = this.vertex2;
					float num5 = (float)tvertex.Position[0];
					tvertex = this.vertex2;
					float num6 = (float)tvertex.Position[1];
					tvertex = this.vertex2;
					Vector3 vector3 = new Vector3(num5, num6, (float)tvertex.Position[2]);
					double num7 = (double)(vector.y * (vector2.z - vector3.z) + vector2.y * (vector3.z - vector.z) + vector3.y * (vector.z - vector2.z));
					double num8 = (double)(vector.z * (vector2.x - vector3.x) + vector2.z * (vector3.x - vector.x) + vector3.z * (vector.x - vector2.x));
					double num9 = -0.5 / (double)(vector.x * (vector2.y - vector3.y) + vector2.x * (vector3.y - vector.y) + vector3.x * (vector.y - vector2.y));
					this.dualPoint = new Vector2((float)(num7 * num9), (float)(num8 * num9));
				}
				return this.dualPoint;
			}

			// Token: 0x0600579A RID: 22426 RVA: 0x000A38C8 File Offset: 0x000A1AC8
			private double Det(double[,] m)
			{
				return m[0, 0] * (m[1, 1] * m[2, 2] - m[2, 1] * m[1, 2]) - m[0, 1] * (m[1, 0] * m[2, 2] - m[2, 0] * m[1, 2]) + m[0, 2] * (m[1, 0] * m[2, 1] - m[2, 0] * m[1, 1]);
			}

			// Token: 0x0600579B RID: 22427 RVA: 0x000A395C File Offset: 0x000A1B5C
			private double LengthSquared(double[] v)
			{
				double num = 0.0;
				foreach (double num2 in v)
				{
					num += num2 * num2;
				}
				return num;
			}

			// Token: 0x0600579C RID: 22428 RVA: 0x000A398C File Offset: 0x000A1B8C
			private Vector2 GetCircumcenter()
			{
				TVertex[] vertices = base.Vertices;
				double[,] array = new double[3, 3];
				for (int i = 0; i < 3; i++)
				{
					array[i, 0] = vertices[i].Position[0];
					array[i, 1] = vertices[i].Position[1];
					array[i, 2] = 1.0;
				}
				double num = this.Det(array);
				double num2 = -1.0 / (2.0 * num);
				for (int j = 0; j < 3; j++)
				{
					array[j, 0] = this.LengthSquared(vertices[j].Position);
				}
				double num3 = -this.Det(array);
				for (int k = 0; k < 3; k++)
				{
					array[k, 1] = vertices[k].Position[0];
				}
				double num4 = this.Det(array);
				return new Vector2((float)(num2 * num3), (float)(num2 * num4));
			}

			// Token: 0x17000EC5 RID: 3781
			// (get) Token: 0x0600579D RID: 22429 RVA: 0x000A3AB0 File Offset: 0x000A1CB0
			public Vector2 Circumcenter
			{
				get
				{
					this.circumCenter = new Vector2?(this.circumCenter ?? this.GetCircumcenter());
					return this.circumCenter.Value;
				}
			}

			// Token: 0x04002517 RID: 9495
			private PowerDiagramSite site;

			// Token: 0x04002518 RID: 9496
			private Vector2 dualPoint;

			// Token: 0x04002519 RID: 9497
			private Vector2? circumCenter;
		}

		// Token: 0x02000ADA RID: 2778
		public class TriangulationCellExt<TVertex> : TriangulationCell<TVertex, PowerDiagram.TriangulationCellExt<TVertex>> where TVertex : IVertex
		{
			// Token: 0x17000EC6 RID: 3782
			// (get) Token: 0x0600579E RID: 22430 RVA: 0x000A3AF2 File Offset: 0x000A1CF2
			public TVertex Vertex0
			{
				get
				{
					return base.Vertices[0];
				}
			}

			// Token: 0x17000EC7 RID: 3783
			// (get) Token: 0x0600579F RID: 22431 RVA: 0x000A3B00 File Offset: 0x000A1D00
			public TVertex Vertex1
			{
				get
				{
					return base.Vertices[1];
				}
			}

			// Token: 0x17000EC8 RID: 3784
			// (get) Token: 0x060057A0 RID: 22432 RVA: 0x000A3B0E File Offset: 0x000A1D0E
			public TVertex Vertex2
			{
				get
				{
					return base.Vertices[2];
				}
			}

			// Token: 0x17000EC9 RID: 3785
			// (get) Token: 0x060057A1 RID: 22433 RVA: 0x000A3B1C File Offset: 0x000A1D1C
			public PowerDiagram.TriangulationCellExt<TVertex> Edge0
			{
				get
				{
					return base.Adjacency[0];
				}
			}

			// Token: 0x17000ECA RID: 3786
			// (get) Token: 0x060057A2 RID: 22434 RVA: 0x000A3B26 File Offset: 0x000A1D26
			public PowerDiagram.TriangulationCellExt<TVertex> Edge1
			{
				get
				{
					return base.Adjacency[1];
				}
			}

			// Token: 0x17000ECB RID: 3787
			// (get) Token: 0x060057A3 RID: 22435 RVA: 0x000A3B30 File Offset: 0x000A1D30
			public PowerDiagram.TriangulationCellExt<TVertex> Edge2
			{
				get
				{
					return base.Adjacency[2];
				}
			}
		}

		// Token: 0x02000ADB RID: 2779
		public class DualSite2d : IVertex
		{
			// Token: 0x17000ECC RID: 3788
			// (get) Token: 0x060057A5 RID: 22437 RVA: 0x000A3B42 File Offset: 0x000A1D42
			public double[] Position
			{
				get
				{
					return new double[]
					{
						(double)this.site.position[0],
						(double)this.site.position[1]
					};
				}
			}

			// Token: 0x17000ECD RID: 3789
			// (get) Token: 0x060057A6 RID: 22438 RVA: 0x000A3B74 File Offset: 0x000A1D74
			// (set) Token: 0x060057A7 RID: 22439 RVA: 0x000A3B7C File Offset: 0x000A1D7C
			public PowerDiagramSite site { get; set; }

			// Token: 0x17000ECE RID: 3790
			// (get) Token: 0x060057A8 RID: 22440 RVA: 0x000A3B85 File Offset: 0x000A1D85
			// (set) Token: 0x060057A9 RID: 22441 RVA: 0x000A3B8D File Offset: 0x000A1D8D
			public bool visited { get; set; }

			// Token: 0x060057AA RID: 22442 RVA: 0x000A3B96 File Offset: 0x000A1D96
			public DualSite2d(PowerDiagramSite site)
			{
				this.site = site;
				this.visited = false;
			}
		}

		// Token: 0x02000ADC RID: 2780
		public class DualSite3d : IVertex
		{
			// Token: 0x17000ECF RID: 3791
			// (get) Token: 0x060057AB RID: 22443 RVA: 0x000A3BAC File Offset: 0x000A1DAC
			public double[] Position
			{
				get
				{
					return new double[]
					{
						(double)this.coord[0],
						(double)this.coord[1],
						(double)this.coord[2]
					};
				}
			}

			// Token: 0x17000ED0 RID: 3792
			// (get) Token: 0x060057AC RID: 22444 RVA: 0x000A3BF8 File Offset: 0x000A1DF8
			// (set) Token: 0x060057AD RID: 22445 RVA: 0x000A3C00 File Offset: 0x000A1E00
			public Vector3 coord { get; set; }

			// Token: 0x17000ED1 RID: 3793
			// (get) Token: 0x060057AE RID: 22446 RVA: 0x000A3C09 File Offset: 0x000A1E09
			// (set) Token: 0x060057AF RID: 22447 RVA: 0x000A3C11 File Offset: 0x000A1E11
			public PowerDiagramSite site { get; set; }

			// Token: 0x17000ED2 RID: 3794
			// (get) Token: 0x060057B0 RID: 22448 RVA: 0x000A3C1A File Offset: 0x000A1E1A
			// (set) Token: 0x060057B1 RID: 22449 RVA: 0x000A3C22 File Offset: 0x000A1E22
			public bool visited { get; set; }

			// Token: 0x060057B2 RID: 22450 RVA: 0x000A3C2B File Offset: 0x000A1E2B
			public DualSite3d()
				: this(0.0, 0.0, 0.0)
			{
			}

			// Token: 0x060057B3 RID: 22451 RVA: 0x000A3C4E File Offset: 0x000A1E4E
			public DualSite3d(double _x, double _y, double _z)
			{
				this.coord = new Vector3((float)_x, (float)_y, (float)_z);
				this.visited = false;
			}

			// Token: 0x060057B4 RID: 22452 RVA: 0x000A3C6E File Offset: 0x000A1E6E
			public DualSite3d(Vector3 pos)
			{
				this.coord = pos;
				this.visited = false;
			}

			// Token: 0x060057B5 RID: 22453 RVA: 0x000A3C84 File Offset: 0x000A1E84
			public DualSite3d(double _x, double _y, double _z, PowerDiagramSite _originalSite)
				: this(_x, _y, _z)
			{
				this.site = _originalSite;
				this.visited = false;
			}
		}
	}
}
