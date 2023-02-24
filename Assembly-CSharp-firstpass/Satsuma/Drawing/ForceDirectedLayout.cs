using System;
using System.Collections.Generic;

namespace Satsuma.Drawing
{
	// Token: 0x02000288 RID: 648
	public sealed class ForceDirectedLayout
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0004CA55 File Offset: 0x0004AC55
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0004CA5D File Offset: 0x0004AC5D
		public IGraph Graph { get; private set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0004CA66 File Offset: 0x0004AC66
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0004CA6E File Offset: 0x0004AC6E
		public Dictionary<Node, PointD> NodePositions { get; private set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0004CA77 File Offset: 0x0004AC77
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x0004CA7F File Offset: 0x0004AC7F
		public Func<double, double> SpringForce { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0004CA88 File Offset: 0x0004AC88
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0004CA90 File Offset: 0x0004AC90
		public Func<double, double> ElectricForce { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0004CA99 File Offset: 0x0004AC99
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x0004CAA1 File Offset: 0x0004ACA1
		public Func<PointD, PointD> ExternalForce { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0004CAAA File Offset: 0x0004ACAA
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0004CAB2 File Offset: 0x0004ACB2
		public double Temperature { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0004CABB File Offset: 0x0004ACBB
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0004CAC3 File Offset: 0x0004ACC3
		public double TemperatureAttenuation { get; set; }

		// Token: 0x06001407 RID: 5127 RVA: 0x0004CACC File Offset: 0x0004ACCC
		public ForceDirectedLayout(IGraph graph, Func<Node, PointD> initialPositions = null, int seed = -1)
		{
			this.Graph = graph;
			this.NodePositions = new Dictionary<Node, PointD>();
			this.SpringForce = (double d) => 2.0 * Math.Log(d);
			this.ElectricForce = (double d) => 1.0 / (d * d);
			this.ExternalForce = null;
			this.TemperatureAttenuation = 0.95;
			this.Initialize(initialPositions, seed);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0004CB5C File Offset: 0x0004AD5C
		public void Initialize(Func<Node, PointD> initialPositions = null, int seed = -1)
		{
			if (initialPositions == null)
			{
				Random r;
				if (seed == -1)
				{
					r = new Random();
				}
				else
				{
					r = new Random(seed);
				}
				initialPositions = (Node node) => new PointD(r.NextDouble(), r.NextDouble());
			}
			foreach (Node node2 in this.Graph.Nodes())
			{
				this.NodePositions[node2] = initialPositions(node2);
			}
			this.Temperature = 0.2;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0004CC00 File Offset: 0x0004AE00
		public void Step()
		{
			Dictionary<Node, PointD> dictionary = new Dictionary<Node, PointD>();
			foreach (Node node in this.Graph.Nodes())
			{
				PointD pointD = this.NodePositions[node];
				double num = 0.0;
				double num2 = 0.0;
				foreach (Arc arc in this.Graph.Arcs(node, ArcFilter.All))
				{
					PointD pointD2 = this.NodePositions[this.Graph.Other(arc, node)];
					double num3 = pointD.Distance(pointD2);
					double num4 = this.Temperature * this.SpringForce(num3);
					num += (pointD2.X - pointD.X) / num3 * num4;
					num2 += (pointD2.Y - pointD.Y) / num3 * num4;
				}
				foreach (Node node2 in this.Graph.Nodes())
				{
					if (!(node2 == node))
					{
						PointD pointD3 = this.NodePositions[node2];
						double num5 = pointD.Distance(pointD3);
						double num6 = this.Temperature * this.ElectricForce(num5);
						num += (pointD.X - pointD3.X) / num5 * num6;
						num2 += (pointD.Y - pointD3.Y) / num5 * num6;
					}
				}
				if (this.ExternalForce != null)
				{
					PointD pointD4 = this.ExternalForce(pointD);
					num += this.Temperature * pointD4.X;
					num2 += this.Temperature * pointD4.Y;
				}
				dictionary[node] = new PointD(num, num2);
			}
			foreach (Node node3 in this.Graph.Nodes())
			{
				Dictionary<Node, PointD> nodePositions = this.NodePositions;
				Node node4 = node3;
				nodePositions[node4] += dictionary[node3];
			}
			this.Temperature *= this.TemperatureAttenuation;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0004CED4 File Offset: 0x0004B0D4
		public void Run(double minimumTemperature = 0.01)
		{
			while (this.Temperature > minimumTemperature)
			{
				this.Step();
			}
		}

		// Token: 0x04000A2A RID: 2602
		public const double DefaultStartingTemperature = 0.2;

		// Token: 0x04000A2B RID: 2603
		public const double DefaultMinimumTemperature = 0.01;

		// Token: 0x04000A2C RID: 2604
		public const double DefaultTemperatureAttenuation = 0.95;
	}
}
