using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Delaunay;
using Delaunay.Geo;
using KSerialization;
using UnityEngine;

namespace VoronoiTree
{
	// Token: 0x020004B3 RID: 1203
	public class Diagram
	{
		// Token: 0x06003366 RID: 13158 RVA: 0x0006E23D File Offset: 0x0006C43D
		public Diagram()
		{
			this.diagram = null;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x0006E257 File Offset: 0x0006C457
		// (set) Token: 0x06003368 RID: 13160 RVA: 0x0006E25F File Offset: 0x0006C45F
		public Voronoi diagram { get; private set; }

		// Token: 0x06003369 RID: 13161 RVA: 0x0006E268 File Offset: 0x0006C468
		public Diagram(Rect bounds, IEnumerable<Diagram.Site> sites)
		{
			this.bounds = bounds;
			this.ids = new List<uint>();
			this.points = new List<Vector2>();
			this.weights = new List<float>();
			this.weightSum = 0f;
			IEnumerator<Diagram.Site> enumerator = sites.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				Diagram.Site site = enumerator.Current;
				this.AddSite(site);
				num++;
			}
			this.MakeVD();
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x0006E2E4 File Offset: 0x0006C4E4
		private void AddSite(Diagram.Site site)
		{
			this.ids.Add(site.id);
			this.points.Add(site.position);
			this.weights.Add(site.weight);
			this.weightSum += site.weight;
			site.currentWeight = site.weight;
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x0006E343 File Offset: 0x0006C543
		private void MakeVD()
		{
			this.diagram = new Voronoi(this.points, this.ids, this.weights, this.bounds);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0006E368 File Offset: 0x0006C568
		public void UpdateWeights(List<Diagram.Site> sites)
		{
			for (int i = 0; i < sites.Count; i++)
			{
				Diagram.Site site = sites[i];
				site.position = site.poly.Centroid();
				site.currentWeight = Mathf.Max(site.currentWeight, 1f);
			}
			float num = 0f;
			for (int j = 0; j < sites.Count; j++)
			{
				Diagram.Site site2 = sites[j];
				float num2 = site2.poly.Area();
				float num3 = site2.weight / this.weightSum * this.Area();
				float num4 = Mathf.Sqrt(num2 / 3.1415927f);
				float num5 = Mathf.Sqrt(num3 / 3.1415927f);
				float num6 = num4 - num5;
				float num7 = num3 / num2;
				if (((double)num7 > 1.1 && (double)site2.previousWeightAdaption < 0.9) || ((double)num7 < 0.9 && (double)site2.previousWeightAdaption > 1.1))
				{
					num7 = Mathf.Sqrt(num7);
				}
				if ((double)num7 < 1.1 && (double)num7 > 0.9 && site2.currentWeight != 1f)
				{
					num7 = Mathf.Sqrt(num7);
				}
				if (site2.currentWeight < 10f)
				{
					num7 *= num7;
				}
				if (site2.currentWeight > 10f)
				{
					num7 = Mathf.Sqrt(num7);
				}
				site2.previousWeightAdaption = num7;
				site2.currentWeight *= num7;
				if (site2.currentWeight < 1f)
				{
					float num8 = Mathf.Sqrt(site2.currentWeight) - num6;
					if (num8 < 0f)
					{
						site2.currentWeight = -(num8 * num8);
						if (site2.currentWeight < num)
						{
							num = site2.currentWeight;
						}
					}
				}
			}
			if (num < 0f)
			{
				num = -num;
				for (int k = 0; k < sites.Count; k++)
				{
					sites[k].currentWeight += num + 1f;
				}
			}
			float num9 = 1f;
			for (int l = 0; l < sites.Count; l++)
			{
				Diagram.Site site3 = sites[l];
				List<uint> neighbours = this.diagram.ListNeighborSitesIDsForSite(this.points[l]);
				int nIndex2;
				int nIndex;
				Predicate<Diagram.Site> <>9__0;
				for (nIndex = 0; nIndex < neighbours.Count; nIndex = nIndex2 + 1)
				{
					Predicate<Diagram.Site> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = (Diagram.Site s) => s.id == neighbours[nIndex]);
					}
					Diagram.Site site4 = sites.Find(predicate);
					float num10 = (site3.position - site4.position).sqrMagnitude / (Mathf.Abs(site3.currentWeight - site4.currentWeight) + 1f);
					if (num10 < num9)
					{
						num9 = num10;
					}
					nIndex2 = nIndex;
				}
			}
			for (int m = 0; m < sites.Count; m++)
			{
				sites[m].currentWeight *= num9;
				this.weights[m] = sites[m].currentWeight;
				this.points[m] = sites[m].position;
			}
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x0006E6C0 File Offset: 0x0006C8C0
		private float Area()
		{
			return this.bounds.width * this.bounds.height;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x0006E6D9 File Offset: 0x0006C8D9
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x0006E6E1 File Offset: 0x0006C8E1
		public int completeIterations { get; set; }

		// Token: 0x06003370 RID: 13168 RVA: 0x0006E6EC File Offset: 0x0006C8EC
		public List<Diagram.Site> ComputePowerDiagram(List<Diagram.Site> sites, int maxIterations)
		{
			this.completeIterations = 0;
			for (int i = 1; i <= maxIterations; i++)
			{
				this.UpdateWeights(sites);
				this.MakeVD();
				float num = 0f;
				foreach (Diagram.Site site in sites)
				{
					float num2 = site.poly.Area();
					float num3 = site.weight / this.weightSum * this.Area();
					num = Mathf.Max(Mathf.Abs(num2 - num3) / num3, num);
				}
				if (num < 0.001f)
				{
					this.completeIterations = i;
					break;
				}
			}
			return sites;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0006E7A4 File Offset: 0x0006C9A4
		public int GetIdxForNode(uint nodeID)
		{
			for (int i = 0; i < this.points.Count; i++)
			{
				if (this.ids[i] == nodeID)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0006E7DC File Offset: 0x0006C9DC
		public List<uint> GetNodeIdsForTopEdgeCells()
		{
			List<uint> list = new List<uint>();
			for (int i = 0; i < this.points.Count; i++)
			{
				if (this.IsTopEdgeCell(i))
				{
					list.Add(this.ids[i]);
				}
			}
			return list;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0006E824 File Offset: 0x0006CA24
		public bool IsTopEdgeCell(int cell)
		{
			if (cell < 0 || cell >= this.points.Count)
			{
				return false;
			}
			List<Vector2> list = this.diagram.Region(this.points[cell]);
			if (list.Count == 0)
			{
				return false;
			}
			Vector2 vector = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				Vector2 vector2 = list[i];
				if (vector.y == vector2.y && vector2.y == this.bounds.height)
				{
					return true;
				}
				vector = vector2;
			}
			return vector.y == list[0].y && list[0].y == this.bounds.height;
		}

		// Token: 0x04001208 RID: 4616
		public static int maxPowerIterations;

		// Token: 0x04001209 RID: 4617
		public static float maxPowerError;

		// Token: 0x0400120A RID: 4618
		private List<Vector2> points;

		// Token: 0x0400120B RID: 4619
		private List<float> weights;

		// Token: 0x0400120C RID: 4620
		private Rect bounds;

		// Token: 0x0400120D RID: 4621
		private float weightSum;

		// Token: 0x0400120E RID: 4622
		private List<uint> ids = new List<uint>();

		// Token: 0x0400120F RID: 4623
		public int siteIndex;

		// Token: 0x02000ADE RID: 2782
		[SerializationConfig(MemberSerialization.OptIn)]
		public class Site
		{
			// Token: 0x060057B9 RID: 22457 RVA: 0x000A3CBA File Offset: 0x000A1EBA
			public Site()
			{
				this.neighbours = new HashSet<KeyValuePair<uint, int>>();
			}

			// Token: 0x060057BA RID: 22458 RVA: 0x000A3CCD File Offset: 0x000A1ECD
			public Site(uint id, Vector2 pos, float weight = 1f)
			{
				this.id = id;
				this.position = pos;
				this.weight = weight;
				this.currentWeight = weight;
				this.neighbours = new HashSet<KeyValuePair<uint, int>>();
			}

			// Token: 0x060057BB RID: 22459 RVA: 0x000A3CFC File Offset: 0x000A1EFC
			[OnDeserializing]
			internal void OnDeserializingMethod()
			{
				this.neighbours = new HashSet<KeyValuePair<uint, int>>();
			}

			// Token: 0x04002521 RID: 9505
			[Serialize]
			public uint id;

			// Token: 0x04002522 RID: 9506
			[Serialize]
			public float weight;

			// Token: 0x04002523 RID: 9507
			public float currentWeight;

			// Token: 0x04002524 RID: 9508
			public float previousWeightAdaption;

			// Token: 0x04002525 RID: 9509
			[Serialize]
			public Vector2 position;

			// Token: 0x04002526 RID: 9510
			[Serialize]
			public Polygon poly;

			// Token: 0x04002527 RID: 9511
			[Serialize]
			public HashSet<KeyValuePair<uint, int>> neighbours;
		}
	}
}
