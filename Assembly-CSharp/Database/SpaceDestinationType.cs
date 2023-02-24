using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Database
{
	// Token: 0x02000CB1 RID: 3249
	[DebuggerDisplay("{Id}")]
	public class SpaceDestinationType : Resource
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060065E3 RID: 26083 RVA: 0x00270EBC File Offset: 0x0026F0BC
		// (set) Token: 0x060065E4 RID: 26084 RVA: 0x00270EC4 File Offset: 0x0026F0C4
		public int maxiumMass { get; private set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060065E5 RID: 26085 RVA: 0x00270ECD File Offset: 0x0026F0CD
		// (set) Token: 0x060065E6 RID: 26086 RVA: 0x00270ED5 File Offset: 0x0026F0D5
		public int minimumMass { get; private set; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060065E7 RID: 26087 RVA: 0x00270EDE File Offset: 0x0026F0DE
		public float replishmentPerCycle
		{
			get
			{
				return 1000f / (float)this.cyclesToRecover;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060065E8 RID: 26088 RVA: 0x00270EED File Offset: 0x0026F0ED
		public float replishmentPerSim1000ms
		{
			get
			{
				return 1000f / ((float)this.cyclesToRecover * 600f);
			}
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x00270F04 File Offset: 0x0026F104
		public SpaceDestinationType(string id, ResourceSet parent, string name, string description, int iconSize, string spriteName, Dictionary<SimHashes, MathUtil.MinMax> elementTable, Dictionary<string, int> recoverableEntities = null, ArtifactDropRate artifactDropRate = null, int max = 64000000, int min = 63994000, int cycles = 6, bool visitable = true)
			: base(id, parent, name)
		{
			this.typeName = name;
			this.description = description;
			this.iconSize = iconSize;
			this.spriteName = spriteName;
			this.elementTable = elementTable;
			this.recoverableEntities = recoverableEntities;
			this.artifactDropTable = artifactDropRate;
			this.maxiumMass = max;
			this.minimumMass = min;
			this.cyclesToRecover = cycles;
			this.visitable = visitable;
		}

		// Token: 0x04004A1C RID: 18972
		public const float MASS_TO_RECOVER = 1000f;

		// Token: 0x04004A1D RID: 18973
		public string typeName;

		// Token: 0x04004A1E RID: 18974
		public string description;

		// Token: 0x04004A1F RID: 18975
		public int iconSize = 128;

		// Token: 0x04004A20 RID: 18976
		public string spriteName;

		// Token: 0x04004A21 RID: 18977
		public Dictionary<SimHashes, MathUtil.MinMax> elementTable;

		// Token: 0x04004A22 RID: 18978
		public Dictionary<string, int> recoverableEntities;

		// Token: 0x04004A23 RID: 18979
		public ArtifactDropRate artifactDropTable;

		// Token: 0x04004A24 RID: 18980
		public bool visitable;

		// Token: 0x04004A27 RID: 18983
		public int cyclesToRecover;
	}
}
