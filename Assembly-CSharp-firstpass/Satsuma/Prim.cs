using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000278 RID: 632
	public sealed class Prim<TCost> where TCost : IComparable<TCost>
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0004AD85 File Offset: 0x00048F85
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0004AD8D File Offset: 0x00048F8D
		public IGraph Graph { get; private set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0004AD96 File Offset: 0x00048F96
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x0004AD9E File Offset: 0x00048F9E
		public Func<Arc, TCost> Cost { get; private set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0004ADA7 File Offset: 0x00048FA7
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0004ADAF File Offset: 0x00048FAF
		public HashSet<Arc> Forest { get; private set; }

		// Token: 0x06001369 RID: 4969 RVA: 0x0004ADB8 File Offset: 0x00048FB8
		public Prim(IGraph graph, Func<Arc, TCost> cost)
		{
			this.Graph = graph;
			this.Cost = cost;
			this.Forest = new HashSet<Arc>();
			this.Run();
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0004ADE0 File Offset: 0x00048FE0
		private void Run()
		{
			this.Forest.Clear();
			PriorityQueue<Node, TCost> priorityQueue = new PriorityQueue<Node, TCost>();
			HashSet<Node> hashSet = new HashSet<Node>();
			Dictionary<Node, Arc> dictionary = new Dictionary<Node, Arc>();
			using (List<HashSet<Node>>.Enumerator enumerator = new ConnectedComponents(this.Graph, ConnectedComponents.Flags.CreateComponents).Components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HashSet<Node> hashSet2 = enumerator.Current;
					Node node = hashSet2.First<Node>();
					hashSet.Add(node);
					foreach (Arc arc in this.Graph.Arcs(node, ArcFilter.All))
					{
						Node node2 = this.Graph.Other(arc, node);
						dictionary[node2] = arc;
						priorityQueue[node2] = this.Cost(arc);
					}
				}
				goto IL_18C;
			}
			IL_D3:
			Node node3 = priorityQueue.Peek();
			priorityQueue.Pop();
			hashSet.Add(node3);
			this.Forest.Add(dictionary[node3]);
			foreach (Arc arc2 in this.Graph.Arcs(node3, ArcFilter.All))
			{
				Node node4 = this.Graph.Other(arc2, node3);
				if (!hashSet.Contains(node4))
				{
					TCost tcost = this.Cost(arc2);
					if (tcost.CompareTo(priorityQueue[node4]) < 0)
					{
						priorityQueue[node4] = tcost;
						dictionary[node4] = arc2;
					}
				}
			}
			IL_18C:
			if (priorityQueue.Count == 0)
			{
				return;
			}
			goto IL_D3;
		}
	}
}
