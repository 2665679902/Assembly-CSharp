using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004D7 RID: 1239
	public class BiomeSettings : IMerge<BiomeSettings>
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x00075020 File Offset: 0x00073220
		// (set) Token: 0x0600353C RID: 13628 RVA: 0x00075028 File Offset: 0x00073228
		public ComposableDictionary<string, ElementBandConfiguration> TerrainBiomeLookupTable { get; private set; }

		// Token: 0x0600353D RID: 13629 RVA: 0x00075031 File Offset: 0x00073231
		public BiomeSettings()
		{
			this.TerrainBiomeLookupTable = new ComposableDictionary<string, ElementBandConfiguration>();
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x00075044 File Offset: 0x00073244
		public string[] GetNames()
		{
			string[] array = new string[this.TerrainBiomeLookupTable.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<string, ElementBandConfiguration> keyValuePair in this.TerrainBiomeLookupTable)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000750B4 File Offset: 0x000732B4
		public BiomeSettings Merge(BiomeSettings other)
		{
			this.TerrainBiomeLookupTable.Merge(other.TerrainBiomeLookupTable);
			return this;
		}
	}
}
