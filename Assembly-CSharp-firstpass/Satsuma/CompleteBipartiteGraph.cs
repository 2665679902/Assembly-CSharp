using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000249 RID: 585
	public sealed class CompleteBipartiteGraph : IGraph, IArcLookup
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00046611 File Offset: 0x00044811
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00046619 File Offset: 0x00044819
		public int RedNodeCount { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00046622 File Offset: 0x00044822
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0004662A File Offset: 0x0004482A
		public int BlueNodeCount { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00046633 File Offset: 0x00044833
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0004663B File Offset: 0x0004483B
		public bool Directed { get; private set; }

		// Token: 0x060011B2 RID: 4530 RVA: 0x00046644 File Offset: 0x00044844
		public CompleteBipartiteGraph(int redNodeCount, int blueNodeCount, Directedness directedness)
		{
			if (redNodeCount < 0 || blueNodeCount < 0)
			{
				throw new ArgumentException("Invalid node count: " + redNodeCount.ToString() + ";" + blueNodeCount.ToString());
			}
			if ((long)redNodeCount + (long)blueNodeCount > 2147483647L || (long)redNodeCount * (long)blueNodeCount > 2147483647L)
			{
				throw new ArgumentException("Too many nodes: " + redNodeCount.ToString() + ";" + blueNodeCount.ToString());
			}
			this.RedNodeCount = redNodeCount;
			this.BlueNodeCount = blueNodeCount;
			this.Directed = directedness == Directedness.Directed;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000466D7 File Offset: 0x000448D7
		public Node GetRedNode(int index)
		{
			return new Node(1L + (long)index);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000466E3 File Offset: 0x000448E3
		public Node GetBlueNode(int index)
		{
			return new Node(1L + (long)this.RedNodeCount + (long)index);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000466F7 File Offset: 0x000448F7
		public bool IsRed(Node node)
		{
			return node.Id <= (long)this.RedNodeCount;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004670C File Offset: 0x0004490C
		public Arc GetArc(Node u, Node v)
		{
			bool flag = this.IsRed(u);
			bool flag2 = this.IsRed(v);
			if (flag == flag2)
			{
				return Arc.Invalid;
			}
			if (flag2)
			{
				Node node = u;
				u = v;
				v = node;
			}
			int num = (int)(u.Id - 1L);
			int num2 = (int)(v.Id - (long)this.RedNodeCount - 1L);
			return new Arc(1L + (long)num2 * (long)this.RedNodeCount + (long)num);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004676F File Offset: 0x0004496F
		public Node U(Arc arc)
		{
			return new Node(1L + (arc.Id - 1L) % (long)this.RedNodeCount);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004678B File Offset: 0x0004498B
		public Node V(Arc arc)
		{
			return new Node(1L + (long)this.RedNodeCount + (arc.Id - 1L) / (long)this.RedNodeCount);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000467AF File Offset: 0x000449AF
		public bool IsEdge(Arc arc)
		{
			return !this.Directed;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000467BA File Offset: 0x000449BA
		public IEnumerable<Node> Nodes(CompleteBipartiteGraph.Color color)
		{
			if (color != CompleteBipartiteGraph.Color.Red)
			{
				if (color == CompleteBipartiteGraph.Color.Blue)
				{
					int num;
					for (int i = 0; i < this.BlueNodeCount; i = num + 1)
					{
						yield return this.GetBlueNode(i);
						num = i;
					}
				}
			}
			else
			{
				int num;
				for (int i = 0; i < this.RedNodeCount; i = num + 1)
				{
					yield return this.GetRedNode(i);
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000467D1 File Offset: 0x000449D1
		public IEnumerable<Node> Nodes()
		{
			int num;
			for (int i = 0; i < this.RedNodeCount; i = num + 1)
			{
				yield return this.GetRedNode(i);
				num = i;
			}
			for (int i = 0; i < this.BlueNodeCount; i = num + 1)
			{
				yield return this.GetBlueNode(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000467E1 File Offset: 0x000449E1
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			if (this.Directed && filter == ArcFilter.Edge)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.RedNodeCount; i = num + 1)
			{
				for (int j = 0; j < this.BlueNodeCount; j = num + 1)
				{
					yield return this.GetArc(this.GetRedNode(i), this.GetBlueNode(j));
					num = j;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x000467F8 File Offset: 0x000449F8
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			bool flag = this.IsRed(u);
			if (this.Directed && (filter == ArcFilter.Edge || (filter == ArcFilter.Forward && !flag) || (filter == ArcFilter.Backward && flag)))
			{
				yield break;
			}
			if (flag)
			{
				int num;
				for (int i = 0; i < this.BlueNodeCount; i = num + 1)
				{
					yield return this.GetArc(u, this.GetBlueNode(i));
					num = i;
				}
			}
			else
			{
				int num;
				for (int i = 0; i < this.RedNodeCount; i = num + 1)
				{
					yield return this.GetArc(this.GetRedNode(i), u);
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00046816 File Offset: 0x00044A16
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			Arc arc = this.GetArc(u, v);
			if (arc != Arc.Invalid && this.ArcCount(u, filter) > 0)
			{
				yield return arc;
			}
			yield break;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004683B File Offset: 0x00044A3B
		public int NodeCount()
		{
			return this.RedNodeCount + this.BlueNodeCount;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004684A File Offset: 0x00044A4A
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			if (this.Directed && filter == ArcFilter.Edge)
			{
				return 0;
			}
			return this.RedNodeCount * this.BlueNodeCount;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00046868 File Offset: 0x00044A68
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			bool flag = this.IsRed(u);
			if (this.Directed && (filter == ArcFilter.Edge || (filter == ArcFilter.Forward && !flag) || (filter == ArcFilter.Backward && flag)))
			{
				return 0;
			}
			if (!flag)
			{
				return this.RedNodeCount;
			}
			return this.BlueNodeCount;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000468AA File Offset: 0x00044AAA
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			if (this.IsRed(u) == this.IsRed(v))
			{
				return 0;
			}
			if (this.ArcCount(u, filter) <= 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000468CC File Offset: 0x00044ACC
		public bool HasNode(Node node)
		{
			return node.Id >= 1L && node.Id <= (long)(this.RedNodeCount + this.BlueNodeCount);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000468F5 File Offset: 0x00044AF5
		public bool HasArc(Arc arc)
		{
			return arc.Id >= 1L && arc.Id <= (long)(this.RedNodeCount * this.BlueNodeCount);
		}

		// Token: 0x02000A70 RID: 2672
		public enum Color
		{
			// Token: 0x0400238A RID: 9098
			Red,
			// Token: 0x0400238B RID: 9099
			Blue
		}
	}
}
