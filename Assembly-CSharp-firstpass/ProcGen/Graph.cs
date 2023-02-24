using System;
using System.Collections.Generic;
using Delaunay.Geo;
using KSerialization;
using Satsuma;
using Satsuma.Drawing;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004BB RID: 1211
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Graph<N, A> where N : Node, new() where A : Arc, new()
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x00070C8B File Offset: 0x0006EE8B
		public List<N> nodes
		{
			get
			{
				return this.nodeList;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x00070C93 File Offset: 0x0006EE93
		public List<A> arcs
		{
			get
			{
				return this.arcList;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x00070C9B File Offset: 0x0006EE9B
		// (set) Token: 0x060033D1 RID: 13265 RVA: 0x00070CA3 File Offset: 0x0006EEA3
		public CustomGraph baseGraph { get; private set; }

		// Token: 0x060033D2 RID: 13266 RVA: 0x00070CAC File Offset: 0x0006EEAC
		public void SetSeed(int seed)
		{
			this.myRandom = new SeededRandom(seed);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x00070CBA File Offset: 0x0006EEBA
		public Graph(int seed)
		{
			this.SetSeed(seed);
			this.nodeList = new List<N>();
			this.arcList = new List<A>();
			this.baseGraph = new CustomGraph();
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x00070CEC File Offset: 0x0006EEEC
		public N AddNode(string type, Vector2 position = default(Vector2))
		{
			N n = new N();
			n.SetNode(this.baseGraph.AddNode());
			n.SetType(type);
			n.SetPosition(position);
			this.nodeList.Add(n);
			return n;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x00070D3A File Offset: 0x0006EF3A
		public void Remove(N n)
		{
			this.baseGraph.DeleteNode(n.node);
			this.nodes.Remove(n);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x00070D60 File Offset: 0x0006EF60
		public A AddArc(N nodeA, N nodeB, string type)
		{
			Arc arc = this.baseGraph.AddArc(nodeA.node, nodeB.node, Directedness.Undirected);
			A a = new A();
			a.SetArc(arc);
			a.SetType(type);
			this.arcList.Add(a);
			return a;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x00070DBC File Offset: 0x0006EFBC
		public N FindNodeByID(uint id)
		{
			return this.nodeList.Find((N node) => node.node.Id == (long)((ulong)id));
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x00070DF0 File Offset: 0x0006EFF0
		public A FindArcByID(uint id)
		{
			return this.arcList.Find((A arc) => arc.arc.Id == (long)((ulong)id));
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x00070E21 File Offset: 0x0006F021
		public N FindNode(Predicate<N> pred)
		{
			return this.nodeList.Find(pred);
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x00070E2F File Offset: 0x0006F02F
		public A FindArc(Predicate<A> pred)
		{
			return this.arcList.Find(pred);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x00070E40 File Offset: 0x0006F040
		public List<A> GetArcs(N node0)
		{
			List<A> list = new List<A>();
			using (IEnumerator<Arc> enumerator = this.baseGraph.Arcs(node0.node, ArcFilter.All).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Arc sarc = enumerator.Current;
					list.Add(this.arcList.Find((A a) => a.arc == sarc));
				}
			}
			return list;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x00070EC8 File Offset: 0x0006F0C8
		public A GetArc(N node0, N node1)
		{
			IEnumerator<Arc> enumerator = this.baseGraph.Arcs(node0.node, node1.node, ArcFilter.All).GetEnumerator();
			if (enumerator.MoveNext())
			{
				Arc sarc = enumerator.Current;
				return this.arcList.Find((A a) => a.arc == sarc);
			}
			return default(A);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x00070F38 File Offset: 0x0006F138
		public List<N> GetNodes(A arc)
		{
			Node u = this.baseGraph.U(arc.arc);
			Node v = this.baseGraph.V(arc.arc);
			return new List<N>
			{
				this.nodeList.Find((N n) => n.node == u),
				this.nodeList.Find((N n) => n.node == v)
			};
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x00070FC4 File Offset: 0x0006F1C4
		public int GetDistanceToTagSetFromNode(N node, TagSet tagset)
		{
			List<N> nodesWithAtLeastOneTag = this.GetNodesWithAtLeastOneTag(tagset);
			if (nodesWithAtLeastOneTag.Count > 0)
			{
				Dijkstra dijkstra = new Dijkstra(this.baseGraph, (Arc arc) => 1.0, DijkstraMode.Sum);
				for (int i = 0; i < nodesWithAtLeastOneTag.Count; i++)
				{
					dijkstra.AddSource(nodesWithAtLeastOneTag[i].node);
				}
				dijkstra.RunUntilFixed(node.node);
				return (int)dijkstra.GetDistance(node.node);
			}
			return -1;
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x0007105C File Offset: 0x0006F25C
		public int GetDistanceToTagFromNode(N node, Tag tag)
		{
			List<N> nodesWithTag = this.GetNodesWithTag(tag);
			if (nodesWithTag.Count > 0)
			{
				Dijkstra dijkstra = new Dijkstra(this.baseGraph, (Arc arc) => 1.0, DijkstraMode.Sum);
				for (int i = 0; i < nodesWithTag.Count; i++)
				{
					dijkstra.AddSource(nodesWithTag[i].node);
				}
				dijkstra.RunUntilFixed(node.node);
				return (int)dijkstra.GetDistance(node.node);
			}
			return -1;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000710F4 File Offset: 0x0006F2F4
		public Dictionary<uint, int> GetDistanceToTag(Tag tag)
		{
			List<N> nodesWithTag = this.GetNodesWithTag(tag);
			if (nodesWithTag.Count > 0)
			{
				Dijkstra dijkstra = new Dijkstra(this.baseGraph, (Arc arc) => 1.0, DijkstraMode.Sum);
				for (int i = 0; i < nodesWithTag.Count; i++)
				{
					dijkstra.AddSource(nodesWithTag[i].node);
				}
				Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
				for (int j = 0; j < this.nodes.Count; j++)
				{
					dijkstra.RunUntilFixed(this.nodes[j].node);
					dictionary[(uint)this.nodes[j].node.Id] = (int)dijkstra.GetDistance(this.nodes[j].node);
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000711F0 File Offset: 0x0006F3F0
		public List<N> GetNodesWithAtLeastOneTag(TagSet tagset)
		{
			return this.nodeList.FindAll((N node) => node.tags.ContainsOne(tagset));
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x00071224 File Offset: 0x0006F424
		public List<N> GetNodesWithTag(Tag tag)
		{
			return this.nodeList.FindAll((N node) => node.tags.Contains(tag));
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x00071258 File Offset: 0x0006F458
		public List<A> GetArcsWithTag(Tag tag)
		{
			return this.arcList.FindAll((A arc) => arc.tags.Contains(tag));
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x0007128C File Offset: 0x0006F48C
		public static PointD GetForceForBoundry(PointD particle, Polygon bounds)
		{
			Vector2 vector = new Vector2((float)particle.X, (float)particle.Y);
			List<KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>>> edgesWithinDistance = bounds.GetEdgesWithinDistance(vector, float.MaxValue);
			double num = 0.0;
			double num2 = 0.0;
			for (int i = 0; i < edgesWithinDistance.Count; i++)
			{
				KeyValuePair<MathUtil.Pair<float, float>, MathUtil.Pair<Vector2, Vector2>> keyValuePair = edgesWithinDistance[i];
				MathUtil.Pair<Vector2, Vector2> value = keyValuePair.Value;
				float second = keyValuePair.Key.Second;
				double num3 = (double)keyValuePair.Key.First;
				Vector2 vector2 = value.First + (value.Second - value.First) * second;
				PointD pointD = new PointD((double)vector2.x, (double)vector2.y);
				double num4 = 1.0 / (num3 * num3);
				num += (particle.X - pointD.X) / num3 * num4;
				num2 += (particle.Y - pointD.Y) / num3 * num4;
			}
			if (bounds.Contains(vector))
			{
				return new PointD(num, num2);
			}
			return new PointD(-num, -num2);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000713B8 File Offset: 0x0006F5B8
		public PointD GetPositionForNode(Node node)
		{
			Node node2 = this.nodeList.Find((N n) => n.node == node);
			return new PointD((double)node2.position.x, (double)node2.position.y);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x0007140C File Offset: 0x0006F60C
		public void SetInitialNodePositions(Polygon bounds)
		{
			List<Vector2> list = PointGenerator.GetRandomPoints(bounds, 50f, 0f, null, PointGenerator.SampleBehaviour.PoissonDisk, true, this.myRandom, true, true);
			int num = 0;
			for (int i = 0; i < this.nodeList.Count; i++)
			{
				if (num == list.Count - 1)
				{
					list = PointGenerator.GetRandomPoints(bounds, 10f, 20f, list, PointGenerator.SampleBehaviour.PoissonDisk, true, this.myRandom, true, true);
					num = 0;
				}
				this.nodeList[i].SetPosition(list[num++]);
			}
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x00071498 File Offset: 0x0006F698
		public bool Layout(Polygon bounds = null)
		{
			bool flag = false;
			int num = 0;
			Vector2 vector = default(Vector2);
			Func<Node, PointD> <>9__0;
			Func<PointD, PointD> <>9__1;
			while (!flag && num < 100)
			{
				flag = true;
				Func<Node, PointD> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Node n) => this.GetPositionForNode(n));
				}
				Func<Node, PointD> func2 = func;
				IGraph baseGraph = this.baseGraph;
				int num2 = num;
				ForceDirectedLayout forceDirectedLayout = new ForceDirectedLayout(baseGraph, func2, num2);
				ForceDirectedLayout forceDirectedLayout2 = forceDirectedLayout;
				Func<PointD, PointD> func3;
				if ((func3 = <>9__1) == null)
				{
					func3 = (<>9__1 = (PointD point) => Graph<N, A>.GetForceForBoundry(point, bounds));
				}
				forceDirectedLayout2.ExternalForce = func3;
				forceDirectedLayout.Run(0.01);
				IEnumerator<Node> enumerator = this.baseGraph.Nodes().GetEnumerator();
				int num3 = 0;
				while (enumerator.MoveNext())
				{
					Node node = enumerator.Current;
					Node node2 = this.nodeList.Find((N n) => n.node == node);
					if (node2 != null)
					{
						vector.x = (float)forceDirectedLayout.NodePositions[node].X;
						vector.y = (float)forceDirectedLayout.NodePositions[node].Y;
						if (!bounds.Contains(vector))
						{
							flag = false;
							global::Debug.LogWarning("Re-doing layout - cell was off map");
							break;
						}
						node2.SetPosition(vector);
					}
					if (!flag)
					{
						break;
					}
					num3++;
				}
				num++;
			}
			if (num >= 10)
			{
				global::Debug.LogWarning("Re-ran layout " + num.ToString() + " times");
			}
			return flag;
		}

		// Token: 0x0400122D RID: 4653
		[Serialize]
		public List<N> nodeList;

		// Token: 0x0400122E RID: 4654
		[Serialize]
		public List<A> arcList;

		// Token: 0x04001230 RID: 4656
		private SeededRandom myRandom;
	}
}
