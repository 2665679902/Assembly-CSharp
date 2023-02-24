using System;
using System.Collections.Generic;
using Delaunay.Geo;
using KSerialization;
using ProcGen;
using ProcGenGame;

namespace Klei
{
	// Token: 0x02000D56 RID: 3414
	public class WorldDetailSave
	{
		// Token: 0x06006856 RID: 26710 RVA: 0x0028A635 File Offset: 0x00288835
		public WorldDetailSave()
		{
			this.overworldCells = new List<WorldDetailSave.OverworldCell>();
		}

		// Token: 0x04004E92 RID: 20114
		public List<WorldDetailSave.OverworldCell> overworldCells;

		// Token: 0x04004E93 RID: 20115
		public int globalWorldSeed;

		// Token: 0x04004E94 RID: 20116
		public int globalWorldLayoutSeed;

		// Token: 0x04004E95 RID: 20117
		public int globalTerrainSeed;

		// Token: 0x04004E96 RID: 20118
		public int globalNoiseSeed;

		// Token: 0x02001E3B RID: 7739
		[SerializationConfig(MemberSerialization.OptOut)]
		public class OverworldCell
		{
			// Token: 0x06009B21 RID: 39713 RVA: 0x00335FCA File Offset: 0x003341CA
			public OverworldCell()
			{
			}

			// Token: 0x06009B22 RID: 39714 RVA: 0x00335FD2 File Offset: 0x003341D2
			public OverworldCell(SubWorld.ZoneType zoneType, TerrainCell tc)
			{
				this.poly = tc.poly;
				this.tags = tc.node.tags;
				this.zoneType = zoneType;
			}

			// Token: 0x04008817 RID: 34839
			public Polygon poly;

			// Token: 0x04008818 RID: 34840
			public TagSet tags;

			// Token: 0x04008819 RID: 34841
			public SubWorld.ZoneType zoneType;
		}
	}
}
