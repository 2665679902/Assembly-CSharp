using System;
using System.Collections.Generic;
using ProcGen;
using ProcGenGame;
using VoronoiTree;

namespace Klei
{
	// Token: 0x02000D54 RID: 3412
	public class Data
	{
		// Token: 0x06006854 RID: 26708 RVA: 0x0028A5AC File Offset: 0x002887AC
		public Data()
		{
			this.worldLayout = new WorldLayout(null, 0);
			this.terrainCells = new List<TerrainCell>();
			this.overworldCells = new List<TerrainCell>();
			this.rivers = new List<ProcGen.River>();
			this.gameSpawnData = new GameSpawnData();
			this.world = new Chunk();
			this.voronoiTree = new Tree(0);
		}

		// Token: 0x04004E7F RID: 20095
		public int globalWorldSeed;

		// Token: 0x04004E80 RID: 20096
		public int globalWorldLayoutSeed;

		// Token: 0x04004E81 RID: 20097
		public int globalTerrainSeed;

		// Token: 0x04004E82 RID: 20098
		public int globalNoiseSeed;

		// Token: 0x04004E83 RID: 20099
		public int chunkEdgeSize = 32;

		// Token: 0x04004E84 RID: 20100
		public WorldLayout worldLayout;

		// Token: 0x04004E85 RID: 20101
		public List<TerrainCell> terrainCells;

		// Token: 0x04004E86 RID: 20102
		public List<TerrainCell> overworldCells;

		// Token: 0x04004E87 RID: 20103
		public List<ProcGen.River> rivers;

		// Token: 0x04004E88 RID: 20104
		public GameSpawnData gameSpawnData;

		// Token: 0x04004E89 RID: 20105
		public Chunk world;

		// Token: 0x04004E8A RID: 20106
		public Tree voronoiTree;

		// Token: 0x04004E8B RID: 20107
		public AxialI clusterLocation;
	}
}
