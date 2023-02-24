using System;

namespace MIConvexHull
{
	// Token: 0x020004AE RID: 1198
	public class VoronoiEdge<TVertex, TCell> where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>
	{
		// Token: 0x0600332F RID: 13103 RVA: 0x0006C93C File Offset: 0x0006AB3C
		public VoronoiEdge()
		{
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x0006C944 File Offset: 0x0006AB44
		public VoronoiEdge(TCell source, TCell target)
		{
			this.Source = source;
			this.Target = target;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x0006C95A File Offset: 0x0006AB5A
		// (set) Token: 0x06003332 RID: 13106 RVA: 0x0006C962 File Offset: 0x0006AB62
		public TCell Source { get; internal set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x0006C96B File Offset: 0x0006AB6B
		// (set) Token: 0x06003334 RID: 13108 RVA: 0x0006C973 File Offset: 0x0006AB73
		public TCell Target { get; internal set; }

		// Token: 0x06003335 RID: 13109 RVA: 0x0006C97C File Offset: 0x0006AB7C
		public override bool Equals(object obj)
		{
			VoronoiEdge<TVertex, TCell> voronoiEdge = obj as VoronoiEdge<TVertex, TCell>;
			return voronoiEdge != null && (this == voronoiEdge || (this.Source == voronoiEdge.Source && this.Target == voronoiEdge.Target) || (this.Source == voronoiEdge.Target && this.Target == voronoiEdge.Source));
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x0006C9FF File Offset: 0x0006ABFF
		public override int GetHashCode()
		{
			return (23 * 31 + this.Source.GetHashCode()) * 31 + this.Target.GetHashCode();
		}
	}
}
