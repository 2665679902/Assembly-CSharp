using System;
using System.Collections.Generic;
using ClipperLib;
using Delaunay.Geo;
using KSerialization;
using UnityEngine;

namespace VoronoiTree
{
	// Token: 0x020004B5 RID: 1205
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Node
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x0006E952 File Offset: 0x0006CB52
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x0006E95A File Offset: 0x0006CB5A
		public Tree parent { get; private set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x0006E963 File Offset: 0x0006CB63
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x0006E96B File Offset: 0x0006CB6B
		public PowerDiagram debug_LastPD { get; private set; }

		// Token: 0x0600337B RID: 13179 RVA: 0x0006E974 File Offset: 0x0006CB74
		public void SetParent(Tree newParent)
		{
			this.parent = newParent;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x0006E97D File Offset: 0x0006CB7D
		public Node()
		{
			this.type = Node.NodeType.Unknown;
			this.log = new LoggerSSF("VoronoiNode", 100);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x0006E9A9 File Offset: 0x0006CBA9
		public Node(Node.NodeType type)
		{
			this.type = type;
			this.tags = new TagSet();
			this.log = new LoggerSSF("VoronoiNode", 100);
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x0006E9E0 File Offset: 0x0006CBE0
		protected Node(Diagram.Site site, Node.NodeType type, Tree parent)
		{
			this.tags = new TagSet();
			this.site = site;
			this.type = type;
			this.parent = parent;
			this.log = new LoggerSSF("VoronoiNode", 100);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x0006EA30 File Offset: 0x0006CC30
		public Node GetNeighbour(uint id)
		{
			foreach (KeyValuePair<uint, int> keyValuePair in this.site.neighbours)
			{
				if (keyValuePair.Key == id)
				{
					return this.GetSibling(id);
				}
			}
			return null;
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x0006EA74 File Offset: 0x0006CC74
		public List<Node> GetNeighbors()
		{
			List<Node> list = new List<Node>();
			if (this.site.neighbours != null)
			{
				HashSet<KeyValuePair<uint, int>>.Enumerator enumerator = this.site.neighbours.GetEnumerator();
				while (enumerator.MoveNext())
				{
					List<Node> list2 = list;
					KeyValuePair<uint, int> keyValuePair = enumerator.Current;
					list2.Add(this.GetSibling(keyValuePair.Key));
				}
			}
			return list;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x0006EACC File Offset: 0x0006CCCC
		public List<KeyValuePair<Node, LineSegment>> GetNeighborsByEdge()
		{
			List<KeyValuePair<Node, LineSegment>> list = new List<KeyValuePair<Node, LineSegment>>();
			for (int i = 0; i < this.site.poly.Vertices.Count; i++)
			{
				if (this.site.neighbours != null)
				{
					LineSegment edge = this.site.poly.GetEdge(i);
					Node node = null;
					foreach (KeyValuePair<uint, int> keyValuePair in this.site.neighbours)
					{
						if (keyValuePair.Value == i)
						{
							HashSet<KeyValuePair<uint, int>>.Enumerator enumerator;
							keyValuePair = enumerator.Current;
							node = this.GetSibling(keyValuePair.Key);
						}
					}
					if (node != null)
					{
						list.Add(new KeyValuePair<Node, LineSegment>(node, edge));
					}
				}
			}
			return list;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x0006EB7B File Offset: 0x0006CD7B
		public Node GetSibling(uint siteId)
		{
			return this.parent.GetChildByID(siteId);
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x0006EB8C File Offset: 0x0006CD8C
		public List<Node> GetSiblings()
		{
			List<Node> list = new List<Node>();
			for (int i = 0; i < this.parent.ChildCount(); i++)
			{
				Node child = this.parent.GetChild(i);
				if (child != this)
				{
					list.Add(child);
				}
			}
			return list;
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x0006EBD0 File Offset: 0x0006CDD0
		public void PlaceSites(List<Diagram.Site> sites, int seed)
		{
			SeededRandom seededRandom = new SeededRandom(seed);
			List<Vector2> list = null;
			List<Vector2> list2 = new List<Vector2>();
			for (int i = 0; i < sites.Count; i++)
			{
				list2.Add(sites[i].position);
			}
			int num = 0;
			for (int j = 0; j < sites.Count; j++)
			{
				if (!this.site.poly.Contains(sites[j].position))
				{
					if (list == null)
					{
						list = PointGenerator.GetRandomPoints(this.site.poly, 5f, 1f, list2, PointGenerator.SampleBehaviour.PoissonDisk, true, seededRandom, true, true);
					}
					if (num >= list.Count - 1)
					{
						list2.AddRange(list);
						list = PointGenerator.GetRandomPoints(this.site.poly, 0.5f, 0.5f, list2, PointGenerator.SampleBehaviour.PoissonDisk, true, seededRandom, true, true);
						num = 0;
					}
					if (list.Count == 0)
					{
						sites[j].position = sites[0].position + Vector2.one * seededRandom.RandomValue();
					}
					else
					{
						sites[j].position = list[num++];
					}
				}
			}
			HashSet<Vector2> hashSet = new HashSet<Vector2>();
			for (int k = 0; k < sites.Count; k++)
			{
				if (hashSet.Contains(sites[k].position))
				{
					this.visited = Node.VisitedType.Error;
					sites[k].position += new Vector2((float)seededRandom.RandomRange(0, 1), (float)seededRandom.RandomRange(0, 1));
				}
				hashSet.Add(sites[k].position);
				sites[k].poly = null;
			}
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x0006ED88 File Offset: 0x0006CF88
		public bool ComputeNode(List<Diagram.Site> diagramSites)
		{
			if (this.site.poly == null || diagramSites == null || diagramSites.Count == 0)
			{
				this.visited = Node.VisitedType.MissingData;
				return false;
			}
			this.visited = Node.VisitedType.VisitedSuccess;
			if (diagramSites.Count == 1)
			{
				diagramSites[0].poly = this.site.poly;
				diagramSites[0].position = diagramSites[0].poly.Centroid();
				return true;
			}
			HashSet<Diagram.Site> hashSet = new HashSet<Diagram.Site>();
			for (int i = 0; i < diagramSites.Count; i++)
			{
				hashSet.Add(new Diagram.Site(diagramSites[i].id, diagramSites[i].position, diagramSites[i].weight));
			}
			hashSet.Add(new Diagram.Site(Node.maxIndex + 1U, new Vector2(this.site.poly.bounds.xMin - 500f, this.site.poly.bounds.yMin + this.site.poly.bounds.height / 2f), 1f));
			hashSet.Add(new Diagram.Site(Node.maxIndex + 2U, new Vector2(this.site.poly.bounds.xMax + 500f, this.site.poly.bounds.yMin + this.site.poly.bounds.height / 2f), 1f));
			hashSet.Add(new Diagram.Site(Node.maxIndex + 3U, new Vector2(this.site.poly.bounds.xMin + this.site.poly.bounds.width / 2f, this.site.poly.bounds.yMin - 500f), 1f));
			hashSet.Add(new Diagram.Site(Node.maxIndex + 4U, new Vector2(this.site.poly.bounds.xMin + this.site.poly.bounds.width / 2f, this.site.poly.bounds.yMax + 500f), 1f));
			Diagram diagram = new Diagram(new Rect(this.site.poly.bounds.xMin - 500f, this.site.poly.bounds.yMin - 500f, this.site.poly.bounds.width + 500f, this.site.poly.bounds.height + 500f), hashSet);
			for (int j = 0; j < diagramSites.Count; j++)
			{
				if (diagramSites[j].id <= Node.maxIndex)
				{
					List<Vector2> list = diagram.diagram.Region(diagramSites[j].position);
					if (list == null)
					{
						if (this.type != Node.NodeType.Leaf)
						{
							this.visited = Node.VisitedType.Error;
							return false;
						}
					}
					else
					{
						Polygon polygon = new Polygon(list).Clip(this.site.poly, ClipType.ctIntersection);
						if (polygon == null || polygon.Vertices.Count < 3)
						{
							if (this.type != Node.NodeType.Leaf)
							{
								this.visited = Node.VisitedType.Error;
								return false;
							}
						}
						else
						{
							diagramSites[j].poly = polygon;
						}
					}
				}
			}
			for (int k = 0; k < diagramSites.Count; k++)
			{
				if (diagramSites[k].id <= Node.maxIndex)
				{
					HashSet<uint> hashSet2 = diagram.diagram.NeighborSitesIDsForSite(diagramSites[k].position);
					Node.FilterNeighbours(diagramSites[k], hashSet2, diagramSites);
					diagramSites[k].position = diagramSites[k].poly.Centroid();
				}
			}
			return true;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x0006F1B0 File Offset: 0x0006D3B0
		public bool ComputeNodePD(List<Diagram.Site> diagramSites, int maxIters = 500, float threshold = 0.2f)
		{
			if (this.site.poly == null || diagramSites == null || diagramSites.Count == 0)
			{
				this.visited = Node.VisitedType.MissingData;
				return false;
			}
			this.visited = Node.VisitedType.VisitedSuccess;
			List<PowerDiagramSite> list = new List<PowerDiagramSite>();
			for (int i = 0; i < diagramSites.Count; i++)
			{
				PowerDiagramSite powerDiagramSite = new PowerDiagramSite(diagramSites[i].id, diagramSites[i].position, diagramSites[i].weight);
				list.Add(powerDiagramSite);
			}
			PowerDiagram powerDiagram = new PowerDiagram(this.site.poly, list);
			powerDiagram.ComputeVD();
			powerDiagram.ComputePowerDiagram(maxIters, threshold);
			for (int j = 0; j < diagramSites.Count; j++)
			{
				diagramSites[j].poly = list[j].poly;
				if (diagramSites[j].poly == null)
				{
					global::Debug.LogErrorFormat("Site [{0}] at index [{1}]: Poly shouldnt be null here ever", new object[]
					{
						diagramSites[j].id,
						j
					});
				}
			}
			for (int k = 0; k < diagramSites.Count; k++)
			{
				HashSet<uint> hashSet = new HashSet<uint>();
				for (int l = 0; l < list[k].neighbours.Count; l++)
				{
					if (!list[k].neighbours[l].dummy)
					{
						hashSet.Add((uint)list[k].neighbours[l].id);
					}
				}
				Node.FilterNeighbours(diagramSites[k], hashSet, diagramSites);
				diagramSites[k].position = diagramSites[k].poly.Centroid();
			}
			this.debug_LastPD = powerDiagram;
			return true;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x0006F374 File Offset: 0x0006D574
		private static void FilterNeighbours(Diagram.Site home, HashSet<uint> neighbours, List<Diagram.Site> sites)
		{
			if (home == null)
			{
				global::Debug.LogError("FilterNeighbours home == null");
			}
			HashSet<KeyValuePair<uint, int>> hashSet = new HashSet<KeyValuePair<uint, int>>();
			HashSet<uint>.Enumerator niter = neighbours.GetEnumerator();
			Predicate<Diagram.Site> <>9__0;
			while (niter.MoveNext())
			{
				Predicate<Diagram.Site> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (Diagram.Site s) => s.id == niter.Current);
				}
				Diagram.Site site = sites.Find(predicate);
				if (site != null)
				{
					if (site.poly == null)
					{
						global::Debug.LogError("FilterNeighbours neighbour.poly == null");
					}
					int num = -1;
					Polygon.DebugLog(string.Format("Testing for {0} common edge with {1}", home.id, site.id));
					LineSegment lineSegment;
					if (home.poly.SharesEdge(site.poly, ref num, out lineSegment) == Polygon.Commonality.Edge)
					{
						hashSet.Add(new KeyValuePair<uint, int>(niter.Current, num));
						Polygon.DebugLog(string.Format(" -> {0} common edge with {1}: {2}", home.id, site.id, num));
					}
					else
					{
						Polygon.DebugLog(string.Format(" -> {0} NO COMMON with {1}: {2}", home.id, site.id, num));
					}
				}
			}
			home.neighbours = hashSet;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x0006F4B4 File Offset: 0x0006D6B4
		public void Reset(List<Diagram.Site> sites = null)
		{
			this.visited = Node.VisitedType.NotVisited;
			if (sites != null)
			{
				HashSet<Vector2> hashSet = new HashSet<Vector2>();
				for (int i = 0; i < sites.Count; i++)
				{
					if (hashSet.Contains(sites[i].position))
					{
						this.visited = Node.VisitedType.Error;
						return;
					}
					hashSet.Add(sites[i].position);
				}
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x0006F511 File Offset: 0x0006D711
		public void SetTags(TagSet originalTags)
		{
			this.tags = new TagSet(originalTags);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0006F51F File Offset: 0x0006D71F
		public void AddTag(Tag tag)
		{
			if (this.tags == null)
			{
				this.tags = new TagSet();
			}
			this.tags.Add(tag);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0006F540 File Offset: 0x0006D740
		public void AddTagToNeighbors(Tag tag)
		{
			foreach (KeyValuePair<uint, int> keyValuePair in this.site.neighbours)
			{
				this.GetNeighbour(keyValuePair.Key).AddTag(tag);
			}
		}

		// Token: 0x04001212 RID: 4626
		public static int maxDepth;

		// Token: 0x04001213 RID: 4627
		public static uint maxIndex;

		// Token: 0x04001214 RID: 4628
		[Serialize]
		public Node.NodeType type;

		// Token: 0x04001215 RID: 4629
		public Node.VisitedType visited;

		// Token: 0x04001216 RID: 4630
		public LoggerSSF log;

		// Token: 0x04001217 RID: 4631
		[Serialize]
		public Diagram.Site site;

		// Token: 0x04001218 RID: 4632
		[Serialize]
		public TagSet tags;

		// Token: 0x0400121B RID: 4635
		public Dictionary<Tag, int> minDistanceToTag = new Dictionary<Tag, int>();

		// Token: 0x02000AE0 RID: 2784
		public enum NodeType
		{
			// Token: 0x0400252C RID: 9516
			Unknown,
			// Token: 0x0400252D RID: 9517
			Internal,
			// Token: 0x0400252E RID: 9518
			Leaf
		}

		// Token: 0x02000AE1 RID: 2785
		public enum VisitedType
		{
			// Token: 0x04002530 RID: 9520
			MissingData = -2,
			// Token: 0x04002531 RID: 9521
			Error,
			// Token: 0x04002532 RID: 9522
			NotVisited,
			// Token: 0x04002533 RID: 9523
			VisitedSuccess
		}

		// Token: 0x02000AE2 RID: 2786
		public class SplitCommand
		{
			// Token: 0x04002534 RID: 9524
			public Node.SplitCommand.SplitType splitType;

			// Token: 0x04002535 RID: 9525
			public TagSet dontCopyTags;

			// Token: 0x04002536 RID: 9526
			public TagSet moveTags;

			// Token: 0x04002537 RID: 9527
			public int minChildCount = 2;

			// Token: 0x04002538 RID: 9528
			public Node.SplitCommand.NodeTypeOverride typeOverride;

			// Token: 0x04002539 RID: 9529
			public Action<Tree, Node.SplitCommand> SplitFunction;

			// Token: 0x02000B51 RID: 2897
			public enum SplitType
			{
				// Token: 0x040026AD RID: 9901
				KeepParentAsCentroid = 1,
				// Token: 0x040026AE RID: 9902
				ChildrenDuplicateParent,
				// Token: 0x040026AF RID: 9903
				ChildrenChosenFromLayer = 4
			}

			// Token: 0x02000B52 RID: 2898
			// (Invoke) Token: 0x060058D2 RID: 22738
			public delegate string NodeTypeOverride(Vector2 position);
		}
	}
}
