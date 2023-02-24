using System;
using System.Diagnostics;

namespace ProcGen
{
	// Token: 0x020004E5 RID: 1253
	[DebuggerDisplay("{world} ({x}, {y})")]
	public class WorldPlacement
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000765B2 File Offset: 0x000747B2
		// (set) Token: 0x060035F7 RID: 13815 RVA: 0x000765BA File Offset: 0x000747BA
		public string world { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000765C3 File Offset: 0x000747C3
		// (set) Token: 0x060035F9 RID: 13817 RVA: 0x000765CB File Offset: 0x000747CB
		public MinMaxI allowedRings { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000765D4 File Offset: 0x000747D4
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x000765DC File Offset: 0x000747DC
		public int buffer { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000765E5 File Offset: 0x000747E5
		// (set) Token: 0x060035FD RID: 13821 RVA: 0x000765ED File Offset: 0x000747ED
		public WorldPlacement.LocationType locationType { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000765F6 File Offset: 0x000747F6
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x000765FE File Offset: 0x000747FE
		public int x { get; private set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x00076607 File Offset: 0x00074807
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x0007660F File Offset: 0x0007480F
		public int y { get; private set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x00076618 File Offset: 0x00074818
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x00076620 File Offset: 0x00074820
		public int width { get; private set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x00076629 File Offset: 0x00074829
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x00076631 File Offset: 0x00074831
		public int height { get; private set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x0007663A File Offset: 0x0007483A
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x00076642 File Offset: 0x00074842
		public bool startWorld { get; set; }

		// Token: 0x06003608 RID: 13832 RVA: 0x0007664B File Offset: 0x0007484B
		public WorldPlacement()
		{
			this.allowedRings = new MinMaxI(0, 9999);
			this.buffer = 2;
			this.locationType = WorldPlacement.LocationType.Cluster;
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x00076672 File Offset: 0x00074872
		public void SetPosition(Vector2I pos)
		{
			this.x = pos.X;
			this.y = pos.Y;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0007668E File Offset: 0x0007488E
		public void SetSize(Vector2I size)
		{
			this.width = size.X;
			this.height = size.Y;
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000766AA File Offset: 0x000748AA
		public static int CompareLocationType(WorldPlacement a, WorldPlacement b)
		{
			if (a.locationType == b.locationType)
			{
				return 0;
			}
			if (a.locationType == WorldPlacement.LocationType.Startworld)
			{
				return -1;
			}
			if (b.locationType == WorldPlacement.LocationType.Startworld)
			{
				return 1;
			}
			if (a.locationType == WorldPlacement.LocationType.InnerCluster)
			{
				return -1;
			}
			WorldPlacement.LocationType locationType = b.locationType;
			return 1;
		}

		// Token: 0x02000B0E RID: 2830
		public enum LocationType
		{
			// Token: 0x040025E7 RID: 9703
			Cluster,
			// Token: 0x040025E8 RID: 9704
			Startworld,
			// Token: 0x040025E9 RID: 9705
			InnerCluster
		}
	}
}
