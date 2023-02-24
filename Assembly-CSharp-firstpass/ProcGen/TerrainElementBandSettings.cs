using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004D6 RID: 1238
	[Serializable]
	public class TerrainElementBandSettings
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x00074F88 File Offset: 0x00073188
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x00074F90 File Offset: 0x00073190
		public Dictionary<string, ElementBandConfiguration> BiomeBackgroundElementBandConfigurations { get; private set; }

		// Token: 0x06003539 RID: 13625 RVA: 0x00074F99 File Offset: 0x00073199
		public TerrainElementBandSettings()
		{
			this.BiomeBackgroundElementBandConfigurations = new Dictionary<string, ElementBandConfiguration>();
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x00074FAC File Offset: 0x000731AC
		public string[] GetNames()
		{
			string[] array = new string[this.BiomeBackgroundElementBandConfigurations.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<string, ElementBandConfiguration> keyValuePair in this.BiomeBackgroundElementBandConfigurations)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}
	}
}
