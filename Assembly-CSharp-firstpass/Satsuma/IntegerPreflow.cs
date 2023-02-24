using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000272 RID: 626
	public sealed class IntegerPreflow : IFlow<long>
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00049FB0 File Offset: 0x000481B0
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x00049FB8 File Offset: 0x000481B8
		public IGraph Graph { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00049FC1 File Offset: 0x000481C1
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x00049FC9 File Offset: 0x000481C9
		public Func<Arc, long> Capacity { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x00049FD2 File Offset: 0x000481D2
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x00049FDA File Offset: 0x000481DA
		public Node Source { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00049FE3 File Offset: 0x000481E3
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x00049FEB File Offset: 0x000481EB
		public Node Target { get; private set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00049FF4 File Offset: 0x000481F4
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x00049FFC File Offset: 0x000481FC
		public long FlowSize { get; private set; }

		// Token: 0x06001327 RID: 4903 RVA: 0x0004A008 File Offset: 0x00048208
		public IntegerPreflow(IGraph graph, Func<Arc, long> capacity, Node source, Node target)
		{
			this.Graph = graph;
			this.Capacity = capacity;
			this.Source = source;
			this.Target = target;
			this.flow = new Dictionary<Arc, long>();
			this.excess = new Dictionary<Node, long>();
			this.label = new Dictionary<Node, long>();
			this.active = new PriorityQueue<Node, long>();
			this.Run();
			this.excess = null;
			this.label = null;
			this.active = null;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004A080 File Offset: 0x00048280
		private void Run()
		{
			foreach (Node node in this.Graph.Nodes())
			{
				this.label[node] = (long)((node == this.Source) ? (-this.Graph.NodeCount()) : 0);
				this.excess[node] = 0L;
			}
			long num = 0L;
			foreach (Arc arc in this.Graph.Arcs(this.Source, ArcFilter.Forward))
			{
				Node node2 = this.Graph.Other(arc, this.Source);
				if (!(node2 == this.Source))
				{
					long num2 = ((this.Graph.U(arc) == this.Source) ? this.Capacity(arc) : (-this.Capacity(arc)));
					if (num2 != 0L)
					{
						this.flow[arc] = num2;
						num2 = Math.Abs(num2);
						Dictionary<Node, long> dictionary;
						Node node3;
						checked
						{
							num += num2;
							dictionary = this.excess;
							node3 = node2;
						}
						dictionary[node3] += num2;
						if (node2 != this.Target)
						{
							this.active[node2] = 0L;
						}
					}
				}
			}
			this.excess[this.Source] = -num;
			while (this.active.Count > 0)
			{
				long num3;
				Node node4 = this.active.Peek(out num3);
				this.active.Pop();
				long num4 = this.excess[node4];
				long num5 = long.MinValue;
				foreach (Arc arc2 in this.Graph.Arcs(node4, ArcFilter.All))
				{
					Node node5 = this.Graph.U(arc2);
					Node node6 = this.Graph.V(arc2);
					if (!(node5 == node6))
					{
						Node node7 = ((node4 == node5) ? node6 : node5);
						bool flag = this.Graph.IsEdge(arc2);
						long num6;
						this.flow.TryGetValue(arc2, out num6);
						long num7 = this.Capacity(arc2);
						long num8 = (flag ? (-this.Capacity(arc2)) : 0L);
						if (node5 == node4)
						{
							if (num6 != num7)
							{
								long num9 = this.label[node7];
								if (num9 <= num3)
								{
									num5 = Math.Max(num5, num9 - 1L);
								}
								else
								{
									long num10 = (long)Math.Min((ulong)num4, (ulong)(num7 - num6));
									this.flow[arc2] = num6 + num10;
									Dictionary<Node, long> dictionary = this.excess;
									Node node3 = node6;
									dictionary[node3] += num10;
									if (node6 != this.Source && node6 != this.Target)
									{
										this.active[node6] = this.label[node6];
									}
									num4 -= num10;
									if (num4 == 0L)
									{
										break;
									}
								}
							}
						}
						else if (num6 != num8)
						{
							long num11 = this.label[node7];
							if (num11 <= num3)
							{
								num5 = Math.Max(num5, num11 - 1L);
							}
							else
							{
								long num12 = (long)Math.Min((ulong)num4, (ulong)(num6 - num8));
								this.flow[arc2] = num6 - num12;
								Dictionary<Node, long> dictionary = this.excess;
								Node node3 = node5;
								dictionary[node3] += num12;
								if (node5 != this.Source && node5 != this.Target)
								{
									this.active[node5] = this.label[node5];
								}
								num4 -= num12;
								if (num4 == 0L)
								{
									break;
								}
							}
						}
					}
				}
				this.excess[node4] = num4;
				if (num4 > 0L)
				{
					if (num5 == -9223372036854775808L)
					{
						throw new InvalidOperationException("Internal error.");
					}
					this.active[node4] = (this.label[node4] = (num3 = num5));
				}
			}
			this.FlowSize = 0L;
			foreach (Arc arc3 in this.Graph.Arcs(this.Source, ArcFilter.All))
			{
				Node node8 = this.Graph.U(arc3);
				Node node9 = this.Graph.V(arc3);
				long num13;
				if (!(node8 == node9) && this.flow.TryGetValue(arc3, out num13))
				{
					if (node8 == this.Source)
					{
						this.FlowSize += num13;
					}
					else
					{
						this.FlowSize -= num13;
					}
				}
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0004A5E8 File Offset: 0x000487E8
		public IEnumerable<KeyValuePair<Arc, long>> NonzeroArcs
		{
			get
			{
				return this.flow.Where((KeyValuePair<Arc, long> kv) => kv.Value != 0L);
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004A614 File Offset: 0x00048814
		public long Flow(Arc arc)
		{
			long num;
			this.flow.TryGetValue(arc, out num);
			return num;
		}

		// Token: 0x040009EB RID: 2539
		private readonly Dictionary<Arc, long> flow;

		// Token: 0x040009EC RID: 2540
		private readonly Dictionary<Node, long> excess;

		// Token: 0x040009ED RID: 2541
		private readonly Dictionary<Node, long> label;

		// Token: 0x040009EE RID: 2542
		private readonly PriorityQueue<Node, long> active;
	}
}
