using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x0200026B RID: 619
	public sealed class NetworkSimplex : IClearable
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00048824 File Offset: 0x00046A24
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x0004882C File Offset: 0x00046A2C
		public IGraph Graph { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00048835 File Offset: 0x00046A35
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x0004883D File Offset: 0x00046A3D
		public Func<Arc, long> LowerBound { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00048846 File Offset: 0x00046A46
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x0004884E File Offset: 0x00046A4E
		public Func<Arc, long> UpperBound { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00048857 File Offset: 0x00046A57
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x0004885F File Offset: 0x00046A5F
		public Func<Node, long> Supply { get; private set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00048868 File Offset: 0x00046A68
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00048870 File Offset: 0x00046A70
		public Func<Arc, double> Cost { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00048879 File Offset: 0x00046A79
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00048881 File Offset: 0x00046A81
		public SimplexState State { get; private set; }

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004888C File Offset: 0x00046A8C
		public NetworkSimplex(IGraph graph, Func<Arc, long> lowerBound = null, Func<Arc, long> upperBound = null, Func<Node, long> supply = null, Func<Arc, double> cost = null)
		{
			this.Graph = graph;
			Func<Arc, long> func = lowerBound;
			if (lowerBound == null && (func = NetworkSimplex.<>c.<>9__33_0) == null)
			{
				func = (NetworkSimplex.<>c.<>9__33_0 = (Arc x) => 0L);
			}
			this.LowerBound = func;
			Func<Arc, long> func2 = upperBound;
			if (upperBound == null && (func2 = NetworkSimplex.<>c.<>9__33_1) == null)
			{
				func2 = (NetworkSimplex.<>c.<>9__33_1 = (Arc x) => long.MaxValue);
			}
			this.UpperBound = func2;
			Func<Node, long> func3 = supply;
			if (supply == null && (func3 = NetworkSimplex.<>c.<>9__33_2) == null)
			{
				func3 = (NetworkSimplex.<>c.<>9__33_2 = (Node x) => 0L);
			}
			this.Supply = func3;
			Func<Arc, double> func4 = cost;
			if (cost == null && (func4 = NetworkSimplex.<>c.<>9__33_3) == null)
			{
				func4 = (NetworkSimplex.<>c.<>9__33_3 = (Arc x) => 1.0);
			}
			this.Cost = func4;
			this.Epsilon = 1.0;
			foreach (Arc arc in graph.Arcs(ArcFilter.All))
			{
				double num = Math.Abs(this.Cost(arc));
				if (num > 0.0 && num < this.Epsilon)
				{
					this.Epsilon = num;
				}
			}
			this.Epsilon *= 1E-12;
			this.Clear();
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000489E4 File Offset: 0x00046BE4
		public long Flow(Arc arc)
		{
			if (this.Saturated.Contains(arc))
			{
				return this.UpperBound(arc);
			}
			long num;
			if (this.Tree.TryGetValue(arc, out num))
			{
				return num;
			}
			num = this.LowerBound(arc);
			if (num != -9223372036854775808L)
			{
				return num;
			}
			return 0L;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00048A3B File Offset: 0x00046C3B
		public IEnumerable<KeyValuePair<Arc, long>> Forest
		{
			get
			{
				return this.Tree.Where((KeyValuePair<Arc, long> kv) => this.Graph.HasArc(kv.Key));
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00048A54 File Offset: 0x00046C54
		public IEnumerable<Arc> UpperBoundArcs
		{
			get
			{
				return this.Saturated;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00048A5C File Offset: 0x00046C5C
		public void Clear()
		{
			Dictionary<Node, long> dictionary = new Dictionary<Node, long>();
			foreach (Node node in this.Graph.Nodes())
			{
				dictionary[node] = this.Supply(node);
			}
			this.Saturated = new HashSet<Arc>();
			foreach (Arc arc in this.Graph.Arcs(ArcFilter.All))
			{
				this.LowerBound(arc);
				if (this.UpperBound(arc) < 9223372036854775807L)
				{
					this.Saturated.Add(arc);
				}
				long num = this.Flow(arc);
				Dictionary<Node, long> dictionary2 = dictionary;
				Node node2 = this.Graph.U(arc);
				dictionary2[node2] -= num;
				dictionary2 = dictionary;
				node2 = this.Graph.V(arc);
				dictionary2[node2] += num;
			}
			this.Potential = new Dictionary<Node, double>();
			this.MyGraph = new Supergraph(this.Graph);
			this.ArtificialNode = this.MyGraph.AddNode();
			this.Potential[this.ArtificialNode] = 0.0;
			this.ArtificialArcs = new HashSet<Arc>();
			Dictionary<Node, Arc> dictionary3 = new Dictionary<Node, Arc>();
			foreach (Node node3 in this.Graph.Nodes())
			{
				long num2 = dictionary[node3];
				Arc arc2 = ((num2 > 0L) ? this.MyGraph.AddArc(node3, this.ArtificialNode, Directedness.Directed) : this.MyGraph.AddArc(this.ArtificialNode, node3, Directedness.Directed));
				this.Potential[node3] = (double)((num2 > 0L) ? (-1) : 1);
				this.ArtificialArcs.Add(arc2);
				dictionary3[node3] = arc2;
			}
			this.Tree = new Dictionary<Arc, long>();
			this.TreeSubgraph = new Subgraph(this.MyGraph);
			this.TreeSubgraph.EnableAllArcs(false);
			foreach (KeyValuePair<Node, Arc> keyValuePair in dictionary3)
			{
				this.Tree[keyValuePair.Value] = Math.Abs(dictionary[keyValuePair.Key]);
				this.TreeSubgraph.Enable(keyValuePair.Value, true);
			}
			this.State = SimplexState.FirstPhase;
			this.EnteringArcEnumerator = this.MyGraph.Arcs(ArcFilter.All).GetEnumerator();
			this.EnteringArcEnumerator.MoveNext();
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00048D60 File Offset: 0x00046F60
		private long ActualLowerBound(Arc arc)
		{
			if (!this.ArtificialArcs.Contains(arc))
			{
				return this.LowerBound(arc);
			}
			return 0L;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00048D7F File Offset: 0x00046F7F
		private long ActualUpperBound(Arc arc)
		{
			if (!this.ArtificialArcs.Contains(arc))
			{
				return this.UpperBound(arc);
			}
			if (this.State != SimplexState.FirstPhase)
			{
				return 0L;
			}
			return long.MaxValue;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00048DB0 File Offset: 0x00046FB0
		private double ActualCost(Arc arc)
		{
			if (this.ArtificialArcs.Contains(arc))
			{
				return 1.0;
			}
			if (this.State != SimplexState.FirstPhase)
			{
				return this.Cost(arc);
			}
			return 0.0;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00048DE8 File Offset: 0x00046FE8
		private static ulong MySubtract(long a, long b)
		{
			if (a == 9223372036854775807L || b == -9223372036854775808L)
			{
				return ulong.MaxValue;
			}
			return (ulong)(a - b);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00048E08 File Offset: 0x00047008
		public void Step()
		{
			if (this.State != SimplexState.FirstPhase && this.State != SimplexState.SecondPhase)
			{
				return;
			}
			Arc arc = this.EnteringArcEnumerator.Current;
			Arc arc2 = Arc.Invalid;
			double num = double.NaN;
			bool flag = false;
			Arc arc3;
			bool flag2;
			double num2;
			for (;;)
			{
				arc3 = this.EnteringArcEnumerator.Current;
				if (!this.Tree.ContainsKey(arc3))
				{
					flag2 = this.Saturated.Contains(arc3);
					num2 = this.ActualCost(arc3) - (this.Potential[this.MyGraph.V(arc3)] - this.Potential[this.MyGraph.U(arc3)]);
					if ((num2 < -this.Epsilon && !flag2) || (num2 > this.Epsilon && (flag2 || this.ActualLowerBound(arc3) == -9223372036854775808L)))
					{
						break;
					}
				}
				if (!this.EnteringArcEnumerator.MoveNext())
				{
					this.EnteringArcEnumerator = this.MyGraph.Arcs(ArcFilter.All).GetEnumerator();
					this.EnteringArcEnumerator.MoveNext();
				}
				if (this.EnteringArcEnumerator.Current == arc)
				{
					goto IL_11B;
				}
			}
			arc2 = arc3;
			num = num2;
			flag = flag2;
			IL_11B:
			if (arc2 == Arc.Invalid)
			{
				if (this.State == SimplexState.FirstPhase)
				{
					this.State = SimplexState.SecondPhase;
					foreach (Arc arc4 in this.ArtificialArcs)
					{
						if (this.Flow(arc4) > 0L)
						{
							this.State = SimplexState.Infeasible;
							break;
						}
					}
					if (this.State == SimplexState.SecondPhase)
					{
						new NetworkSimplex.RecalculatePotentialDfs
						{
							Parent = this
						}.Run(this.TreeSubgraph, null);
						return;
					}
				}
				else
				{
					this.State = SimplexState.Optimal;
				}
				return;
			}
			Node node = this.MyGraph.U(arc2);
			Node node2 = this.MyGraph.V(arc2);
			List<Arc> list = new List<Arc>();
			List<Arc> list2 = new List<Arc>();
			IPath path = this.TreeSubgraph.FindPath(node2, node, Dfs.Direction.Undirected);
			foreach (Node node3 in path.Nodes())
			{
				Arc arc5 = path.NextArc(node3);
				((this.MyGraph.U(arc5) == node3) ? list : list2).Add(arc5);
			}
			ulong num3 = ((num < 0.0) ? NetworkSimplex.MySubtract(this.ActualUpperBound(arc2), this.Flow(arc2)) : NetworkSimplex.MySubtract(this.Flow(arc2), this.ActualLowerBound(arc2)));
			Arc arc6 = arc2;
			bool flag3 = !flag;
			foreach (Arc arc7 in list)
			{
				ulong num4 = ((num < 0.0) ? NetworkSimplex.MySubtract(this.ActualUpperBound(arc7), this.Tree[arc7]) : NetworkSimplex.MySubtract(this.Tree[arc7], this.ActualLowerBound(arc7)));
				if (num4 < num3)
				{
					num3 = num4;
					arc6 = arc7;
					flag3 = num < 0.0;
				}
			}
			foreach (Arc arc8 in list2)
			{
				ulong num5 = ((num > 0.0) ? NetworkSimplex.MySubtract(this.ActualUpperBound(arc8), this.Tree[arc8]) : NetworkSimplex.MySubtract(this.Tree[arc8], this.ActualLowerBound(arc8)));
				if (num5 < num3)
				{
					num3 = num5;
					arc6 = arc8;
					flag3 = num > 0.0;
				}
			}
			long num6 = 0L;
			if (num3 != 0UL)
			{
				if (num3 == 18446744073709551615UL)
				{
					this.State = SimplexState.Unbounded;
					return;
				}
				num6 = (long)((num < 0.0) ? num3 : (-(long)num3));
				foreach (Arc arc9 in list)
				{
					Dictionary<Arc, long> dictionary = this.Tree;
					Arc arc10 = arc9;
					dictionary[arc10] += num6;
				}
				foreach (Arc arc11 in list2)
				{
					Dictionary<Arc, long> dictionary = this.Tree;
					Arc arc10 = arc11;
					dictionary[arc10] -= num6;
				}
			}
			if (!(arc6 == arc2))
			{
				this.Tree.Remove(arc6);
				this.TreeSubgraph.Enable(arc6, false);
				if (flag3)
				{
					this.Saturated.Add(arc6);
				}
				double num7 = this.ActualCost(arc2) - (this.Potential[node2] - this.Potential[node]);
				if (num7 != 0.0)
				{
					new NetworkSimplex.UpdatePotentialDfs
					{
						Parent = this,
						Diff = num7
					}.Run(this.TreeSubgraph, new Node[] { node2 });
				}
				this.Tree[arc2] = this.Flow(arc2) + num6;
				if (flag)
				{
					this.Saturated.Remove(arc2);
				}
				this.TreeSubgraph.Enable(arc2, true);
				return;
			}
			if (flag)
			{
				this.Saturated.Remove(arc2);
				return;
			}
			this.Saturated.Add(arc2);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000493A8 File Offset: 0x000475A8
		public void Run()
		{
			while (this.State == SimplexState.FirstPhase || this.State == SimplexState.SecondPhase)
			{
				this.Step();
			}
		}

		// Token: 0x040009C7 RID: 2503
		private double Epsilon;

		// Token: 0x040009C8 RID: 2504
		private Supergraph MyGraph;

		// Token: 0x040009C9 RID: 2505
		private Node ArtificialNode;

		// Token: 0x040009CA RID: 2506
		private HashSet<Arc> ArtificialArcs;

		// Token: 0x040009CB RID: 2507
		private Dictionary<Arc, long> Tree;

		// Token: 0x040009CC RID: 2508
		private Subgraph TreeSubgraph;

		// Token: 0x040009CD RID: 2509
		private HashSet<Arc> Saturated;

		// Token: 0x040009CE RID: 2510
		private Dictionary<Node, double> Potential;

		// Token: 0x040009CF RID: 2511
		private IEnumerator<Arc> EnteringArcEnumerator;

		// Token: 0x02000A91 RID: 2705
		private class RecalculatePotentialDfs : Dfs
		{
			// Token: 0x06005671 RID: 22129 RVA: 0x000A146F File Offset: 0x0009F66F
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Undirected;
			}

			// Token: 0x06005672 RID: 22130 RVA: 0x000A1474 File Offset: 0x0009F674
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if (arc == Arc.Invalid)
				{
					this.Parent.Potential[node] = 0.0;
				}
				else
				{
					Node node2 = this.Parent.MyGraph.Other(arc, node);
					this.Parent.Potential[node] = this.Parent.Potential[node2] + ((node == this.Parent.MyGraph.V(arc)) ? this.Parent.ActualCost(arc) : (-this.Parent.ActualCost(arc)));
				}
				return true;
			}

			// Token: 0x04002433 RID: 9267
			public NetworkSimplex Parent;
		}

		// Token: 0x02000A92 RID: 2706
		private class UpdatePotentialDfs : Dfs
		{
			// Token: 0x06005674 RID: 22132 RVA: 0x000A151D File Offset: 0x0009F71D
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Undirected;
			}

			// Token: 0x06005675 RID: 22133 RVA: 0x000A1524 File Offset: 0x0009F724
			protected override bool NodeEnter(Node node, Arc arc)
			{
				Dictionary<Node, double> potential = this.Parent.Potential;
				potential[node] += this.Diff;
				return true;
			}

			// Token: 0x04002434 RID: 9268
			public NetworkSimplex Parent;

			// Token: 0x04002435 RID: 9269
			public double Diff;
		}
	}
}
