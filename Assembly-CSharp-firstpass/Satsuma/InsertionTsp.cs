using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000280 RID: 640
	public sealed class InsertionTsp<TNode> : ITsp<TNode>
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0004BF70 File Offset: 0x0004A170
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x0004BF78 File Offset: 0x0004A178
		public IEnumerable<TNode> Nodes { get; private set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0004BF81 File Offset: 0x0004A181
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0004BF89 File Offset: 0x0004A189
		public Func<TNode, TNode, double> Cost { get; private set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0004BF92 File Offset: 0x0004A192
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0004BF9A File Offset: 0x0004A19A
		public TspSelectionRule SelectionRule { get; private set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0004BFA3 File Offset: 0x0004A1A3
		public IEnumerable<TNode> Tour
		{
			get
			{
				return this.tour;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0004BFAB File Offset: 0x0004A1AB
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x0004BFB3 File Offset: 0x0004A1B3
		public double TourCost { get; private set; }

		// Token: 0x060013BA RID: 5050 RVA: 0x0004BFBC File Offset: 0x0004A1BC
		public InsertionTsp(IEnumerable<TNode> nodes, Func<TNode, TNode, double> cost, TspSelectionRule selectionRule = TspSelectionRule.Farthest)
		{
			this.Nodes = nodes;
			this.Cost = cost;
			this.SelectionRule = selectionRule;
			this.tour = new LinkedList<TNode>();
			this.tourNodes = new Dictionary<TNode, LinkedListNode<TNode>>();
			this.insertableNodes = new HashSet<TNode>();
			this.insertableNodeQueue = new PriorityQueue<TNode, double>();
			this.Clear();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004C016 File Offset: 0x0004A216
		private double PriorityFromCost(double c)
		{
			if (this.SelectionRule == TspSelectionRule.Farthest)
			{
				return -c;
			}
			return c;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004C028 File Offset: 0x0004A228
		public void Clear()
		{
			this.tour.Clear();
			this.TourCost = 0.0;
			this.tourNodes.Clear();
			this.insertableNodes.Clear();
			this.insertableNodeQueue.Clear();
			if (this.Nodes.Any<TNode>())
			{
				TNode tnode = this.Nodes.First<TNode>();
				this.tour.AddFirst(tnode);
				this.tourNodes[tnode] = this.tour.AddFirst(tnode);
				foreach (TNode tnode2 in this.Nodes)
				{
					if (!tnode2.Equals(tnode))
					{
						this.insertableNodes.Add(tnode2);
						this.insertableNodeQueue[tnode2] = this.PriorityFromCost(this.Cost(tnode, tnode2));
					}
				}
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004C12C File Offset: 0x0004A32C
		public bool Insert(TNode node)
		{
			if (!this.insertableNodes.Contains(node))
			{
				return false;
			}
			this.insertableNodes.Remove(node);
			this.insertableNodeQueue.Remove(node);
			LinkedListNode<TNode> linkedListNode = null;
			double num = double.PositiveInfinity;
			for (LinkedListNode<TNode> linkedListNode2 = this.tour.First; linkedListNode2 != this.tour.Last; linkedListNode2 = linkedListNode2.Next)
			{
				LinkedListNode<TNode> next = linkedListNode2.Next;
				double num2 = this.Cost(linkedListNode2.Value, node) + this.Cost(node, next.Value);
				if (linkedListNode2 != next)
				{
					num2 -= this.Cost(linkedListNode2.Value, next.Value);
				}
				if (num2 < num)
				{
					num = num2;
					linkedListNode = linkedListNode2;
				}
			}
			this.tourNodes[node] = this.tour.AddAfter(linkedListNode, node);
			this.TourCost += num;
			foreach (TNode tnode in this.insertableNodes)
			{
				double num3 = this.PriorityFromCost(this.Cost(node, tnode));
				if (num3 < this.insertableNodeQueue[tnode])
				{
					this.insertableNodeQueue[tnode] = num3;
				}
			}
			return true;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0004C28C File Offset: 0x0004A48C
		public bool Insert()
		{
			if (this.insertableNodes.Count == 0)
			{
				return false;
			}
			this.Insert(this.insertableNodeQueue.Peek());
			return true;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004C2B0 File Offset: 0x0004A4B0
		public void Run()
		{
			while (this.Insert())
			{
			}
		}

		// Token: 0x04000A1B RID: 2587
		private LinkedList<TNode> tour;

		// Token: 0x04000A1C RID: 2588
		private Dictionary<TNode, LinkedListNode<TNode>> tourNodes;

		// Token: 0x04000A1D RID: 2589
		private HashSet<TNode> insertableNodes;

		// Token: 0x04000A1E RID: 2590
		private PriorityQueue<TNode, double> insertableNodeQueue;
	}
}
