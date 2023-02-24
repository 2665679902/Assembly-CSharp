using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200024A RID: 586
	public sealed class CompleteGraph : IGraph, IArcLookup
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004691E File Offset: 0x00044B1E
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00046926 File Offset: 0x00044B26
		public bool Directed { get; private set; }

		// Token: 0x060011C7 RID: 4551 RVA: 0x00046930 File Offset: 0x00044B30
		public CompleteGraph(int nodeCount, Directedness directedness)
		{
			this.nodeCount = nodeCount;
			this.Directed = directedness == Directedness.Directed;
			if (nodeCount < 0)
			{
				throw new ArgumentException("Invalid node count: " + nodeCount.ToString());
			}
			long num = (long)nodeCount * (long)(nodeCount - 1);
			if (!this.Directed)
			{
				num /= 2L;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentException("Too many nodes: " + nodeCount.ToString());
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000469A4 File Offset: 0x00044BA4
		public Node GetNode(int index)
		{
			return new Node(1L + (long)index);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000469B0 File Offset: 0x00044BB0
		public int GetNodeIndex(Node node)
		{
			return (int)(node.Id - 1L);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000469C0 File Offset: 0x00044BC0
		public Arc GetArc(Node u, Node v)
		{
			int num = this.GetNodeIndex(u);
			int num2 = this.GetNodeIndex(v);
			if (num == num2)
			{
				return Arc.Invalid;
			}
			if (!this.Directed && num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return this.GetArcInternal(num, num2);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000469FF File Offset: 0x00044BFF
		private Arc GetArcInternal(int x, int y)
		{
			return new Arc(1L + (long)y * (long)this.nodeCount + (long)x);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00046A16 File Offset: 0x00044C16
		public Node U(Arc arc)
		{
			return new Node(1L + (arc.Id - 1L) % (long)this.nodeCount);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00046A32 File Offset: 0x00044C32
		public Node V(Arc arc)
		{
			return new Node(1L + (arc.Id - 1L) / (long)this.nodeCount);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00046A4E File Offset: 0x00044C4E
		public bool IsEdge(Arc arc)
		{
			return !this.Directed;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00046A59 File Offset: 0x00044C59
		public IEnumerable<Node> Nodes()
		{
			int num;
			for (int i = 0; i < this.nodeCount; i = num + 1)
			{
				yield return this.GetNode(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00046A69 File Offset: 0x00044C69
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			if (this.Directed)
			{
				int num;
				for (int i = 0; i < this.nodeCount; i = num + 1)
				{
					for (int j = 0; j < this.nodeCount; j = num + 1)
					{
						if (i != j)
						{
							yield return this.GetArcInternal(i, j);
						}
						num = j;
					}
					num = i;
				}
			}
			else
			{
				int num;
				for (int i = 0; i < this.nodeCount; i = num + 1)
				{
					for (int j = i + 1; j < this.nodeCount; j = num + 1)
					{
						yield return this.GetArcInternal(i, j);
						num = j;
					}
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00046A79 File Offset: 0x00044C79
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			if (this.Directed)
			{
				if (filter == ArcFilter.Edge)
				{
					yield break;
				}
				if (filter != ArcFilter.Forward)
				{
					foreach (Node node in this.Nodes())
					{
						if (node != u)
						{
							yield return this.GetArc(node, u);
						}
					}
					IEnumerator<Node> enumerator = null;
				}
			}
			if (!this.Directed || filter != ArcFilter.Backward)
			{
				foreach (Node node2 in this.Nodes())
				{
					if (node2 != u)
					{
						yield return this.GetArc(u, node2);
					}
				}
				IEnumerator<Node> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00046A97 File Offset: 0x00044C97
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			if (this.Directed)
			{
				if (filter == ArcFilter.Edge)
				{
					yield break;
				}
				if (filter != ArcFilter.Forward)
				{
					yield return this.GetArc(v, u);
				}
			}
			if (!this.Directed || filter != ArcFilter.Backward)
			{
				yield return this.GetArc(u, v);
			}
			yield break;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00046ABC File Offset: 0x00044CBC
		public int NodeCount()
		{
			return this.nodeCount;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00046AC4 File Offset: 0x00044CC4
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			int num = this.nodeCount * (this.nodeCount - 1);
			if (!this.Directed)
			{
				num /= 2;
			}
			return num;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00046AEE File Offset: 0x00044CEE
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			if (!this.Directed)
			{
				return this.nodeCount - 1;
			}
			if (filter == ArcFilter.All)
			{
				return 2 * (this.nodeCount - 1);
			}
			if (filter != ArcFilter.Edge)
			{
				return this.nodeCount - 1;
			}
			return 0;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00046B1F File Offset: 0x00044D1F
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			if (!this.Directed)
			{
				return 1;
			}
			if (filter == ArcFilter.All)
			{
				return 2;
			}
			if (filter != ArcFilter.Edge)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00046B39 File Offset: 0x00044D39
		public bool HasNode(Node node)
		{
			return node.Id >= 1L && node.Id <= (long)this.nodeCount;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00046B5C File Offset: 0x00044D5C
		public bool HasArc(Arc arc)
		{
			Node node = this.V(arc);
			if (!this.HasNode(node))
			{
				return false;
			}
			Node node2 = this.U(arc);
			return this.Directed || node2.Id < node.Id;
		}

		// Token: 0x04000973 RID: 2419
		private readonly int nodeCount;
	}
}
