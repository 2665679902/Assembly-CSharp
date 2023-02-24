using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000281 RID: 641
	public sealed class Opt2Tsp<TNode> : ITsp<TNode>
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0004C2BA File Offset: 0x0004A4BA
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0004C2C2 File Offset: 0x0004A4C2
		public Func<TNode, TNode, double> Cost { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0004C2CB File Offset: 0x0004A4CB
		public IEnumerable<TNode> Tour
		{
			get
			{
				return this.tour;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0004C2D3 File Offset: 0x0004A4D3
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0004C2DB File Offset: 0x0004A4DB
		public double TourCost { get; private set; }

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004C2E4 File Offset: 0x0004A4E4
		public Opt2Tsp(Func<TNode, TNode, double> cost, IEnumerable<TNode> tour, double? tourCost)
		{
			this.Cost = cost;
			this.tour = tour.ToList<TNode>();
			this.TourCost = tourCost ?? TspUtils.GetTourCost<TNode>(tour, cost);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004C32C File Offset: 0x0004A52C
		public bool Step()
		{
			bool flag = false;
			for (int i = 0; i < this.tour.Count - 3; i++)
			{
				int j = i + 2;
				int num = this.tour.Count - ((i == 0) ? 2 : 1);
				while (j < num)
				{
					double num2 = this.Cost(this.tour[i], this.tour[j]) + this.Cost(this.tour[i + 1], this.tour[j + 1]) - (this.Cost(this.tour[i], this.tour[i + 1]) + this.Cost(this.tour[j], this.tour[j + 1]));
					if (num2 < 0.0)
					{
						this.TourCost += num2;
						this.tour.Reverse(i + 1, j - i);
						flag = true;
					}
					j++;
				}
			}
			return flag;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004C44A File Offset: 0x0004A64A
		public void Run()
		{
			while (this.Step())
			{
			}
		}

		// Token: 0x04000A21 RID: 2593
		private List<TNode> tour;
	}
}
